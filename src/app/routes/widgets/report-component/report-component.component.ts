import { FlatTreeControl } from '@angular/cdk/tree';
import { ChangeDetectorRef, Component, Inject, NgModule } from '@angular/core';
import { NzTreeFlatDataSource, NzTreeFlattener } from 'ng-zorro-antd/tree-view';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { auditTime, catchError, map } from 'rxjs/operators';
import { ReportService } from './service/report.service';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from "./entitie/report";
import { Router, RouterModule, Routes } from '@angular/router';
import { ReportViewerComponent } from '../report-viewer/report-viewer.component';
import { ReportFilterPipe } from './FilterPipe';

//TODO: Hacer una limpieza completa y acomodamiento completo de todo el codigo TS y LESS que tenga lineas de codigo basuca que no sirven para nada.

// const routes: Routes = [
//   {
//     path: 'report', component: this, children: [
//       { path: 'create', component: this },
//       { path: 'edit/:id', component: this },
//       { path: ':id', component: ReportViewerComponent }
//     ]
//   }
// ];

// import {
//   NzTableFilterFn,
//   NzTableFilterList,
//   NzTableSortFn,
//   NzTableSortOrder
// } from 'ng-zorro-antd/table';
// import { element } from 'protractor';

// interface ColumnItem {
//   name: string;
//   sortOrder: NzTableSortOrder | null;
//   sortFn: NzTableSortFn<any> | null;
//   listOfFilter: NzTableFilterList;
//   filterFn: NzTableFilterFn<any> | null;
//   filterMultiple: boolean;
//   sortDirections: NzTableSortOrder[];
//   width: string;
// }

interface TreeNode {
  name: string;
  children?: TreeNode[];
}

const TREE_DATA: TreeNode[] = [
  {
    name: '0-0',
    children: [{ name: '0-0-0' }, { name: '0-0-1' }, { name: '0-0-2' }]
  },
  {
    name: '0-1',
    children: [
      {
        name: '0-1-0',
        children: [{ name: '0-1-0-0' }, { name: '0-1-0-1' }]
      },
      {
        name: '0-1-1',
        children: [{ name: '0-1-1-0' }, { name: '0-1-1-1' }]
      }
    ]
  }
];

interface FlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

class FilteredTreeResult {
  constructor(
    public treeData: TreeNode[],
    public needsToExpanded: TreeNode[] = []
  ) { }
}

/**
 * From https://stackoverflow.com/a/45290208/6851836
 */
function filterTreeData(data: TreeNode[], value: string): FilteredTreeResult {
  const needsToExpanded = new Set<TreeNode>();
  const _filter = (node: TreeNode, result: TreeNode[]): TreeNode[] => {
    if (node.name.search(value) !== -1) {
      result.push(node);
      return result;
    }
    if (Array.isArray(node.children)) {
      const nodes = node.children.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
      if (nodes.length) {
        const parentNode = { ...node, children: nodes };
        needsToExpanded.add(parentNode);
        result.push(parentNode);
      }
    }
    return result;
  };
  const treeData = data.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
  return new FilteredTreeResult(treeData, [...needsToExpanded]);
}

interface FoodNode {
  name: string;
  disabled?: boolean;
  children?: FoodNode[];
}
/** Flat node with expandable and level information */
interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  disabled: boolean;
}
@Component({
  selector: 'app-report-component',
  templateUrl: './report-component.component.html',
  styleUrls: ['./report-component.component.less'],
})



// @NgModule({
//   imports: [RouterModule.forChild(routes)],
//   exports: [RouterModule]
// })

export class ReportComponentComponent {
  ReportsList: Report[] = [];
  flatNodeMap = new Map<FlatNode, TreeNode>();
  nestedNodeMap = new Map<TreeNode, FlatNode>();
  expandedNodes: TreeNode[] = [];
  searchValue = '';
  originData$ = new BehaviorSubject(TREE_DATA);
  searchValue$ = new BehaviorSubject<string>('');
  currentReport: Report = new Report({});



  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private RService: ReportService,
    private cdr: ChangeDetectorRef, private router: Router) {



    this.filteredData$.subscribe(result => {
      this.dataSource.setData(result.treeData);

      const hasSearchValue = !!this.searchValue;
      if (hasSearchValue) {
        if (this.expandedNodes.length === 0) {
          this.expandedNodes = this.treeControl.expansionModel.selected;
          this.treeControl.expansionModel.clear();
        }
        this.treeControl.expansionModel.select(...result.needsToExpanded);
      } else {
        if (this.expandedNodes.length) {
          this.treeControl.expansionModel.clear();
          this.treeControl.expansionModel.select(...this.expandedNodes);
          this.expandedNodes = [];
        }
      }
    });



  }

  ngOnInit(): void {

    this.GetReports();

    this.cdr.detectChanges();
  }

  transformer = (node: TreeNode, level: number): FlatNode => {
    const existingNode = this.nestedNodeMap.get(node);
    const flatNode =
      existingNode && existingNode.name === node.name
        ? existingNode
        : {
          expandable: !!node.children && node.children.length > 0,
          name: node.name,
          level
        };
    this.flatNodeMap.set(flatNode, node);
    this.nestedNodeMap.set(node, flatNode);
    return flatNode;
  };

  treeControl = new FlatTreeControl<FlatNode, TreeNode>(
    node => node.level,
    node => node.expandable,
    {
      trackBy: flatNode => this.flatNodeMap.get(flatNode)!
    }
  );

  treeFlattener = new NzTreeFlattener<TreeNode, FlatNode, TreeNode>(
    this.transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );

  dataSource = new NzTreeFlatDataSource(this.treeControl, this.treeFlattener);

  filteredData$ = combineLatest([
    this.originData$,
    this.searchValue$.pipe(
      auditTime(300),
      map(value => (this.searchValue = value))
    )
  ]).pipe(map(([data, value]) => (value ? filterTreeData(data, value) : new FilteredTreeResult(data))));


  hasChild = (_: number, node: FlatNode): boolean => node.expandable;

  private GetReports() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid']
      };

      this.RService._GetReports(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      ).subscribe((data: any) => {

        var datos = JSON.parse(data);
        this.ReportsList = datos.map((item: any) => new Report(item));

      });
    }
  }

  viewReport(report: Report) {

    this.router.navigate(['/report', { id: report.ID }]);
    //this.router.navigate([`/report/${report.ID}`]);
    //this.router.navigate(["/report/"], { queryParams: { id: report.ID } });
  }
  search(searchValue: string): void {
    this.searchValue = searchValue;
  }


  // objectKeys(obj: any): string[] {
  //   return Object.keys(obj);
  // }

  // OpenReport(report: Report) {
  //   this.currentReport = report;
  //   const tokenData = this.tokenService.get();
  //   let genericRequest = {};

  //   if (tokenData != null) {
  //     genericRequest = {
  //       UserId: tokenData['userid'],
  //       Params: {
  //         Query: report.Query
  //       }
  //     };

  //     this.RService.GetResultsReportQuery(genericRequest).pipe(
  //       catchError(error => {
  //         console.error('Error al obtener datos:', error);
  //         throw error;
  //       })
  //     )
  //       .subscribe((data: any) => {
  //         var ObjectData = JSON.parse(data);
  //         this.listOfColumns = [];
  //         this.listOfData = [];
  //         this.cdr.detectChanges();

  //         ObjectData.ListColumns.forEach((element: any) => {
  //           var columnWidth = "150px";

  //           //TODO: Hacer esto dinamico
  //           if (element.ColumnName == "Descripcion") {
  //             columnWidth = "600px";
  //           }

  //           var newColumn = {
  //             name: element.ColumnName,
  //             sortOrder: null,
  //             sortFn: null,
  //             sortDirections: [null],
  //             filterMultiple: false,
  //             listOfFilter: [],
  //             // filterFn: (list: string[], item: any) => list.some(name => item[element.ColumnName].indexOf(name) !== -1)
  //             filterFn: null,
  //             width: columnWidth
  //           }

  //           this.listOfColumns.push(newColumn);
  //         });

  //         var newRow: any = [];



  //         ObjectData.RowHashtable.forEach((element: any) => {
  //           ObjectData.ListColumns.forEach((column: any) => {
  //             newRow[column.ColumnName] = element[column.ColumnName];
  //           });

  //           this.listOfData.push(newRow);

  //         });

  //         this.cdr.detectChanges();
  //       });
  //   }
  // }

  // private sortFnByGrid(element: any) {
  //   return (a: any, b: any) => {
  //     const aValue = a[element.ColumnName];
  //     const bValue = b[element.ColumnName];

  //     if (aValue < bValue) {
  //       return -1;
  //     } else if (aValue > bValue) {
  //       return 1;
  //     } else {
  //       return 0;
  //     }
  //   };
  // }
}



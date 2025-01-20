import { FlatTreeControl } from '@angular/cdk/tree';
import { ChangeDetectorRef, Component, Inject } from '@angular/core';
import { NzTreeFlatDataSource, NzTreeFlattener } from 'ng-zorro-antd/tree-view';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { auditTime, catchError, map } from 'rxjs/operators';
import { ReportService } from './service/report.service';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from "./entitie/report";
//TODO: Hacer una limpieza completa y acomodamiento completo de todo el codigo TS y LESS que tenga lineas de codigo basuca que no sirven para nada.



import {
  NzTableFilterFn,
  NzTableFilterList,
  NzTableSortFn,
  NzTableSortOrder
} from 'ng-zorro-antd/table';
import { element } from 'protractor';



interface ColumnItem {
  name: string;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn<any> | null;
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn<any> | null;
  filterMultiple: boolean;
  sortDirections: NzTableSortOrder[];
  width: string;
}

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

export class ReportComponentComponent {
  ReportsList: Report[] = [];
  flatNodeMap = new Map<FlatNode, TreeNode>();
  nestedNodeMap = new Map<TreeNode, FlatNode>();
  expandedNodes: TreeNode[] = [];
  searchValue = '';
  originData$ = new BehaviorSubject(TREE_DATA);
  searchValue$ = new BehaviorSubject<string>('');
  currentReport: Report = new Report({});





  listOfColumns: ColumnItem[] = [
    // {
    //   name: 'Name',
    //   sortOrder: null,
    //   sortFn: (a: any, b: any) => a.name.localeCompare(b.name),
    //   sortDirections: ['ascend', 'descend', null],
    //   filterMultiple: true,
    //   listOfFilter: [],
    //   // filterFn: (list: string[], item: any) => list.some(name => item.name.indexOf(name) !== -1)
    //   filterFn: null,
    //   width: "120px"
    // },
    // {
    //   name: 'Age',
    //   sortOrder: 'descend',
    //   sortFn: (a: any, b: any) => a.age - b.age,
    //   sortDirections: ['descend', null],
    //   listOfFilter: [],
    //   filterFn: null,
    //   filterMultiple: true,
    //   width: "120px"
    // },
    // {
    //   name: 'Address',
    //   sortOrder: null,
    //   sortDirections: ['ascend', 'descend', null],
    //   sortFn: (a: any, b: any) => a.address.length - b.address.length,
    //   filterMultiple: false,
    //   listOfFilter: [],
    //   filterFn: (address: string, item: any) => item.address.indexOf(address) !== -1,
    //   width: "120px"
    // }
  ];
  listOfData: any[] = [
    // {
    //   name: 'John Brown',
    //   age: 32,
    //   address: 'New York No. 1 Lake Park'
    // },
    // {
    //   name: 'Jim Green',
    //   age: 42,
    //   address: 'London No. 1 Lake Park'
    // },
    // {
    //   name: 'Joe Black',
    //   age: 32,
    //   address: 'Sidney No. 1 Lake Park'
    // },
    // {
    //   name: 'Jim Red',
    //   age: 32,
    //   address: 'London No. 2 Lake Park'
    // }
  ];


  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private RService: ReportService,
    private cdr: ChangeDetectorRef) {

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

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  OpenReport(report: Report) {
    this.currentReport = report;
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Query: report.Query
        }
      };

      this.RService.GetResultsReportQuery(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      )
        .subscribe((data: any) => {
          var ObjectData = JSON.parse(data);
          this.listOfColumns = [];
          this.listOfData = [];
          this.cdr.detectChanges();

          ObjectData.ListColumns.forEach((element: any) => {
            var columnWidth = "150px";

            //TODO: Hacer esto dinamico
            if (element.ColumnName == "Descripcion") {
              columnWidth = "600px";
            }

            var newColumn = {
              name: element.ColumnName,
              sortOrder: null,
              sortFn: null,
              sortDirections: [null],
              filterMultiple: false,
              listOfFilter: [],
              // filterFn: (list: string[], item: any) => list.some(name => item[element.ColumnName].indexOf(name) !== -1)
              filterFn: null,
              width: columnWidth
            }

            this.listOfColumns.push(newColumn);
          });

          var newRow: any = [];



          ObjectData.RowHashtable.forEach((element: any) => {
            ObjectData.ListColumns.forEach((column: any) => {
              newRow[column.ColumnName] = element[column.ColumnName];
            });

            this.listOfData.push(newRow);

          });

          this.cdr.detectChanges();
        });
    }
  }



  private sortFnByGrid(element: any) {
    return (a: any, b: any) => {
      const aValue = a[element.ColumnName];
      const bValue = b[element.ColumnName];

      if (aValue < bValue) {
        return -1;
      } else if (aValue > bValue) {
        return 1;
      } else {
        return 0;
      }
    };
  }
}

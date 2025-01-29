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
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';

//TODO: Hacer una limpieza completa y acomodamiento completo de todo el codigo TS y LESS que tenga lineas de codigo basuca que no sirven para nada.


interface TreeNode {
  name: string;
  currentReport: Report;
  children: TreeNode[];
}


const TREE_DATA: TreeNode[] = [];

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


// function filterTreeData(data: TreeNode[], value: string): FilteredTreeResult {
//   const needsToExpanded = new Set<TreeNode>();
//   const _filter = (node: TreeNode, result: TreeNode[]): TreeNode[] => {
//     if (node.name.search(value) !== -1) {
//       result.push(node);
//       return result;
//     }
//     if (Array.isArray(node.children)) {
//       const nodes = node.children.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
//       if (nodes.length) {
//         const parentNode = { ...node, children: nodes };
//         needsToExpanded.add(parentNode);
//         result.push(parentNode);
//       }
//     }
//     return result;
//   };
//   const treeData = data.reduce((a, b) => _filter(b, a), [] as TreeNode[]);
//   return new FilteredTreeResult(treeData, [...needsToExpanded]);
// }

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
  TREE_DATA?: TreeNode[];

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService, private cdr: ChangeDetectorRef,
    private router: Router, private modal: NzModalService) {

    // this.filteredData$.subscribe(result => {
    //   this.dataSource.setData(result.treeData);

    //   const hasSearchValue = !!this.searchValue;
    //   if (hasSearchValue) {
    //     if (this.expandedNodes.length === 0) {
    //       this.expandedNodes = this.treeControl.expansionModel.selected;
    //       this.treeControl.expansionModel.clear();
    //     }
    //     this.treeControl.expansionModel.select(...result.needsToExpanded);
    //   } else {
    //     if (this.expandedNodes.length) {
    //       this.treeControl.expansionModel.clear();
    //       this.treeControl.expansionModel.select(...this.expandedNodes);
    //       this.expandedNodes = [];
    //     }
    //   }
    // });
  }

  ngOnInit(): void {

    this.GetReports();

    this.cdr.detectChanges();
  }

  // transformer = (node: TreeNode, level: number): FlatNode => {
  //   const existingNode = this.nestedNodeMap.get(node);
  //   const flatNode =
  //     existingNode && existingNode.name === node.name
  //       ? existingNode
  //       : {
  //         expandable: !!node.children && node.children.length > 0,
  //         name: node.name,
  //         level
  //       };
  //   this.flatNodeMap.set(flatNode, node);
  //   this.nestedNodeMap.set(node, flatNode);
  //   return flatNode;
  // };

  treeControl = new FlatTreeControl<FlatNode, TreeNode>(
    node => node.level,
    node => node.expandable,
    {
      trackBy: flatNode => this.flatNodeMap.get(flatNode)!
    }
  );

  // treeFlattener = new NzTreeFlattener<TreeNode, FlatNode, TreeNode>(
  //   this.transformer,
  //   node => node.level,
  //   node => node.expandable,
  //   node => node.children
  // );

  // dataSource = new NzTreeFlatDataSource(this.treeControl, this.treeFlattener);

  // filteredData$ = combineLatest([
  //   this.originData$,
  //   this.searchValue$.pipe(
  //     auditTime(300),
  //     map(value => (this.searchValue = value))
  //   )
  // ]).pipe(map(([data, value]) => (value ? filterTreeData(data, value) : new FilteredTreeResult(data))));

  private GetReports() {
    this.TREE_DATA = [];
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

        debugger;

        var datos: Report[] = JSON.parse(data);

        var emi = datos.reduce((acc, item) => {
          if (!acc[item.Category]) {
            acc[item.Category] = [];
          }
          acc[item.Category].push(item);
          return acc;
        }, {} as { [key: string]: Report[] })


        //TODO: hacer este proceso mas performante, solo pasando los datos deseados y no todo el objeto.
        // this.TREE_DATA = Object.keys(emi).map(category => ({
        //   name: category,
        //   children: emi[category].map(item => ({ currentReport: new Report(item) }))
        // }));


        // this.TREE_DATA = Object.keys(emi).map(category => ({
        //   name: category,
        //   children: emi[category].map(item => ({ currentReport: new Report(item) }))
        // }));





        this.ReportsList = datos.map((item: any) => new Report(item));




      });
    }
  }

  deleteReport(report: Report): void {
    this.modal.confirm({
      nzTitle: 'Are you sure delete this task?',
      nzContent: '<b style="color: red;">Some descriptions</b>',
      nzOkText: 'Yes',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzOnOk: () => console.log('OK'),
      nzCancelText: 'No',
      nzOnCancel: () => console.log('Cancel')
    });

    this.RService.deleteReport(report).pipe().subscribe((data: any) => {
      var data = JSON.parse(data);
      var itemId = data.ID;

      this.QuitarItemDeLaLista(itemId);
    });


  }

  search(searchValue: string): void {
    this.searchValue = searchValue;
  }

  QuitarItemDeLaLista(itemId: any) {
    this.ReportsList = this.ReportsList.filter(report => report.ID !== itemId);
    this.cdr.detectChanges();
  }


}


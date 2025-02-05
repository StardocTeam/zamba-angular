import { ChangeDetectorRef, Component, ElementRef, HostListener, Inject, NgModule, QueryList, Renderer2, ViewChildren } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ReportService } from './service/report.service';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from "./entitie/report";
import { Router } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';

export interface TreeNode {
  name: string;
  currentReport?: Report[];
}

@Component({
  selector: 'app-report-component',
  templateUrl: './report-component.component.html',
  styleUrls: ['./report-component.component.less'],
})

export class ReportComponentComponent {
  @ViewChildren('itemTree') itemTrees!: QueryList<ElementRef>;
  @ViewChildren('itemLeaf') itemLeafs!: QueryList<ElementRef>;
  ReportsList: Report[] = [];
  searchValue = '';
  TREE_DATA?: TreeNode[];
  CreatePermission: boolean = false;
  height: number = 400;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService, private cdr: ChangeDetectorRef,
    private router: Router, private modal: NzModalService) {

  }

  ngOnInit(): void {
    this.adjustHeight();
    this.GetPermissions();
    this.GetReports();
    this.cdr.detectChanges();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.adjustHeight();
  }

  adjustHeight() {
    const height = window.innerHeight;
    const reportContainer = document.getElementById('firstDiv');

    this.height = height - 64;
  }


  GetPermissions() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid']
      };

      this.RService._GetPermissions(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      ).subscribe((data: any) => {
        var data = JSON.parse(data);

        this.CreatePermission = data[0]["ADITIONAL"] == -1 ? true : false;
      });
    }

    this.cdr.detectChanges();
  }

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
        var datos: Report[] = JSON.parse(data);
        var Categories = datos.reduce((acc, item) => {
          if (!acc[item.Category]) {
            acc[item.Category] = [];
          }
          acc[item.Category].push(item);
          return acc;
        }, {} as { [key: string]: Report[] })


        //TODO: hacer este proceso mas performante, solo pasando los datos deseados y no todo el objeto.
        this.TREE_DATA = Object.keys(Categories).map(category => ({
          name: category,
          currentReport: Categories[category].map(item => new Report(item))
        }));

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
  QuitarItemDeLaLista(itemId: any) {
    this.ReportsList = this.ReportsList.filter(report => report.ID !== itemId);
    this.cdr.detectChanges();
  }

  search(searchValue: string): void {
    this.searchValue = searchValue;

    this.TREE_DATA?.forEach(node => {
      var filteredReports = node.currentReport?.filter(report => report.Name.toLowerCase().includes(this.searchValue.toLowerCase())) || [];

      this.itemTrees.forEach((itemTree: any) => {
        if (node.name == itemTree.cdkOverlayOrigin.nativeElement.textContent && filteredReports.length == 0) {
          itemTree.cdkOverlayOrigin.nativeElement.style.display = 'none';
        } else if (node.name == itemTree.cdkOverlayOrigin.nativeElement.textContent && filteredReports.length > 0) {
          itemTree.cdkOverlayOrigin.nativeElement.style.display = 'block';
        }
      });
    });
  }
}
import { ChangeDetectorRef, Component, ElementRef, HostListener, inject, Inject, NgModule, QueryList, Renderer2, ViewChild, ViewChildren } from '@angular/core';
import { BehaviorSubject, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { ReportService } from './service/report.service';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from "./entitie/report";
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { NzModalService } from 'ng-zorro-antd/modal';
import { ZambaService } from 'src/app/services/zamba/zamba.service';
import { GridService } from 'src/app/services/Grid/grid.service';

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
  private route = inject(ActivatedRoute);
  @ViewChild('outlet') outlet!: RouterOutlet;
  @ViewChildren('itemTree') itemTrees!: QueryList<ElementRef>;
  @ViewChildren('itemLeaf') itemLeafs!: QueryList<ElementRef>;
  ReportsList: Report[] = [];
  searchValue = '';
  TREE_DATA?: TreeNode[];
  isDashboardVisible: boolean = true;

  ViewPermission: boolean = false;
  UpdatePermission: boolean = false;
  DeletePermission: boolean = false;
  CreatePermission: boolean = true;
  ConsultPermission: boolean = false;

  height: number = 400;
  XsReportListFlag: boolean = true;
  XsReportViewerFlag: boolean = false;
  userId: number = 0;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService, private cdr: ChangeDetectorRef,
    private router: Router, private modal: NzModalService, private zambaService: ZambaService,
    private GService: GridService) {
  }

  ngOnInit() {

    this.route.queryParamMap.subscribe(params => {
      if (params) {
        // var userIdParam: string | null;
        var tokenParam: string | null;

        this.tokenService.set({ token: params.get('t') });

        if (params.get('t')) {
          tokenParam = params.get('t');
          this.fetchUserIdWithToken(tokenParam);
        }

      } else {
        //TODO: hacer un mensaje visual.
        throw new Error('Token not found');
      }
    });
  }

  ngAfterViewInit() {
    const childComponent = this.outlet.component as { createTerminated?: any };
    if (childComponent && childComponent.createTerminated) {
      childComponent.createTerminated.subscribe((data: any) => {

        console.log('Evento recibido del hijo:', data);
        this.GetReports();

      });
    }
  }

  private initializeReportComponents() {
    this.GetPermissions();
    this.GetReports();
    this.adjustHeight();
    this.cdr.detectChanges();
  }


  //#region Bussines Functions
  private GetPermissions() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token']
      };

      this.RService._GetPermissions(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      ).subscribe((data: any) => {
        var data = JSON.parse(data);
        if (data) {
          this.ViewPermission = true;
          //this.UpdatePermission = true;
          //this.DeletePermission = true;
          //this.CreatePermission = true;
          //this.ConsultPermission = true;

          this.cdr.detectChanges();
        } else {
          console.warn('No permissions found.');
        }
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
        UserId: tokenData['userid'],
        token: tokenData['token']
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
  private fetchUserIdWithToken(tokenParam: string | null) {

    let genericRequest = {
      UserId: 0,
      token: tokenParam
    };

    this.zambaService.getUserId(genericRequest).pipe(
      tap(response => {
        response = JSON.parse(response);
        this.tokenService.set({ token: tokenParam, userid: response });

        this.initializeReportComponents();
      }),
      catchError(error => {
        console.error('Error fetching task name:', error);
        return of([]);
      })
    ).subscribe();
  }


  exportToExcel(report: Report): void {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          "Query": report.Query
        }
      };

      const FileName = report.Name.replace(/ /g, "_");

      this.GService.ExportToExcel(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      ).subscribe((data: any) => {
        if (!data) {
          console.error('Error: No data received for export.');

          this.modal.info({
            nzTitle: 'Ocurrio un error',
            nzContent: '<p>No hay resultados</p>',
            nzOkText: 'OK',
            nzOkType: 'primary',
            nzOnOk: () => console.log('OK'),
          });

          return;
        }

        var dataBase64 = 'data:application/octet-stream;base64,' + data;

        const now = new Date();
        const formattedDate = `${now.getFullYear()}-${(now.getMonth() + 1).toString().padStart(2, '0')}-${now.getDate().toString().padStart(2, '0')}`;
        const formattedTime = (`${now.getHours().toString().padStart(2, '0')}:${now.getMinutes().toString().padStart(2, '0')}:${now.getSeconds().toString().padStart(2, '0')}`).replace(':', '_');

        //
        const url = dataBase64;
        const a = document.createElement('a');
        a.href = url;
        a.download = FileName + " " + formattedDate + " " + formattedTime + ".xlsx";
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        this.cdr.detectChanges();
      });
    }
  }


  deleteReport(report: Report): void {
    if (this.DeletePermission == true) {
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

      // this.RService.deleteReport(report).pipe().subscribe((data: any) => {
      //   var data = JSON.parse(data);
      //   var itemId = data.ID;

      //   this.QuitarItemDeLaLista(itemId);
      // });
    }
  }

  QuitarItemDeLaLista(itemId: any) {
    this.ReportsList = this.ReportsList.filter(report => report.ID !== itemId);
    this.cdr.detectChanges();
  }


  //#endregion



  //#region Visual Management
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.adjustHeight();
  }

  adjustHeight() {
    const height = window.innerHeight;
    const reportContainer = document.getElementById('firstDiv');

    this.height = height - 64;
  }

  switchView(view: string) {
    switch (view.toLowerCase()) {
      case 'list':
        this.XsReportListFlag = true;
        this.XsReportViewerFlag = false;
        break;
      case 'viewer':
        this.XsReportListFlag = false;
        this.XsReportViewerFlag = true;
        break;
    }
  }

  //#endregion


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

  navigateToCreate() {
    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
    }

    this.router.navigate(['/tools/reports/create'], { queryParams });
  }

  navigateToEdit(reportId: number) {
    if (this.UpdatePermission == true) {
      const tokenData = this.tokenService.get();
      const queryParams: any = {};

      if (tokenData && tokenData['token']) {
        queryParams.t = tokenData['token'];
      }

      this.router.navigate(['/tools/reports/edit/' + reportId], { queryParams });
    }
  }

  navigateToView(reportId: number) {
    // Navega dinámicamente a la ruta con el ID del reporte
    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
    }

    this.router.navigate(['/tools/reports/view', reportId], { queryParams });
  }

  viewCharts(reportId: number) {

    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
    }

    if (reportId && reportId != 0) {
      queryParams.reportId = reportId.toString();
    }

    this.router.navigate(['/tools/reports/chartcontainer', reportId], { queryParams });
  }

  ReloadList() {
    this.GetReports();
    this.adjustHeight();
    this.cdr.detectChanges();
  }
}
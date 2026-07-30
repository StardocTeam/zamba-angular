import {
  ChangeDetectorRef,
  Component,
  ElementRef,
  HostListener,
  inject,
  Inject,
  NgModule,
  QueryList,
  Renderer2,
  ViewChild,
  ViewChildren,
} from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { NzModalService } from 'ng-zorro-antd/modal';
import { BehaviorSubject, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { GridService } from 'src/app/services/Grid/grid.service';
import { ZambaService } from 'src/app/services/zamba/zamba.service';

import { ReportViewStateDto as ReportViewState } from './entitie/ReportViewState';
import { Report } from './entitie/report';
import { ReportService } from './service/report.service';

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
  //#region Properties
  private route = inject(ActivatedRoute);
  @ViewChild('outlet') outlet!: RouterOutlet;
  @ViewChildren('itemTree') itemTrees!: QueryList<ElementRef>;
  @ViewChildren('itemLeaf') itemLeafs!: QueryList<ElementRef>;
  ReportsList: Report[] = [];
  searchValue = '';
  TREE_DATA?: TreeNode[];
  isDashboardVisible: boolean = true;

  ViewPermission: boolean = false;
  UpdatePermission: boolean = true;
  DeletePermission: boolean = true;
  CreatePermission: boolean = true;
  ConsultPermission: boolean = false;

  height: number = 400;
  XsReportListFlag: boolean = true;
  XsReportViewerFlag: boolean = false;
  userId: number = 0;

  chartsDisabled: boolean = false;
  isLoading: boolean = false;
  //#endregion

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService,
    private cdr: ChangeDetectorRef,
    private router: Router,
    private modal: NzModalService,
    private zambaService: ZambaService,
    private GService: GridService,
  ) { }

  private initialized = false;

  //#region ngOnInit
  ngOnInit() {
    this.route.queryParamMap.subscribe(params => {
      if (params && params.keys.length > 0) {
        if (params.get('t')) {
          this.tokenService.set({ token: params.get('t') });
          this.fetchUserIdWithToken();
        } else {
          console.error('Error: Token no encontrado en los parámetros');
          this.modal.error({
            nzTitle: 'Token no encontrado',
            nzContent: '<p>No se pudo encontrar el token de autenticación. Por favor, inicie sesión nuevamente.</p>',
            nzOkText: 'OK',
            nzOkType: 'primary',
            nzOnOk: () => console.log('OK'),
          });

          throw new Error('Token not found');
        }
      } else if (this.tokenService && this.tokenService.get()?.token) {
        this.fetchUserIdWithToken();
      } else {
        console.error('Error: Token no encontrado en los parámetros ni en el servicio');
        this.modal.error({
          nzTitle: 'Token no encontrado',
          nzContent: '<p>No se pudo encontrar el token de autenticación. Por favor, inicie sesión nuevamente.</p>',
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });

        throw new Error('Token not found');
      }
    });
  }
  //#endregion

  //#region ngAfterViewInit
  ngAfterViewInit() {
    const childComponent = this.outlet.component as { createTerminated?: any };
    if (childComponent && childComponent.createTerminated) {
      childComponent.createTerminated.subscribe((data: any) => {
        console.log('Evento recibido del hijo:', data);
        this.GetReports();
      });
    } else {
      console.log('Evento - NO - recibido del hijo:');
    }
  }
  //#endregion

  //TODO: Revisar este metodo, posiblemente se retire con el arreglo del ticket 1449
  onChildActivate(componentRef: any) {
    if (componentRef && componentRef.createTerminated) {
      componentRef.createTerminated.subscribe(() => {
        console.log('Evento recibido del hermano:');
        this.GetReports();
      });
    } else {
      console.log('Evento - NO - recibido del hermano:');
    }
  }

  private initializeReportComponents() {
    this.GetPermissions();
    this.isLoading = true;
    this.GetReports();
    this.adjustHeight();
    this.cdr.detectChanges();

    this.getViewLastReport();
  }

  private getViewLastReport() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
      };
    }

    this.RService.GetLastReportViewed(genericRequest)
      .pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        }),
      )
      .subscribe((data: ReportViewState) => {
        if (data && data.LastReportIdView && data.LastReportIdView != 0) {
          if (data.ReportViewMode === 'Chart') {
            this.viewCharts(data.LastReportIdView);
          } else if (data.ReportViewMode === 'ResultsGrid') {
            this.navigateToView(data.LastReportIdView);
          }
        } else {
          const queryParams: any = {};

          if (tokenData?.['token']) {
            queryParams.t = tokenData['token'];
          }

          const currentUrl = this.router.url.split('?')[0].replace(/#.*$/, '');
          const cleanedBaseUrl = currentUrl
            .replace(/\/view(\/\d+)?$/, '')
            .replace(/\/create(\/\d+)?$/, '')
            .replace(/\/edit(\/\d+)?$/, '')
            .replace(/\/chartcontainer(\/\d+)?$/, '');

          this.router.navigate([cleanedBaseUrl], { queryParams });
          this.isLoading = false;
          this.cdr.detectChanges();
        }
      });
  }

  //#region Bussines Functions
  private GetPermissions() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
      };

      this.RService._GetPermissions(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          }),
        )
        .subscribe((data: any) => {
          var data = JSON.parse(data);
          if (data) {
            this.ViewPermission = true;

            this.cdr.detectChanges();
          } else {
            console.warn('No permissions found.');
          }
        });
    }

    this.cdr.detectChanges();
  }

  private GetReports() {
    //this.TREE_DATA = [];
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
      };

      this.RService._GetReports(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          }),
        )
        .subscribe((data: any) => {
          var datos: Report[] = JSON.parse(data);

          var Categories = datos.reduce(
            (acc, item) => {
              if (item.Categorydescription == null || item.Categorydescription == '') {
                item.Categorydescription = 'Sin categoria';
              }

              if (!acc[item.Categorydescription]) {
                acc[item.Categorydescription] = [];
              }
              acc[item.Categorydescription].push(item);
              return acc;
            },
            {} as { [key: string]: Report[] },
          );

          // Solo agrega los nuevos items que no existen en ReportsList
          const existingIds = new Set(this.ReportsList.map(r => r.ID));
          const nuevos = datos.filter((item: any) => !existingIds.has(item.ID)).map(item => new Report(item));
          this.ReportsList.push(...nuevos);

          // Actualiza TREE_DATA con los nuevos datos
          this.TREE_DATA = Object.keys(Categories).map(category => ({
            name: category,
            currentReport: Categories[category].map(item => new Report(item)),
          }));

          this.isLoading = false;
        });
    }
  }
  private fetchUserIdWithToken() {
    let genericRequest = {
      UserId: 0,
      token: this.tokenService.get()?.token,
    };

    this.zambaService
      .getUserId(genericRequest)
      .pipe(
        tap(response => {
          response = JSON.parse(response);
          this.tokenService.set({ token: genericRequest.token, userid: response });

          this.initializeReportComponents();
        }),
        catchError(error => {
          console.error('Error fetching task name:', error);
          return of([]);
        }),
      )
      .subscribe();
  }

  exportToExcel(report: Report): void {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          Query: report.Query,
        },
      };

      const FileName = report.Name.replace(/ /g, '_');

      this.GService.ExportToExcel(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          }),
        )
        .subscribe((data: any) => {
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

          var dataBase64 = `data:application/octet-stream;base64,${data}`;

          const now = new Date();
          const formattedDate = `${now.getFullYear()}-${(now.getMonth() + 1).toString().padStart(2, '0')}-${now
            .getDate()
            .toString()
            .padStart(2, '0')}`;
          const formattedTime = `${now.getHours().toString().padStart(2, '0')}:${now.getMinutes().toString().padStart(2, '0')}:${now
            .getSeconds()
            .toString()
            .padStart(2, '0')}`.replace(':', '_');

          //
          const url = dataBase64;
          const a = document.createElement('a');
          a.href = url;
          a.download = `${FileName} ${formattedDate} ${formattedTime}.xlsx`;
          document.body.appendChild(a);
          a.click();
          document.body.removeChild(a);

          this.cdr.detectChanges();
        });
    }
  }

  deleteReport(report: Report): void {
    const tokenData = this.tokenService.get();
    if (this.DeletePermission == true && tokenData != null) {

      var genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          ReportId: report.ID,
        },
      };


      this.modal.confirm({
        nzTitle: '¿Estás seguro de eliminar este reporte?',
        nzContent: '<b style="color: red;">Esta acción no se puede deshacer</b>',
        nzOkText: 'Sí',
        nzOkType: 'primary',
        nzOkDanger: true,
        nzOnOk: () => {

          this.RService.deleteReport(genericRequest).pipe().subscribe((data: any) => {
            var result = JSON.parse(data);

            if (result) {
              this.modal.success({
                nzTitle: 'Exito',
                nzContent: '<p>El reporte ha sido eliminado.</p>',
              });
            } else {
              this.modal.error({
                nzTitle: 'Error',
                nzContent: '<p>Ocurrió un error al eliminar el reporte.</p>',
              });
            }

            const currentUrl = this.router.url.split('?')[0];
            this.QuitarItemDeLaLista(report.ID);

            this.ReturnToReports(currentUrl, report);
          });
        },
        nzCancelText: 'No',
        nzOnCancel: () => console.log('Cancel'),
      });
    }
  }

  private ReturnToReports(currentUrl: string, report: Report) {
    if (currentUrl.endsWith(`/view/${report.ID}`) ||
      currentUrl.endsWith(`/edit/${report.ID}`) ||
      currentUrl.endsWith(`/chartcontainer/${report.ID}`)) {
      const tokenData = this.tokenService.get();
      const queryParams: any = {};

      if (tokenData?.['token']) {
        queryParams.t = tokenData['token'];
      }

      const cleanedBaseUrl = currentUrl
        .replace(/\/view(\/\d+)?$/, '')
        .replace(/\/create(\/\d+)?$/, '')
        .replace(/\/edit(\/\d+)?$/, '')
        .replace(/\/chartcontainer(\/\d+)?$/, '');

      this.router.navigate([cleanedBaseUrl], { queryParams });
    }
  }

  QuitarItemDeLaLista(itemId: any) {
    const id = Number(itemId);

    this.ReportsList = this.ReportsList.filter(report => Number(report.ID) !== id);

    this.TREE_DATA = (this.TREE_DATA ?? [])
      .map(node => ({
        ...node,
        currentReport: (node.currentReport ?? []).filter(report => Number(report.ID) !== id),
      }))
      .filter(node => (node.currentReport?.length ?? 0) > 0);

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
    //TODO: codigo par auna version responsive.
    // switch (view.toLowerCase()) {
    //   case 'list':
    //     this.XsReportListFlag = true;
    //     this.XsReportViewerFlag = false;
    //     break;
    //   case 'viewer':
    //     this.XsReportListFlag = false;
    //     this.XsReportViewerFlag = true;
    //     break;
    // }
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

    const baseUrl = this.router.url.split('?')[0].replace(/#.*$/, '');

    // Si la ruta base termina con 'view' o un id, elimínalos
    const cleanedBaseUrl = baseUrl
      .replace(/\/view(\/\d+)?$/, '')
      .replace(/\/create(\/\d+)?$/, '')
      .replace(/\/edit(\/\d+)?$/, '')
      .replace(/\/chartcontainer(\/\d+)?$/, '');

    this.router.navigate([cleanedBaseUrl, 'create'], { queryParams });
  }

  navigateToEdit(reportId: number) {
    if (this.UpdatePermission == true) {
      const tokenData = this.tokenService.get();
      const queryParams: any = {};

      if (tokenData && tokenData['token']) {
        queryParams.t = tokenData['token'];
      }

      const baseUrl = this.router.url.split('?')[0].replace(/#.*$/, '');

      // Si la ruta base termina con 'view' o un id, elimínalos
      const cleanedBaseUrl = baseUrl
        .replace(/\/view(\/\d+)?$/, '')
        .replace(/\/create(\/\d+)?$/, '')
        .replace(/\/edit(\/\d+)?$/, '')
        .replace(/\/chartcontainer(\/\d+)?$/, '');

      this.router.navigate([cleanedBaseUrl, 'edit', reportId], { queryParams });
    }
  }

  navigateToView(reportId: number) {
    // Navega dinámicamente a la ruta con el ID del reporte
    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
    }

    // Usa router.createUrlTree para obtener la ruta base sin fragmentos ni parámetros
    const baseUrl = this.router.url.split('?')[0].replace(/#.*$/, '');

    // Si la ruta base termina con 'view' o un id, elimínalos
    const cleanedBaseUrl = baseUrl
      .replace(/\/view(\/\d+)?$/, '')
      .replace(/\/create(\/\d+)?$/, '')
      .replace(/\/edit(\/\d+)?$/, '')
      .replace(/\/chartcontainer(\/\d+)?$/, '');

    this.router.navigate([cleanedBaseUrl, 'view', reportId], { queryParams });
  }

  viewCharts(reportId: number) {
    if (!this.chartsDisabled) {
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
  }

  ReloadList() {
    this.GetReports();
    this.adjustHeight();
    this.cdr.detectChanges();
  }
}

import { ChangeDetectorRef, Component, HostListener, Inject, Input, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { environment } from '@env/environment';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzSpinComponent } from 'ng-zorro-antd/spin';
import { NzTableFilterFn, NzTableFilterList, NzTableSortFn, NzTableSortOrder } from 'ng-zorro-antd/table';


import { catchError, Observable, Subscription } from 'rxjs';
import { RuleExecutorComponent } from 'src/app/components/rule-executor/rule-executor.component';
import { GridService } from 'src/app/services/Grid/grid.service';
import { TaskService } from 'src/app/services/task.service';

import { Zvars } from './entitie/ZVar';


import { ReportViewerService } from './service/report-viewer.service';
import { Report } from '../report-component/entitie/report';

@Component({
  selector: 'app-report-viewer',
  templateUrl: './report-viewer.component.html',
  styleUrls: ['./report-viewer.component.less'],
})
export class ReportViewerComponent implements OnInit, OnDestroy {
  executingRule: boolean = false;
  array = Array.from({ length: 20 }, (_, index) => index + 1);

  isLoading: Boolean = true;
  currentReport: Report = new Report({});
  allListOfData: any[] = [];
  listOfData: any[] = [];
  listOfColumns: ColumnItem[] = [];
  Description: string = '';
  height: string = '400px';
  PageIndex: number = 1;

  nzShowPagination: boolean = true;
  isButtonExcelDisabled: boolean = true;
  CanGoToCharts: boolean = true;

  ZVARstartDate: Date = new Date();
  ZVARendDate: Date = new Date();
  ListZVARsFromRule: any[] = [];
  ZvarList: Zvars[] = [];
  endDateVisible: boolean = false;
  startDateVisible: boolean = false;
  ruleId: any;
  /** Optional input to set the report id externally */
  @Input() reportId?: string | number;

  /** Optional observable input that, when it emits, will trigger a refresh of the report */
  @Input() refresh$?: Observable<any>;

  private routeSub?: Subscription;
  private refreshSub?: Subscription;
  public FlagOnClick: boolean = false;
  public searchValue: string = '';

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private RVService: ReportViewerService,
    private route: ActivatedRoute,
    private GService: GridService,
    private modal: NzModalService,
    private router: Router,
    private TService: TaskService,
  ) { }

  ngOnDestroy(): void {
    try {
      this.routeSub?.unsubscribe();
    } catch (e) {
      /* noop */
    }
    try {
      this.refreshSub?.unsubscribe();
    } catch (e) {
      /* noop */
    }
  }

  //#region Bussines Functions

  ngOnInit() {
    this.isLoading = true;
    const tokenData = this.tokenService.get();

    this.routeSub = this.route.params.subscribe(params => {
      let genericRequest = {};

      if (tokenData) {
        genericRequest = {
          UserId: tokenData['userid'],
          token: tokenData['token'],
          Params: {
            Id: params['id'],
          },
        };

        this.RVService.GetReportById(genericRequest)
          .pipe(
            catchError(error => {
              console.error('Error al obtener datos:', error);
              throw error;
            }),
          )
          .subscribe((data: any) => {
            this.isLoading = true;
            var currentReport: Report = JSON.parse(data)[0];
            const oneMonthAgo = new Date();
            oneMonthAgo.setMonth(oneMonthAgo.getMonth() - 1);
            this.ZVARstartDate = oneMonthAgo;
            this.ZVARendDate = new Date();
            this.cdr.detectChanges();

            this.OpenReport(new Report(currentReport));
          });
      }
    });

    // If an external reportId was passed as @Input, load it now
    if (this.reportId != null && tokenData != null) {
      const genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          Id: this.reportId,
        },
      };

      this.RVService.GetReportById(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          }),
        )
        .subscribe((data: any) => {
          var currentReport: Report = JSON.parse(data)[0];
          const oneMonthAgo = new Date();
          oneMonthAgo.setMonth(oneMonthAgo.getMonth() - 1);
          this.ZVARstartDate = oneMonthAgo;
          this.ZVARendDate = new Date();
          this.OpenReport(new Report(currentReport));
        });
    }

    // Subscribe to external refresh trigger if provided
    if (this.refresh$) {
      this.refreshSub = this.refresh$.subscribe(() => {
        this.rechargeReport();
      });
    }
  }

  OpenReport(report: Report) {
    this.ResetAllVars();

    this.currentReport = report;
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    //Este codigo detecta y arma una lista de zVars encontradas
    var zVarsFound = this.extractZvarVariables(this.currentReport.Query);

    if (zVarsFound.includes('FechaDesde')) {
      this.startDateVisible = true;
    }

    if (zVarsFound.includes('FechaHasta')) {
      this.endDateVisible = true;
    }

    this.cdr.detectChanges();

    //--------------------------------

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Query: this.currentReport.Query,
          Id: this.currentReport.ID,
        },
      };

      this.GetRuleIdToReport(genericRequest, tokenData);
    }
  }

  private ResetAllVars() {
    this.isLoading = true;
    this.FlagOnClick = false;

    this.ruleId = 0;
    this.isButtonExcelDisabled = true;
    this.listOfColumns = [];
    this.allListOfData = [];
    this.listOfData = [];

    this.startDateVisible = false;
    this.endDateVisible = false;

    this.ListZVARsFromRule = [];
  }

  private GetRuleIdToReport(genericRequest: any, tokenData: any) {
    this.RVService.GetRuleIdToReport(genericRequest)
      .pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        }),
      )
      .subscribe((Rule: any) => {
        if (Rule != null && Rule != '[]') {
          this.ruleId = JSON.parse(Rule)[0].RuleId;
        } else {
          this.ruleId = null;
        }

        if (this.ruleId && this.ruleId > 0) {
          this.TService.executeTaskRule(this.ruleId, '')
            .pipe(
              catchError(error => {
                console.error('Error al obtener datos:', error);
                throw error;
              }),
            )
            .subscribe((ZvarsData: any) => {
              const ZvarsDataParsed = JSON.parse(ZvarsData).Vars;

              if (ZvarsDataParsed && Object.prototype.hasOwnProperty.call(ZvarsDataParsed, 'tasks')) delete ZvarsDataParsed.tasks;

              var ZvarsDataArray = Object.entries(ZvarsDataParsed);

              this.ListZVARsFromRule = [];
              ZvarsDataArray.forEach(item => {
                const z = new Zvars();
                z.KeyZVar = String(item[0]);
                z.ValueZVar = String(item[1] ?? '');
                this.ListZVARsFromRule.push(z);
              });

              genericRequest = {
                UserId: tokenData['userid'],
                Params: {
                  Zvars: JSON.stringify({
                    ...this.buildZvarsObject(),
                    FechaDesde: this.ZVARstartDate,
                    FechaHasta: this.ZVARendDate,
                  }),
                  Query: this.currentReport.Query,
                  ReportId: this.currentReport.ID,
                },
              };
              this.cdr.detectChanges();

              this.GetResultsByReportId(genericRequest);
            });
        } else {
          genericRequest = {
            UserId: tokenData['userid'],
            Params: {
              Zvars: JSON.stringify({
                ...this.buildZvarsObject(),
                FechaDesde: this.ZVARstartDate,
                FechaHasta: this.ZVARendDate,
              }),
              Query: this.currentReport.Query,
              ReportId: this.currentReport.ID,
            },
          };

          this.GetResultsByReportId(genericRequest);
        }
      });
  }

  private buildZvarsObject(): any {
    const obj: any = {};
    this.ListZVARsFromRule.forEach(v => {
      if (v?.KeyZVar) {
        obj[v.KeyZVar] = v.ValueZVar;
      }
    });
    return obj;
  }

  rechargeReport() {
    this.isButtonExcelDisabled = true;
    this.listOfColumns = [];
    this.allListOfData = [];
    this.listOfData = [];
    this.isLoading = true;
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    if (tokenData != null) {
      let genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Zvars: JSON.stringify({
            ...this.buildZvarsObject(),
            FechaDesde: this.ZVARstartDate,
            FechaHasta: this.ZVARendDate,
          }),
          Query: this.currentReport.Query,
          ReportId: this.currentReport.ID,
        },
      };

      this.GetResultsByReportId(genericRequest);
    }
  }

  normalizeZvars(item: any): Zvars | null {
    if (item == null) return null;

    // Caso: { NombreVar: valor }
    if (typeof item === 'object' && !Array.isArray(item)) {
      const [propName, propValue] = Object.entries(item)[0] || [null, null];
      if (propName == null) return null;
      const z = new Zvars();
      z.KeyZVar = propName as any;
      if (propValue && typeof propValue === 'object' && !Array.isArray(propValue)) {
        z.ValueZVar = (propValue as any).Value ?? (propValue as any).value ?? (propValue as any).Val ?? (propValue as any).val ?? propValue;
      } else {
        z.ValueZVar = propValue as any;
      }
      return z;
    }

    // Valor primitivo
    const z = new Zvars();
    z.KeyZVar = item;
    z.ValueZVar = null as any;
    return z;
  }

  private GetResultsByReportId(genericRequest: {}) {
    this.RVService.GetResultsByReportId(genericRequest)
      .pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        }),
      )
      .subscribe((data: any) => {
        this.SetFinalResultOnGrid(data);
        this.executeRepeatedly(2);
        return;
      });
  }

  private SetFinalResultOnGrid(data: any) {
    if (!data) {
      this.isButtonExcelDisabled = true;

      console.error('Error al cargar el reporte');

      this.modal.error({
        nzTitle: 'No se pudo cargar el reporte',
        nzContent:
          '<p>Lo sentimos — no pudimos mostrar el reporte en este momento. Por favor intenta nuevamente en unos minutos.</p><p>Si el problema continúa, contactá a soporte indicando el nombre del reporte y la acción que realizaste.</p>',
        nzOkText: 'Aceptar',
        nzOkType: 'primary',
        nzOnOk: () => console.log('OK'),
      });

      this.isLoading = false;
      this.cdr.detectChanges();
    } else if (typeof JSON.parse(data) == 'object') {
      this.isButtonExcelDisabled = false;
      var ObjectData = JSON.parse(data);

      if (ObjectData.RowHashtable && ObjectData.ListColumns.length > 0) {
        this.cdr.detectChanges();

        ObjectData.ListColumns.forEach((ColumnName: string) => {
          var baseWidth = 10; // Factor base para el ancho (puedes ajustarlo según el diseño)
          const maxWidth = 800; // Ancho máximo permitido para una columna

          // Calcular el ancho basado en el nombre de la columna
          let columnWidth = ColumnName.length * baseWidth;
          columnWidth -= Math.floor(ColumnName.length / 10) * baseWidth;

          // Calcular el ancho basado en el valor más largo de los datos
          ObjectData.RowHashtable.forEach((row: any) => {
            const cellValue = row[ColumnName] ? row[ColumnName].toString() : '';
            var cellWidth = cellValue.length * baseWidth;

            cellWidth -= Math.floor(cellValue.length / 10) * baseWidth;

            if (cellWidth > columnWidth) {
              columnWidth = cellWidth;
            }
          });

          //Umbral de tamaño (0 to 150)
          if (columnWidth < 80) {
            columnWidth += columnWidth * 0.3;
          }
          //Umbral de tamaño (0 to 150)
          if (columnWidth > 80 && columnWidth < 150) {
            columnWidth += columnWidth * 0.2;
          }

          // Limitar el ancho al máximo permitido
          columnWidth = Math.min(columnWidth, maxWidth);

          var newColumn: ColumnItem = {
            name: ColumnName,
            sortOrder: null,
            sortFn: this.getSortFn(ColumnName),
            sortDirections: this.getSortDirection(ColumnName),
            filterMultiple: false,
            listOfFilter: [],
            filterFn: null,
            width: `${columnWidth}px`,
            visible: ColumnName !== 'TASKID',
          };

          this.listOfColumns.push(newColumn);
        });

        // Si solo existe TASKID como columna, no la ocultes
        if (this.listOfColumns.length === 1 && (this.listOfColumns[0].name || '').toUpperCase() === 'TASKID') {
          this.listOfColumns[0].visible = true;
        }

        this.FlagOnClick = this.listOfColumns.some(c => (c.name || '').toString().toUpperCase() === 'TASKID');

        ObjectData.RowHashtable.forEach((element: any) => {
          const newRow: any = {};

          ObjectData.ListColumns.forEach((ColumnName: string) => {
            newRow[ColumnName] = element[ColumnName];
          });

          this.listOfData.push(newRow);
        });

        this.allListOfData = [...this.listOfData];
        this.applySearchFilter();
      } else {
        this.isButtonExcelDisabled = true;
        console.info('No se encontraron registros para mostrar');
        this.modal.info({
          nzTitle: 'No se encontraron registros para mostrar',
          nzContent: '<p>Verifique los filtros, que el reporte tenga datos y/o la base de datos estén bien configurados.</p>',
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });
      }

      this.isLoading = false;
      this.cdr.detectChanges();

      const tokenData = this.tokenService.get();

      if (tokenData != null) {
        var genericRequestToSaveView = {
          UserId: tokenData['userid'],
          token: tokenData['token'],
          Params: {
            ReportId: this.currentReport.ID,
            ViewMode: 'ResultsGrid',
          },
        };

        this.RVService.SaveLastReportViewed(genericRequestToSaveView).subscribe();
      }

      //return;
    } else if (typeof JSON.parse(data) == 'string') {
      this.isButtonExcelDisabled = true;

      console.error('Error: Ocurrio un error al cargar el reporte');
      this.modal.error({
        nzTitle: 'Error al cargar el reporte',
        nzContent:
          '<p>Ocurrió un error al intentar cargar el reporte. Por favor verifica los filtros e intenta nuevamente.</p><p>Si persiste, contacta a soporte con el nombre del reporte.</p>',
        nzOkText: 'Aceptar',
        nzOkType: 'primary',
        nzOnOk: () => console.log('OK'),
      });

      this.isLoading = false;

      this.cdr.detectChanges();
    }
  }
  private getSortFn(ColumnName: any) {
    return (a: any, b: any) => {
      const valA = a[ColumnName];
      const valB = b[ColumnName];

      // Si ambos son números, compara como números
      if (!isNaN(valA) && !isNaN(valB) && valA !== null && valB !== null && valA !== '' && valB !== '') {
        return Number(valA) - Number(valB);
      }

      // Si ambos son fechas válidas
      if (!isNaN(Date.parse(valA)) && !isNaN(Date.parse(valB))) {
        return new Date(valA).getTime() - new Date(valB).getTime();
      }

      // Si ambos son strings, compara como strings
      return String(valA ?? '').localeCompare(String(valB ?? ''));
    };
  }

  private getSortDirection(ColumnName: any) {
    return ['ascend', 'descend', null];
  }

  exportToExcel(report: Report): void {
    this.isButtonExcelDisabled = true;
    this.cdr.detectChanges();
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    //
    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          Zvars: JSON.stringify({
            ...this.buildZvarsObject(),
            FechaDesde: this.ZVARstartDate,
            FechaHasta: this.ZVARendDate,
          }),
          Query: this.currentReport.Query,
          ReportId: this.currentReport.ID,
        },
      };

      const FileName = `${report.Name.replace(/ /g, '_')} `;

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

            this.modal.error({
              nzTitle: 'No hay datos para exportar',
              nzContent: '<p>El reporte no contiene resultados para exportar. </p>',
              nzOkText: 'Aceptar',
              nzOkType: 'primary',
              nzOnOk: () => console.log('OK'),
            });

            this.isButtonExcelDisabled = false;
            this.cdr.detectChanges();
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

          this.isButtonExcelDisabled = false;
          this.cdr.detectChanges();
        });
    }
  }

  // Navega al contenedor de gráficos del mismo reporte (botón "volver")
  goToCharts(report: Report): void {
    if (!report) return;
    // Intento de usar un identificador; si el objeto ya trae ID lo usamos
    const anyReport: any = report as any;
    const reportId = anyReport.ID || anyReport.Id || anyReport.id; // tolerante a distintas propiedades
    if (!reportId) {
      console.warn('goToCharts: No se encontró el ID del reporte.');
      return;
    }
    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
      queryParams.params = JSON.stringify({
        ...this.buildZvarsObject(),
        FechaDesde: this.ZVARstartDate,
        FechaHasta: this.ZVARendDate,
      });
    }

    this.router.navigate(['/tools/reports/chartcontainer', reportId], { queryParams });
  }

  executeRepeatedly(maxTimeInSeconds: number) {
    const intervalTime = 250; // 500 ms (medio segundo)
    const maxIterations = (maxTimeInSeconds * 1000) / intervalTime; // Número máximo de iteraciones
    let iterationCount = 0;

    const intervalId = setInterval(() => {
      console.log('Ejecutando código cada medio segundo');
      this.adjustHeight();
      this.cdr.detectChanges();

      iterationCount++;
      if (iterationCount >= maxIterations) {
        clearInterval(intervalId); // Detiene la ejecución después del tiempo máximo
        console.log('Ejecución detenida');
      }
    }, intervalTime);
  }

  extractZvarVariables(sql: string): string[] {
    // regex: busca zvar(contenido)
    const regex = /zvar\(([^)]+)\)/gi;
    const variables: string[] = [];
    let match;

    while ((match = regex.exec(sql)) !== null) {
      variables.push(match[1]);
    }

    return variables;
  }

  executeRule(event: any): void {
    console.log('Rule completed event received:', event);

    this.executingRule = false;
    this.ruleId = 0;
    this.isLoading = false;
    this.cdr.markForCheck();
  }

  onRowClick(data: any): void {
    if (this.FlagOnClick) {
      var thisDomain = environment['zambaWeb'];
      const tokenData = this.tokenService.get();

      if (thisDomain && tokenData != null) {
        const Url =
          `${thisDomain}/views/WF/TaskViewer.aspx` +
          `?DocTypeId=${data['ENTITYID']}&docid=${data['DOCID']}&taskid=${data['TASKID']}&wfstepid=${data['STEPID']}&user=${tokenData['userid']}&t=${tokenData['token']}`;

        window.open(Url, '_blank');
      }
    }
  }


  onRowDblClick(row: any): void {
    if (row && row.TASKID) {
      const tokenData = this.tokenService.get();

      if (tokenData) {
        var genericRequest = {
          UserId: tokenData['userid'],
          token: tokenData['token'],
          Params: {
            taskId: row.TASKID,
          },
        };

        this.TService.getTaskByTaskId(genericRequest)
          .pipe(
            catchError(error => {
              console.error('Error al obtener datos:', error);

              throw error;
            }),
          )
          .subscribe((data: any) => {
            console.log('Respuesta del servicio:', data);
            if (data && data.url) {
              var thisDomain = environment['zambaWeb'];
              const tokenData = this.tokenService.get();

              if (thisDomain && tokenData != null) {
                const Url = `${thisDomain + data.url}&user=${tokenData['userid']}&t=${tokenData['token']}`;

                window.open(Url, '_blank');
              }
            } else {
              console.error('URL no encontrada en la respuesta del servicio');
            }
          });
      }
    }
  }

  search(searchValue: string): void {
    this.searchValue = searchValue;

    this.applySearchFilter();
  }

  private applySearchFilter(): void {
    const normalizedSearchValue = (this.searchValue || '').trim().toLowerCase();

    if (!normalizedSearchValue) {
      this.listOfData = [...this.allListOfData];
      return;
    }

    const searchableColumns = this.listOfColumns.filter(column => column.visible).map(column => column.name);

    this.listOfData = this.allListOfData.filter(row =>
      searchableColumns.some(columnName => {
        const cellValue = row?.[columnName];

        return cellValue != null && cellValue.toString().toLowerCase().includes(normalizedSearchValue);
      }),
    );

  }

  ViewAllRegisters(): void {
    this.ZVARstartDate = new Date(Date.UTC(0, 0, 1, 0, 0, 0, 0));
    this.ZVARendDate = new Date(Date.UTC(3000, 11, 31, 23, 59, 59, 999));
    this.rechargeReport();
  }

  //#endregion

  //#region Visual Management
  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.adjustHeight();
  }

  adjustHeight() {
    const getElementHeightWithMargins = (selector: string): number => {
      const element = document.querySelector(selector) as HTMLElement;
      if (!element) return 0;

      const style = window.getComputedStyle(element);
      const marginTop = parseInt(style.marginTop, 10) || 0;
      const marginBottom = parseInt(style.marginBottom, 10) || 0;

      return element.getBoundingClientRect().height + marginTop + marginBottom - 3;
    };

    // Obtener alturas y márgenes de los elementos
    const reportNameHeight = getElementHeightWithMargins('#report-name');
    const reportDescriptionHeight = getElementHeightWithMargins('#report-description');
    const exportToExcelBtnHeight = getElementHeightWithMargins('#exportToExcelBtn');
    const paginationHeight = getElementHeightWithMargins('.ant-table-pagination');
    const alainDefaultHeader = getElementHeightWithMargins('.alain-default__header');
    const antTableHeader = getElementHeightWithMargins('.ant-table-header');
    const ZVarsPanel = getElementHeightWithMargins('#ZVarsPanel');

    // Calcular la altura disponible}
    // Se aplica un -16 por que hay unos margin-bottom que no se detectan
    const totalOccupiedHeight =
      ZVarsPanel +
      reportNameHeight +
      reportDescriptionHeight +
      exportToExcelBtnHeight +
      paginationHeight +
      alainDefaultHeader +
      antTableHeader;
    const availableHeight = window.innerHeight - totalOccupiedHeight;

    this.height = `${availableHeight}px`;
    this.cdr.detectChanges();
  }

  ngAfterViewInit() {
    this.adjustHeight();
  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  isColumnVisible(key: string): boolean {
    const col = this.listOfColumns.find(c => c.name === key);
    return col ? col.visible : true;
  }

  //#endregion
}
interface ColumnItem {
  name: string;
  sortOrder: NzTableSortOrder | null;
  sortFn: NzTableSortFn<any> | null;
  listOfFilter: NzTableFilterList;
  filterFn: NzTableFilterFn<any> | null;
  filterMultiple: boolean;
  sortDirections: NzTableSortOrder[];
  width: string;
  visible: boolean;
}
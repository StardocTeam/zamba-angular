import { Component, Inject, inject, Input, SimpleChanges } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from 'src/app/routes/widgets/report-component/entitie/report';
import { G2BarClickItem, G2BarData, G2BarModule } from '@delon/chart/bar';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError, of, tap } from 'rxjs';
import { ReportService } from 'src/app/routes/widgets/report-component/service/report.service';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { ZambaService } from 'src/app/services/zamba/zamba.service';
import { ChartService } from './service/chart.service';

@Component({
  selector: 'app-chart-component',
  templateUrl: './chart.component.html',
  imports: [G2BarModule, NzButtonModule],
  standalone: true,
})
export class ChartComponent {
  private readonly msg = inject(NzMessageService);
  private route = inject(ActivatedRoute);
  @Input() chartConfigId: number = 0;
  ListValues: G2BarData[] = [];
  currentReport: Report = {} as Report;
  DebugMode: boolean = true;

  @Input() title: string = 'Grafico';
  @Input() AttrSelected: string = 'Id';
  @Input() chartType: string = 'Grafico';
  @Input() ReportData: any;
  dataIsEmpty: boolean = true;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService,
    private zambaService: ZambaService,
    private RVService: ReportViewerService,
    private CService: ChartService
  ) {

  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['ReportData']) {
      console.log('ReportData changed:', changes['ReportData'].currentValue);
    }
  }

  ngOnInit() {




    // if (true) {
    //   this.fetchUserIdWithToken(this.tokenService.get()?.token || null, this.chartConfigId);
    // }




    const attributes = this.AttrSelected.split(',');
    const XAttribute = attributes[0];
    const YAttribute = attributes.length > 1 ? attributes[1] : undefined;
    const ZAttribute = attributes.length > 2 ? attributes[2] : undefined;
    const labelAttribute = attributes.length > 3 ? attributes[3] : undefined;
    const aggretationType = attributes.length > 4 ? attributes[4] : 'count';

    switch (this.chartType) {
      case "Bars":
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        this.dataIsEmpty = false;
        break;
      case "Bars-timeLine": // Tipo timeLine
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        break;
      case "Cake": // Tipo Count
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        break;
      case "MiniArea(TimeLine B)": // Tipo timeLine
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        break;
      case "MiniArea(TimeLine A, el posta)": // Tipo timeLine con mas lineas (iteraciones de columnas)
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        break;
      default:
        this.ListValues = this.setData(this.getDistinctValues(this.ReportData.RowHashtable, aggretationType, XAttribute, YAttribute));
        break;
    }
  }

  //#region Bussiness Logic
  private fetchUserIdWithToken(tokenParam: string | null, reportId: number) {

    let genericRequest = {
      UserId: 0,
      token: tokenParam
    };

    this.zambaService.getUserId(genericRequest).pipe(
      tap(response => {

        response = JSON.parse(response);
        this.tokenService.set({ token: tokenParam, userid: response });

        if (response) {
          this.initializeChartComponent(reportId);
        } else {
          throw new Error('Report ID not found');
        }

      }),
      catchError(error => {
        console.error('Error fetching task name:', error);
        return of([]);
      })
    ).subscribe();
  }

  initializeChartComponent(reportId: number) {
    let GRequest = {};
    const tokenData = this.tokenService.get();

    if (tokenData != null) {
      GRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          ReportId: reportId
        }
      };

      this.CService._GetChart(GRequest).pipe(
        catchError(error => {
          console.error('Error al obtener configuración:', error);
          throw error;
        })
      ).subscribe((config: any) => {
        var datosDeChart = JSON.parse(config)[0];
        this.chartType = datosDeChart.ChartTypeDescripcion;

        const GRequestReport = {
          UserId: tokenData['userid'],
          token: tokenData['token'],
          Params: {
            Id: datosDeChart.ReportId
          }
        };

        this.RVService.GetReportById(GRequestReport).pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        ).subscribe((data: any) => {

          this.currentReport = JSON.parse(data)[0];
          console.log(this.currentReport);

          // Solo después de obtener el reporte, hacemos la segunda petición
          const GRequestWithReportQuery = {
            UserId: tokenData['userid'],
            token: tokenData['token'],
            Params: {
              Query: this.currentReport.Query
            }
          };


          this.RVService.GetReportByQuery(GRequestWithReportQuery).pipe(
            catchError(error => {
              console.error('Error al obtener datos:', error);
              throw error;
            })
          ).subscribe((data: any) => {

            var datos = JSON.parse(data);


            this.title = datosDeChart.ChartTitle || 'Tipo de grafico: ' + this.chartType;

            switch (this.chartType) {
              case "Bars":
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Accion"));
                break;
              case "Bars-timeLine": // Tipo timeLine
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Category"));
                break;
              case "Cake": // Tipo Count
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Category"));
                break;
              case "MiniArea(TimeLine B)": // Tipo timeLine
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Category"));
                break;
              case "MiniArea(TimeLine A, el posta)": // Tipo timeLine con mas lineas (iteraciones de columnas)
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Category"));
                break;
              default:
                this.ListValues = this.setData(this.getDistinctCount(datos.RowHashtable, "Category"));
                break;
            }

            console.log('Configuración obtenida:', config);
          });
        });

        console.log('Configuración obtenida:', config);
      });

    }
  }

  getDistinctCount(datos: any[], campo: string): Array<{ x: string, y: number }> {
    const resultado: { [key: string]: number } = {};

    datos.forEach(obj => {
      const valor = obj[campo];
      if (valor !== undefined && valor !== null) {
        resultado[valor] = (resultado[valor] || 0) + 1;
      }
    });

    return Object.entries(resultado).map(([key, value]) => ({
      x: key,
      y: value
    }));
  }

  getDistinctValues(data: any[], aggregationType: string, XField: string, YField?: string): Array<{ x: string, y: number }> {
    const results: { [key: string]: number } = {};

    data.forEach(obj => {
      const XValue = obj[XField];
      const YValue = YField ? obj[YField] : undefined;
      if (XValue !== undefined && XValue !== null && YValue !== undefined && YValue !== null) {
        const key = `${XValue}`;
        if (aggregationType === 'count') {
          results[key] = (results[key] || 0) + 1;
        } else if (aggregationType === 'sum') {
          results[key] = (results[key] || 0) + YValue;
        }
      }
    });

    return Object.entries(results).map(([key, value]) => ({
      x: key,
      y: value
    }));
  }

  //#endregion


  //#region Visualización
  handleClick(data: G2BarClickItem): void {
    this.msg.info(`${data.item.x} - ${data.item.y}`);
  }

  private setData(list: Array<any>): G2BarData[] {

    const result: G2BarData[] = [];

    list.forEach((item, idx) => {
      result.push({
        x: item.x,
        y: item.y,
        color: idx > (list.length / 2) ? '#f50' : undefined
      });
    });

    return result;
  }
  //#endregion Visualización

  //#region DEBUG MODE
  addChart(): void {

  }
  //#endregion

}

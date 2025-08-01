import { Component, Inject, inject } from '@angular/core';
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

@Component({
  selector: 'chart-bar-basic',
  templateUrl: './chart.component.html',
  imports: [NzButtonModule, G2BarModule],

  standalone: true,
})
export class ChartComponent {
  private readonly msg = inject(NzMessageService);

  private route = inject(ActivatedRoute);
  salesData = this.genData();
  ListValues: G2BarData[] = [];
  currentReport: Report = {} as Report;

  refresh(): void {
    this.salesData = this.genData();
  }

  private genData(): G2BarData[] {
    return new Array(12).fill({}).map((_i, idx) => ({
      x: (idx + 1).toString() + "° MES",
      y: Math.floor(Math.random() * 1000) + 200,
      color: idx > 5 ? '#f50' : undefined
    }));
  }

  private setData(list: Array<any>): G2BarData[] {
    return list.fill({}).map((_i, idx) => ({
      x: list[idx].x,
      y: list[idx].y,
      color: idx > 2 ? '#f50' : undefined
    }));
  }
  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private RService: ReportService,
    private zambaService: ZambaService,
    private RVService: ReportViewerService) {

  }




  private fetchUserIdWithToken(tokenParam: string | null, ReportId: number) {

    let genericRequest = {
      UserId: 0,
      token: tokenParam
    };

    this.zambaService.getUserId(genericRequest).pipe(
      tap(response => {
        response = JSON.parse(response);
        this.tokenService.set({ token: tokenParam, userid: response });

        if (response) {
          this.initializeChartComponent(ReportId);
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


  initializeChartComponent(ReportId: number) {
    let GRequest = {};
    const tokenData = this.tokenService.get();

    if (tokenData != null) {
      GRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          Id: ReportId
        }
      };

      // Primero obtenemos el reporte por ID
      this.RVService.GetReportById(GRequest).pipe(
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
          var datos: Report[] = JSON.parse(data);

          //Si el grafico es de barras
          //Es Tipo TimeLine: es decir, usa una columna de un solo registro con flujo de tiempo? cual es el intervalo?
          //Las Barras cuentan las iteraciones de una columna XXX?  


          var num = "Barras-iteradoras";
          switch (num) {
            case "Barras-iteradoras": // Tipo Count
              this.salesData = this.setData(this.getDistinctCount(datos, "Completar"));
              break;
            case "Barras-timeLine": // Tipo timeLine
              this.salesData = this.setData(this.getDistinctCount(datos, "Category"));
              break;
            case "Torta": // Tipo Count
              this.salesData = this.setData(this.getDistinctCount(datos, "Category"));
              break;
            case "MiniArea(TimeLine B)": // Tipo timeLine
              this.salesData = this.setData(this.getDistinctCount(datos, "Category"));
              break;
            case "MiniArea(TimeLine A, el posta)": // Tipo timeLine con mas lineas (iteraciones de columnas)
              this.salesData = this.setData(this.getDistinctCount(datos, "Category"));
              break;


            default:
              this.salesData = this.setData(this.getDistinctCount(datos, "Category"));
              break;
          }

          // Finalmente, actualizamos los datos del gráfico


          this.setData(datos);



        });
      });
    }
  }






  ngOnInit() {
    this.route.queryParamMap.subscribe(params => {
      if (params) {
        // Si el token existe, lo guardamos en el servicio de token
        var tokenParam: string | null;

        if (params.get('t') && params.get('reportId')) {
          this.tokenService.set({ token: params.get('t') });
          tokenParam = params.get('t');
          const reportId = Number(params.get('reportId'));

          this.fetchUserIdWithToken(tokenParam, reportId);
        } else {
          throw new Error('Token not found');
        }


      } else {
        //TODO: hacer un mensaje visual.
        throw new Error('Token not found');
      }
    });

    /*----------------------------------------------*/










    /*----------------------ESTO NO VAA------------------------*/

    let genericRequest = {};
    const tokenData = this.tokenService.get();

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
        console.log(datos)

        var ArrayResult: Array<{ x: string, y: number }> = this.getDistinctCount(datos, "Category");
        this.salesData = ArrayResult;
      });
    }
  }

  /**
   * Array(12) = a un distinc de alguna propiedad de los resultados de reportes
   * x: iteraciones del resultado del distinc de un campo
   * y: es la cantidad de veces que se repite el campo 
   */

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

  handleClick(data: G2BarClickItem): void {
    this.msg.info(`${data.item.x} - ${data.item.y}`);
  }
}
import { Component, Inject, inject } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';

import { G2BarClickItem, G2BarData, G2BarModule } from '@delon/chart/bar';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';
import { catchError } from 'rxjs';
import { ReportService } from 'src/app/routes/widgets/report-component/service/report.service';

@Component({
  selector: 'chart-bar-basic',
  templateUrl: './chart.component.html',
  imports: [NzButtonModule, G2BarModule],

  standalone: true,
})
export class ChartComponent {
  private readonly msg = inject(NzMessageService);
  salesData = this.genData();
  ListValues: G2BarData[] = [];

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
  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private RService: ReportService) {

  }

  ngOnInit() {
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
import { CommonModule, NgForOf } from '@angular/common';
import { Component, Inject, Input, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzCardComponent, NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzMarks, NzSliderModule } from 'ng-zorro-antd/slider';
import { ChartComponent } from '../chart/chart.component';
import { ChartService } from '../chart/service/chart.service';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { catchError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-chart-container',
  templateUrl: './chart-container.component.html',
  styleUrls: ['./chart-container.component.less'],
  standalone: true,
  imports: [FormsModule, NzGridModule, NzSliderModule, NzCardModule, NgForOf, ChartComponent, CommonModule]
})
export class ChartContainerComponent {
  //a
  DimY: number = 1;
  DimX: number = 1;

  //b
  count = 2;
  array = new Array(this.count);
  marksHGutter: NzMarks = {
    8: '8',
    16: '16',
    24: '24',
    32: '32',
    40: '40',
    48: '48'
  };
  marksVGutter: NzMarks = {
    8: '8',
    16: '16',
    24: '24',
    32: '32',
    40: '40',
    48: '48'
  };
  marksCount: NzMarks = {
    2: '2',
    3: '3',
    4: '4',
    6: '6',
    8: '8',
    12: '12'
  };

  //#region C
  baseCellHeight = 250;
  //baseCellWidth = 250;
  //5 x 5
  // Definís los "bloques"
  blocks = [
    { row: 1, col: 1, rowSpan: 1, colSpan: 4, type: 'grafico' },
    { row: 2, col: 1, rowSpan: 1, colSpan: 2, type: 'grafico' },
    { row: 2, col: 3, rowSpan: 1, colSpan: 2, type: 'grafico' },
  ];

  chartList = [
    { posY: 1, posX: 1, dimYSpan: 1, dimXSpan: 4, type: 'bars' },
    { posY: 2, posX: 1, dimYSpan: 1, dimXSpan: 2, type: 'bars' },
    { posY: 2, posX: 3, dimYSpan: 1, dimXSpan: 2, type: 'bars' },
  ];

  /**
   *
   */
  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private CService: ChartService, private route: ActivatedRoute) {

  }



  ngOnInit() {
    debugger;
    const tokenData = this.tokenService.get();







    this.route.params.subscribe(params => {
      let genericRequest = {};
      debugger;
      if (tokenData) {
        genericRequest = {
          UserId: 183,
          token: tokenData['token'],
          Params: {
            ReportId: params['id']
          }
        };



        this.CService._GetChartContainer(genericRequest).pipe(
          catchError(error => {
            console.error('Error al obtener configuración:', error);
            throw error;
          })
        ).subscribe((data: any) => {
          debugger;
          console.log(data);
          this.DimY = JSON.parse(data)[0].DimY;
          this.DimX = JSON.parse(data)[0].DimX;
        });

      }
    });











    if (tokenData != null) {

      var GRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          ReportId: 10012 //TEST -
        }
      };

      this.CService._GetChartByReportId(GRequest).pipe(
        catchError(error => {
          console.error('Error al obtener configuración:', error);
          throw error;
        })
      ).subscribe((data: any) => {
        console.log(data);

        this.chartList = JSON.parse(data);
        //CDR
      });
    }
  }

  getChartAt(posY: number, posX: number) {
    return this.chartList.find(b => b.posY === posY && b.posX === posX);
  }

  isCellCovered(rowY: number, colX: number) {
    return this.chartList.some(b =>
      rowY >= b.posY &&
      rowY < b.posY + b.dimYSpan &&
      colX >= b.posX &&
      colX < b.posX + b.dimXSpan &&
      !(rowY === b.posY && colX === b.posX) // no es la celda inicial
    );
  }
  //#endregion



  reGenerateArray(count: number): void {
    this.array = new Array(count);
    console.log(this.count);
  }







  // array de cajas simuladas
  items = Array.from({ length: 12 }, (_, i) => i + 1);

  getCoordinates(index: number) {
    const x = (index % this.count) + 1;          // columna
    const y = Math.floor(index / this.count) + 1; // fila
    return { x, y };
  }
}

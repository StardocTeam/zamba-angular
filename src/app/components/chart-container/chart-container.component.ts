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
import { ChartItem } from './ChartItem';
import { ReportService } from 'src/app/routes/widgets/report-component/service/report.service';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';

import { Report } from "../../routes/widgets/report-component/entitie/report";

@Component({
  selector: 'app-chart-container',
  templateUrl: './chart-container.component.html',
  styleUrls: ['./chart-container.component.less'],
  standalone: true,
  imports: [FormsModule, NzGridModule, NzSliderModule, NzCardModule, NgForOf, ChartComponent, CommonModule]
})
export class ChartContainerComponent {
  //a
  DimY: number = 0;
  DimX: number = 0;

  //b
  count = 2;
  array = new Array(this.count);

  //#region C
  baseCellHeight = 250;
  baseCellWidth = 150;
  //5 x 5
  // Definís los "bloques"

  chartList: ChartItem[] = [];
  currentReport: Report = new Report({});
  ReportData: any;

  /**
   *
   */
  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private CService: ChartService, private route: ActivatedRoute, private RViewService: ReportViewerService) {

  }



  ngOnInit() {
    const tokenData = this.tokenService.get();

    this.route.params.subscribe(params => {
      let genericRequest = {};
      if (tokenData) {
        genericRequest = {
          UserId: 183, //TODO USER ID
          token: tokenData['token'],
          Params: {
            Id: params['id']
          }
        };

        this.CService._GetChartContainer(genericRequest).pipe(
          catchError(error => {
            console.error('Error al obtener configuración:', error);
            throw error;
          })
        ).subscribe((data: any) => {
          this.DimY = JSON.parse(data)[0].DimY;
          this.DimX = JSON.parse(data)[0].DimX;
        });

        this.RViewService.GetReportById(genericRequest).pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        )
          .subscribe((data: any) => {

            this.currentReport = JSON.parse(data)[0];

            let genericRequest = {
              UserId: tokenData['userid'],
              Params: {
                Query: this.currentReport.Query
              }
            };

            this.RViewService.GetReportByQuery(genericRequest).pipe(
              catchError(error => {
                console.error('Error al obtener datos:', error);
                throw error;
              })
            )
              .subscribe((data: any) => {
                this.ReportData = JSON.parse(data);

                var GRequest = {
                  UserId: tokenData['userid'],
                  token: tokenData['token'],
                  Params: {
                    ReportId: this.currentReport.ID
                  }
                };


                this.CService._GetChartsByReportId(GRequest).pipe(
                  catchError(error => {
                    console.error('Error al obtener configuración:', error);
                    throw error;
                  })
                ).subscribe((data: any) => {
                  console.log(JSON.parse(data));
                  this.chartList = JSON.parse(data);
                });
              });
          });
      }
    });
  }

  getChartAt(PosY: number, PosX: number) {
    return this.chartList.find(b => b.PosY === PosY && b.PosX === PosX);
  }

  isCellCovered(rowY: number, colX: number): boolean {
    return this.chartList.some(b =>
      rowY >= b.PosY &&
      rowY <= b.PosY + b.DimYSpan - 1 &&
      colX >= b.PosX &&
      colX <= b.PosX + b.DimXSpan - 1 &&
      !(rowY === b.PosY && colX === b.PosX) // excluir celda inicial
    );
  }
  //#endregion


  cambiar() {
    this.DimX = Number(5);
    this.DimY = Number(5);
  }

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

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
import { ActivatedRoute, Router } from '@angular/router';
import { ChartItem } from './ChartItem';
import { ReportService } from 'src/app/routes/widgets/report-component/service/report.service';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';

import { Report } from "../../routes/widgets/report-component/entitie/report";
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-chart-container',
  templateUrl: './chart-container.component.html',
  styleUrls: ['./chart-container.component.less'],
  standalone: true,
  imports: [FormsModule, NzGridModule, NzSliderModule, NzCardModule, NgForOf, ChartComponent, CommonModule, NzIconModule, NzButtonModule]
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
  // Estado para deshabilitar botones hasta que termine la carga (igual que en report-viewer)
  isButtonExcelDisabled: boolean = true;

  /**
   *
   */
  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private CService: ChartService, private modal: NzModalService, private route: ActivatedRoute, private RViewService: ReportViewerService,
    private router: Router) {

  }



  ngOnInit() {
    const tokenData = this.tokenService.get();

    this.route.params.subscribe(params => {
      let genericRequest = {};
      if (tokenData) {
        genericRequest = {
          UserId: tokenData['userid'],
          token: tokenData['token'],
          Params: {
            Id: params['id']
          }
        };

        this.CService._GetChartContainer(genericRequest).pipe(
          catchError(error => {
            console.error('Error al obtener configuración:', error);

            this.modal.error({
              nzTitle: 'Error al obtener configuración',
              nzContent: '<p>No se encontro ningun grafico.</p>',
              nzOkText: 'OK',
              nzOkType: 'primary',
              nzOnOk: () => console.log('OK'),
            });

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

                    this.modal.error({
                      nzTitle: 'Error al obtener configuración',
                      nzContent: '<p>No se encontro ningun grafico.</p>',
                      nzOkText: 'OK',
                      nzOkType: 'primary',
                      nzOnOk: () => console.log('OK'),
                    });

                    throw error;
                  })
                ).subscribe((data: any) => {
                  console.log(JSON.parse(data));

                  if (data == null || data == '[]') {
                    console.info('No hay resultados');

                    this.modal.info({
                      nzTitle: 'No hay resultados',
                      nzContent: '<p>No se encontro ningun grafico valido</p>',
                      nzOkText: 'OK',
                      nzOkType: 'primary',
                      nzOnOk: () => {
                        console.log('OK');
                        this.goToReportViewer();
                      },
                      nzOnCancel: () => {
                        console.log('Modal cerrado por la X');
                        this.goToReportViewer();
                      }
                    });
                  } else {
                    this.chartList = JSON.parse(data);
                    this.isButtonExcelDisabled = false;
                  }
                }, error => {
                  //this.isButtonExcelDisabled = true;
                });
              });
          });
      }
    });
  }

  getChartAt(PosY: number, PosX: number) {
    var result = this.chartList.find(b => b.PosY === PosY && b.PosX === PosX);
    return result;
  }

  isCellCovered(rowY: number, colX: number): boolean {
    return this.chartList.some(b =>
      rowY >= b.PosY &&
      rowY <= b.PosY + b.DimY - 1 &&
      colX >= b.PosX &&
      colX <= b.PosX + b.DimX - 1 &&
      !(rowY === b.PosY && colX === b.PosX) // excluir celda inicial
    );
  }
  //#endregion

  getCoordinates(index: number) {
    const x = (index % this.count) + 1;          // columna
    const y = Math.floor(index / this.count) + 1; // fila
    return { x, y };
  }

  // Navegar a la vista del reporte manteniendo los query params (token, etc.)
  goToReportViewer(): void {
    if (!this.currentReport) return;
    const anyReport: any = this.currentReport as any;
    const reportId = anyReport.ID || anyReport.Id || anyReport.id;
    if (!reportId) return;

    // Copiar query params actuales
    const currentQueryParams = { ...this.route.snapshot.queryParams };
    const tokenData = this.tokenService.get();
    if (tokenData?.token && !currentQueryParams['t']) {
      currentQueryParams['t'] = tokenData.token;
    }


    this.router.navigate(['/tools/reports/view', reportId], { queryParams: currentQueryParams });
  }
}

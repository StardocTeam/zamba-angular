import { ChangeDetectionStrategy, ChangeDetectorRef, Component, HostListener, Inject } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { Report } from "../report-component/entitie/report";


import {
  NzTableFilterFn,
  NzTableFilterList,
  NzTableSortFn,
  NzTableSortOrder,
  NzTableModule
} from 'ng-zorro-antd/table';
import { ReportViewerService } from './service/report-viewer.service';
import { catchError } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { query } from '@angular/animations';
import { Query } from '@delon/theme';
import { GridService } from 'src/app/services/Grid/grid.service';
import { NzModalService } from 'ng-zorro-antd/modal';

@Component({
  selector: 'app-report-viewer',
  templateUrl: './report-viewer.component.html',
  styleUrls: ['./report-viewer.component.less']
})


export class ReportViewerComponent {
  loading: Boolean = true;
  currentReport: Report = new Report({});
  listOfData: any[] = [];
  listOfColumns: ColumnItem[] = [];
  Description: string = "";
  height: string = "400px";
  PageIndex: number = 1;

  DestructorFlag: boolean = false;

  nzShowPagination: boolean = true;
  isButtonExcelDisabled: boolean = true;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef, private RVService: ReportViewerService, private route: ActivatedRoute,
    private GService: GridService, private modal: NzModalService) {
  }

  ShowPagination() {
    this.nzShowPagination = !this.nzShowPagination;
  }

  ngOnInit() {
    this.loading = false;
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

        this.RVService.GetReportById(genericRequest).pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        )
          .subscribe((data: any) => {
            var currentReport: Report = JSON.parse(data)[0];
            this.OpenReport(new Report(currentReport));
          });
      }
    });
    this.adjustHeight();
  }
  GetDescription(Id: string) {

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          reportId: Id
        }
      };

      this.RVService.GetReportDescriptionByQuery(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      )
        .subscribe((data: any) => {
          this.Description = data;
          this.cdr.detectChanges();
        });
    }

    this.loading = true;
    this.cdr.detectChanges();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    this.adjustHeight();
  }

  @HostListener('window:resize', ['$event'])
  adjustHeight() {
    const mediaQueries = [
      { maxHeight: 461, height: '213px' },
      { maxHeight: 493, height: '245px' },
      { maxHeight: 536, height: '288px' },
      { maxHeight: 584, height: '341px' },
      { maxHeight: 643, height: '395px' }
    ];

    for (const query of mediaQueries) {
      if (window.innerHeight <= query.maxHeight) {
        this.height = query.height;
        break;
      }
    }
    this.cdr.detectChanges();
  }


  ngAfterViewInit() {
    this.adjustHeight();
  }

  OpenReport(report: Report) {
    this.listOfColumns = [];
    this.listOfData = [];
    this.currentReport = report;
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Query: report.Query
        }
      };
      //debugger;
      this.RVService.GetReportByQuery(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          //debugger;
          throw error;
        })
      )
        .subscribe((data: any) => {
          if (!data) {
            this.isButtonExcelDisabled = true;

            console.error('Error: No data received for report.');
            this.modal.info({
              nzTitle: 'No hay datos disponibles',
              nzContent: '<p>este reporte no muestra datos o hay un error en la sentencia.</p>',
              nzOkText: 'OK',
              nzOkType: 'primary',
              nzOnOk: () => console.log('OK'),
            });
            this.loading = false;
            this.cdr.detectChanges();
            return;
          }

          this.isButtonExcelDisabled = false;

          var ObjectData = JSON.parse(data);
          this.Description = report.Description;
          this.cdr.detectChanges();

          ObjectData.ListColumns.forEach((element: any) => {
            var columnWidth = "150px";

            //TODO: Hacer esto dinamico
            if (element.ColumnName == "Descripcion") {
              columnWidth = "700px";
            } else if (element.ColumnName == "Fecha") {
              columnWidth = "200px";
            }

            var newColumn = {
              name: element.ColumnName,
              sortOrder: null,
              sortFn: null,
              sortDirections: [null],
              filterMultiple: false,
              listOfFilter: [],
              filterFn: null,
              width: columnWidth
            }

            this.listOfColumns.push(newColumn);
          });

          ObjectData.RowHashtable.forEach((element: any) => {
            var newRow: any = [];
            ObjectData.ListColumns.forEach((column: any) => {
              newRow[column.ColumnName] = element[column.ColumnName];
            });

            this.listOfData.push(newRow);
          });
        });
    }

    this.loading = true;
    this.cdr.detectChanges();

  }

  objectKeys(obj: any): string[] {
    return Object.keys(obj);
  }

  exportToExcel(report: Report): void {
    this.isButtonExcelDisabled = true;
    this.cdr.detectChanges();
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    //debugger;
    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
        Params: {
          "Query": report.Query
        }
      };

      const FileName = report.Name.replace(/ /g, "_") + ".xlsx";

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

        //debugger;
        const url = dataBase64;
        const a = document.createElement('a');
        a.href = url;
        a.download = FileName + " " + formattedDate + " " + formattedTime + ".xlsx";
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        this.isButtonExcelDisabled = false;
        this.cdr.detectChanges();
      });
    }

  }
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
}



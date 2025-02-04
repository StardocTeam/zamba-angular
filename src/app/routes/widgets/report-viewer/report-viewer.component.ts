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

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef, private RVService: ReportViewerService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.adjustHeight();
    this.loading = false;
    this.route.params.subscribe(params => {
      const tokenData = this.tokenService.get();
      let genericRequest = {};

      if (tokenData != null) {
        genericRequest = {
          UserId: tokenData['userid'],
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

  adjustHeight() {
    const height = window.innerHeight;
    const RDContainer = document.getElementsByClassName('report-details')[0];
    const heightRDContainer = RDContainer.clientHeight == 0 ? 66 : RDContainer.clientHeight;

    this.height = (height - 40 - 230 - heightRDContainer - 14 + 88).toString() + "px";
    this.cdr.detectChanges();
  }

  OpenReport(report: Report) {

    this.currentReport = report;
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Query: report.Query
        }
      };

      this.RVService.GetReportByQuery(genericRequest).pipe(
        catchError(error => {
          console.error('Error al obtener datos:', error);
          throw error;
        })
      )
        .subscribe((data: any) => {
          var ObjectData = JSON.parse(data);
          this.Description = report.Description;
          this.listOfColumns = [];
          this.listOfData = [];
          this.cdr.detectChanges();

          ObjectData.ListColumns.forEach((element: any) => {
            var columnWidth = "150px";

            //TODO: Hacer esto dinamico
            if (element.ColumnName == "Descripcion") {
              columnWidth = "600px";
            }

            var newColumn = {
              name: element.ColumnName,
              sortOrder: null,
              sortFn: null,
              sortDirections: [null],
              filterMultiple: false,
              listOfFilter: [],
              // filterFn: (list: string[], item: any) => list.some(name => item[element.ColumnName].indexOf(name) !== -1)
              filterFn: null,
              width: columnWidth
            }

            this.listOfColumns.push(newColumn);
          });

          var newRow: any = [];

          ObjectData.RowHashtable.forEach((element: any) => {
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

  private sortFnByGrid(element: any) {
    return (a: any, b: any) => {
      const aValue = a[element.ColumnName];
      const bValue = b[element.ColumnName];

      if (aValue < bValue) {
        return -1;
      } else if (aValue > bValue) {
        return 1;
      } else {
        return 0;
      }
    };
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
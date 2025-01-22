import { ChangeDetectorRef, Component, Inject } from '@angular/core';
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
  currentReport: Report = new Report({});
  listOfData: any[] = [];
  listOfColumns: ColumnItem[] = [];

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef, private RVService: ReportViewerService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.route.params.subscribe(params => {

      const tokenData = this.tokenService.get();
      let genericRequest = {};

      if (tokenData != null) {
        genericRequest = {
          UserId: tokenData['userid'],
          Params: {
            Id: 10010
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


  OpenReport(report: Report) {
    debugger;
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

          this.cdr.detectChanges();
        });
    }
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
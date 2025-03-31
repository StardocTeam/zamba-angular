import { Component, inject, Inject } from '@angular/core';
import { Report } from '../report-component/entitie/report';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { Category } from './entity/Category';
import { ReportService } from './service/report.service';
import { catchError, of, tap } from 'rxjs';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { ZambaService } from 'src/app/services/zamba/zamba.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
  private route = inject(ActivatedRoute);
  CategoryList: Category[] = []
  inputValue?: string = "";

  report: Report = {
    Category: '',
    Description: '',
    Name: '',
    Query: '',
    Aditional: 0,
    Completar: '',
    ID: 0
  };
  cdr: any;
  isButtonDisabled: boolean = false;
  userId: any;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private REService: ReportService, private zambaService: ZambaService) {

  }

  ngOnInit() {
    debugger;
    //Report - EDITOR
    const tokenData = this.tokenService.get();

    let genericRequest = {};

    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['okten'],
      };
    } else {
      //throw new Error('Token not found');
    }


    if (tokenData != null) {
      this.zambaService.getUserId(genericRequest).pipe(
        tap(response => {
          response = JSON.parse(response);

          this.userId = response;
          this.tokenService.set({ token: tokenData['okten'], userid: response });

          this.getCategories(genericRequest);
          this.cdr.detectChanges();

        }),
        catchError(error => {
          console.error('Error fetching task name:', error);
          return of([]);
        })
      ).subscribe();

    } else {
      this.getCategories(genericRequest);
      this.cdr.detectChanges();
    }
  }

  private getCategories(genericRequest: {}) {
    this.REService.getCategories(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        throw error;
      })
    ).subscribe((data: any) => {
      this.CategoryList = JSON.parse(data);
    });
  }

  onNgDestroy() {
    console.log('ReportEditorComponent onNgDestroy');
  }

  InsertReport() {
    this.isButtonDisabled = true;

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          query: this.report.Query,
          name: this.report.Name,
          description: this.report.Description,
          category: this.report.Category,
          completar: this.report.Completar,
          Aditional: this.report.Aditional
        }
      };
    }

    this.REService.InsertReport(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        this.isButtonDisabled = false;
        throw error;
      })
    ).subscribe((data: any) => {
      this.CategoryList = JSON.parse(data);
    });

    this.isButtonDisabled = false;
    this.cdr.detectChanges();
  }
}

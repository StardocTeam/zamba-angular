import { Component, Inject } from '@angular/core';
import { Report } from '../report-component/entitie/report';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { Category } from './entity/Category';
import { ReportService } from './service/report.service';
import { catchError } from 'rxjs';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';

@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
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

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private REService: ReportService) {

  }

  ngOnInit() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
      };
    }

    this.REService.getCategories(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        throw error;
      })
    ).subscribe((data: any) => {
      this.CategoryList = JSON.parse(data);
    });

    this.cdr.detectChanges();
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

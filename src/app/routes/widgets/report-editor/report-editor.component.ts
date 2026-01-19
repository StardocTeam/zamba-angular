import { ChangeDetectorRef, Component, EventEmitter, inject, Inject, Output } from '@angular/core';
import { Report } from '../report-component/entitie/report';
import { FormsModule } from '@angular/forms';
import { NzInputModule } from 'ng-zorro-antd/input';
import { Category } from './entity/Category';
import { ReportService } from './service/report.service';
import { catchError, of, tap } from 'rxjs';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { ZambaService } from 'src/app/services/zamba/zamba.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NzModalService, NzModalRef } from 'ng-zorro-antd/modal';
import { ReportViewerService } from '../report-viewer/service/report-viewer.service';



@Component({
  selector: 'app-report-editor',
  templateUrl: './report-editor.component.html',
  styleUrls: ['./report-editor.component.less']
})
export class ReportEditorComponent {
  @Output() createTerminated: EventEmitter<string> = new EventEmitter<string>();
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
    ID: 0,
    GroupExpression: "",
    RuleId: null
  };

  isButtonDisabled: boolean = false;
  userId: any;
  ruleId: any;

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private REService: ReportService, private zambaService: ZambaService,
    private router: Router, private modal: NzModalService, private RVService: ReportViewerService) {

  }

  ngOnInit() {
    const tokenData = this.tokenService.get();

    let genericRequest = {};

    if (tokenData) {
      genericRequest = {
        UserId: tokenData['userid'],
        token: tokenData['token'],
      };
    }

    if (tokenData != null) {
      this.zambaService.getUserId(genericRequest).pipe(
        tap(response => {
          response = JSON.parse(response);

          this.userId = response;
          this.tokenService.set({ token: tokenData['token'], userid: response });

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

  TestQuery() {
    this.isButtonDisabled = true;
    var result: boolean = false;
    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userid'],
        Params: {
          Query: this.report.Query
        }
      };
    }

    this.RVService.GetReportByQuery(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        this.isButtonDisabled = false;
        throw error;
      })
    ).subscribe((data: any) => {
      this.isButtonDisabled = false;

      if (data == null) {
        console.log('Error: Ocurrio un error al ejecutar la sentencia', data);

        console.error('Error: La sentencia presenta errores');
        this.modal.error({
          nzTitle: 'Se ejecuto la sentencia pero presento errores',
          nzContent: '<p>Verifique que la sentencia no contenga errores y que la base de datos este bien configurada.</p>',
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });

        result = true;
      } else {
        var ObjectData = JSON.parse(data);

        this.modal.success({
          nzTitle: 'Ejecucion de sentencia exitosa',
          nzContent: '<p>Cantidad de registros obtenidos: ' + ObjectData.RowHashtable.length + '</p>',
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });
      }

      result = false;
    })
    //   ,
    //   catchError(error => {
    //     console.error('Error al obtener datos:', error);

    //     console.error('Error: Ocurrio un error al ejecutar la sentencia');
    //     this.modal.error({
    //       nzTitle: 'Ocurrio un error al ejecutar la sentencia',
    //       nzContent: '<p>Verifique que la sentencia no contenga errores y que la base de datos este bien configurada.</p>',
    //       nzOkText: 'OK',
    //       nzOkType: 'primary',
    //       nzOnOk: () => console.log('OK'),
    //     });

    //     result = false;
    //     throw error;
    //   })
    // ).subscribe();

    this.isButtonDisabled = false;
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
          groupExpression: this.report.GroupExpression,
          category: this.report.Category,
          completar: this.report.Completar,
          Aditional: this.report.Aditional,
          ruleId: this.report.RuleId
        }
      };
    }

    //TODO: TestQuery o sobrecarga del mismo para validar que no se inserte una query erronea

    this.REService.InsertReport(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        this.isButtonDisabled = false;
        throw error;
      })
    ).subscribe((data: any) => {
      var result = JSON.parse(data);


      if (data) {
        console.log('Insertado correctamente', this.report);

        this.modal.success({
          nzTitle: 'Insertado correctamente',
          nzContent: `<p>Reporte: ${this.report.Name}<br> ID: ${data}<br> Categoria: ${this.report.Category}<br> Regla: ${this.report.RuleId != null ? this.report.RuleId : 'ninguna'} </p>`,
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });

        this.createTerminated.emit();
        //this.navigateToListReport();
        this.clearForm();

      } else if (data == null) {
        console.log('No se ha insertado correctamente', data);

        console.error('Error: Ocurrio un error al insertar el reporte');
        this.modal.error({
          nzTitle: 'Ocurrio un error al insertar el reporte',
          nzContent: '<p>Verifique los datos ingresados.</p>',
          nzOkText: 'OK',
          nzOkType: 'primary',
          nzOnOk: () => console.log('OK'),
        });
      }

    });

    this.isButtonDisabled = false;
  }

  clearForm() {
    this.report = {
      Category: '',
      Description: '',
      Name: '',
      Query: '',
      Aditional: 0,
      Completar: '',
      ID: 0,
      GroupExpression: "",
      RuleId: null
    };
  }

  isFormValid(): boolean {
    return (
      this.report.Name.trim() !== '' &&
      this.report.Query.trim() !== '' &&
      this.report.Description.trim() !== '' &&
      this.report.Category.trim() !== ''
    );
  }

  _DebugForm() {
    this.report.Name = 'TEST select: ';
    this.report.Query = "select * from zopt where item like '%test%'";
    this.report.Description = 'Prueba ';
    this.report.Category = '1';
  }

  navigateToListReport() {
    const tokenData = this.tokenService.get();
    const queryParams: any = {};

    if (tokenData && tokenData['token']) {
      queryParams.t = tokenData['token'];
    }

    // Usa router.createUrlTree para obtener la ruta base sin fragmentos ni parámetros
    const baseUrl = this.router.url.split('?')[0].replace(/#.*$/, '');

    // Si la ruta base termina con 'view' o un id, elimínalos
    const cleanedBaseUrl = baseUrl
      .replace(/\/view(\/\d+)?$/, '')
      .replace(/\/create(\/\d+)?$/, '')
      .replace(/\/edit(\/\d+)?$/, '')
      .replace(/\/chartContainer(\/\d+)?$/, '');

    this.router.navigate([cleanedBaseUrl], { queryParams });

    //this.router.navigate(['/tools/reports'], { queryParams });
  }

}

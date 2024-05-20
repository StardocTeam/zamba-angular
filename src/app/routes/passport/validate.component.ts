import { HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ALLOW_ANONYMOUS, TokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { catchError, finalize, throwError } from 'rxjs';

@Component({
  selector: 'passport-validate',
  templateUrl: './validate.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ValidateComponent implements OnInit {
  showSuccessMessage = false;
  serverError = false;
  constructor(
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
    private tokenService: TokenService
  ) {}

  ngOnInit(): void {
    setTimeout(() => {
      this.route.queryParams.subscribe(params => {
        console.log('los parametros son', params);
        //convertir el objeto params en un array de objetos
        const tokenData: any = this.tokenService.get();
        let genericRequest = {};

        genericRequest = {
          UserId: tokenData['userID'],
          token: tokenData['token'],
          Params: params
        };
        this.http
          .post(`${environment['apiRestBasePath']}/ActivateUser`, genericRequest, null, {
            observe: 'response',
            responseType: 'json',
            context: new HttpContext().set(ALLOW_ANONYMOUS, true)
          })
          .pipe(
            catchError(error => {
              console.error('Error en la solicitud:', error);
              this.serverError = true;
              return throwError(() => error);
            }),
            finalize(() => {
              this.cdr.detectChanges();
            })
          )
          .subscribe(response => {
            this.showSuccessMessage = true;
          });
      });
      this.cdr.detectChanges();
    }, 2000);
  }

  //funcion para tomar parametros de la url
}

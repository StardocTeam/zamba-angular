import { HttpContext } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS, DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { catchError, throwError, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PassportService {
  constructor(
    private http: _HttpClient,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService
  ) { }

  doLogin(
    data: Partial<{
      email: string | null;
      password: string | null;
      mobile: string | null;
      captcha: string | null;
      remember: boolean | null;
    }>
  ) {
    const genericRequest = {
      UserId: 0,
      Params: data
    };
    return this.http
      .post(`${environment['apiRestBasePath']}/login`, genericRequest, null, {
        context: new HttpContext().set(ALLOW_ANONYMOUS, true)
      })
      .pipe(
        catchError(error => {
          console.error('Error en la solicitud:', error);
          return throwError(() => error);
        }),
        tap(res => {
          res = JSON.parse(res);
          debugger;
          console.log(res);
          res.user.time = +new Date();
          res.user.expired = -1;

          this.tokenService.set(res.user);

        })
      );
  }
}

import { TmplAstImmediateDeferredTrigger } from '@angular/compiler';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { Subscription, zip, filter, catchError } from 'rxjs';

import { employeeUser } from './entitie/employeeUser';
import { EmployeeUserService } from './service/employee-user.service';

@Component({
  selector: 'app-account-center',
  templateUrl: './center.component.html',
  styleUrls: ['./center.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProAccountCenterComponent implements OnInit {
  user: employeeUser = new employeeUser();

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
    private employeeUserService: EmployeeUserService
  ) {}

  ngOnInit(): void {
    this.cdr.detectChanges();

    const tokenData: any = this.tokenService.get();
    let genericRequest = {};
    if (tokenData != null) {
      console.log('Imprimo los valores en tokenService en el service', tokenData);

      genericRequest = {
        UserId: tokenData['userID'],
        token: tokenData['token'],
        Params: {}
      };

      this.employeeUserService
        .getEmployeeUser(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        )
        .subscribe((res: any) => {
          this.user = res;

          this.cdr.detectChanges();
        });
    }
  }
}

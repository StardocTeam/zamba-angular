import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { _HttpClient } from '@delon/theme';
import { Subscription, zip, filter, catchError } from 'rxjs';

import { employeeUser } from './entitie/employeeUser';
import { EmployeeUserService } from "./service/employee-user.service";
import { TmplAstImmediateDeferredTrigger } from '@angular/compiler';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';

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
  ) { }

  ngOnInit(): void {
    this.cdr.detectChanges();

    const tokenData = this.tokenService.get();
    let genericRequest = {};

    if (tokenData != null) {
      genericRequest = {
        UserId: tokenData['userID'],
        Params: {}
      };


      this.employeeUserService.getEmployeeUser(genericRequest)
        .pipe(
          catchError(error => {
            console.error('Error al obtener datos:', error);
            throw error;
          })
        ).subscribe((res: any) => {
          this.user = res;

          this.cdr.detectChanges();
        });
    }

  }
}

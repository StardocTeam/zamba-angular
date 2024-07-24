import { TmplAstImmediateDeferredTrigger } from '@angular/compiler';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { SettingsService, _HttpClient } from '@delon/theme';
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
  avatarSize = "200px";

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
    private employeeUserService: EmployeeUserService,
    private settings: SettingsService
  ) { }

  ngOnInit(): void {

    if (this.settings.user.avatar != null && this.settings.user.avatar != '' && this.settings.user.avatar != "data:image/jpg;base64,") {
      this.user.avatar = this.settings.user.avatar;
    }
    this.cdr.detectChanges();
  }
}

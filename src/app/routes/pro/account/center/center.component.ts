import { TmplAstImmediateDeferredTrigger } from '@angular/compiler';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, HostListener, Inject, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivationEnd, Router } from '@angular/router';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { SettingsService, _HttpClient } from '@delon/theme';
import { Subscription, zip, filter, catchError, of } from 'rxjs';

import { employeeUser } from './entitie/employeeUser';
import { EmployeeUserService } from './service/employee-user.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-account-center',
  templateUrl: './center.component.html',
  styleUrls: ['./center.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProAccountCenterComponent implements OnInit {
  user: employeeUser = new employeeUser();
  avatarSize = "200px";
  isMobile: any;
  private breakpointSubscription!: Subscription;

  constructor(
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
    private employeeUserService: EmployeeUserService,
    private settings: SettingsService,
    private breakpointObserver: BreakpointObserver
  ) { }

  ngOnInit(): void {
    this.isMobile = this.breakpointObserver.isMatched([Breakpoints.Handset]);

    if (this.settings.user.avatar != null && this.settings.user.avatar != '' && this.settings.user.avatar != "data:image/jpg;base64,") {
      this.user.avatar = this.settings.user.avatar;
    }
    this.getEmployeeUserInfo();
    this.cdr.detectChanges();
  }


  private SwitchViewMode() {
    this.breakpointSubscription = this.breakpointObserver.observe([
      Breakpoints.Handset
    ]).subscribe(result => {
      this.isMobile = result.matches;
    });
  }

  @HostListener('window:resize')
  onResize() {
    this.SwitchViewMode();
  }

  getEmployeeUserInfo() {
    const tokenData: any = this.tokenService.get();
    let genericRequest = {};
    genericRequest = {
      UserId: 0,
      token: tokenData['token'],

      Params: {
        EntityID: '258',
        DoctypesId: '110'
      }
    };

    this.employeeUserService.getEmployeeUser(genericRequest)
      .pipe(
        filter(data => data != '[]'),
        catchError(error => {
          console.error('An error occurred:', error);
          return of('[]');
        })
      )
      .subscribe(data => {
        var dataJson = JSON.parse(data)[0];

        this.user.name = dataJson.name;
        this.user.lastName = dataJson.lastName;
        this.user.workEmail = dataJson.workEmail;
        this.user.workCellPhone = dataJson.workCellPhone;
        this.user.birthday = dataJson.birthday;

        this.user.company = dataJson.company;
        this.user.department = dataJson.department;
        this.user.workMode = dataJson.workMode;
        this.user.area = dataJson.area;
        this.user.employmentStatus = dataJson.employmentStatus;
        this.user.position = dataJson.position;
        this.user.dateEmploymentEntry = dataJson.dateEmploymentEntry;

        this.cdr.detectChanges();

      });


  }
}

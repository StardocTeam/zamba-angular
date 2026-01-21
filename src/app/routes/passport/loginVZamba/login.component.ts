import { HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, Optional } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { StartupService } from '@core';
import { ReuseTabService } from '@delon/abc/reuse-tab';
import { ALLOW_ANONYMOUS, DA_SERVICE_TOKEN, ITokenService, SocialOpenType, SocialService } from '@delon/auth';
import { SettingsService, _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { NzTabChangeEvent } from 'ng-zorro-antd/tabs';
import { catchError, finalize, Subscription, throwError } from 'rxjs';

import { PassportService } from '../services/passport.service';

@Component({
  selector: 'passport-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  providers: [SocialService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserLoginVZambaComponent implements OnDestroy, OnInit {
  subscriptions: Subscription[] = [];

  form = this.fb.group({
    email: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/)]],
    mobile: ['', [Validators.required, Validators.pattern(/^1\d{10}$/)]],
    captcha: ['', [Validators.required]],
    remember: [true]
  });
  error = '';
  serverError = false;
  authServerError = false;
  type = 0;
  loading = false;
  errorUserIsNotActive = false;

  // #region get captcha

  count = 0;
  interval$: any;
  passwordVisible = false;
  safeZambaUrl: SafeResourceUrl = '';

  constructor(
    private sanitizer: DomSanitizer,

    private fb: FormBuilder,
    private router: Router,
    private settingsService: SettingsService,
    private socialService: SocialService,
    @Optional()
    @Inject(ReuseTabService)
    private reuseTabService: ReuseTabService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private startupService: StartupService,
    private cdr: ChangeDetectorRef,
    private passportService: PassportService
  ) { }
  ngOnInit(): void {
    window.addEventListener('message', event => {
      const response = JSON.parse(event.data);
      if (response.data === 'login-rrhh-ok') {
        console.log('Ha devueto un Ok el sitio web de zamba');
        this.router.navigateByUrl('/');
      } else if (response.data === 'login-rrhh-error') {
        this.authServerError = true;
        this.cdr.detectChanges();
      }
    });
  }

  submit(): void {
    this.error = '';
    this.serverError = false;
    this.errorUserIsNotActive = false;
    this.authServerError = false;
    const { email, password } = this.form.controls;
    email.markAsDirty();
    email.updateValueAndValidity();
    password.markAsDirty();
    password.updateValueAndValidity();
    if (email.invalid || password.invalid) {
      return;
    }

    const data = this.form.value;

    this.loading = true;
    this.cdr.detectChanges();

    this.subscriptions.push(
      this.passportService
        .doLogin(data)
        .pipe(
          catchError(error => {
            console.error('Error en la solicitud:', error);
            this.serverError = true;
            return throwError(() => error);
          }),
          finalize(() => {
            this.loading = false;
            this.cdr.detectChanges();
          })
        )
        .subscribe(res => {
          res = JSON.parse(res);
          console.log(res);
          if (res.msg == 'Invalid username or password') {
            this.error = res.msg;
            this.cdr.detectChanges();
            return;
          } else if (res.isActive == false) {
            this.errorUserIsNotActive = true;
            this.cdr.detectChanges();
            return;
          }
          else if (res.isActive == true) {
            this.reuseTabService.clear();

            this.startupService.load().subscribe(() => {
              let url = this.tokenService.referrer!.url || '/';

              if (url.includes('/passport'))
                url = '/';

              let tokenService = this.tokenService.get();
              console.log(tokenService);

              this.router.navigateByUrl('main');
              this.cdr.detectChanges();
            });
          }

        })
    );
  }
  ngOnDestroy(): void {
    if (this.interval$) {
      clearInterval(this.interval$);
    }
    this.subscriptions.forEach(sub => {
      sub.unsubscribe();
    });
  }
}

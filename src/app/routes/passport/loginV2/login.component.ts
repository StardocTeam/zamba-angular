import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, Optional } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { SafeResourceUrl } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { ReuseTabService } from '@delon/abc/reuse-tab';
import { DA_SERVICE_TOKEN, ITokenService, SocialService } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { catchError, finalize, Subscription, throwError } from 'rxjs';
import { ZambaService } from 'src/app/services/zamba/zamba.service';

import { PassportService } from '../services/passport.service';

@Component({
  selector: 'passport-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.less'],
  providers: [SocialService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserLoginV2Component implements OnDestroy, OnInit {
  subscriptions: Subscription[] = [];

  form = this.fb.group({
    email: ['', [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.pattern(/^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]*$/)]],
    mobile: ['', [Validators.required, Validators.pattern(/^1\d{10}$/)]],
    captcha: ['', [Validators.required]],
    remember: [true]
  });
  error = false;
  serverError = false;
  authServerError = false;
  type = 0;
  loading = false;
  errorUserIsNotActive = false;

  // #region get captcha

  count = 0;
  interval$: any;

  safeZambaUrl: SafeResourceUrl = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    @Optional()
    @Inject(ReuseTabService)
    private reuseTabService: ReuseTabService,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
    private passportService: PassportService,
    private zambaService: ZambaService
  ) {
    this.responseFromZambaLogin = this.responseFromZambaLogin.bind(this);
  }
  ngOnInit(): void {
    window.addEventListener('message', this.responseFromZambaLogin);
  }

  responseFromZambaLogin(event: MessageEvent) {
    try {
      var message = JSON.parse(event.data);

      switch (message.type) {
        case 'auth':
          console.log(message.data);

          if (message.data === 'login-rrhh-ok') {
            console.log('Ha devueto un Ok el sitio web de zamba');
            window.removeEventListener('message', this.responseFromZambaLogin);
            this.safeZambaUrl = '';
            this.router.navigateByUrl('/dashboard');
          } else if (message.data === 'login-rrhh-error') {
            this.authServerError = true;
            this.cdr.detectChanges();
          }
          break;
      }
    } catch (error) {}
  }

  submit(): void {
    this.error = false;
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
            if (error.status == 403) this.error = true;
            else this.serverError = true;

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
            this.error = true;
            this.cdr.detectChanges();
            return;
          } else if (res.isActive == false) {
            this.errorUserIsNotActive = true;
            this.cdr.detectChanges();
            return;
          }
          this.reuseTabService.clear();

          this.safeZambaUrl = this.zambaService.preFlightLogin();

          // this.router.navigateByUrl('/dashboard');
          this.cdr.detectChanges();
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

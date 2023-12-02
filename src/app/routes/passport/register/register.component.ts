import { HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { MatchControl } from '@delon/util/form';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { catchError, finalize, throwError } from 'rxjs';

import { environment } from '@env/environment';

@Component({
  selector: 'passport-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UserRegisterComponent implements OnDestroy {
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
  ) { }


  // #region fields

  form = this.fb.nonNullable.group(
    {
      companyName: ['', [Validators.required, Validators.maxLength(50)]],
      name: ['', [Validators.required, Validators.maxLength(50)]],
      lastname: ['', [Validators.required, Validators.maxLength(50)]],
      department: ['', [Validators.required]],
      rol: ['', [Validators.required]],
      mail: ['', [Validators.required, Validators.email, Validators.maxLength(50)]],
      username: ['', [Validators.required, Validators.maxLength(50)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(50), UserRegisterComponent.checkPassword.bind(this)]],
      confirm: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(50)]],
      mobilePrefix: ['+86'],
      mobile: ['', [Validators.required, Validators.maxLength(50)]],
    },
    {
      validators: MatchControl('password', 'confirm')
    }
  );

  disableSubmitButton = true;
  error = '';
  type = 0;
  loading = false;
  visible = false;
  status = 'pool';
  progress = 0;
  passwordProgressMap: { [key: string]: 'success' | 'normal' | 'exception' } = {
    ok: 'success',
    pass: 'normal',
    pool: 'exception'
  };

  // #endregion

  // #region get captcha

  count = 0;
  interval$: NzSafeAny;

  static checkPassword(control: FormControl): NzSafeAny {
    if (!control) {
      return null;
    }
    // eslint-disable-next-line @typescript-eslint/no-this-alias
    const self: NzSafeAny = this;
    self.visible = !!control.value;
    if (control.value && control.value.length > 9) {
      self.status = 'ok';
    } else if (control.value && control.value.length > 5) {
      self.status = 'pass';
    } else {
      self.status = 'pool';
    }

    if (self.visible) {
      self.progress = control.value.length * 10 > 100 ? 100 : control.value.length * 10;
    }
  }


  resolved(captchaResponse: string) {
    console.log(`Resolved captcha with response: ${captchaResponse}`);
    this.disableSubmitButton = false;
    console.log("disabledSubmitButton", this.disableSubmitButton);
    console.log("form valid", this.form.invalid);
    console.log(this.form.errors)
    this.cdr.detectChanges();
  }

  // #endregion
  submit(): void {
    this.error = '';
    Object.keys(this.form.controls).forEach(key => {
      const control = (this.form.controls as NzSafeAny)[key] as AbstractControl;
      control.markAsDirty();
      control.updateValueAndValidity();
    });
    if (this.form.invalid) {
      return;
    }

    const data = this.form.value;
    const genericRequest = {
      UserId: 0,
      Params: data
    };

    this.loading = true;
    this.cdr.detectChanges();
    this.http
      .post(`${environment.apiRestBasePath}/register`, genericRequest, null, {
        observe: 'response',
        responseType: 'json',
        context: new HttpContext().set(ALLOW_ANONYMOUS, true)
      })
      .pipe(
        catchError((error) => {
          console.error('Error en la solicitud:', error);

          return throwError(() => error);
        }),
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        })
      )
      .subscribe((response) => {
        var dataResponse = JSON.parse(response.body);
        if (dataResponse != null && dataResponse != undefined && dataResponse != "") {

          if (dataResponse.usernameIsTaken) {
            const usernameControl = this.form.get('username');
            if (usernameControl) {
              usernameControl.setErrors({ usernameExists: true });
            }
          }
          if (dataResponse.emailIsTaken) {
            const mailControl = this.form.get('mail');
            if (mailControl) {
              mailControl.setErrors({ emailExists: true });
            }
          }
          if (!dataResponse.emailIsTaken && !dataResponse.usernameIsTaken) {
            this.router.navigate(['passport', 'register-result'], { queryParams: { email: data.mail } });
          }
        }
      });
  }
  ngOnDestroy(): void {
    if (this.interval$) {
      clearInterval(this.interval$);
    }
  }
}
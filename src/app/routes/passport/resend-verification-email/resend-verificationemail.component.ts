import { HttpContext } from '@angular/common/http';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { MatchControl } from '@delon/util/form';
import { environment } from '@env/environment';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { catchError, finalize, throwError } from 'rxjs';

@Component({
  selector: 'resend-verificationemail',
  templateUrl: './resend-verificationemail.component.html',
  styleUrls: ['./resend-verificationemail.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ResendVerificationEmailComponent implements OnDestroy {
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private http: _HttpClient,
    private cdr: ChangeDetectorRef,
  ) {}

  // #region fields

  form = this.fb.nonNullable.group({
    mail: ['', [Validators.required, Validators.email, Validators.maxLength(50)]],
  });

  disableSubmitButton = true;
  error = '';
  serverError = false;
  type = 0;
  loading = false;
  visible = false;
  status = 'pool';
  progress = 0;
  passwordProgressMap: { [key: string]: 'success' | 'normal' | 'exception' } = {
    ok: 'success',
    pass: 'normal',
    pool: 'exception',
  };

  body: String = '';

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
    console.log('disabledSubmitButton', this.disableSubmitButton);
    console.log('form valid', this.form.invalid);
    console.log(this.form.errors);
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
      Params: data,
    };

    this.serverError = false;
    this.loading = true;
    this.cdr.detectChanges();
    this.http
      .post(`${environment['apiRestBasePath']}/ResendVerificationEmail`, genericRequest, null, {
        observe: 'response',
        responseType: 'json',
        context: new HttpContext().set(ALLOW_ANONYMOUS, true),
      })
      .pipe(
        catchError(error => {
          console.error('Error en la solicitud:', error);
          this.serverError = true;
          return throwError(() => error);
        }),
        finalize(() => {
          this.loading = false;
          this.cdr.detectChanges();
        }),
      )
      .subscribe(response => {
        this.router.navigate(['passport', 'resend-result'], { queryParams: { email: data.mail } });
      });
  }

  ngOnDestroy(): void {
    if (this.interval$) {
      clearInterval(this.interval$);
    }
  }
}

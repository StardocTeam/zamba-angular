import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({ selector: 'app-mobile-login', templateUrl: './mobile-login.component.html', styleUrls: ['./mobile-login.component.less'] })
export class MobileLoginComponent {
  @Input() busy = false;
  @Input() errorMessage = '';
  @Input() mockMode = false;
  @Output() submitLogin = new EventEmitter<{ username: string; password: string }>();

  username = '';
  password = '';

  submit(): void {
    if (this.username.trim() && this.password) this.submitLogin.emit({ username: this.username.trim(), password: this.password });
  }
}
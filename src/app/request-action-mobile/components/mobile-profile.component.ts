import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({ selector: 'app-mobile-profile', templateUrl: './mobile-profile.component.html', styleUrls: ['./mobile-profile.component.less'] })
export class MobileProfileComponent {
  @Input() username = '';
  @Input() busy = false;
  @Input() message = '';
  @Output() savePassword = new EventEmitter<{ currentPassword: string; newPassword: string; confirmation: string }>();
  @Output() signOut = new EventEmitter<void>();

  currentPassword = '';
  newPassword = '';
  confirmation = '';

  save(): void {
    this.savePassword.emit({ currentPassword: this.currentPassword, newPassword: this.newPassword, confirmation: this.confirmation });
  }
}

import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DashboardCounts, RequestActionQueue } from '../request-action-mobile.model';

@Component({ selector: 'app-mobile-home', templateUrl: './mobile-home.component.html', styleUrls: ['./mobile-home.component.less'] })
export class MobileHomeComponent {
  @Input() username = '';
  @Input() counts: DashboardCounts = { approval: 0, confirmation: 0, summary: 0 };
  @Output() openQueue = new EventEmitter<RequestActionQueue>();

  readonly today = new Date();

  get firstName(): string { return this.username.split(/[.\s]/)[0] || 'Hola'; }
}

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';

import { RequestActionMobileRoutingModule } from './request-action-mobile-routing.module';
import { RequestActionMobileComponent } from './request-action-mobile.component';
import { MobileHomeComponent } from './components/mobile-home.component';
import { MobileLoginComponent } from './components/mobile-login.component';
import { MobileProfileComponent } from './components/mobile-profile.component';
import { MobileSummaryDetailComponent } from './components/mobile-summary-detail.component';
import { MobileTaskCardComponent } from './components/mobile-task-card.component';
import { MobileTaskListComponent } from './components/mobile-task-list.component';

@NgModule({
  declarations: [RequestActionMobileComponent, MobileHomeComponent, MobileLoginComponent, MobileProfileComponent, MobileSummaryDetailComponent, MobileTaskCardComponent, MobileTaskListComponent],
  imports: [CommonModule, FormsModule, RequestActionMobileRoutingModule],
})
export class RequestActionMobileModule {}

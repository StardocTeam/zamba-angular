import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { RequestActionMobileComponent } from './request-action-mobile.component';

const routes: Routes = [{ path: '', component: RequestActionMobileComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class RequestActionMobileRoutingModule {}

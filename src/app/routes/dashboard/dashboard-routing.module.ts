import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardAnalysisComponent } from './analysis/analysis.component';
import { DashboardMonitorComponent } from './monitor/monitor.component';
import { DashboardV1Component } from './v2/v2.component';
import { DashboardWorkplaceComponent } from './workplace/workplace.component';
import { DefaultComponent } from '../default/default.component';
import { WidgetsContainerComponent } from '../widgets-container/widgets-container.component';
import { SignatureComponent } from 'src/app/signature/signature.component';

const routes: Routes = [
  { path: '', redirectTo: 'default', pathMatch: 'full' },
  { path: 'default', component: DefaultComponent },
  { path: 'v2', component: DashboardV1Component },
  { path: 'analysis', component: DashboardAnalysisComponent },
  { path: 'monitor', component: DashboardMonitorComponent },
  { path: 'workplace', component: DashboardWorkplaceComponent },
  { path: 'widgets', component: WidgetsContainerComponent },
  { path: 'signature', loadChildren: () => import('src/app/signature/signature.module').then(m => m.SignatureModule) },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }

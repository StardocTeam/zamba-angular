import { RouterModule, Routes } from '@angular/router';

import { NgModule } from '@angular/core';
import { RuleComponent } from './rule/rule.component';
import { ViewFormComponent } from '../widgets/view-form/view-form.component';
import { ZambaService } from '../../services/zamba/zamba.service';
import { aclCanActivate } from '@delon/acl';

const routes: Routes = [
  { path: 'rule', component: RuleComponent, title: 'Formulario de WorkFlow' },
  { path: 'form', component: ViewFormComponent, title: 'Formulario' },
  {
    path: 'schedule',
    loadChildren: () => import('./schedule/schedule.module').then(m => m.ScheduleModule),
    title: 'Schedules'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ZambaRoutingModule {}

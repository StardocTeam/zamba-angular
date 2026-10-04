import { RouterModule, Routes } from '@angular/router';

import { ExecutionLogsComponent } from './schedule-logs/execution-logs.component';
import { NgModule } from '@angular/core';
import { ScheduleDashboardComponent } from './schedule-dashboard/schedule-dashboard.component';
import { ScheduleDetailComponent } from './schedule-detail/schedule-detail.component';
import { ScheduleFormComponent } from './schedule-form/schedule-form.component';
import { ScheduleListComponent } from './schedule-list/schedule-list.component';

const routes: Routes = [
  {
    path: 'dashboard',
    component: ScheduleDashboardComponent,
    data: { title: 'Schedule Monitor' }
  },
  {
    path: '',
    component: ScheduleListComponent,
    data: { title: 'Schedules' }
  },
  {
    path: 'new',
    component: ScheduleFormComponent,
    data: { title: 'Create Schedule' }
  },
  {
    path: ':id',
    component: ScheduleDetailComponent,
    data: { title: 'Schedule Details' }
  },
  {
    path: ':id/edit',
    component: ScheduleFormComponent,
    data: { title: 'Edit Schedule' }
  },
  {
    path: ':id/logs',
    component: ExecutionLogsComponent,
    data: { title: 'Execution Logs' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ScheduleRoutingModule { }

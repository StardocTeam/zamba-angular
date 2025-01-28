import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CalendarComponent } from './calendar/calendar.component';
import { CarouselComponent } from './carousel/carousel.component';
import { PendingTasksComponent } from './pending-tasks/pending-tasks.component';
import { PendingVacationsComponent } from './pending-vacations/pending-vacations.component';
import { WidgetsComponent } from './widgets/widgets.component';
import { ReportComponentComponent } from './report-component/report-component.component';
import { ReportViewerComponent } from './report-viewer/report-viewer.component';
import { ReportEditorComponent } from './report-editor/report-editor.component';

const routes: Routes = [
  { path: 'calendar', component: CalendarComponent },
  { path: 'carousel', component: CarouselComponent },
  { path: 'pending-tasks', component: PendingTasksComponent },
  { path: 'pending-vacations', component: PendingVacationsComponent },
  {
    path: 'report', component: ReportComponentComponent, children: [
      { path: 'create', component: ReportEditorComponent },
      { path: 'edit/:id', component: ReportEditorComponent },
      { path: ':id', component: ReportViewerComponent },
    ]
  },
  //{ path: "create", component: ReportEditorComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WidgetsRoutingModule { }

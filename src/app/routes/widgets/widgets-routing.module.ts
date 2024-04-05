import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CalendarComponent } from './calendar/calendar.component';
import { CarouselComponent } from './carousel/carousel.component';
import { PendingTasksComponent } from './pending-tasks/pending-tasks.component';
import { PendingVacationsComponent } from './pending-vacations/pending-vacations.component';
import { WidgetsComponent } from './widgets/widgets.component';

const routes: Routes = [
  { path: 'calendar', component: CalendarComponent },
  { path: 'carousel', component: CarouselComponent },
  { path: 'pending-tasks', component: PendingTasksComponent },
  { path: 'pending-vacations', component: PendingVacationsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WidgetsRoutingModule {}

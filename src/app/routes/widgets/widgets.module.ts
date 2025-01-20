import { ScrollingModule } from '@angular/cdk/scrolling';
import { NgModule, Type } from '@angular/core';
import { YouTubePlayerModule } from '@angular/youtube-player';
import { G2MiniAreaModule } from '@delon/chart/mini-area';
import { G2MiniBarModule } from '@delon/chart/mini-bar';
import { ContextMenuModule } from '@perfectmemory/ngx-contextmenu';
import { SharedModule } from '@shared';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { FlatpickrModule } from 'angularx-flatpickr';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzTreeViewModule } from 'ng-zorro-antd/tree-view';

import {
  NzTableFilterFn,
  NzTableFilterList,
  NzTableModule,
  NzTableSortFn,
  NzTableSortOrder
} from 'ng-zorro-antd/table';

import { CalendarComponent } from './calendar/calendar.component';
import { CarouselComponent } from './carousel/carousel.component';
import { PendingTasksComponent } from './pending-tasks/pending-tasks.component';
import { PendingVacationsComponent } from './pending-vacations/pending-vacations.component';
import { VideoplayerComponent } from './videoplayer/videoplayer.component';
import { WidgetsComponent } from './widgets/widgets.component';
import { WidgetsRoutingModule } from './widgets-routing.module';
import { ReportComponentComponent } from './report-component/report-component.component';



const COMPONENTS: Array<Type<void>> = [
  WidgetsComponent,
  CalendarComponent,
  CarouselComponent,
  VideoplayerComponent,
  PendingTasksComponent,
  PendingVacationsComponent,
  ReportComponentComponent
];

@NgModule({
  imports: [
    SharedModule,
    WidgetsRoutingModule,
    NzCarouselModule,
    G2MiniBarModule,
    G2MiniAreaModule,
    FlatpickrModule.forRoot(),
    NzModalModule,
    NzInputModule,
    NzGridModule,
    NzTypographyModule,
    NzSelectModule,
    YouTubePlayerModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory
    }),
    NzLayoutModule,
    ContextMenuModule,
    NzSkeletonModule,
    ScrollingModule,
    NzTreeViewModule,
    NzTableModule
  ],
  declarations: COMPONENTS,
  exports: COMPONENTS
})
export class WidgetsModule { }

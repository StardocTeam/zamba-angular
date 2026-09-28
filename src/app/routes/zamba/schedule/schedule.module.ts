import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { CommonModule } from '@angular/common';
import { ExecutionLogsComponent } from './schedule-logs/execution-logs.component';
import { NgModule } from '@angular/core';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzModalService } from 'ng-zorro-antd/modal';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { ScheduleDetailComponent } from './schedule-detail/schedule-detail.component';
import { ScheduleFormComponent } from './schedule-form/schedule-form.component';
import { ScheduleListComponent } from './schedule-list/schedule-list.component';
import { ScheduleRoutingModule } from './schedule-routing.module';
import { SharedModule } from '@shared';

@NgModule({
  declarations: [
    ScheduleListComponent,
    ScheduleFormComponent,
    ExecutionLogsComponent,
    ScheduleDetailComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    SharedModule,
    ScheduleRoutingModule,

    // ng-zorro modules
    NzTableModule,
    NzPaginationModule,
    NzButtonModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzDatePickerModule,
    NzTimePickerModule,
    NzCheckboxModule,
    NzRadioModule,
    NzSwitchModule,
    NzBadgeModule,
    NzTagModule,
    NzDropDownModule,
    NzModalModule,
    NzMessageModule,
    NzSpinModule,
    NzIconModule,
    NzToolTipModule,
    NzGridModule
  ]
})
export class ScheduleModule { }


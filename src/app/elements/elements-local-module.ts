import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { Injector, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { DefaultInterceptor } from '@core';
import { DelonAuthModule } from '@delon/auth';
import { SharedModule } from '@shared';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { CoreModule } from 'src/app/core/core.module';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';

import { ChecklistComponent } from './checklist/checklist.component';

const routes: Routes = [{ path: '', component: ChecklistComponent }];

@NgModule({
  declarations: [ChecklistComponent],
  imports: [
    CommonModule,
    BrowserModule,
    DelonAuthModule,
    HttpClientModule,
    CoreModule,
    SharedModule,
    FormsModule,
    NzIconModule,
    RouterModule.forChild(routes),
  ],
  providers: [HttpClientModule, ReportViewerService, { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true }],
  exports: [ChecklistComponent, NzIconModule],
})
export class ElementsLocalModule {
  constructor() {}
}

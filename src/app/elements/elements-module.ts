import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { Injector, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BrowserModule } from '@angular/platform-browser';
import { ChecklistComponent } from './checklist/checklist.component';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';
import { DefaultInterceptor } from '@core';
import { DelonAuthModule } from '@delon/auth';
import { FormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { SharedModule } from '@shared';
import { createCustomElement } from '@angular/elements';

const routes: Routes = [
  { path: '', component: ChecklistComponent }
];

@NgModule({
  declarations: [ChecklistComponent],
  imports: [CommonModule, BrowserModule, DelonAuthModule, HttpClientModule, CoreModule, SharedModule,
    FormsModule, NzIconModule, RouterModule.forChild(routes)],
  providers: [HttpClientModule, ReportViewerService, { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true }],
  exports: [ChecklistComponent, NzIconModule],
})
export class ElementsModule {

  constructor(private injector: Injector) {

  }

  ngDoBootstrap() {
    const ChecklistElement = createCustomElement(ChecklistComponent, { injector: this.injector });
    if (!customElements.get('zamba-checklist')) {
      customElements.define('zamba-checklist', ChecklistElement);
    }
  }

}

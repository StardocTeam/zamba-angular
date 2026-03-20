import { DoBootstrap, Injector, NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ChecklistComponent } from './checklist/checklist.component';
import { CommonModule } from '@angular/common';
import { CoreModule } from 'src/app/core/core.module';
import { DefaultInterceptor } from '@core';
import { DelonAuthModule, TokenService } from '@delon/auth';
import { EditorModule } from '@tinymce/tinymce-angular';
import { FormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { GlobalConfigModule } from 'src/app/global-config.module';
import { SharedModule } from '@shared';
import { TinymceElementComponent } from './tinymce-editor/tinymce-editor.component';
import { createCustomElement } from '@angular/elements';

const routes: Routes = [
  { path: '', component: ChecklistComponent },
  { path: 'tinymce-editor', component: TinymceElementComponent }
];

@NgModule({
  declarations: [ChecklistComponent, TinymceElementComponent],
  imports: [CommonModule, BrowserModule, BrowserAnimationsModule, DelonAuthModule, HttpClientModule, GlobalConfigModule.forRoot(), CoreModule, SharedModule,
    FormsModule, NzIconModule, EditorModule, RouterModule.forRoot(routes)],
  providers: [HttpClientModule, ReportViewerService, TokenService, { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true }],
  exports: [ChecklistComponent, TinymceElementComponent, NzIconModule],
})
export class ElementsModule implements DoBootstrap {

  constructor(private readonly injector: Injector) {

  }

  ngDoBootstrap(): void {
    const ChecklistElement = createCustomElement(ChecklistComponent, { injector: this.injector });
    if (!customElements.get('zamba-checklist')) {
      customElements.define('zamba-checklist', ChecklistElement);
    }

    const TinymceEditorElement = createCustomElement(TinymceElementComponent, { injector: this.injector });
    if (!customElements.get('zamba-tinymce-editor')) {
      customElements.define('zamba-tinymce-editor', TinymceEditorElement);
    }
  }

}

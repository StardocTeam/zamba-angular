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
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { GlobalConfigModule } from 'src/app/global-config.module';
import { SharedModule } from '@shared';
import { TinymceElementComponent } from './tinymce-editor/tinymce-editor.component';
import { GlobalSearchElementComponent } from './global-search/global-search.component';
import { createCustomElement } from '@angular/elements';

const routes: Routes = [
  { path: '', component: ChecklistComponent },
  { path: 'tinymce-editor', component: TinymceElementComponent },
  { path: 'global-search', component: GlobalSearchElementComponent, data: { EntityId: 'HB Documentos', IndexId: 'GlobalSearch' } }
];

@NgModule({
  declarations: [ChecklistComponent, TinymceElementComponent, GlobalSearchElementComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    DelonAuthModule,
    HttpClientModule,
    GlobalConfigModule.forRoot(),
    CoreModule,
    SharedModule,
    FormsModule,
    NzAlertModule,
    NzButtonModule,
    NzCardModule,
    NzIconModule,
    NzMessageModule,
    NzSwitchModule,
    EditorModule,
    RouterModule.forRoot(routes)
  ],
  providers: [HttpClientModule, ReportViewerService, TokenService, { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true }],
  exports: [ChecklistComponent, TinymceElementComponent, GlobalSearchElementComponent, NzIconModule],
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

    const GlobalSearchElement = createCustomElement(GlobalSearchElementComponent, { injector: this.injector });
    if (!customElements.get('zamba-global-search')) {
      customElements.define('zamba-global-search', GlobalSearchElement);
    }

  }

}

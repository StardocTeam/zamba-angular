import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DoBootstrap, Injector, NgModule } from '@angular/core';
import { createCustomElement } from '@angular/elements';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { DefaultInterceptor } from '@core';
import { DelonAuthModule, TokenService } from '@delon/auth';
import { SharedModule } from '@shared';
import { EditorModule } from '@tinymce/tinymce-angular';

import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { CoreModule } from 'src/app/core/core.module';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { GlobalConfigModule } from 'src/app/global-config.module';
import { ChecklistComponent } from './checklist/checklist.component';

import { GlobalSearchElementComponent } from './global-search/global-search.component';
import { MiniwebBookmarkComponent } from './miniweb-bookmark/miniweb-bookmark.component';
import { TinymceElementComponent } from './tinymce-editor/tinymce-editor.component';
import { WebBookmarkComponent } from './web-bookmark/web-bookmark.component';

const routes: Routes = [
  { path: '', component: ChecklistComponent },
  { path: 'tinymce-editor', component: TinymceElementComponent },
  { path: 'global-search', component: GlobalSearchElementComponent, data: { EntityId: 'HB Documentos', IndexId: 'GlobalSearch' } },
  { path: 'web-bookmark', component: WebBookmarkComponent },
  { path: 'miniweb-bookmark', component: MiniwebBookmarkComponent },
];

@NgModule({
  declarations: [ChecklistComponent, TinymceElementComponent, GlobalSearchElementComponent, WebBookmarkComponent, MiniwebBookmarkComponent],
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
    RouterModule.forRoot(routes),
  ],
  providers: [
    HttpClientModule,
    ReportViewerService,
    TokenService,
    { provide: HTTP_INTERCEPTORS, useClass: DefaultInterceptor, multi: true },
  ],
  exports: [
    ChecklistComponent,
    TinymceElementComponent,
    GlobalSearchElementComponent,
    WebBookmarkComponent,
    MiniwebBookmarkComponent,
    NzIconModule,
  ],
})
export class ElementsModule implements DoBootstrap {
  constructor(private readonly injector: Injector) { }

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

    const WebBookmarkElement = createCustomElement(WebBookmarkComponent, { injector: this.injector });
    if (!customElements.get('zamba-web-bookmark')) {
      customElements.define('zamba-web-bookmark', WebBookmarkElement);
    }

    const MiniwebBookmarkElement = createCustomElement(MiniwebBookmarkComponent, { injector: this.injector });
    if (!customElements.get('zamba-miniweb-bookmark')) {
      customElements.define('zamba-miniweb-bookmark', MiniwebBookmarkElement);
    }
  }
}

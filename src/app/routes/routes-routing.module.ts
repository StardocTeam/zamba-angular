import { RouterModule, Routes } from '@angular/router';

import { ChartContainerComponent } from '../components/chart-container/chart-container.component';
import { Ckeditor5PremiumEditorComponent } from '../components/ckeditor5-premium-editor/ckeditor5-premium-editor.component';
import { DoShowTableComponent } from '../components/doshowtable/do-show-table/do-show-table.component';
import { DocxEditorComponent } from '../components/docx-editor/docx-editor.component';
import { LayoutSimpleComponent } from '../layout/simple/simple.component';
import { NgModule } from '@angular/core';
import { OnlyofficeEditorComponent } from '../components/onlyoffice-editor/onlyoffice-editor.component';
import { PermissionsUserGroupComponent } from '../components/permissions-user-group/permissions-user-group';
import { PreloadOptionalModules } from '@delon/theme';
import { QuickActionsComponent } from '../components/quick-actions/quick-actions.component';
import { ReportComponentComponent } from "./widgets/report-component/report-component.component";
import { ReportEditorComponent } from './widgets/report-editor/report-editor.component';
import { ReportViewerComponent } from './widgets/report-viewer/report-viewer.component';
import { SignatureContainerComponent } from '../signature-container/signature-container.component';
import { TaskHistoryComponent } from '../components/task-history/task-history.component';
import { TinymcePremiumEditorComponent } from '../components/tinymce-premium-editor/tinymce-premium-editor.component';
import { ViewFormComponent } from './widgets/view-form/view-form.component';
import { environment } from '@env/environment';
import { TinymceElementComponent } from '../elements/tinymce-editor/tinymce-editor.component';
import { GlobalSearchElementComponent } from '../elements/global-search/global-search.component';

// Lazy-load CheckListModule below; remove static import

const routes: Routes = [
  {
    path: 'tools',
    component: LayoutSimpleComponent,
    children: [
      {
        path: 'elements',
        loadChildren: () => import('../elements/elements-module').then(m => m.ElementsModule),
        data: { title: 'Zamba Elements' }
      },
      {
        path: 'taskhistory',
        component: TaskHistoryComponent,
        data: { title: 'Historial de Tareas' },
        pathMatch: 'full'
      },
      {
        path: 'editor-docx',
        component: DocxEditorComponent,
        data: { title: 'Editor DOCX' },
        pathMatch: 'full'
      },
      {
        path: 'editor-onlyoffice',
        component: OnlyofficeEditorComponent,
        data: { title: 'OnlyOffice Docs' },
        pathMatch: 'full'
      },
      {
        path: 'editor-tinymce-premium',
        component: TinymceElementComponent,
        data: { title: 'TinyMCE Self-hosted' },
        pathMatch: 'full'
      },
      {
        path: 'editor-ckeditor5-premium',
        component: Ckeditor5PremiumEditorComponent,
        data: { title: 'CKEditor 5 Premium' },
        pathMatch: 'full'
      },
      {
        path: 'global-search',
        component: GlobalSearchElementComponent,
        data: { title: 'Buscador Zamba', EntityId: 'HB Documentos', IndexId: 'GlobalSearch' },
        pathMatch: 'full'
      },
      {
        path: 'reports',
        component: ReportComponentComponent,
        data: { title: 'Reportes' },
        children: [
          {
            path: 'create',
            component: ReportEditorComponent,
            data: { title: 'Crear reporte' }
          },
          {
            path: 'edit/:id',
            component: ReportEditorComponent,
            data: { title: 'Editar reporte' }
          },
          {
            path: 'view/:id',
            component: ReportViewerComponent,
            data: { title: 'Reporte' }
          },
          {
            path: 'chartcontainer/:id',
            component: ChartContainerComponent,
            data: { title: 'Vista de graficos' },
            pathMatch: 'full'
          },
        ]
      },
      {
        path: 'gestion',
        component: QuickActionsComponent,
      },
      {
        path: 'doshowtable',
        component: DoShowTableComponent,
      },
      {
        path: 'signature',
        component: SignatureContainerComponent,
      },
      {
        path: 'form',
        component: ViewFormComponent,
      },
      {
        path: 'permisos',
        component: PermissionsUserGroupComponent,
        data: { title: 'Grupos y Usuarios' },
      }
    ]
  },
  { path: 'exception', loadChildren: () => import('./exception/exception.module').then(m => m.ExceptionModule) },
  { path: '**', redirectTo: 'exception/404' },
];

@NgModule({
  providers: [PreloadOptionalModules],
  imports: [
    RouterModule.forRoot(routes, {
      useHash: environment.useHash,
      scrollPositionRestoration: 'top',
      preloadingStrategy: PreloadOptionalModules,
      bindToComponentInputs: true,
      onSameUrlNavigation: 'ignore'
    })
  ],
  exports: [RouterModule]
})
export class RouteRoutingModule { }

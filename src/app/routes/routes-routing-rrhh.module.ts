import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authSimpleCanActivate, authSimpleCanActivateChild } from '@delon/auth';
import { PreloadOptionalModules } from '@delon/theme';
import { environment } from '@env/environment';

import { ReportComponentComponent } from './widgets/report-component/report-component.component';
import { ReportEditorComponent } from './widgets/report-editor/report-editor.component';
import { ReportViewerComponent } from './widgets/report-viewer/report-viewer.component';
import { ChartContainerComponent } from '../components/chart-container/chart-container.component';
import { Ckeditor5PremiumEditorComponent } from '../components/ckeditor5-premium-editor/ckeditor5-premium-editor.component';
import { DocxEditorComponent } from '../components/docx-editor/docx-editor.component';
import { DoShowTableComponent } from '../components/doshowtable/do-show-table/do-show-table.component';
import { OnlyofficeEditorComponent } from '../components/onlyoffice-editor/onlyoffice-editor.component';
import { PermissionsUserGroupComponent } from '../components/permissions-user-group/permissions-user-group';


import { QuickActionsComponent } from '../components/quick-actions/quick-actions.component';
import { SignatureContainerComponent } from '../signature-container/signature-container.component';
import { TaskHistoryComponent } from '../components/task-history/task-history.component';
import { TinymcePremiumEditorComponent } from '../components/tinymce-premium-editor/tinymce-premium-editor.component';
import { TinymceElementComponent } from '../elements/tinymce-editor/tinymce-editor.component';
import { GlobalSearchElementComponent } from '../elements/global-search/global-search.component';
import { WebBookmarkComponent } from '../elements/web-bookmark/web-bookmark.component';
import { MiniwebBookmarkComponent } from '../elements/miniweb-bookmark/miniweb-bookmark.component';
import { LayoutBasicComponent } from '../layout/basic/basic.component';
import { LayoutBlankComponent } from '../layout/blank/blank.component';
import { LayoutSimpleComponent } from '../layout/simple/simple.component';
import { MainPageComponent } from '../main-page/main-page.component';

// layout

const routes: Routes = [
  {
    path: '',
    component: LayoutBasicComponent,
    canActivate: [authSimpleCanActivate],
    canActivateChild: [authSimpleCanActivateChild],
    data: {},
    children: [
      { path: '', redirectTo: '/main', pathMatch: 'full' },
      {
        path: 'main',
        component: MainPageComponent,
        data: { title: 'Inicio' },
        pathMatch: 'full',
      },
      {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule),
        data: { preload: true, title: 'Dashboard' },
      },
      {
        path: 'default',
        loadChildren: () => import('./default/default.component').then(m => m.DefaultComponent),
        data: { preload: true },
      },
      {
        path: 'widgets',
        loadChildren: () => import('./widgets/widgets.module').then(m => m.WidgetsModule),
        data: { title: 'Dashboard' },
      },
      {
        path: 'taskhistory',
        component: TaskHistoryComponent,
        data: { title: 'Task History' },
        pathMatch: 'full',
      },
      {
        path: 'dashboard/gestion',
        component: QuickActionsComponent,
      },
      {
        path: 'dashboard/reports',
        component: ReportComponentComponent,
        children: [
          { path: 'create', component: ReportEditorComponent },
          { path: 'edit/:id', component: ReportEditorComponent },
          { path: 'view/:id', component: ReportViewerComponent },
          { path: 'chartcontainer/:id', component: ChartContainerComponent },
        ],
      },
      { path: 'style', loadChildren: () => import('./style/style.module').then(m => m.StyleModule) },
      { path: 'delon', loadChildren: () => import('./delon/delon.module').then(m => m.DelonModule) },
      { path: 'pro', loadChildren: () => import('./pro/pro.module').then(m => m.ProModule) },
      { path: 'ges', loadChildren: () => import('./ges/ges.module').then(r => r.GesModule) },
      { path: 'zamba', loadChildren: () => import('./zamba/zamba.module').then(r => r.ZambaModule) },
    ],
  },
  {
    path: 'tools',
    component: LayoutSimpleComponent,
    children: [
      {
        path: 'taskhistory',
        component: TaskHistoryComponent,
        data: { title: 'Historial de Tareas' },
        pathMatch: 'full',
      },
      {
        path: 'editor-docx',
        component: DocxEditorComponent,
        data: { title: 'Editor DOCX' },
        pathMatch: 'full',
      },
      {
        path: 'editor-onlyoffice',
        component: OnlyofficeEditorComponent,
        data: { title: 'OnlyOffice Docs' },
        pathMatch: 'full',
      },
      {
        path: 'word-editor',
        component: TinymceElementComponent,
        data: { title: 'Editor de archivos Word' },
        pathMatch: 'full',
      },
      {
        path: 'chart-viewer-premium',
        component: ChartContainerComponent,
        data: { title: 'ChartViewer Self-hosted' },
        pathMatch: 'full',
      },
      {
        path: 'editor-ckeditor5-premium',
        component: Ckeditor5PremiumEditorComponent,
        data: { title: 'CKEditor 5 Premium' },
        pathMatch: 'full',
      },
      {
        path: 'global-search',
        component: GlobalSearchElementComponent,
        data: { title: 'Buscador', EntityId: 'HB Documentos', IndexId: 'GlobalSearch' },
        pathMatch: 'full',
      },
      {
        path: 'web-bookmark',
        component: WebBookmarkComponent,
        data: { title: 'Web Bookmark' },
        pathMatch: 'full',
      },
      {
        path: 'miniweb-bookmark',
        component: MiniwebBookmarkComponent,
        data: { title: 'Mini Web Bookmark' },
        pathMatch: 'full',
      },
      {
        path: 'reports',
        component: ReportComponentComponent,
        data: { title: 'Reportes' },
        children: [
          {
            path: 'create',
            component: ReportEditorComponent,
            data: { title: 'Crear reporte' },
          },
          {
            path: 'edit/:id',
            component: ReportEditorComponent,
            data: { title: 'Editar reporte' },
          },
          {
            path: 'view/:id',
            component: ReportViewerComponent,
            data: { title: 'Reportes' },
          },
          {
            path: 'chartcontainer/:id',
            component: ChartContainerComponent,
            data: { title: 'Vista de graficos' },
            pathMatch: 'full',
          },
        ],
      },
      {
        path: 'gestion',
        component: QuickActionsComponent,
        data: { title: 'Gestión' },
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
        path: 'permisos',
        component: PermissionsUserGroupComponent,
        data: { title: 'Gestión de grupos y usuarios' },
      },
    ],
  },
  // Blak Layout 空白布局
  {
    path: 'data-v',
    component: LayoutBlankComponent,
    children: [{ path: '', loadChildren: () => import('./data-v/data-v.module').then(m => m.DataVModule) }],
  },
  // passport
  { path: '', loadChildren: () => import('./passport/passport.module').then(m => m.PassportModule), data: { preload: true } },
  { path: 'exception', loadChildren: () => import('./exception/exception.module').then(m => m.ExceptionModule) },
  { path: '**', redirectTo: 'exception/404' },
];

@NgModule({
  providers: [PreloadOptionalModules],
  imports: [
    RouterModule.forRoot(routes, {
      useHash: environment.useHash,
      // NOTICE: If you use `reuse-tab` component and turn on keepingScroll you can set to `disabled`
      // Pls refer to https://ng-alain.com/components/reuse-tab
      scrollPositionRestoration: 'top',
      preloadingStrategy: PreloadOptionalModules,
      bindToComponentInputs: true,
      onSameUrlNavigation: 'ignore',
    }),
  ],
  exports: [RouterModule],
})
export class RouteRoutingRRHHModule { }

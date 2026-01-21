import { RouterModule, Routes } from '@angular/router';
import { authSimpleCanActivate, authSimpleCanActivateChild } from '@delon/auth';

import { ChartComponent } from '../components/chart/chart.component';
import { ChartContainerComponent } from '../components/chart-container/chart-container.component';
import { ChecklistComponent } from '../elements/checklist/checklist.component';
import { DoShowTableComponent } from '../components/doshowtable/do-show-table/do-show-table.component';
import { LayoutBasicComponent } from '../layout/basic/basic.component';
import { LayoutBlankComponent } from '../layout/blank/blank.component';
import { LayoutSimpleComponent } from '../layout/simple/simple.component';
import { NgModule } from '@angular/core';
import { PreloadOptionalModules } from '@delon/theme';
import { QuickActionsComponent } from '../components/quick-actions/quick-actions.component';
import { ReportComponentComponent } from "./widgets/report-component/report-component.component";
import { ReportEditorComponent } from './widgets/report-editor/report-editor.component';
import { ReportViewerComponent } from './widgets/report-viewer/report-viewer.component';
import { SignatureContainerComponent } from '../signature-container/signature-container.component';
import { TaskHistoryComponent } from '../components/task-history/task-history.component';
import { ViewFormComponent } from './widgets/view-form/view-form.component';
import { environment } from '@env/environment';
import { PermissionsUserGroupComponent } from '../components/permissions-user-group/permissions-user-group';

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
        path: 'reports',
        component: ReportComponentComponent,
        data: { title: 'Listado de reportes' },
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
            data: { title: 'Vista general del reporte' }
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
  // passport
  //{ path: '', loadChildren: () => import('./passport/passport.module').then(m => m.PassportModule), data: { preload: true } },
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
      onSameUrlNavigation: 'reload'
    })
  ],
  exports: [RouterModule]
})
export class RouteRoutingModule { }

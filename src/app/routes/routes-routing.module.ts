import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authSimpleCanActivate, authSimpleCanActivateChild } from '@delon/auth';
import { PreloadOptionalModules } from '@delon/theme';
import { environment } from '@env/environment';


import { TaskHistoryComponent } from '../components/task-history/task-history.component';
// layout
import { LayoutBasicComponent } from '../layout/basic/basic.component';
import { LayoutBlankComponent } from '../layout/blank/blank.component';
import { LayoutSimpleComponent } from '../layout/simple/simple.component';
import { ReportComponentComponent } from "./widgets/report-component/report-component.component";
import { ReportEditorComponent } from './widgets/report-editor/report-editor.component';
import { ReportViewerComponent } from './widgets/report-viewer/report-viewer.component';
import { ChartComponent } from '../components/chart/chart.component';
import { QuickActionsComponent } from '../components/quick-actions/quick-actions.component';
import { DoShowTableComponent } from '../components/doshowtable/do-show-table/do-show-table.component';
import { ChartContainerComponent } from '../components/chart-container/chart-container.component';

const routes: Routes = [
  {
    path: 'tools',
    component: LayoutSimpleComponent,
    children: [
      {
        path: 'taskhistory',
        component: TaskHistoryComponent,
        data: { title: 'Task History' },
        pathMatch: 'full'
      },
      {
        path: 'reports',
        component: ReportComponentComponent,
        data: { title: 'Reports' },
        children: [
          {
            path: 'create',
            component: ReportEditorComponent,
            data: { title: 'Create' }
          },
          {
            path: 'edit/:id',
            component: ReportEditorComponent,
            data: { title: 'Edit' }
          },
          {
            path: 'view/:id',
            component: ReportViewerComponent,
            data: { title: 'View' }
          },
          {
            path: 'chartcontainer/:id',
            component: ChartContainerComponent,
            data: { title: 'charts Container' },
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

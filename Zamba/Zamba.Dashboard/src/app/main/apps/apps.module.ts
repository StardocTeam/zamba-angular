import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import { AuthGuard } from 'app/shared/guards/auth.guard';

const routes: Routes = [
    {
        path        : 'dashboards/analytics',
        loadChildren: './dashboards/analytics/analytics.module#AnalyticsDashboardModule',
        //canActivate: [AuthGuard],
    },
    {
        path        : 'IMAP',
        loadChildren: './client-imap/client-imap.module#ClientIMAPModule',
        //canActivate: [AuthGuard],
    },
];

@NgModule({
    imports     : [
        RouterModule.forChild(routes),
        FuseSharedModule
    ]
})
export class AppsModule
{
}

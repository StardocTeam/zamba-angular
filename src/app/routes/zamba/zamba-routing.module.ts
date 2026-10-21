import { RouterModule, Routes } from '@angular/router';

import { NgModule } from '@angular/core';
import { RuleComponent } from './rule/rule.component';
import { ViewFormComponent } from '../widgets/view-form/view-form.component';
import { ZambaService } from '../../services/zamba/zamba.service';
import { aclCanActivate } from '@delon/acl';
import { TraceDashboardComponent } from './traces/trace-dashboard.component';
import { EntityRagIndexingComponent } from './entity-rag-indexing/entity-rag-indexing.component';

const routes: Routes = [
  { path: 'rule', component: RuleComponent, title: 'Formulario de WorkFlow' },
  { path: 'form', component: ViewFormComponent, title: 'Formulario' },
  { path: 'traces', component: TraceDashboardComponent, title: 'Trace Explorer' },
  { path: 'entity-rag-indexing', component: EntityRagIndexingComponent, title: 'Entity RAG indexing' },
  {
    path: 'schedule',
    loadChildren: () => import('./schedule/schedule.module').then(m => m.ScheduleModule),
    title: 'Schedules'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ZambaRoutingModule {}

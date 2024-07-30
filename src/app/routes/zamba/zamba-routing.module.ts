import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { aclCanActivate } from '@delon/acl';

import { RuleComponent } from './rule/rule.component';
import { ZambaService } from '../../services/zamba/zamba.service';
import { ViewFormComponent } from '../widgets/view-form/view-form.component';

const routes: Routes = [
  { path: 'rule', component: RuleComponent, title: 'Formulario de WorkFlow' },
  { path: 'form', component: ViewFormComponent, title: 'Formulario' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ZambaRoutingModule {}

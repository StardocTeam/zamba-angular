import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { aclCanActivate } from '@delon/acl';

import { RuleComponent } from './rule/rule.component';
import { ZambaService } from '../../services/zamba/zamba.service';

const routes: Routes = [{ path: 'rule', component: RuleComponent, title: 'Formulario' }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ZambaRoutingModule { }

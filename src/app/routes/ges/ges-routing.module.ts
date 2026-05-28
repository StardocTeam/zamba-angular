import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { aclCanActivate } from '@delon/acl';

import { EmpleadoComponent } from './empleado/empleado.component';

const routes: Routes = [{ path: 'empleado', component: EmpleadoComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class GesRoutingModule {}

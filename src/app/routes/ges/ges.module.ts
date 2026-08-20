import { NgModule } from '@angular/core';
import { DownFileModule } from '@delon/abc/down-file';
import { FullContentModule } from '@delon/abc/full-content';
import { QRModule } from '@delon/abc/qr';
import { G2MiniBarModule } from '@delon/chart/mini-bar';
import { SharedModule } from '@shared';

import { EmpleadoComponent } from './empleado/empleado.component';
import { GesRoutingModule } from './ges-routing.module';

const COMPONENTS = [EmpleadoComponent];

@NgModule({
  imports: [SharedModule, GesRoutingModule, DownFileModule, FullContentModule, QRModule, G2MiniBarModule],
  declarations: COMPONENTS,
})
export class GesModule {}

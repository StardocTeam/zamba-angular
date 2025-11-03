import { NgModule, Type } from '@angular/core';
import { SharedModule } from '@shared';

import { RouteRoutingModule } from './routes-routing.module';
import { environment } from '@env/environment';
import { RouteRoutingRRHHModule } from './routes-routing-rrhh.module';

const COMPONENTS: Array<Type<null>> = [];
const ROUTES_MODULE = environment['cliente'] === 'rrhh'
  ? RouteRoutingRRHHModule
  : RouteRoutingModule;
@NgModule({
  imports: [SharedModule, ROUTES_MODULE],
  declarations: [...COMPONENTS]
})
export class RoutesModule { }

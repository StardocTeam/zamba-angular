import { NgModule, Type } from '@angular/core';
import { environment } from '@env/environment';
import { SharedModule } from '@shared';

import { RouteRoutingRRHHModule } from './routes-routing-rrhh.module';
import { RouteRoutingModule } from './routes-routing.module';

const COMPONENTS: Array<Type<null>> = [];
const ROUTES_MODULE = RouteRoutingRRHHModule;
@NgModule({
  imports: [SharedModule, ROUTES_MODULE],
  declarations: [...COMPONENTS],
})
export class RoutesModule {}

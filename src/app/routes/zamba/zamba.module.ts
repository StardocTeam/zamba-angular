import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule } from 'ng-zorro-antd/message';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { DownFileModule } from '@delon/abc/down-file';
import { FullContentModule } from '@delon/abc/full-content';
import { QRModule } from '@delon/abc/qr';
import { G2MiniBarModule } from '@delon/chart/mini-bar';
import { SharedModule } from '@shared';

import { RuleComponent } from './rule/rule.component';
import { ZambaRoutingModule } from './zamba-routing.module';
import { ViewFormComponent } from '../widgets/view-form/view-form.component';
import { TraceDashboardComponent } from './traces/trace-dashboard.component';
import { EntityRagIndexingComponent } from './entity-rag-indexing/entity-rag-indexing.component';
//import { SignatureModule } from 'src/app/signature/signature.module';

const COMPONENTS = [RuleComponent, TraceDashboardComponent, EntityRagIndexingComponent];

@NgModule({
  imports: [SharedModule, ZambaRoutingModule, DownFileModule, FullContentModule, QRModule, G2MiniBarModule, ViewFormComponent,
    FormsModule, NzButtonModule, NzIconModule, NzInputModule, NzMessageModule, NzSelectModule, NzSpinModule, NzTableModule, NzToolTipModule],
  declarations: COMPONENTS,
})
export class ZambaModule {}

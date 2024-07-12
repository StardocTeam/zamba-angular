import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { QRGeneratorComponent } from './qrgenerator-container.component';

const routes: Routes = [
  { path: '', component: QRGeneratorComponent },
  { path: ':id', component: QRGeneratorComponent }
];

@NgModule({
  declarations: [QRGeneratorComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NzModalModule,
    NzButtonModule,
    NzIconModule,
    NzSpinModule
  ],
  exports: [QRGeneratorComponent]
})
export class QRGeneratorModule { }

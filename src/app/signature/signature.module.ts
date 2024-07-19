import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignatureComponent } from './signature.component';
import { RouterModule, Routes } from '@angular/router';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { SignatureContainerComponent } from '../signature-container/signature-container.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { FormsModule } from '@angular/forms';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { SignatureFABComponent } from '../signature-container-v2/signature-container-v2.component';
import { SignatureV2Component } from '../signature-v2/signature-v2.component';

const routes: Routes = [
  { path: '', component: SignatureContainerComponent }
];

@NgModule({
  declarations: [SignatureComponent, SignatureV2Component, SignatureContainerComponent, SignatureFABComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NzModalModule,
    NzButtonModule,
    NzIconModule,
    NzSpinModule,
    NzRadioModule,
    FormsModule,
    NzTabsModule
  ],
  exports: [SignatureComponent, SignatureV2Component, SignatureContainerComponent, SignatureFABComponent]
})
export class SignatureModule { }

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignatureComponent } from './signature.component';
import { RouterModule, Routes } from '@angular/router';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { SignatureContainerComponent } from '../signature-container/signature-container.component';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';

const routes: Routes = [
  { path: '', component: SignatureContainerComponent }
];

@NgModule({
  declarations: [SignatureComponent, SignatureContainerComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NzModalModule,
    NzButtonModule,
    NzIconModule
  ],
  exports: [SignatureComponent, SignatureContainerComponent]
})
export class SignatureModule { }

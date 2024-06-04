import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SignatureComponent } from './signature.component';
import { RouterModule, Routes } from '@angular/router';
import { NzModalModule } from 'ng-zorro-antd/modal';

const routes: Routes = [
  { path: '', component: SignatureComponent },
];

@NgModule({
  declarations: [SignatureComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    NzModalModule
  ],
  exports: [SignatureComponent]
})
export class SignatureModule { }

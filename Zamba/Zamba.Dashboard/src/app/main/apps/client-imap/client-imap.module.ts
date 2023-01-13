import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';
import {​​ MaterialModule }​​ from 'app/main/angular-material-elements/material.module'
import {MatDialog} from '@angular/material/dialog';

import { FuseSharedModule } from '@fuse/shared.module';
import { ClientIMAPComponent } from './client-imap/client-imap.component';
import { EditComponent } from './edit/edit.component';

import { AuthGuard} from '../../../shared/guards/auth.guard';

const routes: Routes = [
  {
    
    path     : 'client',
    component: ClientIMAPComponent
  },  
  {
    path     : 'edit',
    component: EditComponent
  },
  {
    path     : 'edit:process',
    component: EditComponent
  },
];

@NgModule({
  declarations: [ClientIMAPComponent, EditComponent,],
  imports: [
    RouterModule.forChild(routes),

        MatButtonModule,
        MatFormFieldModule,
        MatIconModule,
        MatInputModule,
        MatSelectModule,
        MatStepperModule,
        MatTableModule,
        FuseSharedModule,
        MaterialModule
  ]
})
export class ClientIMAPModule { }

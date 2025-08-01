import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { G2BarModule } from '@delon/chart/bar';

import { RouterModule, Routes } from '@angular/router';
import { ChartComponent } from './chart.component';

const routes: Routes = [
  { path: '', component: ChartComponent }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    G2BarModule,
    RouterModule.forChild(routes),
  ]
})
export class ChartModule { }

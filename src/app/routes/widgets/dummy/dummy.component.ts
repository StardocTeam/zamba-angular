import { NgIf } from '@angular/common';
import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { GridsterItem } from 'angular-gridster2';

import { WidgetsModule } from '../widgets.module';
/* Agregar los otros widgets
import { WidgetAComponent } from './widgetA.component';
import { WidgetBComponent } from './widgetB.component';
import { WidgetCComponent } from './widgetC.component';
*/
@Component({
  selector: 'dummy',
  templateUrl: './dummy.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['dummy.component.css'],
  encapsulation: ViewEncapsulation.None,
  standalone: true,
  imports: [],
})
export class DummyComponent {}

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
  selector: 'widget-selector',
  templateUrl: './widget-selector.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['widget-selector.component.css'],
  encapsulation: ViewEncapsulation.None,
  standalone: true,
  imports: [NgIf, WidgetsModule],
})
export class WidgetSelectorComponent {
  @Input()
  widget: GridsterItem = {
    type: '',
    title: '',
    cols: 0,
    rows: 0,
    x: 0,
    y: 0,
    resizeEvent: new EventEmitter<GridsterItem>(),
  };
  @Input()
  resizeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();
  @Input()
  changeEvent: EventEmitter<GridsterItem> = new EventEmitter<GridsterItem>();
}

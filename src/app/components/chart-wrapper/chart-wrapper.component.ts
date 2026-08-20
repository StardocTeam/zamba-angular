import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-chart-wrapper',
  templateUrl: './chart-wrapper.component.html',
  styleUrls: ['./chart-wrapper.component.less'],
  standalone: true,
})
export class ChartWrapperComponent {
  @Input() row = 1;
  @Input() col = 1;
  @Input() rowSpan = 1;
  @Input() colSpan = 1;
}

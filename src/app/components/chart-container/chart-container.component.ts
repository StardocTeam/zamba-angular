import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-chart-container',
  templateUrl: './chart-container.component.html',
  styleUrls: ['./chart-container.component.less'],
  standalone: true
})
export class ChartContainerComponent {
  @Input() rows = 4;
  @Input() cols = 4;
}

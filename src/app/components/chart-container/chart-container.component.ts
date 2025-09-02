import { CommonModule, NgForOf } from '@angular/common';
import { Component, Input, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzCardComponent, NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzMarks, NzSliderModule } from 'ng-zorro-antd/slider';
import { ChartComponent } from '../chart/chart.component';

// @NgModule({
//   declarations: [ChartComponent],
//   imports: [FormsModule, NzGridModule, NzSliderModule, NzCardModule, NgForOf],
//   exports: [ChartComponent]
// })

@Component({
  selector: 'app-chart-container',
  templateUrl: './chart-container.component.html',
  styleUrls: ['./chart-container.component.less'],
  standalone: true,
  imports: [FormsModule, NzGridModule, NzSliderModule, NzCardModule, NgForOf, ChartComponent, CommonModule]
})
export class ChartContainerComponent {
  //a
  rows: number = 5;
  cols: number = 5;

  //b
  hGutter = 16;
  vGutter = 16;
  count = 2;
  array = new Array(this.count);
  marksHGutter: NzMarks = {
    8: '8',
    16: '16',
    24: '24',
    32: '32',
    40: '40',
    48: '48'
  };
  marksVGutter: NzMarks = {
    8: '8',
    16: '16',
    24: '24',
    32: '32',
    40: '40',
    48: '48'
  };
  marksCount: NzMarks = {
    2: '2',
    3: '3',
    4: '4',
    6: '6',
    8: '8',
    12: '12'
  };

  //#region C
  //5 x 5
  // Definís los "bloques"
  blocks = [
    { row: 1, col: 1, rowSpan: 2, colSpan: 2, type: 'grafico' },
    { row: 3, col: 1, rowSpan: 1, colSpan: 2, type: 'grafico' }
  ];

  getBlockAt(row: number, col: number) {
    return this.blocks.find(b => b.row === row && b.col === col);
  }

  isCellCovered(row: number, col: number) {
    return this.blocks.some(b =>
      row >= b.row &&
      row < b.row + b.rowSpan &&
      col >= b.col &&
      col < b.col + b.colSpan &&
      !(row === b.row && col === b.col) // no es la celda inicial
    );
  }
  //#endregion



  reGenerateArray(count: number): void {
    this.array = new Array(count);
    console.log(this.count);
  }







  // array de cajas simuladas
  items = Array.from({ length: 12 }, (_, i) => i + 1);

  getCoordinates(index: number) {
    const x = (index % this.count) + 1;          // columna
    const y = Math.floor(index / this.count) + 1; // fila
    return { x, y };
  }
}

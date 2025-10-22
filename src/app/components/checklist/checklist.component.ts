import { Component, Input } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-checklist-component',
  standalone: true,
  imports: [CommonModule, FormsModule, NzIconModule],
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.scss']
})
export class ChecklistComponent {
  /**
   * Width can be a number (px) or a CSS string (%, px, etc). Defaults to '100%'.
   */
  @Input() width: string | number = '100%';

  /**
   * Height for the list area. Can be number (px) or CSS string. Defaults to 260px.
   */
  @Input() height: string | number = '260px';

  get cardStyle() {
    return { width: this.toCss(this.width) };
  }

  get listStyle() {
    return { height: this.toCss(this.height) };
  }

  private toCss(value: string | number) {
    if (value == null) return '';
    return typeof value === 'number' ? `${value}px` : value;
  }
  items = [
    { id: 1, title: 'Prepare report skeleton', done: true },
    { id: 2, title: 'Fetch data from API', done: false },
    { id: 3, title: 'Build charts', done: false },
    { id: 4, title: 'Write unit tests', done: true }
  ];

  newTitle = '';

  add() {
    const title = (this.newTitle || '').trim();
    if (!title) return;
    const id = this.items.length ? Math.max(...this.items.map(i => i.id)) + 1 : 1;
    this.items = [{ id, title, done: false }, ...this.items];
    this.newTitle = '';
  }

  toggle(item: { id: number; done: boolean }) {
    item.done = !item.done;
  }

  remove(item: { id: number }) {
    this.items = this.items.filter(i => i.id !== item.id);
  }

  trackById(_index: number, item: { id: number }) {
    return item.id;
  }
}

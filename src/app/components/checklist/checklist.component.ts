import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-checklist-component',
    standalone: true,
    imports: [CommonModule, FormsModule],
  templateUrl: './checklist.component.html',
    styles: [
        `
    .checklist-card {
      border: 1px solid #e0e0e0;
      border-radius: 6px;
      padding: 12px;
      max-width: 720px;
      background: #fff;
    }

    .add-row { display:flex; gap:8px; margin-bottom:8px; }
    .add-row input { flex:1; padding:6px; }

    .checklist {
      list-style: none;
      padding: 0;
      margin: 0;
      height: 260px; /* fixed height as requested */
      overflow-y: auto;
      border-top: 1px solid #f0f0f0;
      padding-top: 8px;
    }

    .item { display:flex; justify-content:space-between; align-items:center; padding: 8px; border-bottom: 1px dashed #f2f2f2; }
    .left { display:flex; align-items:center; gap:10px; }

    .icon { width:26px; height:26px; display:inline-flex; align-items:center; justify-content:center; font-weight:700; border-radius:50%; background:#fff; border:1px solid transparent; }
    .icon.success { color:#0b8a0b; background:#e9f6ea; border-color:#bfe6bd; }
    .icon.pending { color:#b36f00; background:#fff6e6; border-color:#f0d9b0; }

    .title { max-width:560px; word-break:break-word; }
    .title.done { text-decoration:line-through; color:#9e9e9e; }

    .actions button { margin-left:6px; }
    .actions .danger { color:#a00; background:transparent; border:0; cursor:pointer; }
    `
    ]
})
export class ChecklistComponent {
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

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { By } from '@angular/platform-browser';
import { ChecklistComponent } from './checklist.component';

describe('ChecklistComponent', () => {
    let fixture: ComponentFixture<ChecklistComponent>;
    let component: ChecklistComponent;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [ChecklistComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(ChecklistComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should add a new item', () => {
        const initial = component.items.length;
        component.newTitle = 'New task from test';
        component.add();
        expect(component.items.length).toBe(initial + 1);
        expect(component.items[0].title).toBe('New task from test');
        expect(component.newTitle).toBe('');
    });

    it('should toggle an item done state', () => {
        const item = component.items[0];
        const prev = item.done;
        component.toggle(item);
        expect(item.done).toBe(!prev);
    });

    it('should remove an item', () => {
        const item = component.items[0];
        const id = item.id;
        component.remove(item);
        expect(component.items.find(i => i.id === id)).toBeUndefined();
    });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiniwebBookmarkComponent } from './miniweb-bookmark.component';

describe('MiniwebBookmarkComponent', () => {
  let component: MiniwebBookmarkComponent;
  let fixture: ComponentFixture<MiniwebBookmarkComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MiniwebBookmarkComponent],
    });
    fixture = TestBed.createComponent(MiniwebBookmarkComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

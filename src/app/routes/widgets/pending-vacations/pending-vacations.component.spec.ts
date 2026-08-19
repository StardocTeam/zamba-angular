import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PendingVacationsComponent } from './pending-vacations.component';

describe('PendingVacationsComponent', () => {
  let component: PendingVacationsComponent;
  let fixture: ComponentFixture<PendingVacationsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PendingVacationsComponent],
    });
    fixture = TestBed.createComponent(PendingVacationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

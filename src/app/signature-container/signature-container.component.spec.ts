import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignatureContainerComponent } from './signature-container.component';

describe('SignatureContainerComponent', () => {
  let component: SignatureContainerComponent;
  let fixture: ComponentFixture<SignatureContainerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SignatureContainerComponent]
    });
    fixture = TestBed.createComponent(SignatureContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

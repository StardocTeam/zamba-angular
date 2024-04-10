import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InitialPasswordWizardComponent } from './initial-password-wizard.component';

describe('InitialPasswordWizardComponent', () => {
  let component: InitialPasswordWizardComponent;
  let fixture: ComponentFixture<InitialPasswordWizardComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [InitialPasswordWizardComponent]
    });
    fixture = TestBed.createComponent(InitialPasswordWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

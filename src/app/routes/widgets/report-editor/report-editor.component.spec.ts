import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReportEditorComponent } from './report-editor.component';

describe('ReportEditorComponent', () => {
  let component: ReportEditorComponent;
  let fixture: ComponentFixture<ReportEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ReportEditorComponent]
    });
    fixture = TestBed.createComponent(ReportEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

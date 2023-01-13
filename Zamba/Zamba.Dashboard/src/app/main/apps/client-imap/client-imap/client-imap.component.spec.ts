import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClientIMAPComponent } from './client-imap.component';

describe('ClientIMAPComponent', () => {
  let component: ClientIMAPComponent;
  let fixture: ComponentFixture<ClientIMAPComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClientIMAPComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClientIMAPComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

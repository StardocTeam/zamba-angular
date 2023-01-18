import { TestBed } from '@angular/core/testing';

import { Style1Service } from './style-1.service';

describe('Style1Service', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Style1Service = TestBed.get(Style1Service);
    expect(service).toBeTruthy();
  });
});

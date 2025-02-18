import { TestBed } from '@angular/core/testing';

import { TaskHistoryServiceService } from './task-history-service.service';

describe('TaskHistoryServiceService', () => {
  let service: TaskHistoryServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TaskHistoryServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

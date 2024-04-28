import { TestBed } from '@angular/core/testing';

import { MessageListenerService } from './message-listener.service';

describe('MessageListenerService', () => {
  let service: MessageListenerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MessageListenerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

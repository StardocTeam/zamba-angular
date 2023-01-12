import { TestBed } from '@angular/core/testing';

import { ClientImapService } from './client-imap.service';

describe('ClientImapService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ClientImapService = TestBed.get(ClientImapService);
    expect(service).toBeTruthy();
  });
});

import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class PendingVacationsService {
  constructor(private http: _HttpClient) {}

  _GetVacation(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getVacation`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  _GetExternalsearchInfo(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getExternalsearchInfo`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
}

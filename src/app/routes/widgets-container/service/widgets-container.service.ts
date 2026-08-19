import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class WidgetsContainerService {
  constructor(private http: _HttpClient) {}

  _getWidgetsContainer(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getWidgetsContainer`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _setWidgetsContainer(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/setWidgetsContainer`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

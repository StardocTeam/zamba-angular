import { HttpContext, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

import { Generic } from '../entitie/generic';



@Injectable({
  providedIn: 'root'
})
export class PendingVacationsService {
  constructor(private http: _HttpClient) { }

  _GetVacation(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getVacation`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  _PostExternalsearchInfo(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/postExternalsearchInfo`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  _GetExternalsearchInfo(request: Generic) {
    let headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    let options = { headers: headers };

    return this.http.get(`${environment['apiRestBasePath']}/getExternalsearchInfo`, request, options);
  }
}

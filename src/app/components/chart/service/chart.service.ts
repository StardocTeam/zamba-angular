import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ChartService {

  constructor(private http: _HttpClient) { }

  _GetChart(genericRequest: any) {
    return this.http.post(`${environment['charts']}/GetChart`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  _AddChart(genericRequest: any) {
    return this.http.post(`${environment['charts']}/AddChart`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
}

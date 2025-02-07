import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  InsertReport(genericRequest: {}) {
    return this.http.post(`${environment['apiRestBasePath']}/insertReport`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
  constructor(private http: _HttpClient) { }

  getCategories(genericRequest: {}) {
    return this.http.post(`${environment['apiRestBasePath']}/getReportCategories`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
}

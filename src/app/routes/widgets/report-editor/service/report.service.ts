import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  UpdateReport(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/updateReport`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  InsertReport(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/insertReport`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  constructor(private http: _HttpClient) { }

  getCategories(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/getReportCategories`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

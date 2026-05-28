import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  GetLastReportViewed(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetLastReportViewed`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  constructor(private http: _HttpClient) {}

  _GetPermissions(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/getPermissions`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _GetReports(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/getReports`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  deleteReport(reportId: any) {
    return this.http.delete(`${environment['restApi']}/reports/deleteReport/${reportId}`, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

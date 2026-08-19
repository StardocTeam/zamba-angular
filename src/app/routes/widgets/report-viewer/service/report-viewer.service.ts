import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class ReportViewerService {
  TestReportQuery(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/TestReportQuery`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  SaveLastReportViewed(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/SaveLastReportViewed`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  GetZVarsInserted(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/GetZVarsInserted`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  GetResultsByReportId(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetResultsByReportId`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  GetRuleIdToReport(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/GetRuleIdsToReport`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  GetReportDescriptionByQuery(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetReportDescriptionByQuery`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  constructor(private http: _HttpClient) {}

  GetReportByQuery(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/GetReportByQuery`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  GetReportById(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/GetReportById`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

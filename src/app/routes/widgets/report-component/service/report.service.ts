import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ReportService {
  private reportFocusSubject = new BehaviorSubject<number | null>(null);
  public reportFocus$ = this.reportFocusSubject.asObservable();

  notifyReportFocus(reportId: number | null | undefined): void {
    const id = Number(reportId);
    if (!Number.isFinite(id) || id <= 0) {
      return;
    }

    this.reportFocusSubject.next(id);
  }

  GetLastReportViewed(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetLastReportViewed`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  constructor(private http: _HttpClient) { }

  _GetPermissions(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/getPermissions`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  //todo: encapsular todos los metodos de services de reportes...
  _GetDeletePermission(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetDeleteRight`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _GetCreatePermission(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetCreateRight`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _GetUpdatePermission(genericRequest: {}) {
    return this.http.post(`${environment['restApi']}/reports/GetEditorRight`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _GetReports(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/getReports`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  deleteReport(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/reports/deleteReport`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

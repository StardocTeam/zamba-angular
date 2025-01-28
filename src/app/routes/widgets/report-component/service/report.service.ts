import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  constructor(private http: _HttpClient) { }

  _GetReports(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getReports`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  createReport(report: any) {
    return this.http.post(`${environment['apiRestBasePath']}/createReport`, report, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  updateReport(report: any) {
    return this.http.put(`${environment['apiRestBasePath']}/updateReport`, report, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  deleteReport(reportId: any) {
    return this.http.delete(`${environment['apiRestBasePath']}/deleteReport/${reportId}`, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }


}







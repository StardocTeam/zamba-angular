import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class PendingTasksService {
  constructor(private http: _HttpClient) {}

  getMyTasks(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getMyTasks`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  getMyTasksCount(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getMyTasksCount`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
}

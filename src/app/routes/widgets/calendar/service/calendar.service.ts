import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class CalendarService {
  constructor(private http: _HttpClient) {}

  getEvents(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getEvents`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  deleteEvent(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/deleteEvent`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  insertNewEvent(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/insertNewEvent`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  updateEvent(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/updateEvent`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

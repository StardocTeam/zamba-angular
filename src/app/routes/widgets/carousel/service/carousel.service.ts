import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class CarouselService {
  constructor(private http: _HttpClient) {}

  _getCarouselContent(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getCarouselContent`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  _getCarouselConfig(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/getCarouselConfig`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

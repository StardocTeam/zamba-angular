import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root',
})
export class SignatureService {
  constructor(private http: _HttpClient) { }

  SignPDF(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/Dashboard/SignPDF`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  GetPDFBase64PayStub(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/Dashboard/GetPDFBase64PayStub`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  SignTask(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/Dashboard/SignTask`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  ValidateAlreadySigned(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/Dashboard/ValidateAlreadySigned`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  UserHasSignature(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/Dashboard/UserHasSignature`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
  getDocument(genericRequest: any) {
    return this.http.post(`${environment['restApi']}/ExternalSearch/getDocument`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }
}

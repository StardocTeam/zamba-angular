import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class QRService {
  constructor(private http: _HttpClient) {}

  GenerateQRCodePDF(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/GenerateQRCodePDF`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  GetPDFBase64ByFileName(genericRequest: any) {
    return this.http.post(`${environment['apiRestBasePath']}/GetPDFBase64ByFileName`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }
}

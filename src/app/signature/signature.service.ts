import { HttpContext } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ALLOW_ANONYMOUS } from '@delon/auth';
import { _HttpClient } from '@delon/theme';
import { environment } from '@env/environment';

@Injectable({
    providedIn: 'root'
})
export class SignatureService {
    constructor(private http: _HttpClient) { }

    SignPDF(genericRequest: any) {
        return this.http.post(`${environment['apiRestBasePath']}/SignPDF`, genericRequest, null, {
            context: new HttpContext().set(ALLOW_ANONYMOUS, true)
        });
    }

    GetPDFBase64PayStub(genericRequest: any) {
        return this.http.post(`${environment['apiRestBasePath']}/GetPDFBase64PayStub`, genericRequest, null, {
            context: new HttpContext().set(ALLOW_ANONYMOUS, true)
        });
    }
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private serviceBase: string = '';
  constructor(private http: HttpClient) {
    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    restAPIUrl = restAPIUrl.replace('dashboard', '');
    this.serviceBase = restAPIUrl;
  }
  executeTaskRule(ruleId: number | string, resultIds: any, formVars?: any): Observable<any> {
    let resultIdsForRequest: string = '';

    var genericRequest: any = {
      "UserId": 0,
      "token": "",
      "Params": {
        "ruleId": ruleId.toString(),
        "resultIds": resultIdsForRequest,
        "userid": "0"
      }
    };
    if (formVars !== undefined) {
      genericRequest.Params["FormVariables"] = formVars;
    }
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(
      `${this.serviceBase}tasks/ExecuteTaskRule`,
      genericRequest,
      { headers }
    );


  }

  getDynamicButtons(): Observable<any> {

    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post(
      `${this.serviceBase}DynamicButtons/GetButtons`,
      {},
      { headers }
    );

  }


  checkAccion(obj: any): string {
    if (obj && obj.Vars && obj.Vars.hasOwnProperty('accion')) {
      return obj.Vars.accion;
    } else {
      return "";
    }
  }

}

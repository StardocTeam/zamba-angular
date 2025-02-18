import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';

declare var ZambaWebRestApiURL: string;

@Injectable({
  providedIn: 'root'
})
export class TaskHistoryService {

  private serviceBase: string;
  private apiUrl: string;
  private apiUrl2: string;

  constructor(private http: HttpClient) {
    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    restAPIUrl = restAPIUrl.replace('dashboard', 'TasksHistory');
    this.serviceBase = restAPIUrl;
    this.apiUrl = `${this.serviceBase}/GetTaskHistory`;
    this.apiUrl2 = `${this.serviceBase}/GetIndexesHistory`;

    console.log('Service Base URL:', this.serviceBase); // Sacar por consola el valor de ZambaWebRestApiURL
  }

  getTaskHistory(docId: any): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docid: docId }
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<any>(this.apiUrl, genericRequest, { headers });
  }

  getIndexesHistory(docId: any): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docId: docId }
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.post<any>(this.apiUrl2, genericRequest, { headers });
  }
}

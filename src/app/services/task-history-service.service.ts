import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

declare var ZambaWebRestApiURL: string;

@Injectable({
  providedIn: 'root',
})
export class TaskHistoryService {
  private serviceBase: string;
  private apiUrl: string;
  private apiUrl2: string;
  private apiUrl3: string;
  private apiUrl4: string;
  private apiUrl5: string;
  private apiUrl6: string;

  constructor(private http: HttpClient) {
    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    restAPIUrl = restAPIUrl.replace('dashboard', 'TasksHistory');
    this.serviceBase = restAPIUrl;
    this.apiUrl = `${this.serviceBase}/GetTaskHistory`;
    this.apiUrl2 = `${this.serviceBase}/GetIndexesHistory`;
    this.apiUrl3 = `${this.serviceBase}/GetTaskName`;
    this.apiUrl4 = `${this.serviceBase}/GetTaskHistoryPaged`;
    this.apiUrl5 = `${this.serviceBase}/GetIndexesHistoryPaged`;
    this.apiUrl6 = `${this.serviceBase}/GetGeneralHistoryPaged`;

    console.log('Service Base URL:', this.serviceBase); // Sacar por consola el valor de ZambaWebRestApiURL
  }

  getTaskName(docId: any, taskId: any): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docid: docId, taskid: taskId },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl3, genericRequest, { headers });
  }
  getTaskHistory(docId: any, taskId: any): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docid: docId, taskid: taskId },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl, genericRequest, { headers });
  }

  getTaskHistoryPaged(docId: any, taskId: any, pageIndex: number, pageSize: number): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docid: docId, taskid: taskId, pageIndex, pageSize },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl4, genericRequest, { headers });
  }

  getIndexesHistory(docId: any): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docId: docId },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl2, genericRequest, { headers });
  }

  getIndexesHistoryPaged(docId: any, pageIndex: number, pageSize: number): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docId: docId, pageIndex, pageSize },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl5, genericRequest, { headers });
  }

  getGeneralHistoryPaged(docId: any, pageIndex: number, pageSize: number): Observable<any> {
    const genericRequest = {
      UserId: 0,
      Params: { docId: docId, pageIndex, pageSize },
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.post<any>(this.apiUrl6, genericRequest, { headers });
  }
}

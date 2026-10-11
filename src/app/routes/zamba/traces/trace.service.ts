import { CreateTraceSource, TracePage, TraceScanResult, TraceSource } from './trace.model';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';

export interface TraceSearch {
  applicationName?: string;
  environment?: string;
  userId?: string;
  search?: string;
  from?: string;
  to?: string;
  page: number;
  pageSize: number;
}

export interface TraceContentStatus {
  length: number;
  lastModifiedUtc: string;
}

@Injectable({ providedIn: 'root' })
export class TraceService {
  private readonly baseUrl = this.getApiBaseUrl();

  constructor(private readonly http: HttpClient) {}

  getSources(): Observable<TraceSource[]> {
    return this.http.get<TraceSource[]>(`${this.baseUrl}/sources`);
  }

  createSource(source: CreateTraceSource): Observable<TraceSource> {
    return this.http.post<TraceSource>(`${this.baseUrl}/sources`, source);
  }

  scanSource(sourceId: number): Observable<TraceScanResult> {
    return this.http.post<TraceScanResult>(`${this.baseUrl}/sources/${sourceId}/scan`, {});
  }

  searchTraces(search: TraceSearch): Observable<TracePage> {
    let params = new HttpParams()
      .set('page', search.page.toString())
      .set('pageSize', search.pageSize.toString());

    for (const key of ['applicationName', 'environment', 'userId', 'search', 'from', 'to'] as const) {
      const value = search[key];
      if (value) params = params.set(key, value);
    }

    return this.http.get<TracePage>(this.baseUrl, { params });
  }

  getTraceContent(id: number, asText = false): Observable<unknown> {
    const url = `${this.baseUrl}/${id}/content`;
    return asText ? this.http.get(url, { responseType: 'text' }) : this.http.get<unknown>(url);
  }

  getTraceContentStatus(id: number): Observable<TraceContentStatus> {
    return this.http.get<TraceContentStatus>(`${this.baseUrl}/${id}/status`);
  }

  private getApiBaseUrl(): string {
    const appConfig = (window as any).appConfig;
    const configuredBaseUrl = appConfig?.scheduleApiBaseUrl || appConfig?.copilotApiBaseUrl || environment['restApi'];
    const apiBaseUrl = configuredBaseUrl.replace(/\/+$/, '');
    return `${apiBaseUrl.endsWith('/api') ? apiBaseUrl : `${apiBaseUrl}/api`}/traces`;
  }
}

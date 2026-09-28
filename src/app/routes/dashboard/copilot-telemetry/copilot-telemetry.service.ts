import {
  CopilotNegativeRunsPage,
  CopilotRunDetail,
  CopilotTelemetryFilters,
  CopilotTelemetrySummary
} from './copilot-telemetry.models';
import { HttpClient, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CopilotTelemetryService {
  private readonly baseUrl = ((window as any).appConfig?.copilotApiBaseUrl || 'http://localhost:5000').replace(/\/$/, '');

  constructor(private readonly http: HttpClient) { }

  getSummary(filters: CopilotTelemetryFilters): Observable<CopilotTelemetrySummary> {
    return this.http.get<CopilotTelemetrySummary>(`${this.baseUrl}/api/copilot/telemetry/summary`, {
      params: this.makeParams(filters, false)
    });
  }

  getNegativeRuns(filters: CopilotTelemetryFilters): Observable<CopilotNegativeRunsPage> {
    return this.http.get<CopilotNegativeRunsPage>(`${this.baseUrl}/api/copilot/telemetry/negative-runs`, {
      params: this.makeParams(filters, true)
    });
  }

  getRunDetail(runId: string): Observable<CopilotRunDetail> {
    return this.http.get<CopilotRunDetail>(`${this.baseUrl}/api/copilot/telemetry/runs/${encodeURIComponent(runId)}`);
  }

  private makeParams(filters: CopilotTelemetryFilters, includeListParams: boolean): HttpParams {
    let params = new HttpParams();
    if (filters.fromUtc) params = params.set('fromUtc', filters.fromUtc);
    if (filters.toUtc) params = params.set('toUtc', filters.toUtc);
    if (filters.documentTypeCode !== null) params = params.set('documentTypeCode', filters.documentTypeCode);
    if (includeListParams) {
      params = params.set('page', filters.page).set('pageSize', filters.pageSize);
      if (filters.search.trim()) params = params.set('search', filters.search.trim());
    }
    return params;
  }
}

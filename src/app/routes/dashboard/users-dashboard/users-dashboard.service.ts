import { HttpClient, HttpParams } from '@angular/common/http';
import {
  UsersDashboardFiltersData,
  UsersDashboardOverview,
  UsersDashboardQuery,
  UsersDashboardTasksPage
} from './users-dashboard.models';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class UsersDashboardService {
  private readonly baseUrl = this.getBaseUrl();

  constructor(private readonly http: HttpClient) { }

  getFilters(): Observable<UsersDashboardFiltersData> {
    return this.http.get<UsersDashboardFiltersData>(`${this.baseUrl}/workflow-dashboard/filters`);
  }

  getOverview(query: UsersDashboardQuery): Observable<UsersDashboardOverview> {
    let params = new HttpParams().set('historyLimit', 100);
    for (const key of ['userId', 'workflowId', 'stepId', 'documentTypeId', 'from', 'to'] as const) {
      if (query[key]) params = params.set(key, query[key]);
    }
    return this.http.get<UsersDashboardOverview>(`${this.baseUrl}/workflow-dashboard/users/overview`, { params });
  }

  getUserTasks(query: UsersDashboardQuery, page: number): Observable<UsersDashboardTasksPage> {
    let params = new HttpParams().set('page', page).set('pageSize', 25).set('dateField', 'checkin');
    for (const key of ['workflowId', 'stepId', 'documentTypeId', 'from', 'to'] as const) {
      if (query[key]) params = params.set(key, query[key]);
    }
    params = params.append('assigneeIds', query.userId);
    return this.http.get<UsersDashboardTasksPage>(`${this.baseUrl}/workflow-dashboard/tasks`, { params });
  }

  reassignTask(taskId: string, userId: string): Observable<unknown> {
    return this.http.post(`${this.baseUrl}/workflow-dashboard/tasks/${encodeURIComponent(taskId)}/reassign`, {
      userId
    });
  }

  private getBaseUrl(): string {
    const config = (window as any).appConfig;
    const configuredUrl = config?.copilotApiBaseUrl || config?.scheduleApiBaseUrl || '';
    let normalized = String(configuredUrl);
    while (normalized.endsWith('/')) normalized = normalized.slice(0, -1);
    return normalized.endsWith('/api') ? normalized : `${normalized}/api`;
  }
}

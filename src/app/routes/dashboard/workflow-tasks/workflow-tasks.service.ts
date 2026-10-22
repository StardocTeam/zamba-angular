import { HttpClient, HttpParams } from '@angular/common/http';
import {
    WorkflowDashboardAttribute,
    WorkflowDashboardFiltersData,
    WorkflowDashboardQuery,
    WorkflowDashboardTasksPage
} from './workflow-tasks.models';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class WorkflowTasksService {
    private readonly baseUrl = this.getBaseUrl();

    constructor(private readonly http: HttpClient) { }

    getFilters(): Observable<WorkflowDashboardFiltersData> {
        return this.http.get<WorkflowDashboardFiltersData>(`${this.baseUrl}/workflow-dashboard/filters`);
    }

    getTasks(query: WorkflowDashboardQuery): Observable<WorkflowDashboardTasksPage> {
        let params = new HttpParams()
            .set('page', query.page)
            .set('pageSize', query.pageSize)
            .set('dateField', query.dateField);
        for (const key of ['workflowId', 'stepId', 'documentTypeId', 'from', 'to'] as const) {
            if (query[key]) params = params.set(key, query[key]);
        }
        for (const assigneeId of query.assigneeIds) {
            params = params.append('assigneeIds', assigneeId);
        }
        return this.http.get<WorkflowDashboardTasksPage>(`${this.baseUrl}/workflow-dashboard/tasks`, { params });
    }

    getTaskDetail(taskId: string): Observable<{ documentId: string; documentTypeId: string; attributes: WorkflowDashboardAttribute[] }> {
        return this.http.get<{ documentId: string; documentTypeId: string; attributes: WorkflowDashboardAttribute[] }>(
            `${this.baseUrl}/workflow-dashboard/tasks/${encodeURIComponent(taskId)}/detail`);
    }

    private getBaseUrl(): string {
        const config = (window as any).appConfig;
        const configuredUrl = config?.copilotApiBaseUrl || config?.scheduleApiBaseUrl || '';
        let normalized = String(configuredUrl);
        while (normalized.endsWith('/')) normalized = normalized.slice(0, -1);
        return normalized.endsWith('/api') ? normalized : `${normalized}/api`;
    }
}

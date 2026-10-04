import {
  ExecutionLogModel,
  PagedResponseModel,
  RecurrenceType,
  ScheduleDashboardModel,
  ScheduleEventModel,
  ScheduleExecutionResultModel
} from './schedule.model';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private baseUrl = this.getApiBaseUrl();

  constructor(private http: HttpClient) { }

  private getApiBaseUrl(): string {
    const configuredBaseUrl = (window as any).appConfig?.copilotApiBaseUrl || environment['restApi'];
    const apiBaseUrl = configuredBaseUrl.replace(/\/+$/, '');
    return `${apiBaseUrl.endsWith('/api') ? apiBaseUrl : `${apiBaseUrl}/api`}/ScheduleEvents`;
  }

  // ============ Schedule Events CRUD ============

  /**
   * Get all schedule events with optional pagination and filtering
   */
  getScheduleEvents(
    pageNumber: number = 1,
    pageSize: number = 10,
    search?: string,
    isActive?: boolean
  ): Observable<PagedResponseModel<ScheduleEventModel>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (search) {
      params = params.set('name', search);
    }

    if (isActive !== undefined) {
      params = params.set('isActive', isActive.toString());
    }

    return this.http.get<PagedResponseModel<ScheduleEventModel>>(
      this.baseUrl,
      { params }
    );
  }

  /**
   * Get a specific schedule event by ID
   */
  getScheduleEventById(id: number): Observable<ScheduleEventModel> {
    return this.http.get<ScheduleEventModel>(
      `${this.baseUrl}/${id}`
    );
  }

  getScheduleDashboard(days: number = 7): Observable<ScheduleDashboardModel> {
    return this.http.get<ScheduleDashboardModel>(`${this.baseUrl}/dashboard`, {
      params: new HttpParams().set('days', days.toString())
    });
  }

  getAllExecutionLogs(
    pageNumber: number = 1,
    pageSize: number = 20,
    scheduleEventId?: number,
    status?: number,
    fromDate?: string,
    toDate?: string
  ): Observable<PagedResponseModel<ExecutionLogModel>> {
    let params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (scheduleEventId !== undefined) params = params.set('scheduleEventId', scheduleEventId.toString());
    if (status !== undefined) params = params.set('status', status.toString());
    if (fromDate) params = params.set('fromDate', fromDate);
    if (toDate) params = params.set('toDate', toDate);

    return this.http.get<PagedResponseModel<ExecutionLogModel>>(`${this.baseUrl}/logs`, { params });
  }

  /**
   * Create a new schedule event
   */
  createScheduleEvent(event: ScheduleEventModel): Observable<ScheduleEventModel> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<ScheduleEventModel>(
      this.baseUrl,
      event,
      { headers }
    );
  }

  /**
   * Update an existing schedule event
   */
  updateScheduleEvent(id: number, event: ScheduleEventModel): Observable<ScheduleEventModel> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<ScheduleEventModel>(
      `${this.baseUrl}/${id}`,
      event,
      { headers }
    );
  }

  /** Test an unsaved schedule action. */
  testScheduleEvent(event: ScheduleEventModel): Observable<ScheduleExecutionResultModel> {
    return this.http.post<ScheduleExecutionResultModel>(`${this.baseUrl}/test`, event);
  }

  /**
   * Delete a schedule event
   */
  deleteScheduleEvent(id: number): Observable<void> {
    return this.http.delete<void>(
      `${this.baseUrl}/${id}`
    );
  }

  /**
   * Activate a schedule event
   */
  toggleScheduleEvent(id: number): Observable<ScheduleEventModel> {
    return this.http.patch<ScheduleEventModel>(`${this.baseUrl}/${id}/toggle`, {});
  }

  // ============ Execution Logs ============

  /**
   * Get execution logs for a specific schedule event
   */
  getExecutionLogs(
    scheduleEventId: number,
    pageNumber: number = 1,
    pageSize: number = 10,
    status?: number
  ): Observable<PagedResponseModel<ExecutionLogModel>> {
    let params = new HttpParams()
      .set('scheduleEventId', scheduleEventId.toString())
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    if (status !== undefined) {
      params = params.set('status', status.toString());
    }

    return this.http.get<PagedResponseModel<ExecutionLogModel>>(
      `${this.baseUrl}/logs`,
      { params }
    );
  }

  /**
   * Get recurrence types for dropdown
   */
  getRecurrenceTypes(): RecurrenceTypeOption[] {
    return [
      { id: RecurrenceType.Once, name: 'Once (No Recurrence)', description: 'Execute once at specified time' },
      { id: RecurrenceType.Minutely, name: 'Minutely', description: 'Execute every N minutes' },
      { id: RecurrenceType.Hourly, name: 'Hourly', description: 'Execute every N hours' },
      { id: RecurrenceType.Daily, name: 'Daily', description: 'Execute every day at specified time' },
      { id: RecurrenceType.Weekly, name: 'Weekly', description: 'Execute on specific days of week' },
      { id: RecurrenceType.Monthly, name: 'Monthly', description: 'Execute on specific day of month' },
      { id: RecurrenceType.CronExpression, name: 'Cron Expression', description: 'Advanced scheduling with Cron' }
    ];
  }

  /**
   * Get execution statuses for dropdown
   */
  getExecutionStatuses(): ExecutionStatusOption[] {
    return [
      { id: 0, name: 'Running', color: 'processing' },
      { id: 1, name: 'Success', color: 'green' },
      { id: 2, name: 'Failed', color: 'red' },
      { id: 3, name: 'Skipped', color: 'default' },
      { id: 4, name: 'Timeout', color: 'orange' }
    ];
  }
}

// ============ Helper Interfaces ============

export interface RecurrenceTypeOption {
  id: RecurrenceType;
  name: string;
  description: string;
}

export interface ExecutionStatusOption {
  id: number;
  name: string;
  color: string;
}

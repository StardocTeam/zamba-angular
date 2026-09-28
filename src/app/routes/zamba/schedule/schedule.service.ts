import {
  ApiResponseModel,
  ExecutionLogModel,
  PagedResponseModel,
  RecurrenceType,
  ScheduleEventModel
} from './schedule.model';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {
  private baseUrl = `${environment['restApi']}/api/schedule`;
  private apiTimeout = 30000; // 30 seconds

  constructor(private http: HttpClient) { }

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
      params = params.set('search', search);
    }

    if (isActive !== undefined) {
      params = params.set('isActive', isActive.toString());
    }

    return this.http.get<PagedResponseModel<ScheduleEventModel>>(
      `${this.baseUrl}/events`,
      { params }
    );
  }

  /**
   * Get a specific schedule event by ID
   */
  getScheduleEventById(id: number): Observable<ApiResponseModel<ScheduleEventModel>> {
    return this.http.get<ApiResponseModel<ScheduleEventModel>>(
      `${this.baseUrl}/events/${id}`
    );
  }

  /**
   * Create a new schedule event
   */
  createScheduleEvent(event: ScheduleEventModel): Observable<ApiResponseModel<ScheduleEventModel>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<ApiResponseModel<ScheduleEventModel>>(
      `${this.baseUrl}/events`,
      event,
      { headers }
    );
  }

  /**
   * Update an existing schedule event
   */
  updateScheduleEvent(id: number, event: ScheduleEventModel): Observable<ApiResponseModel<ScheduleEventModel>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<ApiResponseModel<ScheduleEventModel>>(
      `${this.baseUrl}/events/${id}`,
      event,
      { headers }
    );
  }

  /**
   * Delete a schedule event
   */
  deleteScheduleEvent(id: number): Observable<ApiResponseModel<void>> {
    return this.http.delete<ApiResponseModel<void>>(
      `${this.baseUrl}/events/${id}`
    );
  }

  /**
   * Activate a schedule event
   */
  activateScheduleEvent(id: number): Observable<ApiResponseModel<void>> {
    return this.http.post<ApiResponseModel<void>>(
      `${this.baseUrl}/events/${id}/activate`,
      {}
    );
  }

  /**
   * Deactivate a schedule event
   */
  deactivateScheduleEvent(id: number): Observable<ApiResponseModel<void>> {
    return this.http.post<ApiResponseModel<void>>(
      `${this.baseUrl}/events/${id}/deactivate`,
      {}
    );
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
      `${this.baseUrl}/execution-logs`,
      { params }
    );
  }

  /**
   * Get a specific execution log
   */
  getExecutionLogById(id: number): Observable<ApiResponseModel<ExecutionLogModel>> {
    return this.http.get<ApiResponseModel<ExecutionLogModel>>(
      `${this.baseUrl}/execution-logs/${id}`
    );
  }

  // ============ Schedule Validation ============

  /**
   * Validate a schedule configuration
   */
  validateScheduleConfig(config: any): Observable<ApiResponseModel<{ isValid: boolean; errors?: string[] }>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<ApiResponseModel<{ isValid: boolean; errors?: string[] }>>(
      `${this.baseUrl}/validate-config`,
      config,
      { headers }
    );
  }

  /**
   * Test a schedule (execute immediately)
   */
  testScheduleEvent(id: number): Observable<ApiResponseModel<{ success: boolean; message: string }>> {
    return this.http.post<ApiResponseModel<{ success: boolean; message: string }>>(
      `${this.baseUrl}/events/${id}/test`,
      {}
    );
  }

  // ============ Statistics ============

  /**
   * Get schedule statistics
   */
  getScheduleStatistics(): Observable<ApiResponseModel<{
    totalSchedules: number;
    activeSchedules: number;
    failedExecutions: number;
    successfulExecutions: number;
    pendingExecutions: number;
  }>> {
    return this.http.get<ApiResponseModel<any>>(
      `${this.baseUrl}/statistics`
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
      { id: 0, name: 'Pending', color: 'blue' },
      { id: 1, name: 'Running', color: 'processing' },
      { id: 2, name: 'Success', color: 'green' },
      { id: 3, name: 'Failed', color: 'red' },
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

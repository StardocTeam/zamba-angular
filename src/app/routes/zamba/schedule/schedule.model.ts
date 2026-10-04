/**
 * Schedule Data Models and Interfaces
 */

// Recurrence types
export enum RecurrenceType {
  Once = 0,
  Minutely = 1,
  Hourly = 2,
  Daily = 3,
  Weekly = 4,
  Monthly = 5,
  CronExpression = 6
}

// Execution status
export enum ExecutionStatus {
  Running = 0,
  Success = 1,
  Failed = 2,
  Skipped = 3,
  Timeout = 4
}

export enum ScheduleExecutionType {
  ExecuteRule = 0,
  ExecuteQuery = 1,
  ExecuteEndPoint = 2
}

export interface ScheduleExecutionResultModel {
  statusCode: number;
  responseBody?: string;
  errorMessage?: string;
  durationMs: number;
}

// Schedule Config Model
export interface ScheduleConfigModel {
  recurrenceType: RecurrenceType;
  startDate?: string; // ISO 8601 date
  endDate?: string; // ISO 8601 date
  timeOfDay?: string; // HH:MM format
  daysOfWeek?: string; // Comma-separated: SUN,MON,TUE,WED,THU,FRI,SAT
  dayOfMonth?: number; // 1-31
  cronExpression?: string;
  timeZone?: string; // IANA timezone ID
  intervalValue?: number; // For minutely/hourly
}

// Schedule Event Model
export interface ScheduleEventModel {
  id: number;
  name: string;
  description?: string;
  ruleId: string;
  executionType: ScheduleExecutionType;
  scheduleConfig: ScheduleConfigModel;
  isActive: boolean;
  endpointOverride?: string;
  query?: string;
  endpointUrl?: string;
  endpointMethod?: 'GET' | 'POST';
  endpointParameters?: string;
  endpointBody?: string;
  createdAt: string;
  createdBy?: string;
  modifiedAt?: string;
  modifiedBy?: string;
}

// Execution Log Model
export interface ExecutionLogModel {
  id: number;
  scheduleEventId: number;
  scheduleEventName?: string;
  scheduledAt: string;
  startedAt?: string;
  finishedAt?: string;
  status: ExecutionStatus;
  httpStatusCode?: number;
  responseBody?: string;
  errorMessage?: string;
  durationMs?: number;
  retryCount: number;
}

export interface ScheduleDashboardModel {
  fromDate: string;
  toDate: string;
  totalEvents: number;
  activeEvents: number;
  totalExecutions: number;
  runningExecutions: number;
  successfulExecutions: number;
  failedExecutions: number;
  timedOutExecutions: number;
  skippedExecutions: number;
  successRate: number;
  averageDurationMs?: number;
  trend: ScheduleDashboardDayModel[];
  topEvents: ScheduleDashboardEventModel[];
  activeExecutions: ExecutionLogModel[];
  worker?: ScheduleWorkerStatusModel;
}

export interface ScheduleDashboardDayModel {
  date: string;
  success: number;
  failed: number;
  running: number;
  timedOut: number;
  skipped: number;
}

export interface ScheduleDashboardEventModel {
  scheduleEventId: number;
  name: string;
  executions: number;
  failures: number;
  averageDurationMs?: number;
}

export interface ScheduleWorkerStatusModel {
  workerInstanceId: string;
  hostName: string;
  startedAtUtc: string;
  lastHeartbeatAtUtc: string;
  lastSyncAtUtc?: string;
  registeredJobCount: number;
  activeJobCount: number;
  lastError?: string;
  isOnline: boolean;
}

// API Response wrapper
export interface ApiResponseModel<T> {
  success: boolean;
  data?: T;
  message?: string;
  errors?: string[];
}

// Paginated response
export interface PagedResponseModel<T> {
  items: T[];
  pageNumber: number;
  pageSize: number;
  totalCount: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

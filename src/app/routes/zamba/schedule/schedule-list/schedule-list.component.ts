import { Component, OnDestroy, OnInit } from '@angular/core';
import {
  ExecutionStatusOption,
  RecurrenceTypeOption,
  ScheduleService
} from '../schedule.service';
import {
  PagedResponseModel,
  RecurrenceType,
  ScheduleEventModel,
  ScheduleExecutionType
} from '../schedule.model';
import { debounceTime, distinctUntilChanged, takeUntil } from 'rxjs/operators';

import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-schedule-list',
  templateUrl: './schedule-list.component.html',
  styleUrls: ['./schedule-list.component.css']
})
export class ScheduleListComponent implements OnInit, OnDestroy {
  schedules: ScheduleEventModel[] = [];
  loading = false;
  searchValue = '';
  filterActive: boolean | null = null;
  recurrenceTypeMap: Map<RecurrenceType, string> = new Map();
  executionTypeMap = new Map<ScheduleExecutionType, string>([
    [ScheduleExecutionType.ExecuteRule, 'Execute Rule'],
    [ScheduleExecutionType.ExecuteQuery, 'Execute Query'],
    [ScheduleExecutionType.ExecuteEndPoint, 'Execute Endpoint']
  ]);

  // Pagination
  pageIndex = 1;
  pageSize = 10;
  total = 0;

  // Subjects for cleanup
  private destroy$ = new Subject<void>();
  private search$ = new Subject<string>();

  constructor(
    private scheduleService: ScheduleService,
    private router: Router,
    private message: NzMessageService,
    private modal: NzModalService
  ) {
    this.initRecurrenceTypeMap();
  }

  ngOnInit(): void {
    this.loadSchedules();

    // Setup debounced search
    this.search$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      takeUntil(this.destroy$)
    ).subscribe(search => {
      this.searchValue = search;
      this.pageIndex = 1;
      this.loadSchedules();
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Load schedules from API
   */
  loadSchedules(): void {
    this.loading = true;
    this.scheduleService.getScheduleEvents(
      this.pageIndex,
      this.pageSize,
      this.searchValue || undefined,
      this.filterActive !== null ? this.filterActive : undefined
    )
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response: PagedResponseModel<ScheduleEventModel>) => {
          this.schedules = response.items;
          this.total = response.totalCount;
          this.loading = false;
        },
        error: (error) => {
          this.message.error('Failed to load schedules');
          console.error('Error loading schedules:', error);
          this.loading = false;
        }
      });
  }

  /**
   * Handle search input
   */
  onSearchChange(value: string): void {
    this.search$.next(value);
  }

  /**
   * Handle filter change
   */
  onFilterChange(): void {
    this.pageIndex = 1;
    this.loadSchedules();
  }

  /**
   * Handle pagination change
   */
  onPageIndexChange(pageIndex: number): void {
    this.pageIndex = pageIndex;
    this.loadSchedules();
  }

  onPageSizeChange(pageSize: number): void {
    this.pageSize = pageSize;
    this.pageIndex = 1;
    this.loadSchedules();
  }

  /**
   * Navigate to create schedule
   */
  goToCreate(): void {
    this.router.navigate(['/zamba/schedule/new']);
  }

  /**
   * Navigate to edit schedule
   */
  goToEdit(id: number): void {
    this.router.navigate(['/zamba/schedule', id, 'edit']);
  }

  /**
   * View schedule details
   */
  viewDetails(schedule: ScheduleEventModel): void {
    this.router.navigate(['/zamba/schedule', schedule.id]);
  }

  /**
   * View execution logs
   */
  viewLogs(schedule: ScheduleEventModel): void {
    this.router.navigate(['/zamba/schedule', schedule.id, 'logs']);
  }

  /**
   * Activate schedule
   */
  activateSchedule(schedule: ScheduleEventModel): void {
    this.scheduleService.toggleScheduleEvent(schedule.id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this.message.success('Schedule activated successfully');
          this.loadSchedules();
        },
        error: (error) => {
          this.message.error('Failed to activate schedule');
          console.error('Error activating schedule:', error);
        }
      });
  }

  /**
   * Deactivate schedule
   */
  deactivateSchedule(schedule: ScheduleEventModel): void {
    this.scheduleService.toggleScheduleEvent(schedule.id)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          this.message.success('Schedule deactivated successfully');
          this.loadSchedules();
        },
        error: (error) => {
          this.message.error('Failed to deactivate schedule');
          console.error('Error deactivating schedule:', error);
        }
      });
  }

  /**
   * Delete schedule
   */
  deleteSchedule(schedule: ScheduleEventModel): void {
    this.modal.confirm({
      nzTitle: 'Delete Schedule',
      nzContent: `Are you sure you want to delete the schedule "${schedule.name}"?`,
      nzOkText: 'Delete',
      nzCancelText: 'Cancel',
      nzOkDanger: true,
      nzOnOk: () => {
        this.scheduleService.deleteScheduleEvent(schedule.id)
          .pipe(takeUntil(this.destroy$))
          .subscribe({
            next: () => {
              this.message.success('Schedule deleted successfully');
              this.loadSchedules();
            },
            error: (error) => {
              this.message.error('Failed to delete schedule');
              console.error('Error deleting schedule:', error);
            }
          });
      }
    });
  }

  /**
   * Get recurrence type label
   */
  getRecurrenceTypeLabel(type: RecurrenceType): string {
    return this.recurrenceTypeMap.get(type) || 'Unknown';
  }

  getExecutionTypeLabel(type: ScheduleExecutionType): string {
    return this.executionTypeMap.get(type) || 'Execute Rule';
  }

  getExecutionTarget(schedule: ScheduleEventModel): string {
    switch (schedule.executionType) {
      case ScheduleExecutionType.ExecuteQuery:
        return schedule.query?.trim().slice(0, 80) || 'SQL query';
      case ScheduleExecutionType.ExecuteEndPoint:
        return schedule.endpointUrl || 'Endpoint';
      default:
        return schedule.ruleId;
    }
  }

  /**
   * Initialize recurrence type map
   */
  private initRecurrenceTypeMap(): void {
    this.recurrenceTypeMap.set(RecurrenceType.Once, 'Once');
    this.recurrenceTypeMap.set(RecurrenceType.Minutely, 'Every N Minutes');
    this.recurrenceTypeMap.set(RecurrenceType.Hourly, 'Every N Hours');
    this.recurrenceTypeMap.set(RecurrenceType.Daily, 'Daily');
    this.recurrenceTypeMap.set(RecurrenceType.Weekly, 'Weekly');
    this.recurrenceTypeMap.set(RecurrenceType.Monthly, 'Monthly');
    this.recurrenceTypeMap.set(RecurrenceType.CronExpression, 'Cron Expression');
  }

  /**
   * Get active status badge color
   */
  getStatusColor(isActive: boolean): string {
    return isActive ? 'green' : 'red';
  }

  /**
   * Clear filters
   */
  clearFilters(): void {
    this.searchValue = '';
    this.filterActive = null;
    this.pageIndex = 1;
    this.loadSchedules();
  }
}

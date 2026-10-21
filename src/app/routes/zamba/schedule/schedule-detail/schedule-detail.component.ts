import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RecurrenceType, ScheduleEventModel } from '../schedule.model';

import { NzMessageService } from 'ng-zorro-antd/message';
import { ScheduleService } from '../schedule.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-schedule-detail',
  templateUrl: './schedule-detail.component.html',
  styleUrls: ['./schedule-detail.component.css']
})
export class ScheduleDetailComponent implements OnInit, OnDestroy {
  schedule: ScheduleEventModel | null = null;
  loading = false;
  scheduleId: number | null = null;
  recurrenceTypeMap: Map<RecurrenceType, string> = new Map();

  private destroy$ = new Subject<void>();

  constructor(
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router,
    private message: NzMessageService
  ) {
    this.initRecurrenceTypeMap();
  }

  ngOnInit(): void {
    this.route.params.pipe(takeUntil(this.destroy$)).subscribe(params => {
      if (params['id']) {
        this.scheduleId = parseInt(params['id']);
        this.loadSchedule();
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Load schedule details
   */
  loadSchedule(): void {
    if (!this.scheduleId) return;

    this.loading = true;
    this.scheduleService.getScheduleEventById(this.scheduleId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.schedule = response;
          this.loading = false;
        },
        error: (error) => {
          this.message.error('Failed to load schedule');
          console.error('Error loading schedule:', error);
          this.loading = false;
        }
      });
  }

  /**
   * Navigate to edit schedule
   */
  goToEdit(): void {
    this.router.navigate(['/zamba/schedule', this.scheduleId, 'edit']);
  }

  /**
   * View execution logs
   */
  viewLogs(): void {
    this.router.navigate(['/zamba/schedule', this.scheduleId, 'logs']);
  }

  /**
   * Go back to list
   */
  goBack(): void {
    this.router.navigate(['/zamba/schedule']);
  }

  /**
   * Get recurrence type label
   */
  getRecurrenceTypeLabel(type: RecurrenceType): string {
    return this.recurrenceTypeMap.get(type) || 'Unknown';
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
   * Get status badge color
   */
  getStatusColor(): string {
    return this.schedule?.isActive ? 'green' : 'red';
  }

  /**
   * Get status text
   */
  getStatusText(): string {
    return this.schedule?.isActive ? 'Active' : 'Inactive';
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import {
    ExecutionLogModel,
    ExecutionStatus,
    PagedResponseModel,
    ScheduleDashboardModel,
    ScheduleEventModel
} from '../schedule.model';
import { Subject, interval } from 'rxjs';

import { NzMessageService } from 'ng-zorro-antd/message';
import { Router } from '@angular/router';
import { ScheduleService } from '../schedule.service';
import { takeUntil } from 'rxjs/operators';

@Component({
    selector: 'app-schedule-dashboard',
    templateUrl: './schedule-dashboard.component.html',
    styleUrls: ['./schedule-dashboard.component.css']
})
export class ScheduleDashboardComponent implements OnInit, OnDestroy {
    readonly ExecutionStatus = ExecutionStatus;
    dashboard: ScheduleDashboardModel | null = null;
    history: ExecutionLogModel[] = [];
    scheduleOptions: ScheduleEventModel[] = [];
    loadingDashboard = false;
    loadingHistory = false;
    rangeDays = 7;
    pageIndex = 1;
    pageSize = 10;
    totalHistory = 0;
    filterEventId: number | null = null;
    filterStatus: ExecutionStatus | null = null;
    lastUpdated: Date | null = null;

    private readonly destroy$ = new Subject<void>();

    constructor(
        private readonly scheduleService: ScheduleService,
        private readonly router: Router,
        private readonly message: NzMessageService
    ) { }

    ngOnInit(): void {
        this.refresh();
        this.scheduleService.getScheduleEvents(1, 100)
            .pipe(takeUntil(this.destroy$))
            .subscribe({ next: response => this.scheduleOptions = response.items });

        interval(15000).pipe(takeUntil(this.destroy$)).subscribe(() => this.refresh(true));
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }

    refresh(silent = false): void {
        this.loadDashboard(silent);
        this.loadHistory(silent);
    }

    setRange(days: number): void {
        if (this.rangeDays === days) return;
        this.rangeDays = days;
        this.pageIndex = 1;
        this.refresh();
    }

    onFiltersChange(): void {
        this.pageIndex = 1;
        this.loadHistory();
    }

    onPageIndexChange(pageIndex: number): void {
        this.pageIndex = pageIndex;
        this.loadHistory();
    }

    onPageSizeChange(pageSize: number): void {
        this.pageSize = pageSize;
        this.pageIndex = 1;
        this.loadHistory();
    }

    filterByEvent(eventId: number): void {
        this.filterEventId = eventId;
        this.onFiltersChange();
    }

    getTrendTotal(day: ScheduleDashboardModel['trend'][number]): number {
        return day.success + day.failed + day.running + day.timedOut + day.skipped;
    }

    getTrendMaximum(): number {
        return Math.max(1, ...(this.dashboard?.trend.map(day => this.getTrendTotal(day)) ?? [1]));
    }

    getTrendHeight(value: number): number {
        return value * 100 / this.getTrendMaximum();
    }

    getStatusLabel(status: ExecutionStatus): string {
        return {
            [ExecutionStatus.Running]: 'Running',
            [ExecutionStatus.Success]: 'Success',
            [ExecutionStatus.Failed]: 'Failed',
            [ExecutionStatus.Skipped]: 'Skipped',
            [ExecutionStatus.Timeout]: 'Timeout'
        }[status] ?? 'Unknown';
    }

    getStatusColor(status: ExecutionStatus): string {
        return {
            [ExecutionStatus.Running]: 'processing',
            [ExecutionStatus.Success]: 'green',
            [ExecutionStatus.Failed]: 'red',
            [ExecutionStatus.Skipped]: 'default',
            [ExecutionStatus.Timeout]: 'orange'
        }[status] ?? 'default';
    }

    openSchedule(id: number): void {
        this.router.navigate(['/zamba/schedule', id]);
    }

    manageSchedules(): void {
        this.router.navigate(['/zamba/schedule']);
    }

    createSchedule(): void {
        this.router.navigate(['/zamba/schedule/new']);
    }

    private loadDashboard(silent: boolean): void {
        if (!silent) this.loadingDashboard = true;
        this.scheduleService.getScheduleDashboard(this.rangeDays)
            .pipe(takeUntil(this.destroy$))
            .subscribe({
                next: dashboard => {
                    this.dashboard = dashboard;
                    this.lastUpdated = new Date();
                    this.loadingDashboard = false;
                },
                error: error => {
                    this.loadingDashboard = false;
                    if (!silent) this.message.error('Could not load schedule monitoring data');
                    console.error('Error loading schedule dashboard:', error);
                }
            });
    }

    private loadHistory(silent = false): void {
        if (!silent) this.loadingHistory = true;
        const toDate = new Date();
        const fromDate = new Date(toDate.getTime() - this.rangeDays * 24 * 60 * 60 * 1000);
        this.scheduleService.getAllExecutionLogs(
            this.pageIndex,
            this.pageSize,
            this.filterEventId ?? undefined,
            this.filterStatus ?? undefined,
            fromDate.toISOString(),
            toDate.toISOString()
        ).pipe(takeUntil(this.destroy$)).subscribe({
            next: (response: PagedResponseModel<ExecutionLogModel>) => {
                this.history = response.items;
                this.totalHistory = response.totalCount;
                this.loadingHistory = false;
            },
            error: error => {
                this.loadingHistory = false;
                if (!silent) this.message.error('Could not load execution history');
                console.error('Error loading schedule history:', error);
            }
        });
    }
}

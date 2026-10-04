import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ExecutionLogModel, ExecutionStatus, PagedResponseModel } from '../schedule.model';
import { ExecutionStatusOption, ScheduleService } from '../schedule.service';

import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-execution-logs',
  templateUrl: './execution-logs.component.html',
  styleUrls: ['./execution-logs.component.css']
})
export class ExecutionLogsComponent implements OnInit, OnDestroy {
  logs: ExecutionLogModel[] = [];
  loading = false;
  scheduleId: number | null = null;
  scheduleName = '';

  // Pagination
  pageIndex = 1;
  pageSize = 20;
  total = 0;

  // Filters
  filterStatus: ExecutionStatus | null = null;
  statusOptions: ExecutionStatusOption[] = [];

  // Status mapping
  statusMap: Map<ExecutionStatus, string> = new Map();
  statusColorMap: Map<ExecutionStatus, string> = new Map();

  private destroy$ = new Subject<void>();

  constructor(
    private scheduleService: ScheduleService,
    private route: ActivatedRoute,
    private router: Router,
    private message: NzMessageService,
    private modal: NzModalService
  ) {
    this.initStatusMaps();
  }

  ngOnInit(): void {
    this.statusOptions = this.scheduleService.getExecutionStatuses();

    this.route.params.pipe(takeUntil(this.destroy$)).subscribe(params => {
      if (params['id']) {
        this.scheduleId = parseInt(params['id']);
        this.loadScheduleName();
        this.loadExecutionLogs();
      }
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  /**
   * Load schedule name
   */
  private loadScheduleName(): void {
    if (!this.scheduleId) return;

    this.scheduleService.getScheduleEventById(this.scheduleId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response) => {
          this.scheduleName = response.name;
        },
        error: (error) => {
          console.error('Error loading schedule:', error);
        }
      });
  }

  /**
   * Load execution logs
   */
  loadExecutionLogs(): void {
    if (!this.scheduleId) return;

    this.loading = true;
    this.scheduleService.getExecutionLogs(
      this.scheduleId,
      this.pageIndex,
      this.pageSize,
      this.filterStatus !== null ? this.filterStatus : undefined
    )
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (response: PagedResponseModel<ExecutionLogModel>) => {
          this.logs = response.items;
          this.total = response.totalCount;
          this.loading = false;
        },
        error: (error) => {
          this.message.error('Failed to load execution logs');
          console.error('Error loading logs:', error);
          this.loading = false;
        }
      });
  }

  /**
   * Handle pagination changes
   */
  onPageIndexChange(pageIndex: number): void {
    this.pageIndex = pageIndex;
    this.loadExecutionLogs();
  }

  onPageSizeChange(pageSize: number): void {
    this.pageSize = pageSize;
    this.pageIndex = 1;
    this.loadExecutionLogs();
  }

  /**
   * Handle status filter change
   */
  onFilterChange(): void {
    this.pageIndex = 1;
    this.loadExecutionLogs();
  }

  /**
   * View log details
   */
  viewLogDetails(log: ExecutionLogModel): void {
    this.modal.create({
      nzTitle: 'Execution Log Details',
      nzContent: `
        <div class="log-details">
          <p><strong>Status:</strong> ${this.getStatusLabel(log.status)}</p>
          <p><strong>Scheduled At:</strong> ${new Date(log.scheduledAt).toLocaleString()}</p>
          <p><strong>Started At:</strong> ${log.startedAt ? new Date(log.startedAt).toLocaleString() : 'N/A'}</p>
          <p><strong>Finished At:</strong> ${log.finishedAt ? new Date(log.finishedAt).toLocaleString() : 'N/A'}</p>
          <p><strong>Duration:</strong> ${log.durationMs ? log.durationMs + ' ms' : 'N/A'}</p>
          <p><strong>HTTP Status:</strong> ${log.httpStatusCode || 'N/A'}</p>
          <p><strong>Retry Count:</strong> ${log.retryCount}</p>
          ${log.errorMessage ? `<p><strong>Error:</strong> ${log.errorMessage}</p>` : ''}
          ${log.responseBody ? `<p><strong>Response:</strong><pre>${log.responseBody}</pre></p>` : ''}
        </div>
      `,
      nzOkText: 'Close',
      nzCancelText: null,
      nzFooter: null
    });
  }

  /**
   * Export logs to CSV
   */
  exportLogs(): void {
    if (this.logs.length === 0) {
      this.message.warning('No logs to export');
      return;
    }

    const headers = ['Scheduled At', 'Started At', 'Finished At', 'Status', 'Duration (ms)', 'HTTP Status', 'Error Message'];
    const rows = this.logs.map(log => [
      new Date(log.scheduledAt).toLocaleString(),
      log.startedAt ? new Date(log.startedAt).toLocaleString() : 'N/A',
      log.finishedAt ? new Date(log.finishedAt).toLocaleString() : 'N/A',
      this.getStatusLabel(log.status),
      log.durationMs || 'N/A',
      log.httpStatusCode || 'N/A',
      log.errorMessage || ''
    ]);

    const csv = [
      headers.join(','),
      ...rows.map(row => row.map(cell => `"${cell}"`).join(','))
    ].join('\n');

    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const link = document.createElement('a');
    link.setAttribute('href', URL.createObjectURL(blob));
    link.setAttribute('download', `execution-logs-${this.scheduleName}-${new Date().toISOString().split('T')[0]}.csv`);
    link.style.visibility = 'hidden';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    this.message.success('Logs exported successfully');
  }

  /**
   * Clear old logs
   */
  clearOldLogs(): void {
    this.modal.confirm({
      nzTitle: 'Clear Old Logs',
      nzContent: 'Are you sure you want to delete execution logs older than 30 days? This action cannot be undone.',
      nzOkText: 'Delete',
      nzCancelText: 'Cancel',
      nzOkDanger: true,
      nzOnOk: () => {
        // This would call a backend endpoint to clear old logs
        this.message.info('Clear old logs functionality coming soon');
      }
    });
  }

  /**
   * Go back to schedule details
   */
  goBack(): void {
    this.router.navigate(['/zamba/schedule', this.scheduleId]);
  }

  /**
   * Get status label
   */
  getStatusLabel(status: ExecutionStatus): string {
    return this.statusMap.get(status) || 'Unknown';
  }

  /**
   * Get status badge color
   */
  getStatusColor(status: ExecutionStatus): string {
    return this.statusColorMap.get(status) || 'default';
  }

  /**
   * Initialize status maps
   */
  private initStatusMaps(): void {
    this.statusMap.set(ExecutionStatus.Running, 'Running');
    this.statusMap.set(ExecutionStatus.Success, 'Success');
    this.statusMap.set(ExecutionStatus.Failed, 'Failed');
    this.statusMap.set(ExecutionStatus.Skipped, 'Skipped');
    this.statusMap.set(ExecutionStatus.Timeout, 'Timeout');

    this.statusColorMap.set(ExecutionStatus.Running, 'processing');
    this.statusColorMap.set(ExecutionStatus.Success, 'green');
    this.statusColorMap.set(ExecutionStatus.Failed, 'red');
    this.statusColorMap.set(ExecutionStatus.Skipped, 'default');
    this.statusColorMap.set(ExecutionStatus.Timeout, 'orange');
  }

  /**
   * Clear filters
   */
  clearFilters(): void {
    this.filterStatus = null;
    this.pageIndex = 1;
    this.loadExecutionLogs();
  }

  /**
   * Refresh logs
   */
  refreshLogs(): void {
    this.loadExecutionLogs();
  }
}

import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  WorkflowDashboardAttribute,
  WorkflowDashboardCatalogItem,
  WorkflowDashboardFiltersData,
  WorkflowDashboardQuery,
  WorkflowDashboardTask,
  WorkflowDashboardTasksPage
} from './workflow-tasks.models';

import { WorkflowTasksService } from './workflow-tasks.service';

@Component({
  selector: 'app-workflow-tasks',
  templateUrl: './workflow-tasks.component.html',
  styleUrls: ['./workflow-tasks.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class WorkflowTasksComponent implements OnInit {
  filtersData: WorkflowDashboardFiltersData | null = null;
  result: WorkflowDashboardTasksPage | null = null;
  selectedTask: WorkflowDashboardTask | null = null;
  selectedAttributes: WorkflowDashboardAttribute[] = [];
  filters: WorkflowDashboardQuery = this.defaultFilters();
  loadingFilters = true;
  loadingTasks = false;
  loadingDetail = false;
  errorMessage = '';
  readonly pageSize = 25;

  constructor(private readonly service: WorkflowTasksService, private readonly cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.service.getFilters().subscribe({
      next: data => {
        this.filtersData = data;
        this.loadingFilters = false;
        this.loadTasks();
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingFilters = false;
        this.loadingTasks = false;
        this.errorMessage = 'No se pudo cargar el catálogo de workflows. Verificá la conexión con Zamba.Api.';
        this.cdr.markForCheck();
      }
    });
  }

  applyFilters(): void {
    this.filters.page = 1;
    this.selectedTask = null;
    this.loadTasks();
  }

  clearFilters(): void {
    this.filters = this.defaultFilters();
    this.applyFilters();
  }

  refresh(): void {
    this.loadTasks();
  }

  changePage(page: number): void {
    if (page < 1 || page > this.pageCount) return;
    this.filters.page = page;
    this.loadTasks();
  }

  selectTask(task: WorkflowDashboardTask): void {
    if (this.selectedTask === task) {
      this.selectedTask = null;
      this.selectedAttributes = [];
      return;
    }
    this.selectedTask = task;
    this.selectedAttributes = [];
    const taskId = String(task.taskId ?? task.documentId ?? '');
    if (!taskId) return;
    this.loadingDetail = true;
    this.service.getTaskDetail(taskId).subscribe({
      next: detail => {
        this.selectedAttributes = detail.attributes ?? [];
        this.loadingDetail = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingDetail = false;
        this.errorMessage = 'No se pudieron cargar los atributos de la entidad.';
        this.cdr.markForCheck();
      }
    });
    this.cdr.markForCheck();
  }

  get visibleSteps(): WorkflowDashboardCatalogItem[] {
    const steps = this.filtersData?.steps ?? [];
    return this.filters.workflowId
      ? steps.filter(step => this.toText(this.optionValue(step, 'workid', 'work_id')) === this.filters.workflowId)
      : steps;
  }

  get pageCount(): number {
    return Math.max(1, Math.ceil((this.result?.total ?? 0) / this.pageSize));
  }

  get firstRow(): number {
    return this.result?.total ? (this.filters.page - 1) * this.pageSize + 1 : 0;
  }

  get lastRow(): number {
    return Math.min(this.filters.page * this.pageSize, this.result?.total ?? 0);
  }

  optionId(item: WorkflowDashboardCatalogItem, ...keys: string[]): string {
    return this.toText(this.optionValue(item, ...keys));
  }

  optionName(item: WorkflowDashboardCatalogItem, ...keys: string[]): string {
    return this.toText(this.optionValue(item, ...keys)).trim() || 'Sin nombre';
  }

  principalLabel(principal: WorkflowDashboardCatalogItem): string {
    const name = this.optionName(principal, 'name');
    const type = this.toText(this.optionValue(principal, 'principalType')).trim();
    return type ? `${name} · ${type}` : name;
  }

  display(value: unknown, fallback = '—'): string {
    return this.toText(value) || fallback;
  }

  formatDate(value: unknown): string {
    if (!value) return '—';
    const date = new Date(this.toText(value));
    if (!Number.isFinite(date.getTime())) return '—';
    return new Intl.DateTimeFormat('es-AR', { dateStyle: 'medium', timeStyle: 'short' }).format(date);
  }

  formatNumber(value: number | null | undefined): string {
    return new Intl.NumberFormat('es-AR').format(value ?? 0);
  }

  trackTask(index: number, task: WorkflowDashboardTask): string {
    const id = task.taskId ?? task.documentId;
    return typeof id === 'string' || typeof id === 'number' ? String(id) : String(index);
  }

  trackAttribute(_index: number, attribute: WorkflowDashboardAttribute): string {
    return attribute.indexId;
  }

  private loadTasks(): void {
    this.loadingTasks = true;
    this.errorMessage = '';
    this.cdr.markForCheck();
    this.service.getTasks({ ...this.filters, pageSize: this.pageSize }).subscribe({
      next: result => {
        this.result = result;
        this.loadingTasks = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingTasks = false;
        this.errorMessage = 'No se pudieron consultar las tareas. Revisá la disponibilidad del API y de SQL Server.';
        this.cdr.markForCheck();
      }
    });
  }

  private optionValue(item: WorkflowDashboardCatalogItem, ...keys: string[]): unknown {
    const normalized = new Map(Object.entries(item).map(([key, value]) => [key.toLowerCase(), value]));
    for (const key of keys) {
      const value = normalized.get(key.toLowerCase());
      if (value !== null && value !== undefined) return value;
    }
    return null;
  }

  private toText(value: unknown): string {
    if (typeof value === 'string' || typeof value === 'number' || typeof value === 'boolean') return String(value);
    if (value instanceof Date) return value.toISOString();
    return '';
  }

  private defaultFilters(): WorkflowDashboardQuery {
    return {
      workflowId: '',
      stepId: '',
      documentTypeId: '',
      assigneeIds: [],
      from: '',
      to: '',
      dateField: 'checkin',
      page: 1,
      pageSize: this.pageSize
    };
  }
}

import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
  UsersDashboardCatalogItem,
  UsersDashboardDistributionItem,
  UsersDashboardFiltersData,
  UsersDashboardHistoryItem,
  UsersDashboardOverview,
  UsersDashboardQuery,
  UsersDashboardTask,
  UsersDashboardTasksPage
} from './users-dashboard.models';

import { UsersDashboardService } from './users-dashboard.service';

@Component({
  selector: 'app-users-dashboard',
  templateUrl: './users-dashboard.component.html',
  styleUrls: ['./users-dashboard.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UsersDashboardComponent implements OnInit {
  filtersData: UsersDashboardFiltersData | null = null;
  overview: UsersDashboardOverview | null = null;
  tasks: UsersDashboardTasksPage | null = null;
  filters: UsersDashboardQuery = this.defaultFilters();
  userDirectory: Array<{ id: string; name: string }> = [];
  selectedUserName = '';
  reassignmentTargets: Record<string, string> = {};
  capacityHoursPerDay = 6;
  loading = true;
  loadingTasks = false;
  errorMessage = '';
  statusMessage = '';
  readonly pageSize = 25;

  constructor(private readonly service: UsersDashboardService, private readonly cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.service.getFilters().subscribe({
      next: data => {
        this.filtersData = data;
        this.loading = false;
        this.loadOverview();
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No se pudieron cargar los usuarios y filtros del dashboard.';
        this.cdr.markForCheck();
      }
    });
  }

  applyFilters(): void {
    this.selectedUserName = this.userDirectory.find(user => user.id === this.filters.userId)?.name ?? '';
    this.loadOverview();
    this.loadTasks(1);
  }

  clearFilters(): void {
    this.filters = this.defaultFilters();
    this.tasks = null;
    this.selectedUserName = '';
    this.applyFilters();
  }

  selectUser(userId: string, userName: string): void {
    this.filters.userId = userId;
    this.selectedUserName = userName;
    this.loadOverview();
    this.loadTasks(1);
  }

  refresh(): void {
    this.applyFilters();
  }

  changePage(page: number): void {
    if (page < 1 || page > this.taskPageCount) return;
    this.loadTasks(page);
  }

  reassign(task: UsersDashboardTask, destinationId: string): void {
    const taskId = String(task.taskId ?? '');
    if (!taskId || !destinationId) return;
    this.statusMessage = '';
    this.service.reassignTask(taskId, destinationId).subscribe({
      next: () => {
        const destination = this.userDirectory.find(user => user.id === destinationId);
        this.statusMessage = `Tarea reasignada a ${destination?.name ?? 'otro usuario'}.`;
        this.loadOverview();
        this.loadTasks(this.tasks?.page ?? 1);
      },
      error: () => {
        this.errorMessage = 'No se pudo reasignar la tarea. Verificá que el usuario destino siga activo.';
        this.cdr.markForCheck();
      }
    });
  }

  get visibleSteps(): UsersDashboardCatalogItem[] {
    const steps = this.filtersData?.steps ?? [];
    return this.filters.workflowId
      ? steps.filter(step => this.toText(this.optionValue(step, 'workid', 'work_id')) === this.filters.workflowId)
      : steps;
  }

  get taskPageCount(): number {
    return Math.max(1, Math.ceil((this.tasks?.total ?? 0) / this.pageSize));
  }

  get maxPendingTasks(): number {
    return Math.max(1, ...((this.overview?.distribution ?? []).map(item => this.asNumber(item.pendingTasks))));
  }

  get maxAverageMinutes(): number {
    return Math.max(1, ...((this.overview?.distribution ?? []).map(item => this.asNumber(item.averageExecutionMinutes))));
  }

  get forecastUserCount(): number {
    const distribution = this.overview?.distribution ?? [];
    return distribution.filter(item => this.asNumber(item.pendingTasks) > 0 && item.forecastMinutes !== null).length;
  }

  optionId(item: UsersDashboardCatalogItem, ...keys: string[]): string {
    return this.toText(this.optionValue(item, ...keys));
  }

  optionName(item: UsersDashboardCatalogItem, ...keys: string[]): string {
    return this.toText(this.optionValue(item, ...keys)).trim() || 'Sin nombre';
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

  formatNumber(value: unknown): string {
    return new Intl.NumberFormat('es-AR', { maximumFractionDigits: 0 }).format(this.asNumber(value));
  }

  formatMinutes(value: unknown): string {
    if (value === null || value === undefined || value === '') return 'Sin datos';
    const minutes = this.asNumber(value);
    if (!Number.isFinite(minutes) || minutes < 0) return 'Sin datos';
    if (minutes < 60) return `${new Intl.NumberFormat('es-AR', { maximumFractionDigits: 1 }).format(minutes)} min`;
    const hours = minutes / 60;
    return `${new Intl.NumberFormat('es-AR', { maximumFractionDigits: 1 }).format(hours)} h`;
  }

  forecastDate(value: unknown, capacityUsers: number): string {
    if (value === null || value === undefined || value === '') return 'Sin datos';
    const minutes = this.asNumber(value);
    const dailyCapacity = Math.max(0.5, this.asNumber(this.capacityHoursPerDay)) * 60 * Math.max(1, capacityUsers);
    if (minutes < 0 || !Number.isFinite(minutes)) return 'Sin datos';
    const remainingDays = Math.ceil(minutes / dailyCapacity);
    const date = new Date();
    let added = 0;
    while (added < remainingDays) {
      date.setDate(date.getDate() + 1);
      if (date.getDay() !== 0 && date.getDay() !== 6) added++;
    }
    return new Intl.DateTimeFormat('es-AR', { dateStyle: 'medium' }).format(date);
  }

  trackDistribution(_index: number, item: UsersDashboardDistributionItem): string {
    return item.userId;
  }

  trackHistory(index: number, item: UsersDashboardHistoryItem): string {
    return `${item.documentId ?? ''}-${item.date ?? index}`;
  }

  trackTask(index: number, task: UsersDashboardTask): string {
    return String(task.taskId ?? task.documentId ?? index);
  }

  private loadOverview(): void {
    this.loading = true;
    this.errorMessage = '';
    this.statusMessage = '';
    this.service.getOverview(this.filters).subscribe({
      next: overview => {
        this.overview = overview;
        if (!this.userDirectory.length && !this.filters.userId) {
          this.userDirectory = overview.distribution.map(item => ({ id: item.userId, name: item.userName }));
        }
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No se pudo consultar el resumen de usuarios y actividad.';
        this.cdr.markForCheck();
      }
    });
  }

  private loadTasks(page: number): void {
    if (!this.filters.userId) {
      this.tasks = null;
      this.loadingTasks = false;
      this.cdr.markForCheck();
      return;
    }
    this.loadingTasks = true;
    this.service.getUserTasks(this.filters, page).subscribe({
      next: tasks => {
        this.tasks = tasks;
        this.loadingTasks = false;
        this.cdr.markForCheck();
      },
      error: () => {
        this.loadingTasks = false;
        this.errorMessage = 'No se pudieron cargar las tareas del usuario seleccionado.';
        this.cdr.markForCheck();
      }
    });
  }

  private optionValue(item: UsersDashboardCatalogItem, ...keys: string[]): unknown {
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

  asNumber(value: unknown): number {
    const parsed = Number(value);
    return Number.isFinite(parsed) ? parsed : 0;
  }

  private defaultFilters(): UsersDashboardQuery {
    return { userId: '', workflowId: '', stepId: '', documentTypeId: '', from: '', to: '' };
  }
}

import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import {
    CopilotNegativeRun,
    CopilotNegativeRunsPage,
    CopilotRunDetail,
    CopilotTelemetryFilters,
    CopilotTelemetrySummary
} from './copilot-telemetry.models';

import { CopilotTelemetryService } from './copilot-telemetry.service';
import { forkJoin } from 'rxjs';

@Component({
    selector: 'app-copilot-telemetry',
    templateUrl: './copilot-telemetry.component.html',
    styleUrls: ['./copilot-telemetry.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class CopilotTelemetryComponent implements OnInit {
    filters: CopilotTelemetryFilters = this.defaultFilters();
    summary: CopilotTelemetrySummary | null = null;
    result: CopilotNegativeRunsPage | null = null;
    selected: CopilotRunDetail | null = null;
    selectedRunId: string | null = null;
    loading = false;
    detailLoading = false;
    errorMessage = '';
    readonly pageSize = 20;

    constructor(private readonly telemetry: CopilotTelemetryService, private readonly cdr: ChangeDetectorRef) { }

    ngOnInit(): void {
        this.loadDashboard();
    }

    applyFilters(): void {
        this.filters.page = 1;
        this.selected = null;
        this.selectedRunId = null;
        this.loadDashboard();
    }

    clearFilters(): void {
        this.filters = this.defaultFilters();
        this.applyFilters();
    }

    search(): void {
        this.applyFilters();
    }

    changePage(page: number): void {
        if (!this.result || page < 1 || page > this.pageCount) return;
        this.filters.page = page;
        this.loadList();
    }

    selectRun(run: CopilotNegativeRun): void {
        this.selectedRunId = run.runId;
        this.selected = null;
        this.detailLoading = true;
        this.cdr.markForCheck();
        this.telemetry.getRunDetail(run.runId).subscribe({
            next: detail => {
                this.selected = detail;
                this.detailLoading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.detailLoading = false;
                this.errorMessage = 'No se pudo cargar el análisis de esta ejecución.';
                this.cdr.markForCheck();
            }
        });
    }

    formatNumber(value: number | null | undefined, maximumFractionDigits = 0): string {
        if (value === null || value === undefined || !Number.isFinite(value)) return '—';
        return new Intl.NumberFormat('es-AR', { maximumFractionDigits }).format(value);
    }

    formatCost(value: number | null | undefined): string {
        if (value === null || value === undefined || !Number.isFinite(value)) return '—';
        return new Intl.NumberFormat('es-AR', { style: 'currency', currency: 'USD', maximumFractionDigits: 5 }).format(value);
    }

    formatDate(value: string | null | undefined): string {
        if (!value) return '—';
        return new Intl.DateTimeFormat('es-AR', { dateStyle: 'medium', timeStyle: 'short' }).format(new Date(value));
    }

    get pageCount(): number {
        return Math.max(1, Math.ceil((this.result?.totalRuns ?? 0) / this.pageSize));
    }

    get pageStart(): number {
        return this.result?.totalRuns ? (this.filters.page - 1) * this.pageSize + 1 : 0;
    }

    get pageEnd(): number {
        return Math.min(this.filters.page * this.pageSize, this.result?.totalRuns ?? 0);
    }

    trackRun(_: number, run: CopilotNegativeRun): string {
        return run.runId;
    }

    trackStep(_: number, step: CopilotRunDetail['steps'][number]): number {
        return step.id;
    }

    loadDashboard(): void {
        this.loading = true;
        this.errorMessage = '';
        const query = this.toQueryFilters();
        this.cdr.markForCheck();
        forkJoin({
            summary: this.telemetry.getSummary(query),
            runs: this.telemetry.getNegativeRuns(query)
        }).subscribe({
            next: ({ summary, runs }) => {
                this.summary = summary;
                this.result = runs;
                this.loading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.loading = false;
                this.errorMessage = 'No se pudieron cargar las estadísticas. Verificá la URL y disponibilidad del API.';
                this.cdr.markForCheck();
            }
        });
    }

    private loadList(): void {
        this.loading = true;
        this.errorMessage = '';
        this.cdr.markForCheck();
        this.telemetry.getNegativeRuns(this.toQueryFilters()).subscribe({
            next: result => {
                this.result = result;
                this.loading = false;
                this.cdr.markForCheck();
            },
            error: () => {
                this.loading = false;
                this.errorMessage = 'No se pudo actualizar la lista de respuestas.';
                this.cdr.markForCheck();
            }
        });
    }

    private toQueryFilters(): CopilotTelemetryFilters {
        return {
            ...this.filters,
            fromUtc: this.filters.fromUtc ? new Date(`${this.filters.fromUtc}T00:00:00`).toISOString() : '',
            toUtc: this.filters.toUtc
                ? new Date(new Date(`${this.filters.toUtc}T00:00:00`).getTime() + 86400000).toISOString()
                : '',
            pageSize: this.pageSize
        };
    }

    private defaultFilters(): CopilotTelemetryFilters {
        const today = new Date();
        const from = new Date(today.getFullYear(), today.getMonth(), today.getDate() - 29);
        const toDateInput = (date: Date): string => {
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        };
        return {
            fromUtc: toDateInput(from),
            toUtc: toDateInput(today),
            documentTypeCode: null,
            search: '',
            page: 1,
            pageSize: this.pageSize
        };
    }
}

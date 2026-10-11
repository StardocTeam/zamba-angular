import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CreateTraceSource, TraceRecord, TraceSource } from './trace.model';
import { TraceSearch, TraceService } from './trace.service';

@Component({
  selector: 'app-trace-dashboard',
  templateUrl: './trace-dashboard.component.html',
  styleUrls: ['./trace-dashboard.component.css']
})
export class TraceDashboardComponent implements OnInit {
  sources: TraceSource[] = [];
  traces: TraceRecord[] = [];
  selectedTrace: TraceRecord | null = null;
  previewLines: string[] = [];
  loadingSources = false;
  loadingTraces = false;
  loadingPreview = false;
  scanningSourceId: number | null = null;
  showSourceForm = false;
  total = 0;
  page = 1;
  pageSize = 25;
  search = '';
  filterApplication = '';
  filterEnvironment = '';
  filterUserId = '';
  filterFrom = '';
  filterTo = '';
  sourceApplication = '';
  sourceEnvironment = 'Development';
  sourceFolder = '';

  constructor(private readonly traceService: TraceService, private readonly message: NzMessageService) {}

  ngOnInit(): void {
    this.loadSources();
  }

  get applications(): string[] {
    return [...new Set(this.sources.map(source => source.applicationName))].sort();
  }

  get environments(): string[] {
    const matchingSources = this.filterApplication
      ? this.sources.filter(source => source.applicationName === this.filterApplication)
      : this.sources;
    return [...new Set(matchingSources.map(source => source.environment))].sort();
  }

  loadSources(): void {
    this.loadingSources = true;
    this.traceService.getSources().subscribe({
      next: sources => {
        this.sources = sources;
        this.loadingSources = false;
        this.loadTraces();
      },
      error: () => {
        this.loadingSources = false;
        this.message.error('Unable to load trace sources from Zamba.Api.');
      }
    });
  }

  loadTraces(): void {
    this.loadingTraces = true;
    this.traceService.searchTraces(this.buildSearch()).subscribe({
      next: result => {
        this.traces = result.items;
        this.total = result.totalCount;
        this.page = result.page;
        this.pageSize = result.pageSize;
        this.loadingTraces = false;
        if (this.selectedTrace && !this.traces.some(trace => trace.id === this.selectedTrace?.id)) {
          this.selectedTrace = null;
          this.previewLines = [];
        }
      },
      error: () => {
        this.loadingTraces = false;
        this.message.error('Unable to load traces from Zamba.Api.');
      }
    });
  }

  applyFilters(): void {
    this.page = 1;
    this.selectedTrace = null;
    this.previewLines = [];
    this.loadTraces();
  }

  changePage(page: number): void {
    this.page = page;
    this.loadTraces();
  }

  changePageSize(pageSize: number): void {
    this.pageSize = pageSize;
    this.page = 1;
    this.loadTraces();
  }

  selectTrace(trace: TraceRecord): void {
    this.selectedTrace = trace;
    this.previewLines = [];
    this.loadingPreview = true;
    this.traceService.getTraceContent(trace.id).subscribe({
      next: content => {
        this.previewLines = JSON.stringify(content, null, 2).split('\n');
        this.loadingPreview = false;
      },
      error: () => {
        this.loadingPreview = false;
        this.message.error('The trace file could not be loaded from its registered folder.');
      }
    });
  }

  copyPreview(): void {
    if (!this.previewLines.length) return;
    navigator.clipboard.writeText(this.previewLines.join('\n'))
      .then(() => this.message.success('Trace JSON copied.'))
      .catch(() => this.message.error('Clipboard access is unavailable in this browser context.'));
  }

  createSource(): void {
    const source: CreateTraceSource = {
      applicationName: this.sourceApplication.trim(),
      environment: this.sourceEnvironment.trim(),
      folderPath: this.sourceFolder.trim()
    };
    if (!source.applicationName || !source.environment || !source.folderPath) {
      this.message.warning('Enter an application, environment, and existing log folder.');
      return;
    }

    this.traceService.createSource(source).subscribe({
      next: created => {
        this.showSourceForm = false;
        this.sourceApplication = '';
        this.sourceFolder = '';
        this.message.success(`Source added for ${created.applicationName}. Scanning existing traces...`);
        this.loadSources();
        this.scanSource(created, false);
      },
      error: error => this.message.error(error?.error || 'Unable to register this trace source.')
    });
  }

  scanSource(source: TraceSource, announce = true): void {
    this.scanningSourceId = source.id;
    this.traceService.scanSource(source.id).subscribe({
      next: result => {
        this.scanningSourceId = null;
        if (announce) this.message.success(`${result.registered} new traces indexed from ${source.applicationName}.`);
        this.loadTraces();
      },
      error: error => {
        this.scanningSourceId = null;
        this.message.error(error?.error || `Unable to scan ${source.applicationName}.`);
      }
    });
  }

  clearFilters(): void {
    this.search = '';
    this.filterApplication = '';
    this.filterEnvironment = '';
    this.filterUserId = '';
    this.filterFrom = '';
    this.filterTo = '';
    this.applyFilters();
  }

  private buildSearch(): TraceSearch {
    return {
      applicationName: this.filterApplication || undefined,
      environment: this.filterEnvironment || undefined,
      userId: this.filterUserId.trim() || undefined,
      search: this.search.trim() || undefined,
      from: this.filterFrom ? new Date(`${this.filterFrom}T00:00:00`).toISOString() : undefined,
      to: this.filterTo ? new Date(`${this.filterTo}T23:59:59`).toISOString() : undefined,
      page: this.page,
      pageSize: this.pageSize
    };
  }
}

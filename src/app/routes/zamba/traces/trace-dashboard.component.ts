import { Component, OnInit } from '@angular/core';
import { CreateTraceSource, TraceRecord, TraceSource } from './trace.model';
import { TraceSearch, TraceService } from './trace.service';

import { NzMessageService } from 'ng-zorro-antd/message';

interface JsonTreeNode {
  key: string | null;
  kind: 'object' | 'array' | 'string' | 'number' | 'boolean' | 'null';
  value: unknown;
  children: JsonTreeNode[];
  expanded: boolean;
  hasNext: boolean;
}

@Component({
  selector: 'app-trace-dashboard',
  templateUrl: './trace-dashboard.component.html',
  styleUrls: ['./trace-dashboard.component.css']
})
export class TraceDashboardComponent implements OnInit {
  sources: TraceSource[] = [];
  traces: TraceRecord[] = [];
  selectedTrace: TraceRecord | null = null;
  previewRoot: JsonTreeNode | null = null;
  formattedJson = '';
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
          this.previewRoot = null;
          this.formattedJson = '';
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
    this.previewRoot = null;
    this.formattedJson = '';
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
    this.previewRoot = null;
    this.formattedJson = '';
    this.loadingPreview = true;
    this.traceService.getTraceContent(trace.id).subscribe({
      next: content => {
        this.formattedJson = JSON.stringify(content, null, 2) ?? 'null';
        this.previewRoot = this.buildJsonNode(content, null, false, 0);
        this.loadingPreview = false;
      },
      error: () => {
        this.loadingPreview = false;
        this.message.error('The trace file could not be loaded from its registered folder.');
      }
    });
  }

  copyPreview(): void {
    if (!this.formattedJson) return;
    navigator.clipboard.writeText(this.formattedJson)
      .then(() => this.message.success('Trace JSON copied.'))
      .catch(() => this.message.error('Clipboard access is unavailable in this browser context.'));
  }

  setTreeExpanded(expanded: boolean): void {
    if (this.previewRoot) this.updateTreeExpanded(this.previewRoot, expanded);
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

  private buildJsonNode(value: unknown, key: string | null, hasNext: boolean, depth: number): JsonTreeNode {
    if (Array.isArray(value)) {
      return {
        key,
        kind: 'array',
        value: null,
        children: value.map((child, index) => this.buildJsonNode(child, null, index < value.length - 1, depth + 1)),
        expanded: depth < 2,
        hasNext
      };
    }

    if (value !== null && typeof value === 'object') {
      const entries = Object.entries(value as Record<string, unknown>);
      return {
        key,
        kind: 'object',
        value: null,
        children: entries.map(([childKey, childValue], index) => this.buildJsonNode(childValue, childKey, index < entries.length - 1, depth + 1)),
        expanded: depth < 2,
        hasNext
      };
    }

    const valueType = typeof value;
    let kind: JsonTreeNode['kind'] = 'null';
    if (valueType === 'string' || valueType === 'number' || valueType === 'boolean') kind = valueType;
    return {
      key,
      kind,
      value,
      children: [],
      expanded: false,
      hasNext
    };
  }

  private updateTreeExpanded(node: JsonTreeNode, expanded: boolean): void {
    node.expanded = expanded;
    node.children.forEach(child => this.updateTreeExpanded(child, expanded));
  }
}

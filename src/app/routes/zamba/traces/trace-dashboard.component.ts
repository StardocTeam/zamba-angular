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

type PreviewKind = 'json' | 'text' | 'html' | null;
type HtmlPreviewMode = 'page' | 'source';

interface TextToken {
  text: string;
  kind: 'plain' | 'comment' | 'tag' | 'property' | 'string' | 'timestamp' | 'error' | 'warning' | 'info' | 'debug' | 'literal' | 'number' | 'punctuation';
}

interface HighlightedTextLine {
  tokens: TextToken[];
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
  previewKind: PreviewKind = null;
  previewText = '';
  textLines: HighlightedTextLine[] = [];
  htmlSrcdoc = '';
  htmlPreviewMode: HtmlPreviewMode = 'page';
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
          this.clearPreview();
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
    this.clearPreview();
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
    this.clearPreview();
    this.loadingPreview = true;
    const isTextFile = /\.txt$/i.test(trace.fileName);
    this.traceService.getTraceContent(trace.id, isTextFile).subscribe({
      next: content => {
        if (isTextFile) {
          this.showTextContent(content);
        } else {
          this.showJsonContent(content);
        }
        this.loadingPreview = false;
      },
      error: () => {
        this.loadingPreview = false;
        this.message.error('The trace file could not be loaded from its registered folder.');
      }
    });
  }

  copyPreview(): void {
    if (this.previewKind === null) return;
    const content = this.previewKind === 'json' ? this.formattedJson : this.previewText;
    navigator.clipboard.writeText(content)
      .then(() => this.message.success('Trace content copied.'))
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

  private showJsonContent(content: unknown): void {
    this.previewKind = 'json';
    this.formattedJson = JSON.stringify(content, null, 2) ?? 'null';
    this.previewRoot = this.buildJsonNode(content, null, false, 0);
  }

  private showTextContent(content: unknown): void {
    let text: string;
    if (typeof content === 'string') {
      text = content;
    } else {
      const serialized = JSON.stringify(content, null, 2);
      text = serialized ?? '';
    }
    this.previewText = text;

    if (this.isJsonDocument(text)) {
      this.showJsonContent(JSON.parse(text));
      return;
    }

    this.textLines = this.highlightText(text);
    if (this.isHtmlDocument(text)) {
      this.previewKind = 'html';
      this.htmlSrcdoc = this.createSandboxedHtml(text);
      this.htmlPreviewMode = 'page';
      return;
    }

    this.previewKind = 'text';
  }

  private isJsonDocument(text: string): boolean {
    const trimmed = text.trim();
    if (!trimmed.startsWith('{') && !trimmed.startsWith('[')) return false;
    try {
      JSON.parse(trimmed);
      return true;
    } catch {
      return false;
    }
  }

  private isHtmlDocument(text: string): boolean {
    return /^\s*(?:<!doctype\s+html\b|<html\b|<head\b|<body\b)/i.test(text);
  }

  private createSandboxedHtml(html: string): string {
    const parsed = new DOMParser().parseFromString(html, 'text/html');
    const policy = parsed.createElement('meta');
    policy.httpEquiv = 'Content-Security-Policy';
    policy.content = "default-src 'none'; img-src data: blob:; style-src 'unsafe-inline'; font-src data:;";
    parsed.head.prepend(policy);
    return parsed.documentElement.outerHTML;
  }

  private highlightText(text: string): HighlightedTextLine[] {
    return text.split('\n').map(line => ({
      tokens: this.highlightTextLine(line.endsWith('\r') ? line.slice(0, -1) : line)
    }));
  }

  private highlightTextLine(line: string): TextToken[] {
    const tokens: TextToken[] = [];
    let offset = 0;
    while (offset < line.length) {
      const token = this.readStructuredTextToken(line, offset);
      const end = token?.end ?? this.readPlainTextEnd(line, offset);
      tokens.push(token?.token ?? { text: line.slice(offset, end), kind: 'plain' });
      offset = end;
    }
    return tokens;
  }

  private readStructuredTextToken(line: string, offset: number): { token: TextToken; end: number } | null {
    return this.readMarkupToken(line, offset) ??
      this.readQuotedToken(line, offset) ??
      this.readTimestampToken(line, offset) ??
      this.readWordToken(line, offset) ??
      this.readNumberToken(line, offset) ??
      this.readPunctuationToken(line, offset);
  }

  private readMarkupToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const remaining = line.slice(offset);
    if (remaining.startsWith('<!--')) {
      const closingComment = line.indexOf('-->', offset + 4);
      const end = closingComment < 0 ? line.length : closingComment + 3;
      return { token: { text: line.slice(offset, end), kind: 'comment' }, end };
    }
    if (line.slice(offset, offset + 9).toLowerCase() === '<!doctype') {
      const closingTag = line.indexOf('>', offset);
      const end = closingTag < 0 ? line.length : closingTag + 1;
      return { token: { text: line.slice(offset, end), kind: 'tag' }, end };
    }

    const tagLength = this.readHtmlTagName(line, offset);
    if (tagLength === 0) return null;
    const end = offset + tagLength;
    return { token: { text: line.slice(offset, end), kind: 'tag' }, end };
  }

  private readQuotedToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const quote = line[offset];
    if (quote !== '"' && quote !== "'") return null;
    let end = offset + 1;
    while (end < line.length) {
      if (line[end] === '\\') {
        end += 2;
        continue;
      }
      if (line[end++] === quote) break;
    }
    const afterString = /^\s*:/.exec(line.slice(end));
    return {
      token: { text: line.slice(offset, end), kind: afterString ? 'property' : 'string' },
      end
    };
  }

  private readTimestampToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const match = /^\d{4}-\d{2}-\d{2}(?:T\d{2}:\d{2}:\d{2}(?:\.\d+)?Z?)?/.exec(line.slice(offset));
    if (!match) return null;
    const end = offset + match[0].length;
    return { token: { text: match[0], kind: 'timestamp' }, end };
  }

  private readWordToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const match = /^[A-Za-z_$][\w.$-]*/.exec(line.slice(offset));
    if (!match) return null;
    const end = offset + match[0].length;
    const kind = this.getWordTokenKind(match[0], line.slice(end));
    return { token: { text: match[0], kind }, end };
  }

  private readNumberToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const match = /^-?\d+(?:\.\d+)?(?:[eE][+-]?\d+)?/.exec(line.slice(offset));
    if (!match) return null;
    const end = offset + match[0].length;
    return { token: { text: match[0], kind: 'number' }, end };
  }

  private readPunctuationToken(line: string, offset: number): { token: TextToken; end: number } | null {
    const character = line[offset];
    if (!'{}[]()<>,:=/'.includes(character)) return null;
    return { token: { text: character, kind: 'punctuation' }, end: offset + 1 };
  }

  private readPlainTextEnd(line: string, offset: number): number {
    let end = offset + 1;
    while (end < line.length && !this.isTextTokenStart(line[end])) end++;
    return end;
  }

  private readHtmlTagName(line: string, offset: number): number {
    if (line[offset] !== '<') return 0;
    let nameStart = offset + 1;
    if (line[nameStart] === '/') nameStart++;
    if (!/[A-Za-z]/.test(line[nameStart] ?? '')) return 0;
    let end = nameStart + 1;
    while (end < line.length && /[\w:-]/.test(line[end])) end++;
    return end - offset;
  }

  private getWordTokenKind(word: string, remaining: string): TextToken['kind'] {
    if (/^(ERROR|FATAL)$/i.test(word)) return 'error';
    if (/^WARN(?:ING)?$/i.test(word)) return 'warning';
    if (/^INFO$/i.test(word)) return 'info';
    if (/^(TRACE|DEBUG)$/i.test(word)) return 'debug';
    if (/^(true|false|null|undefined)$/i.test(word)) return 'literal';
    if (/^\s*[:=]/.test(remaining)) return 'property';
    return 'plain';
  }

  private isTextTokenStart(character: string): boolean {
    return `<>'"{}[]()>,:=/`.includes(character) || /[A-Za-z0-9_$]/.test(character);
  }

  private clearPreview(): void {
    this.previewRoot = null;
    this.formattedJson = '';
    this.previewKind = null;
    this.previewText = '';
    this.textLines = [];
    this.htmlSrcdoc = '';
  }
}

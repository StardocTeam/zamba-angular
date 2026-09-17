import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, EventEmitter, Input, OnDestroy, Output, ViewChild, ViewEncapsulation } from '@angular/core';
import { field, GlobalSearchService, SearchRow } from './global-search.service';

@Component({
  selector: 'app-global-search-element',
  templateUrl: './global-search.component.html',
  styleUrls: ['./global-search.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.ShadowDom,
})
export class GlobalSearchElementComponent implements OnDestroy {
  @Input('entity-id') entityId?: number | string | null;
  @Input('index-ids') indexIds?: number | string | Array<number | string> | null;
  @Output() searchSubmitted = new EventEmitter<{ EntityId: string; IndexIds: string[]; value: string }>();
  @ViewChild('dialog', { static: true }) dialog!: ElementRef<HTMLDialogElement>;
  @ViewChild('modalInput', { static: true }) modalInput!: ElementRef<HTMLInputElement>;
  @ViewChild('searchInput', { static: true }) searchInput!: ElementRef<HTMLInputElement>;
  @ViewChild('results', { static: true }) results!: ElementRef<HTMLElement>;
  searchValue = '';
  rows: SearchRow[] = [];
  selected = -1;
  busy = false;
  opening = false;
  hasMore = false;
  status = 'Escribí al menos 3 caracteres y presioná Enter para buscar.';
  private page = 0;
  private query = '';
  private requestId = 0;
  private pendingWindow: Window | null = null;
  readonly field = field;
  constructor(private readonly service: GlobalSearchService, private readonly cdr: ChangeDetectorRef) {}

  openModal(): void {
    if (this.dialog.nativeElement.open) return;
    this.resetResults();
    this.dialog.nativeElement.showModal();
    this.modalInput.nativeElement.focus();
  }
  close(): void { this.dialog.nativeElement.close(); this.onClosed(); }
  onClosed(): void {
    ++this.requestId;
    this.busy = false;
    this.opening = false;
    this.pendingWindow?.close();
    this.pendingWindow = null;
    this.searchInput.nativeElement.focus();
  }
  ngOnDestroy(): void {
    ++this.requestId;
    this.pendingWindow?.close();
    this.dialog.nativeElement.close();
  }
  changeText(value: string): void { this.searchValue = value; this.resetResults(); }
  clear(inModal = false): void {
    this.changeText('');
    (inModal ? this.modalInput : this.searchInput).nativeElement.focus();
  }
  private resetResults(): void {
    ++this.requestId;
    this.busy = false;
    this.rows = [];
    this.selected = -1;
    this.hasMore = false;
    this.status = 'Escribí al menos 3 caracteres y presioná Enter para buscar.';
  }
  async search(append = false): Promise<void> {
    const text = this.searchValue.trim();
    if (this.busy || this.opening) return;
    if (text.length < 3) { this.status = 'Escribí al menos 3 caracteres para buscar.'; return; }
    if (append && text !== this.query) return;
    const current = ++this.requestId;
    const nextPage = append ? this.page + 1 : 0;
    this.query = text;
    this.busy = true;
    this.status = 'Buscando…';
    if (!append) {
      this.rows = []; this.selected = -1; this.hasMore = false;
      this.searchSubmitted.emit({ EntityId: String(this.entityId ?? ''),
        IndexIds: (Array.isArray(this.indexIds) ? this.indexIds : String(this.indexIds ?? '').split(',')).map(String).map(v => v.trim()).filter(Boolean), value: text });
    }
    try {
      const response = await this.service.search(text, nextPage);
      if (current !== this.requestId || !this.dialog.nativeElement.open) return;
      this.rows = append ? this.rows.concat(response.data) : response.data;
      this.page = nextPage;
      this.selected = -1;
      // The supplied controller returns the current page count in `total`.
      this.hasMore = response.data.length >= this.service.pageSize;
      this.status = this.rows.length ? `${this.rows.length} resultados` : 'No se encontraron resultados.';
    } catch {
      if (current === this.requestId) this.status = 'No se pudo realizar la búsqueda. Verificá la sesión e intentá nuevamente.';
    } finally {
      if (current === this.requestId) { this.busy = false; this.cdr.markForCheck(); }
    }
  }
  onKeydown(event: KeyboardEvent): void {
    if (event.isComposing) return;
    if (event.key === 'ArrowDown' || event.key === 'ArrowUp') {
      event.preventDefault();
      if (this.rows.length) this.select(this.selected < 0 ? (event.key === 'ArrowDown' ? 0 : this.rows.length - 1)
        : (this.selected + (event.key === 'ArrowDown' ? 1 : -1) + this.rows.length) % this.rows.length);
    } else if (event.key === 'Enter') {
      event.preventDefault();
      if (this.selected >= 0) void this.openResult(this.selected); else void this.search();
    }
  }
  select(index: number): void {
    this.selected = index;
    this.results.nativeElement.children[index]?.scrollIntoView({ block: 'nearest' });
    this.modalInput.nativeElement.focus();
  }
  async openResult(index: number): Promise<void> {
    if (this.busy || this.opening || !this.rows[index] || this.searchValue.trim() !== this.query) return;
    // Reserve a window during the user gesture, before the asynchronous rights check.
    const popup = window.open('about:blank', '_blank');
    if (!popup) { this.status = 'Permití las ventanas emergentes para abrir el resultado.'; return; }
    popup.opener = null;
    this.pendingWindow = popup;
    this.opening = true;
    const current = this.requestId;
    try {
      const url = await this.service.resultUrl(this.rows[index]);
      if (current !== this.requestId || !this.dialog.nativeElement.open) { popup.close(); return; }
      popup.location.replace(url);
      this.pendingWindow = null;
      this.close();
    } catch {
      popup.close();
      if (current === this.requestId) this.status = 'No se pudo abrir el resultado. Intentá nuevamente.';
    } finally { this.pendingWindow = null; this.opening = false; this.cdr.markForCheck(); }
  }
  backdrop(event: MouseEvent): void {
    const rect = this.dialog.nativeElement.getBoundingClientRect();
    if (event.target === this.dialog.nativeElement && (event.clientX < rect.left || event.clientX > rect.right || event.clientY < rect.top || event.clientY > rect.bottom)) this.close();
  }
  date(value: string): string {
    if (!value) return '—';
    const dotnet = /^\/Date\((-?\d+)/.exec(value);
    const date = new Date(dotnet ? Number(dotnet[1]) : value);
    if (Number.isNaN(date.getTime())) return '—';
    const pad = (n: number) => String(n).padStart(2, '0');
    return `${pad(date.getDate())}/${pad(date.getMonth() + 1)}/${date.getFullYear()} ${pad(date.getHours())}:${pad(date.getMinutes())}`;
  }
  matches(row: SearchRow): Array<{ text: string; match: boolean }> {
    const normalize = (s: string) => s.toLowerCase().normalize('NFD').replace(/[\u0300-\u036f]/g, '');
    const terms = this.query.split(/\s+/).filter(Boolean).map(normalize);
    return (field(row, ['ZAMBA_WORD']) || '—').split(',').flatMap((word, index) => {
      const leading = word.match(/^\s*/)?.[0] || '';
      const clean = word.slice(leading.length);
      const length = Math.max(0, ...terms.filter(t => normalize(clean).startsWith(t)).map(t => t.length));
      return [{ text: (index ? ',' : '') + leading, match: false }, { text: clean.slice(0, length), match: true }, { text: clean.slice(length), match: false }];
    });
  }
}

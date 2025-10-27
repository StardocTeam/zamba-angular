import { AfterViewInit, ChangeDetectorRef, Component, Inject, Input, OnDestroy } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { BehaviorSubject, Subscription, catchError } from 'rxjs';

import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';
import { ChecklistItem } from './checklist-item';

@Component({
  selector: 'app-checklist-component',
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.scss']
})
export class ChecklistComponent implements OnDestroy, AfterViewInit {

  @Input() reportId: number | undefined;
  @Input() taskId: number | undefined;
  @Input() userId: number | undefined;
  @Input() token: string | undefined;
  @Input() refresh$?: BehaviorSubject<any> = new BehaviorSubject<any>(undefined);
  private refreshSub?: Subscription;
  /**
   * Width can be a number (px) or a CSS string (%, px, etc). Defaults to '100%'.
   */
  @Input() width: string | number = '100%';

  /**
   * Height for the list area. Can be number (px) or CSS string. Defaults to 260px.
   */
  @Input() height: string | number = '260px';

  // Use BehaviorSubject seeded with an empty array so late subscribers (the template async pipe)
  // always receive the most recent value. This fixes the case where the first emission
  // happens before the template subscribes and nothing is rendered.
  items: BehaviorSubject<ChecklistItem[]> = new BehaviorSubject<ChecklistItem[]>([]);

  constructor(
    private RVService: ReportViewerService | null,
    @Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService,
    private cdr: ChangeDetectorRef,
  ) {



    // runtime debug logging to help locate missing providers when used as a webcomponent
    if (!this.RVService) {
      const missing: string[] = [];
      if (!this.RVService) missing.push('ReportViewerService');
      setTimeout(() => console.info(`[zamba-checklist] missing providers: ${missing.join(', ') || 'none'}`), 0);
    }

    // Listen for a CustomEvent "zamba-checklist-refresh" so host pages can trigger reload without recreating element
    try {
      const el = (this as any).hostElement || null;
      const target = el || document;
      const handler = () => this.loadData();
      target.addEventListener && target.addEventListener('zamba-checklist-refresh', handler as EventListener);
    } catch (e) {
      // ignore
    }
  }
  ngAfterViewInit(): void {

    // Subscribe to external refresh trigger if provided
    if (this.refresh$) {
      this.refreshSub = this.refresh$.subscribe(() => {
        this.loadData();
      });
    }
  }

  get cardStyle() {
    return { width: this.toCss(this.width) };
  }

  get listStyle() {
    return { height: this.toCss(this.height) };
  }

  private toCss(value: string | number) {
    if (value == null) return '';
    return typeof value === 'number' ? `${value}px` : value;
  }

  public loadData() {
    this.tokenService.set({ token: this.token });

    if (this.RVService == null) {
      return;
    }

    if (this.userId && this.reportId && this.taskId) {
      let genericRequest = {
        UserId: this.userId,
        Params: {
          Zvars: JSON.stringify({
            taskId: this.taskId,
          }),
          Id: this.reportId
        }
      };

      this.GetResultsByReportId(genericRequest);
    }
  }

  toggle(item: ChecklistItem) {
    item.done = !item.done;
  }


  trackById(_index: number, item: ChecklistItem) {
    return item.id;
  }

  private GetResultsByReportId(genericRequest: {}) {
    if (!this.RVService) return;

    this.RVService.GetResultsByReportId(genericRequest).pipe(
      catchError(error => {
        console.error('Error al obtener datos:', error);
        throw error;
      })
    )
      .subscribe((data: any) => {
        this.SetData(data);
        return;
      });
  }

  private SetData(data: any) {
    try {
      const parsed = typeof data === 'string' ? JSON.parse(data) : data;
      const rows = parsed?.RowHashtable || [];
      const mapped = (rows || []).map((item: any) => ({
        id: item?.ID,
        title: item?.Title,
        done: item?.Done
      })) as ChecklistItem[];
      this.items.next(mapped);
      // Ensure change detection runs for hosting pages which may not be
      // running Angular's normal change detection cycle (webcomponent hosts).
      try {
        this.cdr.detectChanges();
      } catch (e) {
        // ignore if detectChanges is not allowed in this context
      }
    } catch (err) {
      console.error('[zamba-checklist] SetData parse error', err, data);
      this.items.next([]);
      try {
        this.cdr.detectChanges();
      } catch (e) {
        // ignore
      }
    }
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
  }
}

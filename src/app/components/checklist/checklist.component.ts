import { Component, Inject, Input, OnDestroy, OnInit } from '@angular/core';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { catchError, Observable, Subscription } from 'rxjs';
import { ReportViewerService } from 'src/app/routes/widgets/report-viewer/service/report-viewer.service';

@Component({
  selector: 'app-checklist-component',
  standalone: true,
  imports: [CommonModule, FormsModule, NzIconModule],
  templateUrl: './checklist.component.html',
  styleUrls: ['./checklist.component.scss']
})
export class ChecklistComponent implements OnInit, OnDestroy {

  @Input() reportId: number | undefined;
  @Input() taskId: number | undefined;
  @Input() refresh$?: Observable<any>;
  private refreshSub?: Subscription;
  /**
   * Width can be a number (px) or a CSS string (%, px, etc). Defaults to '100%'.
   */
  @Input() width: string | number = '100%';

  /**
   * Height for the list area. Can be number (px) or CSS string. Defaults to 260px.
   */
  @Input() height: string | number = '260px';

  constructor(@Inject(DA_SERVICE_TOKEN) private tokenService: ITokenService, private RVService: ReportViewerService) {
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

  ngOnInit(): void {

    this.loadData();

    // Subscribe to external refresh trigger if provided
    if (this.refresh$) {
      this.refreshSub = this.refresh$.subscribe(() => {
        this.loadData();
      });
    }

  }

  private loadData() {
    const tokenData = this.tokenService.get();
    if (tokenData && this.reportId && this.taskId) {
      let genericRequest = {
        UserId: tokenData['userid'],
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
  items = [
    { id: 1, title: 'Prepare report skeleton', done: true },
    { id: 2, title: 'Fetch data from API', done: false },
    { id: 3, title: 'Build charts', done: false },
    { id: 4, title: 'Write unit tests', done: true }
  ];


  toggle(item: { id: number; done: boolean }) {
    item.done = !item.done;
  }


  trackById(_index: number, item: { id: number }) {
    return item.id;
  }

  private GetResultsByReportId(genericRequest: {}) {
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
    this.items = data.map((item: any) => ({
      id: item.id,
      title: item.title,
      done: item.done
    }));
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
  }
}

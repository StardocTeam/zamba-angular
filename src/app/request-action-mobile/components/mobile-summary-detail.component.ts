import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RequestActionTask } from '../request-action-mobile.model';

@Component({ selector: 'app-mobile-summary-detail', templateUrl: './mobile-summary-detail.component.html', styleUrls: ['./mobile-summary-detail.component.less'] })
export class MobileSummaryDetailComponent {
  @Input() summary: RequestActionTask | null = null;
  @Input() busy = false;
  @Output() approve = new EventEmitter<RequestActionTask>();
  @Output() return = new EventEmitter<RequestActionTask>();
  @Output() addComment = new EventEmitter<{ task: RequestActionTask; comment: string }>();

  comment = '';

  get sections(): Array<{ title: string; value: unknown }> {
    if (!this.summary) return [];
    return [
      { title: 'Responsabilidad', value: this.summary.RESPONSABILIDAD },
      { title: 'Mecánica del hecho', value: this.summary.MECANICA_DEL_ACCIDENTE },
      { title: 'Víctimas', value: this.summary.VICTIMAS },
      { title: 'Reclamante', value: this.summary.RECLAMANTE },
      { title: 'Negociación', value: this.summary.NEGOCIACION },
      { title: 'Observaciones', value: this.summary.OBSERVACIONES },
    ].filter(section => !!section.value);
  }

  submitComment(): void {
    if (this.summary && this.comment.trim()) {
      this.addComment.emit({ task: this.summary, comment: this.comment.trim() });
      this.comment = '';
    }
  }
}

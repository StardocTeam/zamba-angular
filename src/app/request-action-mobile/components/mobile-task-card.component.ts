import { Component, EventEmitter, HostListener, Input, OnDestroy, Output } from '@angular/core';
import { RequestActionQueue, RequestActionTask, TaskCardAction } from '../request-action-mobile.model';

@Component({ selector: 'app-mobile-task-card', templateUrl: './mobile-task-card.component.html', styleUrls: ['./mobile-task-card.component.less'] })
export class MobileTaskCardComponent implements OnDestroy {
  @Input() task!: RequestActionTask;
  @Input() queue: RequestActionQueue = 'approval';
  @Input() saved = false;
  @Input() selected = false;
  @Output() action = new EventEmitter<{ action: TaskCardAction; task: RequestActionTask }>();

  expanded = false;
  fullscreenPreview = false;
  private thumbnailClickTimer: ReturnType<typeof setTimeout> | null = null;

  get title(): string { return String(this.task.PROVEEDOR || this.task.TITLE || `Documento ${this.task.ID ?? this.task.DOC_ID ?? ''}`); }
  get taskId(): string { return String(this.task.ID ?? this.task.DOC_ID ?? ''); }
  get thumbnailUrl(): string | null {
    const fields = new Set(['THUMB', 'THUMB_URL', 'THUMBNAIL', 'THUMBNAIL_URL', 'THUMBNAIL_BASE64', 'THUMB_BASE64', 'PREVIEW', 'PREVIEW_URL', 'PREVIEW_BASE64', 'IMAGE', 'IMAGE_URL', 'IMAGE_BASE64', 'IMG', 'IMG_URL', 'FOTO', 'FOTO_URL']);
    const value = Object.entries(this.task).find(([key, candidate]) => fields.has(key.toUpperCase()) && typeof candidate === 'string')?.[1];
    if (typeof value !== 'string' || !value.trim()) return null;
    const source = value.trim();
    if (/^(data:image\/|https?:\/\/|blob:|\/|\.\/|\.\.\/)/i.test(source)) return source;
    return /^[A-Za-z0-9+/]+={0,2}$/.test(source) && source.length > 80 ? `data:image/jpeg;base64,${source}` : source;
  }
  handleThumbnailClick(): void {
    if (this.thumbnailClickTimer) clearTimeout(this.thumbnailClickTimer);
    this.thumbnailClickTimer = setTimeout(() => {
      this.fullscreenPreview = true;
      this.thumbnailClickTimer = null;
    }, 500);
  }
  handleThumbnailDoubleClick(): void {
    if (this.thumbnailClickTimer) clearTimeout(this.thumbnailClickTimer);
    this.thumbnailClickTimer = null;
    this.emit('open');
  }
  closePreview(): void { this.fullscreenPreview = false; }
  @HostListener('document:keydown.escape')
  onEscape(): void { this.closePreview(); }
  ngOnDestroy(): void {
    if (this.thumbnailClickTimer) clearTimeout(this.thumbnailClickTimer);
  }
  emit(action: TaskCardAction): void { this.action.emit({ action, task: this.task }); }
}

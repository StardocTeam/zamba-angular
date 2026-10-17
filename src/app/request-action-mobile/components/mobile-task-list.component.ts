import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { RequestActionQueue, RequestActionTask, TaskCardAction } from '../request-action-mobile.model';

@Component({ selector: 'app-mobile-task-list', templateUrl: './mobile-task-list.component.html', styleUrls: ['./mobile-task-list.component.less'] })
export class MobileTaskListComponent implements OnChanges {
  @Input() queue: RequestActionQueue = 'approval';
  @Input() queueLabel = '';
  @Input() tasks: RequestActionTask[] = [];
  @Input() tab: 'pending' | 'saved' = 'pending';
  @Input() pendingCount = 0;
  @Input() savedCount = 0;
  @Input() busy = false;
  @Input() errorMessage = '';
  @Output() tabChange = new EventEmitter<'pending' | 'saved'>();
  @Output() taskAction = new EventEmitter<{ action: TaskCardAction; task: RequestActionTask }>();
  @Output() bulkApprove = new EventEmitter<RequestActionTask[]>();

  selectedKeys = new Set<string>();

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['queue'] || changes['tab'] || changes['tasks']) this.selectedKeys = new Set<string>();
  }

  get selectedTasks(): RequestActionTask[] { return this.tasks.filter(task => this.selectedKeys.has(this.taskKey(task))); }

  onTaskAction(event: { action: TaskCardAction; task: RequestActionTask }): void {
    if (event.action === 'detail' && this.queue !== 'summary') {
      const key = this.taskKey(event.task);
      if (this.selectedKeys.has(key)) this.selectedKeys.delete(key);
      else this.selectedKeys.add(key);
      this.selectedKeys = new Set(this.selectedKeys);
      return;
    }
    this.taskAction.emit(event);
  }

  approveBatch(): void { this.bulkApprove.emit(this.selectedTasks.length ? this.selectedTasks : this.tasks); }
  trackTask = (_index: number, task: RequestActionTask): string => this.taskKey(task);
  taskKey(task: RequestActionTask): string { return String(task.DOC_ID ?? `${task.ID ?? ''}-${task.ID1 ?? ''}`); }
}

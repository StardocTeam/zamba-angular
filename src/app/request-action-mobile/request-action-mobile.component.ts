import { Component, OnInit } from '@angular/core';
import { DashboardCounts, MobileUser, RequestActionQueue, RequestActionTask, TaskCardAction } from './request-action-mobile.model';

import { MobileRequestActionService } from './request-action-mobile.service';
import { environment } from '@env/environment';

@Component({
  selector: 'app-request-action-mobile',
  templateUrl: './request-action-mobile.component.html',
  styleUrls: ['./request-action-mobile.component.less'],
})
export class RequestActionMobileComponent implements OnInit {
  user: MobileUser | null = null;
  screen: 'home' | 'queue' | 'summary' | 'profile' = 'home';
  queue: RequestActionQueue = 'approval';
  queueTab: 'pending' | 'saved' = 'pending';
  tasks: RequestActionTask[] = [];
  counts: DashboardCounts = { approval: 0, confirmation: 0, summary: 0 };
  readonly queueTypes: RequestActionQueue[] = ['approval', 'confirmation', 'summary'];
  savedCounts: Record<RequestActionQueue, number> = { approval: 0, confirmation: 0, summary: 0 };
  selectedSummary: RequestActionTask | null = null;
  loading = false;
  errorMessage = '';
  actionDialog: { action: 'approve' | 'reject'; tasks: RequestActionTask[] } | null = null;
  actionComment = '';
  actionMessage = '';
  constructor(readonly requestService: MobileRequestActionService) {}

  ngOnInit(): void {
    const savedUser = localStorage.getItem('requestActionMobile.user');
    if (!savedUser) return;
    try {
      this.user = JSON.parse(savedUser) as MobileUser;
      this.refreshSavedCounts();
      this.loadCounts();
    } catch {
      localStorage.removeItem('requestActionMobile.user');
    }
  }

  signIn(credentials: { username: string; password: string }): void {
    this.loading = true;
    this.errorMessage = '';
    this.requestService.login(credentials.username, credentials.password).subscribe({
      next: user => {
        this.user = user;
        localStorage.setItem('requestActionMobile.user', JSON.stringify(user));
        this.refreshSavedCounts();
        this.loading = false;
        this.loadCounts();
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No pudimos iniciar sesión. Revisa tus datos e inténtalo nuevamente.';
      },
    });
  }

  openQueue(queue: RequestActionQueue, tab: 'pending' | 'saved' = 'pending'): void {
    this.queue = queue;
    this.queueTab = tab;
    this.screen = 'queue';
    this.selectedSummary = null;
    if (tab === 'saved') {
      this.tasks = this.getSavedTasks(queue);
      return;
    }
    this.tasks = [];
    this.loadQueue();
  }

  changeQueueTab(tab: 'pending' | 'saved'): void {
    this.openQueue(this.queue, tab);
  }

  showHome(): void {
    this.screen = 'home';
    this.selectedSummary = null;
    this.loadCounts();
  }

  showProfile(): void {
    this.screen = 'profile';
    this.errorMessage = '';
  }

  refresh(): void {
    if (this.screen === 'queue' && this.queueTab === 'pending') this.loadQueue();
    else this.loadCounts();
  }

  handleTaskAction(event: { action: TaskCardAction; task: RequestActionTask }): void {
    if (event.action === 'save') this.saveTask(event.task);
    else if (event.action === 'restore') this.restoreTask(event.task);
    else if (event.action === 'open') this.openDocument(event.task);
    else if (event.action === 'detail') {
      this.selectedSummary = event.task;
      this.screen = 'summary';
    } else this.openActionDialog(event.action, [event.task]);
  }

  openBulkApproval(tasks: RequestActionTask[]): void {
    if (tasks.length) this.openActionDialog('approve', tasks);
  }

  addSummaryComment(event: { task: RequestActionTask; comment: string }): void {
    if (!this.user || !event.comment.trim()) return;
    this.loading = true;
    this.errorMessage = '';
    this.requestService.addSummaryComment(this.user.id, event.task, event.comment.trim()).subscribe({
      next: () => {
        this.loading = false;
        this.screen = 'queue';
        this.selectedSummary = null;
        this.loadQueue();
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No se pudo agregar el comentario.';
      },
    });
  }

  openActionDialog(action: 'approve' | 'reject', tasks: RequestActionTask[]): void {
    this.actionDialog = { action, tasks };
    this.actionComment = '';
    this.actionMessage = '';
  }

  submitAction(): void {
    if (!this.user || !this.actionDialog) return;
    const { action, tasks } = this.actionDialog;
    const comment = this.actionComment.trim();
    if ((action === 'reject' || this.queue === 'summary') && !comment) {
      this.actionMessage = this.queue === 'summary' ? 'Agrega un comentario para continuar.' : 'Indica el motivo del rechazo.';
      return;
    }
    const formVariable = action === 'reject'
      ? [{ name: this.queue === 'summary' ? 'ObsAprob' : 'motivo', value: comment }]
      : this.queue === 'summary' ? [{ name: 'ObsAprob', value: comment }] : undefined;
    const ruleId = this.requestService.getRuleId(this.queue, action);
    this.loading = true;
    this.requestService.executeTasks(this.user.id, ruleId, tasks, formVariable ? JSON.stringify(formVariable) : undefined).subscribe({
      next: () => {
        const completed = new Set(tasks.map(task => this.taskKey(task)));
        this.tasks = this.tasks.filter(task => !completed.has(this.taskKey(task)));
        this.removeSavedTasks(completed);
        this.actionDialog = null;
        this.actionComment = '';
        this.loading = false;
        this.loadCounts();
        if (this.screen === 'summary') {
          this.screen = 'queue';
          this.selectedSummary = null;
          this.queueTab = 'pending';
          this.loadQueue();
        }
      },
      error: () => {
        this.loading = false;
        this.actionMessage = 'No se pudo completar la acción. Inténtalo nuevamente.';
      },
    });
  }

  savePassword(passwords: { currentPassword: string; newPassword: string; confirmation: string }): void {
    if (!this.user) return;
    if (passwords.newPassword !== passwords.confirmation) {
      this.errorMessage = 'Las contraseñas nuevas no coinciden.';
      return;
    }
    this.loading = true;
    this.errorMessage = '';
    this.requestService.changePassword(this.user.id, passwords).subscribe({
      next: () => {
        this.loading = false;
        this.errorMessage = 'Contraseña actualizada.';
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No se pudo actualizar la contraseña.';
      },
    });
  }

  signOut(): void {
    localStorage.removeItem('requestActionMobile.user');
    this.user = null;
    this.tasks = [];
    this.screen = 'home';
    this.errorMessage = '';
    this.refreshSavedCounts();
  }

  get totalPendingCount(): number {
    const total = this.queueTypes.reduce((sum, queue) => sum + this.counts[queue], 0);
    return Math.max(0, total - this.totalSavedCount);
  }

  get totalSavedCount(): number {
    return this.queueTypes.reduce((sum, queue) => sum + this.savedCounts[queue], 0);
  }

  pendingCountForQueue(queue: RequestActionQueue): number {
    if (this.screen === 'queue' && this.queue === queue && this.queueTab === 'pending' && !this.loading) {
      return this.tasks.length;
    }
    return Math.max(0, this.counts[queue] - this.savedCounts[queue]);
  }

  get screenTitle(): string {
    // if (this.screen === 'queue') return this.queue === 'approval' ? 'Aprobaciones' : this.queue === 'confirmation' ? 'Conformaciones' : 'Resumen gerencial';
    // if (this.screen === 'summary') return 'Resumen gerencial';
    // if (this.screen === 'profile') return 'Mi perfil';
    return 'Mis tareas';
  }

  get queueLabel(): string {
    return this.queue === 'approval' ? 'Aprobaciones' : this.queue === 'confirmation' ? 'Conformaciones' : 'Resumen gerencial';
  }

  private loadCounts(): void {
    if (!this.user) return;
    this.requestService.getDashboardCounts(this.user.id).subscribe({
      next: counts => this.counts = counts,
      error: () => this.errorMessage = 'No se pudieron actualizar tus tareas.',
    });
  }

  private loadQueue(): void {
    if (!this.user) return;
    this.loading = true;
    this.errorMessage = '';
    this.requestService.getTasks(this.user.id, this.queue).subscribe({
      next: tasks => {
        const savedKeys = new Set(this.getSavedTasks(this.queue).map(task => this.taskKey(task)));
        this.tasks = tasks.filter(task => !savedKeys.has(this.taskKey(task)));
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'No se pudo cargar la bandeja. Intenta actualizar.';
      },
    });
  }

  private saveTask(task: RequestActionTask): void {
    const saved = this.getSavedTasks(this.queue);
    if (!saved.some(item => this.taskKey(item) === this.taskKey(task))) {
      saved.unshift(task);
      this.setSavedTasks(this.queue, saved);
    }
    this.tasks = this.tasks.filter(item => this.taskKey(item) !== this.taskKey(task));
  }

  private restoreTask(task: RequestActionTask): void {
    this.setSavedTasks(this.queue, this.getSavedTasks(this.queue).filter(item => this.taskKey(item) !== this.taskKey(task)));
    this.tasks = this.tasks.filter(item => this.taskKey(item) !== this.taskKey(task));
  }

  private removeSavedTasks(completed: Set<string>): void {
    (['approval', 'confirmation', 'summary'] as RequestActionQueue[]).forEach(queue => {
      this.setSavedTasks(queue, this.getSavedTasks(queue).filter(task => !completed.has(this.taskKey(task))));
    });
  }

  private getSavedTasks(queue: RequestActionQueue): RequestActionTask[] {
    if (!this.user) return [];
    try {
      return JSON.parse(localStorage.getItem(this.savedKey(queue)) || '[]') as RequestActionTask[];
    } catch {
      return [];
    }
  }

  private setSavedTasks(queue: RequestActionQueue, tasks: RequestActionTask[]): void {
    if (!this.user) return;
    localStorage.setItem(this.savedKey(queue), JSON.stringify(tasks));
    this.savedCounts[queue] = tasks.length;
  }

  private refreshSavedCounts(): void {
    this.queueTypes.forEach(queue => this.savedCounts[queue] = this.getSavedTasks(queue).length);
  }

  private savedKey(queue: RequestActionQueue): string {
    return `requestActionMobile.saved.${this.user?.id}.${queue}`;
  }

  private taskKey(task: RequestActionTask): string {
    return String(task.DOC_ID ?? `${task.ID ?? ''}-${task.ID1 ?? ''}`);
  }

  private openDocument(task: RequestActionTask): void {
    if (!this.user) return;
    const baseUrl = environment['zambaWeb'] || window.location.origin;
    const documentType = task.EID ?? task.DOC_TYPE_ID ?? '';
    const documentId = task.ID1 ?? task.DOC_ID ?? '';
    const taskId = task.TID ?? '';
    const url = `${String(baseUrl).replace(/\/$/, '')}/views/WF/TaskViewer?DocType=${encodeURIComponent(documentType)}&docid=${encodeURIComponent(documentId)}&taskid=${encodeURIComponent(taskId)}&mode=s&s=13&user=${encodeURIComponent(this.user.id)}#Zamba/`;
    window.open(url, '_blank');
  }
}

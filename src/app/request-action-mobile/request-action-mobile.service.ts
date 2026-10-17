import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { forkJoin, map, Observable, of, switchMap, timer } from 'rxjs';

import { environment } from '@env/environment';
import { DashboardCounts, MobileUser, RequestActionQueue, RequestActionTask } from './request-action-mobile.model';
import { REQUEST_ACTION_REPORTS, REQUEST_ACTION_RULES, SUMMARY_COMMENT_RULE, USE_MOCK_DATA } from './request-action-mobile.config';
import { MOCK_TASKS, MOCK_USER } from './request-action-mobile.mock';

@Injectable({ providedIn: 'root' })
export class MobileRequestActionService {
  readonly useMockData = USE_MOCK_DATA;
  private readonly apiBase = String(environment['restApi'] || '').replace(/\/$/, '');
  private readonly headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  private readonly mockTasks = new Map<number, RequestActionTask[]>(
    Object.entries(MOCK_TASKS).map(([reportId, tasks]) => [Number(reportId), tasks.map(task => ({ ...task }))]),
  );

  constructor(private readonly http: HttpClient) {}

  login(username: string, password: string): Observable<MobileUser> {
    if (this.useMockData) {
      if (!username.trim() || !password) return of(null as unknown as MobileUser);
      return timer(350).pipe(map(() => ({ ...MOCK_USER, username: username.trim() })));
    }
    const userName = encodeURIComponent(username.trim());
    return this.http.get<any>(`${this.apiBase}/search/GetUserInfoForName?UserName=${userName}`).pipe(
      map(response => this.extractUserId(response)),
      switchMap(id => this.http.post<any>(`${this.apiBase}/ExternalSearch/Login`, {
        UserName: username.trim(), Password: password, ComputerNameOrIp: '127.1.1.5', name: '', lastName: '', eMail: '', type: '',
      }, { headers: this.headers }).pipe(map(result => {
        if (!result?.token) throw new Error('Invalid login');
        return { id, username: username.trim(), token: result.token };
      }))),
    );
  }

  getTasks(userId: number, queue: RequestActionQueue): Observable<RequestActionTask[]> {
    const reportId = REQUEST_ACTION_REPORTS[queue];
    if (this.useMockData) return timer(220).pipe(map(() => (this.mockTasks.get(reportId) || []).map(task => ({ ...task }))));
    return this.http.get<any>(`${this.apiBase}/rp/${reportId}-${encodeURIComponent(userId)}`).pipe(map(response => this.parseTasks(response)));
  }

  getDashboardCounts(userId: number): Observable<DashboardCounts> {
    return forkJoin({
      approval: this.getTasks(userId, 'approval'),
      confirmation: this.getTasks(userId, 'confirmation'),
      summary: this.getTasks(userId, 'summary'),
    }).pipe(map(result => ({ approval: result.approval.length, confirmation: result.confirmation.length, summary: result.summary.length })));
  }

  getRuleId(queue: RequestActionQueue, action: 'approve' | 'reject'): number {
    return REQUEST_ACTION_RULES[queue][action];
  }

  executeTasks(userId: number, ruleId: number, tasks: RequestActionTask[], formVariables?: string): Observable<any[]> {
    if (this.useMockData) {
      return timer(500).pipe(map(() => {
        const completed = new Set(tasks.map(task => this.taskKey(task)));
        this.mockTasks.forEach(list => {
          const remaining = list.filter(task => !completed.has(this.taskKey(task)));
          list.splice(0, list.length, ...remaining);
        });
        return tasks.map(() => ({ executionResult: 1, Vars: { error: '' } }));
      }));
    }
    return forkJoin(tasks.map(task => this.http.post<any>(`${this.apiBase}/Tasks/ExecuteTaskRule`, {
      userId: Number(userId),
      Params: {
        ruleId: Number(ruleId),
        resultIds: Number(task.ID1 ?? task.DOC_ID ?? task.ID),
        ...(formVariables ? { FormVariables: formVariables } : {}),
      },
    }, { headers: this.headers })));
  }

  changePassword(userId: number, passwords: { currentPassword: string; newPassword: string; confirmation: string }): Observable<any> {
    if (this.useMockData) return timer(350).pipe(map(() => ({ message: 'Contraseña actualizada' })));
    return this.http.post(`${this.apiBase}/Account/Password`, {
      Userid: Number(userId), CurrentPassword: passwords.currentPassword, NewPassword: passwords.newPassword, NewPassword2: passwords.confirmation,
    }, { headers: this.headers });
  }

  addSummaryComment(userId: number, task: RequestActionTask, comment: string): Observable<any> {
    if (this.useMockData) return timer(400).pipe(map(() => ({ executionResult: 1, Vars: { error: '' } })));
    return this.http.post(`${this.apiBase}/Tasks/ExecuteTaskRule`, {
      userId: Number(userId),
      Params: {
        ruleId: SUMMARY_COMMENT_RULE,
        resultIds: Number(task.DOC_ID ?? task.ID1 ?? task.ID),
        FormVariables: JSON.stringify([{ name: 'ObsAprob', value: comment }]),
      },
    }, { headers: this.headers });
  }

  private extractUserId(response: any): number {
    const value = typeof response === 'string' ? this.parseMaybeJson(response) : response;
    const candidate = Array.isArray(value) ? value[0] : value?.userid ?? value?.userId ?? value?.ID ?? value;
    const id = Number(candidate);
    if (!Number.isFinite(id)) throw new Error('User not found');
    return id;
  }

  private parseTasks(response: any): RequestActionTask[] {
    const value = typeof response === 'string' ? this.parseMaybeJson(response) : response;
    return Array.isArray(value) ? value as RequestActionTask[] : [];
  }

  private parseMaybeJson(value: string): any {
    try { return JSON.parse(value); } catch { return value; }
  }

  private taskKey(task: RequestActionTask): string {
    return String(task.DOC_ID ?? `${task.ID ?? ''}-${task.ID1 ?? ''}`);
  }
}

import { DOCUMENT } from '@angular/common';
import { HttpBackend, HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { firstValueFrom } from 'rxjs';

export type SearchRow = Record<string, unknown>;
export interface SearchResponse { data: SearchRow[]; total?: number; }
interface LegacyWindow extends Window {
  GetUID?: () => string | number;
  ZambaWebRestApiURL?: string;
  thisDomain?: string;
  angular?: { element: (element: Element | Document) => { injector: () => {
    get: (name: string) => { defaults: { headers: { common: { Authorization?: string } } } };
  } | undefined } };
}

export function field(row: SearchRow, names: string[]): string {
  for (const name of names) {
    const value = row[name];
    if (value !== undefined && value !== null && value !== '') return String(value);
  }
  return '';
}

@Injectable({ providedIn: 'root' })
export class GlobalSearchService {
  readonly pageSize = 100;
  private readonly http: HttpClient;
  constructor(backend: HttpBackend, @Inject(DOCUMENT) private readonly document: Document) {
    // The embedded control uses the legacy session, not Angular 16's auth interceptors.
    this.http = new HttpClient(backend);
  }
  private get host(): LegacyWindow { return this.document.defaultView as LegacyWindow; }
  private userId(): number {
    const id = Number(this.host.GetUID?.());
    if (!Number.isSafeInteger(id) || id <= 0) throw new Error('Usuario no disponible en la página anfitriona.');
    return id;
  }
  private token(): string {
    try {
      const angular = this.host.angular;
      const root = this.document.querySelector('[ng-app], [data-ng-app]') || this.document.body;
      const injector = angular?.element(root).injector() || angular?.element(this.document).injector();
      const authorization = injector?.get('$http').defaults.headers.common.Authorization;
      if (authorization && /^Bearer\s+/i.test(authorization)) return authorization.replace(/^Bearer\s+/i, '');
    } catch { /* AngularJS may not be bootstrapped; use its persisted token. */ }
    return (this.host.localStorage.getItem('authorizationData') || '').replace(/^Bearer\s+/i, '');
  }
  private headers(): HttpHeaders {
    const token = this.token();
    return new HttpHeaders(token ? { Authorization: `Bearer ${token}` } : {});
  }
  private endpoint(path: string): string {
    const base = this.host.ZambaWebRestApiURL;
    if (!base) throw new Error('ZambaWebRestApiURL no está disponible en la página anfitriona.');
    return `${base.replace(/\/$/, '')}/${path}`;
  }
  async search(text: string, page: number): Promise<SearchResponse> {
    const response = await firstValueFrom(this.http.post<SearchResponse>(this.endpoint('search/Results'), {
      Parameters: [{ color: 'b1', editMode: false, groupnum: 1, id: 0, maingroup: true,
        name: text, type: 0, operator: 'Empieza', placeholder: '', value: text, value2: '' }],
      UserId: this.userId(), SizePage: { LastPage: page, PageSize: this.pageSize },
    }, { headers: this.headers() }));
    if (!response || !Array.isArray(response.data)) throw new Error('Respuesta de búsqueda inválida.');
    return response;
  }
  async resultUrl(row: SearchRow): Promise<string> {
    const user = this.userId();
    const doc = field(row, ['DOC_ID', 'Doc_Id']);
    const type = field(row, ['DOC_TYPE_ID', 'Doc_Type_Id']);
    const step = field(row, ['STEP_ID', 'Step_Id']);
    const task = field(row, ['TASK_ID', 'Task_Id']);
    if (!doc || !type) throw new Error('Documento no disponible.');
    const base = (this.host.thisDomain || this.host.location.origin).replace(/\/$/, '');
    const hasTaskAndStep = !!task && task !== '0' && !!step && step !== '0';
    let canUseStep = false;
    if (hasTaskAndStep) {
      try {
        canUseStep = await firstValueFrom(this.http.post<boolean>(this.endpoint('Tasks/GetUsersWFStepsRights'), null, {
          headers: this.headers(),
          params: new HttpParams().set('stepId', step).set('right', '19').set('userid', String(user)),
        })) === true;
      } catch {
        // A failed permission check must not grant workflow access.
        canUseStep = false;
      }
    }
    const url = new URL(`${base}/views/${canUseStep ? 'WF/TaskViewer.aspx' : 'search/docviewer.aspx'}`, this.document.baseURI);
    url.searchParams.set('DocType', type);
    url.searchParams.set('docid', doc);
    url.searchParams.set('mode', 's');
    url.searchParams.set('user', String(user));
    url.searchParams.set('gridClicked', '1');
    if (canUseStep) {
      url.searchParams.set('taskid', task);
      url.searchParams.set('s', step);
    }
    const token = this.token();
    if (token) url.searchParams.set('t', token);
    return url.toString();
  }
}

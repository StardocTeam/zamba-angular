import { HttpClient, HttpHeaders, HttpContext } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ACLService } from '@delon/acl';
import { DA_SERVICE_TOKEN, ITokenService, ALLOW_ANONYMOUS } from '@delon/auth';
import { MenuService, SettingsService, TitleService, _HttpClient } from '@delon/theme';
import { NzSafeAny } from 'ng-zorro-antd/core/types';
import { NzIconService } from 'ng-zorro-antd/icon';
import { Observable, catchError, map, of } from 'rxjs';

import { SharedService } from './shared.service';
import { environment } from '../../../environments/environment';
import { ICONS } from '../../../style-icons';
import { ICONS_AUTO } from '../../../style-icons-auto';

export interface ZambaDocumentPayload {
  base64: string;
  fileName?: string;
  extension?: string;
  mimeType?: string;
  userId?: string;
  documentId?: string;
  entityId?: string;
}

export interface ZambaDocumentRequest {
  userId: string;
  documentId: string;
  entityId: string;
}

export interface ZambaReplaceDocumentRequest extends ZambaDocumentRequest {
  base64: string;
  fileName?: string;
}

@Injectable({
  providedIn: 'root',
})
export class ZambaService {
  LOGIN_URL = environment['apiRestBasePath'];
  private readonly apiUrlGetUserId: string = '';
  private apiRestBasePath: string = `${environment['apiRestBasePath']}`;
  private restApiBasePath: string = `${environment['restApi']}`;
  private externalSearchApiBasePath: string = `${environment['externalSearchApi']}`;
  private zambaWebBasePath: string = `${environment['zambaWeb']}`;
  serverError = false;
  type = 0;
  loading = false;
  token: string = '';

  ServiceBase: string = "";
  URL: string = "";
  WebUrl: string = "";

  constructor(
    iconSrv: NzIconService,
    private readonly menuService: MenuService,
    private readonly settingService: SettingsService,
    private readonly aclService: ACLService,
    private readonly titleService: TitleService,
    private readonly httpClient: HttpClient,
    private readonly http: _HttpClient,
    public sharedService: SharedService,
    @Inject(DA_SERVICE_TOKEN) private readonly tokenService: ITokenService,
    private readonly sanitizer: DomSanitizer,
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);

    if (typeof (window as any).getValueFromWebConfig === 'function') {
      this.ServiceBase = (window as any).getValueFromWebConfig('replaceServiceBase');
      this.WebUrl = (window as any).getValueFromWebConfig('WebUrl');
    }

    if (this.ServiceBase && this.ServiceBase.trim()) {
      const serviceBase = this.ServiceBase.trim();
      this.apiRestBasePath = serviceBase;
      this.restApiBasePath = serviceBase;
      this.externalSearchApiBasePath = serviceBase;
    }

    if (this.WebUrl && this.WebUrl.trim()) {
      this.zambaWebBasePath = this.WebUrl.trim();
    }

    this.LOGIN_URL = this.apiRestBasePath;

    const restAPIUrl = this.apiRestBasePath.toLocaleLowerCase();
    this.apiUrlGetUserId = this.buildUrl(restAPIUrl, '/getUserId');

  }

  public getUserId(genericRequest: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };

    return this.httpClient.post<any>(this.apiUrlGetUserId, genericRequest, httpOptions);
  }

  public GetProfileImage() {
    const url = this.buildUrl(this.LOGIN_URL, '/GetProfileImage');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.httpClient.post(url, httpOptions);
  }

  public GetUserInfoForName(data: any) {
    const url = this.buildUrl(this.LOGIN_URL, `/search/GetUserInfoForName?UserName=${data}`);
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
    };
    return this.httpClient.post(url, httpOptions);
  }

  public GetConfigUserSidbar() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    let groupsid: any[] = [];

    if (tokenData != null) {
      tokenData['groups'].forEach(function (values: any) {
        groupsid.push(values['ID']);
      });

      genericRequest = {
        UserId: 0, // tokenData['userID'],
        token: tokenData['token'],
        Params: {
          groups: groupsid.toString(),
        },
      };
    }
    //TODO: este codigo carga la visualizacion de la sidbar pensar mas adelante en ponerlo asyncronico
    //actualmente no funciona de esa manera ya que recarga 2 veces la  interfaz
    const xhr = new XMLHttpRequest();
    xhr.open('POST', this.buildUrl(this.LOGIN_URL, '/configUserSidbar'), false); // El tercer parámetro indica si la solicitud es síncrona
    xhr.setRequestHeader('Content-Type', 'application/json');

    try {
      xhr.send(JSON.stringify(genericRequest));
      if (xhr.status === 200) {
        const deserealize = JSON.parse(xhr.responseText);
        let response = JSON.parse(deserealize);

        if (response.length == 0) return true;

        const valueToAnalize = Number.parseInt(response[0].value, 10);

        if (valueToAnalize == 0) return true;
        else return false;
      } else {
        // Manejar errores aquí
        console.error('Error en la solicitud POST:', xhr.statusText);
        return false;
      }
    } catch (error) {
      // Manejar errores aquí
      console.error('Error en la solicitud POST:', error);
      return false;
    }
  }
  public getParametersFromURL(url: string) {
    var res: any = {};
    if (url != undefined) {
      if (url.indexOf('?') === -1) return res;
      var pairs = url.split('?')[1].split('&');
      var i, pair;
      for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1]) res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
      }
      return res;
    } else {
      var pairs = window.location.search.substring(1).split(/[&?]/);
      var i, pair;
      for (i = 0; i < pairs.length; i++) {
        pair = pairs[i].toLowerCase().split('=');
        if (pair[1]) res[decodeURIComponent(pair[0])] = decodeURIComponent(pair[1]);
      }
      return res;
    }
  }

  public ensureAuthToken(): Observable<boolean> {
    const tokenData = this.tokenService.get();
    if (tokenData?.token) {
      return of(true);
    }

    const urlParams = this.getParametersFromURL(window.location.href);
    const userId = urlParams['userId'] || urlParams['UserId'] || urlParams['u'] || urlParams['user'];

    if (userId) {
      return this.httpClient.get(this.buildUrl(this.restApiBasePath, `/auth/GetJwt?userId=${userId}`), { responseType: 'text' }).pipe(
        map((newToken: string) => {
          if (newToken) {
            this.tokenService.set({
              token: newToken,
              name: userId,
              email: '',
              id: 0,
              time: +new Date(),
            });
            return true;
          }
          return false;
        }),
        catchError(error => {
          console.error('Error fetching JWT:', error);
          return of(false);
        }),
      );
    }

    return of(false);
  }

  public getDocument(url: string): ZambaDocumentRequest | null {
    const UrlParams = this.getParametersFromURL(url);
    let userid = null;
    let docid = null;
    let doctypeid = null;

    if (UrlParams) {
      if (UrlParams.user != undefined) {
        userid = UrlParams.user;
      } else if (UrlParams.userid != undefined) {
        userid = UrlParams.userid;
      } else if (UrlParams.u != undefined) {
        userid = UrlParams.u;
      }

      docid = UrlParams.docid;

      if (UrlParams.doctypeid != undefined) {
        doctypeid = UrlParams.doctypeid;
      } else if (UrlParams.doctype != undefined) {
        doctypeid = UrlParams.doctype;
      } else if (UrlParams.taskid != undefined) {
        // TODO: Reemplazar con la lógica de DocumentViewerServices.getDoctypeId si es necesario
        //doctypeid = DocumentViewerServices.getDoctypeId(UrlParams.taskid);
      }
    }

    if (userid && docid && doctypeid) {
      return {
        userId: userid,
        documentId: docid,
        entityId: doctypeid,
      };
    }

    console.error('[Error]: Fallo al obtener el ID de la entidad (DocTypeId).');
    return null;
  }

  public GetSidebarItems() {
    const tokenData = this.tokenService.get();
    let genericRequest = {};
    let groupsid: any[] = [];
    if (tokenData != null) {
      if (tokenData['groups'] != null) {
        tokenData['groups'].forEach(function (values: any) {
          groupsid.push(values['ID']);
        });

        genericRequest = {
          UserId: 0, // tokenData['userID'],
          token: tokenData['token'],
          Params: {
            groups: groupsid.toString(),
          },
        };
      }
    }

    this.http
      .post(this.buildUrl(this.apiRestBasePath, '/Dashboard/getSidebarItems'), genericRequest, null, {
        context: new HttpContext().set(ALLOW_ANONYMOUS, true),
      })
      .pipe(
        catchError(res => {
          console.warn(`Network request failed`, res);
          return of(res);
        }),
        map((appData: NzSafeAny) => {
          appData = JSON.parse(appData);
          if (appData) {
            this.settingService.setApp(appData.app);
            this.aclService.setFull(true);
            this.menuService.add(appData.menu.items);
            this.titleService.default = '';
            this.titleService.suffix = appData.app.name;
          }
        }),
      )
      .subscribe();
  }

  executeRule(genericRequest: any): Observable<any> {
    return this.http.post(this.buildUrl(this.apiRestBasePath, '/Dashboard/executeRuleDashboard'), genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true),
    });
  }

  // Web Bookmarks
  public getBookmarks(contextData?: any): Observable<any> {
    const tokenData = this.tokenService.get();
    const genericRequest = {
      UserId: tokenData?.['userID'] ?? 0,
      token: tokenData?.['token'] ?? '',
      Params: {
        ...contextData
      }
    };
    return this.httpClient.post(this.buildUrl(this.restApiBasePath, '/WebBookmark/GetBookmarks'), genericRequest);
  }

  public saveBookmark(bookmarkData: any, contextData?: any): Observable<any> {
    const tokenData = this.tokenService.get();
    const genericRequest = {
      UserId: tokenData?.['userID'] ?? 0,
      token: tokenData?.['token'] ?? '',
      Params: {
        ...bookmarkData,
        ...contextData
      }
    };
    return this.httpClient.post(this.buildUrl(this.restApiBasePath, '/WebBookmark/SaveBookmark'), genericRequest);
  }

  public deleteBookmark(bookmarkId: number, contextData?: any): Observable<any> {
    const tokenData = this.tokenService.get();
    const genericRequest = {
      UserId: tokenData?.['userID'] ?? 0,
      token: tokenData?.['token'] ?? '',
      Params: {
        id: bookmarkId,
        ...contextData
      }
    };
    return this.httpClient.post(this.buildUrl(this.restApiBasePath, '/WebBookmark/DeleteBookmark'), genericRequest);
  }

  public getDocumentBase64(request: ZambaDocumentRequest): Observable<ZambaDocumentPayload> {
    const tokenData = this.tokenService.get();
    const genericRequest = {
      UserId: tokenData?.['userID'] ?? 0,
      token: tokenData?.['token'] ?? '',
      Params: this.buildGetDocumentParams(request),
    };

    return this.httpClient
      .post(this.buildUrl(this.externalSearchApiBasePath, '/ExternalSearch/getDocument'), genericRequest, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
        responseType: 'text',
      })
      .pipe(map((responseText: string) => this.extractDocumentPayload(responseText)));
  }

  public replaceDocument(request: ZambaReplaceDocumentRequest): Observable<string> {
    const body = {
      ExternUserID: request.userId,
      IdDocument: request.documentId,
      Id: request.entityId,
      Base64StringArray: this.buildBase64StringArray(request.base64, request.fileName),
      DocTypeId: request.entityId,
      EncryptedData: false,
    };

    return this.httpClient.post(this.buildUrl(this.externalSearchApiBasePath, '/ExternalSearch/ReplaceDoc'), body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      responseType: 'text',
    });
  }

  public preFlightLogin(): SafeResourceUrl {
    let tokenService = this.tokenService.get();
    let userid = tokenService ? tokenService['userID'] : null;
    let token = tokenService ? tokenService['token'] : null;
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      this.buildUrl(this.zambaWebBasePath, '/Views/Security/LoginRRHH.aspx?') + `c=${userid}&t=${token}`,
    );
  }

  private buildUrl(basePath: string, endpoint: string): string {
    const normalizedBasePath = (basePath || '').trim();
    const normalizedEndpoint = (endpoint || '').trim();

    if (!normalizedBasePath) {
      return normalizedEndpoint;
    }

    if (!normalizedEndpoint) {
      return normalizedBasePath;
    }

    const baseEndsWithSlash = normalizedBasePath.endsWith('/');
    const endpointStartsWithSlash = normalizedEndpoint.startsWith('/');

    if (baseEndsWithSlash && endpointStartsWithSlash) {
      return `${normalizedBasePath}${normalizedEndpoint.substring(1)}`;
    }

    if (!baseEndsWithSlash && !endpointStartsWithSlash) {
      return `${normalizedBasePath}/${normalizedEndpoint}`;
    }

    return `${normalizedBasePath}${normalizedEndpoint}`;
  }

  private extractDocumentPayload(responseText: string): ZambaDocumentPayload {
    const payload = this.findTaskDocumentPayload(this.tryParseJson(responseText));

    if (!payload) {
      throw new Error('La respuesta del servicio no contiene un documento en base64 reconocible.');
    }

    return payload;
  }

  private findTaskDocumentPayload(value: unknown, depth: number = 0): ZambaDocumentPayload | null {
    if (value == null || depth > 6) {
      return null;
    }

    if (typeof value === 'string') {
      return this.findTaskDocumentPayloadInString(value, depth);
    }

    if (Array.isArray(value)) {
      return this.findTaskDocumentPayloadInArray(value, depth);
    }

    if (typeof value === 'object') {
      return this.findTaskDocumentPayloadInObject(value as Record<string, unknown>, depth);
    }

    return null;
  }

  private findTaskDocumentPayloadInString(value: string, depth: number): ZambaDocumentPayload | null {
    const trimmedValue = value.trim();

    if (!trimmedValue) {
      return null;
    }

    const dataUrlPayload = this.parseDataUrl(trimmedValue);
    if (dataUrlPayload) {
      return dataUrlPayload;
    }

    const parsedValue = this.tryParseJson(trimmedValue);
    if (parsedValue !== trimmedValue) {
      return this.findTaskDocumentPayload(parsedValue, depth + 1);
    }

    return this.looksLikeBase64(trimmedValue) ? { base64: trimmedValue } : null;
  }

  private findTaskDocumentPayloadInArray(values: unknown[], depth: number): ZambaDocumentPayload | null {
    for (const item of values) {
      const nestedPayload = this.findTaskDocumentPayload(item, depth + 1);
      if (nestedPayload) {
        return nestedPayload;
      }
    }

    return null;
  }

  private findTaskDocumentPayloadInObject(record: Record<string, unknown>, depth: number): ZambaDocumentPayload | null {
    const directPayload = this.buildDirectTaskDocumentPayload(record);
    if (directPayload) {
      return directPayload;
    }

    const documentMetadata = this.extractDocumentMetadata(record);
    const fileName = this.pickFirstString(record, [
      'fileName',
      'FileName',
      'filename',
      'Filename',
      'documentName',
      'DocumentName',
      'title',
      'Title',
      'name',
      'Name',
    ]);
    const extension = this.pickFirstString(record, ['extension', 'Extension', 'ext', 'Ext']);
    const mimeType = this.pickFirstString(record, ['mimeType', 'MimeType', 'contentType', 'ContentType', 'type', 'Type']);

    for (const nestedValue of Object.values(record)) {
      const nestedPayload = this.findTaskDocumentPayload(nestedValue, depth + 1);
      if (nestedPayload) {
        return {
          base64: nestedPayload.base64,
          fileName: nestedPayload.fileName ?? fileName,
          extension: nestedPayload.extension ?? extension,
          mimeType: nestedPayload.mimeType ?? mimeType,
          userId: nestedPayload.userId ?? documentMetadata.userId,
          documentId: nestedPayload.documentId ?? documentMetadata.documentId,
          entityId: nestedPayload.entityId ?? documentMetadata.entityId,
        };
      }
    }

    return null;
  }

  private buildDirectTaskDocumentPayload(record: Record<string, unknown>): ZambaDocumentPayload | null {
    const documentMetadata = this.extractDocumentMetadata(record);
    const directBase64 = this.pickFirstString(record, [
      'base64',
      'Base64',
      'documentBase64',
      'DocumentBase64',
      'fileBase64',
      'FileBase64',
      'contentBase64',
      'ContentBase64',
      'payloadBase64',
      'PayloadBase64',
    ]);

    if (!directBase64) {
      return null;
    }

    const parsedDataUrl = this.parseDataUrl(directBase64);
    return {
      base64: parsedDataUrl?.base64 ?? directBase64,
      fileName: this.pickFirstString(record, [
        'fileName',
        'FileName',
        'filename',
        'Filename',
        'documentName',
        'DocumentName',
        'title',
        'Title',
        'name',
        'Name',
      ]),
      extension: this.pickFirstString(record, ['extension', 'Extension', 'ext', 'Ext']),
      mimeType:
        parsedDataUrl?.mimeType ?? this.pickFirstString(record, ['mimeType', 'MimeType', 'contentType', 'ContentType', 'type', 'Type']),
      userId: documentMetadata.userId,
      documentId: documentMetadata.documentId,
      entityId: documentMetadata.entityId,
    };
  }

  private extractDocumentMetadata(record: Record<string, unknown>): Pick<ZambaDocumentPayload, 'userId' | 'documentId' | 'entityId'> {
    return {
      userId: this.pickFirstText(record, ['ExternUserID', 'ExternUserId', 'externUserId', 'UserId', 'userId', 'user', 'userid', 'u']),
      documentId: this.pickFirstText(record, ['IdDocument', 'idDocument', 'DocumentId', 'documentId', 'DocId', 'docId', 'docid']),
      entityId: this.pickFirstText(record, [
        'EntityId',
        'entityId',
        'EntityID',
        'DocType',
        'DocTypeId',
        'doctype',
        'Id',
        'id',
        'TaskId',
        'taskId',
        'taskid',
      ]),
    };
  }

  private buildGetDocumentParams(request: ZambaDocumentRequest): Record<string, string> {
    const userId = request.userId.trim();
    const documentId = request.documentId.trim();
    const entityId = request.entityId.trim();

    return {
      userId: userId,
      docid: documentId,
      doctypeId: entityId,
      converttopdf: 'false',
    };
  }

  private pickFirstString(record: Record<string, unknown>, keys: readonly string[]): string | undefined {
    for (const key of keys) {
      const value = record[key];
      if (typeof value === 'string' && value.trim()) {
        return value.trim();
      }
    }

    return undefined;
  }

  private pickFirstText(record: Record<string, unknown>, keys: readonly string[]): string | undefined {
    for (const key of keys) {
      const value = record[key];

      if (typeof value === 'string' && value.trim()) {
        return value.trim();
      }

      if (typeof value === 'number' && Number.isFinite(value)) {
        return String(value);
      }
    }

    return undefined;
  }

  private pickFirstArray(record: Record<string, unknown>, keys: readonly string[]): unknown[] | undefined {
    for (const key of keys) {
      const value = record[key];

      if (Array.isArray(value)) {
        return value;
      }

      if (typeof value === 'string' && value.trim()) {
        const parsedValue = this.tryParseJson(value);
        if (Array.isArray(parsedValue)) {
          return parsedValue;
        }
      }
    }

    return undefined;
  }

  private buildBase64StringArray(base64Value: string, fileName?: string): Array<Record<string, string>> {
    const normalizedBase64 = this.removeBase64Prefix(base64Value).split(/\s+/).join('');

    return [{ Base64String: normalizedBase64, FileName: fileName || '' }];
  }

  private parseDataUrl(value: string): ZambaDocumentPayload | null {
    const normalizedValue = value.split(/\s+/).join('');
    const matches = /^data:([^;]+);base64,(.+)$/i.exec(normalizedValue);

    if (!matches) {
      return null;
    }

    return {
      mimeType: matches[1],
      base64: matches[2],
    };
  }

  private looksLikeBase64(value: string): boolean {
    const normalizedValue = value.split(/\s+/).join('');

    return normalizedValue.length >= 16 && normalizedValue.length % 4 === 0 && /^[A-Za-z0-9+/]+={0,2}$/.test(normalizedValue);
  }

  private removeBase64Prefix(value: string): string {
    return value.replace(/^data:[^;]+;base64,/i, '').trim();
  }

  private tryParseJson(value: string): unknown {
    try {
      return JSON.parse(value);
    } catch {
      return value;
    }
  }
}

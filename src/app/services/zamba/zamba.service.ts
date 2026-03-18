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
}

@Injectable({
  providedIn: 'root'
})
export class ZambaService {
  LOGIN_URL = environment['apiRestBasePath'];
  private readonly apiUrlGetUserId: string = "";
  serverError = false;
  type = 0;
  loading = false;
  token: string = "";

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
    private readonly sanitizer: DomSanitizer
  ) {
    iconSrv.addIcon(...ICONS_AUTO, ...ICONS);

    let restAPIUrl = `${environment['apiRestBasePath']}`.toLocaleLowerCase();
    this.apiUrlGetUserId = restAPIUrl + "/getUserId";
  }

  public getUserId(genericRequest: any) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.httpClient.post<any>(this.apiUrlGetUserId, genericRequest, httpOptions);
  }

  public GetProfileImage() {
    const url = `${this.LOGIN_URL}/GetProfileImage`;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post(url, httpOptions);
  }

  public GetUserInfoForName(data: any) {
    const url = `${this.LOGIN_URL}search/GetUserInfoForName?UserName=${data}`;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
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
          groups: groupsid.toString()
        }
      };
    }
    //TODO: este codigo carga la visualizacion de la sidbar pensar mas adelante en ponerlo asyncronico
    //actualmente no funciona de esa manera ya que recarga 2 veces la  interfaz
    const xhr = new XMLHttpRequest();
    xhr.open('POST', `${this.LOGIN_URL}/configUserSidbar`, false); // El tercer parámetro indica si la solicitud es síncrona
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
            groups: groupsid.toString()
          }
        };
      }
    }

    this.http
      .post(`${environment['apiRestBasePath']}/getSidebarItems`, genericRequest, null, {
        context: new HttpContext().set(ALLOW_ANONYMOUS, true)
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
        })
      )
      .subscribe();
  }

  executeRule(genericRequest: any): Observable<any> {
    return this.http.post(`${environment['apiRestBasePath']}/executeRuleDashboard`, genericRequest, null, {
      context: new HttpContext().set(ALLOW_ANONYMOUS, true)
    });
  }

  public getDocumentBase64(request: ZambaDocumentRequest): Observable<ZambaDocumentPayload> {
    const tokenData = this.tokenService.get();
    const genericRequest = {
      UserId: tokenData?.['userID'] ?? 0,
      token: tokenData?.['token'] ?? '',
      Params: this.buildGetDocumentParams(request)
    };

    return this.httpClient
      .post(`${environment['externalSearchApi']}/getDocument`, genericRequest, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        }),
        responseType: 'text'
      })
      .pipe(map((responseText: string) => this.extractDocumentPayload(responseText)));
  }

  public replaceDocument(request: ZambaReplaceDocumentRequest): Observable<string> {
    const body = {
      ExternUserID: request.userId,
      IdDocument: request.documentId,
      Id: request.entityId,
      Base64StringArray: this.buildBase64StringArray(request.base64)
    };

    return this.httpClient.post(`${environment['externalSearchApi']}/ReplaceDoc`, body, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'text'
    });
  }

  public preFlightLogin(): SafeResourceUrl {
    let tokenService = this.tokenService.get();
    let userid = tokenService ? tokenService['userID'] : null;
    let token = tokenService ? tokenService['token'] : null;
    return this.sanitizer.bypassSecurityTrustResourceUrl(
      `${environment['zambaWeb']}/Views/Security/LoginRRHH.aspx?` + `c=${userid}&t=${token}`
    );
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
    const fileName = this.pickFirstString(record, ['fileName', 'FileName', 'filename', 'Filename', 'documentName', 'DocumentName', 'title', 'Title', 'name', 'Name']);
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
          entityId: nestedPayload.entityId ?? documentMetadata.entityId
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
      'PayloadBase64'
    ]);

    if (!directBase64) {
      return null;
    }

    const parsedDataUrl = this.parseDataUrl(directBase64);
    return {
      base64: parsedDataUrl?.base64 ?? directBase64,
      fileName: this.pickFirstString(record, ['fileName', 'FileName', 'filename', 'Filename', 'documentName', 'DocumentName', 'title', 'Title', 'name', 'Name']),
      extension: this.pickFirstString(record, ['extension', 'Extension', 'ext', 'Ext']),
      mimeType: parsedDataUrl?.mimeType ?? this.pickFirstString(record, ['mimeType', 'MimeType', 'contentType', 'ContentType', 'type', 'Type']),
      userId: documentMetadata.userId,
      documentId: documentMetadata.documentId,
      entityId: documentMetadata.entityId
    };
  }

  private extractDocumentMetadata(record: Record<string, unknown>): Pick<ZambaDocumentPayload, 'userId' | 'documentId' | 'entityId'> {
    return {
      userId: this.pickFirstText(record, ['ExternUserID', 'ExternUserId', 'externUserId', 'UserId', 'userId', 'user', 'userid', 'u']),
      documentId: this.pickFirstText(record, ['IdDocument', 'idDocument', 'DocumentId', 'documentId', 'DocId', 'docId', 'docid']),
      entityId: this.pickFirstText(record, ['EntityId', 'entityId', 'EntityID', 'DocType', 'DocTypeId', 'doctype', 'Id', 'id', 'TaskId', 'taskId', 'taskid'])
    };
  }

  private buildGetDocumentParams(request: ZambaDocumentRequest): Record<string, string> {
    const userId = request.userId.trim();
    const documentId = request.documentId.trim();
    const entityId = request.entityId.trim();

    return {
      UserId: userId,
      userId,
      userid: userId,
      ExternUserID: userId,
      IdDocument: documentId,
      DocumentId: documentId,
      documentId,
      DocId: documentId,
      docId: documentId,
      docid: documentId,
      EntityId: entityId,
      entityId,
      EntityID: entityId,
      DocType: entityId,
      DocTypeId: entityId,
      doctype: entityId,
      Id: entityId
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

  private buildBase64StringArray(base64Value: string): Array<Record<string, string>> {
    const normalizedBase64 = this.removeBase64Prefix(base64Value).split(/\s+/).join('');

    return [{ Base64String: normalizedBase64 }];
  }

  private parseDataUrl(value: string): ZambaDocumentPayload | null {
    const normalizedValue = value.split(/\s+/).join('');
    const matches = /^data:([^;]+);base64,(.+)$/i.exec(normalizedValue);

    if (!matches) {
      return null;
    }

    return {
      mimeType: matches[1],
      base64: matches[2]
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

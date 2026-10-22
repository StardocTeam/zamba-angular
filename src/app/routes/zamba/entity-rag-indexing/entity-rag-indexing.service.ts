import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '@env/environment';
import { EntityRagIndexingConfig, UpdateEntityRagIndexingConfig } from './entity-rag-indexing.model';

@Injectable({ providedIn: 'root' })
export class EntityRagIndexingService {
  private readonly baseUrl = this.getApiBaseUrl();

  constructor(private http: HttpClient) { }

  getConfigurations(): Observable<EntityRagIndexingConfig[]> {
    return this.http.get<EntityRagIndexingConfig[]>(this.baseUrl);
  }

  saveConfiguration(documentTypeId: number, config: UpdateEntityRagIndexingConfig): Observable<EntityRagIndexingConfig> {
    return this.http.put<EntityRagIndexingConfig>(`${this.baseUrl}/${documentTypeId}`, config);
  }

  deleteConfiguration(documentTypeId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${documentTypeId}`);
  }

  private getApiBaseUrl(): string {
    const appConfig = (window as any).appConfig;
    const configuredBaseUrl = appConfig?.scheduleApiBaseUrl || appConfig?.copilotApiBaseUrl || environment['restApi'];
    const apiBaseUrl = configuredBaseUrl.replace(/\/+$/, '');
    return `${apiBaseUrl.endsWith('/api') ? apiBaseUrl : `${apiBaseUrl}/api`}/entity-rag-indexing`;
  }
}

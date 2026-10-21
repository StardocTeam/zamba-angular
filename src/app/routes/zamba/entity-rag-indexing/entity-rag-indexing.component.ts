import { Component, OnDestroy, OnInit } from '@angular/core';
import { EntityRagIndexMode, EntityRagIndexingConfig, UpdateEntityRagIndexingConfig } from './entity-rag-indexing.model';

import { EntityRagIndexingService } from './entity-rag-indexing.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-entity-rag-indexing',
  templateUrl: './entity-rag-indexing.component.html',
  styleUrls: ['./entity-rag-indexing.component.css']
})
export class EntityRagIndexingComponent implements OnInit, OnDestroy {
  readonly IndexMode = EntityRagIndexMode;
  configurations: EntityRagIndexingConfig[] = [];
  loading = false;
  creating = false;
  createFormVisible = false;
  createDocumentTypeId: number | null = null;
  createEnabled = true;
  createIndexFromDate = '';
  createIndexMode = EntityRagIndexMode.Batch;
  private readonly savingIds = new Set<number>();
  private readonly deletingIds = new Set<number>();
  private readonly destroy$ = new Subject<void>();

  constructor(private service: EntityRagIndexingService, private message: NzMessageService) { }

  ngOnInit(): void {
    this.loadConfigurations();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  loadConfigurations(): void {
    this.loading = true;
    this.service.getConfigurations().pipe(takeUntil(this.destroy$)).subscribe({
      next: configurations => {
        this.configurations = configurations;
        this.loading = false;
      },
      error: () => {
        this.message.error('Could not load document types and RAG indexing settings');
        this.loading = false;
      }
    });
  }

  getDateValue(config: EntityRagIndexingConfig): string {
    return config.indexFromDate ? config.indexFromDate.slice(0, 10) : '';
  }

  get availableDocumentTypes(): EntityRagIndexingConfig[] {
    return this.configurations.filter(config => config.isSupported && !config.createdAtUtc);
  }

  openCreateForm(): void {
    this.createFormVisible = true;
    this.createDocumentTypeId = this.availableDocumentTypes[0]?.documentTypeId ?? null;
  }

  cancelCreate(): void {
    this.createFormVisible = false;
    this.createDocumentTypeId = null;
    this.createEnabled = true;
    this.createIndexFromDate = '';
    this.createIndexMode = EntityRagIndexMode.Batch;
  }

  createConfiguration(): void {
    const config = this.availableDocumentTypes.find(item => item.documentTypeId === this.createDocumentTypeId);
    if (!config) {
      this.message.error('Select an available document type');
      return;
    }

    this.creating = true;
    this.service.saveConfiguration(config.documentTypeId, {
      isEnabled: this.createEnabled,
      indexFromDate: this.createIndexFromDate ? `${this.createIndexFromDate}T00:00:00` : null,
      indexMode: this.createIndexMode
    }).pipe(takeUntil(this.destroy$)).subscribe({
      next: saved => {
        Object.assign(config, saved);
        this.message.success('RAG indexing configuration created');
        this.cancelCreate();
        this.creating = false;
      },
      error: error => {
        this.message.error(error?.error?.detail || 'Could not create RAG indexing configuration');
        this.creating = false;
      }
    });
  }

  setDateValue(config: EntityRagIndexingConfig, value: string): void {
    config.indexFromDate = value ? `${value}T00:00:00` : null;
  }

  isSaving(documentTypeId: number): boolean {
    return this.savingIds.has(documentTypeId);
  }

  isDeleting(documentTypeId: number): boolean {
    return this.deletingIds.has(documentTypeId);
  }

  save(config: EntityRagIndexingConfig): void {
    const request: UpdateEntityRagIndexingConfig = {
      isEnabled: config.isEnabled,
      indexFromDate: config.indexFromDate || null,
      indexMode: config.indexMode
    };
    this.savingIds.add(config.documentTypeId);
    this.service.saveConfiguration(config.documentTypeId, request)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: saved => {
          Object.assign(config, saved);
          this.message.success('RAG indexing configuration saved');
          this.savingIds.delete(config.documentTypeId);
        },
        error: error => {
          this.message.error(error?.error?.detail || 'Could not save RAG indexing configuration');
          this.savingIds.delete(config.documentTypeId);
        }
      });
  }

  remove(config: EntityRagIndexingConfig): void {
    if (!config.createdAtUtc || !window.confirm(`Remove the saved RAG indexing configuration for ${this.getName(config)}?`)) {
      return;
    }

    this.deletingIds.add(config.documentTypeId);
    this.service.deleteConfiguration(config.documentTypeId)
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: () => {
          config.isEnabled = false;
          config.indexFromDate = null;
          config.indexMode = EntityRagIndexMode.Batch;
          config.createdAtUtc = null;
          config.updatedAtUtc = null;
          this.message.success('RAG indexing configuration removed');
          this.deletingIds.delete(config.documentTypeId);
        },
        error: () => {
          this.message.error('Could not remove RAG indexing configuration');
          this.deletingIds.delete(config.documentTypeId);
        }
      });
  }

  getName(config: EntityRagIndexingConfig): string {
    return config.documentTypeName?.trim() || `Document type ${config.documentTypeId}`;
  }
}

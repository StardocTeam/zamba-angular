export enum EntityRagIndexMode {
  OnInsert = 0,
  Batch = 1
}

export interface EntityRagIndexingConfig {
  documentTypeId: number;
  documentTypeName?: string;
  isSupported: boolean;
  isEnabled: boolean;
  indexFromDate?: string | null;
  indexMode: EntityRagIndexMode;
  createdAtUtc?: string | null;
  updatedAtUtc?: string | null;
}

export interface UpdateEntityRagIndexingConfig {
  isEnabled: boolean;
  indexFromDate: string | null;
  indexMode: EntityRagIndexMode;
}

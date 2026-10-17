export type RequestActionQueue = 'approval' | 'confirmation' | 'summary';
export type TaskCardAction = 'approve' | 'reject' | 'save' | 'restore' | 'open' | 'detail';

export interface MobileUser { id: number; username: string; token: string; }

export interface RequestActionTask {
  ID?: number | string;
  ID1?: number | string;
  EID?: number | string;
  TID?: number | string;
  DOC_ID?: number | string;
  DOC_TYPE_ID?: number | string;
  PROVEEDOR?: string;
  MONEDA?: string;
  IMPORTE?: string | number;
  TIPO_GASTO?: string;
  CONCEPTO?: string;
  AREA?: string;
  TITLE?: string;
  THUMB?: string;
  THUMB_URL?: string;
  THUMBNAIL?: string;
  THUMBNAIL_URL?: string;
  PREVIEW_URL?: string;
  IMAGE_URL?: string;
  FECHA?: string;
  SOLICITANTE?: string;
  RESPONSABILIDAD?: string;
  MECANICA_DEL_ACCIDENTE?: string;
  VICTIMAS?: string;
  RECLAMANTE?: string;
  NEGOCIACION?: string;
  OBSERVACIONES?: string;
  [key: string]: unknown;
}

export interface DashboardCounts { approval: number; confirmation: number; summary: number; }

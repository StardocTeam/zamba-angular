export interface TraceSource {
  id: number;
  applicationName: string;
  environment: string;
  folderPath: string;
  isEnabled: boolean;
}

export interface TraceRecord {
  id: number;
  traceSourceId: number;
  applicationName: string;
  environment: string;
  folderPath: string;
  relativePath: string;
  fileName: string;
  traceTimestampUtc: string;
  userId: string | null;
  traceConfig: string | null;
  title: string;
  registeredAtUtc: string;
}

export interface TracePage {
  items: TraceRecord[];
  totalCount: number;
  page: number;
  pageSize: number;
}

export interface CreateTraceSource {
  applicationName: string;
  environment: string;
  folderPath: string;
}

export interface TraceScanResult {
  discovered: number;
  registered: number;
}

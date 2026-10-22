export interface UsersDashboardCatalogItem {
  [key: string]: unknown;
}

export interface UsersDashboardFiltersData {
  workflows: UsersDashboardCatalogItem[];
  steps: UsersDashboardCatalogItem[];
  documentTypes: UsersDashboardCatalogItem[];
  principals: UsersDashboardCatalogItem[];
}

export interface UsersDashboardQuery {
  userId: string;
  workflowId: string;
  stepId: string;
  documentTypeId: string;
  from: string;
  to: string;
}

export type UsersDashboardCount = number | string;
export type UsersDashboardNumericValue = number | string | null;

export interface UsersDashboardDistributionItem {
  userId: string;
  userName: string;
  pendingTasks: UsersDashboardCount;
  overdueTasks: UsersDashboardCount;
  recordedSteps: UsersDashboardCount;
  averageExecutionMinutes: UsersDashboardNumericValue;
  forecastMinutes: UsersDashboardNumericValue;
}

export interface UsersDashboardHistoryItem {
  [key: string]: unknown;
  documentId: string | null;
  date: string | null;
  userName: string | null;
  action: string | null;
  documentName?: string | null;
  workflowName?: string | null;
  stepName?: string | null;
  tool?: string | null;
  description?: string | null;
}

export interface UsersDashboardOverview {
  summary: {
    pendingTasks: UsersDashboardCount;
    overdueTasks: UsersDashboardCount;
    workflowHistoryEntries: UsersDashboardCount;
    userActions: UsersDashboardCount;
    recordedSteps: UsersDashboardCount;
    averageExecutionMinutes: UsersDashboardNumericValue;
    forecastMinutes: UsersDashboardNumericValue;
  };
  distribution: UsersDashboardDistributionItem[];
  performanceHistory: UsersDashboardExecutionItem[];
  workflowHistory: UsersDashboardHistoryItem[];
  userActions: UsersDashboardHistoryItem[];
}

export interface UsersDashboardExecutionItem {
  userName: string | null;
  workflowId: string | null;
  stepId: string | null;
  consumedMinutes: UsersDashboardNumericValue;
  checkedOut: string | null;
}

export interface UsersDashboardTask {
  [key: string]: unknown;
  documentId: string | null;
  taskId: string | null;
  taskName: string | null;
  workflowName: string | null;
  stepName: string | null;
  stateName: string | null;
  stateDescription: string | null;
  assigneeId: string | null;
  expiresAt: string | null;
}

export interface UsersDashboardTasksPage {
  page: number;
  pageSize: number;
  total: number;
  items: UsersDashboardTask[];
}

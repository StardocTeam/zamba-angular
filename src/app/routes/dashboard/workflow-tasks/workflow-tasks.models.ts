export interface WorkflowDashboardCatalogItem {
    [key: string]: unknown;
}

export interface WorkflowDashboardFiltersData {
    workflows: WorkflowDashboardCatalogItem[];
    steps: WorkflowDashboardCatalogItem[];
    states: WorkflowDashboardCatalogItem[];
    documentTypes: WorkflowDashboardCatalogItem[];
    principals: WorkflowDashboardCatalogItem[];
}

export interface WorkflowDashboardQuery {
    workflowId: string;
    stepId: string;
    documentTypeId: string;
    assigneeIds: string[];
    from: string;
    to: string;
    dateField: 'checkin' | 'created';
    page: number;
    pageSize: number;
}

export interface WorkflowDashboardTask {
    [key: string]: unknown;
    documentId: string | null;
    taskId: string | null;
    taskName: string | null;
    documentTypeId: string | null;
    documentTypeName: string | null;
    workflowId: string | null;
    workflowName: string | null;
    stepId: string | null;
    stepName: string | null;
    stateId: string | null;
    taskStateId: string | null;
    stateName: string | null;
    stateDescription: string | null;
    assigneeId: string | null;
    assigneeName: string | null;
    assignedById: string | null;
    assignedByName: string | null;
    creatorId: string | null;
    creatorName: string | null;
    createdAt: string | null;
    checkIn: string | null;
    expiresAt: string | null;
    comments: string | null;
    tag: string | null;
}

export interface WorkflowDashboardAttribute {
    indexId: string;
    name: string | null;
    type: string | null;
    value: unknown;
    displayValue: unknown;
    choices: Array<{ code: unknown; description: unknown }>;
}

export interface WorkflowDashboardTasksPage {
    page: number;
    pageSize: number;
    total: number;
    summary: {
        total: number;
        overdue: number;
        unassigned: number;
    };
    items: WorkflowDashboardTask[];
}

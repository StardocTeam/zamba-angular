export interface CopilotTelemetryFilters {
    fromUtc: string;
    toUtc: string;
    documentTypeCode: number | null;
    search: string;
    page: number;
    pageSize: number;
}

export interface CopilotTelemetrySummary {
    totalRuns: number;
    succeededRuns: number;
    failedRuns: number;
    ratedRuns: number;
    negativeRuns: number;
    positiveVotes: number;
    negativeVotes: number;
    negativeVotePercent: number;
    negativeRunPercent: number;
    averageTotalDurationMs: number | null;
    averageInputTokens: number | null;
    averageEstimatedCost: number | null;
    totalInputTokens: number;
    totalOutputTokens: number;
    totalTokens: number;
    averageTotalTokensPerRequest: number | null;
    creditsUsed: number;
    averageCostAtOverageRateUsd: number | null;
    monthRequestCount: number;
    monthTotalTokens: number;
    monthCreditsUsed: number;
    monthCreditsExceeded: number;
    monthAdditionalCostUsd: number;
    monthEstimatedCostUsd: number;
    lifetimeRequestCount: number;
    lifetimeTotalTokens: number;
    lifetimeEstimatedCostUsd: number;
}

export interface CopilotNegativeRun {
    runId: string;
    startedAtUtc: string;
    status: string;
    documentTypeCode: number | null;
    fileName: string | null;
    promptPreview: string;
    responsePreview: string | null;
    chatModel: string | null;
    totalDurationMs: number | null;
    inputTokens: number | null;
    outputTokens: number | null;
    copilotCreditsUsed: number | null;
    estimatedCostAtOverageRateUsd: number | null;
    negativeVoteCount: number;
    latestFeedbackComment: string | null;
}

export interface CopilotNegativeRunsPage {
    page: number;
    pageSize: number;
    totalRuns: number;
    items: CopilotNegativeRun[];
}

export interface CopilotRunStep {
    id: number;
    name: string;
    status: string;
    startedAtUtc: string;
    completedAtUtc: string | null;
    durationMs: number | null;
    detail: string | null;
    errorMessage: string | null;
}

export interface CopilotFeedbackRecord {
    rating: number;
    comment: string | null;
    createdAtUtc: string;
}

export interface CopilotRunDetail extends Omit<CopilotNegativeRun, 'promptPreview' | 'responsePreview' | 'negativeVoteCount' | 'latestFeedbackComment'> {
    completedAtUtc: string | null;
    prompt: string;
    response: string | null;
    documentId: string | null;
    sessionId: string | null;
    embeddingProvider: string | null;
    documentAlreadyIndexed: boolean | null;
    chunksIndexed: number;
    chunksUsed: number;
    embeddingDurationMs: number | null;
    retrievalDurationMs: number | null;
    chatDurationMs: number | null;
    cacheReadTokens: number | null;
    cacheWriteTokens: number | null;
    estimatedCost: number | null;
    errorMessage: string | null;
    steps: CopilotRunStep[];
    feedback: CopilotFeedbackRecord[];
}

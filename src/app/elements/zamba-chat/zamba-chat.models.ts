export interface CopilotPromptRequest {
    prompt: string;
    fileBase64?: string;
    fileName?: string;
    documentId?: string;
}

export interface PromptStats {
    embeddingProvider?: string;
    documentAlreadyIndexed?: boolean;
    chunksIndexed?: number;
    embeddingDurationMs?: number;
    retrievalDurationMs?: number;
    chatDurationMs?: number;
    totalDurationMs?: number;
    chatModel?: string;
    inputTokens?: number;
    outputTokens?: number;
    cacheReadTokens?: number;
    cacheWriteTokens?: number;
    estimatedCost?: number;
}

export interface CopilotPromptResponse {
    response: string;
    documentId: string;
    chunksUsed: number;
    stats: PromptStats;
}

export type ZambaChatRole = 'user' | 'assistant' | 'error';

export interface ZambaChatMessage {
    role: ZambaChatRole;
    text: string;
    fileName?: string;
}

/** Binds an extraction prompt (answered right after a file is attached) to a target input's id. */
export interface ZambaFieldBinding {
    inputId: string;
    prompt: string;
}

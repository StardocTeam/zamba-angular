import { AfterViewChecked, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { CopilotPromptRequest, CopilotPromptResponse, ZambaChatMessage } from './zamba-chat.models';

import { HttpClient } from '@angular/common/http';

const MAX_FILE_SIZE_BYTES = 5 * 1024 * 1024;

@Component({
  selector: 'zamba-chat',
  templateUrl: './zamba-chat.component.html',
  styleUrls: ['./zamba-chat.component.less'],
})
export class ZambaChatComponent implements AfterViewChecked {
  /** Base URL of the Zamba.Api instance, e.g. https://localhost:7088 */
  @Input() apiBaseUrl = 'https://localhost:7088';

  /** Path (relative to apiBaseUrl) of the ask endpoint. */
  @Input() askPath = '/api/Copilot/ask';

  @Input() width: string | number = '380px';
  @Input() height: string | number = '520px';

  @ViewChild('fileInput') fileInputRef?: ElementRef<HTMLInputElement>;
  @ViewChild('messagesEnd') messagesEndRef?: ElementRef<HTMLDivElement>;

  messages: ZambaChatMessage[] = [];
  prompt = '';
  isSending = false;
  errorMessage: string | null = null;

  pendingFileName: string | null = null;
  private pendingFileBase64: string | null = null;

  /** Id of the document already indexed by Zamba.Api; reused instead of re-sending the file. */
  documentId: string | null = null;

  private shouldScrollToBottom = false;

  constructor(private readonly http: HttpClient) {}

  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
      this.shouldScrollToBottom = false;
    }
  }

  get hostWidth(): string {
    return typeof this.width === 'number' ? `${this.width}px` : this.width;
  }

  get hostHeight(): string {
    return typeof this.height === 'number' ? `${this.height}px` : this.height;
  }

  triggerFilePicker(): void {
    this.fileInputRef?.nativeElement.click();
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.[0];
    input.value = '';

    if (!file) {
      return;
    }

    if (file.size > MAX_FILE_SIZE_BYTES) {
      this.errorMessage = `El archivo supera el tamaño máximo permitido (${MAX_FILE_SIZE_BYTES / (1024 * 1024)} MB).`;
      return;
    }

    this.errorMessage = null;

    const reader = new FileReader();
    reader.onload = () => {
      const result = reader.result as string;
      const base64 = result.substring(result.indexOf(',') + 1);
      this.pendingFileBase64 = base64;
      this.pendingFileName = file.name;
      // A newly attached file starts a new document context.
      this.documentId = null;
    };
    reader.onerror = () => {
      this.errorMessage = 'No se pudo leer el archivo seleccionado.';
    };
    reader.readAsDataURL(file);
  }

  clearPendingFile(): void {
    this.pendingFileBase64 = null;
    this.pendingFileName = null;
  }

  canSend(): boolean {
    return !this.isSending && this.prompt.trim().length > 0 && (!!this.pendingFileBase64 || !!this.documentId);
  }

  sendPrompt(): void {
    const trimmedPrompt = this.prompt.trim();
    if (!trimmedPrompt || this.isSending) {
      return;
    }

    if (!this.pendingFileBase64 && !this.documentId) {
      this.errorMessage = 'Adjuntá un archivo antes de enviar el primer mensaje.';
      return;
    }

    const request: CopilotPromptRequest = { prompt: trimmedPrompt };
    const attachedFileName = this.pendingFileName ?? undefined;

    if (this.pendingFileBase64) {
      request.fileBase64 = this.pendingFileBase64;
      request.fileName = this.pendingFileName ?? 'archivo';
    } else if (this.documentId) {
      request.documentId = this.documentId;
    }

    this.messages.push({ role: 'user', text: trimmedPrompt, fileName: attachedFileName });
    this.prompt = '';
    this.errorMessage = null;
    this.isSending = true;
    this.shouldScrollToBottom = true;

    const url = this.buildUrl();

    this.http.post<CopilotPromptResponse>(url, request).subscribe({
      next: response => {
        this.documentId = response.documentId || this.documentId;
        this.clearPendingFile();
        this.messages.push({ role: 'assistant', text: response.response });
        this.isSending = false;
        this.shouldScrollToBottom = true;
      },
      error: err => {
        const message = err?.error && typeof err.error === 'string' ? err.error : 'Ocurrió un error al consultar la API de Zamba.';
        this.messages.push({ role: 'error', text: message });
        this.isSending = false;
        this.shouldScrollToBottom = true;
      },
    });
  }

  private buildUrl(): string {
    const base = this.apiBaseUrl.endsWith('/') ? this.apiBaseUrl.slice(0, -1) : this.apiBaseUrl;
    const path = this.askPath.startsWith('/') ? this.askPath : `/${this.askPath}`;
    return `${base}${path}`;
  }

  private scrollToBottom(): void {
    this.messagesEndRef?.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end' });
  }
}

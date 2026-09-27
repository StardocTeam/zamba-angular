import { AfterViewChecked, Component, ElementRef, HostBinding, Inject, Input, OnDestroy, ViewChild } from '@angular/core';
import { CopilotPromptRequest, CopilotPromptResponse, ZambaChatMessage, ZambaFieldBinding } from './zamba-chat.models';

import { DOCUMENT } from '@angular/common';
import { HttpClient } from '@angular/common/http';

const MAX_FILE_SIZE_BYTES = 5 * 1024 * 1024;

@Component({
  selector: 'zamba-chat',
  templateUrl: './zamba-chat.component.html',
  styleUrls: ['./zamba-chat.component.less'],
})
export class ZambaChatComponent implements AfterViewChecked, OnDestroy {
  /** Base URL of the Zamba.Api instance, e.g. https://localhost:7088 */
  @Input() apiBaseUrl = 'https://localhost:7088';

  /** Path (relative to apiBaseUrl) of the ask endpoint. */
  @Input() askPath = '/api/Copilot/ask';

  @Input() documentTypeCode: number | null = null;

  @Input() width: string | number = '380px';
  @Input() height: string | number = '520px';

  /** Bindings between a target input's id and the prompt used to auto-extract its value from the file. */
  @Input() fieldBindings: ZambaFieldBinding[] = [];

  /** Enables extraction requests immediately after a file is attached. */
  @Input() automaticExtraction = true;

  isOpen = false;

  @ViewChild('fileInput') fileInputRef?: ElementRef<HTMLInputElement>;
  @ViewChild('messagesEnd') messagesEndRef?: ElementRef<HTMLDivElement>;
  ZambaWebRestApiURL: any = "";

  private endpoint(path: string): string {
    if (typeof (window as any).getValueFromWebConfig === 'function') {
      this.ZambaWebRestApiURL = (window as any).getValueFromWebConfig('ZambaWebRestApiURL');
    }

    if (!this.ZambaWebRestApiURL) throw new Error('ZambaWebRestApiURL no está disponible en la página anfitriona.');
    return `${this.ZambaWebRestApiURL.replace(/\/$/, '')}/${path}`;
  }


  messages: ZambaChatMessage[] = [];
  prompt = '';
  isSending = false;
  isExtracting = false;
  errorMessage: string | null = null;

  pendingFileName: string | null = null;
  private pendingFileBase64: string | null = null;

  /** Id of the document already indexed by Zamba.Api; reused instead of re-sending the file. */
  documentId: string | null = null;

  private shouldScrollToBottom = false;
  private readonly voterId: string;
  private previousBodyPaddingRight: { value: string; priority: string } | null = null;
  private previousBodyTransition: { value: string; priority: string } | null = null;

  @HostBinding('class.is-open')
  get hostIsOpen(): boolean {
    return this.isOpen;
  }

  @HostBinding('style.width')
  get hostPanelWidth(): string | null {
    return this.isOpen ? this.hostWidth : null;
  }

  constructor(
    private readonly http: HttpClient,
    @Inject(DOCUMENT) private readonly document: Document
  ) {
    this.voterId = this.getOrCreateVoterId();
  }

  ngAfterViewChecked(): void {
    if (this.shouldScrollToBottom) {
      this.scrollToBottom();
      this.shouldScrollToBottom = false;
    }
  }

  ngOnDestroy(): void {
    this.restorePageLayout();
  }

  openChat(): void {
    if (this.isOpen) {
      return;
    }

    this.isOpen = true;
    const body = this.document.body;
    this.previousBodyPaddingRight = {
      value: body.style.getPropertyValue('padding-right'),
      priority: body.style.getPropertyPriority('padding-right'),
    };
    this.previousBodyTransition = {
      value: body.style.getPropertyValue('transition'),
      priority: body.style.getPropertyPriority('transition'),
    };

    const currentPadding = getComputedStyle(body).paddingRight;
    const existingTransition = this.previousBodyTransition.value;
    const transition = existingTransition
      ? `${existingTransition}, padding-right 240ms ease`
      : 'padding-right 240ms ease';
    body.style.setProperty('transition', transition);
    body.style.setProperty('padding-right', `calc(${currentPadding} + min(${this.hostWidth}, 100vw))`);
  }

  closeChat(): void {
    if (!this.isOpen) {
      return;
    }

    this.isOpen = false;
    this.restorePageLayout();
  }

  private restorePageLayout(): void {
    const body = this.document.body;
    this.restoreInlineStyle(body, 'padding-right', this.previousBodyPaddingRight);
    this.restoreInlineStyle(body, 'transition', this.previousBodyTransition);
    this.previousBodyPaddingRight = null;
    this.previousBodyTransition = null;
  }

  private restoreInlineStyle(
    element: HTMLElement,
    property: string,
    previous: { value: string; priority: string } | null
  ): void {
    if (!previous) {
      return;
    }

    if (previous.value) {
      element.style.setProperty(property, previous.value, previous.priority);
    } else {
      element.style.removeProperty(property);
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
      if (this.automaticExtraction) {
        this.runFieldExtractions();
      }
    };
    reader.onerror = () => {
      this.errorMessage = 'No se pudo leer el archivo seleccionado.';
    };
    reader.readAsDataURL(file);
  }

  /** Automatically asks each configured prompt right after a file is attached and writes the answers into the bound inputs. */
  private runFieldExtractions(): void {
    if (!this.fieldBindings?.length || !this.pendingFileBase64) {
      return;
    }

    const [first, ...rest] = this.fieldBindings;
    this.isExtracting = true;

    const firstRequest: CopilotPromptRequest = {
      prompt: first.prompt,
      fileBase64: this.pendingFileBase64,
      fileName: this.pendingFileName ?? 'archivo',
      documentTypeCode: this.documentTypeCode ?? undefined,
    };

    this.http.post<CopilotPromptResponse>(this.buildUrl(), firstRequest).subscribe({
      next: response => {
        this.documentId = response.documentId;
        this.applyFieldValue(first.inputId, response.response);
        this.runRemainingExtractions(rest);
      },
      error: () => {
        this.isExtracting = false;
        this.errorMessage = 'No se pudieron extraer los datos automáticos del documento.';
      },
    });
  }

  private runRemainingExtractions(bindings: ZambaFieldBinding[]): void {
    if (bindings.length === 0) {
      this.isExtracting = false;
      return;
    }

    let pending = bindings.length;
    const onSettled = () => {
      pending--;
      if (pending === 0) {
        this.isExtracting = false;
      }
    };

    bindings.forEach(binding => {
      const request: CopilotPromptRequest = {
        prompt: binding.prompt,
        documentId: this.documentId!,
        documentTypeCode: this.documentTypeCode ?? undefined,
      };
      this.http.post<CopilotPromptResponse>(this.buildUrl(), request).subscribe({
        next: response => {
          this.applyFieldValue(binding.inputId, response.response);
          onSettled();
        },
        error: onSettled,
      });
    });
  }

  private applyFieldValue(inputId: string | undefined, value: string): void {
    if (!inputId) {
      try {
        const jsonValue = value
          .trim()
          .replace(/^```(?:json)?\s*/i, '')
          .replace(/\s*```$/i, '')
          .trim();
        const values = JSON.parse(jsonValue) as Record<string, unknown>;
        Object.entries(values).forEach(([key, fieldValue]) => {
          this.setInputValue(key, String(fieldValue ?? ''));
        });
        return;
      } catch {
        this.errorMessage = 'La extracción automática no devolvió un JSON válido.';
        return;
      }
    }

    this.setInputValue(inputId, value.trim());
  }

  private setInputValue(inputId: string, value: string): void {
    const target = this.document.getElementById(inputId) as HTMLInputElement | null;
    if (target) {
      target.value = value;
      target.dispatchEvent(new Event('input', { bubbles: true }));
      target.dispatchEvent(new Event('change', { bubbles: true }));
    }
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

    const request: CopilotPromptRequest = {
      prompt: trimmedPrompt,
      documentTypeCode: this.documentTypeCode ?? undefined,
    };
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
        this.messages.push({ role: 'assistant', text: response.response, runId: response.runId });
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

  rateAnswer(message: ZambaChatMessage, rating: 1 | -1): void {
    if (!message.runId || message.feedbackSending) {
      return;
    }

    const previousRating = message.feedbackRating;
    message.feedbackSending = true;
    message.feedbackError = false;
    this.http.post<void>(this.buildFeedbackUrl(), {
      runId: message.runId,
      voterId: this.voterId,
      rating,
    }).subscribe({
      next: () => {
        message.feedbackRating = rating;
        message.feedbackSending = false;
      },
      error: () => {
        message.feedbackRating = previousRating;
        message.feedbackSending = false;
        message.feedbackError = true;
      },
    });
  }

  private getOrCreateVoterId(): string {
    const storageKey = 'zamba-chat-voter-id';
    let voterId = localStorage.getItem(storageKey);
    if (!voterId) {
      voterId = globalThis.crypto.randomUUID();
      localStorage.setItem(storageKey, voterId);
    }

    return voterId;
  }

  private buildUrl(): string {
    const base = this.apiBaseUrl.replace(/\/$/, '');
    const path = this.askPath.startsWith('/') ? this.askPath : `/${this.askPath}`;
    return `${base}${path}`;
  }

  private buildFeedbackUrl(): string {
    return this.buildUrl().replace(/\/ask\/?$/, '/feedback');
  }

  private scrollToBottom(): void {
    this.messagesEndRef?.nativeElement.scrollIntoView({ behavior: 'smooth', block: 'end' });
  }
}

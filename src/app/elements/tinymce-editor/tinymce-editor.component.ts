import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, Input, OnChanges, Optional, SimpleChanges } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';
import { firstValueFrom } from 'rxjs';

import { ZambaDocumentPayload, ZambaDocumentRequest, ZambaService } from '../../services/zamba/zamba.service';

const SELF_HOSTED_ASSET_PATH = 'assets/tinymce';
const SELF_HOSTED_BASE_URL = resolveTinyMceAssetUrl();
const SELF_HOSTED_SCRIPT_SRC = resolveTinyMceAssetUrl('tinymce.min.js');
const LICENSE_KEY = 'gpl';
const BLANK_DOCUMENT = '<p></p>';
const DOCX_MIME_TYPE = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';

type ElementInputValue = number | string | null | undefined;
type BooleanLike = boolean | string | null | undefined;

interface MammothMessage {
  message: string;
}

interface MammothResult {
  value: string;
  messages: MammothMessage[];
}

function resolveTinyMceAssetUrl(pathSuffix?: string): string {
  const relativePath = pathSuffix ? `${SELF_HOSTED_ASSET_PATH}/${pathSuffix}` : `${SELF_HOSTED_ASSET_PATH}/`;
  const baseUri = globalThis.document?.baseURI ?? globalThis.location?.href;

  if (!baseUri) {
    return pathSuffix ? `${SELF_HOSTED_ASSET_PATH}/${pathSuffix}` : SELF_HOSTED_ASSET_PATH;
  }

  const resolvedUrl = new URL(relativePath, baseUri).toString();
  return pathSuffix ? resolvedUrl : resolvedUrl.replace(/\/$/, '');
}

@Component({
  selector: 'app-tinymce-element',
  templateUrl: './tinymce-editor.component.html',
  styleUrls: ['./tinymce-editor.component.less'],
  providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: SELF_HOSTED_SCRIPT_SRC }],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TinymceElementComponent implements OnChanges {
  @Input() userId?: ElementInputValue;
  @Input() documentId?: ElementInputValue;
  @Input() entityId?: ElementInputValue;
  @Input() token?: string | null;

  @Input()
  set readOnly(value: BooleanLike) {
    this._readOnly = this.toBoolean(value);
    this.editorInit = this.buildEditorInit();
    this.cdr.markForCheck();
  }

  get readOnly(): boolean {
    return this._readOnly;
  }

  readonly licenseKey = LICENSE_KEY;

  editorContent = BLANK_DOCUMENT;
  editorInit = this.buildEditorInit();
  documentName = this.buildDefaultDocumentName();
  loading = false;
  openingLocalDocx = false;
  exporting = false;
  saving = false;
  statusMessage = 'Esperando userId, documentId y entityId para cargar el documento.';
  errorMessage = '';

  private _readOnly = false;

  constructor(
    private readonly cdr: ChangeDetectorRef,
    @Optional() private readonly zambaService: ZambaService | null,
    @Optional() @Inject(DA_SERVICE_TOKEN) private readonly tokenService: ITokenService | null
  ) { }

  get hasDocumentContext(): boolean {
    return this.resolveDocumentRequest() !== null;
  }

  get documentIdLabel(): string {
    return this.normaliseTextInput(this.documentId);
  }

  get entityIdLabel(): string {
    return this.normaliseTextInput(this.entityId);
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['userId'] || changes['documentId'] || changes['entityId'] || changes['token']) {
      void this.loadDocumentFromInputs();
    }
  }

  reload(): void {
    void this.loadDocumentFromInputs();
  }

  createBlankDocument(): void {
    this.editorContent = BLANK_DOCUMENT;
    this.documentName = this.buildDefaultDocumentName(this.documentId);
    this.errorMessage = '';
    this.statusMessage = 'Nuevo documento.';
    this.cdr.markForCheck();
  }

  onContentChange(value: string): void {
    this.editorContent = value;

    if (!this.loading) {
      this.errorMessage = '';
      this.statusMessage = this.readOnly
        ? 'Documento cargado en modo solo lectura.'
        : 'Cambios locales pendientes de guardar.';
      this.cdr.markForCheck();
    }
  }

  downloadHtml(): void {
    const blob = new Blob([this.editorContent], { type: 'text/html;charset=utf-8' });
    this.downloadBlob(blob, this.ensureExtension(this.documentName, 'html'));
    this.statusMessage = 'Se descargó el documento como HTML.';
    this.errorMessage = '';
    this.cdr.markForCheck();
  }

  async onLocalDocxSelected(event: Event): Promise<void> {
    const input = event.target as HTMLInputElement | null;
    const file = input?.files?.[0];

    if (!file) {
      return;
    }

    if (!this.isDocxFileName(file.name)) {
      this.errorMessage = 'Selecciona un archivo DOCX valido.';
      this.statusMessage = 'No se abrio el archivo seleccionado.';
      this.resetFileInput(input);
      this.cdr.markForCheck();
      return;
    }

    this.openingLocalDocx = true;
    this.errorMessage = '';
    this.statusMessage = 'Abriendo DOCX local...';
    this.cdr.markForCheck();

    try {
      const result = await this.convertDocxToHtml(await file.arrayBuffer());

      this.editorContent = this.normaliseLoadedHtml(result.value);
      this.documentName = this.stripExtension(file.name);
      this.statusMessage = this.buildLocalDocxStatusMessage();
      this.errorMessage = '';
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error opening local DOCX', error);
      this.errorMessage = 'No se pudo abrir el archivo DOCX seleccionado.';
      this.statusMessage = 'Ocurrio un error al procesar el archivo local.';
    } finally {
      this.openingLocalDocx = false;
      this.resetFileInput(input);
      this.cdr.markForCheck();
    }
  }

  async downloadDocx(): Promise<void> {
    this.exporting = true;
    this.errorMessage = '';
    this.statusMessage = 'Generando archivo DOCX...';
    this.cdr.markForCheck();

    try {
      const blob = await this.convertHtmlToDocx(this.buildHtmlDocument(this.editorContent));
      this.downloadBlob(blob, this.ensureExtension(this.documentName, 'docx'));
      this.statusMessage = 'Se descargó el documento como DOCX.';
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error exporting DOCX', error);
      this.errorMessage = 'No se pudo exportar el documento en formato DOCX.';
      this.statusMessage = 'Ocurrió un error al exportar el documento.';
    } finally {
      this.exporting = false;
      this.cdr.markForCheck();
    }
  }

  async saveDocument(): Promise<void> {
    if (this.readOnly || this.saving) {
      return;
    }

    if (!this.zambaService) {
      this.errorMessage = 'ZambaService no está disponible en el contexto del elemento.';
      this.statusMessage = 'No se pudo guardar el documento.';
      this.cdr.markForCheck();
      return;
    }

    const request = this.resolveDocumentRequest();
    if (!request) {
      this.errorMessage = 'Faltan userId, documentId o entityId para guardar el documento.';
      this.statusMessage = 'Completa los identificadores requeridos e intenta nuevamente.';
      this.cdr.markForCheck();
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.statusMessage = 'Guardando documento en Zamba...';
    this.cdr.markForCheck();

    try {
      const docxBlob = await this.convertHtmlToDocx(this.buildHtmlDocument(this.editorContent));
      const base64 = await this.blobToBase64(docxBlob);

      await firstValueFrom(
        this.zambaService.replaceDocument({
          ...request,
          base64
        })
      );

      this.statusMessage = 'Documento guardado correctamente en Zamba.';
      this.errorMessage = '';
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error saving document', error);
      this.errorMessage = 'No se pudo guardar el documento en Zamba.';
      this.statusMessage = 'Ocurrió un error durante el guardado.';
    } finally {
      this.saving = false;
      this.cdr.markForCheck();
    }
  }

  private async loadDocumentFromInputs(): Promise<void> {
    const request = this.resolveDocumentRequest();

    if (!request) {
      this.editorContent = BLANK_DOCUMENT;
      this.documentName = this.buildDefaultDocumentName(this.documentId);
      this.errorMessage = '';
      this.statusMessage = 'Esperando userId, documentId y entityId para cargar el documento.';
      this.cdr.markForCheck();
      return;
    }

    if (this.token && this.tokenService) {
      this.tokenService.set({ token: this.token });
    }

    if (!this.zambaService) {
      this.editorContent = BLANK_DOCUMENT;
      this.documentName = this.buildDefaultDocumentName(request.documentId);
      this.errorMessage = 'ZambaService no está disponible en el contexto del elemento.';
      this.statusMessage = 'Se dejó un documento en blanco porque no fue posible acceder a ZambaService.';
      this.cdr.markForCheck();
      return;
    }

    this.loading = true;
    this.errorMessage = '';
    this.statusMessage = 'Cargando documento...';
    this.cdr.markForCheck();

    try {
      const documentPayload = await firstValueFrom(this.zambaService.getDocumentBase64(request));
      const editorDocument = await this.mapPayloadToEditorDocument(documentPayload, request.documentId);

      this.editorContent = editorDocument.html;
      this.documentName = editorDocument.fileName;
      this.statusMessage = 'Documento cargado correctamente.';
      this.errorMessage = '';
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error loading document', error);
      this.errorMessage = 'No se pudo cargar el documento solicitado.';
      this.statusMessage = 'Se dejó un documento en blanco para continuar editando.';
      this.editorContent = BLANK_DOCUMENT;
      this.documentName = this.buildDefaultDocumentName(request.documentId);
    } finally {
      this.loading = false;
      this.editorInit = this.buildEditorInit();
      this.cdr.markForCheck();
    }
  }

  private resolveDocumentRequest(): ZambaDocumentRequest | null {
    const userId = this.normaliseTextInput(this.userId);
    const documentId = this.normaliseTextInput(this.documentId);
    const entityId = this.normaliseTextInput(this.entityId);

    if (!userId || !documentId || !entityId) {
      return null;
    }

    return {
      userId,
      documentId,
      entityId
    };
  }

  private async mapPayloadToEditorDocument(
    payload: ZambaDocumentPayload,
    fallbackDocumentId: string
  ): Promise<{ html: string; fileName: string }> {
    const bytes = this.base64ToBytes(payload.base64);
    const fileName = this.stripExtension(payload.fileName ?? this.buildDefaultDocumentName(fallbackDocumentId));

    if (this.isDocxPayload(payload, bytes)) {
      const result = await this.convertDocxToHtml(Uint8Array.from(bytes).buffer);
      return {
        html: this.normaliseLoadedHtml(result.value),
        fileName
      };
    }

    return {
      html: this.normaliseLoadedHtml(this.decodeText(bytes)),
      fileName
    };
  }

  private isDocxPayload(payload: ZambaDocumentPayload, bytes: Uint8Array): boolean {
    const extension = payload.extension?.toLowerCase();
    const mimeType = payload.mimeType?.toLowerCase();

    return (
      extension === 'docx' ||
      mimeType === DOCX_MIME_TYPE ||
      (bytes.length >= 4 && bytes[0] === 0x50 && bytes[1] === 0x4b && bytes[2] === 0x03 && bytes[3] === 0x04)
    );
  }

  private normaliseLoadedHtml(value: string): string {
    const trimmedValue = value.trim();

    if (!trimmedValue) {
      return BLANK_DOCUMENT;
    }

    if (this.looksLikeHtml(trimmedValue)) {
      return trimmedValue;
    }

    const normalizedText = trimmedValue.split('\r\n').join('\n');
    const paragraphs = normalizedText
      .split(/\n{2,}/)
      .map((paragraph: string) => `<p>${this.escapeHtml(paragraph).split('\n').join('<br />')}</p>`)
      .join('');

    return paragraphs || BLANK_DOCUMENT;
  }

  private looksLikeHtml(value: string): boolean {
    return /<\/?[a-z][\s\S]*>/i.test(value);
  }

  private buildEditorInit(): Record<string, unknown> {
    return {
      base_url: SELF_HOSTED_BASE_URL,
      branding: false,
      content_style: 'body { font-family: Arial, Helvetica, sans-serif; font-size: 14px; line-height: 1.6; }',
      height: 640,
      menubar: 'file edit view insert format tools table help',
      promotion: false,
      plugins: [
        'advlist',
        'anchor',
        'autolink',
        'charmap',
        'code',
        'fullscreen',
        'help',
        'image',
        'link',
        'lists',
        'media',
        'preview',
        'searchreplace',
        'table',
        'visualblocks',
        'wordcount'
      ],
      quickbars_selection_toolbar: 'bold italic underline | blocks | quicklink blockquote',
      readonly: this.readOnly,
      skin: 'oxide',
      suffix: '.min',
      toolbar:
        'undo redo | blocks fontfamily fontsize | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image table | removeformat code preview fullscreen',
      toolbar_sticky: true
    };
  }

  private buildDefaultDocumentName(documentId?: ElementInputValue): string {
    const normalizedDocumentId = this.normaliseTextInput(documentId);
    return normalizedDocumentId ? `documento-${normalizedDocumentId}` : 'documento';
  }

  private normaliseTextInput(value: ElementInputValue): string {
    if (typeof value === 'string') {
      return value.trim();
    }

    if (typeof value === 'number' && Number.isFinite(value)) {
      return String(value);
    }

    return '';
  }

  private toBoolean(value: BooleanLike): boolean {
    if (typeof value === 'string') {
      const normalizedValue = value.trim().toLowerCase();

      if (!normalizedValue || normalizedValue === 'true' || normalizedValue === '1' || normalizedValue === 'readonly') {
        return true;
      }

      if (normalizedValue === 'false' || normalizedValue === '0' || normalizedValue === 'null' || normalizedValue === 'undefined') {
        return false;
      }
    }

    return Boolean(value);
  }

  private stripExtension(value: string): string {
    return value.replace(/\.(docx|html?)$/i, '') || 'documento';
  }

  private isDocxFileName(fileName: string): boolean {
    return /\.docx$/i.test(fileName.trim());
  }

  private ensureExtension(value: string, extension: 'docx' | 'html'): string {
    return value.toLowerCase().endsWith(`.${extension}`) ? value : `${value}.${extension}`;
  }

  private base64ToBytes(base64Value: string): Uint8Array {
    const sanitizedBase64 = this.removeBase64Prefix(base64Value).split(/\s+/).join('');
    const normalizedBase64 = sanitizedBase64.padEnd(sanitizedBase64.length + ((4 - (sanitizedBase64.length % 4)) % 4), '=');
    const binaryValue = atob(normalizedBase64);

    return Uint8Array.from(binaryValue, character => character.codePointAt(0) ?? 0);
  }

  private removeBase64Prefix(value: string): string {
    return value.replace(/^data:[^;]+;base64,/i, '').trim();
  }

  private buildLocalDocxStatusMessage(): string {
    if (this.readOnly) {
      return 'Documento DOCX local cargado en modo solo lectura.';
    }

    return this.hasDocumentContext
      ? 'Documento DOCX local cargado. Si guardas, se usara el contexto actual de Zamba.'
      : 'Documento DOCX local cargado desde la PC. Para guardar en Zamba siguen siendo necesarios userId, documentId y entityId.';
  }

  private resetFileInput(input: HTMLInputElement | null): void {
    if (input) {
      input.value = '';
    }
  }

  private decodeText(bytes: Uint8Array): string {
    return new TextDecoder('utf-8').decode(bytes).replace(/^\uFEFF/, '');
  }

  private async convertDocxToHtml(arrayBuffer: ArrayBuffer): Promise<MammothResult> {
    const mammothModule = (await import('mammoth')) as unknown as {
      default?: {
        convertToHtml?: (input: { arrayBuffer: ArrayBuffer }, options?: { includeDefaultStyleMap?: boolean }) => Promise<MammothResult>;
      };
      convertToHtml?: (input: { arrayBuffer: ArrayBuffer }, options?: { includeDefaultStyleMap?: boolean }) => Promise<MammothResult>;
    };

    const convertToHtml = mammothModule.default?.convertToHtml ?? mammothModule.convertToHtml;

    if (!convertToHtml) {
      throw new Error('No se pudo inicializar el conversor de DOCX.');
    }

    return convertToHtml(
      { arrayBuffer },
      {
        includeDefaultStyleMap: true
      }
    );
  }

  private async convertHtmlToDocx(html: string): Promise<Blob> {
    const { asBlob } = await import('html-docx-js-typescript');
    const file = await asBlob(html);

    return file instanceof Blob ? file : new Blob([file as BlobPart], { type: DOCX_MIME_TYPE });
  }

  private buildHtmlDocument(body: string): string {
    return `<!DOCTYPE html>
<html lang="es">
  <head>
    <meta charset="UTF-8" />
    <title>${this.escapeHtml(this.documentName || 'documento')}</title>
    <style>
      body {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12pt;
        line-height: 1.6;
        color: #1f1f1f;
      }
      h1, h2, h3 {
        color: #141414;
        margin-bottom: 0.6em;
      }
      p {
        margin: 0 0 0.75em;
      }
      ul, ol {
        margin: 0 0 0.75em 1.5em;
      }
      blockquote {
        margin: 0.75em 0;
        padding-left: 1em;
        border-left: 4px solid #d9d9d9;
        color: #595959;
      }
      table {
        width: 100%;
        border-collapse: collapse;
      }
      td, th {
        border: 1px solid #d9d9d9;
        padding: 8px;
      }
      img {
        max-width: 100%;
      }
    </style>
  </head>
  <body>
    ${body}
  </body>
</html>`;
  }

  private escapeHtml(value: string): string {
    const entityMap: Record<string, string> = {
      '&': '&amp;',
      '<': '&lt;',
      '>': '&gt;',
      '"': '&quot;',
      "'": '&#39;'
    };

    return Array.from(value, character => entityMap[character] ?? character).join('');
  }

  private downloadBlob(blob: Blob, fileName: string): void {
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    anchor.href = url;
    anchor.download = fileName;
    document.body.appendChild(anchor);
    anchor.click();
    anchor.remove();
    globalThis.setTimeout(() => URL.revokeObjectURL(url), 300);
  }

  private async blobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();

      fileReader.onload = () => {
        const result = typeof fileReader.result === 'string' ? fileReader.result : '';
        resolve(result.replace(/^data:[^;]+;base64,/i, ''));
      };

      fileReader.onerror = () => reject(fileReader.error ?? new Error('No se pudo convertir el archivo a base64.'));
      fileReader.readAsDataURL(blob);
    });
  }
}

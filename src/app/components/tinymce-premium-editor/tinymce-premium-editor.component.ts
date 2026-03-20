import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Input, OnChanges, OnInit, SimpleChanges, inject } from '@angular/core';
import { EditorModule, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { ZambaDocumentPayload, ZambaDocumentRequest, ZambaService } from '../../services/zamba/zamba.service';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { firstValueFrom } from 'rxjs';

const SELF_HOSTED_ASSET_PATH = 'assets/tinymce';
const SELF_HOSTED_BASE_URL = resolveTinyMceAssetUrl();
const SELF_HOSTED_SCRIPT_SRC = resolveTinyMceAssetUrl('tinymce.min.js');
const LICENSE_KEY = 'gpl';
const BLANK_DOCUMENT = '<p></p>';
const DOCX_MIME_TYPE = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';

type IdentifierInputValue = number | string | null | undefined;

interface MammothMessage {
  message: string;
}

interface MammothResult {
  value: string;
  messages: MammothMessage[];
}

interface TinyMceSettings {
  readonly: boolean;
}

const DEFAULT_SETTINGS: TinyMceSettings = {
  readonly: false
};

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
  selector: 'app-tinymce-premium-editor',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    EditorModule,
    NzAlertModule,
    NzButtonModule,
    NzCardModule,
    NzMessageModule,
    NzSwitchModule
  ],
  providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: SELF_HOSTED_SCRIPT_SRC }],
  templateUrl: './tinymce-premium-editor.component.html',
  styleUrls: ['./tinymce-premium-editor.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TinymcePremiumEditorComponent implements OnChanges, OnInit {
  @Input() userId?: IdentifierInputValue;
  @Input() documentId?: IdentifierInputValue;
  @Input() entityId?: IdentifierInputValue;

  readonly licenseKey = LICENSE_KEY;

  settings: TinyMceSettings = { ...DEFAULT_SETTINGS };
  documentTitle = this.buildDefaultDocumentName();
  editorContent = BLANK_DOCUMENT;
  editorInit = this.buildEditorInit();
  loading = false;
  openingLocalDocx = false;
  exporting = false;
  saving = false;
  statusMessage = 'Esperando userId, documentId y entityId para cargar el documento.';
  errorMessage = '';

  private readonly cdr = inject(ChangeDetectorRef);
  private readonly message = inject(NzMessageService);
  private readonly zambaService = inject(ZambaService);

  get hasDocumentContext(): boolean {
    return this.resolveDocumentRequest() !== null;
  }

  get isEditable(): boolean {
    return !this.settings.readonly;
  }

  ngOnInit(): void {
    const documentRequest = this.zambaService.getDocument(window.location.href);
    console.log('ZambaService.getDocument result:', documentRequest);

    if (documentRequest) {
      this.userId = documentRequest.userId;
      this.documentId = documentRequest.documentId;
      this.entityId = documentRequest.entityId;
      void this.loadDocument();
    }
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['userId'] || changes['documentId'] || changes['entityId']) {
      void this.loadDocument();
    }
  }

  setEditable(value: boolean): void {
    this.settings = { readonly: !value };
    this.editorInit = this.buildEditorInit();
    this.cdr.markForCheck();
  }

  reload(): void {
    void this.loadDocument();
  }

  createBlankDocument(): void {
    this.editorContent = BLANK_DOCUMENT;
    this.documentTitle = this.buildDefaultDocumentName();
    this.errorMessage = '';
    this.statusMessage = 'Nuevo documento.';
    this.cdr.markForCheck();
  }

  downloadHtml(): void {
    const blob = new Blob([this.editorContent], { type: 'text/html;charset=utf-8' });
    this.downloadBlob(blob, this.ensureExtension(this.documentTitle, 'html'));
    this.statusMessage = 'Se descargó el documento como HTML.';
    this.errorMessage = '';
    this.cdr.markForCheck();
  }

  async downloadDocx(): Promise<void> {
    this.exporting = true;
    this.errorMessage = '';
    this.statusMessage = 'Generando archivo DOCX...';
    this.cdr.markForCheck();

    try {
      const blob = await this.convertHtmlToDocx(this.buildHtmlDocument(this.editorContent));
      this.downloadBlob(blob, this.ensureExtension(this.documentTitle, 'docx'));
      this.statusMessage = 'Se descargó el documento como DOCX.';
      this.message.success('Se descargó el documento como DOCX.');
    } catch (error) {
      console.error('[tinymce-premium-editor] Error exporting DOCX', error);
      this.errorMessage = 'No se pudo exportar el documento en formato DOCX.';
      this.statusMessage = 'Ocurrió un error al exportar el documento.';
      this.message.error('No se pudo exportar el documento en formato DOCX.');
    } finally {
      this.exporting = false;
      this.cdr.markForCheck();
    }
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
      this.message.warning('Selecciona un archivo DOCX valido.');
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
      this.documentTitle = this.stripExtension(file.name);
      this.statusMessage = this.hasDocumentContext
        ? 'Documento DOCX local cargado. Si guardas, se usara el contexto actual de Zamba.'
        : 'Documento DOCX local cargado desde la PC. Para guardar en Zamba siguen siendo necesarios userId, documentId y entityId.';
    } catch (error) {
      console.error('[tinymce-premium-editor] Error opening local DOCX', error);
      this.errorMessage = 'No se pudo abrir el archivo DOCX seleccionado.';
      this.statusMessage = 'Ocurrio un error al procesar el archivo local.';
      this.message.error('No se pudo abrir el archivo DOCX seleccionado.');
    } finally {
      this.openingLocalDocx = false;
      this.resetFileInput(input);
      this.cdr.markForCheck();
    }
  }

  async saveDocument(): Promise<void> {
    if (this.settings.readonly || this.saving) {
      return;
    }

    const request = this.resolveDocumentRequest();
    if (!request) {
      this.message.warning('Completa userId, documentId y entityId para guardar el documento.');
      return;
    }

    this.saving = true;
    this.errorMessage = '';
    this.statusMessage = 'Guardando documento en Zamba...';
    this.cdr.markForCheck();

    try {
      // Usamos el metodo manual con JSZip y AltChunk
      const docxBlob = await this.generateDocxWithAltChunk(this.editorContent);

      const base64 = await this.blobToBase64(docxBlob);

      await firstValueFrom(
        this.zambaService.replaceDocument({
          ...request,
          base64,
          fileName: this.ensureExtension(this.documentTitle, 'docx')
        })
      );

      this.statusMessage = 'El documento se guardó correctamente en Zamba.';
      this.message.success('El documento se guardó correctamente en Zamba.');
    } catch (error) {
      console.error('[tinymce-premium-editor] Error saving document', error);
      this.errorMessage = 'No se pudo guardar el documento en Zamba.';
      this.statusMessage = 'Ocurrió un error durante el guardado.';
      this.message.error('No se pudo guardar el documento en Zamba.');
    } finally {
      this.saving = false;
      this.cdr.markForCheck();
    }
  }

  private async loadDocument(): Promise<void> {
    const request = this.resolveDocumentRequest();

    if (!request) {
      this.editorContent = BLANK_DOCUMENT;
      this.documentTitle = this.buildDefaultDocumentName();
      this.errorMessage = '';
      this.statusMessage = 'Esperando userId, documentId y entityId para cargar el documento.';
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
      this.documentTitle = editorDocument.fileName;

    } catch (error) {
      this.editorContent = BLANK_DOCUMENT;
      this.documentTitle = this.buildDefaultDocumentName(request.documentId);
    } finally {
      this.loading = false;
      this.editorInit = this.buildEditorInit();
      this.statusMessage = '';
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
    const fileName = payload.fileName?.toLowerCase();

    return (
      extension === 'docx' ||
      mimeType === DOCX_MIME_TYPE ||
      fileName?.endsWith('.docx') ||
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
    const plugins = [
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
    ];

    return {
      base_url: SELF_HOSTED_BASE_URL,
      branding: false,
      content_style: 'body { font-family: Arial, Helvetica, sans-serif; font-size: 14px; line-height: 1.6; }',
      height: 720,
      menubar: 'file edit view insert format tools table help',
      promotion: false,
      plugins,
      quickbars_selection_toolbar: 'bold italic underline | blocks | quicklink blockquote',
      readonly: this.settings.readonly,
      skin: 'oxide',
      suffix: '.min',
      toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image table | removeformat code preview fullscreen',
      toolbar_sticky: true,
      setup: (editor: any) => {
        editor.on('Change KeyUp', () => {
          this.editorContent = editor.getContent();
          this.cdr.markForCheck();
        });
      }
    };
  }

  private buildDefaultDocumentName(documentId: IdentifierInputValue = this.documentId): string {
    const normalizedDocumentId = this.normaliseTextInput(documentId);
    return normalizedDocumentId ? `documento-${normalizedDocumentId}` : 'documento';
  }

  private normaliseTextInput(value: IdentifierInputValue): string {
    if (typeof value === 'string') {
      return value.trim();
    }

    if (typeof value === 'number' && Number.isFinite(value)) {
      return String(value);
    }

    return '';
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

    const result = await convertToHtml(
      { arrayBuffer },
      {
        includeDefaultStyleMap: true
      }
    );

    // Si Mammoth detecta altChunk y el resultado está vacío, intentamos extraer manualmente
    if (!result.value.trim() && result.messages.some(m => m.message.includes('altChunk'))) {
      const fallbackHtml = await this.extractAltChunkHtml(arrayBuffer);
      if (fallbackHtml) {
        return {
          value: fallbackHtml,
          messages: result.messages
        };
      }
    }

    return result;
  }

  private async extractAltChunkHtml(arrayBuffer: ArrayBuffer): Promise<string | null> {
    try {
      console.log('Iniciando extracción manual de AltChunk con JSZip...');
      const JSZip = (await import('jszip')).default;
      const zip = await JSZip.loadAsync(arrayBuffer);

      const files = Object.keys(zip.files);
      console.log('Archivos encontrados en el DOCX:', files);

      const htmlFiles = files.filter(filename => filename.endsWith('.html') || filename.endsWith('.htm'));
      console.log('Archivos HTML candidatos:', htmlFiles);

      if (htmlFiles.length === 0) {
        console.warn('No se encontraron archivos HTML dentro del DOCX.');
        return null;
      }

      // Intentar leer el contenido del archivo que parezca ser el chunk principal
      // Priorizamos nombres comunes de altChunk generados por librerías
      const contentFile = htmlFiles.find(f => f.includes('htmlChunk') || f.includes('content') || f.includes('document')) || htmlFiles[0];

      console.log('Intentando extraer contenido de:', contentFile);
      const content = await zip.file(contentFile)?.async('string');

      if (content) {
        console.log('Contenido extraído (longitud):', content.length);
        return content;
      }
      return null;
    } catch (e) {
      console.error('Error crítico al intentar extraer altChunk HTML:', e);
      return null;
    }
  }

  private async generateDocxWithAltChunk(htmlContent: string): Promise<Blob> {
    const JSZip = (await import('jszip')).default;
    const zip = new JSZip();

    // 1. [Content_Types].xml
    const contentTypes = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
  <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
  <Default Extension="xml" ContentType="application/xml"/>
  <Default Extension="html" ContentType="text/html"/>
  <Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/>
</Types>`;
    zip.file('[Content_Types].xml', contentTypes);

    // 2. _rels/.rels
    const rels = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
  <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
</Relationships>`;
    zip.file('_rels/.rels', rels);

    // 3. word/window.xml
    const documentXml = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
  <w:body>
    <w:altChunk r:id="htmlChunk" />
  </w:body>
</w:document>`;
    zip.folder('word')?.file('document.xml', documentXml);

    // 4. word/_rels/document.xml.rels (Map HTML file)
    const documentRels = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
  <Relationship Id="htmlChunk" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk" Target="htmlChunk.html"/>
</Relationships>`;
    zip.folder('word')?.folder('_rels')?.file('document.xml.rels', documentRels);

    // 5. word/htmlChunk.html (El contenido real)
    // Aseguramos una estructura basica de HTML para que Word lo interprete mejor
    const fullHtml = this.buildHtmlDocument(htmlContent);
    // IMPORTANTE: Aseguramos UTF-8 y Byte-Order-Mark (BOM) para que Word reconozca caracteres especiales
    const htmlBlob = new Blob(['\uFEFF', fullHtml], { type: 'text/html;charset=utf-8' });
    zip.folder('word')?.file('htmlChunk.html', htmlBlob);

    // Generar Blob
    return await zip.generateAsync({ type: 'blob', mimeType: DOCX_MIME_TYPE });
  }

  // Se mantiene como fallback o referencia, pero ya no se usa en saveDocument
  private async convertHtmlToDocx(html: string): Promise<Blob> {
    const { asBlob } = await import('html-docx-js-typescript');
    const file = await asBlob(html);
    return file instanceof Blob ? file : new Blob([file as BlobPart], { type: DOCX_MIME_TYPE });
  }

  private buildHtmlDocument(body: string): string {
    return `<!DOCTYPE html>
<html lang="es">
  <head>
    <meta charset="UTF-8">
    <title>${this.escapeHtml(this.documentTitle || 'documento')}</title>
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

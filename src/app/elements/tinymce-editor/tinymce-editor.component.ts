import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, Input, OnChanges, OnInit, Optional, SimpleChanges, ViewChild, ElementRef, ViewEncapsulation } from '@angular/core';
import { DA_SERVICE_TOKEN, ITokenService } from '@delon/auth';
import { DOCUMENT } from '@angular/common';
import { firstValueFrom } from 'rxjs';
import { asBlob } from 'html-docx-js-typescript';
import * as mammoth from 'mammoth';
import JSZip from 'jszip';
import { toDocx } from 'docshift';

import { ZambaDocumentPayload, ZambaDocumentRequest, ZambaService } from '../../services/zamba/zamba.service';

const LICENSE_KEY = 'gpl';
const BLANK_DOCUMENT = '<p></p>';
const DOCX_MIME_TYPE = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';

// Declare TinyMCE interface for global access
declare const tinymce: any;

type ElementInputValue = number | string | null | undefined;
type BooleanLike = boolean | string | null | undefined;

interface MammothMessage {
  message: string;
}

interface MammothResult {
  value: string;
  messages: MammothMessage[];
}

@Component({
  selector: 'app-tinymce-element',
  templateUrl: './tinymce-editor.component.html',
  styleUrls: ['./tinymce-editor.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None
})
export class TinymceElementComponent implements OnChanges, OnInit {
  @Input() userId?: ElementInputValue;
  @Input() documentId?: ElementInputValue;
  @Input() entityId?: ElementInputValue;
  @Input() token?: string | null;
  /**
   * Base URL for TinyMCE assets. Defaults to 'assets/tinymce' for local development.
   * Override this when using the web component in a different environment.
   */
  @Input() assetsUrl: string = 'assets/tinymce';

  /**
   * Altura del editor. Puede ser un número (píxeles) o string ('100%', '500px').
   * Si no se especifica, usa el valor por defecto: 720.
   */
  @Input() height?: ElementInputValue;

  /**
   * Ancho del editor. Puede ser un número (píxeles) o string ('100%', '500px').
   * Si no se especifica, usa el ancho disponible (width: '100%').
   */
  @Input() width?: ElementInputValue;

  @Input()
  set readOnly(value: BooleanLike) {
    this._readOnly = this.toBoolean(value);
    this.cdr.markForCheck();
  }

  get readOnly(): boolean {
    return this._readOnly;
  }

  @ViewChild('localDocxInput') localDocxInput!: ElementRef<HTMLInputElement>;

  readonly licenseKey = LICENSE_KEY;

  editorContent = BLANK_DOCUMENT;
  editorInit: Record<string, unknown> = {};
  documentName = this.buildDefaultDocumentName();
  loading = false;
  openingLocalDocx = false;
  exporting = false;
  saving = false;
  statusMessage = '';
  errorMessage = '';
  isTinymceLoaded = false;

  private _readOnly = false;

  constructor(
    private readonly cdr: ChangeDetectorRef,
    @Optional() private readonly zambaService: ZambaService | null,
    @Optional() @Inject(DA_SERVICE_TOKEN) private readonly tokenService: ITokenService | null,
    @Inject(DOCUMENT) private readonly document: Document
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

  setEditable(value: boolean): void {
    if (this._readOnly === !value) {
      return;
    }
    this._readOnly = !value;
    this.cdr.detectChanges();
  }

  ngOnInit(): void {
    this.loadTinyMce().then(() => {
      this.registerSpanishUiTexts();
      this.isTinymceLoaded = true;
      this.editorInit = this.buildEditorInit();
      this.cdr.markForCheck();
    });

    if (!this.zambaService) {
      return;
    }

    this.zambaService.ensureAuthToken().subscribe(hasToken => {
      if (hasToken) {
        if (this.userId && this.documentId && this.entityId) {
          void this.loadDocumentFromInputs();
        } else {
          const documentRequest = this.zambaService!.getDocument(window.location.href);

          if (documentRequest) {
            this.userId = documentRequest.userId;
            this.documentId = documentRequest.documentId;
            this.entityId = documentRequest.entityId;
            void this.loadDocumentFromInputs();
          }
        }
      } else {
        console.error('No se pudo obtener el token de autenticación.');
        this.errorMessage = 'No se pudo obtener el token de autenticación.';
        this.cdr.markForCheck();
      }
    });
  }

  private loadTinyMce(): Promise<void> {
    if (typeof tinymce !== 'undefined') {
      return Promise.resolve();
    }

    const scriptUrl = this.resolveAssetUrl('tinymce.min.js');

    return new Promise((resolve, reject) => {
      const script = this.document.createElement('script');
      script.src = scriptUrl;
      script.onload = () => resolve();
      script.onerror = () => reject(new Error(`Could not load TinyMCE from ${scriptUrl}`));
      this.document.head.appendChild(script);
    });
  }

  private resolveAssetUrl(pathSuffix?: string): string {
    const assetsUrl = this.assetsUrl || 'assets/tinymce';
    const basePath = assetsUrl.endsWith('/') ? assetsUrl : `${assetsUrl}/`;
    const relativePath = pathSuffix ? `${basePath}${pathSuffix}` : basePath;
    const baseUri = this.document.baseURI ?? globalThis.location?.href;

    if (!baseUri) {
      return relativePath;
    }

    if (basePath.startsWith('/') || basePath.startsWith('http')) {
      const url = new URL(relativePath, baseUri).toString();
      return pathSuffix ? url : url.replace(/\/$/, '');
    }

    const resolvedUrl = new URL(relativePath, baseUri).toString();
    return pathSuffix ? resolvedUrl : resolvedUrl.replace(/\/$/, '');
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
    // SILENT NEW DOC: No message shown
    this.statusMessage = '';
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
      // SILENT SUCCESS: Message removed
      this.statusMessage = '';
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
    this.cdr.markForCheck();

    try {
      const blob = await toDocx(this.editorContent);
      this.downloadBlob(blob, this.ensureExtension(this.documentName, 'docx'));
      this.statusMessage = '';
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
      const docxBlob = await this.generateDocxWithAltChunk(this.editorContent);
      const base64 = await this.blobToBase64(docxBlob);

      await firstValueFrom(
        this.zambaService.replaceDocument({
          ...request,
          base64,
          fileName: this.ensureExtension(this.documentName, 'docx')
        })
      );

      this.statusMessage = 'El documento se guardó correctamente en Zamba.';
      this.errorMessage = '';
      this.autoCloseStatusMessage();
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error saving document', error);
      this.errorMessage = 'No se pudo guardar el documento en Zamba.';
      this.statusMessage = 'Ocurrió un error durante el guardado.';
      this.autoCloseStatusMessage();
    } finally {
      this.saving = false;
      this.cdr.markForCheck();
    }
  }

  private autoCloseStatusMessage(): void {
    globalThis.setTimeout(() => {
      this.statusMessage = '';
      this.cdr.markForCheck();
    }, 2000);
  }

  private async loadDocumentFromInputs(): Promise<void> {
    const request = this.resolveDocumentRequest();

    if (!request) {
      this.editorContent = BLANK_DOCUMENT;
      this.documentName = this.buildDefaultDocumentName(this.documentId);
      this.errorMessage = '';
      this.statusMessage = '';
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
    // SILENT LOADING: No message shown
    this.statusMessage = '';
    this.cdr.markForCheck();

    try {
      const documentPayload = await firstValueFrom(this.zambaService.getDocumentBase64(request));
      const editorDocument = await this.mapPayloadToEditorDocument(documentPayload, request.documentId);

      this.editorContent = editorDocument.html;
      this.documentName = editorDocument.fileName;
      // SILENT SUCCESS: Message removed
      this.statusMessage = '';
      this.errorMessage = '';
    } catch (error) {
      console.error('[zamba-tinymce-editor] Error loading document', error);
      // SILENT ERROR
      this.errorMessage = '';
      this.statusMessage = '';
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

  private registerSpanishUiTexts(): void {
    if (typeof tinymce === 'undefined' || typeof tinymce.addI18n !== 'function') {
      return;
    }

    tinymce.addI18n('en', {
      'File': 'Archivo',
      'Edit': 'Editar',
      'View': 'Ver',
      'Insert': 'Insertar',
      'Format': 'Formato',
      'Tools': 'Herramientas',
      'Table': 'Tabla',
      'Help': 'Ayuda',
      'New document': 'Nuevo documento',
      'New Document': 'Nuevo documento',
      'Print': 'Imprimir',
      'Print...': 'Imprimir...',
      'Save': 'Guardar',
      'Restore last draft': 'Restaurar ultimo borrador',
      'Open help dialog': 'Abrir ayuda',
      'Undo': 'Deshacer',
      'Redo': 'Rehacer',
      'Cut': 'Cortar',
      'Copy': 'Copiar',
      'Paste': 'Pegar',
      'Paste as text': 'Pegar como texto',
      'Paste as text...': 'Pegar como texto...',
      'Select all': 'Seleccionar todo',
      'Find and replace': 'Buscar y reemplazar',
      'Find and replace...': 'Buscar y reemplazar...',
      'Bold': 'Negrita',
      'Italic': 'Cursiva',
      'Underline': 'Subrayado',
      'Strikethrough': 'Tachado',
      'Superscript': 'Superindice',
      'Subscript': 'Subindice',
      'Blocks': 'Bloques',
      'Headings': 'Encabezados',
      'Heading 1': 'Encabezado 1',
      'Heading 2': 'Encabezado 2',
      'Heading 3': 'Encabezado 3',
      'Heading 4': 'Encabezado 4',
      'Heading 5': 'Encabezado 5',
      'Heading 6': 'Encabezado 6',
      'Paragraph': 'Parrafo',
      'Inline': 'En linea',
      'Div': 'Division',
      'Pre': 'Preformateado',
      'Code': 'Codigo',
      'Font': 'Fuente',
      'Font family': 'Familia tipografica',
      'Fonts': 'Fuentes',
      'Size': 'Tamano',
      'Font sizes': 'Tamanos de fuente',
      'Text color': 'Color de texto',
      'Background color': 'Color de fondo',
      'Align left': 'Alinear a la izquierda',
      'Align center': 'Centrar',
      'Align right': 'Alinear a la derecha',
      'No alignment': 'Sin alineacion',
      'Justify': 'Justificar',
      'Bullet list': 'Lista con viñetas',
      'Disc': 'Disco',
      'Circle': 'Circulo',
      'Square': 'Cuadrado',
      'Numbered list': 'Lista numerada',
      'Lower Alpha': 'Alfabetica minuscula',
      'Lower Greek': 'Griego minusculo',
      'Lower Roman': 'Romano minusculo',
      'Upper Alpha': 'Alfabetica mayuscula',
      'Upper Roman': 'Romano mayusculo',
      'Increase indent': 'Aumentar sangría',
      'Decrease indent': 'Disminuir sangría',
      'Line height': 'Altura de linea',
      'Formats': 'Formatos',
      'Remove': 'Quitar',
      'Insert/edit link': 'Insertar/editar enlace',
      'Link': 'Enlace',
      'Anchor': 'Ancla',
      'Insert/edit media': 'Insertar/editar multimedia',
      'Insert/edit image': 'Insertar/editar imagen',
      'Insert image': 'Insertar imagen',
      'Insert table': 'Insertar tabla',
      'Special character': 'Caracter especial',
      'Special Character': 'Caracter especial',
      'Horizontal line': 'Linea horizontal',
      'Page break': 'Salto de pagina',
      'Nonbreaking space': 'Espacio de no separacion',
      'Clear formatting': 'Quitar formato',
      'Source code': 'Código fuente',
      'Preview': 'Vista previa',
      'Visual aids': 'Ayudas visuales',
      'Visual blocks': 'Bloques visuales',
      'Show blocks': 'Mostrar bloques',
      'Fullscreen': 'Pantalla completa',
      'Align': 'Alineacion',
      'Left': 'Izquierda',
      'Center': 'Centrado',
      'Right': 'Derecha',
      'Word count': 'Recuento de palabras',
      'Search': 'Buscar',
      'Replace': 'Reemplazar',
      'Source': 'Origen',
      'Alternative description': 'Descripcion alternativa',
      'Width': 'Ancho',
      'Height': 'Alto',
      'Reveal or hide additional toolbar items': 'Mostrar más opciones',
      'Reveal or hide additional toolbar items.': 'Mostrar más opciones.',
      'Table properties': 'Propiedades de tabla',
      'Delete table': 'Eliminar tabla',
      'Cell': 'Celda',
      'Row': 'Fila',
      'Column': 'Columna',
      'Cell properties': 'Propiedades de celda',
      'Merge cells': 'Combinar celdas',
      'Split cell': 'Dividir celda',
      'Insert row before': 'Insertar fila antes',
      'Insert row after': 'Insertar fila despues',
      'Delete row': 'Eliminar fila',
      'Row properties': 'Propiedades de fila',
      'Cut row': 'Cortar fila',
      'Copy row': 'Copiar fila',
      'Paste row before': 'Pegar fila antes',
      'Paste row after': 'Pegar fila despues',
      'Insert column before': 'Insertar columna antes',
      'Insert column after': 'Insertar columna despues',
      'Delete column': 'Eliminar columna',
      'Cut column': 'Cortar columna',
      'Copy column': 'Copiar columna',
      'Paste column before': 'Pegar columna antes',
      'Paste column after': 'Pegar columna despues'
    });
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
      base_url: this.resolveAssetUrl(),
      branding: false,
      content_style: 'body { font-family: Arial, Helvetica, sans-serif; font-size: 14px; line-height: 1.6; }',
      height: this.height ?? 720,
      width: this.width ?? '100%',
      menubar: 'file edit view insert format tools table help',
      promotion: false,
      automatic_uploads: false,
      paste_data_images: true,
      plugins,
      quickbars_selection_toolbar: 'bold italic underline | blocks | quicklink blockquote',
      readonly: this.readOnly,
      skin: 'oxide',
      suffix: '.min',
      toolbar: 'save_zamba open_docx download_docx reload_doc new_doc toggle_edit | undo redo | blocks fontfamily fontsize | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image table | removeformat code preview fullscreen',
      toolbar_sticky: true,
      file_picker_callback: (callback: any, value: any, meta: any) => {
        if (meta.filetype === 'image') {
          const input = document.createElement('input');
          input.setAttribute('type', 'file');
          input.setAttribute('accept', 'image/*');

          input.onchange = (e: Event) => {
            const file = (e.target as HTMLInputElement).files?.[0];
            if (file) {
              const reader = new FileReader();
              reader.onload = () => {
                const id = 'blobid' + (new Date()).getTime();
                const blobCache = tinymce.activeEditor.editorUpload.blobCache;
                const base64 = (reader.result as string).split(',')[1];
                const blobInfo = blobCache.create(id, file, base64);
                blobCache.add(blobInfo);
                callback(blobInfo.blobUri(), { title: file.name });
              };
              reader.readAsDataURL(file);
            }
          };

          input.click();
        }
      },
      setup: (editor: any) => {
        editor.on('Change KeyUp', () => {
          this.editorContent = editor.getContent();
          this.cdr.markForCheck();
        });

        editor.ui.registry.addButton('reload_doc', {
          icon: 'reload',
          tooltip: 'Recargar documento',
          onAction: () => this.reload()
        });

        editor.ui.registry.addButton('open_docx', {
          icon: 'folder',
          tooltip: 'Abrir DOCX local',
          onAction: () => this.localDocxInput?.nativeElement.click()
        });

        editor.ui.registry.addButton('new_doc', {
          icon: 'new-document',
          tooltip: 'Nuevo documento en blanco',
          onAction: () => this.createBlankDocument()
        });

        editor.ui.registry.addIcon(
          'floppy',
          `<svg width="24" height="24">
            <g transform="scale(0.24)">
              <path d="M 8.955 10.021 L 71.81 10.021 L 89.552 27.763 L 89.552 90.618 L 8.955 90.618 Z"
                    style="fill: rgb(255, 255, 255); stroke: rgb(0, 0, 0); stroke-width: 6px; stroke-linejoin: round; stroke-linecap: round;" />
              <rect x="24.844" y="9.917" width="39.393" height="28.046"
                    style="stroke: rgb(0, 0, 0); stroke-width: 0px;" />
              <path d="M 27.352 57.938 L 73.128 57.938 L 73.128 90.635 L 27.352 90.635 L 27.352 57.938 Z"
                    style="fill: rgb(216, 216, 216); stroke: rgb(0, 0, 0); stroke-linejoin: round; stroke-width: 6px;" />
            </g>
          </svg>`
        );

        editor.ui.registry.addButton('save_zamba', {
          icon: 'floppy',
          tooltip: 'Guardar en Zamba',
          onAction: () => this.saveDocument()
        });

        editor.ui.registry.addButton('download_docx', {
          icon: 'save',
          tooltip: 'Descargar DOCX',
          onAction: () => this.downloadDocx()
        });

        editor.ui.registry.addToggleButton('toggle_edit', {
          icon: 'lock',
          tooltip: 'Bloquear/Desbloquear edición',
          onAction: (api: any) => {
            const isCurrentlyLocked = api.isActive();
            const newLockedState = !isCurrentlyLocked; // Si estaba Locked, ahora Unlocked.

            api.setActive(newLockedState); // Actualizamos estado visual
            this.setEditable(!newLockedState); // Si newLockedState=TRUE (Locked) -> editable=FALSE
          },
          onSetup: (api: any) => {
            api.setActive(this._readOnly);
            const forceEnable = () => api.setEnabled(true);
            setTimeout(forceEnable, 0);
            editor.on('SwitchMode', forceEnable);
            return () => editor.off('SwitchMode', forceEnable);
          }
        });
      }
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
    const convertToHtml = mammoth.convertToHtml;

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
      const zip = await JSZip.loadAsync(arrayBuffer);
      const files = Object.keys(zip.files);
      const htmlFiles = files.filter(f => f.endsWith('.html') || f.endsWith('.htm'));

      if (htmlFiles.length === 0) {
        return null;
      }

      const contentFile = htmlFiles.find(f => f.includes('htmlChunk') || f.includes('content') || f.includes('document')) || htmlFiles[0];
      return await zip.file(contentFile)?.async('string') ?? null;
    } catch (e) {
      console.error('[zamba-tinymce-editor] Error extracting altChunk HTML:', e);
      return null;
    }
  }

  private async generateDocxWithAltChunk(htmlContent: string): Promise<Blob> {
    const zip = new JSZip();

    // 1. [Content_Types].xml
    // Added Override for htmlChunk.html for better Word compatibility
    const contentTypes = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
  <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
  <Default Extension="xml" ContentType="application/xml"/>
  <Default Extension="html" ContentType="text/html"/>
  <Override PartName="/word/document.xml" ContentType="application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml"/>
  <Override PartName="/word/htmlChunk.html" ContentType="text/html"/>
</Types>`;
    zip.file('[Content_Types].xml', contentTypes);

    // 2. _rels/.rels
    const rels = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
  <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="word/document.xml"/>
</Relationships>`;
    zip.file('_rels/.rels', rels);

    // 3. word/document.xml
    const documentXml = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<w:document xmlns:w="http://schemas.openxmlformats.org/wordprocessingml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
  <w:body>
    <w:altChunk r:id="htmlChunk" />
  </w:body>
</w:document>`;
    zip.file('word/document.xml', documentXml);

    // 4. word/_rels/document.xml.rels
    const documentRels = `<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
<Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
  <Relationship Id="htmlChunk" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/aFChunk" Target="htmlChunk.html"/>
</Relationships>`;
    zip.file('word/_rels/document.xml.rels', documentRels);

    // 5. word/htmlChunk.html
    const fullHtml = this.buildHtmlDocument(htmlContent);

    const htmlBlob = new Blob(['\uFEFF', fullHtml], { type: 'text/html;charset=utf-8' });
    zip.file('word/htmlChunk.html', htmlBlob);

    return await zip.generateAsync({ type: 'blob', mimeType: DOCX_MIME_TYPE });
  }

  private async convertHtmlToDocx(html: string): Promise<Blob> {
    const file = await asBlob(html);
    return file instanceof Blob ? file : new Blob([file as BlobPart], { type: DOCX_MIME_TYPE });
  }

  private buildHtmlDocument(body: string): string {
    // Si el contenido ya parece ser un documento HTML completo, no lo volvemos a envolver
    if (body.trim().match(/^<!DOCTYPE html>/i) || body.includes('<html')) {
      return body;
    }

    return `<!DOCTYPE html>
<html lang="es">
  <head>
    <meta charset="UTF-8">
    <title>${this.escapeHtml(this.documentName || 'documento')}</title>
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

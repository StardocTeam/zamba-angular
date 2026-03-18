import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  ElementRef,
  ViewChild,
  inject
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzSpinModule } from 'ng-zorro-antd/spin';

const DOCX_MIME_TYPE = 'application/vnd.openxmlformats-officedocument.wordprocessingml.document';
const DEFAULT_DOCUMENT_HTML = '<h1>Nuevo documento</h1><p>Escribe aquí el contenido del documento.</p>';
const EMPTY_DOCUMENT_HTML = '<p></p>';

interface MammothMessage {
  message: string;
}

interface MammothResult {
  value: string;
  messages: MammothMessage[];
}

interface LegacyRichTextDocument {
  execCommand(commandId: string, showUI?: boolean, value?: string): boolean;
}

@Component({
  selector: 'app-docx-editor',
  standalone: true,
  imports: [CommonModule, FormsModule, NzButtonModule, NzCardModule, NzIconModule, NzInputModule, NzMessageModule, NzSpinModule],
  templateUrl: './docx-editor.component.html',
  styleUrls: ['./docx-editor.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DocxEditorComponent {
  private editorSurface?: ElementRef<HTMLDivElement>;

  @ViewChild('editorSurface')
  private set editorSurfaceRef(value: ElementRef<HTMLDivElement> | undefined) {
    this.editorSurface = value;
    this.renderDocument();
  }

  documentName = 'documento';
  currentFileName?: string;
  documentHtml = DEFAULT_DOCUMENT_HTML;
  conversionMessages: string[] = [];
  isBusy = false;
  isSaving = false;

  private readonly cdr = inject(ChangeDetectorRef);
  private readonly message = inject(NzMessageService);

  createNewDocument(): void {
    this.currentFileName = undefined;
    this.conversionMessages = [];
    this.documentName = 'documento';
    this.setDocumentHtml(DEFAULT_DOCUMENT_HTML);
    this.message.info('Documento nuevo listo para editar.');
  }

  async onFileSelected(event: Event): Promise<void> {
    const input = event.target as HTMLInputElement | null;
    const file = input?.files?.item(0);

    if (!file) {
      return;
    }

    if (!file.name.toLowerCase().endsWith('.docx')) {
      this.message.warning('Selecciona un archivo con extensión .docx.');
      if (input) {
        input.value = '';
      }
      return;
    }

    this.isBusy = true;
    this.cdr.markForCheck();

    try {
      const result = await this.convertDocxToHtml(await file.arrayBuffer());
      this.currentFileName = file.name;
      this.documentName = this.stripDocxExtension(file.name) || 'documento';
      this.conversionMessages = result.messages.map(item => item.message);
      this.setDocumentHtml(result.value);
      this.message.success('Documento DOCX cargado correctamente.');
    } catch (error) {
      console.error('Error al abrir el documento DOCX:', error);
      this.message.error('No se pudo abrir el documento DOCX.');
    } finally {
      this.isBusy = false;
      if (input) {
        input.value = '';
      }
      this.cdr.markForCheck();
    }
  }

  async saveDocument(): Promise<void> {
    this.captureEditorContent();
    this.isSaving = true;
    this.cdr.markForCheck();

    try {
      const blob = await this.convertHtmlToDocx(this.buildHtmlDocument(this.documentHtml));
      this.downloadBlob(blob, this.ensureDocxExtension(this.sanitizeFileName(this.documentName)));
      this.message.success('Documento guardado correctamente.');
    } catch (error) {
      console.error('Error al guardar el documento DOCX:', error);
      this.message.error('No se pudo guardar el documento DOCX.');
    } finally {
      this.isSaving = false;
      this.cdr.markForCheck();
    }
  }

  onEditorInput(): void {
    this.captureEditorContent();
  }

  onToolbarMouseDown(event: MouseEvent): void {
    event.preventDefault();
  }

  formatBlock(tag: 'p' | 'h1' | 'h2' | 'blockquote'): void {
    this.executeEditorCommand('formatBlock', `<${tag}>`);
  }

  applyCommand(command: string): void {
    this.executeEditorCommand(command);
  }

  private executeEditorCommand(command: string, value?: string): void {
    this.focusEditor();
    (document as unknown as LegacyRichTextDocument).execCommand(command, false, value);
    this.captureEditorContent();
  }

  private focusEditor(): void {
    this.editorSurface?.nativeElement.focus();
  }

  private captureEditorContent(): void {
    this.documentHtml = this.normaliseHtml(this.editorSurface?.nativeElement.innerHTML ?? '', EMPTY_DOCUMENT_HTML);
  }

  private setDocumentHtml(html: string): void {
    this.documentHtml = this.normaliseHtml(html, DEFAULT_DOCUMENT_HTML);
    this.renderDocument();
    this.cdr.markForCheck();
  }

  private renderDocument(): void {
    if (!this.editorSurface) {
      return;
    }

    this.editorSurface.nativeElement.innerHTML = this.documentHtml;
  }

  private normaliseHtml(html: string, fallback: string): string {
    const trimmedHtml = html.trim();
    return trimmedHtml.length > 0 ? trimmedHtml : fallback;
  }

  private stripDocxExtension(fileName: string): string {
    return fileName.toLowerCase().endsWith('.docx') ? fileName.slice(0, -5) : fileName;
  }

  private ensureDocxExtension(fileName: string): string {
    return fileName.toLowerCase().endsWith('.docx') ? fileName : `${fileName}.docx`;
  }

  private sanitizeFileName(fileName: string): string {
    const invalidCharacters = new Set(['<', '>', ':', '"', '/', '\\', '|', '?', '*']);
    const cleaned = Array.from(fileName.trim(), character => {
      const codePoint = character.codePointAt(0) ?? 0;
      return invalidCharacters.has(character) || codePoint < 32 ? '-' : character;
    })
      .join('')
      .split(/\s+/)
      .filter(Boolean)
      .join(' ');

    return cleaned || 'documento';
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
}

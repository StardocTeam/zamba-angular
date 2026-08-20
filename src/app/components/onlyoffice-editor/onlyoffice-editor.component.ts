import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DocumentEditorModule, type IConfig } from '@onlyoffice/document-editor-angular';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTagModule } from 'ng-zorro-antd/tag';

interface OnlyofficeFormModel {
  documentServerUrl: string;
  documentUrl: string;
  callbackUrl: string;
  documentTitle: string;
  documentKey: string;
  autosave: boolean;
  compactToolbar: boolean;
  mode: 'edit' | 'view';
}

@Component({
  selector: 'app-onlyoffice-editor',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    DocumentEditorModule,
    NzAlertModule,
    NzButtonModule,
    NzCardModule,
    NzInputModule,
    NzMessageModule,
    NzSwitchModule,
    NzTagModule,
  ],
  templateUrl: './onlyoffice-editor.component.html',
  styleUrls: ['./onlyoffice-editor.component.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OnlyofficeEditorComponent {
  readonly sampleDocumentUrl = 'https://static.onlyoffice.com/assets/docs/samples/demo.docx';
  readonly localDocumentServerUrl = 'http://localhost:8081/';
  readonly localDocumentUrl = 'http://host.docker.internal:3001/files/demo.docx';
  readonly localCallbackUrl = 'http://host.docker.internal:3001/onlyoffice/callback';

  form: OnlyofficeFormModel = {
    documentServerUrl: this.localDocumentServerUrl,
    documentUrl: this.localDocumentUrl,
    callbackUrl: this.localCallbackUrl,
    documentTitle: 'demo.docx',
    documentKey: this.generateDocumentKey(),
    autosave: true,
    compactToolbar: false,
    mode: 'edit',
  };

  config: IConfig | null = null;
  showEditor = false;
  lastEvent = 'Aún no se cargó el editor.';
  lastError = '';

  private readonly cdr = inject(ChangeDetectorRef);
  private readonly message = inject(NzMessageService);

  useCommunityDefaults(): void {
    this.form = {
      documentServerUrl: this.localDocumentServerUrl,
      documentUrl: this.localDocumentUrl,
      callbackUrl: this.localCallbackUrl,
      documentTitle: 'demo.docx',
      documentKey: this.generateDocumentKey(),
      autosave: true,
      compactToolbar: false,
      mode: 'edit',
    };
    this.config = null;
    this.showEditor = false;
    this.lastError = '';
    this.lastEvent = 'Modo Community/free restaurado con la configuración local por defecto.';
    this.cdr.markForCheck();
    this.message.info('Se restauraron los defaults free/community de OnlyOffice.');
  }

  useSampleDocument(): void {
    this.form.documentUrl = this.sampleDocumentUrl;
    this.form.documentTitle = 'onlyoffice-demo.docx';
    this.form.documentKey = this.generateDocumentKey();
    this.message.info('Se cargó la URL pública de ejemplo de OnlyOffice.');
  }

  regenerateKey(): void {
    this.form.documentKey = this.generateDocumentKey();
    this.message.info('Se regeneró la clave del documento.');
  }

  initializeEditor(): void {
    if (!this.form.documentServerUrl.trim()) {
      this.message.warning('Indica la URL de tu Document Server de OnlyOffice.');
      return;
    }

    if (!this.form.documentUrl.trim()) {
      this.message.warning('Indica la URL del archivo DOCX a abrir.');
      return;
    }

    this.lastError = '';
    this.lastEvent = 'Inicializando editor...';
    this.config = this.buildConfig();
    this.showEditor = false;
    this.cdr.markForCheck();

    globalThis.setTimeout(() => {
      this.showEditor = true;
      this.cdr.markForCheck();
    }, 0);
  }

  readonly onDocumentReady = (): void => {
    this.lastEvent = 'Documento cargado correctamente en OnlyOffice.';
    this.message.success('OnlyOffice cargó el documento.');
    this.cdr.markForCheck();
  };

  readonly onDocumentStateChange = (event: { data?: boolean } | undefined): void => {
    this.lastEvent = event?.data ? 'Hay cambios pendientes de guardar.' : 'El documento está sincronizado.';
    this.cdr.markForCheck();
  };

  readonly onEditorError = (event: unknown): void => {
    this.lastError = this.stringifyEvent(event);
    this.message.error('OnlyOffice reportó un error.');
    this.cdr.markForCheck();
  };

  readonly onLoadComponentError = (errorCode: number, errorDescription: string): void => {
    this.lastError = `${errorCode}: ${errorDescription}`;
    this.showEditor = false;
    this.message.error('No se pudo cargar el componente de OnlyOffice.');
    this.cdr.markForCheck();
  };

  private buildConfig(): IConfig {
    return {
      documentType: 'word',
      type: 'desktop',
      document: {
        fileType: 'docx',
        key: this.form.documentKey,
        title: this.form.documentTitle,
        url: this.form.documentUrl,
        permissions: {
          comment: true,
          copy: true,
          download: true,
          edit: this.form.mode === 'edit',
          fillForms: true,
          print: true,
          review: true,
        },
      },
      editorConfig: {
        callbackUrl: this.form.callbackUrl || undefined,
        lang: 'es',
        mode: this.form.mode,
        user: {
          id: 'demo-user',
          name: 'Usuario demo',
        },
        customization: {
          autosave: this.form.autosave,
          comments: true,
          compactHeader: false,
          compactToolbar: this.form.compactToolbar,
          help: true,
          plugins: true,
          review: {
            reviewDisplay: 'markup',
            showReviewChanges: true,
            trackChanges: true,
          },
          close: {
            visible: false,
            text: 'Cerrar',
          },
        },
      },
    };
  }

  private generateDocumentKey(): string {
    return `onlyoffice-${Date.now()}`;
  }

  private stringifyEvent(event: unknown): string {
    try {
      return typeof event === 'string' ? event : JSON.stringify(event, null, 2);
    } catch {
      return 'No fue posible serializar el evento.';
    }
  }
}

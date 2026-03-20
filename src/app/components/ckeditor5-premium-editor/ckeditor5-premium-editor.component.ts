import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CKEditorModule, type CKEditorCloudConfig, loadCKEditorCloud } from '@ckeditor/ckeditor5-angular';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTagModule } from 'ng-zorro-antd/tag';

const SAMPLE_DOCUMENT = `
<h1>Prueba de CKEditor 5</h1>
<p>
  Este escenario usa el componente oficial de Angular y carga el editor desde el CDN de CKEditor.
</p>
<p>
  Puedes compararlo con TinyMCE y OnlyOffice en edición, toolbar, estructura de documento y experiencia general.
</p>
<ul>
  <li>Headings</li>
  <li>Negrita, cursiva y subrayado</li>
  <li>Listas</li>
  <li>Bloques y tablas</li>
</ul>
<blockquote>
  Si activas las funciones premium y colocas una licencia trial/comercial, podrás evaluar extras como Format Painter.
</blockquote>
`;

interface CkeditorSettings {
    version: string;
    licenseKey: string;
    enablePremiumFeatures: boolean;
}

const FREE_CKEDITOR_SETTINGS: CkeditorSettings = {
    version: '47.6.0',
    licenseKey: 'GPL',
    enablePremiumFeatures: false
};

@Component({
    selector: 'app-ckeditor5-premium-editor',
    standalone: true,
    imports: [
        CommonModule,
        FormsModule,
        CKEditorModule,
        NzAlertModule,
        NzButtonModule,
        NzCardModule,
        NzInputModule,
        NzMessageModule,
        NzSpinModule,
        NzSwitchModule,
        NzTagModule
    ],
    templateUrl: './ckeditor5-premium-editor.component.html',
    styleUrls: ['./ckeditor5-premium-editor.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class Ckeditor5PremiumEditorComponent implements OnInit {
    settings: CkeditorSettings = { ...FREE_CKEDITOR_SETTINGS };

    Editor: any = null;
    editorConfig: Record<string, unknown> | null = null;
    editorData = SAMPLE_DOCUMENT;
    isLoading = false;
    loadError = '';

    private readonly cdr = inject(ChangeDetectorRef);
    private readonly message = inject(NzMessageService);

    ngOnInit(): void {
        void this.initializeEditor();
    }

    useFreeDefaults(): void {
        this.settings = { ...FREE_CKEDITOR_SETTINGS };
        void this.initializeEditor();
        this.message.info('CKEditor 5 volvió al modo free/open-source por defecto.');
    }

    async initializeEditor(): Promise<void> {
        this.isLoading = true;
        this.loadError = '';
        this.Editor = null;
        this.editorConfig = null;
        this.cdr.markForCheck();

        try {
            const cloud = await loadCKEditorCloud({
                premium: this.settings.enablePremiumFeatures,
                translations: ['es'],
                version: this.settings.version as CKEditorCloudConfig['version']
            });

            const ckeditor = (cloud as { CKEditor?: Record<string, unknown> }).CKEditor ?? {};
            const premiumFeatures = (cloud as { CKEditorPremiumFeatures?: Record<string, unknown> }).CKEditorPremiumFeatures ?? {};

            const DecoupledEditor = ckeditor['DecoupledEditor'];
            const Essentials = ckeditor['Essentials'];
            const Paragraph = ckeditor['Paragraph'];
            const Heading = ckeditor['Heading'];
            const Bold = ckeditor['Bold'];
            const Italic = ckeditor['Italic'];
            const Underline = ckeditor['Underline'];
            const Link = ckeditor['Link'];
            const List = ckeditor['List'];
            const BlockQuote = ckeditor['BlockQuote'];
            const Table = ckeditor['Table'];
            const TableToolbar = ckeditor['TableToolbar'];
            const FormatPainter = premiumFeatures['FormatPainter'];

            if (!DecoupledEditor) {
                throw new Error('No se pudo cargar DecoupledEditor desde el CDN de CKEditor.');
            }

            const plugins = [Essentials, Paragraph, Heading, Bold, Italic, Underline, Link, List, BlockQuote, Table, TableToolbar].filter(Boolean);
            const toolbar = [
                'undo',
                'redo',
                '|',
                'heading',
                '|',
                'bold',
                'italic',
                'underline',
                '|',
                'link',
                'insertTable',
                'blockQuote',
                'bulletedList',
                'numberedList'
            ];

            if (this.settings.enablePremiumFeatures && FormatPainter) {
                plugins.push(FormatPainter);
                toolbar.push('|', 'formatPainter');
            }

            this.Editor = DecoupledEditor;
            this.editorConfig = {
                licenseKey: this.settings.licenseKey || 'GPL',
                placeholder: 'Escribe aquí tu contenido...',
                plugins,
                toolbar
            };
        } catch (error) {
            console.error('Error al inicializar CKEditor 5:', error);
            this.loadError = error instanceof Error ? error.message : 'No fue posible inicializar CKEditor 5.';
            this.message.error('CKEditor 5 no pudo inicializarse.');
        } finally {
            this.isLoading = false;
            this.cdr.markForCheck();
        }
    }

    onReady(editor: any): void {
        const editableElement = editor.ui.getEditableElement();
        const toolbarElement = editor.ui.view.toolbar.element;

        if (editableElement && toolbarElement && toolbarElement.parentElement !== editableElement.parentElement) {
            editableElement.before(toolbarElement);
        }
    }

    onChange(event: { editor: { getData: () => string } }): void {
        this.editorData = event.editor.getData();
    }

    restoreSample(): void {
        this.editorData = SAMPLE_DOCUMENT;
        this.cdr.markForCheck();
        this.message.info('Se restauró el contenido de ejemplo.');
    }

    downloadHtml(): void {
        const blob = new Blob([this.editorData], { type: 'text/html;charset=utf-8' });
        const url = URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = 'ckeditor5-demo.html';
        document.body.appendChild(anchor);
        anchor.click();
        anchor.remove();
        globalThis.setTimeout(() => URL.revokeObjectURL(url), 300);
    }
}

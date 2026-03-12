import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { EditorModule, TINYMCE_SCRIPT_SRC } from '@tinymce/tinymce-angular';
import { NzMessageModule, NzMessageService } from 'ng-zorro-antd/message';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzTagModule } from 'ng-zorro-antd/tag';

const SELF_HOSTED_BASE_URL = 'assets/tinymce';
const SELF_HOSTED_SCRIPT_SRC = `${SELF_HOSTED_BASE_URL}/tinymce.min.js`;
const LICENSE_KEY = 'gpl';
const BLANK_DOCUMENT = '<p></p>';

const SAMPLE_CONTENT = `
<h1>Prueba de TinyMCE</h1>
<p>
    Este editor usa TinyMCE open source en modo self-hosted desde el paquete instalado por npm.
</p>
<ul>
  <li>Formato enriquecido</li>
  <li>Tablas</li>
  <li>Imágenes</li>
    <li>Plugins gratuitos incluidos en TinyMCE</li>
</ul>
<p><strong>Consejo:</strong> compara velocidad, toolbar, UX y calidad de pegado desde Word.</p>
`;

interface TinyMceSettings {
    readonly: boolean;
}

const FREE_TINYMCE_SETTINGS: TinyMceSettings = {
    readonly: false
};

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
        NzSwitchModule,
        NzTagModule
    ],
    providers: [{ provide: TINYMCE_SCRIPT_SRC, useValue: SELF_HOSTED_SCRIPT_SRC }],
    templateUrl: './tinymce-premium-editor.component.html',
    styleUrls: ['./tinymce-premium-editor.component.less'],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TinymcePremiumEditorComponent {
    readonly licenseKey = LICENSE_KEY;
    settings: TinyMceSettings = { ...FREE_TINYMCE_SETTINGS };

    editorContent = BLANK_DOCUMENT;
    editorInit = this.buildEditorInit();

    private readonly message = inject(NzMessageService);

    useFreeDefaults(): void {
        this.settings = { ...FREE_TINYMCE_SETTINGS };
        this.editorInit = this.buildEditorInit();
        this.editorContent = BLANK_DOCUMENT;
        this.message.info('TinyMCE quedó en modo open source self-hosted con un documento en blanco.');
    }

    applySettings(): void {
        this.message.success('La configuración de edición fue actualizada.');
    }

    loadSampleContent(): void {
        this.editorContent = SAMPLE_CONTENT;
        this.message.info('Se restauró el contenido de ejemplo.');
    }

    createBlankDocument(): void {
        this.editorContent = BLANK_DOCUMENT;
        this.message.info('Se creó un documento en blanco.');
    }

    downloadHtml(): void {
        const blob = new Blob([this.editorContent], { type: 'text/html;charset=utf-8' });
        const url = URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = 'tinymce-demo.html';
        document.body.appendChild(anchor);
        anchor.click();
        anchor.remove();
        globalThis.setTimeout(() => URL.revokeObjectURL(url), 300);
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
            toolbar_sticky: true
        };
    }
}

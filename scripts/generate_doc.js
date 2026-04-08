const fs = require('fs');
// const { asBlob } = require('html-docx-js-typescript'); // This might fail if it's ESM only
// Let's try dynamic import or just standard require if supported.

// Since we are in a hurry and want to guarantee results, let's try a safe approach:
// We will create the HTML content first.
const htmlContent = `
<!DOCTYPE html>
<html lang="es">
<head>
<meta charset="UTF-8">
<style>
    body { font-family: 'Calibri', 'Arial', sans-serif; line-height: 1.5; color: #333; }
    h1 { color: #2E74B5; border-bottom: 2px solid #2E74B5; padding-bottom: 10px; }
    h2 { color: #1F4D78; margin-top: 20px; }
    table { border-collapse: collapse; width: 100%; margin-top: 15px; }
    th, td { border: 1px solid #999; padding: 8px; vertical-align: top; }
    th { background-color: #f2f2f2; text-align: left; font-weight: bold; }
    tr:nth-child(even) { background-color: #fafafa; }
    code { background-color: #f0f0f0; padding: 2px 4px; border-radius: 4px; font-family: Consolas, monospace; }
</style>
</head>
<body>
    <h1>Configuración del Componente TinymceEditor</h1>
    <p>Este documento detalla la configuración y uso del componente web <code>&lt;app-tinymce-element&gt;</code> (Web Component: <code>tinymce-editor-component</code>).</p>

    <h2>Descripción General</h2>
    <p>El componente permite la edición de documentos con soporte para carga y guardado en Zamba, así como edición de archivos DOCX locales.</p>

    <h2>Atributos Configurables (Inputs)</h2>
    <p>Los siguientes atributos pueden ser pasados al componente para configurar su comportamiento:</p>

    <table>
        <thead>
            <tr>
                <th style="width: 15%">Propiedad</th>
                <th style="width: 15%">Tipo</th>
                <th style="width: 10%">Requerido</th>
                <th style="width: 45%">Descripción</th>
                <th style="width: 15%">Valor por Defecto</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><strong>userId</strong></td>
                <td><code>string | number</code></td>
                <td>Sí*</td>
                <td>Identificador del usuario (ID de Zamba). Necesario para cargar y guardar documentos en el servicio.</td>
                <td><code>undefined</code></td>
            </tr>
            <tr>
                <td><strong>documentId</strong></td>
                <td><code>string | number</code></td>
                <td>Sí*</td>
                <td>Identificador del documento. Si el documento existe en Zamba, se cargará. Si no, se preparará para crear uno nuevo con este ID.</td>
                <td><code>undefined</code></td>
            </tr>
            <tr>
                <td><strong>entityId</strong></td>
                <td><code>string | number</code></td>
                <td>Sí*</td>
                <td>Identificador de la entidad de negocio asociada al documento.</td>
                <td><code>undefined</code></td>
            </tr>
            <tr>
                <td><strong>token</strong></td>
                <td><code>string</code></td>
                <td>No</td>
                <td>Token de autenticación (JWT). Si no se proporciona explícitamente, el componente intentará obtenerlo del servicio de autenticación global (<code>DA_SERVICE_TOKEN</code>).</td>
                <td><code>null</code></td>
            </tr>
            <tr>
                <td><strong>assetsUrl</strong></td>
                <td><code>string</code></td>
                <td>No</td>
                <td>URL base donde se encuentran alojados los assets de TinyMCE (tinymce.min.js, themes, plugins).</td>
                <td><code>'assets/tinymce'</code></td>
            </tr>
            <tr>
                <td><strong>height</strong></td>
                <td><code>string | number</code></td>
                <td>No</td>
                <td>Altura del editor. Puede especificarse en píxeles (ej. <code>500</code>) o porcentaje/unidades CSS (ej. <code>'100%'</code>).</td>
                <td><code>720</code></td>
            </tr>
            <tr>
                <td><strong>width</strong></td>
                <td><code>string | number</code></td>
                <td>No</td>
                <td>Ancho del editor editor.</td>
                <td><code>'100%'</code></td>
            </tr>
            <tr>
                <td><strong>readOnly</strong></td>
                <td><code>boolean | string</code></td>
                <td>No</td>
                <td>Indica si el editor debe iniciarse en modo de solo lectura. Admite booleanos o strings como 'true', 'false', 'readonly', '1', '0'.</td>
                <td><code>false</code></td>
            </tr>
        </tbody>
    </table>

    <p style="font-size: 0.9em; font-style: italic; margin-top: 10px;">
        * <strong>Nota:</strong> Los campos <code>userId</code>, <code>documentId</code> y <code>entityId</code> son obligatorios para habilitar la interacción con el servidor (Zamba). 
        Si faltan, el editor cargará en blanco, pero no se podrá "Guardar en Zamba" ni cargar contenido previo automáticamente.
    </p>

    <h2>Botones de la Barra de Herramientas</h2>
    <p>El editor incluye botones personalizados:</p>
    <ul>
        <li><strong>Reload:</strong> Recarga el documento desde el servidor.</li>
        <li><strong>Abrir DOCX:</strong> Permite cargar un archivo .docx local.</li>
        <li><strong>Nuevo:</strong> Limpia el editor para comenzar un documento en blanco.</li>
        <li><strong>Guardar (Zamba):</strong> Guarda los cambios en el servidor usando los IDs provistos.</li>
        <li><strong>Descargar DOCX:</strong> Exporta el contenido actual como archivo .docx.</li>
        <li><strong>Bloquear/Desbloquear:</strong> Alterna el modo de solo lectura manualmente.</li>
        <li><strong>Insertar Imagen (Nuevo):</strong> Permite seleccionar una imagen local desde el explorador de archivos.</li>
    </ul>

</body>
</html>
`;

// Try to generate a .doc file (fake docx, actually HTML but Word opens it)
// This is the most reliable way without complex node libraries dependencies execution in this environment.
// Renaming .html to .doc usually works for Word to open it.
// Ideally usage of 'html-docx-js-typescript' would be better.

async function createDocs() {
    // 1. Save as HTML (always useful)
    fs.writeFileSync('TinymceEditor_Config.html', htmlContent);

    // 2. Try to use html-docx-js-typescript if available to create real DOCX
    try {
        // Dynamic import attempt
        const { asBlob } = require('html-docx-js-typescript');
        
        const blob = await asBlob(htmlContent, { orientation: 'portrait', margins: { top: 720 } });
        
        let buffer;
        if (blob instanceof Buffer) {
            buffer = blob;
        } else if (blob.arrayBuffer) {
             const ab = await blob.arrayBuffer();
             buffer = Buffer.from(ab);
        } else {
            console.log('Blob type unknown, trying text');
             // Fallback
             buffer = Buffer.from(await blob.text());
        }

        fs.writeFileSync('TinymceEditor_Config.docx', buffer);
        console.log('DOCX created successfully.');

    } catch (e) {
        console.warn('Could not create native DOCX using library. Fallback to renaming HTML.', e.message);
        // Fallback: Just copy HTML to .doc (Word handles HTML content in .doc files)
        // Warning: This is not a real .docx (XML based), but often acceptable for "Open in Word".
        // But the user asked for .docx specifically usually implies the format.
        // Let's create a .doc file as fallback.
        fs.writeFileSync('TinymceEditor_Config.doc', htmlContent);
    }
}

createDocs();

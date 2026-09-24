# Buscador global embebido

`<zamba-global-search>` contiene el disparador y el modal en un único template y stylesheet (Shadow DOM). No carga CSS, JavaScript, jQuery ni Moment externos para el modal.

## Integración en la web legacy

1. Actualizar los bundles de Angular Elements con el proceso de build/deploy habitual.
2. Mantener `<zamba-global-search>` en la página.
3. Quitar las referencias a `global-search-modal.js`, `global-search-modal.css` y al antiguo `assets/global-search/global-search.css`.
4. Quitar las llamadas a `window.ZambaGlobalSearchModal(...)` de `zamba.search.js`. Sus listeners en captura interceptan los clics y `searchSubmitted`, interfiriendo con el modal integrado.
5. Si una toolbar usaba la directiva `zambaViewerSearch` del script eliminado, reemplazar su montaje por `<zamba-global-search>` en esa toolbar. El componente no modifica toolbars de terceros automáticamente.

El host debe proporcionar `window.GetUID()`, `window.ZambaWebRestApiURL` (base hasta `/api`) y `window.thisDomain` (base de la aplicación Web, incluido su directorio virtual). Sin `thisDomain`, se usa el origen, como en el modal de los viewers del contexto.

El servicio busca el Bearer en `$http.defaults.headers.common.Authorization` del injector AngularJS y luego en `localStorage.authorizationData`. Lo envía como Authorization en las llamadas y como `t` en la URL del viewer. Se usa HttpBackend para evitar que los interceptores de la aplicación Angular 16 sustituyan esta sesión. La sesión nativa Angular 16 todavía no se integra.

`POST /search/Results` recibe el mismo filtro `Empieza`, usuario y páginas de 100 del contexto. El controlador adjunto devuelve el tamaño de la página como total; se habilita Cargar más si llegaron 100 filas (puede requerir una última página vacía).

La apertura verifica el derecho 19 mediante `POST /Tasks/GetUsersWFStepsRights`: si está permitido abre TaskViewer; sin etapa o sin permiso abre DocViewer. Un fallo HTTP de permisos conserva el modal y muestra un error. Se abre en otra ventana, como en el contexto de los viewers; no se replica el manejo de ventanas de escritorio/WinForms, previews ni notificaciones de lectura de la grilla legacy.

Se conserva `searchSubmitted` como evento informativo al iniciar una búsqueda nueva; no requiere un manejador externo. `entity-id` e `index-ids` se conservan en ese evento, sin introducir filtros adicionales en el contrato global proporcionado.

## Verificación

- `node --test scripts/test-global-search.cjs`: contratos de token, payload, permisos, URLs, paginación y respuestas obsoletas; dependencias Angular simuladas.
- `node node_modules/@angular/compiler-cli/bundles/src/bin/ngc.js -p tsconfig.app.json --noEmit`: tipos y templates de app y Elements.

La verificación con sesión autenticada, datos reales y ventanas emergentes se realiza desde la página legacy anfitriona.

# API de búsqueda externa de Zamba

Esta guía describe el contrato que utiliza `src/assets/API_Search.html`. El HTML funciona como ejemplo de consumo, no como especificación formal del servidor; los nombres exactos de campos y el significado de algunos identificadores deben confirmarse con el equipo que mantiene la API.

## Flujo obligatorio

1. Cada usuario final inicia sesión con `Login`.
2. Se conservan el `token` y el `userID` de la respuesta para esa sesión.
3. Se buscan documentos con `SearchResults`, enviando el token en el encabezado `Authorization` y el `userID` en el cuerpo.
4. Las operaciones sobre un documento usan el campo `ID` de una fila concreta devuelta por `SearchResults`. No se debe inventar un ID ni tomarlo de otra fuente.
5. Al terminar, se puede cerrar la sesión con `EndSession`.

Una respuesta de búsqueda vacía no habilita operaciones documentales: primero hay que repetir o ajustar la búsqueda hasta obtener el documento correcto.

## URL base y autenticación

Configure `BASE_URL` como la raíz del servicio, sin `/api/ExternalSearch` al final y, preferentemente, sin `/` final. Por ejemplo:

```text
https://servidor/Zamba.RestApi
```

Los endpoints descritos se forman como:

```text
{BASE_URL}/api/ExternalSearch/{metodo}
```

El HTML envía el token directamente en `Authorization`, sin el prefijo `Bearer`. Esta guía conserva ese comportamiento. No se debe añadir ni quitar un prefijo sin confirmar qué espera el servidor.

Todas las solicitudes mostradas son `POST` con cuerpo JSON y `Content-Type: application/json; charset=utf-8`, excepto que el backend indique otro formato.

## Endpoints

| Método | Ruta | Autenticación según el HTML | Uso |
| --- | --- | --- | --- |
| `Login` | `/api/ExternalSearch/Login` | Sin token | Crear la sesión y obtener `token` y `userID`. |
| `SearchResults` | `/api/ExternalSearch/SearchResults` | `Authorization: {token}` | Buscar y obtener los IDs de documentos disponibles. |
| `GetDocFile` | `/api/ExternalSearch/GetDocFile` | `Authorization: {token}` | Obtener el archivo de un documento encontrado. |
| `DeleteDocFile` | `/api/ExternalSearch/DeleteDocFile` | `Authorization: {token}` | Eliminar un documento encontrado. Es una acción destructiva. |
| `EditDoc` | `/api/ExternalSearch/EditDoc` | `Authorization: {token}` | Editar atributos de un documento encontrado. |
| `ReplaceDoc` | `/api/ExternalSearch/ReplaceDoc` | `Authorization: {token}` | Reemplazar el archivo asociado a un documento. |
| `EndSession` | `/api/ExternalSearch/EndSession` | El HTML no envía token | Cerrar la sesión por `userID`. Confirmar con el backend si también requiere token. |

## 1. Iniciar sesión

`POST {BASE_URL}/api/ExternalSearch/Login`

```json
{
  "UserName": "usuario",
  "Password": "clave",
  "ComputerNameOrIp": "identificador-de-sesion-o-ip",
  "name": "Nombre opcional",
  "lastName": "Apellido opcional",
  "eMail": "correo@ejemplo.com",
  "type": "tipo opcional"
}
```

El HTML indica que `name`, `lastName` y `eMail` son datos opcionales para usuarios nuevos, y reserva `type` para uso futuro. La respuesta que consume el ejemplo contiene `token` y `userID`:

```json
{
  "token": "TOKEN_DEVUELTO_POR_ZAMBA",
  "userID": 123
}
```

Guardar ambos valores asociados a la sesión del usuario. No registrar la contraseña ni exponer el token en logs o URLs.

## 2. Buscar documentos

`POST {BASE_URL}/api/ExternalSearch/SearchResults`

Encabezado:

```http
Authorization: TOKEN_DEVUELTO_POR_ZAMBA
```

Cuerpo de ejemplo con dos atributos de búsqueda:

```json
{
  "ExternUserID": 123,
  "DoctypesIds": ["ID_DE_ENTIDAD"],
  "Indexs": [
    { "id": "ID_ATRIBUTO_1", "data": "valor 1" },
    { "id": "ID_ATRIBUTO_2", "data": "valor 2" }
  ],
  "removeSearchColumns": false
}
```

El ejemplo incluye el campo opcional `Url` con valor `true` solo cuando está activada esa opción. `DoctypesIds` es una colección aunque el formulario permita ingresar un único valor; `Indexs` también es una colección. Los IDs y valores válidos de entidad y atributos deben obtenerse del responsable funcional de Zamba.

El HTML trata la respuesta como una colección y usa el primer resultado. De forma ilustrativa:

```json
[
  {
    "ID": "ID_DE_RESULTADO",
    "DOC_ID": "ID_INTERNO_DEL_DOCUMENTO",
    "DOC_TYPE_ID": "ID_DEL_TIPO"
  }
]
```

Para `GetDocFile`, `DeleteDocFile` y `EditDoc`, el HTML usa `resultado.ID`. Aunque también lee `DOC_ID` y `DOC_TYPE_ID`, no los usa en esas tres llamadas. No asuma que esos campos son intercambiables. La operación de reemplazo tiene dos campos (`IdDocument` e `Id`) cuya semántica no queda clara en el ejemplo; confirme con el backend qué identificador corresponde a cada uno.

Siempre valide la respuesta antes de acceder a `resultados[0]`: debe ser un array con al menos un elemento y el registro debe incluir `ID`.

## 3. Descargar un documento

`POST {BASE_URL}/api/ExternalSearch/GetDocFile`

```json
{
  "Params": {
    "id": "ID_DE_RESULTADO_OBTENIDO_EN_SEARCH",
    "externuserid": 123,
    "converttopdf": false
  }
}
```

El HTML interpreta la respuesta exitosa como una cadena Base64 y la muestra en un `iframe` usando `data:application/pdf;base64,`. Verifique el tipo de contenido real y el efecto de `converttopdf` con el servidor: el ejemplo envía `false` aunque construye una URL de PDF.

## 4. Eliminar un documento

`POST {BASE_URL}/api/ExternalSearch/DeleteDocFile`

```json
{
  "Params": {
    "id": "ID_DE_RESULTADO_OBTENIDO_EN_SEARCH",
    "externuserid": 123
  }
}
```

El ejemplo no envía un cuerpo independiente de confirmación. La aplicación cliente debería solicitar confirmación antes de ejecutar esta operación y mostrar la respuesta del servidor.

## 5. Editar atributos

`POST {BASE_URL}/api/ExternalSearch/EditDoc`

```json
{
  "ExternUserID": 123,
  "Indexs": [
    { "id": "ID_ATRIBUTO_A_EDITAR", "data": "nuevo valor" }
  ],
  "Id": "ID_DE_RESULTADO_OBTENIDO_EN_SEARCH"
}
```

El `id` dentro de `Indexs` identifica el atributo que se modifica; `Id` identifica el resultado documental seleccionado. Los IDs de atributo válidos deben solicitarse al responsable funcional.

## 6. Reemplazar el archivo

`POST {BASE_URL}/api/ExternalSearch/ReplaceDoc`

El HTML construye un cuerpo con esta forma:

```json
{
  "ExternUserID": 123,
  "IdDocument": "ID_DE_RESULTADO_OBTENIDO_EN_SEARCH",
  "Id": "ID_CONFIRMADO_PARA_EL_CAMPO_ID",
  "Base64StringArray": [
    {
      "FileName": "documento.pdf",
      "Base64": "CONTENIDO_DEL_ARCHIVO_EN_BASE64"
    }
  ]
}
```

`IdDocument` e `Id` aparecen como propiedades distintas en el código. El formulario llena ambas con el mismo `ID` del resultado, pero eso no demuestra que el backend espere el mismo valor. Confirme esta correspondencia antes de usar el endpoint en producción.

El archivo debe leerse completamente como Base64 antes de enviar la solicitud. La lectura con `FileReader` es asíncrona; no envíe el reemplazo inmediatamente después de iniciar la lectura.

## 7. Cerrar la sesión

`POST {BASE_URL}/api/ExternalSearch/EndSession`

El HTML envía:

```json
{
  "Params": {
    "externuserid": 123
  }
}
```

La página no adjunta el encabezado `Authorization` en este método. Confirme si el despliegue de la API exige también el token.

## Ejemplo secuencial en JavaScript

El ejemplo hace login, búsqueda y descarga del primer resultado. La selección del primer resultado es solo demostrativa: en una interfaz real se debe permitir seleccionar el registro correcto antes de operar sobre él.

```js
const baseUrl = 'https://servidor/Zamba.RestApi';
const endpoint = (method) => `${baseUrl}/api/ExternalSearch/${method}`;

async function postJson(method, body, token) {
  const headers = { 'Content-Type': 'application/json; charset=utf-8' };
  if (token) headers.Authorization = token;

  const response = await fetch(endpoint(method), {
    method: 'POST',
    headers,
    body: JSON.stringify(body)
  });

  const responseText = await response.text();
  if (!response.ok) {
    throw new Error(`${method} respondió HTTP ${response.status}: ${responseText}`);
  }

  return responseText ? JSON.parse(responseText) : null;
}

const session = await postJson('Login', {
  UserName: 'usuario',
  Password: 'clave',
  ComputerNameOrIp: 'identificador-de-sesion'
});

if (!session?.token || session.userID == null) {
  throw new Error('Login no devolvió token y userID.');
}

const results = await postJson('SearchResults', {
  ExternUserID: session.userID,
  DoctypesIds: ['ID_DE_ENTIDAD'],
  Indexs: [{ id: 'ID_ATRIBUTO', data: 'valor de búsqueda' }],
  removeSearchColumns: false
}, session.token);

if (!Array.isArray(results) || results.length === 0 || results[0]?.ID == null) {
  throw new Error('La búsqueda no devolvió documentos con un ID utilizable.');
}

const documentId = results[0].ID;
const fileResponse = await fetch(endpoint('GetDocFile'), {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json; charset=utf-8',
    Authorization: session.token
  },
  body: JSON.stringify({
    Params: {
      id: documentId,
      externuserid: session.userID,
      converttopdf: false
    }
  })
});

if (!fileResponse.ok) {
  throw new Error(`GetDocFile respondió HTTP ${fileResponse.status}: ${await fileResponse.text()}`);
}

// Según el HTML, la respuesta exitosa de descarga es texto Base64.
const base64File = await fileResponse.text();
console.log({ documentId, base64File });
```

Si el endpoint de descarga devuelve JSON en lugar de texto Base64, adapte esa lectura al formato real de la respuesta. En aplicaciones web, el navegador también debe poder realizar solicitudes CORS al origen del servicio.

## Observaciones sobre el HTML de referencia

- El `$(document).ready` sobrescribe la URL ingresada en el formulario y asigna usuario y clave de ejemplo. Esos valores no deben tratarse como credenciales válidas ni copiarse a producción.
- `ReplaceDoc` concatena `DominioUrl + "api/..."` sin `/` entre el host y la ruta. La ruta correcta debe construirse con `/api/ExternalSearch/ReplaceDoc`.
- La condición de búsqueda `data != ""` no verifica que el resultado tenga filas. Compruebe `Array.isArray(data) && data.length > 0` antes de acceder al primer elemento.
- La selección inicial coloca `data[0].ID` en los formularios de descarga, borrado, edición y reemplazo. En uso real, se debe usar el ID del documento seleccionado por el usuario, siempre dentro de los resultados de una búsqueda válida.
- La lectura de archivos para reemplazo se realiza con `FileReader` de forma asíncrona. Espere a que todas las lecturas finalicen antes de enviar `Base64StringArray`.
- La página no ofrece aquí una especificación de códigos de error ni un esquema formal de respuestas. Inspeccione el estado HTTP y el cuerpo de error de cada endpoint.

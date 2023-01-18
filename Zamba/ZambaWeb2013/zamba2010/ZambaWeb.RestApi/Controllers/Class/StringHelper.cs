using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZambaWeb.RestApi.Controllers
{
    public class StringHelper
    {
        public const string ValidUserError = "Error al validar usuario";
        public const string InvalidUser = "Usuario invalido o sesion expirada";
        public const string InvalidParameter = "Parametros invalidos al invocar al metodo";
        public const string ExceptionExpected = "Deberia producirse una excepcion";
        public const string TaskIdExpected = "No se ingreso taskId";
        public const string IndexExpected = "No se ingresaron indices";
        public const string TaskIdOrDocTypeIdExpected = "No se ingreso etapa-doctype";
        public const string BadInsertParameter = "Verifique indices, doctypeid y archivo a insertar";
        public const string RecentTaskNotFound = "No se encontraron tareas recientes";
        public const string FeedsNotPermission = "No se encontraron novedades o no tiene permiso para visualizarlas";
    }
}
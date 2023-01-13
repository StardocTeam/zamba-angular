using System.Data;
using Zamba.Core;
using System;
using System.Collections;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los tipos de documento
    /// 28/09/07 Creada - Marcelo
    /// </summary>
    public class DocType : IService
    {
        #region Miembros de IService
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.DocType;
        }

        /// <summary>
        /// Trae todos los ids y nombres de los tipos de documentos
        /// </summary>
        /// <returns>Dataset con Ids y Nombres</returns>
        public static ArrayList GetDocTypesIdsAndNames(Int32 UserId)
        {
            return DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserId,RightsType.Create);
        }
        public static ArrayList GetDocTypesIdsAndNamesbyRightView(Int32 UserId)
        {
            return DocTypesBusiness.GetDocTypesbyUserRightsOfView(UserId, RightsType.View);
        }
        #endregion

        public static DataTable  GetAllDocTypes()
        {
            return DocTypesBusiness.GetAllDocType();
        }
    }
}
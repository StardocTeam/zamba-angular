using System.Data;
using Zamba.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los tipos de documento
    /// 28/09/07 Creada - Marcelo
    /// </summary>
    public class sDocType : IService
    {        
        #region Miembros de IService
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.DocType;
        }


        private DocTypesBusiness DocTypesBusiness;
        private WFStepBusiness WFStepBusinessobj;

        public sDocType()
        {
            DocTypesBusiness = new DocTypesBusiness();
            WFStepBusinessobj = new WFStepBusiness();
        }

        /// <summary>
        /// Trae todos los ids y nombres de los tipos de documentos
        /// </summary>
        /// <returns>Dataset con Ids y Nombres</returns>
        public List<DocType> GetDocTypesIdsAndNames(Int64 userId)
        {
         
            return DocTypesBusiness.GetDocTypesbyUserRights(userId, RightsType.Create);
        }
        public List<DocType> GetDocTypesIdsAndNamesbyRightView(long userId)
        {
            return DocTypesBusiness.GetDocTypesbyUserRights(userId, RightsType.View);
        }
        #endregion

        public DataTable GetAllDocTypes()
        {
            return DocTypesBusiness.GetAllDocType();
        }

    


        public DocType GetDocType(Int64 DocTypeId, bool useCache)
        {
           
            return DocTypesBusiness.GetDocType(DocTypeId);
        }

    
        public DataSet GetIndexsProperties(Int64 DocTypeId, bool withCache)
        {
           
            return DocTypesBusiness.GetIndexsProperties(DocTypeId);
        }

        public DataSet GetIndexsProperties(Int64 DocTypeId, Int64 IndexId)
        {
            return DocTypesBusiness.GetIndexsProperties(DocTypeId, IndexId);
        }

      
    }
}
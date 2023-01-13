using System;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los indices
    /// </summary>
    public class Index : IService
    {
        #region IService Members
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Index;
        }
        #endregion

        public static Dictionary<Int64 , String>  GetAllIndexsIdAndName()
        {
            return IndexsBusiness.GetAllIndexsIdsAndNames();
        }
        /// <summary>
        /// Trae los datos de los indices de un tipo de documento
        /// </summary>
        /// <param name="docTypeId">Id del tipo de Documento</param>
        /// <returns>Dataset con los datos de los indices</returns>
        public static DataSet getIndexByDocTypeId(Int32 docTypeId)
        {
            return IndexsBusiness.GetIndexSchemaAsDataSet(docTypeId);
        }

    ///<summary>
    ///Obtiene los indices de un tipo de documento segun permisos
    ///</summary>
    ///<param name="DocTypeId">Id de tipo de documento</param>
    ///<param name="GUID">Id usuario/grupo</param>
    ///<param name="_RightsType">Tupo de permiso a filtrar</param>
    ///<returns>Dataset</returns>
        ///<remarks></remarks>
        public static DataTable getIndexByDocTypeId(Int32 docTypeId, Int64 GUID , RightsType _RightsType)
        {
            return IndexsBusiness.getIndexByDocTypeId(docTypeId, GUID, _RightsType);
        }

        /// <summary>
        /// Trae todos los valores distintos que tiene un indice en un tipo de documento determinado
        /// </summary>
        /// <param name="docTypeId">ID del tipo de Documento</param>
        /// <param name="indexId">Id del indice</param>
        /// <returns>Dataset con los valores, una columna con el nombre "ITEM"</returns>
        public static DataSet GetDistinctIndexValues(Int32 docTypeId, Int32 indexId,Int32 UserId,Int32 dataType)
        {
            return IndexsBusiness.GetDistinctIndexValues(docTypeId, indexId, UserId,dataType);
        }

       

       

       


        
        public static Int16 GetIndexDropDownType(Int64 IndexId)
        {

            return IndexsBusiness.GetIndexDropDownType(IndexId);
        }

        public static DataTable GetIndexSubstitutionTable(Int64 indexID)
        {

            return DocTypesBusiness.GetIndexSubstitutionTable(indexID);
        }

        public static Int64 GetIndexidByName(String IndexName)
        {

            return IndexsBusiness.GetIndexId(IndexName);
        }

        //public static Int16 GetIndexTypeByName(String IndexName)
        //{
        //    return WFTaskBusiness.GetIndexTypeByName(IndexName);
        //}

        /// <summary>
        /// Gets a List of values from Indexs of Find Type
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public static DataTable LoadIndexFindTypeValues(Int64 Indexid)
        {

            return IndexsBusiness.LoadIndexFindTypeValues(Indexid);
        }

        /// <summary>
        /// Get Rights by indexs
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="taskIds"></param>
        /// <returns></returns>
        public static System.Collections.Generic.List<Int64> GetIndexsIdsByRight(Int64 GUID, Int64 doctypeid, Zamba.Core.RightsType _righttype)
        {
            System.Collections.Generic.List<Int64> indexsids = new System.Collections.Generic.List<Int64>();

            System.Collections.Hashtable IRI = UserBusiness.Rights.GetIndexsRights(doctypeid, GUID, true,true);

            foreach (Zamba.Core.IndexsRightsInfo ir in IRI.Values)
            {
                if (ir.GetIndexRightValue(_righttype))
                {
                    indexsids.Add(ir.Indexid);
                }
            }
            return indexsids;


        }

    }
}

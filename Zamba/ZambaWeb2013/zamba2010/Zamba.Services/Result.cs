using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los metodos para los results
    /// 
    /// Creado 28/09/07 - Marcelo
    /// </summary>
    public class Result : IService
    {
        #region Miembros de IService
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Result;
        }

        /// <summary>
        /// Devuelve los datos de los results llamando a la vista
        /// </summary>
        /// <param name="docTypeId">Id del tipo de Documento a buscar</param>
        /// <param name="indexId">Id del indice a comparar</param>
        ///<param name="comparateValue">Valor a comparar contra el indice</param>
        /// <returns></returns>
        public static DataSet getResultsData(Int32 docTypeId, Int32 indexId, String comparateValue,
                                             List<ArrayList> genIndex,Boolean filter,Int32 UserId)
        {
            return Zamba.Core.Results_Business.getResultsData(docTypeId, indexId, genIndex, UserId, comparateValue, filter);
        }

        public static DataTable getResultsAndPageQueryResults(Int16 PageId, Int16 PageSize,Int32 docTypeId, Int64 indexId,List<ArrayList> genIndex, Int32 UserId, String comparateValue,
                                               String ComparateDateValue, String Operation, Boolean filter, String SortExpression, String SymbolToReplace, String BySimbolReplace, ref int resultCount)  
        {
            return Zamba.Core.Results_Business.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, indexId, genIndex, UserId, comparateValue, ComparateDateValue, Operation, filter, SortExpression, SymbolToReplace, BySimbolReplace, ref resultCount);
        }

        //public static Int32 getResultsCount(Int32 docTypeId, Int64 indexId, List<ArrayList> genIndex, Int32 UserId, String comparateValue,
        //                                       String ComparateDateValue, String Operation, Boolean filter)
        //{
        //    return Results_Business.getResultsCount(docTypeId, indexId, genIndex, UserId, comparateValue, ComparateDateValue, Operation, filter);
        //}

        #endregion
    }
}

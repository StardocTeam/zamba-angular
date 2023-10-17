using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Zamba.Core;
using System.IO;
using System.Configuration;

namespace Zamba.Services
{
    /// <summary>
    /// Clase que se encarga del manejo de los metodos para los results
    /// 
    /// Creado 28/09/07 - Marcelo
    /// </summary>
    public class SResult : IService
    {
        #region Attributes
        private Results_Business Results_Business;
        private WebSearch ModDocuments;
        #endregion

        #region Miembros de IService
        public ServicesTypes ServiceType()
        {
            return ServicesTypes.Result;
        }
        #endregion

        #region Constructors

        public SResult()
        {
            Results_Business = new Results_Business();
            ModDocuments = new WebSearch();
        }
        #endregion



        /// <summary>
        /// Devuelve los datos de los results llamando a la vista
        /// </summary>
        /// <param name="docTypeId">Id del tipo de Documento a buscar</param>
        /// <param name="indexId">Id del indice a comparar</param>
        ///<param name="comparateValue">Valor a comparar contra el indice</param>
        /// <returns></returns>
        public DataSet getResultsData(Int32 docTypeId, Int32 indexId, String comparateValue,
                                             List<ArrayList> genIndex, Boolean filter, Int32 UserId)
        {
            return Results_Business.getResultsData(docTypeId, indexId, genIndex, UserId, comparateValue, filter);
        }

        public DataTable getResultsAndPageQueryResults(Int16 PageId, Int16 PageSize, Int32 docTypeId, Int64 indexId, List<ArrayList> genIndex, Int32 UserId, String comparateValue,
                                               String ComparateDateValue, String Operation, Boolean filter, String SortExpression, String SymbolToReplace, String BySimbolReplace, ref int resultCount)
        {
            return Results_Business.getResultsAndPageQueryResults(PageId, PageSize, docTypeId, indexId, genIndex, UserId, comparateValue, ComparateDateValue, Operation, filter, SortExpression, SymbolToReplace, BySimbolReplace, ref resultCount);
        }

        public DataTable webRunSearch(string[] SQL)
        {
            return ModDocuments.WebRunSearch(SQL);
        }

        /// <summary>
        /// Devuelve los datos(en forma de tabla) de una búsqueda por todos los indices.
        /// </summary>
        /// <param name="textToSearch"></param>
        /// <returns></returns>
        public DataTable RunWebTextSearch(ISearch search,List<Int64> DocTypesSelected)
        {
            return ModDocuments.RunWebTextSearch(search, DocTypesSelected);
        }


        public string[] webMakeSearch(List<IDocType> dtypes, List<IIndex> Indexs, IUser currentUser)
        {
            return ModDocuments.WebMakeSearch(dtypes, Indexs, currentUser);
        }

        public INewResult GetNewNewResult(long docTypeId)
        {
            return Results_Business.GetNewNewResult(docTypeId);
        }

        public INewResult GetNewNewResult(long docId, IDocType docType)
        {
            return Results_Business.GetNewNewResult(docId, docType);
        }

        public INewResult GetNewNewResult(IDocType DocType, int _UserId, string File)
        {
            return Results_Business.GetNewNewResult(DocType, _UserId, File);
        }

        public IResult GetNewResult(long docId, IDocType docType)
        {
            return Results_Business.GetNewResult(docId, docType);
        }

        public INewResult GetNewResult(long docTypeId, string File)
        {
            return Results_Business.GetNewResult(docTypeId, File);
        }

        public IResult GetResult(Int64 DocId, Int64 DocTypeId, Boolean FullLoad)
        {
            return Results_Business.GetResult(DocId, DocTypeId, FullLoad);
        }

         public InsertResult Insert(ref INewResult newresult, string fileName, long docTypeId, List<IIndex> indexs,long userid, Int64 newId  = 0)
         {
            foreach (IIndex i in indexs)
            {
                foreach (IIndex ir in newresult.Indexs)
                {
                    if (i.ID == ir.ID)
                    {
                        ir.DataTemp = i.DataTemp;
                        ir.dataDescription = i.dataDescription;
                        ir.Data = i.Data;
                        ir.dataDescriptionTemp = i.dataDescriptionTemp;
                    }
                }
            }

            newresult.DocTypeId = docTypeId;
            newresult.File = fileName;
            newresult.UserId = userid;
            return Insert(ref newresult, false, false, false, false, false, false, false, false, true, userid, newId);
         }

        public InsertResult Insert(ref INewResult newresult, bool move, bool ReIndexFlag, bool reemplazarFlag, bool showQuestions, bool isVirtual, bool isReplica, bool hasName, bool throwex, bool refreshWfAfterAInsert,long userid=0, Int64 newId = 0, bool ExecuteEntryRules = true)
        {
            return Results_Business.Insert(ref newresult, move, ReIndexFlag, reemplazarFlag, showQuestions, isVirtual, isReplica, hasName, throwex, refreshWfAfterAInsert,userid, newId, ExecuteEntryRules);
        }


        public InsertResult InsertBaremo(INewResult newresult, string fileName, long docTypeId, List<IIndex> indexs, long userid = 0)
        {
            newresult.Indexs = indexs;                       
            newresult.DocTypeId = docTypeId;
            newresult.File = fileName;
            newresult.UserId = userid;
            return Insert(ref newresult, true, false, false, false, true, false, false, false, true, userid);
        }

        public void deleteBaremo(IResult Result)
        {
            delete((Result) Result);
        }

        public void delete(IResult Result)
        {
            Results_Business.Delete(ref Result);
        }

        public void SaveModifiedIndexs(IResult Result, List<Int64> modifiedIndexs, List<IIndex> indexs)
        {
            Result.Indexs = indexs;
            SaveModifiedIndexs(ref Result, modifiedIndexs);
        }


        public int GetFileIcon(string File)
        {
            return Results_Business.GetFileIcon(File);
        }

        public byte[] GetFileFromResultForWeb(IResult result, out Boolean IsBlob )
        {
            IsBlob = false;
            //volumen db
            ZOptBusiness zoptb = new ZOptBusiness();
            if (result.Disk_Group_Id > 0 &&
               (VolumesBusiness.GetVolumeType(result.Disk_Group_Id) == (int)VolumeType.DataBase ||
               (!String.IsNullOrEmpty(zoptb.GetValue("ForceBlob")) && bool.Parse(zoptb.GetValue("ForceBlob")))))
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "LoadFileFromDB");
                Results_Business.LoadFileFromDB(ref result);
                IsBlob = true;
                //si es un archivo viejo no esta codificado
                if (result.EncodedFile == null)
                {
                    //se lo codifica e inserta en la base
                    result.EncodedFile = FileEncode.Encode(result.RealFullPath());
                   Results_Business.InsertIntoDOCB(result);
                }
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "result.RealFullPath" + result.RealFullPath());
                if (File.Exists(result.RealFullPath()))
                {
                    //es un volumen en disco, se lo serializa en el momento
                    result.EncodedFile = FileEncode.Encode(result.RealFullPath());
                }
                IsBlob = false;
            }
            zoptb = null;
            return result.EncodedFile;
        }

      

        //Ezequiel: Metodo que completa la propiedad encodefile del result
        public void LoadFileFromDB(ref IResult res)
        {
            Results_Business.LoadFileFromDB(ref res);
        }

        public void SaveModifiedIndexs(ref IResult result, List<Int64> modifiedIndexs)
        {
            Results_Business.SaveModifiedIndexData(ref result, true, true, modifiedIndexs, null);
        }

        public string GetResultName(long docId, long docTypeId)
        {
            return Results_Business.GetName(docId, docTypeId);
        }

        /// <summary>
        /// Inserta o actualiza un documento, en base al array de bytes y el nombre del archivo nuevo
        /// </summary>
        /// <param name="res"></param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public void InserDocFile(IResult res, byte[] file, string fileName)
        {
            Results_Business.InsertDocFile(res,file,fileName);
        }

        /// <summary>
        /// Llama al ws para obtener el blob de un documento de zamba
        /// </summary>
        /// <param name="docTypeId"></param>
        /// <param name="docId"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public byte[] GetWebDocFileWS(Int64 docTypeId, Int64 docId, Int64 userID)
        {
            return Results_Business.GetWebDocFileWS(docTypeId, docId, userID);
        }

        /// <summary>
        /// Llama al ws para copiar el blob de un doc de zamba a fisico
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="docTypeId"></param>
        /// <returns></returns>
        public bool CopyBlobToVolumeWS(long docId, long docTypeId)
        {
            return Results_Business.CopyBlobToVolumeWS(docId, docTypeId);
        }

        /// <summary>
        /// Llama al ws para insertar un blob a un documento de zamba.
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="docTypeId"></param>
        /// <param name="fileBytes"></param>
        /// <param name="incomingFile"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool InsertDocFileWS(long docId, long docTypeId, byte[] fileBytes, string incomingFile, long userId)
        {
            return Results_Business.InsertDocFileWS(docId, docTypeId, fileBytes, incomingFile, userId);
        }

        public DataTable loadDoSearchResults(long UserID, string ObjectSearch)
        {
            return Results_Business.loadDoSearchResults(UserID, ObjectSearch);
        }
       

        public object removeDoSearchResults(long userId, string Mode)
        {
            return Results_Business.removeDoSearchResults(userId, Mode);
        }
    }
}
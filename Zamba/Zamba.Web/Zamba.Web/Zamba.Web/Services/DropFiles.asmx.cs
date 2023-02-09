using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Collections;
using System.Web.Script.Services;
using System.Data;
using System.Text;
using Zamba;

namespace Zamba.Web.Services
{
    /// <summary>
    /// Summary description for DropFiles
    /// </summary>
    [WebService(Namespace = "ScriptWebServices")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [ScriptService]
    public class DropFiles : System.Web.Services.WebService
    {
        public class insertResultDto
        {

            public insertResultDto(string ErrorMessage, InsertResult InsertResult)
            {
                this.errorMessage = ErrorMessage;
                this.insertResult = InsertResult;
            }

            public insertResultDto(InsertResult InsertResult, List<long> newDocIds)
            {
                this.insertResult = InsertResult;
                this.newDocIds = newDocIds;
            }

            public InsertResult insertResult { get; set; }
            public string errorMessage { get; set; }
            public List<Int64> newDocIds { get; set; } = new List<long>();
            public Int64 entityId { get; set; }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public insertResultDto InsertDoc(Int64 parentEntityId, Int64 parentDocId, Int64 newEntityId, List<string> FileNames)
        {
            Results_Business RB = new Results_Business();
            try
            {
                IResult parentResult = RB.GetResult(parentDocId, parentEntityId, true);
                string ErrorMsg = string.Empty;

                ZOptBusiness zopt = new ZOptBusiness();
                IndexsBusiness IB = new IndexsBusiness();
                List<IIndex> parentIndexs = parentResult.Indexs;
                List<IIndex> newResultIndexs = null;

                if (newEntityId != 0)
                {
                    newResultIndexs = IB.GetIndexsSchemaAsListOfDT(newEntityId);

                    foreach (IIndex parentindex in parentIndexs)
                    {
                        foreach (IIndex newindex in newResultIndexs)
                        {
                            if (parentindex.ID == newindex.ID)
                            {
                                newindex.Data = parentindex.Data;
                                newindex.Data = parentindex.dataDescription;
                                newindex.DataTemp = parentindex.Data;
                                newindex.dataDescriptionTemp = parentindex.dataDescription;

                                if (newindex.Required && string.IsNullOrEmpty(newindex.DataTemp))
                                {
                                    ErrorMsg = string.Format("El indice {0} es obligatorio", newindex);
                                    return new insertResultDto(ErrorMessage: ErrorMsg, InsertResult: InsertResult.ErrorIndicesIncompletos);
                                }
                            }
                        }
                    }
                }


                long docTypeId = newEntityId;
                InsertResult res = InsertResult.NoInsertado;
                SResult sResult = new SResult();
                List<Int64> newDocIds = new List<long>();

                //Se crea el result a insertar
                foreach (string filename in FileNames)
                {
                    if (newEntityId != 0)
                    {
                        INewResult newresult = new SResult().GetNewNewResult(docTypeId);
                        res = sResult.Insert(ref newresult, filename, docTypeId, newResultIndexs);
                        if (res == InsertResult.Insertado)
                        {
                            newDocIds.Add(newresult.ID);
                            if (newresult.Disk_Group_Id > 0 &&
                                VolumesBusiness.GetVolumeType(newresult.Disk_Group_Id) != (int)VolumeType.DataBase)
                            {
                                //Guarda el documento en el volumen utilizando un webservice
                                string useWebService = zopt.GetValue("UseWebService");
                                string wsResultsUrl = ZOptBusiness.GetValueOrDefault("WSResultsUrl", "http://www.zamba.com.ar/zambastardoc");

                                if (!String.IsNullOrEmpty(useWebService) && bool.Parse(useWebService) && !String.IsNullOrEmpty(wsResultsUrl))
                                {
                                    sResult.CopyBlobToVolumeWS(newresult.ID, newresult.DocTypeId);
                                }
                            }

                            //Codigo de ejecucion de reglas de entrada de la nueva tarea
                            STasks stasks = new STasks();
                            ITaskResult Task = stasks.GetTaskByDocId(newresult.ID);

                            if (Session["ListOfTask"] == null)
                            {
                                Session["ListOfTask"] = new List<IExecutionRequest>();
                            }
                            IExecutionRequest exec = new ExecutionRequest();
                            exec.ExecutionTask = Task;
                            exec.StartRule = -1;
                            ((List<IExecutionRequest>)Session["ListOfTask"]).Add(exec);

                        }
                        else if (res == InsertResult.ErrorIndicesIncompletos || res == InsertResult.ErrorIndicesInvalidos)
                        {
                            ErrorMsg = "Los indices marcados con (*) son de ingreso obligatorio";
                            ZTrace.WriteLineIf(ZTrace.IsVerbose,ErrorMsg);
                            return new insertResultDto(ErrorMessage: ErrorMsg, InsertResult: InsertResult.ErrorIndicesIncompletos);
                        }
                        else if (res == InsertResult.NoInsertado)
                        {
                            /* ErrorMsg += "Se produjo un error al insertar el documento: " + filename;
                             return new insertResultDto(ErrorMessage: ErrorMsg, InsertResult: InsertResult.ErrorInsertar);*/
                            ErrorMsg = "Se produjo un error al insertar el documento compruebe su conexión, recargue y vuelva a ingresar al sistema";
                            ZClass.raiseerror(new Exception(ErrorMsg));
                            return new insertResultDto(ErrorMessage: ErrorMsg, InsertResult: InsertResult.ErrorInsertar);
                        }
                    }
                    else
                    {
                        STasks Stasks = new STasks();
                        ITaskResult TaskResult = Stasks.GetTaskByDocId(Convert.ToInt64(parentResult.ID));
                        if (TaskResult != null)
                        {
                            RB.HistoricDocumentDropzone(TaskResult.TaskId, TaskResult.Name, TaskResult.DocTypeId, TaskResult.DocType.Name, TaskResult.StepId, TaskResult.WorkId, TaskResult.State.Name);
                        }

                        Data.Transaction t = null;
                        RB.ReplaceDocument(ref parentResult, filename, false,  t);
                        
                    }
                }
                return new insertResultDto(InsertResult: InsertResult.Insertado, newDocIds: newDocIds);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return new insertResultDto(ErrorMessage: ex.ToString(), InsertResult: InsertResult.ErrorInsertar);
            }
            finally
            {
                RB = null;
            }
        }
    }
}

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
using System.Diagnostics;
using System.Xml.Serialization;


namespace ScriptWebServices
{
    /// <summary>
    /// Summary description for JsonServices
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class TasksService : System.Web.Services.WebService
    {
        //Constantes para devolver los ids de los tabs
        const string DOCIDFORMAT = "D{0}";
        const string TASKIDFORMAT = "T{0}";
        const string RESPONSE_ID_FORMAT = "{0}|{1}";

        public TasksService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        //23/08/11:Este método es usado para finalizar las tareas abiertas en la aplicacion Web
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String CloseAllAsignedTask(Int64 UserId)
        {
            try
            {
                STasks STasks = new STasks();
                STasks.UpdateUserTaskStateToAsign(UserId);
                STasks = null;
            }    
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return String.Empty;
        }

        //Se habilito la session solo para este metodo y asi poder liberar la instancia de ZCore
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void RemoveConnectionFromWeb(Int64 connectionId, string computer)
        {
            try
            {
                SRights sRights = new SRights();
                sRights.RemoveConnectionFromWeb(connectionId);
                sRights = null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        //06/08/12: Método para obtener IDs de las tabs
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GetTabID(long taskID, long docID, long docTypeId, bool asDoc, long userID)
        {
            STasks STasks;
            SResult sdoc;
            SRights sRights;
            ITaskResult task;

            try
            {
                sdoc = new SResult();

                //Si pide documento directamente no evaluo mas y lo abro como documento.
                if (asDoc && docID > 0)
                {
                    return string.Format(RESPONSE_ID_FORMAT, string.Format(DOCIDFORMAT, docID), sdoc.GetResultName(docID, docTypeId));
                }
                else
                {
                    STasks = new STasks();
                    sRights = new SRights();

                    //Se obtiene la tarea
                    if (taskID == 0)
                    {
                        task = STasks.GetTaskByDocId(docID, userID);
                    }
                    else
                    {
                        task = STasks.GetTask(taskID, userID);
                    }

                    if (task == null)
                    {
                        //Si la tarea es nula puede deberse a que no existe o a que no tiene permisos.
                        //Para este último caso es posible que el docID no se encuentre completo,
                        //por eso se lo busca por el taskId.
                        if (docID <= 0 && taskID > 0)
                        {
                            docID = STasks.GetDocId(taskID);
                        }
                        return string.Format(RESPONSE_ID_FORMAT, string.Format(DOCIDFORMAT, docID), sdoc.GetResultName(docID, docTypeId));
                    }
                    else
                    {
                        if (sRights.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, task.StepId, userID))
                        {
                            return string.Format(RESPONSE_ID_FORMAT, string.Format(TASKIDFORMAT, task.TaskId), task.Name);
                        }
                        else
                        {
                            if (docID <= 0)
                            {
                                docID = task.ID;
                            }
                            return string.Format(RESPONSE_ID_FORMAT, string.Format(DOCIDFORMAT, docID), task.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLineIf(ZTrace.IsInfo, ex.ToString());
                ZClass.raiseerror(ex);
                throw ex;
            }
            finally
            {
                STasks = null;
                sdoc = null;
                sRights = null;
                task = null;
                Trace.Flush();
            }
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTaskCount(long stepId, long usrId, string nodeId)
        {
            try
            {
                string taskCount = new STasks().GetTaskCount(stepId, usrId).ToString();

                return taskCount + "|" + nodeId;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ZFeed> GetUserFeeds(long userId)
        {
            try
            {
                return (new SFeeds()).GetFeeds(userId);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
        }

        /// <summary>
        /// Metodo para recibir la llamada de la vista y pasarla al controler de una nueva ejecucion de reglas
        /// </summary>
        /// <param name="ruleId"></param>
        [WebMethod(true)]
        public void SetNewRuleExecution(long ruleId)
        {
            DynamicButtonController dbC = new DynamicButtonController();
            dbC.SetNewRuleExecution((IUser)HttpContext.Current.Session["User"], ruleId);
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public int KeepSessionAlive()
        {
            try
            {
                HttpContext.Current.Session["SessionRefreshToken"] = DateTime.Now;
                return 1;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return 0;
        }

        [WebMethod]
        public long GetUserIdByName(string userName)
        {
            IUser user = Zamba.Core.UserBusiness.GetUserByname(userName);
            string name=user.Nombres;
            string surname = user.Apellidos;
            long id=user.ID;
            return id;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GetUserId()
        {
            var windowsUserDomain = System.Web.HttpContext.Current.User.Identity.Name;
            var windowsUser = windowsUserDomain.Split('\\')[1];
            IUser user = Zamba.Core.UserBusiness.GetUserByname(windowsUser);

            //this.Context.Response.ContentType = "application/json; charset=utf-8";

            //JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            //return jsonSerialiser.Serialize(jsonList);
            return "Newtonsoft.Json.JsonConvert.SerializeObject(user)";
        }


    }
}
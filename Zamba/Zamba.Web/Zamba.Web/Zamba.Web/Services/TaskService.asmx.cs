using Newtonsoft.Json;
using mshtml;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Zamba;
using Zamba.Core;
using Zamba.Core.WF.WF;
using Zamba.Filters;
using Zamba.Framework;
using Zamba.Services;
using Zamba.Web;
using static Zamba.Core.UserBusiness;

namespace ScriptWebServices
{
    /// <summary>
    /// Descripción breve de TaskService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class TaskService : System.Web.Services.WebService
    {

        //Constantes para devolver los ids de los tabs
        const string DOCIDFORMAT = "D{0}";
        const string TASKIDFORMAT = "T{0}";
        const string RESPONSE_ID_FORMAT = "{0}|{1}";

        //public TaskService()
        //{
        //    //Uncomment the following line if using designed components 
        //    //InitializeComponent(); 
        //}

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
                Zamba.Membership.MembershipHelper.SetCurrentUser(null);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }


        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTaskCount(long stepId, long usrId, string nodeId)
        {
            string taskCount = new WFTaskBusiness().GetTaskCount(stepId, true, usrId).ToString();
            return taskCount + "|" + nodeId;
        }


        //[WebMethod(true)]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public List<string> GetTaskCountArray(long usrId, string[] nodeIdArray, long[] stepIdArray)
        //{
        //    var taskCount = new List<string>();
        //    int i = 0;
        //    try
        //    {
        //        foreach (var node in nodeIdArray)
        //        {
        //            taskCount.Add(GetTaskCount(stepIdArray[i++], usrId, node));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //    }
        //    return taskCount;
        //}

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<ZFeed> GetUserFeeds(long userId)
        {
            return (new SFeeds()).GetFeeds(userId);
        }




        /// <summary>
        /// Metodo para recibir la llamada de la vista y pasarla al controler de una nueva ejecucion de reglas
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="userId"></param>
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void SetNewRuleExecution(long ruleId, long userId)
        {
            try
            {
                UserBusiness UBR = new UserBusiness();
                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                if (user == null)
                {
                    user = UBR.ValidateLogIn(userId, ClientType.Web);
                }
                DynamicButtonController dbC = new DynamicButtonController();
                dbC.SetNewRuleExecution(user, ruleId);
            }
            catch (Exception e)
            {
                throw (e);
            }
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
            UserBusiness UserBusiness = new UserBusiness();
            IUser user = UserBusiness.GetUserByname(userName, true);
            long id = user.ID;
            return id;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public String GetUserId()
        {
            UserBusiness UserBusiness = new UserBusiness();
            var windowsUserDomain = System.Web.HttpContext.Current.User.Identity.Name;
            var windowsUser = windowsUserDomain.Split('\\')[1];
            IUser user = UserBusiness.GetUserByname(windowsUser, true);

            //this.Context.Response.ContentType = "application/json; charset=utf-8";

            //JavaScriptSerializer jsonSerialiser = new JavaScriptSerializer();
            //return jsonSerialiser.Serialize(jsonList);
            return Newtonsoft.Json.JsonConvert.SerializeObject(user);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetUserIdWeb()
        {
            try
            {
                string user = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }

        }
        public partial class dataTimeOut
        {

            public string timeOutUserText;
            public string TimeOutUserText
            {
                get { return timeOutUserText; }
                set { timeOutUserText = value; }
            }
            public string timeOutPass;
            public string TimeOutPass
            {
                get { return timeOutPass; }
                set { timeOutPass = value; }
            }

            public int connectionId;
            public int ConnectionId
            {
                get { return connectionId; }
                set { connectionId = value; }
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string TimeOutLogin(dataTimeOut data)
        {
            UserBusiness UB = new UserBusiness();

            Login log = new Login();
            //log.timeOutLogin(data.TimeOutUserText, data.TimeOutPass, data.ConnectionId);
            Zamba.Core.IUser user = UB.ValidateLogIn(data.TimeOutUserText, data.TimeOutPass, ClientType.Web);
            log.timeOutLogin(user, data.ConnectionId);
            return "loginok";
        }

        /// <summary>
        /// Metodo para recibir la llamada de la vista y verificar que el currenUser no este null
        /// </summary>
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string IsConnectionActive(int userId, string token, int userLocalStorage)
        {
            try
            {


                Dictionary<string, string> result = new Dictionary<string, string>();
                var newresults = string.Empty;
                Results_Business RB = new Results_Business();
                UserBusiness ub = new UserBusiness();

                bool IsValid = Zamba.Membership.MembershipHelper.CurrentUser != null;
                bool IsseccionValid = RB.getValidateActiveSession(userId, token);
                //userId = 183;
                DataTable sessionInfo = RB.getUserSessionInfoforToken(userId);
                var validUserToken = sessionInfo.Rows[0]["token"].ToString();


                // para evaluar si el curren esta muerto y el usuario de la url esta vivo hacer relogin
                if (!IsValid && IsseccionValid && userId == userLocalStorage)
                {
                    ub.ValidateLogIn(userId, ClientType.Web);
                    IsValid = true;
                }


                bool isReload = Zamba.Membership.MembershipHelper.CurrentUser == null || Zamba.Membership.MembershipHelper.CurrentUser.ID != userId;
                bool RebuildUrl = Zamba.Membership.MembershipHelper.CurrentUser != null && Zamba.Membership.MembershipHelper.CurrentUser.ID != userLocalStorage;

                // esta condicion es para cuando se reconstruye la url
                if (validUserToken != null && validUserToken != token)
                {
                    RebuildUrl = true;
                    isReload = true;

                }
                else
                {

                    // esta condicion es para cuando se reconstruye la url
                    if (!RebuildUrl)
                    {

                        RebuildUrl = IsValid && isReload && !RebuildUrl;
                    }

                }


                //if (IsValid && !RebuildUrl)
                //{

                //    IsValid = RB.getValidateActiveSession(userId, token);

                //    if (IsValid)
                //    {

                //        ub.ValidateLogIn(userId, ClientType.Web);
                //    }

                //}

                result.Add("IsValid", IsValid.ToString());
                result.Add("isReload", isReload.ToString());
                result.Add("RebuildUrl", RebuildUrl.ToString());
                result.Add("NewToken", validUserToken);


                newresults = JsonConvert.SerializeObject(result);

                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("No se pudo validar el requerimiento");
            }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool CheckTimeOut(int CurrentUserId, int ConnectionId)
        {
            Ucm ucm = new Ucm();
            if (CurrentUserId == 0 || ConnectionId == 0 || ucm.verifyIfUserStillExistsInUCM(ConnectionId) == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public partial class Filterdata
        {
            public long indexid;
            public long IndexId
            {
                get { return indexid; }
                set { indexid = value; }
            }
            public long currentUserId;
            public long CurrentUserId
            {
                get { return currentUserId; }
                set { currentUserId = value; }
            }

            public string valueString;
            public string ValueString
            {
                get { return valueString; }
                set { valueString = value; }
            }

            public long stepid;
            public long StepId
            {
                get { return stepid; }
                set { stepid = value; }
            }


            public string entitiesIds;
            public string EntitiesIds
            {
                get { return entitiesIds; }
                set { entitiesIds = value; }
            }


            public string compareOperator;
            public string CompareOperator
            {
                get { return compareOperator; }
                set { compareOperator = value; }
            }


        }

        public partial class UsedFilters
        {
            public long stepid;
            public long StepId
            {

                get { return stepid; }
                set { stepid = value; }
            }

            public long currentUserid;
            public long CurrentUserId
            {

                get { return currentUserid; }
                set { currentUserid = value; }
            }
        }

        public partial class FilterDelete
        {
            public long id;
            public long Id
            {
                get { return id; }
                set { id = value; }

            }

            public long stepid;
            public long StepId
            {

                get { return stepid; }
                set { stepid = value; }
            }

            public long currentUserid;
            public long CurrentUserId
            {

                get { return currentUserid; }
                set { currentUserid = value; }
            }
        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Dictionary<string, long> getUsedFilters(UsedFilters usedfilters)
        {
            Dictionary<string, long> filters = new Dictionary<string, long>();
            //List<string> filters = new List<string>();
            IFiltersComponent ss = new FiltersComponent();
            WFStepBusiness WFSB = new WFStepBusiness();
            DataTable DocTypes = WFSB.GetDocTypesByWfStepAsDT(usedfilters.StepId, usedfilters.currentUserid);

            if (DocTypes.Rows.Count == 0) return null;
            var UsedFilter = ss.GetLastUsedFilters(Convert.ToInt64(DocTypes.Rows[0][0]), usedfilters.currentUserid, true);

            foreach (FilterElem d in UsedFilter)
            {
                //filters.Clear();
                filters.Add(d.Text, d.Id);
                //filters.Add(d.Id.ToString());
            }

            if (filters != null)
            {
                return filters;
            }
            else
            {
                return null;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void filterdata(Filterdata filterdata)
        {
            if (filterdata.CompareOperator == "")
            {
                filterdata.CompareOperator = "=";
            }
            try
            {
                sDocType sDocType = new sDocType();
                DataTable DocTypes = null;
                if (filterdata.StepId > 0)
                {
                    WFStepBusiness WFSB = new WFStepBusiness();
                    DocTypes = WFSB.GetDocTypesByWfStepAsDT(filterdata.StepId, filterdata.currentUserId);
                    WFSB = null;
                }
                else if (filterdata.EntitiesIds != string.Empty)
                {
                    DocTypesBusiness DTB = new DocTypesBusiness();
                    DataSet DsDocTypes = DTB.GetDocTypesIdsAndNames(filterdata.EntitiesIds);
                    DocTypes = DsDocTypes.Tables[0];
                }

                IndexsBusiness IndexB = new IndexsBusiness();
                IFiltersComponent ss = new FiltersComponent();
                if (filterdata.IndexId < 0)
                {
                    string indexName = GridColumns.VisibleColumns.FirstOrDefault(x => x.Value == filterdata.IndexId).Key;
                    var search = ss.SetNewFilter(filterdata.IndexId,
                        indexName,
                        GridColumns.ZambaColumnsType[indexName],
                        filterdata.currentUserId,
                        filterdata.CompareOperator,
                        filterdata.ValueString,
                        Convert.ToInt64(DocTypes.Rows[0]["DOC_TYPE_ID"]),
                        true,
                        GridColumns.VisibleColumns.FirstOrDefault(x => x.Value == filterdata.IndexId).Key,
                        IndexAdditionalType.LineText,
                        "manual",
                        true);
                }
                else
                {
                    var ind = IndexB.GetIndex(filterdata.IndexId);
                    var search = ss.SetNewFilter(filterdata.IndexId,
                        ind.Name,
                        ind.Type,
                        filterdata.currentUserId,
                        filterdata.CompareOperator,
                        filterdata.ValueString,
                        Convert.ToInt64(DocTypes.Rows[0]["DOC_TYPE_ID"]),
                        true,
                        ind.Name,
                        IndexAdditionalType.LineText,
                        "manual",
                        true);
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                Console.WriteLine(ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void deleteFilter(FilterDelete deleteFilter)
        {
            FiltersComponent FC = new FiltersComponent();
            WFStepBusiness WFSB = new WFStepBusiness();
            DataTable DocTypes = WFSB.GetDocTypesByWfStepAsDT(deleteFilter.StepId, deleteFilter.CurrentUserId);


            var UsedFilter = FC.GetLastUsedFilters(Convert.ToInt64(DocTypes.Rows[0][0]), deleteFilter.CurrentUserId, true);

            IFilterElem filterElement = null;
            foreach (FilterElem d in UsedFilter)
            {
                if (d.Id == deleteFilter.Id)
                {
                    filterElement = d;
                }
            }
            FC.RemoveFilter(filterElement, true);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string OpenRules()
        {
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                var user = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                StringBuilder querybldr = new StringBuilder();
                querybldr.Append("SELECT distinct * FROM WFRules INNER JOIN WFStep ON WFRules.step_Id = WFStep.step_Id WHERE type = 40");
                querybldr.Append("AND WFRules.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=19 and (groupid= " + user + " OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= " + user + ")))");
                querybldr.Append("AND WFRules.step_id IN (SELECT aditional FROM USR_RIGHTS WHERE objid=42 and rtype=35 and (groupid= " + user + " OR groupid IN (SELECT groupid FROM usr_r_group WHERE usrid= " + user + ")))");
                string query = string.Format(querybldr.ToString(), Zamba.Membership.MembershipHelper.CurrentUser.ID);
                var RuleId = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, query);
                if (RuleId != null)
                    return RuleId.ToString();
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearAllCache()
        {
            CacheBusiness.ClearCaches();
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearRulesCache()
        {
            CacheBusiness.ClearRulesCaches();
        }
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearRightsCache(Int64 userId)
        {
            CacheBusiness.ClearRightsCaches(userId);
        }
        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void ClearStructureCache()
        {
            CacheBusiness.ClearStructureCaches();
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void ClearUserCache(long userId)
        {
            UserBusiness UB = new UserBusiness();
            UB.ClearUserCache(userId);
        }

        [WebMethod(true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public void CleanSessionVariable(string varName)
        {            
                Session[varName] = null;            
        }
    }
}
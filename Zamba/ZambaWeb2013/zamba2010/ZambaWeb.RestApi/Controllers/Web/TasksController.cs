using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Framework;
using Zamba.Data;
using ZambaWeb.RestApi.Controllers.Web;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.FileTools;
using System.IO;
using Zamba.Membership;
using System.Security.Cryptography.X509Certificates;
using static ZambaWeb.RestApi.Controllers.SearchController;

namespace ZambaWeb.RestApi.Controllers
{
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Tasks")]
    //[Authorize]
    public class TasksController : ApiController
    {

        #region Init&ClassHelpers
        #region Constantes

        private const string DISKGROUPIDCOLUMNNAME = "DISK_GROUP_ID";
        private const string DOC_ID_COLUMNNAME = "Doc_ID";
        private const string DOCID_COLUMNNAME = "DocId";
        private const string DOC_FILE_COLUMNNAME = "DOC_FILE";
        private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
        private const string DISK_VOL_ID_COLUMNNAME = "disk_Vol_id";
        private const string DISK_VOL_PATH_COLUMNNAME = "DISK_VOL_PATH";
        private const string DO_STATE_ID_COLUMNNAME = "Do_State_ID";
        private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
        private const string IMAGEN_COLUMNNAME = "Imagen";
        private const string PLATTER_ID_COLUMNNAME = "PLATTER_ID";
        private const string VOL_ID_COLUMNNAME = "VOL_ID";
        private const string OFFSET_COLUMNNAME = "OFFSET";

        public void UpdateTaskState(long taskId, TaskStates asignada)
        {

        }

        ///// <summary>
        ///// Esta funcion itera una lista de archivos para ejecutar el metodo "UpdateTaskIndex" por cada uno de ellos.
        ///// </summary>
        ///// <param name="DoCTypeId">Doc Type Id</param>
        ///// <param name="List_DocId">Lista de Ids de los archivos</param>
        ///// <param name="IndexId">Index Id</param>
        ///// <param name="Data">Data</param>
        //public void Call_UpdateTaskIndex(long DoCTypeId, List<long> List_DocId, int IndexId, string Data)
        //{
        //    foreach (long item in List_DocId)
        //    {
        //        UpdateTaskIndex(DoCTypeId, item, IndexId, Data);
        //    }
        //}

        public void UpdateTaskIndex(long DoCTypeId, long DocId, int IndexId, string Data)
        {
            Results_Business RB = new Results_Business();
            IResult R = RB.GetResult(DocId, DoCTypeId, true);
            IIndex I = R.get_GetIndexById(IndexId);
            I.DataTemp = Data;
            RB.SaveModifiedIndexData(ref R, true, false, new List<long>() { IndexId }, null);

            UserBusiness UB = new UserBusiness();
            UB.SaveAction(R.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, R.Name, 0);
        }
        private const string ICON_ID_COLUMNNAME = "ICON_ID";
        private const string SHARED_COLUMNNAME = "SHARED";
        private const string VER_PARENT_ID_COLUMNNAME = "ver_Parent_id";
        private const string VERSION_COLUMNNAME = "version";
        private const string ROOTID_COLUMNNAME = "RootId";
        private const string ORIGINAL_FILENAME_COLUMNNAME = "original_Filename";
        private const string NUMEROVERSION_COLUMNNAME = "NumeroVersion";
        private const string NUMERO_DE_VERSION_COLUMNNAME = "numero de version";
        private const string CRDATE_COLUMNNAME = "crdate";
        private const string NAME1_COLUMNNAME = "NAME1";
        private const string STEP_ID_COLUMNNAME = "Step_Id";
        private const string ICONID_COLUMNNAME = "IconId";
        private const string CHECKIN_COLUMNNAME = "CheckIn";
        private const string TASK_ID_COLUMNNAME = "Task_ID";
        private const string WFSTEPID_COLUMNNAME = "WfStepId";
        private const string ASIGNADO_COLUMNNAME = "Asignado";
        private const string STATE_COLUMNNAME = "State";
        private const string ESTADO_TAREA_COLUMNNAME = "Estado";
        private const string SITUACION_COLUMNNAME = "Situacion";
        private const string EXPIREDATE_COLUMNNAME = "ExpireDate";
        private const string VENCIMIENTO_COLUMNNAME = "Vencimiento";
        private const string NAME_COLUMNNAME = "Name";
        private const string TASK_STATE_ID_COLUMNNAME = "task_state_id";
        private const string INGRESO_COLUMNNAME = "Ingreso";
        private const string USER_ASIGNED_COLUMNNAME = "User_Asigned";
        private const string USER_ASIGNED_BY_COLUMNNAME = "User_Asigned_By";
        private const string DATE_ASIGNED_BY_COLUMNNAME = "Date_asigned_By";
        private const string REMARK_COLUMNNAME = "Remark";
        private const string TAG_COLUMNNAME = "Tag";
        private const string DOCTYPEID_COLUMNNAME = "DoctypeId";
        private const string TASKCOLOR_COLUMNNAME = "TaskColor";
        private const string VER_COLUMNNAME = "Ver";
        private const string WORK_ID_COLUMNNAME = "Work_Id";
        private const string NOMBRE_DOCUMENTO_COLUMNNAME = "Nombre Documento";
        private const string SOLICITUD_COLUMNNAME = "Solicitud";

        #endregion
        #region Variables Privadas

        private string nombreDocumento_currUserConfig;
        private string imagen_currUserConfig;
        private string ver_currUserConfig;
        private string EstadoTarea_currUserConfig;
        private string Asignado_currUserConfig;
        private string Situacion_currUserConfig;
        private string Nombre_Original_currUserConfig;
        private string TipoDocumento_currUserConfig;

        private string version_UserConfig;
        private string NroVersion_UserConfig;

        private string FechaCreacion_currUserConfig;
        private string FechaModificacion_currUserConfig;

        private Zamba.Core.ITaskResult iTaskResult;
        RightsBusiness RB = new RightsBusiness();
        UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();

        private bool dontLoadUAC;
        private System.Collections.Generic.List<Int64> idRules = new System.Collections.Generic.List<Int64>();
        private Hashtable hshRulesNames = new Hashtable();

        private bool StateWasChanged = false;

        #endregion


        private void loadPreferences()
        {
            nombreDocumento_currUserConfig = GridColumns.NOMBRE_DOCUMENTO_COLUMNNAME;
            imagen_currUserConfig = GridColumns.IMAGEN_COLUMNNAME;
            ver_currUserConfig = GridColumns.VER_COLUMNNAME;
            TipoDocumento_currUserConfig = GridColumns.DOC_TYPE_NAME_COLUMNNAME;
            EstadoTarea_currUserConfig = GridColumns.STATE_COLUMNNAME;
            Asignado_currUserConfig = GridColumns.ASIGNADO_COLUMNNAME;
            Situacion_currUserConfig = GridColumns.SITUACION_COLUMNNAME;
            Nombre_Original_currUserConfig = GridColumns.ORIGINAL_FILENAME_COLUMNNAME;
            version_UserConfig = GridColumns.VERSION_COLUMNNAME;
            NroVersion_UserConfig = GridColumns.NUMERO_DE_VERSION_COLUMNNAME;
            FechaCreacion_currUserConfig = GridColumns.CRDATE_COLUMNNAME;
            FechaModificacion_currUserConfig = GridColumns.LASTUPDATE_COLUMNNAME;
        }

        public TasksController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
            wFExecution._haceralgoEvent += HandleWFExecutionPendingEvents;
        }

        WFExecution wFExecution = new WFExecution(MembershipHelper.CurrentUser);


        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("user"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
                }


                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        private Hashtable GetDoEnableRules()
        {
            ITaskResult TaskResult = new TaskResult();
            try
            {
                //Se comenta esto, dado que no es necesario obtener la etapa para ver sus reglas
                //Obtenemos las reglas de esa etapa
                //Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

                Hashtable returnEnableRules = new Hashtable();
                Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
                Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
                List<Int64> ExecutedIDs = new List<Int64>();
                Hashtable Params = new Hashtable();
                List<Int64> PendingChildRules = new List<Int64>();
                //Dim tempRuleBooleanList As List(Of Boolean) = New List(Of Boolean)()
                System.Collections.Generic.List<ITaskResult> List = new System.Collections.Generic.List<ITaskResult>();
                List.Add(TaskResult);

                //Se mueven estas variables del foreach, ya que se utilizara linq y for, 
                //para luego ver si se puede hacer en paralelo
                WFRulesBusiness WFRB = new WFRulesBusiness();
                bool RefreshRule = false;
                SRules sRules = new SRules();


                List<IWFRuleParent> rules = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

                if ((rules != null))
                {
                    var temprulesOpenDoc = from rule in rules where rule.RuleType == TypesofRules.AbrirDocumento && string.Compare(rule.GetType().FullName, "Zamba.WFActivity.Regular.DoShowForm") != 0 select rule;
                    var rulesOpenDoc = temprulesOpenDoc.ToList();

                    if (rulesOpenDoc != null && rulesOpenDoc.Count > 0)
                    {
                        if (TaskResult.WfStep.Rules.Count == 0)
                        {
                            var userActionRules = from rule in rules where rule.ParentType == TypesofRules.AccionUsuario select rule;

                            TaskResult.WfStep.Rules.AddRange(userActionRules);
                        }

                        //for (int i = 0; i <= rulesOpenDoc.Count - 1; i++)
                        //{
                        //    RefreshRule = rulesOpenDoc[i].RefreshRule.Value;
                        //    WFRB.ExecuteWebRule(rulesOpenDoc[i].ID, List, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh);
                        //}
                    }
                }

                return TaskResult.UserRules;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        public partial class ListNews
        {
            //public Int64 Id { get; set; }
            public string Name { get; set; }
            public string crdate { get; set; }
            public string value { get; set; }

            public ListNews(string Name, string crdate, string value)
            {
                //this.Id = id;
                this.Name = Name;
                this.crdate = crdate;
                this.value = value;
            }
        }


        private ITaskResult TaskResult
        {
            get { return iTaskResult; }
            set
            {
                iTaskResult = value;

                try
                {
                    FillHeader();
                    WFTaskBusiness WFTB = new WFTaskBusiness();
                    if (iTaskResult.TaskState == TaskStates.Desasignada && iTaskResult.m_AsignedToId != 0)
                    {
                        iTaskResult.TaskState = TaskStates.Asignada;
                        STasks staks = new STasks();
                        staks.UpdateTaskState(iTaskResult.TaskId, TaskStates.Asignada);
                        staks = null;
                    }

                    TaskResult.EditDate = DateTime.Now;
                    SetStepName(TaskResult.StepId);
                    //UACCell.Controls.Clear();
                    ////DisablePropertyControls();
                    //IniciarTareaAlAbrir(TaskResult);
                    WFTB.RegisterTaskAsOpen(TaskResult.TaskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                    if (CheckUserActionLoad())
                    {
                        LoadUserAction();
                    }
                    else
                    {
                        HideFormRules();
                    }
                }
                catch (Exception ex)
                {
                    Zamba.Core.ZClass.raiseerror(ex);
                }
            }
        }

        private void FillHeader()
        {
            UserPreferences UserPreferences = new UserPreferences();
            SUsers SUsers = new SUsers();
            SSteps SSteps = new SSteps();
            //if (TaskResult.AsignedToId > 0)
            //    lblAsignedTo.InnerHtml = SUsers.GetUserorGroupNamebyId(TaskResult.AsignedToId);
            //else
            //    lblAsignedTo.InnerHtml = "Ninguno";

            //lbletapadata.InnerHtml = SSteps.GetStepNameById(TaskResult.StepId);
            //dtpFecVenc.Text = TaskResult.ExpireDate.ToString("dd/MM/yyyy");

            //CboStates.Items.Clear();
            //CboStates.DataTextField = "Name";
            //CboStates.DataValueField = "ID";
            //CboStates.SelectedValue = null;
            //WFStepBusiness WFSB = new WFStepBusiness();
            ////23/09/11: Se cambia la forma en que accede a la lista de estados por etapas.
            //CboStates.DataSource = WFSB.GetStepById(TaskResult.StepId).States;
            //WFSB = null;
            //CboStates.DataBind();
            //CboStates.SelectedValue = TaskResult.State.ID.ToString();
            //CboStates.SelectedIndexChanged += CboStates_SelectedIndexChanged;
        }

        private void HideFormRules()
        {
            //     string script = "$(function() { $('input[id^=zamba_rule_]').hide(); });";
            // Page.ClientScript.RegisterStartupScript(Page.GetType(), "HideFormRules", script, true);
        }

        private bool CheckUserActionLoad()
        {
            List<long> users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId);
            return CheckUserActionLoad(users);
        }

        private bool CheckUserActionLoad(List<long> users)
        {
            //if (!dontLoadUAC && TaskResult != null && TaskResult.TaskState == TaskStates.Ejecucion && (TaskResult.m_AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || TaskResult.m_AsignedToId == 0 || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID) || GetExecuteAssignedToOtherRight()))
            if (TaskResult != null && TaskResult.TaskState == TaskStates.Ejecucion && (TaskResult.m_AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || TaskResult.m_AsignedToId == 0 || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID) || GetExecuteAssignedToOtherRight(RightsType.allowExecuteTasksAssignedToOtherUsers)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool GetExecuteAssignedToOtherRight(RightsType right)
        {
            //RightsType.allowExecuteTasksAssignedToOtherUsers
            return RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, right, Convert.ToInt32(TaskResult.StepId));
        }

        private void ExecuteAsignedToRules()
        {
            try
            {
                List<ITaskResult> Results = new List<ITaskResult>();
                Results.Add(TaskResult);
                var rules = TaskResult.WfStep.Rules;

                loadWfStepRules(TaskResult.StepId, ref rules);

                WFRulesBusiness WFRB = new WFRulesBusiness();
                foreach (WFRuleParent Rule in TaskResult.WfStep.Rules)
                {
                    if (Rule.RuleType == TypesofRules.Asignar)
                    {
                        WFRB.ExecuteRule(Rule, Results, true);
                    }
                }
                WFRB = null;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }

        private void loadWfStepRules(long stepId, ref List<IWFRuleParent> rules)
        {
            // Si la colección de reglas es Nothing o rules no posee elementos
            if (((rules == null) || (rules.Count == 0)))
            {
                WFRulesBusiness WFRulesBusiness = new WFRulesBusiness();
                rules = WFRulesBusiness.GetCompleteHashTableRulesByStep(stepId);
                WFRulesBusiness = null;
            }
        }

        private void UpdateGUITaskAsignedSituation(ITaskResult taskResult)
        {
            //throw new NotImplementedException();
        }

        private void GenerateUserActions()
        {
            SetAsignedTo();
            GetStatesOfTheButtonsRule();
        }

        private string SetStepName(Int64 id)
        {
            string stepName = string.Empty;
            try
            {
                stepName = WFStepBusiness.GetStepNameById(id);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                stepName = string.Empty;
            }
            return stepName;
        }

        private void ExecuteFinishRules()
        {
            try
            {
                System.Collections.Generic.List<Zamba.Core.ITaskResult> Results = new System.Collections.Generic.List<Zamba.Core.ITaskResult>();
                Results.Add(TaskResult);

                foreach (Zamba.Core.WFRuleParent Rule in TaskResult.WfStep.Rules)
                {
                    if (Rule.RuleType == TypesofRules.Terminar)
                    {
                        SRules SRules = new SRules();
                        SRules.ExecuteRule(Rule, Results, true);
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        public void SetAsignedTo()
        {
            try
            {
                ////UpdateGUITaskAsignedSituation(iTaskResult);
                EnablePropietaryControls();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }


        //public DataTable GetTasksByStepandDocTypeId(long stepId, long DocTypeId, Boolean WithGridRights, List<IFilterElem> filterElements, Int64 LastDocId, Int32 PageSize, ref long totalCount)
        //{
        //    IFiltersComponent fc = new FiltersComponent();
        //    fc.AddFiltersElements(filterElements);
        //    long userId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
        //    return WFTaskBusiness.GetTasksByStepandDocTypeId(stepId, DocTypeId, WithGridRights, userId, ref fc, LastDocId, PageSize, ref totalCount, false);
        //}
        #endregion

        [AcceptVerbs("GET", "POST")]
        [Route("LoadTree")]
        [OverrideAuthorization]
        public IHttpActionResult LoadTree()
        {
            try
            {
                List<EntityView> WorkFlows;
                var user = GetUser(null);
                if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError("Usuario invalido o sesion expirada")));
                SWorkflow SWorkflow = new SWorkflow();
                var treeVM = new List<LoadTreeVM>();
                WorkFlows = SWorkflow.GetUserWFIdsAndNamesWithSteps(user.ID);
                foreach (EntityView wf in WorkFlows)
                {
                    var steps = new List<StepVM>();
                    foreach (EntityView ce in wf.ChildsEntities)
                    {
                        var tc = new WFTaskBusiness().GetTaskCount(ce.ID, true, user.ID);
                        steps.Add(new StepVM(ce.Name, (int)tc));
                    }
                    treeVM.Add(new LoadTreeVM(wf.Name, steps));
                }
                var js = JsonConvert.SerializeObject(treeVM);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("LoadTasks")]
        [OverrideAuthorization]

        public IHttpActionResult LoadTasks(LoadTasksParamVM param)
        {
            var user = GetUser(null);
            if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

            if (param.StepId == 0 || param.DocTypeIds.Count == 0) return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new
     HttpError(StringHelper.TaskIdOrDocTypeIdExpected)));

            var sTasks = new STasks();
            long total = 0;
            var f = new FiltersComponent();
            var iFE = new List<IFilterElem>();
            iFE.AddRange(param.FiltersElem.ToList());
            f.AddFiltersElements(iFE);

            DataTable dt = sTasks.getTasksAsDT(param.StepId, new ArrayList(param.DocTypeIds), param.LastPage, param.PageSize, f, ref total);

            var vm = new LoadTasksVM(dt);
            var js = JsonConvert.SerializeObject(vm);
            return Ok(js);
        }

        /// <summary>
        /// Tomado y refactorizado de TaskViewer.aspx.cs, llamado por TaskSelector.aspx
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [Route("GetTask")]
        //[OverrideAuthorization]

        public IHttpActionResult GetTask(int taskId)
        {
            var user = GetUser(null);
            if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
            if (taskId == 0) return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.TaskIdExpected)));

            try
            {
                STasks sTasks = new STasks();
                SRights sRights = new SRights();
                ITaskResult TaskResult;
                TaskResult = sTasks.GetTask(taskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                TaskResult.UserRules = GetDoEnableRules();

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Error = (serializer, err) => { err.ErrorContext.Handled = true; };
                var js = JsonConvert.SerializeObject((TaskResult)TaskResult, settings);

                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }



        /// <summary>
        /// Tomado y refactorizado de DocViewer.aspx.cs, llamado por TaskSelector.aspx
        /// </summary>
        /// <param name="docId"></param>
        /// <param name="doctypeId"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [Route("GetResult")]
        [OverrideAuthorization]
        public IHttpActionResult GetResult(long docId, long docTypeId)
        {
            if (docId == 0 || docTypeId == 0) return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se ingreso docId-docTypeId")));

            try
            {
                SResult SResult = new SResult();
                var result = SResult.GetResult(docId, docTypeId, true);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Error = (serializer, err) => { err.ErrorContext.Handled = true; };
                var js = JsonConvert.SerializeObject(result, settings);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [HttpPost]
        [Route("GetNewsSummary")]
        public IHttpActionResult GetNewsSummary(NewsPostDto newsDto)
        {
            try
            {
                if (newsDto == null)
                    return BadRequest("Objeto request nulo");

                if (newsDto.UserId <= 0)
                    return BadRequest("Id de usuario debe ser mayor a cero");

                var searchType = (NewsSearchType)Enum.Parse(typeof(NewsSearchType), newsDto.SearchType.ToUpper());

                List<News> newsList = new NewsBusiness().GetNewsSummary(newsDto.UserId, searchType);
                return Ok(newsList);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }

        public class NewsPostDto
        {
            public long UserId { get; set; }
            public string SearchType { get; set; }
        }

        [HttpPost]
        [Route("SetNewsRead")]
        public IHttpActionResult SetNewsRead(SetNewsReadPostDto setReadDto)
        {
            try
            {
                if (setReadDto == null)
                    return BadRequest("Objeto request nulo");

                if (setReadDto.DocId <= 0)
                    return BadRequest("Id de documento debe ser mayor a cero");

                if (setReadDto.DocTypeId <= 0)
                    return BadRequest("Id de tipo de documento debe ser mayor a cero");

                new NewsBusiness().SetRead(setReadDto.DocTypeId, setReadDto.DocId);

                return Ok();
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }

        public class SetNewsReadPostDto
        {
            public long DocId { get; set; }
            public long DocTypeId { get; set; }
        }


        [HttpPost]
        [Route("GetTaskId")]
        public IHttpActionResult GetTaskIdByDocAndDoctypeId(GetTaskIdPostDto getTaskDto)
        {
            try
            {
                if (getTaskDto == null)
                    return BadRequest("Objeto request nulo");

                if (getTaskDto.DocId <= 0)
                    return BadRequest("Id de documento debe ser mayor a cero");

                if (getTaskDto.DocTypeId <= 0)
                    return BadRequest("Id de tipo de documento debe ser mayor a cero");

                var task = new WFTaskBusiness().GetTaskByDocIdAndDocTypeId(getTaskDto.DocId, getTaskDto.DocTypeId);

                if (task != null)
                    return Ok(task.ID);

                return Ok(0);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener Id de tarea"));
            }
        }

        public class GetTaskIdPostDto
        {
            public long DocId { get; set; }
            public long DocTypeId { get; set; }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("GetNewsResults")]
        [OverrideAuthorization]
        public List<ListNews> GetNewsResults(genericRequest paramRequest)
        {
            UserBusiness UB = new UserBusiness();
            string docid = paramRequest.Params["docid"];
            List<ListNews> NovedadesList = new List<ListNews>();
            try
            {

                StringBuilder QueryString = new StringBuilder();
                QueryString.Append("SELECT u.name Usuario, n.crdate Fecha, n.c_value Novedad FROM ZNEWS n inner join usrtable u on u.id= n.userid where docid =" + docid);

                string query = string.Format(QueryString.ToString());
                var dataSet = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, query);
                DataTable firstTable = dataSet.Tables[0];

                foreach (DataRow row in firstTable.Rows)
                {
                    NovedadesList.Add(new ListNews(row["USUARIO"].ToString(), row["FECHA"].ToString(), row["NOVEDAD"].ToString()));
                }
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            finally { UB = null; }
            return NovedadesList;
        }

        public string validateParamRequest(string paramData)
        {
            string data = null;
            if (paramData == null)
            {
                data = string.Empty;
            }
            else
            {
                data = paramData;
            }
            return data;
        }

        public class Group
        {
            public long ID { get; set; }
            public string Name { get; set; }
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetUsers")]
        //[OverrideAuthorization]
        public List<BaseImageFileResult> GetUsers(long stepId)
        {

            try
            {
                List<BaseImageFileResult> usrList = WFBusiness.GeUsersOnlUsersByUserIdAndRightsType(stepId);
                return usrList;
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return null;
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetGroups")]
        [OverrideAuthorization]
        public List<BaseImageFileResult> GetGroups(long stepId)
        {
            try
            {
                List<BaseImageFileResult> Groups = WFBusiness.GetGroupsUserGroupsIdsByStepID(stepId);
                return Groups;
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return null;
            }

        }

        //[AcceptVerbs("GET", "POST")]
        //[AllowAnonymous]
        //[Route("GetUsersAndGroups")]

        //public List<UsersAndGroups> GetUsersAndGroups(long WFstepId)
        //{

        //    List<UsersAndGroups> usersgroupsList = new List<UsersAndGroups>();
        //    try
        //    {
        //        var groupsids = WFStepBusiness.GetStepUserGroupsIdsAndNames(ref WFstepId);

        //        for (var i = 0; i < groupsids.Count; i++)
        //        {
        //            var User = UserGroupBusiness.GetUsersByGroup(groupsids[i].ID);
        //            UsersAndGroups U = new UsersAndGroups();
        //            U.groupname = groupsids[i].Name;
        //            U.users = User;

        //            usersgroupsList.Add(U);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    return usersgroupsList;
        //}

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("DeriveTask")]
        [OverrideAuthorization]

        public IHttpActionResult DeriveTask(long taskId, long userIDToAsign, long currentUserID, bool isUser, string url, string comments)
        {
            return DeriveTaskToUser(taskId, userIDToAsign, currentUserID, comments);

        }



        private IHttpActionResult DeriveTaskToUser(long taskId, long userIDToAsign, long currentUserID, string comments)
        {
            try
            {
                EmailData emailData = new EmailData();
                WFTaskBusiness WFTB = new WFTaskBusiness();
                TaskResult task = (TaskResult)WFTB.GetTask(taskId, currentUserID);
                Boolean IsGroup = false;
                string userOrGroupName = UserGroups.GetUserorGroupNamebyId(userIDToAsign, ref IsGroup);
                string username = UserGroups.GetUserorGroupNamebyId(currentUserID, ref IsGroup);
                WFTB.Derivar(task, task.StepId, userIDToAsign, userOrGroupName, currentUserID, DateTime.Now, true, currentUserID);
                string usermail = UserGroups.GetUserMail(userIDToAsign);

                AllObjects.Tarea = task;


                string subject = ZOptBusiness.GetValueOrDefault("DerivateMailSubject", "<<FuncionesComunes>>.<<UsuarioActualNombreYApellido>> te ha derivado la siguiente tarea:   <<Tarea>>.<<Nombre>>   ");
                subject = TextoInteligente.GetValueFromZvarOrSmartText(subject, task);

                string messageBody = ZOptBusiness.GetValueOrDefault("DerivateMailBody", "<<FuncionesComunes>>.<<UsuarioActualNombreYApellido>> te ha derivado la siguiente tarea: <br/> <br/> <b>   <<Tarea>>.<<Nombre>>   </b>");
                comments = Results_Factory.EncodeHTML(comments);
                messageBody = TextoInteligente.GetValueFromZvarOrSmartText(messageBody, task);
                messageBody += "<br>";
                messageBody += "<br>";
                messageBody += comments;
                messageBody += "<br>";
                messageBody += "<br>";


                Int64 docId = task.ID;
                Int64 doctypeId = task.DocTypeId;

                var IdInfo = new EmailData.IdInfo();
                IdInfo.DocId = docId;
                IdInfo.DocTypeid = doctypeId;

                var attachsIds = new List<EmailData.IdInfo>();
                attachsIds.Add(IdInfo);
                emailData.Idinfo = attachsIds;

                setMailContent(emailData, usermail, subject, messageBody);
                emailData.AddLink = true;
                EmailController emailController = new EmailController();
                emailController.SendEmail(emailData);

                WFTaskBusiness.setCExclusive(task.TaskId, 0);

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("DeriveTasks")]
        [OverrideAuthorization]

        public IHttpActionResult DeriveTasks(string docIds, long userIDToAsign, long currentUserID, bool isUser, string url, string comments)
        {


            try
            {
                WFTaskBusiness wFTaskBusiness = new WFTaskBusiness();
                foreach (string id in docIds.Replace("[", "").Replace("]", "").Split(char.Parse(",")))
                {
                    Int64 TaskId = wFTaskBusiness.GetTaskByDocId(Int64.Parse(id), currentUserID).TaskId;
                    DeriveTaskToUser(TaskId, userIDToAsign, currentUserID, comments);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }


        public void setMailContent(EmailData emailData, string usermail, string subject, string messageBody)
        {
            emailData.MailTo = usermail;
            emailData.Subject = subject;
            emailData.MessageBody = messageBody;
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("EndTask")]
        [OverrideAuthorization]
        public void EndTask(bool isTakeTask, long taskId, long userId)
        {
            long userID = GetUser(userId).ID;

            SRights sRights = new SRights();
            STasks sTasks = new STasks();
            WFTaskBusiness WFTB = new WFTaskBusiness();
            RightsBusiness RB = new RightsBusiness();
            TaskResult = sTasks.GetTask(taskId, userId);
            //sTasks = null;

            try
            {
                if (TaskResult != null)
                {
                    WFTB.UnLockTask(TaskResult.TaskId);
                }


                if (TaskResult.TaskState == Zamba.Core.TaskStates.Asignada && TaskResult.AsignedToId != Zamba.Membership.MembershipHelper.CurrentUser.ID)
                {


                }

                else
                {

                    if (isTakeTask == false)
                    {
                        TaskResult.TaskState = Zamba.Core.TaskStates.Asignada;
                        TaskResult.AsignedToId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    }
                    else if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(TaskResult.StepId)))
                    {
                        TaskResult.TaskState = Zamba.Core.TaskStates.Desasignada;
                        TaskResult.AsignedToId = 0;
                    }
                    else
                    {
                        TaskResult.TaskState = Zamba.Core.TaskStates.Asignada;
                        TaskResult.AsignedToId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    }
                }


                sTasks.Finalizar(TaskResult);
                StateWasChanged = true;
                ExecuteFinishRules();
                //Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByTaskId(TaskResult.TaskId)
                SetAsignedTo();
                LoadUserAction();
                GetStatesOfTheButtonsRule();
                EnablePropietaryControls();
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
            finally
            {
                sRights = null;
                sTasks = null;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetUsersRights")]
        [OverrideAuthorization]
        public bool GetUsersRights(long stepId, RightsType right, long userid)
        {
            //long userID = Zamba.Membership.MembershipHelper.CurrentUser.ID;
            long userID = GetUser(userid).ID;
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("Obteniendo permisos del usuario {0} para stepId {1}", userID, stepId));

                return RB.GetUserRights(userID, ObjectTypes.WFSteps, right, stepId);

            }
            catch (Exception e)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, string.Format("Error al obtener los permisos del usuario {0} para stepId {1}", userID, stepId));
                ZClass.raiseerror(e);
                return false;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("StartTask")]
        [OverrideAuthorization]
        public ITaskResult StartTask(long taskId, long userid)
        {
            long userID = GetUser(userid).ID;

            SUsers SUsers = new SUsers();
            SRights SRights = new SRights();
            STasks Stasks = new STasks();
            SRules Srules = new SRules();
            RightsBusiness RB = new RightsBusiness();
            TaskResult = Stasks.GetTask(taskId, userid);
            iTaskResult = TaskResult;
            //Stasks = null;

            try
            {
                List<long> users = SUsers.GetUsersIds(TaskResult.AsignedToId);

                //Si la tarea no esta asignada, esta asignada al usuario o asignada a algun grupo del usuario o tengo el permiso de desasignar

                if (TaskResult.AsignedToId == 0 || TaskResult.AsignedToId == userID || users.Contains(userID) || (TaskResult.TaskState == TaskStates.Asignada && RB.GetUserRights(userID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)) == true))
                {
                    if (TaskResult.AsignedToId == 0)
                    {
                        Stasks.AsignTask(ref iTaskResult, userID, userID, false);
                        ExecuteAsignedToRules();
                    }
                    else if (users.Contains(userID))
                    {
                        DataTable dt = WFStepBusiness.getTypesOfPermit(TaskResult.StepId, TypesofPermits.DontAsignTaskAsignedToGroup);

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][2] == null || dt.Rows[0][2].ToString() == "0")
                            {
                                Stasks.AsignTask(ref iTaskResult, userID, userID, false);
                                ExecuteAsignedToRules();
                            }
                        }
                        else
                        {
                            Stasks.AsignTask(ref iTaskResult, userID, userID, false);
                            ExecuteAsignedToRules();
                        }

                    }
                    else if (TaskResult.m_AsignedToId == userID)
                    {
                    }

                    if ((TaskResult.TaskState == TaskStates.Asignada && RB.GetUserRights(userID, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)) == true) && TaskResult.AsignedToId != userID)
                    {
                        Stasks.AsignTask(ref iTaskResult, userID, userID, true);
                        ExecuteAsignedToRules();
                    }

                    //La coleccion de tareas se pasa por referencia
                    TaskResult.TaskState = TaskStates.Ejecucion;
                    Stasks.InitialiceTask(ref iTaskResult);
                    //WFTaskBusiness.setCExclusiveToZero(docId);

                    List<ITaskResult> Results = new List<ITaskResult>();
                    Results.Add(TaskResult);

                    WFRulesBusiness WFRulesBusiness = new WFRulesBusiness();
                    WFRulesBusiness.ExecuteStartRules(ref Results);

                    GenerateUserActions();
                }
                else
                {
                    UpdateGUITaskAsignedSituation(TaskResult);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return TaskResult;
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("LoadUserAction")]
        [OverrideAuthorization]
        private void LoadUserAction()
        {
            try
            {
                SRules SRules = new SRules();
                string userActionName = string.Empty;
                //idRules.Clear();
                //UACCell.Controls.Clear();

                //todo wf falta ver si no se modificaron las reglas y cargarlas de nuevo desde la base en el wfstep

                List<IWFRuleParent> Rules = SRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

                if ((Rules != null))
                {
                    WFRulesBusiness WFRB = new WFRulesBusiness();
                    bool RuleEnabled = false;

                    foreach (Zamba.Core.WFRuleParent Rule in Rules)
                    {
                        if (TaskResult.UserRules.ContainsKey(Rule.ID))
                        {
                            //Lista que en la posicion 0 guarda si esta habilitada la regla o no
                            //y en la 1 si se acumula a la habilitacion de las solapas o no
                            List<bool> lstRulesEnabled = (List<bool>)TaskResult.UserRules[Rule.ID];
                            RuleEnabled = lstRulesEnabled[0];
                        }
                        else
                        {
                            RuleEnabled = true;
                        }

                        if (RuleEnabled && Rule.ParentType == TypesofRules.AccionUsuario && !idRules.Contains(Rule.ID))
                        {
                            //Button UAB = new Button();
                            //HtmlGenericControl li = new HtmlGenericControl("li");
                            //li.Attributes.Add("style", "padding:1px");

                            //UAB.ID = "UAB_Rule_" + Rule.ID;
                            //UAB.CssClass = "btn btn-primary btn-xs";
                            //UAB.OnClientClick = "ShowLoadingAnimation();";
                            //UAB.TabIndex = -1;

                            //UAB.Click += UAB_Click;

                            //Busca en la tabla si existe un nombre de acción de usuario para esa regla
                            try
                            {
                                userActionName = SRules.GetUserActionName(Rule);
                            }
                            catch (Exception ex)
                            {
                                Zamba.Core.ZClass.raiseerror(ex);
                                userActionName = string.Empty;
                            }

                            //Si el nombre no existe entonces le asigna el nombre de la regla
                            if (string.IsNullOrEmpty(userActionName))
                            {
                                userActionName = Rule.Name.ToUpper();
                            }

                            //Asigna el nombre al botón. Si este es mayor que 20 lo corta y le agrega 3 puntos
                            //UAB.ToolTip = userActionName;

                            //if (userActionName.Length > 20)
                            //{
                            //    UAB.Text = userActionName.Substring(0, 20) + "...";
                            //}
                            //else
                            //{
                            //    UAB.Text = userActionName;
                            //}

                            //Guarda el nombre en un hash para luego utilizarlo cuando se llame al saveAction
                            if (hshRulesNames.ContainsKey(Rule.ID))
                            {
                                hshRulesNames[Rule.ID] = userActionName;
                            }
                            else
                            {
                                hshRulesNames.Add(Rule.ID, userActionName);
                            }

                            //li.Controls.Add(UAB);
                            //this.UACCell.Controls.Add(li);

                            //Se guarda el id de la regla
                            idRules.Add(Rule.ID);
                        }
                    }

                    userActionName = string.Empty;
                    WFRB = null;

                    //Oculta/muestra reglas segun preferencias por cada result
                    GetStatesOfTheButtonsRule();

                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetStatesOfTheButtonsRule")]
        [OverrideAuthorization]
        public void GetStatesOfTheButtonsRule()
        {
            try
            {
                SRules SRules = new SRules();
                SSteps SSteps = new SSteps();
                SUsers SUsers = new SUsers();
                SWorkflow SWorkflow = new SWorkflow();
                Int32 contador = 0;
                //Dice si se va a usar el enable del tab o no
                bool useTabEnable = false;


                //if (UACCell.Controls.Count > 0)
                //{
                //Recorre cada regla activa en el documento
                foreach (Int64 idRule in idRules)
                {
                    useTabEnable = true;

                    //Si la regla no fue procesada antes por la DoEnable
                    if (TaskResult.UserRules.Contains(idRule))
                    {
                        //Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        //y en la 1 si se acumula a la habilitacion de las solapas o no
                        List<bool> lstRulesEnabled = (List<bool>)TaskResult.UserRules[idRule];
                        //Si la regla esta deshabilitada, no uso los estados de los tabs
                        if (lstRulesEnabled[0])
                        {
                            //Si no esta marcada la opcion de ejecucion conjunta con los tabs, no uso los estados
                            if (lstRulesEnabled[1] == false)
                            {
                                useTabEnable = false;
                            }
                        }
                        else
                        {
                            useTabEnable = false;
                        }
                    }

                    //Si utilizo los tabs (porq no uso la doenable o porq la ejecucion es conjunta)
                    if (useTabEnable)
                    {
                        WFBusiness WFB = new WFBusiness();
                        //UACCell.Controls[contador].Visible = WFB.CanExecuteRules(idRule, Zamba.Membership.MembershipHelper.CurrentUser.ID, (WFStepState)TaskResult.State, (TaskResult)TaskResult);
                    }
                    contador = contador + 1;
                }
                //}

            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("EnablePropietaryControls")]
        [OverrideAuthorization]
        public void EnablePropietaryControls()
        {

            SRights SRights = new SRights();
            UserPreferences UserPreferences = new UserPreferences();
            List<long> users = default(List<long>);
            UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();
            try
            {
                //Si el id es un grupo, users tendra los usuarios del mismo, caso contrario se encontrara vacio
                users = UserGroupBusiness.GetUsersIds(TaskResult.AsignedToId);


                if (users.Count <= 0)
                {
                    List<long> AsignedUser = new List<long>();
                    AsignedUser.Add(TaskResult.AsignedToId);
                    users = AsignedUser;

                }
                //Estado de tarea
                if ((TaskResult.TaskState == TaskStates.Desasignada && TaskResult.AsignedToId != 0))
                {
                    TaskResult.TaskState = TaskStates.Asignada;
                }
                if (TaskResult.AsignedToId == 0)
                {
                    TaskResult.TaskState = TaskStates.Desasignada;
                }

                //Iniciar
                //BtnIniciar.Visible = GetBtnIniciarVisibility(SRights, users);
                //BtnIniciar.Visible = true;

                ////Acciones de usuario
                if (CheckUserActionLoad(users))
                {
                    LoadUserAction();
                }
                else
                {
                    HideFormRules();
                }

                //Habilitacion de opciones y combo de estados de etapa
                //    if ((TaskResult.TaskState == Zamba.Core.TaskStates.Ejecucion))
                //    {
                //        if ((RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.AllowStateComboBox, TaskResult.StepId) == true))
                //        {
                //            CboStates.Enabled = true;
                //        }
                //        else
                //        {
                //            CboStates.Enabled = false;
                //        }

                //        if (TaskResult.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID)
                //        {
                //            this.chkTakeTask.Enabled = true;
                //        }
                //        else
                //        {
                //            this.chkTakeTask.Enabled = false;
                //        }
                //        this.chkCloseTaskAfterDistribute.Enabled = true;
                //    }
                //    else
                //    {
                //        this.CboStates.Enabled = false;
                //        this.dtpFecVenc.Enabled = false;
                //        this.chkTakeTask.Enabled = false;
                //        this.chkCloseTaskAfterDistribute.Enabled = false;
                //    }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            finally
            {
                SRights = null;
                UserPreferences = null;
                users = null;
            }
        }



        private void IniciarTareaAlAbrir(ITaskResult task, long userid)
        {
            STasks sTasks = new STasks();
            long userId = GetUser(userid).ID;

            if (RB.GetUserRights(userId, ObjectTypes.WFSteps, RightsType.Iniciar, Convert.ToInt32(TaskResult.StepId)))
            {
                if (task.TaskState == TaskStates.Servicio)
                {
                    //DisableGUI("Tarea en ejecución por un servicio");
                }
                else
                {
                    Zamba.Core.IWFStep wfstep = default(Zamba.Core.IWFStep);

                    try
                    {
                        string currentLockedUser = null;

                        if (WFTaskBusiness.LockTask(TaskResult.TaskId, ref currentLockedUser))
                        {
                            SSteps WFSTEPSER = new SSteps();
                            wfstep = WFSTEPSER.GetStep(task.StepId);

                            if (wfstep.StartAtOpenDoc)
                            {
                                List<long> users = UserGroupBusiness.GetUsersIds(task.m_AsignedToId);
                                if ((task.m_AsignedToId == userId || users.Contains(userId)))
                                {
                                    //Asignada a mi o a un grupo al que pertenezco
                                    //StartTask(sTasks.GetDocId(task.TaskId));
                                }
                                else if (task.m_AsignedToId != 0)
                                {
                                    switch (task.TaskState)
                                    {
                                        case TaskStates.Asignada:
                                            if (RB.GetUserRights(userId, ObjectTypes.WFSteps, RightsType.UnAssign, Convert.ToInt32(TaskResult.StepId)))
                                            {
                                                GenerateUserActions();
                                            }
                                            break;
                                        case TaskStates.Desasignada:
                                            //Nunca deberia pasar por aca, porque si esta asignada a otro usuario o grupo, deberia estar en asignada o ejecucion
                                            //StartTask(sTasks.GetDocId(task.TaskId));
                                            break;
                                        default:
                                            //DisableGUI("Tarea en ejecución por otro usuario");
                                            break;
                                    }
                                }
                                else if (task.m_AsignedToId == 0)
                                {
                                    //Esta asignada a ninguno
                                    //StartTask(sTasks.GetDocId(task.TaskId));
                                }
                            }
                            else
                            {
                                System.Collections.Generic.List<long> users = UserGroupBusiness.GetUsersIds(task.m_AsignedToId);
                                if (task.m_AsignedToId != 0 && task.m_AsignedToId != userId && users.Contains(userId) == false)
                                {
                                    if (task.TaskState == TaskStates.Ejecucion || task.TaskState == TaskStates.Servicio)
                                    {
                                        //BtnIniciar.Visible = false;
                                    }
                                    else
                                    {
                                        SetAsignedTo();
                                    }
                                }
                                else
                                {
                                    if (task.TaskState == TaskStates.Servicio)
                                    {
                                        //BtnIniciar.Visible = false;
                                    }
                                    else
                                    {
                                        SetAsignedTo();
                                    }
                                }
                            }
                        }
                        else
                        {
                            //DisableGUI("Tarea en Ejecucion por: " + currentLockedUser);
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                }
            }
            else
            {
                //DisableGUI("No tiene permisos suficientes para iniciar la tarea");
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("GetGroupsByUserIds")]
        [OverrideAuthorization]
        public List<long> GetGroupsByUserIds(long usrID)
        {
            try
            {
                IUser usr = GetUser(usrID);
                List<long> groups = new List<long>();
                foreach (IUserGroup gr in usr.Groups)
                    groups.Add(gr.ID);

                return groups;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return new List<long>();
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("OnLoadPage")]
        [OverrideAuthorization]
        public ITaskResult OnLoadPage(long taskId, long userid)
        {
            //Para validar el usuario, y setearlo para el Web Api en caso de ser necesario.
            userid = GetUser(userid).ID;
            STasks sTasks = new STasks();
            //var task = sTasks.GetTaskByDocId(docId, userid);
            //if (StateWasChanged == true)
            //{
            //    WFTasksFactory wft = new WFTasksFactory();
            //    wft.UpdateState1(task.ID, 0);
            //}

            TaskResult = sTasks.GetTask(taskId, userid);
            return TaskResult;
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getStepNameById")]
        [OverrideAuthorization]
        public string getStepNameById(Int64 stepid)
        {
            SSteps SSteps = new SSteps();
            return SSteps.GetStepNameById(stepid);
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetUserorGroupNamebyId")]
        [OverrideAuthorization]
        public string GetUserorGroupNamebyId(Int64 asginedToId)
        {
            SUsers SUsers = new SUsers();
            Boolean IsGroup = false;

            return SUsers.GetUserorGroupNamebyId(asginedToId, ref IsGroup);
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetAsociatedIndexsDropzon")]
        [OverrideAuthorization]
        public string GetAsociatedIndexsDropzon(Int64 indexId, Int64 entityId)
        {

            searchList searchlist = new searchList();
            searchlist.IndexId = indexId;
            searchlist.LimitTo = 50;
            searchlist.Value = "";
            try
            {
                List<IIndexList> List = new List<IIndexList>();
                if (searchlist.IndexId > 0)
                {
                    if (searchlist.Value == null) searchlist.Value = string.Empty;
                    IndexsBusiness IB = new IndexsBusiness();
                    IIndex index = IB.GetIndex(searchlist.IndexId);
                    List = IB.GetIndexList(searchlist.IndexId, index.DropDown, searchlist.Value, searchlist.LimitTo + 1);
                }
                var newresults = JsonConvert.SerializeObject(List, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener la lista: " + ex.ToString());
            }
            finally
            {
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GeneratePdfCoverPage")]
        [OverrideAuthorization]
        public string GeneratePdfCoverPage(string TempPath, short CopiesCount, float width, float height)
        {
            string _Pdfile = string.Empty;
            try
            {
                // Recupero el htmlString 
                string htmlString = File.ReadAllText(TempPath);
                // La ruta para guardarlo como PDF
                _Pdfile = FileBusiness.GetUniqueFileName(Path.Combine(Path.GetDirectoryName(TempPath), Path.GetFileNameWithoutExtension(TempPath) + ".pdf"));
                SpireTools ST = new SpireTools();
                ST.PrintHtmlDocByHtmlString(htmlString, CopiesCount, _Pdfile, width, height);
                _Pdfile = _Pdfile.Substring(_Pdfile.IndexOf("Log"));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }

            return _Pdfile;
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetResultDocumentContent")]
        [OverrideAuthorization]
        public string GetResultDocumentContent(long docid, long doctypeid)
        {
            string docContent = string.Empty;
            try
            {
                // Con el docid y el doctypeid obtengo la tarea y su documento.
                //WFTaskBusiness WFTB = new WFTaskBusiness();
                //ITaskResult result = WFTB.GetTaskByDocIdAndDocTypeId(docid, doctypeid);
                Results_Business RB = new Results_Business();
                IResult res = RB.GetResult(docid, doctypeid, false);
                if (File.Exists(res.FullPath))
                {
                    docContent = File.ReadAllText(res.FullPath);
                }
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }

            return docContent;
        }

        public class HtmlContent
        {
            public long docid { get; set; }
            public long doctypeid { get; set; }
            public string content { get; set; }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SaveResultDocContent")]
        [OverrideAuthorization]
        public void SaveResultDocContent(HtmlContent obj)
        {
            try
            {
                // Con el docid y el doctypeid obtengo la tarea y su documento para pisarlo.
                //WFTaskBusiness WFTB = new WFTaskBusiness();
                //ITaskResult result = WFTB.GetTaskByDocIdAndDocTypeId(obj.docid, obj.doctypeid);
                Results_Business RB = new Results_Business();
                IResult res = RB.GetResult(obj.docid, obj.doctypeid, false);
                if (File.Exists(res.FullPath))
                {
                    File.WriteAllText(res.FullPath, obj.content);
                }
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GeneratePdf")]
        [OverrideAuthorization]
        public List<string> GeneratePdf(genericRequest paramRequest)
        {
            try
            {

                //string TempPath = "C:/Users/Stardoc/Desktop/carpeta1/pepito.pdf";
                List<string> TempList = new List<string>();
                string item = string.Empty;
                string PathToSave = paramRequest.Params["PathToSave"];

                foreach (KeyValuePair<string, string> entry in paramRequest.Params)
                {
                    if (entry.Key != "PathToSave")
                    {
                        item = entry.Value;
                        TempList.Add(item);
                    }

                }

                string[] list = TempList.ToArray();
                SpireTools ST = new SpireTools();
                ST.CreatePdfFromAnotherPdf(list, PathToSave);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            return new List<string>();

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("MarkAsFavorite")]
        [OverrideAuthorization]
        public IHttpActionResult MarkAsFavorite(genericRequest paramRequest)
        {

            var user = GetUser(paramRequest.UserId);
            try
            {
                if (paramRequest.Params.ContainsKey("TaskId") && paramRequest.Params["TaskId"] != null && user != null)
                {
                    Int64 TaskId = Int64.Parse(paramRequest.Params["TaskId"]);
                    Boolean Mark = Boolean.Parse(paramRequest.Params["Mark"]);

                    DocumentLabelsBusiness DLB = new DocumentLabelsBusiness();
                    WFTaskBusiness WFTB = new WFTaskBusiness();
                    ITaskResult task = WFTB.GetTask(TaskId, paramRequest.UserId);
                    if (task != null)
                    {
                        task.IsFavorite = !Mark;
                        DLB.UpdateFavoriteLabel(task);
                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
            }
            return Ok();

        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetRuleNames")]
        [OverrideAuthorization]
        public IHttpActionResult GetRuleNames(genericRequest paramRequest)
        {
            List<string> ruleIds = new List<string>();
            WFRulesBusiness wFRulesBusiness = new WFRulesBusiness();
            Dictionary<string, string> rules = new Dictionary<string, string>();
            try
            {
                if (paramRequest.Params.ContainsKey("ruleIds") && paramRequest.Params["ruleIds"] != null)
                {
                    ruleIds.AddRange(paramRequest.Params["ruleIds"].ToString().Split(char.Parse(",")));
                    for (int i = 0; i < ruleIds.Count; i++)
                    {
                        var ruleName = wFRulesBusiness.GetRuleNameById(Int32.Parse(ruleIds[i]));
                        rules[ruleIds[i]] = ruleName;
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
            }

            var ruleDictionary = JsonConvert.SerializeObject(rules, Formatting.Indented,
              new JsonSerializerSettings
              {
                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
              });

            return Ok(ruleDictionary);
        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("ExecuteTaskRule")]
        //[OverrideAuthorization]
        public IHttpActionResult ExecuteTaskRule(genericRequest paramRequest)
        {
            cleanRuleVariables_ByConvention();

            try
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("ruleId") && !string.IsNullOrEmpty(paramRequest.Params["ruleId"]))
                {
                    Int64 ruleId = 0;
                    List<string> docIds = new List<string>();
                    STasks sTasks = new STasks();
                    List<Zamba.Core.ITaskResult> Results = new List<Zamba.Core.ITaskResult>();

                    ruleId = Int64.Parse(paramRequest.Params["ruleId"].ToString());
                    if (paramRequest.Params.ContainsKey("resultIds") && !string.IsNullOrEmpty(paramRequest.Params["resultIds"]))
                    {
                        docIds.AddRange( paramRequest.Params["resultIds"].ToString().Split(char.Parse(",")));
                    }

                    string FormVariables = string.Empty;
                    if (paramRequest.Params.ContainsKey("FormVariables") && !string.IsNullOrEmpty(paramRequest.Params["FormVariables"]))
                    {
                        FormVariables = paramRequest.Params["FormVariables"];
                    }

                    if (docIds.Count > 0)
                    {
                        for (int i = 0; i < docIds.Count; i++)
                        {
                            if (int.Parse(docIds[i]) > 0)
                            {
                                Results.Add(sTasks.GetTaskByDocId(Int64.Parse(docIds[i])));
                            }
                            else
                            {

                                ITaskResult ExecutionTask = new TaskResult();
                                ExecutionTask.AsignedToId = user.ID;
                                ExecutionTask.UserId = (int)user.ID;
                                ExecutionTask.TaskId = 0;
                                ExecutionTask.Name = "Ejecucion de regla IMAP"; //
                                                                                //ExecutionTask.StartRule = ruleId;

                                Results.Add(ExecutionTask);
                            }
                        }
                    }
                    else
                    {
                        ITaskResult ExecutionTask = new TaskResult();
                        ExecutionTask.AsignedToId = user.ID;
                        ExecutionTask.UserId = (int)user.ID;
                        ExecutionTask.TaskId = 0;
                        ExecutionTask.Name = "Ejecucion de regla sin tarea"; //
                                                                        //ExecutionTask.StartRule = ruleId;

                        Results.Add(ExecutionTask);
                    }

                    if (FormVariables != string.Empty)
                    {
                        Dictionary<string, string> dicFormVariables = new Dictionary<string, string>();
                        List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                        if (!string.IsNullOrEmpty(FormVariables))
                        {
                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                        }

                        //Se itera el diccionario de los valores
                        foreach (var itemlist in dicFormVariables)
                        {
                            if (!VariablesInterReglas.ContainsKey(itemlist.Key))
                                VariablesInterReglas.Add(itemlist.Key, itemlist.Value);
                            else
                                VariablesInterReglas.set_Item(itemlist.Key, itemlist.Value);
                        }
                    }

                    WFTaskBusiness WFTB = new WFTaskBusiness();
                    GenericExecutionResponse genericExecutionResult = null;
                    foreach (ITaskResult result in Results)
                    {
                        List<ITaskResult> currentResults = new List<ITaskResult>() { result };
                        genericExecutionResult = ExecuteRule(ruleId, currentResults, true);

                    }
                    WFTB = null;

                    string js = null;
                    try
                    {
                        js = JsonConvert.SerializeObject(genericExecutionResult);
                        return Ok(js);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                    return Ok();

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }

        }

        /// <summary>
        /// Elimina las variables interreglas definidas por convencion en el enumerador "EVariablesInterReglas_Convencion".
        /// </summary>
        private void cleanRuleVariables_ByConvention()
        {
            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("Execute_ZambaTaskRule")]
        //[OverrideAuthorization]
        public IHttpActionResult Execute_ZambaTaskRule(genericRequest paramRequest)
        {
            try
            {
                //var user = GetUser();
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("ruleId") && !string.IsNullOrEmpty(paramRequest.Params["ruleId"]) &&
                paramRequest.Params.ContainsKey("resultIds") && !string.IsNullOrEmpty(paramRequest.Params["resultIds"]))
                {
                    Int64 ruleId = 0;
                    List<string> docIds = new List<string>();
                    STasks sTasks = new STasks();
                    List<Zamba.Core.ITaskResult> Results = new List<Zamba.Core.ITaskResult>();

                    ruleId = Int64.Parse(paramRequest.Params["ruleId"].ToString());
                    docIds.AddRange(paramRequest.Params["resultIds"].ToString().Split(char.Parse(",")));

                    for (int i = 0; i < docIds.Count; i++)
                    {
                        Results.Add(sTasks.GetTaskByDocId(Int64.Parse(docIds[i])));
                    }

                    var genericExecutionResult = ExecuteRule(ruleId, Results, true);

                    string js = null;
                    try
                    {
                        //js = JsonConvert.SerializeObject(genericExecutionResult);
                        return Ok(VariablesInterreglesToJson(VariablesInterReglas.Keys()));
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                    return Ok();

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }



        //c#
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetEnabledRuleId")]
        [OverrideAuthorization]
        public IHttpActionResult GetEnabledRuleId(genericRequest paramRequest)
        {
            List<string> ruleIds = new List<string>();
            WFRulesBusiness wFRulesBusiness = new WFRulesBusiness();
            Dictionary<string, string> rulesEnable = new Dictionary<string, string>();
            var user = GetUser(paramRequest.UserId);
            try
            {
                if (paramRequest.Params.ContainsKey("ruleIds") && paramRequest.Params["ruleIds"] != null && user != null)
                {
                    ruleIds.AddRange(paramRequest.Params["ruleIds"].ToString().Split(char.Parse(",")));
                    for (int i = 0; i < ruleIds.Count; i++)
                    {
                        var isRuleEnabled = true; //ruleIds[i]
                        rulesEnable[ruleIds[i]] = isRuleEnabled.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Zamba.Core.ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
            }

            var ruleEnabledDictionary = JsonConvert.SerializeObject(rulesEnable, Formatting.Indented,
              new JsonSerializerSettings
              {
                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
              });

            return Ok(ruleEnabledDictionary);
        }


        public IHttpActionResult PrivEx(genericRequest Obj)
        {
            return ExecuteTaskRule(Obj);
            //return ExecuteRule(int64, new List<ITaskResult>(), false);
        }


        /// <summary>
        /// Ejecuta las reglas desde la master
        /// </summary>
        /// <param name="ruleId">ID de la regla que se quiere ejecutar</param>
        /// <param name="results">Tareas a ejecutar</param>
        /// <remarks></remarks>
        private GenericExecutionResponse ExecuteRule(Int64 ruleId, List<Zamba.Core.ITaskResult> results, Boolean IsAsync)
        {
            Hashtable Params = new Hashtable();
            Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
            Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
            List<Int64> ExecutedIDs = new List<Int64>();
            List<Int64> PendingChildRules = new List<Int64>();
            bool RefreshRule = false;

            try
            {
                //  WFExecution wFExecution = new WFExecution(MembershipHelper.CurrentUser);
                WFTaskBusiness WFTB = new WFTaskBusiness();
                //   wFExecution._haceralgoEvent += HandleWFExecutionPendingEvents;
                foreach (ITaskResult result in results)
                {
                    string RuleName = new WFRulesBusiness().GetRuleNameById(ruleId);
                    string tracemsg = string.Format("El usuario presiono {0} (Regla: {1}) ", RuleName, ruleId.ToString());
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, tracemsg);
                    WFTB.LogTask(result, tracemsg);
                    List<ITaskResult> currentResults = new List<ITaskResult>() { result };
                    wFExecution.ExecuteRule(ruleId, ref currentResults, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, new List<Int64>(), true);
                }
                WFTB = null;

                var GER = new GenericExecutionResponse(ruleId, PendigEvent, ExecutionResult, Params, RefreshRule, PendingChildRules, ref results);
                foreach (var v in VariablesInterReglas.Keys())
                {
                    if (GER.Vars.ContainsKey(v) == false)
                        GER.Vars.Add(v, VariablesInterReglas.get_Item(v));
                    else
                        GER.Vars[v] = VariablesInterReglas.get_Item(v);
                }

                return GER;
            }
            catch (Exception ex)
            {
                if (PendigEvent == RulePendingEvents.ExecuteErrorRule && Params != null && Params.Count > 0)
                {
                    PendingChildRules.Clear();

                    //Ejecuto la nueva regla marcada en la DoExecuteRule
                    Int64 newRuleID = Int64.Parse(Params["RuleId"].ToString());
                    Params.Clear();
                    ExecutedIDs.Clear();
                    wFExecution.ExecuteRule(newRuleID, ref results, ref PendigEvent, ref ExecutionResult,
                        ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, null, false);

                    List<ITaskResult> Errresults = results;
                    var GER = new GenericExecutionResponse(newRuleID, RulePendingEvents.NoPendingEvent, RuleExecutionResult.FailedExecution, null, false, null, ref Errresults);
                    foreach (var v in VariablesInterReglas.Keys())
                    {
                        if (GER.Vars.ContainsKey(v) == false)
                            GER.Vars.Add(v, VariablesInterReglas.get_Item(v));
                        else
                            GER.Vars[v] = VariablesInterReglas.get_Item(v);
                    }
                    return GER;

                }
                else
                {
                    Zamba.AppBlock.ZException.Log(ex);
                    List<ITaskResult> Errresults = results;
                    var GER = new GenericExecutionResponse(ruleId, RulePendingEvents.NoPendingEvent, RuleExecutionResult.FailedExecution, null, false, null, ref Errresults);


                    foreach (var v in VariablesInterReglas.Keys())
                    {
                        if (GER.Vars.ContainsKey(v) == false)
                            GER.Vars.Add(v, VariablesInterReglas.get_Item(v));
                        else
                            GER.Vars[v] = VariablesInterReglas.get_Item(v);
                    }

                    if (GER.Vars.ContainsKey("error") == false)
                        GER.Vars.Add("error", ex.Message);

                    return GER;
                }
            }
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void HandleWFExecutionPendingEvents(long RuleId, ref List<ITaskResult> results,
        ref RulePendingEvents PendigEvent, ref RuleExecutionResult ExecutionResult,
        ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules,
        ref Boolean RefreshRule, List<long> TaskIdsToRefresh, Boolean IsAsync)
        {
            if (ExecutionResult == RuleExecutionResult.FailedExecution)
            {
                if (!VariablesInterReglas.ContainsKey("error"))
                    VariablesInterReglas.Add("error", "Ha ocurrido un error en la regla");
                else
                    VariablesInterReglas.set_Item("error", "Ha ocurrido un error en la regla");

                //                Session["ExecutingRule"] = null;
            }
            else if (ExecutionResult == RuleExecutionResult.CorrectExecution)
            {
                //  ZTrace.WriteLineIf(ZTrace.IsVerbose, "HandleWFExecutionPendingEvents CorrectExecution  Session[ExecutingRule] = null");
                //              Session["ExecutingRule"] = null;
            }
            else
            {
                // ZTrace.WriteLineIf(ZTrace.IsVerbose, "HandleWFExecutionPendingEvents else  LoadUCRule RuleId: " + RuleId.ToString());
                LoadUCRule(true, RuleId, results, PendigEvent, ExecutionResult, ExecutedIDs,
                    Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
            }
        }

        Boolean RegisterStartupScript = false;
        private void LoadUCRule(bool firstload, long RuleId, List<ITaskResult> results,
            RulePendingEvents PendigEvent, RuleExecutionResult ExecutionResult,
            List<Int64> ExecutedIDs, Hashtable Params, List<Int64> PendingChildRules,
            ref Boolean RefreshRule, List<long> TaskIdsToRefresh)
        {
            //Guardo en sesion la regla que se esta ejeucutando
            //Control ucrule = null;
            Int32 ParentAction = 0;

            // ZTrace.WriteLineIf(ZTrace.IsVerbose, "LoadUCRule PendigEvent : " + PendigEvent.ToString());

            switch (PendigEvent)
            {
                case RulePendingEvents.ShowExportPDFControl:
                    if (Params["Content"] != null)
                    {

                        string Content = Params["Content"].ToString();
                        //UTF8Encoding encoder = new UTF8Encoding();
                        //byte[] bytes = encoder.GetBytes(Content);
                        Random ran = new Random((int)DateTime.Now.Ticks);
                        long newRan = ran.Next();
                        //Session[newRan.ToString()] = Content;
                        string DoExportHTMLToPDF = "$(document).ready(function() { parent.OpenWindow('../UC/WF/Rules/DoExportToPDF.aspx?Content=" + newRan.ToString();
                        if (Params["ReturnFileName"] != null)
                        {
                            DoExportHTMLToPDF += "&ReturnFileName=" + Params["ReturnFileName"].ToString();
                        }
                        if (Params["CanEditable"] != null)
                        {
                            DoExportHTMLToPDF += "&CanEditable=" + Params["CanEditable"].ToString();
                        }
                        DoExportHTMLToPDF += "');});";

                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", DoExportHTMLToPDF);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", DoExportHTMLToPDF);

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "DoExportHTMLToPDF", script, true);
                        RegisterStartupScript = true;

                    }
                    break;
                case RulePendingEvents.ShowDoAsociatedForm:
                    //if (Params["FormID"] != null)
                    //{
                    //    if ((bool)Params["HaveSpecificAttributes"])
                    //    {
                    //      //  Session["SpecificAttrubutes" + _taskID.ToString()] = Params["SpecificAttrubutes"];
                    //    }

                    //    string DoAddAsociatedFormScript = "AsociateForm('" + Params["FormID"] + "','" + Params["DocID"] + "','" + Params["DocTypeId"] + "','" +
                    //        _taskID + "','" + Params["ContinueWithCurrentTasks"] + "','" + Params["DontOpenTaskAfterInsert"] +
                    //        "','" + Params["FillCommonAttributes"] + "','" + Params["HaveSpecificAttributes"] + "');";

                    //    if (!VariablesInterReglas.ContainsKey("accion"))
                    //        VariablesInterReglas.Add("accion", "executescript");
                    //    else
                    //        VariablesInterReglas.set_Item("accion", "executescript");

                    //    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                    //        VariablesInterReglas.Add("scripttoexecute", DoAddAsociatedFormScript);
                    //    else
                    //        VariablesInterReglas.set_Item("scripttoexecute", DoAddAsociatedFormScript);

                    //    //Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddAsociatedFormScript", DoAddAsociatedFormScript, true);
                    //    RegisterStartupScript = true;

                    //}
                    break;
                //05/07/11: Sumada la opcion de la regla DoAddAsociatedDocument
                case RulePendingEvents.ShowDoAddAsociatedDoc:
                    //if (Params["DocID"] != null)
                    //{
                    //    StringBuilder url = new StringBuilder();
                    //    url.Append("../../Views/Insert/Insert.aspx?docid=");
                    //    url.Append(Params["DocID"]);
                    //    url.Append("&doctypeid=");
                    //    url.Append(Params["DocTypeId"]);
                    //    //19/07/11: Sumado un parámetro mas para poder obtener los indices del documento que llama la regla
                    //    url.Append("&FillIndxDocTypeID=");
                    //    url.Append(Params["FillIndxDocTypeID"]);
                    //    url.Append("&isview=true");
                    //    url.Append("&CallTaskID=");
                    //    url.Append(_taskID.ToString());
                    //    url.Append("&haveSpecificAtt=");
                    //    url.Append(Params["HaveSpecificAttributes"]);

                    //    if ((bool)Params["HaveSpecificAttributes"])
                    //    {
                    //        Session["SpecificAttrubutes" + _taskID.ToString()] = Params["SpecificAttrubutes"];
                    //    }


                    //    //"instalamos" el javascript cuando refresca la página.
                    //    string script = "$(document).ready(function () { $('#IFDialogContent').unbind('load'); parent.ShowInsertAsociated('" + url.ToString() + "'); });";

                    //    if (!VariablesInterReglas.ContainsKey("accion"))
                    //        VariablesInterReglas.Add("accion", "executescript");
                    //    else
                    //        VariablesInterReglas.set_Item("accion", "executescript");

                    //    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                    //        VariablesInterReglas.Add("scripttoexecute", script);
                    //    else
                    //        VariablesInterReglas.set_Item("scripttoexecute", script);


                    //   // Page.ClientScript.RegisterStartupScript(this.GetType(), "DoAddAsociatedDocumentScript", script, true);
                    //   RegisterStartupScript = true;

                    //}
                    break;
                //04/07/11: Sumada la opcion del la regla DoOpenTask
                case RulePendingEvents.OpenTask:
                    if (Params["DocID"] != null)
                    {
                        Int32 openMode = 0;
                        if (Params.ContainsKey("OpenMode"))
                        {
                            openMode = Int32.Parse(Params["OpenMode"].ToString());
                        }
                        //construimos la url
                        System.Text.StringBuilder url = new System.Text.StringBuilder();
                        url.Append("../WF/TaskSelector.ashx?doctype=");
                        url.Append(Params["DocTypeId"]);
                        url.Append("&docid=");
                        url.Append(Params["DocID"]);
                        url.Append("&userId=");
                        url.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        IResult res = new SResult().GetResult((long)Params["DocID"], (long)Params["DocTypeId"], false);

                        string openTaskscript = string.Format("parent.OpenDocTask3({0},{1},{2},{3},'{4}','{5}',{6},{7},{8});", 0, res.ID, res.DocTypeId, "false", res.Name, url, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0, openMode);
                        openTaskscript += "parent.SelectTabFromMasterPage('tabtasks');";

                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", openTaskscript);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", openTaskscript);


                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "openTask", openTaskscript, true);
                        RegisterStartupScript = true;

                    }
                    break;

                case RulePendingEvents.RuleRefreshTask:
                    //if (Params["TaskIDToRefresh"] != null && (long)Params["TaskIDToRefresh"] > 0)
                    //{
                    //   // Session[TaskID + "CurrentExecution"] = null;

                    //    if (results == null)
                    //    {
                    //        results = new List<ITaskResult>();
                    //        STasks stasks = new STasks();
                    //        results.Add(stasks.GetTask(TaskID, Zamba.Membership.MembershipHelper.CurrentUser.ID));
                    //    }

                    //    if (TaskIdsToRefresh != null)
                    //    {
                    //        TaskIdsToRefresh.Add((long)Params["TaskIDToRefresh"]);
                    //    }

                    //    wfexec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult,
                    //        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    //}
                    break;

                case RulePendingEvents.CloseTask:
                    ParentAction = 0;
                    if (Params.ContainsKey("TaskID"))
                    {
                        STasks st = new STasks();
                        long taskId = Int64.Parse(Params["TaskID"].ToString());
                        ITaskResult rs = null;

                        if (results != null && results.Count > 0 && results[0] != null)
                            if (taskId > 0 && taskId != results[0].TaskId)
                                rs = st.GetTask(taskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                            else
                                rs = results[0];
                        else if (taskId > 0)
                            rs = st.GetTask(taskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);


                        if (rs != null)
                        {

                            try
                            {
                                if (Params.ContainsKey("ParentAction"))
                                    ParentAction = Int32.Parse(Params["ParentAction"].ToString());
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);

                            }


                            CloseTask(rs, ParentAction);
                        }
                        st = null;
                        rs = null;
                    }
                    else
                    {
                        if (results != null && results.Count > 0)
                        {

                            try
                            {
                                if (Params.ContainsKey("ParentAction"))
                                    ParentAction = Int32.Parse(Params["ParentAction"].ToString());
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                            }

                            CloseTask(results[0], ParentAction);
                        }
                    }
                    break;

                case RulePendingEvents.RefreshTask:
                    //RefreshTask(results[0], false, ref TaskIdsToRefresh);
                    break;

                case RulePendingEvents.ShowForum:
                    break;

                case RulePendingEvents.ShowInsert:
                    break;

                case RulePendingEvents.ShowMail:
                    //if (Params != null && Params.Count > 0)
                    //{
                    //    String resultDocId = results[0].ID.ToString();

                    //    Session["Subject" + resultDocId] = Params["Subject"].ToString();
                    //    Session["Body" + resultDocId] = Params["Body"].ToString();
                    //    Session["To" + resultDocId] = Params["To"].ToString();
                    //    Session["CC" + resultDocId] = Params["CC"].ToString();
                    //    Session["CCO" + resultDocId] = Params["CCO"].ToString();
                    //    Params = null;
                    //    resultDocId = null;
                    //    StringBuilder sb = new StringBuilder();
                    //    sb.Append("$(document).ready(function() {");
                    //    sb.Append("Email_Click();");
                    //    sb.Append("});");

                    //    if (!VariablesInterReglas.ContainsKey("accion"))
                    //        VariablesInterReglas.Add("accion", "executescript");
                    //    else
                    //        VariablesInterReglas.set_Item("accion", "executescript");

                    //    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                    //        VariablesInterReglas.Add("scripttoexecute", sb.ToString());
                    //    else
                    //        VariablesInterReglas.set_Item("scripttoexecute", sb.ToString());

                    //   // Page.ClientScript.RegisterStartupScript(this.GetType(), "DoMail", sb.ToString(), true);
                    //    RegisterStartupScript = true;

                    //}
                    break;

                case RulePendingEvents.ExecuteRule:
                    if (Params != null && Params.Count > 0)
                    {
                        //Guardo el ID de la DoExecute para ejecutar luego los hijos al finalizar
                        List<Int64> idlist = (List<Int64>)(Params["ExecuteRuleID"]);

                        foreach (Int64 i in idlist)
                        {
                            if (PendingChildRules.Contains(i) == false)
                            {
                                PendingChildRules.Add(i);
                            }
                        }

                        //Ejecuto la nueva regla marcada en la DoExecuteRule
                        Int64 newRuleID = Int64.Parse(Params["RuleID"].ToString());
                        Params.Clear();
                        ExecutedIDs.Clear();
                        wFExecution.ExecuteRule(newRuleID, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    }
                    break;
                case RulePendingEvents.ExecuteErrorRule:
                    //if (Params != null && Params.Count > 0)
                    //{
                    //    PendingChildRules.Clear();

                    //    //Ejecuto la nueva regla marcada en la DoExecuteRule
                    //    Int64 newRuleID = Int64.Parse(Params["ErrorRuleId"].ToString());
                    //    Params.Clear();
                    //    ExecutedIDs.Clear();
                    //    WFExec.ExecuteRule(newRuleID, ref results, PendigEvent, ExecutionResult,
                    //        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    //}
                    break;
                case RulePendingEvents.CancelExecution:
                    if (ExecutedIDs != null)
                        ExecutedIDs.Clear();
                    break;
                case RulePendingEvents.Distribuir:
                    {
                        SRules RulesS = new SRules();
                        List<IWFRuleParent> rules = RulesS.GetCompleteHashTableRulesByStep(results[0].StepId);

                        // Page.Session.Add("Distribuir" + results[0].TaskId, true);

                        if (rules != null)
                        {
                            //Ezequiel: Obtengo las id de las reglas de entrada de la etapa a derivar.
                            var stepRules = from rule in rules
                                            where rule.ParentType == TypesofRules.Entrada
                                            select rule.ID;


                            if (stepRules.ToList().Count > 0)
                            {
                                PendingChildRules.AddRange(stepRules);
                            }


                            wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                                ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                        }

                        // 23 / 08 / 11: Como la grilla es refrescada "on-demand"(solo cuando se selecciona su tab) no es necesaria esta llamada.
                        // string script = "parent.RefreshGrid();";
                        // Page.ClientScript.RegisterStartupScript(typeof(Page), "Message", script, true);
                        break;
                    }
                case RulePendingEvents.ResponseToDelete:

                    ParentAction = 0;
                    try
                    {
                        if (Params.ContainsKey("ParentAction"))
                            ParentAction = Int32.Parse(Params["ParentAction"].ToString());
                    }
                    catch (Exception)
                    {
                    }

                    CloseTask(results[0], ParentAction);


                    break;
                case RulePendingEvents.ValidateDistribute:
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ValidateDistribuir");


                    break;
                case RulePendingEvents.ExecuteEntryInWFDoGenerateTaskResult:
                    if (Params != null && Params.Count > 0 && Params.ContainsKey("ResultID") && Int64.Parse(Params["ResultID"].ToString()) > 0 && ((Params.ContainsKey("OpenTaskAfterInsert") && Boolean.Parse(Params["OpenTaskAfterInsert"].ToString())) || !Params.ContainsKey("OpenTaskAfterInsert")))
                    {

                        Int64 newresultID = Int64.Parse(Params["ResultID"].ToString());
                        //Codigo de ejecucion de reglas de entrada de la nueva tarea
                        STasks stasks = new STasks();
                        ITaskResult Task = stasks.GetTaskByDocId(newresultID);

                        //Ejecutar javascript que abre la pagina
                        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                        string ExecuteEntryInWFDoGenerateTaskResultscript = String.Empty;


                        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                        if (Task != null)
                        {
                            //Page.Session.Add("Entrada" + Task.ID, true);
                            SRights sRights = new SRights();

                            Int32 openMode = 0;
                            if (Params.ContainsKey("OpenMode"))
                            {
                                openMode = Int32.Parse(Params["OpenMode"].ToString());
                            }

                            System.Text.StringBuilder url = new System.Text.StringBuilder();
                            url.Append("../WF/TaskSelector.ashx?doctype=");
                            url.Append(Task.DocTypeId);
                            url.Append("&docid=");
                            url.Append(Task.ID);
                            url.Append("&userId=");
                            url.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                            ExecuteEntryInWFDoGenerateTaskResultscript = string.Format("parent.OpenDocTask3({0},{1},{2},{3},'{4}','{5}',{6},{7},{8});", Task.TaskId, 0, Task.DocTypeId, "false", Task.Name, url, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0, openMode);
                            ExecuteEntryInWFDoGenerateTaskResultscript += "parent.SelectTabFromMasterPage('tabtasks');";
                        }
                        // }

                        if (!string.IsNullOrEmpty(ExecuteEntryInWFDoGenerateTaskResultscript))
                        {
                            if (!VariablesInterReglas.ContainsKey("accion"))
                                VariablesInterReglas.Add("accion", "executescript");
                            else
                                VariablesInterReglas.set_Item("accion", "executescript");

                            if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                                VariablesInterReglas.Add("scripttoexecute", ExecuteEntryInWFDoGenerateTaskResultscript);
                            else
                                VariablesInterReglas.set_Item("scripttoexecute", ExecuteEntryInWFDoGenerateTaskResultscript);

                            // Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
                            RegisterStartupScript = true;
                        }


                        wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    }
                    else
                    {
                        //if (ExecutedIDs != null && ExecutedIDs.Count > 0)
                        //{
                        //    if (ExecutedIDs[ExecutedIDs.Count - 1] < 0)
                        //        ExecutedIDs[ExecutedIDs.Count - 1] = ExecutedIDs[ExecutedIDs.Count - 1] * -1;
                        //}
                        wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    }

                    break;

                case RulePendingEvents.ShowMessage:
                    //  ucrule = (UC_WF_Rules_UCDoScreenMessage)LoadControl("~/Views/UC/WF/Rules/UCDoScreenMessage.ascx");
                    Params["Nuevo Mensaje"].ToString();
                    if (Params.ContainsKey("Nuevo Mensaje"))
                    {
                        String scriptmsg = "$(document).ready( function(){swal('','" + Params["Nuevo Mensaje"].ToString().Replace("\n", "<br/> ").Replace(System.Environment.NewLine, " ") + "','info')";

                        if (Params.ContainsKey("Action") && Params["Action"].ToString() != string.Empty)
                        {
                            string action = Params["Action"].ToString();
                            switch (action)
                            {
                                case "Close":
                                    scriptmsg = scriptmsg + "window.close();";
                                    break;
                                case "Refresh":
                                    scriptmsg = scriptmsg + "window.location.reload();";

                                    break;
                            }
                            scriptmsg = scriptmsg + ".then(function{" + action + "})";
                        }


                        scriptmsg = scriptmsg + ";});";
                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", scriptmsg);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", scriptmsg);

                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "ExecuteMessage", ExecuteMessagescript, true);
                        RegisterStartupScript = true;
                    }
                    wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                    ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    break;


                case RulePendingEvents.GoToTabHome:

                    string script = "$(document).ready(function(){GoToTabHome();});";
                    if (!VariablesInterReglas.ContainsKey("accion"))
                        VariablesInterReglas.Add("accion", "executescript");
                    else
                        VariablesInterReglas.set_Item("accion", "executescript");

                    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                        VariablesInterReglas.Add("scripttoexecute", script);
                    else
                        VariablesInterReglas.set_Item("scripttoexecute", script);

                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "GoToTabHome", "$(document).ready(function(){GoToTabHome();});", true);
                    RegisterStartupScript = true;


                    break;

                case RulePendingEvents.ShowDoInputIndex:
                    //  ucrule = (UC_WF_Rules_UCDoInputIndex)LoadControl("~/Views/UC/WF/Rules/UCDoInputIndex.ascx");
                    break;

                case RulePendingEvents.ShowDoAsk:
                    // ucrule = (UC_WF_Rules_UCDoAsk)LoadControl("~/Views/UC/WF/Rules/UCDoAsk.ascx");
                    break;

                case RulePendingEvents.ShowDoAskDesition:
                    //   ucrule = (Views_UC_WF_Rules_UCDoAskDesition)LoadControl("~/Views/UC/WF/Rules/UCDoAskDesition.ascx");
                    break;

                case RulePendingEvents.ShowDoRequestData:
                    //   ucrule = (UC_WF_Rules_UCDoRequestData)LoadControl("~/Views/UC/WF/Rules/UCDoRequestData.ascx");
                    break;

                //08/07/11: Sumado el caso de la DoShowTable, para que carge nuevamente el control.
                case RulePendingEvents.ShowDoShowTable:
                    //ucrule = (Views_UC_WF_Rules_UCDoShowTable)LoadControl("~/Views/UC/WF/Rules/UCDoShowTable.ascx");
                    //if (Params != null && pnlUcRules != null && Params.ContainsKey("tableTitle"))
                    //    pnlUcRules.Attributes.Add("title", Params["tableTitle"].ToString());
                    break;

                case RulePendingEvents.OpenUrl:
                    if (Params.ContainsKey("url"))
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                        if (Params["OpenMode"] == null)
                        {

                            string OpenUrlscript = String.Format("parent.OpenWindow('{0}');", Params["url"].ToString());
                            if (!VariablesInterReglas.ContainsKey("accion"))
                                VariablesInterReglas.Add("accion", "executescript");
                            else
                                VariablesInterReglas.set_Item("accion", "executescript");

                            if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                                VariablesInterReglas.Add("scripttoexecute", OpenUrlscript);
                            else
                                VariablesInterReglas.set_Item("scripttoexecute", OpenUrlscript);

                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                            RegisterStartupScript = true;
                        }
                        else
                        {
                            string OpenUrlscript = string.Empty;

                            switch ((OpenType)Params["OpenMode"])
                            {


                                case OpenType.NewWindow:
                                    OpenUrlscript = String.Format("OpenWindow('{0}');", Params["url"].ToString());
                                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("OpenWindow('{0}');", Params["url"].ToString()), true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.NewTab:
                                    OpenUrlscript = String.Format("parent.OpenWindow('{0}');", Params["url"].ToString());
                                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.Modal:
                                    OpenUrlscript = "$(document).ready(function(){ShowIFrameModal('','" + Params["url"].ToString() + "', 800,600);});";
                                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameModal('','" + Params["url"].ToString() + "', 800,600);});", true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.Home:
                                    OpenUrlscript = "$(document).ready(function(){ShowIFrameHome('" + Params["url"].ToString() + "', 600);});";
                                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameHome('" + Params["url"].ToString() + "', 600);});", true);
                                    RegisterStartupScript = true;
                                    break;
                            }


                            if (!VariablesInterReglas.ContainsKey("accion"))
                                VariablesInterReglas.Add("accion", "executescript");
                            else
                                VariablesInterReglas.set_Item("accion", "executescript");

                            if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                                VariablesInterReglas.Add("scripttoexecute", OpenUrlscript);
                            else
                                VariablesInterReglas.set_Item("scripttoexecute", OpenUrlscript);
                        }
                    }
                    else
                    {
                        string OpenUrlscript = String.Format("parent.OpenWindow('{0}');", "../Tools/Messages.aspx?msg=1");
                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", OpenUrlscript);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", OpenUrlscript);

                        //                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", "../Tools/Messages.aspx?msg=1"), true);
                        RegisterStartupScript = true;
                    }
                    wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                         ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);

                    break;
                case RulePendingEvents.ExecuteScript:
                    if (Params.ContainsKey("ScriptToExecute"))
                    {
                        //   Session[TaskID + "CurrentExecution"] = null;

                        String ExecuteScriptscript = Params["ScriptToExecute"].ToString();

                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", ExecuteScriptscript);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", ExecuteScriptscript);

                        // Page.ClientScript.RegisterStartupScript(this.GetType(), "executescript", " " + script + " ", true);
                        RegisterStartupScript = true;
                    }
                    //Params.Clear();
                    //ExecutionResult = RuleExecutionResult.CorrectExecution;
                    //PendigEvent = RulePendingEvents.NoPendingEvent;
                    //PendingChildRules.Clear();

                    wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                 ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    break;
                case RulePendingEvents.EnableRules:
                    if (Params.ContainsKey("UserRules"))
                    {
                        Hashtable UserRules = (Hashtable)Params["UserRules"];
                        string EnableScript = string.Empty;
                        foreach (Int64 currentRuleId in UserRules.Keys)
                        {
                            List<Boolean> EnableList = (List<Boolean>)UserRules[currentRuleId];
                            Boolean IsEnabled = (Boolean)EnableList[0];
                            EnableScript = EnableScript + string.Format("EnableDisableRule({0},{1}); ", currentRuleId, IsEnabled).Replace("False", "false").Replace("True", "true");
                        }

                        string ExecuteScriptscript = "$(document).ready(function(){" + EnableScript + "});";
                        if (!VariablesInterReglas.ContainsKey("accion"))
                            VariablesInterReglas.Add("accion", "executescript");
                        else
                            VariablesInterReglas.set_Item("accion", "executescript");

                        if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                            VariablesInterReglas.Add("scripttoexecute", ExecuteScriptscript);
                        else
                            VariablesInterReglas.set_Item("scripttoexecute", ExecuteScriptscript);

                        //  Page.ClientScript.RegisterStartupScript(this.GetType(), "EnableRuleScript", "$(document).ready(function(){" + EnableScript + "});", true);
                        RegisterStartupScript = true;
                    }
                    wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                        ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    break;
                case RulePendingEvents.DoShowForm:
                    if (Params.ContainsKey("formid"))
                    {
                        var currentformId = Convert.ToInt64(results[0].ID);
                        var taskid = results[0].TaskId;
                        var formid = Convert.ToInt32(Params["formid"]);

                        if ((currentformId != 0) && currentformId != formid)
                        {
                            //Page.Session.Add("CurrentFormID" + results[0].ID, Params["formid"]);
                            List<Int64> refreshTasks = new List<Int64>() { results[0].TaskId };
                            //RefreshTask(results[0], false, ref refreshTasks);

                            //OpenFormInWindow(results[0]);
                        }
                    }
                    wFExecution.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                        ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, true);
                    break;
                case RulePendingEvents.RefreshWfTree:
                    //RefreshWFTree();
                    break;
                case RulePendingEvents.ShowBinary:
                    //if (Params.ContainsKey("BinaryFile") && Params.ContainsKey("BinaryMime"))
                    //{
                    //    //Se guardan los datos en la sesion y se quitan del objeto Params.
                    //    string id = new Random().Next().ToString();
                    //    Session["BinaryFile" + id] = Params["BinaryFile"];
                    //    Session["BinaryMime" + id] = Params["BinaryMime"];
                    //    Params.Remove("BinaryFile");
                    //    Params.Remove("BinaryMime");

                    //    //Se abre la ventana con el ashx que mostrará el archivo.
                    //    string script = MembershipHelper.Protocol + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/Services/GetBinary.ashx?id=" + id;
                    //    script = "$(document).ready(function() {parent.OpenWindow('" + script + "');});";

                    //    if (!VariablesInterReglas.ContainsKey("accion"))
                    //        VariablesInterReglas.Add("accion", "executescript");
                    //    else
                    //        VariablesInterReglas.set_Item("accion", "executescript");

                    //    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                    //        VariablesInterReglas.Add("scripttoexecute", script);
                    //    else
                    //        VariablesInterReglas.set_Item("scripttoexecute", script);

                    //   // Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowBinary", script, true);
                    //    RegisterStartupScript = true;
                    //}

                    break;
                case RulePendingEvents.LookingForFileToPrint:
                    //if (Params.ContainsKey("BarcodeToPrintPath"))
                    //{
                    //    string path = Params["BarcodeToPrintPath"].ToString().Replace(@"\", @"\\");
                    //    string CopiesCount = Params["CopiesCount"].ToString();
                    //    string Width = Params["Width"].ToString();
                    //    string Height = Params["Height"].ToString();

                    //    string script = @"$(document).ready(function(){PrintBarcode('" + path + "', " + CopiesCount + ", " + Width + ", " + Height + ");});";

                    //    if (!VariablesInterReglas.ContainsKey("accion"))
                    //        VariablesInterReglas.Add("accion", "executescript");
                    //    else
                    //        VariablesInterReglas.set_Item("accion", "executescript");

                    //    if (!VariablesInterReglas.ContainsKey("scripttoexecute"))
                    //        VariablesInterReglas.Add("scripttoexecute", script);
                    //    else
                    //        VariablesInterReglas.set_Item("scripttoexecute", script);


                    // //   Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintBarcode", script, true);
                    //    RegisterStartupScript = true;
                    //}
                    break;
            }




            //try
            //{

            //    if (System.Web.HttpContext.Current.Session["VariablesInterReglas"] != null)
            //    {
            //        string func = @"$(document).ready(function(){";
            //        foreach (string variable in VariablesInterReglas.Keys())
            //        {
            //            try
            //            {

            //                string variablevalue = VariablesInterReglas.get_Item(variable).ToString();
            //                func += string.Format(@"$('<input>').attr({ type: 'hidden',    id: 'zvar_{0}',    name: 'zvar_{0}',value:'{1}'}).appendTo('form');", variable, variablevalue);
            //            }
            //            catch (Exception)
            //            {
            //            }
            //        }
            //        func += @"});";
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateVariables", func, true);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ZClass.raiseerror(ex);
            //}

        }

        protected void CloseTask(ITaskResult result, Int32 ParentAction)
        {
            //STasks sTasks = new STasks();
            //if (new RightsBusiness().GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(result.StepId)) && Convert.ToBoolean(UP.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, "True", MembershipHelper.CurrentUser.ID)))
            //{
            //    sTasks.Finalizar(result);
            //}

            //sTasks = null;

            //if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseTask"))
            //{
            //    if (ParentAction == 0)
            //    {
            //        string js = "$(document).ready(function(){CloseTaskFromRule(" + result.TaskId.ToString() + ");});";
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
            //        RegisterStartupScript = true;
            //    }
            //    else if (ParentAction == 1)
            //    {
            //        string js = "$(document).ready(function(){ });";
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
            //        RegisterStartupScript = true;
            //    }
            //    else
            //    {
            //        string js = "$(document).ready(function(){CloseTaskFromRule(" + result.TaskId.ToString() + ");});";
            //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
            //        RegisterStartupScript = true;
            //    }
            //}
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------







        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getValuesToHerarchicalTag")]
        [OverrideAuthorization]
        public IHttpActionResult getValuesToHerarchicalTag(Int64 UserId, Int64 parentTagValue)
        {
            var user = GetUser(UserId);
            if (user == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));
            Results_Business resultBusiness = new Results_Business();

            try
            {
                DataTable dataFromHerarchicalParent = resultBusiness.getDataFromHerarchicalParent(parentTagValue);
                var newresults = JsonConvert.SerializeObject(dataFromHerarchicalParent, Formatting.Indented,
                                    new JsonSerializerSettings
                                    {
                                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                    });

                return Ok(newresults);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                         new HttpError(StringHelper.InvalidParameter)));
            }
            finally
            {
                resultBusiness = null;
            }

        }


        //Genérico

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getHerarchicalTagValues")]
        [OverrideAuthorization]
        public IHttpActionResult getHerarchicalTagValues(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                Results_Business resultBusiness = new Results_Business();
                Int64 parentTagValue = 0;
                List<string> indexs = new List<string>();
                bool isView = false;
                string tableId = string.Empty;

                if (paramRequest.Params != null)
                {
                    isView = bool.Parse(paramRequest.Params["isView"].ToString());
                    tableId = paramRequest.Params["tableId"].ToString();
                    parentTagValue = Int64.Parse(paramRequest.Params["parentTagValue"].ToString());
                    indexs.AddRange(paramRequest.Params["indexs"].ToString().Split(char.Parse(",")));
                }


                try
                {
                    DataTable dataFromHerarchicalParent = resultBusiness.getDataFromHerarchicalParent(parentTagValue, indexs, tableId, isView);
                    var newresults = JsonConvert.SerializeObject(dataFromHerarchicalParent, Formatting.Indented,
                                        new JsonSerializerSettings
                                        {
                                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                        });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                             new HttpError(StringHelper.InvalidParameter)));
                }
                finally
                {
                    resultBusiness = null;
                }
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                            new HttpError(StringHelper.InvalidParameter)));
            }

        }


        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getAttributeDescription")]
        [OverrideAuthorization]
        public IHttpActionResult getAttributeDescription(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));


                    if (paramRequest.Params != null)
                    {
                        var AttributeId = Int64.Parse(paramRequest.Params["AttributeId"].ToString());
                        var AttributeValue = paramRequest.Params["AttributeValue"].ToString();


                        AutoSubstitutionBusiness AS = new AutoSubstitutionBusiness();
                        var description = AS.getDescription(AttributeValue, AttributeId);


                        return Ok(description);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                    new HttpError(StringHelper.InvalidParameter)));
                    }

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                         new HttpError(StringHelper.InvalidParameter)));
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getAttributeDescriptionMotivoDemanda")]
        [OverrideAuthorization]
        public IHttpActionResult getAttributeDescriptionMotivoDemanda(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));


                    if (paramRequest.Params != null)
                    {
                        var motivo = Int64.Parse(paramRequest.Params["Motivo"].ToString());
                        var ramo = paramRequest.Params["Ramo"].ToString();

                        var reportId = Int64.Parse(paramRequest.Params["reportId"].ToString());

                        Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();
                        Hashtable completedValues = new Hashtable();

                        completedValues.Add("zamba.vars.ramo", ramo);
                        completedValues.Add("zamba.vars.motivo", motivo);

                        string description = string.Empty;

                        DataSet ds = RB.EvaluationRunWebQueryBuilder(reportId, false, completedValues, null);
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            description = ds.Tables[0].Rows[0][0].ToString();
                        }
                        return Ok(description);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                    new HttpError(StringHelper.InvalidParameter)));
                    }

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                         new HttpError(StringHelper.InvalidParameter)));
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getAttributeListMotivoDemanda")]
        [OverrideAuthorization]
        public IHttpActionResult getAttributeListMotivoDemanda(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));


                    if (paramRequest.Params != null)
                    {
                        var ramo = paramRequest.Params["Ramo"].ToString();

                        var reportId = Int64.Parse(paramRequest.Params["reportId"].ToString());

                        Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();
                        Hashtable completedValues = new Hashtable();

                        completedValues.Add("zamba.vars.ramo", ramo);

                        string description = string.Empty;

                        DataSet ds = RB.EvaluationRunWebQueryBuilder(reportId, false, completedValues, null);
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            var newresults = JsonConvert.SerializeObject(ds.Tables[0], Formatting.Indented,
                                          new JsonSerializerSettings
                                          {
                                              PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                          });

                            return Ok(newresults);
                        }
                        return Ok();
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                    new HttpError(StringHelper.InvalidParameter)));
                    }

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                                new HttpError(StringHelper.InvalidParameter)));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                         new HttpError(StringHelper.InvalidParameter)));
            }

        }

        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getHerarchicalEstudiosByJurisdiccion")]
        [OverrideAuthorization]
        public IHttpActionResult getEstudiosByJurisdiccion(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                Int64 parentTagValue = 0;

                if (paramRequest.Params != null)
                {
                    parentTagValue = Int64.Parse(paramRequest.Params["parentTagValue"].ToString());
                }


                try
                {
                    DataSet dataFromHerarchicalParent = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, string.Format(@"select LISTAGG ('""CD_ESTUDIO"":""' || a.CD_ESTUDIO  || '"",""DESC_ESTUDIO"":""' || a.DESC_ESTUDIO  || '"",""DESC_JURISDICCION"":""' || a.DESC_JURISDICCION  || '"",""PROVINCIA"":""' || a.PROVINCIA  || '"",""REFERENTE"":""' || b.i1001011  || '"",""NEGOCIADOR"":""' || B.I10170  || '"",""IDGRUPO"":""' || B.I10250  || '"",""JUICIO"":""' || CASE WHEN A.FE_INHABILITAC_ASOC_JUIC IS NULL THEN 'S' ELSE 'N' END  || '"",""MEDIACION"":""' || CASE WHEN A.FE_INHABILITAC_ASOC_MED IS NULL THEN 'S' ELSE 'N'  END  || '"",""HABILITADO"":""' || CASE WHEN A.FE_INHABILIT_ESTUDIO IS NULL THEN 'S' ELSE 'N' END  || '""') WITHIN GROUP(ORDER BY DESC_ESTUDIO) AS namelist  from sinv_estudios_jurisdiccion A,  doc_i10116 B where a.cd_jurisdiccion = {0} and a.cd_estudio = b.i2732 ", parentTagValue));

                    var newresults = JsonConvert.SerializeObject(dataFromHerarchicalParent, Formatting.Indented,
                                        new JsonSerializerSettings
                                        {
                                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                        });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                             new HttpError(StringHelper.InvalidParameter)));
                }
            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                            new HttpError(StringHelper.InvalidParameter)));
            }

        }


        /// <summary>
        /// Obtiene las variables interreglas, lo convierte en un objeto JSON y lo devuelve.
        /// </summary>
        /// <param name="keyList"></param>
        /// <returns></returns>
        public string VariablesInterreglesToJson(ICollection keyList)
        {
            Dictionary<string, object> Lista_VariablesInterReglas = new Dictionary<string, object>();
            foreach (var item in keyList)
            {
                Lista_VariablesInterReglas.Add(item.ToString(), item != "" ? VariablesInterReglas.get_Item(item).ToString() : "");
            }

            return JsonConvert.SerializeObject(Lista_VariablesInterReglas, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        /// <summary>
        /// Obtiene las reglas que tiene asignado el usuario logueado para la grilla de resultados.
        /// </summary>
        /// <param name="paramRequest"></param>
        /// <returns></returns>
        [AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetResultsGridButtons")]

        [OverrideAuthorization]
        public List<IDynamicButton> GetResultsGridButtons(genericRequest paramRequest)
        {
            List<IDynamicButton> List_ActionsForResultsGrid = new List<IDynamicButton>();

            DynamicButtonBusiness miDinamicButtonInstance = DynamicButtonBusiness.GetInstance();

            List_ActionsForResultsGrid = miDinamicButtonInstance.GetButtons(ButtonType.Rule, ButtonPlace.GrillaResultados, GetUser(paramRequest.UserId));

            return List_ActionsForResultsGrid;
        }


    }

    /// <summary>
    /// Enumerador de las variables interreglas por convencion.
    /// </summary>
    enum EVariablesInterReglas_Convencion
    {
        msg,
        message,
        error,
        accion
    }
}



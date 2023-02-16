
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Zamba.Core.WF.WF;
using System.Web.Security;
using System.Web.UI;
using System.Text;
using Zamba.Web.App_Code.Helpers;
using Zamba.Web;
using Zamba.Membership;
using Zamba;
using Zamba.Framework;
using System.Web;

public partial class TaskViewer : System.Web.UI.Page, ITaskViewer
{

    #region "Properties"
    public Int64 Task_ID { get; set; }

    public ITaskResult TaskResult { get; set; }
    #endregion

    public Boolean IsStartupScriptRegistered { get; set; }

    WFTaskBusiness WFTB = new WFTaskBusiness();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        
        Int64 UrlUserId = 0;
        string userToken = string.Empty;
        try
        {
            // se optiene el user de la url
            if (!string.IsNullOrEmpty(Request.QueryString["user"])){
                UrlUserId = Convert.ToInt64(Request.QueryString["user"]);  
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userid"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["userId"]))
            {
                UrlUserId = Convert.ToInt64(Request.QueryString["userId"]);
            }

            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                userToken = Request.QueryString["t"].ToString().Trim();
            }

             ZTrace.WriteLineIf(ZTrace.IsVerbose, "UrlUserId : " + UrlUserId);

            //HttpContext.Current.Session["User"] = null;

            Results_Business RB = new Results_Business();

            if (MembershipHelper.CurrentUser != null && Response.IsClientConnected)
            {
                
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1 - El currentUser no es null ");
                Int64 userid = MembershipHelper.CurrentUser.ID;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "userid :  " + userid);
                //Se evalua si el usuario de la url es diferente al del MembershipHelper para hacer relogin

                // si el usuario es diferente el usuario se sacar del currentuser y se relogea, vuelve a pasar y valida ya q el usuario esta bien
                bool reloadModalLogin = userid != UrlUserId ? true : false;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "reloadModalLogin :  " + reloadModalLogin);

                bool isActiveSession = true;

                if (!reloadModalLogin && !Page.IsPostBack)
                {
                    isActiveSession = RB.getValidateActiveSession(userid, userToken);
                }

                
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);

                
                if ( !reloadModalLogin && !isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 1.1 - isActiveSession es false se dispara modal ");
                    Modal_login(reloadModalLogin);
                }
            }
            else {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2 - El currentUser  es null ");
                bool isActiveSession = RB.getValidateActiveSession(UrlUserId, userToken);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "isActiveSession :  " + isActiveSession);

                if (isActiveSession)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1 - isActiveSession  es true, reestablezo la session ");
                    UserBusiness ub = new UserBusiness();
                    ub.ValidateLogIn(UrlUserId, ClientType.Web);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.1.1 -  valido el usuario creado" + MembershipHelper.CurrentUser.ID);
                }
                else {

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Paso 2.2 - isActiveSession  es false, se dispara modal ");
                    Modal_login(true);
                }
            }
        }
        catch (global::System.Exception ex)
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }

        ZCore ZC = new ZCore();
        Page.Theme = ZC.InitWebPage();
        ZC.VerifyFileServer();
    }
    void PushNotification()
    {
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            hdnPushNotification_app_id.Value = zopt.GetValue("push_notification_app_id");
            string queryPushNotificationPlayerId = "select player_id from push_notification where user_id=" + Request.QueryString["userid"];
            DataTable retPushNotificationPlayerId = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, queryPushNotificationPlayerId).Tables[0];
            if (retPushNotificationPlayerId.Rows.Count == 1)
                hdnPushNotification_player_id.Value = retPushNotificationPlayerId.Rows[0][0].ToString();
        }
        catch (Exception)
        {

        }


    }

    public void Modal_login( Boolean reloadModalLogin) {
        string loginUrl = FormsAuthentication.LoginUrl.ToString();
        string script = "$(document).ready(function() {  var linkSrc = location.origin.trim() + '" + loginUrl.Replace(".aspx", "").Trim() + "?showModal=true&reloadLogin=" + reloadModalLogin + "';  document.getElementById('iframeModalLogin').src = linkSrc; $('#modalLogin').modal('show');});";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);
    }

    protected void Page_PreLoad(object sender, System.EventArgs e)
    {
        try
        {


            //PushNotification();
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["TaskId"]))
                {
                    STasks STasks = new STasks();
                    Results_Business RB = new Results_Business();

                    Task_ID = Convert.ToInt64(Request.QueryString["TaskId"]);

                    if (Task_ID == 0)
                    {
                        if (!string.IsNullOrEmpty(Request.QueryString["DocTypeId"]))
                        {
                            Int64 DocTypeId = Convert.ToInt64(Request.QueryString["DocTypeId"]);
                            if (DocTypeId > 0)
                            {
                                INewResult result = RB.GetNewNewResult(DocTypeId);
                                if (RB.Insert(ref result, false, false, false, false, true, false, false, false, false, Zamba.Membership.MembershipHelper.CurrentUser.ID) == InsertResult.Insertado)
                                {
                                    if (!string.IsNullOrEmpty(Request.QueryString["WFId"]))
                                    {
                                        Int64 WFId = Convert.ToInt64(Request.QueryString["WFId"]);
                                        RB.AdjuntarAWF(new ArrayList() { result }, WFId);
                                    }

                                    TaskResult = STasks.GetTaskByDocIdAndDocTypeId(result.ID, result.DocTypeId);
                                }
                            }
                        }

                    }
                    else
                    {
                        TaskResult = STasks.GetTask(Task_ID, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                    }

                    STasks = null;

                    if ((TaskResult != null))
                    {
                        //Se comprueba si se pulsó el botón cerrar, si lo hizo se cierra desde acá
                        //y se corta la cade de page load con los controles asignandolos a null
                        if (!string.IsNullOrEmpty(this.Request["__EVENTTARGET"]) && this.Request["__EVENTTARGET"].Contains("BtnClose"))
                        {
                            CloseAndFinishTask();

                        }
                        else
                        {
                            //Obtener las reglas que se podrán ejecutar por el usuario
                            TaskResult.UserRules = GetDoEnableRules();
                            ucTaskHeader.TaskResult = TaskResult;
                            ucTaskDetail.TaskResult = TaskResult;

                            ucTaskHeader.ExecuteRule -= ExecuteRule;
                            ucTaskHeader.ExecuteRule += ExecuteRule;
                            ucTaskDetail.ExecuteRule -= ExecuteRule;
                            ucTaskDetail.ExecuteRule += ExecuteRule;

                            //Ezequiel - 01/02/10 - Creo variable de ejecucion de workflow y se la paso al taskheader.
                            WFExecution WFExec = new WFExecution(Zamba.Membership.MembershipHelper.CurrentUser);
                            WFExec._haceralgoEvent -= UC_WFExecution.HandleWFExecutionPendingEvents;
                            WFExec._haceralgoEvent += UC_WFExecution.HandleWFExecutionPendingEvents;
                            UC_WFExecution.RefreshTask += RefreshTask;
                            UC_WFExecution.OpenFormInWindow += openFormInWindow;
                            UC_WFExecution.RefreshWFTree -= GenerateWfRefreshJs;
                            UC_WFExecution.RefreshWFTree += GenerateWfRefreshJs;
                            this.UC_WFExecution.WFExec = WFExec;
                            this.UC_WFExecution.TaskID = Task_ID;
                            WFTaskBusiness wFTaskBusiness = new WFTaskBusiness();
                            UserGroupBusiness UGB = new UserGroupBusiness();
                            System.Collections.Generic.List<long> users = UGB.GetUsersIds(TaskResult.AsignedToId);
                            if ((TaskResult.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID || users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)))
                            {
                                wFTaskBusiness.UpdateTaskState(Task_ID, (Int64)TaskStates.Ejecucion);
                            }
                        }
                    }
                    else {

                        string script = "$(document).ready(function() { swal({ title: '', text: 'No tiene permisos para ver esta tarea', icon: 'info', buttons: true, dangerMode: true, closeOnClickOutside: false, buttons:{ agregar: { text: 'ok' }, }, }) .then((value) => { switch (value) { case 'agregar': window.close(); break; } }); });";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);

                    }
                    hdnTaskID.Value = Task_ID.ToString();
                }
            }
            else
            {
                this.Controls.Remove(ucTaskDetail);
                this.Controls.Remove(ucTaskHeader);
                ucTaskDetail = null;
                ucTaskHeader = null;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void CloseAndFinishTask(ITaskResult task = null)
    {
        try
        {
            this.Controls.Remove(ucTaskDetail);
            this.Controls.Remove(ucTaskHeader);
            ucTaskDetail = null;
            ucTaskHeader = null;

            ITaskResult taskToClose = default(ITaskResult);
            if (task == null)
            {
                taskToClose = this.TaskResult;
            }
            else
            {
                taskToClose = task;
            }

            if (TaskResult != null)
            {
                WFTB.UnLockTask(TaskResult.TaskId);
            }

            //Marca a la tarea como cerrada para el usuario
            WFTB.CloseOpenTasksByTaskId(taskToClose.TaskId);

            //Se cierra la tarea abierta
            string script = "$(window).on('load',function(){hideLoading();isClosingTask=true;parent.CloseTask(" + taskToClose.TaskId + ",true);});";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "closingScript", script, true);
            IsStartupScriptRegistered = true;

            //Finalización de la tarea en caso de que corresponda
            if (taskToClose.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID)
            {
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                SRights SRights = new SRights();

                //Si tiene el permiso de TERMINAR o el tilde de FINALIZAR_AL_CERRAR, entonces desasigna la tarea.
                if (new RightsBusiness().GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(taskToClose.StepId)) && Convert.ToBoolean(UP.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, "True", Zamba.Membership.MembershipHelper.CurrentUser.ID)))
                {
                    taskToClose.TaskState = Zamba.Core.TaskStates.Desasignada;
                    taskToClose.AsignedToId = 0;
                }
                else
                {
                    taskToClose.TaskState = Zamba.Core.TaskStates.Asignada;
                    taskToClose.AsignedToId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                }
                SRights = null;
                UP = null;

                STasks STasks = new STasks();
                STasks.Finalizar(taskToClose);
                STasks = null;

                System.Collections.Generic.List<Zamba.Core.ITaskResult> Results = new System.Collections.Generic.List<Zamba.Core.ITaskResult>();
                Results.Add(taskToClose);

                foreach (Zamba.Core.WFRuleParent Rule in taskToClose.WfStep.Rules)
                {
                    if (Rule.RuleType == TypesofRules.Terminar)
                    {
                        SRules SRules = new SRules();
                        SRules.ExecuteRule(Rule, Results, false);
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Zamba.Core.ZClass.raiseerror(ex);
        }
        finally
        {
            TaskResult = null;
            string Script = "$(document).ready(function(){ hideLoading();});";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseLoadingDialog", Script, true);
            IsStartupScriptRegistered = true;

        }
    }

    private void RefreshTask(ITaskResult task, bool DoPostBack, ref List<long> TaskIDsToRefresh)
    {

        try
        {
            if (TaskIDsToRefresh == null)
            {
                TaskIDsToRefresh = new List<long>();
            }

            if (!TaskIDsToRefresh.Contains(task.TaskId))
            {
                TaskIDsToRefresh.Add(task.TaskId);
            }

            StringBuilder sbScript = new StringBuilder();
            long taskIdToRefresh = 0;
            List<long> idsRegistereds = new List<long>();
            ITaskResult tempTask = default(ITaskResult);

            long max = TaskIDsToRefresh.Count;

            sbScript.Append("$(document).ready(function(){");
            Zamba.Core.WF.WF.WFTaskBusiness WFTB = new Zamba.Core.WF.WF.WFTaskBusiness();
            for (int i = 0; i <= max - 1; i++)
            {
                taskIdToRefresh = TaskIDsToRefresh[i];

                if (!idsRegistereds.Contains(taskIdToRefresh) && taskIdToRefresh > 0)
                {
                    tempTask = WFTB.GetTask(taskIdToRefresh, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                    if (tempTask != null)
                    {
                        sbScript.Append("RefreshTask(");
                        sbScript.Append(taskIdToRefresh);
                        sbScript.Append(",");
                        sbScript.Append(tempTask.ID);
                        sbScript.Append(");");

                        idsRegistereds.Add(taskIdToRefresh);
                    }
                }
            }
            WFTB = null;

            sbScript.Append("});");
            TaskIDsToRefresh.Clear();
            if (IsStartupScriptRegistered == false)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshTaskScript", sbScript.ToString(), true);
            }
            else
            {
                IsStartupScriptRegistered = false;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }



    private void openFormInWindow(ITaskResult task)
    {
        StringBuilder sbScript = new StringBuilder();
        //string url = ;
        Zamba.Core.WF.WF.WFTaskBusiness WFTB = new Zamba.Core.WF.WF.WFTaskBusiness();
        //string host = Request.Url.Host;
        if (Request.QueryString.Get("breakmodal") == "1")
        {
            return;
        }
        string url = "'/bpm/Views/WF/TaskViewer.aspx?doctypeid=" + task.DocTypeId.ToString() + "&taskid=" + task.TaskId.ToString() + "&docid=" + task.ID.ToString() + "&mode=&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString() + "&f=" + task.CurrentFormID + "&mf=" + task.ModalFormID + "'";

        sbScript.Append("$(document).ready(function(){");
        sbScript.Append("OpenDocTask3(");
        sbScript.Append(task.TaskId.ToString() + ", ");
        sbScript.Append(task.ID.ToString() + ", ");
        sbScript.Append(task.DocTypeId.ToString() + ", ");
        sbScript.Append("null" + ", ");
        sbScript.Append("'" + task.Name.ToString() + "'" + ", ");
        sbScript.Append(url + ", ");
        sbScript.Append("null" + ", ");
        sbScript.Append(task.StepId.ToString() + ", ");
        sbScript.Append("1,");
        sbScript.Append(task.CurrentFormID.ToString() + ",");
        sbScript.Append(task.ModalFormID.ToString());
        sbScript.Append(");");
        sbScript.Append("});");



        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenwinScript", sbScript.ToString(), true);
        IsStartupScriptRegistered = true;

    }




    private void GenerateCloseTaskJs(Int64 taskID)
    {
        try
        {
            Response.Write("<script language='javascript'> { parent.CloseTask(" + taskID + "),false;}</script>");
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Actualizo el arbol de tareas
    /// </summary>
    /// <remarks></remarks>
    private void GenerateWfRefreshJs()
    {
        try
        {
            Response.Write("<script language='javascript'> { parent.RefreshGrid('tareas');}</script>");
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    /// <summary>
    /// Metodo el cual verifica si existe una doshowform entre las reglas.
    /// </summary>
    /// <param name="_rule"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    //private IWFRuleParent CheckChildRules(IWFRuleParent _rule)
    //{
    //    IWFRuleParent raux = null;
    //    try
    //    {
    //        foreach (IRule _ruleaux in _rule.ChildRules)
    //        {
    //            raux = CheckChildRules((IWFRuleParent)_ruleaux);
    //        }
    //        if (_rule.GetType().FullName == "Zamba.WFActivity.Regular.DoShowForm")
    //        {
    //            raux = _rule;
    //            ((IDoShowForm)_rule).RuleParentType = TypesofRules.AbrirDocumento;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Zamba.AppBlock.ZException.Log(ex);
    //    }
    //    return raux;
    //}

    /// <summary>
    /// Verifica que reglas se habilitan por la DoEnableRule
    /// </summary>
    /// <returns>Devuelve una hashtable donde la key es el ID de la regla(long) y el valor es una lista de boolans.</returns>
    /// <remarks></remarks>
    private Hashtable GetDoEnableRules()
    {
        try
        {
            ////Se comenta esto, dado que no es necesario obtener la etapa para ver sus reglas
            ////Obtenemos las reglas de esa etapa
            ////Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

            //Hashtable returnEnableRules = new Hashtable();
            //Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
            //Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
            //List<Int64> ExecutedIDs = new List<Int64>();
            //Hashtable Params = new Hashtable();
            //List<Int64> PendingChildRules = new List<Int64>();
            ////Dim tempRuleBooleanList As List(Of Boolean) = New List(Of Boolean)()
            //System.Collections.Generic.List<ITaskResult> List = new System.Collections.Generic.List<ITaskResult>();
            //List.Add(TaskResult);

            ////Se mueven estas variables del foreach, ya que se utilizara linq y for, 
            ////para luego ver si se puede hacer en paralelo
            //WFRulesBusiness WFRB = new WFRulesBusiness();
            //bool RefreshRule = false;
            //SRules sRules = new SRules();

            //List<IWFRuleParent> rules = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

            //if ((rules != null))
            //{
            //    //var temprulesOpenDoc = from rule in rules where rule.RuleType == TypesofRules.AbrirDocumento && string.Compare(rule.GetType().FullName, "Zamba.WFActivity.Regular.DoShowForm") != 0 select rule;
            //    var temprulesOpenDoc = from rule in rules where rule.RuleType == TypesofRules.AbrirDocumento  select rule;
            //    var rulesOpenDoc = temprulesOpenDoc.ToList();

            //    if (rulesOpenDoc != null && rulesOpenDoc.Count > 0)
            //    {
            //        if (TaskResult.WfStep.Rules.Count == 0)
            //        {
            //            var userActionRules = from rule in rules where rule.ParentType == TypesofRules.AccionUsuario select rule;

            //            TaskResult.WfStep.Rules.AddRange(userActionRules);
            //        }

            //        for (int i = 0; i <= rulesOpenDoc.Count - 1; i++)
            //        {
            //            RefreshRule = rulesOpenDoc[i].RefreshRule.Value;
            //            WFRB.ExecuteWebRule(rulesOpenDoc[i].ID, List, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh);
            //        }
            //    }

            //    if (TaskResult.CurrentFormID > 0 || HasForms(TaskResult.DocTypeId))
            //    {
            //        ucTaskDetail.HiddenCurrentFormIDValue = TaskResult.CurrentFormID.ToString();
            //    }
            //}

            return TaskResult.UserRules;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return null;
        }
    }

    public bool HasForms(Int64 docTypeId)
    {
        SForms sForms = new SForms();
        bool result = sForms.HasForms(docTypeId);
        sForms = null;

        return result;
    }

    /// <summary>
    /// Ejecuta las reglas desde la master
    /// </summary>
    /// <param name="ruleId">ID de la regla que se quiere ejecutar</param>
    /// <param name="results">Tareas a ejecutar</param>
    /// <remarks></remarks>
    private void ExecuteRule(Int64 ruleId, List<Zamba.Core.ITaskResult> results)
    {
        try
        {
            Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
            Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
            List<Int64> ExecutedIDs = new List<Int64>();
            List<Int64> PendingChildRules = new List<Int64>();
            Hashtable Params = new Hashtable();
            bool RefreshRule = false;

            if (UC_WFExecution.TaskIdsToRefresh == null)
            {
                UC_WFExecution.TaskIdsToRefresh = new List<long>();
            }

            //  ZTrace.WriteLineIf(ZTrace.IsVerbose, "UC_WFExecution.WFExec.ExecuteRule" + ruleId.ToString());

            this.UC_WFExecution.WFExec.ExecuteRule(ruleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh, false);

        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            string script = "$(document).ready(function() { swal( '','" + ex.Message + "','info' ); });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ZException", script, true);
            throw ex;
        }
    }



    /// <summary>
    /// Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
    /// </summary>
    /// <remarks></remarks>
    [WebMethod()]
    public static FieldOptions GetZVarOptions(string controlId, string dataSourceName, string displayMember, string valueMember, string filterColumn, string filterValue)
    {
        FormControlsController formControlsController = new FormControlsController();

        return formControlsController.GetZVarOptions(controlId, dataSourceName, displayMember, valueMember, filterColumn, filterValue);
    }

    /// <summary>
    /// Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
    /// </summary>
    /// <remarks></remarks>
    [WebMethod()]
    public static FieldOptions GetZDynamicTable(string controlId, string dataSource, string showColumns, string filterFieldId, string editableColumns, string editableColumnsAttributes, string filterValues, string additionalValidationButton, string postAjaxFuncion)
    {
        FormControlsController formControlsController = new FormControlsController();

        return formControlsController.GetZDynamicTable(controlId, dataSource, showColumns, filterFieldId, editableColumns, editableColumnsAttributes, filterValues, additionalValidationButton, postAjaxFuncion);
    }

    /// <summary>
    /// Devuelve una lista de key value, para renderizar a las opciones de un autocomplete. ZVar aun no soportado.
    /// </summary>
    /// <remarks></remarks>
    [WebMethod()]
    public static List<KeyValuePair<string, string>> GetAutoCompleteOptions(string query, string dataSource, string displayMember, string valueMember, string additionalFilters)
    {
        FormControlsController formControlsController = new FormControlsController();

        return formControlsController.GetAutoCompleteOptions(query, dataSource, displayMember, valueMember, additionalFilters);
    }

}

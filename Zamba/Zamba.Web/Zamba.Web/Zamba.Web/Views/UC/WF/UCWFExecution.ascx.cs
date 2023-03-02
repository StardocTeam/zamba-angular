using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Collections;
using System.Text;
using System.Data;
using Zamba.Services;
using Zamba.Core.Enumerators;
using Zamba.Core.WF.WF;
using Zamba.Membership;
using Zamba.Web.App_Code.Helpers;
using Zamba;
using Zamba.Framework;
using Newtonsoft.Json;
using AttributeRouting.Helpers;

public partial class UC_WF_UCWFExecution : System.Web.UI.UserControl
{
    private long _taskID;
    private WFExecution _wfExec;
    public delegate void _refreshTask(ITaskResult task, Boolean DoPostBack, ref List<long> TaskIDsToRefresh);
    public delegate void _refreshWFTree();
    public delegate void _openFormInWindow(ITaskResult task);

    public event _refreshTask RefreshTask;
    public event _refreshWFTree RefreshWFTree;
    public event _openFormInWindow OpenFormInWindow;

    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
    RightsBusiness RB = new RightsBusiness();
    UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();

    public WFExecution WFExec
    {
        get { return this._wfExec; }
        set { this._wfExec = value; }
    }

    public long TaskID
    {
        get
        {
            return this._taskID;
        }
        set
        {
            this._taskID = value;
        }
    }

    public List<long> TaskIdsToRefresh
    {
        get
        {
            return (List<long>)Session["TaskIdsToRefresh"];
        }
        set
        {
            Session["TaskIdsToRefresh"] = value;
        }
    }

    public Boolean RegisterStartupScript { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
            Session[TaskID + "CurrentExecution"] = null;

        //Actualiza el timeout
        if (Page.IsPostBack == false && Zamba.Membership.MembershipHelper.CurrentUser != null)
        {
            try
            {
                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                Int32 type = 0;

                //if (user.WFLic)
                //{
                //    type = 1;
                //}
                //else
                //{
                //    Ucm ucm = new Ucm();
                //    ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, Request.UserHostAddress.Replace("::1","127.0.0.1"), 30, 1);

                //    Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                //    (Zamba.Membership.MembershipHelper.CurrentUser).WFLic = true;
                //    type = 1;
                //}

                SRights rights = new SRights();
                if (user.ConnectionId > 0)
                {
                    Ucm ucm = new Ucm();
                    ucm.UpdateOrInsertActionTime(user.ID, user.Name, Request.UserHostAddress.Replace("::1", "127.0.0.1"), user.ConnectionId, 30, type);

                }

            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }


        //Si es postback y hay un evento pendiente vuelvo a regenerar el control.
        if (Page.IsPostBack && Session[TaskID + "CurrentExecution"] != null)
        {
            RulePendingEvents uctoexecute = (RulePendingEvents)Session[TaskID + "CurrentExecution"];
            Boolean RefreshRule = false;
            LoadUCRule(false, 0, null, uctoexecute, RuleExecutionResult.PendingEventExecution, null, null, null, ref RefreshRule, TaskIdsToRefresh);
        }
    }

    public string RuleButtonOk
    {
        get { return this.hdnRuleName.Value.ToString(); }
        set { this.hdnRuleName.Value = value; }
    }

    public void HandleWFExecutionPendingEvents(long RuleId, ref List<ITaskResult> results,
        ref RulePendingEvents PendigEvent, ref RuleExecutionResult ExecutionResult,
        ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules,
        ref Boolean RefreshRule, List<long> TaskIdsToRefresh, Boolean IsAsync)
    {
        if (ExecutionResult == RuleExecutionResult.FailedExecution)
        {
            ShowScreenMessage("Ha ocurrido un error en la regla");
            Session["ExecutingRule"] = null;
        }
        else if (ExecutionResult == RuleExecutionResult.CorrectExecution)
        {
            //  ZTrace.WriteLineIf(ZTrace.IsVerbose, "HandleWFExecutionPendingEvents CorrectExecution  Session[ExecutingRule] = null");
            Session["ExecutingRule"] = null;
        }
        else
        {
            // ZTrace.WriteLineIf(ZTrace.IsVerbose, "HandleWFExecutionPendingEvents else  LoadUCRule RuleId: " + RuleId.ToString());
            LoadUCRule(true, RuleId, results, PendigEvent, ExecutionResult, ExecutedIDs,
                Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
        }
    }

    private void LoadUCRule(bool firstload, long RuleId, List<ITaskResult> results,
        RulePendingEvents PendigEvent, RuleExecutionResult ExecutionResult,
        List<Int64> ExecutedIDs, Hashtable Params, List<Int64> PendingChildRules,
        ref Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        try
        {

            //Guardo en sesion la regla que se esta ejeucutando
            Control ucrule = null;
            Int32 ParentAction = 0;
            if (this.TaskID <= 0 && results != null && results.Count > 0)
            {
                this.TaskID = results[0].TaskId;
                this.hdnCurrTaskID.Value = this.TaskID.ToString();
            }

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
                        Session[newRan.ToString()] = Content;
                        string script = "$(document).ready(function() { parent.OpenWindow('../UC/WF/Rules/DoExportToPDF.aspx?Content=" + newRan.ToString();
                        if (Params["ReturnFileName"] != null)
                        {
                            script += "&ReturnFileName=" + Params["ReturnFileName"].ToString();
                        }
                        if (Params["CanEditable"] != null)
                        {
                            script += "&CanEditable=" + Params["CanEditable"].ToString();
                        }
                        script += "');});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "DoExportHTMLToPDF", script, true);
                        RegisterStartupScript = true;
                    }
                    break;
                case RulePendingEvents.ShowDoAsociatedForm:
                    if (Params["FormID"] != null)
                    {
                        if ((bool)Params["HaveSpecificAttributes"])
                        {
                            Session["SpecificAttrubutes" + _taskID.ToString()] = Params["SpecificAttrubutes"];
                        }

                        string script = "AsociateForm('" + Params["FormID"] + "','" + Params["DocID"] + "','" + Params["DocTypeId"] + "','" +
                            _taskID + "','" + Params["ContinueWithCurrentTasks"] + "','" + Params["DontOpenTaskAfterInsert"] +
                            "','" + Params["FillCommonAttributes"] + "','" + Params["HaveSpecificAttributes"] + "');";
                        Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddAsociatedFormScript", script, true);
                        RegisterStartupScript = true;

                    }
                    break;
                //05/07/11: Sumada la opcion de la regla DoAddAsociatedDocument
                case RulePendingEvents.ShowDoAddAsociatedDoc:
                    if (Params["DocID"] != null)
                    {
                        StringBuilder url = new StringBuilder();
                        url.Append("../../Views/Insert/Insert.aspx?docid=");
                        url.Append(Params["DocID"]);
                        url.Append("&doctypeid=");
                        url.Append(Params["DocTypeId"]);
                        //19/07/11: Sumado un parámetro mas para poder obtener los indices del documento que llama la regla
                        url.Append("&FillIndxDocTypeID=");
                        url.Append(Params["FillIndxDocTypeID"]);
                        url.Append("&isview=true");
                        url.Append("&CallTaskID=");
                        url.Append(_taskID.ToString());
                        url.Append("&haveSpecificAtt=");
                        url.Append(Params["HaveSpecificAttributes"]);

                        if ((bool)Params["HaveSpecificAttributes"])
                        {
                            Session["SpecificAttrubutes" + _taskID.ToString()] = Params["SpecificAttrubutes"];
                        }

                        if (Params.ContainsKey("NextRuleIds"))
                        {
                            url.Append("&NR=");
                            url.Append(Params["NextRuleIds"]);
                        }
                        Boolean OpenDoAddAsociatedDocumentInModal = false;

                        if (Params.ContainsKey("DontOpenTaskIfIsAsociatedToWF"))
                        {
                            OpenDoAddAsociatedDocumentInModal = Boolean.Parse(Params["DontOpenTaskIfIsAsociatedToWF"].ToString());
                        }

                        OpenDoAddAsociatedDocumentInModal = Boolean.Parse(ZOptBusiness.GetValueOrDefault("OpenDoAddAsociatedDocumentInModal", "true"));

                        //"instalamos" el javascript cuando refresca la página.
                        string script = "$(document).ready(function () { $('#IFDialogContent').unbind('load'); if(parent != null) {parent.ShowInsertAsociated('" + url.ToString() + "'," + OpenDoAddAsociatedDocumentInModal.ToString().ToLower() + ");} else { ShowInsertAsociated('" + url.ToString() + "'," + OpenDoAddAsociatedDocumentInModal.ToString().ToLower() + "); } DisableRulesByTaskId(" + TaskID + ");});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "DoAddAsociatedDocumentScript", script, true);
                        RegisterStartupScript = true;

                    }
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
                        StringBuilder url = new StringBuilder();
                        url.Append("../WF/TaskSelector.ashx?doctype=");
                        url.Append(Params["DocTypeId"]);
                        url.Append("&docid=");
                        url.Append(Params["DocID"]);
                        url.Append("&userId=");
                        url.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        IResult res = new SResult().GetResult((long)Params["DocID"], (long)Params["DocTypeId"], false);

                        string script = string.Format("parent.OpenDocTask3({0},{1},{2},{3},'{4}','{5}',{6},{7},{8});", 0, res.ID, res.DocTypeId, "false", res.Name, url, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0, openMode);
                        script += "parent.SelectTabFromMasterPage('tabtasks');";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "openTask", script, true);
                        RegisterStartupScript = true;

                    }
                    break;

                case RulePendingEvents.RuleRefreshTask:
                    if (Params["TaskIDToRefresh"] != null && (long)Params["TaskIDToRefresh"] > 0)
                    {
                        Session[TaskID + "CurrentExecution"] = null;

                        if (results == null)
                        {
                            results = new List<ITaskResult>();
                            STasks stasks = new STasks();
                            results.Add(stasks.GetTask(TaskID, Zamba.Membership.MembershipHelper.CurrentUser.ID));
                        }

                        if (TaskIdsToRefresh != null)
                        {
                            TaskIdsToRefresh.Add((long)Params["TaskIDToRefresh"]);
                        }

                        WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                         ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    }
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
                    RefreshTask(results[0], false, ref TaskIdsToRefresh);
                    break;

                case RulePendingEvents.ShowForum:
                    break;

                case RulePendingEvents.ShowInsert:
                    break;

                case RulePendingEvents.ShowMail:
                    if (Params != null && Params.Count > 0)
                    {
                        String resultDocId = results[0].ID.ToString();

                        Session["Subject" + resultDocId] = Params["Subject"].ToString();
                        Session["Body" + resultDocId] = Params["Body"].ToString();
                        Session["To" + resultDocId] = Params["To"].ToString();
                        Session["AttachLink" + resultDocId] = Params["AttachLink"].ToString();
                        Session["SendDocument" + resultDocId] = Params["SendDocument"].ToString();
                        Session["MailPathVariable" + resultDocId] = Params["MailPathVariable"].ToString();
                        Session["CC" + resultDocId] = Params["CC"].ToString();
                        Session["CCO" + resultDocId] = Params["CCO"].ToString();
                        resultDocId = null;
                        StringBuilder sb = new StringBuilder();

                        sb.Append("$(document).ready(function() {");
                        sb.Append("Email_Click('" +
                            Params["Subject"].ToString() + "', '" +
                            Params["Body"].ToString() + "', '" +
                            Params["To"].ToString() + "', '" +
                            Params["AttachLink"].ToString() + "', '" +
                            Params["SendDocument"].ToString() + "', '" +
                            Params["NextRuleIds"].ToString() + "', '" +
                            Params["MailPathVariable"].ToString() + "', '" +
                            Params["CC"].ToString() + "', '" +
                            Params["CCO"].ToString() + "', );");
                        sb.Append("});");

                        //if ((bool)Params["SendDocument"])                        
                        //    hdnSendDocument.Value = "true";
                        //else                        
                        //    hdnSendDocument.Value = "false";                        

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "DoMail", sb.ToString(), true);
                        RegisterStartupScript = true;

                        Params = null;
                    }
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
                        WFExec.ExecuteRule(newRuleID, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    }
                    break;
                case RulePendingEvents.ExecuteErrorRule:
                    if (Params != null && Params.Count > 0)
                    {
                        PendingChildRules.Clear();
                        Params.Clear();
                        ExecutedIDs.Clear();
                        Int64 newRuleID;
                        //Ejecuto la nueva regla marcada en la DoExecuteRule
                        if (Params.ContainsKey("ErrorRuleId"))
                        {
                            newRuleID = Int64.Parse(Params["ErrorRuleId"].ToString());
                            WFExec.ExecuteRule(newRuleID, ref results, ref PendigEvent, ref ExecutionResult,
                                ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                        }
                    }
                    break;
                case RulePendingEvents.CancelExecution:
                    if (ExecutedIDs != null)
                        ExecutedIDs.Clear();
                    break;
                case RulePendingEvents.Distribuir:
                    {
                        //SRules RulesS = new SRules();
                        // List<IWFRuleParent> rules = RulesS.GetCompleteHashTableRulesByStep(results[0].StepId);

                        Page.Session.Add("Distribuir" + results[0].TaskId, true);

                        //if (rules != null)
                        //{
                        //    //Ezequiel: Obtengo las id de las reglas de entrada de la etapa a derivar.
                        //    var stepRules = from rule in rules
                        //                    where rule.ParentType == TypesofRules.Entrada && rule.Enable == true
                        //                    select rule.ID;


                        //    if (stepRules.ToList().Count > 0)
                        //    {
                        //        PendingChildRules.AddRange(stepRules);
                        //    }
                        if (Params.ContainsKey("CloseTask") && Convert.ToBoolean(Params["CloseTask"])) {
                            
                                string script = "$(document).ready(function () { window.close()});";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseDoDistribuir", script, true);
                                RegisterStartupScript = true;
                          
                        }
                        

                        if (Params.ContainsKey("CloseTask") && Convert.ToBoolean(Params["CloseTask"]))
                        {

                            string script = "$(document).ready(function () { window.close()});";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseDoDistribuir", script, true);
                            RegisterStartupScript = true;

                        }


                        WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                        // }

                        //23/08/11: Como la grilla es refrescada "on-demand" (solo cuando se selecciona su tab) no es necesaria esta llamada.
                        //string script = "parent.RefreshGrid();";
                        //Page.ClientScript.RegisterStartupScript(typeof(Page), "Message", script, true);
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

                    if ((results.Count == 1) && (Session["Distribuir" + results[0].TaskId] != null))
                    {
                        Session.Remove("Distribuir" + results[0].TaskId);
                        System.Collections.Generic.List<Int64> users = UserGroupBusiness.GetUsersIds(results[0].AsignedToId);

                        //Verifica si debe o no cerrar la tarea.
                        if ((results[0].AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, results[0].StepId) || RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, results[0].StepId) || RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, results[0].StepId))
                        {
                            if (results[0].TaskId == this._taskID)
                                //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                                if (RegisterStartupScript == false)
                                    RefreshTask(results[0], false, ref TaskIdsToRefresh);
                        }
                        else
                        {

                            CloseTask(results[0], ParentAction);
                        }

                        ParentAction = 0;
                        try
                        {
                            if (Params.ContainsKey("ParentAction"))
                                ParentAction = Int32.Parse(Params["ParentAction"].ToString());
                        }
                        catch (Exception)
                        {
                        }


                        if (results[0].TaskId != this._taskID)
                        {
                            if (Session["Distribuir" + this._taskID] != null)
                            {
                                STasks stasks = new STasks();
                                ITaskResult res = stasks.GetTask(_taskID, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                                if (res != null)
                                {
                                    Session.Remove("Distribuir" + res.TaskId);
                                    users = UserGroupBusiness.GetUsersIds(res.AsignedToId);

                                    //Verifica si debe o no cerrar la tarea.
                                    if ((res.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, res.StepId) || RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, results[0].StepId) || RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, results[0].StepId))
                                    {
                                        //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                                        if (RegisterStartupScript == false) RefreshTask(results[0], false, ref TaskIdsToRefresh);
                                    }
                                    else
                                    {
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
                                    }

                                }
                                stasks = null;
                            }
                        }
                    }
                    else
                    {
                        if (TaskIdsToRefresh.Count > 0 && results.Count > 0 && RegisterStartupScript == false) RefreshTask(results[0], true, ref TaskIdsToRefresh);
                    }

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
                        string script = String.Empty;

                        //ZOptBusiness zopt = new ZOptBusiness();

                        //string doctypeidsexc = zopt.GetValue("DTIDShowDocAfterInsert");

                        //zopt = null;
                        //bool opendoc = false;
                        //if (Task != null)
                        //{
                        //    if (!string.IsNullOrEmpty(doctypeidsexc))
                        //    {
                        //        foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        //        {
                        //            if (string.Compare(dtid.Trim(), Task.DocTypeId.ToString()) == 0)
                        //                opendoc = true;
                        //        }
                        //    }
                        //}

                        //if ( opendoc)
                        //{

                        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                        if (Task != null)
                        {
                            if (Page.Session["Entrada" + Task.ID] == null)
                                Page.Session.Add("Entrada" + Task.ID, true);
                            SRights sRights = new SRights();

                            Int32 openMode = 0;
                            if (Params.ContainsKey("OpenMode"))
                            {
                                openMode = Int32.Parse(Params["OpenMode"].ToString());
                            }

                            StringBuilder url = new StringBuilder();
                            url.Append("../WF/TaskSelector.ashx?doctype=");
                            url.Append(Task.DocTypeId);
                            url.Append("&docid=");
                            url.Append(Task.ID);
                            url.Append("&userId=");
                            url.Append(Zamba.Membership.MembershipHelper.CurrentUser.ID);
                            script = string.Format("parent.OpenDocTask3({0},{1},{2},{3},'{4}','{5}',{6},{7},{8});", Task.TaskId, Task.ID, Task.DocTypeId, "false", Task.Name, url, Zamba.Membership.MembershipHelper.CurrentUser.ID, Task.StepId, openMode);
                            script += "parent.SelectTabFromMasterPage('tabtasks');";
                        }
                        // }

                        if (!string.IsNullOrEmpty(script))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
                            RegisterStartupScript = true;
                        }


                        WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    }
                    else
                    {
                        //if (ExecutedIDs != null && ExecutedIDs.Count > 0)
                        //{
                        //    if (ExecutedIDs[ExecutedIDs.Count - 1] < 0)
                        //        ExecutedIDs[ExecutedIDs.Count - 1] = ExecutedIDs[ExecutedIDs.Count - 1] * -1;
                        //}
                        WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    }

                    break;

                case RulePendingEvents.ShowMessage:
                    //  ucrule = (UC_WF_Rules_UCDoScreenMessage)LoadControl("~/Views/UC/WF/Rules/UCDoScreenMessage.ascx");
                    Params["Nuevo Mensaje"].ToString();
                    if (Params.ContainsKey("Nuevo Mensaje"))
                    {
                        String scriptmsg = "$(document).ready( function(){swal('','" + Params["Nuevo Mensaje"].ToString().Replace("\n", "\\n").Replace(System.Environment.NewLine, " ") + "', 'info')";

                        string ActionExecute = string.Empty;
                        if (Params.ContainsKey("Action") && Params["Action"].ToString() != string.Empty)
                        {
                            string action = Params["Action"].ToString();
                            switch (action)
                            {
                                case "Close":
                                    ActionExecute = "window.close();";
                                    break;
                                case "Refresh":
                                    ActionExecute = "window.location.reload();";

                                    break;
                            }
                            scriptmsg = scriptmsg + ".then(function(value){" + ActionExecute + "})";
                        }


                        scriptmsg = scriptmsg + ";});";

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ExecuteMessage", scriptmsg, true);
                        RegisterStartupScript = true;
                    }
                    WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                    ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    break;


                case RulePendingEvents.GoToTabHome:

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "GoToTabHome", "$(document).ready(function(){GoToTabHome();});", true);
                    RegisterStartupScript = true;

                    break;

                case RulePendingEvents.ShowDoInputIndex:
                    ucrule = (UC_WF_Rules_UCDoInputIndex)LoadControl("~/Views/UC/WF/Rules/UCDoInputIndex.ascx");
                    break;

                case RulePendingEvents.ShowDoAsk:
                    ucrule = (UC_WF_Rules_UCDoAsk)LoadControl("~/Views/UC/WF/Rules/UCDoAsk.ascx");
                    break;

                case RulePendingEvents.ShowDoAskDesition:
                    ucrule = (Views_UC_WF_Rules_UCDoAskDesition)LoadControl("~/Views/UC/WF/Rules/UCDoAskDesition.ascx");
                    break;

                case RulePendingEvents.ShowDoRequestData:
                    ucrule = (UC_WF_Rules_UCDoRequestData)LoadControl("~/Views/UC/WF/Rules/UCDoRequestData.ascx");
                    break;

                //08/07/11: Sumado el caso de la DoShowTable, para que carge nuevamente el control.
                case RulePendingEvents.ShowDoShowTable:
                    ucrule = (Views_UC_WF_Rules_UCDoShowTable)LoadControl("~/Views/UC/WF/Rules/UCDoShowTable.ascx");
                    if (Params != null && pnlUcRules != null && Params.ContainsKey("tableTitle"))
                        pnlUcRules.Attributes.Add("title", Params["tableTitle"].ToString());
                    break;

                case RulePendingEvents.OpenUrl:
                    if (Params.ContainsKey("url"))
                    {
                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                        if (Params["OpenMode"] == null)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                            RegisterStartupScript = true;
                        }
                        else
                        {
                            switch ((OpenType)Params["OpenMode"])
                            {
                                case OpenType.NewWindow:
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("OpenWindow('{0}');", Params["url"].ToString()), true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.NewTab:
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.Modal:
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameModal('','" + Params["url"].ToString() + "', 800,600);});", true);
                                    RegisterStartupScript = true;
                                    break;
                                case OpenType.Home:
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameHome('" + Params["url"].ToString() + "', 600);});", true);
                                    RegisterStartupScript = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", "../Tools/Messages.aspx?msg=1"), true);
                        RegisterStartupScript = true;
                    }
                    WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                         ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);

                    break;
                case RulePendingEvents.ExecuteScript:
                    if (Params.ContainsKey("ScriptToExecute"))
                    {
                        //   Session[TaskID + "CurrentExecution"] = null;

                        String script = Params["ScriptToExecute"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ExecuteScript", " " + script + " ", true);
                        RegisterStartupScript = true;
                    }
                    //Params.Clear();
                    //ExecutionResult = RuleExecutionResult.CorrectExecution;
                    //PendigEvent = RulePendingEvents.NoPendingEvent;
                    //PendingChildRules.Clear();

                    WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                 ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    break;
                case RulePendingEvents.EnableRules:
                    if (Params.ContainsKey("UserRules"))
                    {
                        Hashtable UserRules = (Hashtable)Params["UserRules"];
                        List<string> UserRulesList = new List<string>();
                        string EnableScript = string.Empty;
                        foreach (Int64 currentRuleId in UserRules.Keys)
                        {


                            List<Boolean> EnableList = (List<Boolean>)UserRules[currentRuleId];
                            Boolean IsEnabled = (Boolean)EnableList[0];
                            EnableScript = EnableScript + string.Format("EnableDisableRule({0},{1}); ", currentRuleId, IsEnabled).Replace("False", "false").Replace("True", "true");

                            var item = new { currentRuleId, IsEnabled };

                            UserRulesList.Add(JsonConvert.SerializeObject(item));

                        }
                        string jsonData = JsonConvert.SerializeObject(UserRulesList);
                        var executableScript = "sessionStorage.setItem('DisabledRules-" + TaskID + "','" + jsonData + "');";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "EnableRuleScript", "$(document).ready(function(){" + EnableScript + "}); " + executableScript, true);
                        RegisterStartupScript = true;
                    }
                    WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                        ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);
                    break;
                case RulePendingEvents.DoShowForm:
                    if (Params.ContainsKey("formid"))
                    {
                        var taskid = results[0].TaskId;
                        var formid = Convert.ToInt32(Params["formid"]);
                        var openmode = Params["openmode"];

                        if (formid != 0)
                        {
                            if (openmode == "modal")
                            {
                                Page.Session.Add("ModalFormID" + results[0].ID, Params["formid"]);
                                Page.Session.Add("CurrentFormID" + results[0].ID, results[0].CurrentFormID);
                            }
                            else
                            {
                              
                                Page.Session.Add("CurrentFormID" + results[0].ID, Params["formid"]);
                            }
                            List<Int64> refreshTasks = new List<Int64>() { results[0].TaskId };
                            //RefreshTask(results[0], false, ref refreshTasks);

                            if (openmode == "modal")
                                OpenFormInWindow(results[0]);
                        }
                    }
                    WFExec.ExecuteRule(RuleId, ref results, ref PendigEvent, ref ExecutionResult,
                        ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, TaskIdsToRefresh, false);



                    break;
                case RulePendingEvents.RefreshWfTree:
                    RefreshWFTree();
                    break;
                case RulePendingEvents.ShowBinary:
                    if (Params.ContainsKey("BinaryFile") && Params.ContainsKey("BinaryMime"))
                    {
                        //Se guardan los datos en la sesion y se quitan del objeto Params.
                        string id = new Random().Next().ToString();
                        Session["BinaryFile" + id] = Params["BinaryFile"];
                        Session["BinaryMime" + id] = Params["BinaryMime"];
                        Params.Remove("BinaryFile");
                        Params.Remove("BinaryMime");

                        //Se abre la ventana con el ashx que mostrará el archivo.
                        string script = MembershipHelper.Protocol + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + "/Services/GetBinary.ashx?id=" + id;
                        script = "$(document).ready(function() {parent.OpenWindow('" + script + "');});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowBinary", script, true);
                        RegisterStartupScript = true;
                    }

                    break;
                case RulePendingEvents.LookingForFileToPrint:
                    if (Params.ContainsKey("BarcodeToPrintPath"))
                    {
                        string path = Params["BarcodeToPrintPath"].ToString().Replace(@"\", @"\\");
                        string CopiesCount = Params["CopiesCount"].ToString();
                        string Width = Params["Width"].ToString();
                        string Height = Params["Height"].ToString();

                        string func = @"$(document).ready(function(){PrintBarcode('" + path + "', " + CopiesCount + ", " + Width + ", " + Height + ");});";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintBarcode", func, true);
                        RegisterStartupScript = true;
                    }
                    break;
            }

            if (PendigEvent != RulePendingEvents.ExecuteRule && PendigEvent != RulePendingEvents.ShowDoAsk && PendigEvent != RulePendingEvents.ShowDoAskDesition && PendigEvent != RulePendingEvents.ShowDoInputIndex && PendigEvent != RulePendingEvents.ShowDoRequestData && PendigEvent != RulePendingEvents.ShowDoShowTable && PendigEvent != RulePendingEvents.ShowMessage && PendigEvent != RulePendingEvents.ShowDoAsociatedForm)
            {
                this.pnlUcRules.Controls.Clear();

                try
                {
                    if (Session["ExecutingOpenRules"] == null || Session["ExecutingOpenRules"].ToString() == "false")
                    {
                        string script = "$(document).ready(function(){ RefreshParentDataFromChildWindow()});";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "RefreshParentDataFromChildWindow", script, true);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            if (ucrule != null)//Si se cargo el uc de regla cargo sus atributos.
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ucrule != null");

                Session[this.TaskID + "CurrentExecution"] = PendigEvent;
                this.pnlUcRules.Controls.Clear();
                this.pnlUcRules.Controls.Add(ucrule);
                ucrule.ID = PendigEvent.ToString();

                IRule iucrule = (IRule)ucrule;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, $"TaskId: {this.TaskID.ToString()}");

                if (this.TaskID <= 0 && results != null && results.Count > 0 && results[0].TaskId > 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, $"results.Count: {results.Count.ToString()}");

                    this.TaskID = results[0].TaskId;
                    this.hdnCurrTaskID.Value = this.TaskID.ToString();

                }
                iucrule.TaskID = this.TaskID;
                iucrule.ContinueExecution += new WFExecution._continueExecution(WFExec.ExecuteRule);


                if (firstload)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, $"First Load");
                    iucrule.NonVisibleTaskWithGuiRules = false;
                    iucrule.ExecutionResult = ExecutionResult;
                    iucrule.PendingChildRules = PendingChildRules;
                    iucrule.ExecutedIDs = ExecutedIDs;
                    iucrule.PendigEvent = PendigEvent;
                    iucrule.results = results;
                    iucrule.Params = Params;
                    iucrule.RuleID = RuleId;

                    //Se guarda el nombre de la regla para poder cerrar los dialogos.
                    RuleButtonOk = "ctl00_ContentPlaceHolder_UC_WFExecution_" + PendigEvent.ToString() + "__btnok";
                    showJqueryDialog();
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, $"Second Load");

                    if (iucrule.Params.ContainsKey("selectedChecks" + iucrule.RuleID.ToString()))
                        iucrule.Params["selectedChecks" + iucrule.RuleID.ToString()] = hdnChecks.Value;
                    else
                    {
                        iucrule.Params.Add("selectedChecks" + iucrule.RuleID.ToString(), hdnChecks.Value);

                    }
                }

                hdnChecks.Value = string.Empty;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, $"LoadOptions");
                iucrule.LoadOptions();
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, $"UnLoad");

                if (PendigEvent != RulePendingEvents.ExecuteRule && firstload == false)
                    this.pnlUcRules.Controls.Clear();
                return;
                //UnblockWebPageInteraction();
            }

            try
            {

                if (System.Web.HttpContext.Current.Session["VariablesInterReglas"] != null)
                {
                    string func = @"$(document).ready(function(){";
                    foreach (string variable in VariablesInterReglas.Keys())
                    {
                        try
                        {

                            string variablevalue = VariablesInterReglas.get_Item(variable).ToString();
                            func += string.Format(@"$('<input>').attr({ type: 'hidden',    id: 'zvar_{0}',    name: 'zvar_{0}',value:'{1}'}).appendTo('form');", variable, variablevalue);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    func += @";DisableRulesByTaskId(" + TaskID + ");});";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CreateVariables", func, true);
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            ShowScreenMessage(ex);
        }

    }

    protected void CloseTask(ITaskResult result, Int32 ParentAction)
    {
        STasks sTasks = new STasks();
        if (new RightsBusiness().GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(result.StepId)) && Convert.ToBoolean(UP.getValue("CheckFinishTaskAfterClose", UPSections.WorkFlow, "True", MembershipHelper.CurrentUser.ID)))
        {
            sTasks.Finalizar(result);
        }

        sTasks = null;

        if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseTask"))
        {
            if (ParentAction == 0)
            {
                string js = "$(document).ready(function(){CloseTaskFromRule(" + result.TaskId.ToString() + ");});";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
                RegisterStartupScript = true;
            }
            else if (ParentAction == 1)
            {
                string js = "$(document).ready(function(){ });";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
                RegisterStartupScript = true;
            }
            else
            {
                string js = "$(document).ready(function(){CloseTaskFromRule(" + result.TaskId.ToString() + ");});";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseTask", js, true);
                RegisterStartupScript = true;
            }
        }
    }


    //Muestra dialogo jquery con el UC de las reglas.
    //private void showJqueryDialog()
    //{
    //    if (!Page.ClientScript.IsClientScriptBlockRegistered("RuleDialog"))
    //    {
    //        var script = "$(document).ready(function() {addpnlUcRulesToSeccion();});";
    //        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "RuleDialog", script, true);
    //    }
    //}



    private void showJqueryDialog()
    {
        if (!Page.ClientScript.IsClientScriptBlockRegistered("RuleDialog"))
        {
            //        string script = "$(document).ready(function() { " +
            //"parent.BlockWebPageInteraction();" +
            //"var topDialog = window.screen.height / 2 - $('#ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules').height() / 2;" +
            //"$('#ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules').dialog({" +
            //"closeOnEscape:false,autoOpen:true,modal:true,top:topDialog,width:600,resizable:false});" +
            //"var pnlRulesParent = $('#ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules').parent();" +
            //"pnlRulesParent.css('position', 'absolute');" +
            //"var zIndexMax = getMaxZ();" +
            //"$(pnlRulesParent).css({ 'z-index': Math.round(zIndexMax) });" +
            //"$('.ui-widget-overlay').appendTo($('form:first'));" +
            //"pnlRulesParent.appendTo($('form:first'));" +
            //"$('.ui-dialog-titlebar').hide();" +
            //"});";
            //   var script = "$(document).ready(function() {ShowDivModal('',$('#ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules'),'800','600');});";
            var script = "$(document).ready(function() { $('#openModalIFUcRules').modal();DisableRulesByTaskId(" + TaskID + ");});";
            ZTrace.WriteLineIf(ZTrace.IsVerbose, $"rule Script: {script}");

            Page.ClientScript.RegisterStartupScript(this.GetType(), "RuleDialog", script, true);
            RegisterStartupScript = true;
        }
    }



    //private void UnblockWebPageInteraction()
    //{
    //    string script = "$(document).ready(function() {$('#ContentPlaceHolder_UC_WFExecution_pnlUcRules');});";
    //    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "UnblockWebPageInteraction", script, true);   
    //}

    private void ShowScreenMessage(string msj)
    {
        if (!Page.ClientScript.IsClientScriptBlockRegistered("ScreenMessage"))
        {
            string script = "<script>$(document).ready(function() { swal('','" + msj + "','error'); });</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ScreenMessage", script);
            RegisterStartupScript = true;
        }
    }

    private void ShowScreenMessage(Exception ex)
    {
        Exception LastEx = GetInnerException(ex);

        if (!Page.ClientScript.IsClientScriptBlockRegistered("ScreenMessage"))
        {
            string script = "<script>$(document).ready(function() { swal('Ocurrio un error','" + ex.Message.Replace("\r\n", "") + " " + LastEx.Message.Replace("\r\n", "") + "','error'); });</script>";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ScreenMessage", script);
            RegisterStartupScript = true;
        }
    }

    private Exception GetInnerException(Exception ex)
    {
        if (ex.InnerException != null)
            return GetInnerException(ex.InnerException);
        else
            return ex;
    }

}

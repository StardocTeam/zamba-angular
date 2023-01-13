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

public partial class UC_WF_UCWFExecution : System.Web.UI.UserControl
{
    private long _taskID;
    private WFExecution _wfExec;
    public delegate void _refreshTask(ITaskResult task, Boolean DoPostBack, ref List<long> TaskIDsToRefresh);
    public delegate void _refreshWFTree();
    public event _refreshTask RefreshTask;
    public event _refreshWFTree RefreshWFTree;

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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            Session[TaskID+ "CurrentExecution"] = null;

        //Actualiza el timeout
        if (Page.IsPostBack == false && Session["User"] != null)
        {
            try
            {
                IUser user = (IUser)Session["User"];
                Int32 type = 0;

                if (user.WFLic)
                {
                    type = 1;
                }
                else
                {
                    SUserPreferences SUserPreferences = new SUserPreferences();
                    Ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), 1);
                    SUserPreferences = null;
                    Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                    ((IUser)Session["User"]).WFLic = true;
                    type = 1;
                }

                SRights rights = new SRights();
                if (user.ConnectionId > 0)
                {
                    SUserPreferences SUserPreferences = new SUserPreferences();
                    rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                    SUserPreferences = null;
                }
                else
                    Response.Redirect("~/Views/Security/LogIn.aspx");

                rights = null;
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }


        //Si es postback y hay un evento pendiente vuelvo a regenerar el control.
        if (Page.IsPostBack && Session[TaskID+ "CurrentExecution"] != null)
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
        ref  Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        if (ExecutionResult == RuleExecutionResult.FailedExecution)
        {
            ShowScreenMessage("Ha ocurrido un error en la regla");
            Session["ExecutingRule"] = null;
        }
        else if (ExecutionResult == RuleExecutionResult.CorrectExecution)
        {
            Session["ExecutingRule"] = null;
        }
        else
        {
            LoadUCRule(true, RuleId, results, PendigEvent, ExecutionResult, ExecutedIDs,
                Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
        }
    }

    private void LoadUCRule(bool firstload, long RuleId, List<ITaskResult> results, 
        RulePendingEvents PendigEvent, RuleExecutionResult ExecutionResult, 
        List<Int64> ExecutedIDs, Hashtable Params, List<Int64> PendingChildRules,
        ref  Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        //Guardo en sesion la regla que se esta ejeucutando
        Control ucrule = null;
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
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DoExportHTMLToPDF", script, true);
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
                        _taskID + "','" + Params["ContinueWithCurrentTasks"] + "','"  + Params["DontOpenTaskAfterInsert"] +
                        "','" + Params["FillCommonAttributes"] + "','" + Params["HaveSpecificAttributes"] + "');";
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddAsociatedFormScript", script, true);
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

                    //"instalamos" el javascript cuando refresca la página.
                    string script = "$(document).ready(function () { $('#IFDialogContent').unbind('load'); parent.ShowInsertAsociated('" + url.ToString() + "'); });";
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "DoAddAsociatedDocumentScript", script, true);
                }
                break;
            //04/07/11: Sumada la opcion del la regla DoOpenTask
            case RulePendingEvents.OpenTask:
                if (Params["DocID"] != null)
                {
                    //construimos la url
                    StringBuilder url = new StringBuilder();
                    url.Append("../WF/TaskSelector.ashx?doctype=");
                    url.Append(Params["DocTypeId"]);
                    url.Append("&docid=");
                    url.Append(Params["DocID"]);

                    IResult res = new SResult().GetResult((long)Params["DocID"], (long)Params["DocTypeId"]);

                    string script = string.Format("parent.OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6});", 0, res.ID, res.DocTypeId, "false", res.Name, url, Session["UserId"]);
                    script += "parent.SelectTabFromMasterPage('tabtasks');";
                    ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"openTask",script,true);
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
                        results.Add(stasks.GetTask(TaskID));
                    }

                    if (TaskIdsToRefresh != null)
                    {
                        TaskIdsToRefresh.Add((long)Params["TaskIDToRefresh"]);
                    }

                    WFExec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult, 
                        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                }
                break;

            case RulePendingEvents.CloseTask:
                if (Params.ContainsKey("TaskID"))
                {
                    STasks st = new STasks();
                    long taskId = Int64.Parse(Params["TaskID"].ToString());
                    ITaskResult rs = null;

                    if (results != null && results.Count > 0 && results[0] != null)
                        if (taskId > 0 && taskId != results[0].TaskId)
                            rs = st.GetTask(taskId);
                        else
                            rs = results[0];
                    else if (taskId > 0)
                        rs = st.GetTask(taskId);

                    if (rs != null)
                        CloseTask(rs);

                    st = null;
                    rs = null;
                }
                else
                {
                    if (results != null && results.Count > 0)
                        CloseTask(results[0]);
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
                    Session["CC" + resultDocId] = Params["CC"].ToString();
                    Session["CCO" + resultDocId] = Params["CCO"].ToString();
                    Params = null;
                    resultDocId = null;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("$(document).ready(function() {");
                    sb.Append("Email_Click();");
                    sb.Append("});");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DoMail", sb.ToString(), true);
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
                    WFExec.ExecuteRule(newRuleID, ref results, PendigEvent, ExecutionResult, 
                        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                }
                break;
            case RulePendingEvents.ExecuteErrorRule:
                if (Params != null && Params.Count > 0)
                {
                    PendingChildRules.Clear();

                    //Ejecuto la nueva regla marcada en la DoExecuteRule
                    Int64 newRuleID = Int64.Parse(Params["RuleId"].ToString());
                    Params.Clear();
                    ExecutedIDs.Clear();
                    WFExec.ExecuteRule(newRuleID, ref results, PendigEvent, ExecutionResult, 
                        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                }
                break;
            case RulePendingEvents.CancelExecution:
                if (ExecutedIDs != null)
                    ExecutedIDs.Clear();
                break;
            case RulePendingEvents.Distribuir:
                {
                    SRules RulesS = new SRules();
                    List<IWFRuleParent> rules = RulesS.GetCompleteHashTableRulesByStep(results[0].StepId);

                    Page.Session.Add("Distribuir" + results[0].TaskId, true);

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

                        WFExec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult,
                            ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                    }

                    //23/08/11: Como la grilla es refrescada "on-demand" (solo cuando se selecciona su tab) no es necesaria esta llamada.
                    //string script = "parent.RefreshGrid();";
                    //Page.ClientScript.RegisterStartupScript(typeof(Page), "Message", script, true);
                    break;
                }
            case RulePendingEvents.ResponseToDelete:
                CloseTask(results[0]);
                break;
            case RulePendingEvents.ValidateDistribute:
                if ((results.Count == 1) && (Session["Distribuir" + results[0].TaskId] != null))
                {
                    Session.Remove("Distribuir" + results[0].TaskId);
                    System.Collections.Generic.List<Int64> users = UserGroupBusiness.GetUsersIds(results[0].AsignedToId, true);

                    //Verifica si debe o no cerrar la tarea.
                    if ((results[0].AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.Use, results[0].StepId) || new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, results[0].StepId) || new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, results[0].StepId))
                    {
                        if (results[0].TaskId == this._taskID)
                            //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                            RefreshTask(results[0], false, ref TaskIdsToRefresh);
                    }
                    else
                    {
                        CloseTask(results[0]);
                    }

                    if (results[0].TaskId != this._taskID)
                    {
                        if (Session["Distribuir" + this._taskID] != null)
                        {
                            STasks stasks = new STasks();
                            ITaskResult res = stasks.GetTask(_taskID);
                            if (res != null)
                            {
                                Session.Remove("Distribuir" + res.TaskId);
                                users = UserGroupBusiness.GetUsersIds(res.AsignedToId, true);

                                //Verifica si debe o no cerrar la tarea.
                                if ((res.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.Use, res.StepId) || new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.VerAsignadosAOtros, results[0].StepId) || new SRights().GetUserRights(ObjectTypes.WFSteps, RightsType.VerAsignadosANadie, results[0].StepId))
                                {
                                    //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                                    RefreshTask(results[0], false, ref TaskIdsToRefresh);
                                }
                                else
                                {
                                    CloseTask(results[0]);
                                }
                            }
                            stasks = null;
                        }
                    }
                }
                else
                {
                    if (RefreshRule) RefreshTask(results[0], true, ref TaskIdsToRefresh);
                }

                break;
            case RulePendingEvents.ExecuteEntryInWFDoGenerateTaskResult:
                if (Params != null && Params.Count > 0)
                {
                    if (Params.ContainsKey("ResultID") && Int64.Parse(Params["ResultID"].ToString()) > 0)
                    {
                        Int64 newresultID = Int64.Parse(Params["ResultID"].ToString());
                        //Codigo de ejecucion de reglas de entrada de la nueva tarea
                        STasks stasks = new STasks();
                        ITaskResult Task = stasks.GetTaskByDocId(newresultID);

                        //Ejecutar javascript que abre la pagina
                        //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                        string script = String.Empty;

                        ZOptBusiness zopt = new ZOptBusiness();

                        string doctypeidsexc = zopt.GetValue("DTIDShowDocAfterInsert");

                        zopt = null;
                        bool opendoc = false;
                        if (Task != null)
                        {
                            if (!string.IsNullOrEmpty(doctypeidsexc))
                            {
                                foreach (string dtid in doctypeidsexc.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    if (string.Compare(dtid.Trim(), Task.DocTypeId.ToString()) == 0)
                                        opendoc = true;
                                }
                            }
                        }

                        SUserPreferences SUserPreferences = new SUserPreferences();
                        if (bool.Parse(SUserPreferences.getValue("ShowDocAfterInsert", Sections.InsertPreferences, "true")) || opendoc)
                        {

                            //Realiza la apertura del documento dependiendo de si tiene tareas o permisos.
                            if (Task != null)
                            {
                                Page.Session.Add("Entrada" + Task.ID, true);
                                SRights sRights = new SRights();

                                StringBuilder url = new StringBuilder();
                                url.Append("../WF/TaskSelector.ashx?doctype=");
                                url.Append(Task.DocTypeId);
                                url.Append("&docid=");
                                url.Append(Task.ID);

                                script = string.Format("parent.OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6});", Task.TaskId, 0, Task.DocTypeId, "false", Task.Name, url, Session["UserId"]);
                                script += "parent.SelectTabFromMasterPage('tabtasks');";
                            }
                        }

                        if (!string.IsNullOrEmpty(script))
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
                    }

                    WFExec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult, 
                        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                }
                else
                {
                    if (ExecutedIDs != null && ExecutedIDs.Count > 0)
                    {
                        if (ExecutedIDs[ExecutedIDs.Count - 1] < 0)
                            ExecutedIDs[ExecutedIDs.Count - 1] = ExecutedIDs[ExecutedIDs.Count - 1] * -1;
                    }
                    WFExec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult,
                        ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
                }

                break;

            case RulePendingEvents.ShowMessage:
                ucrule = (UC_WF_Rules_UCDoScreenMessage)LoadControl("~/Views/UC/WF/Rules/UCDoScreenMessage.ascx");
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
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                      switch ((OpenType)Params["openmode"])
                {
                    case OpenType.NewTab:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                        break;
                    case OpenType.Modal:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameModal('','" + Params["url"].ToString() + "', 800,600);});", true);
                        break;
                    case OpenType.Home:
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameHome('" + Params["url"].ToString() + "', 600);});", true);
                        break;
                }
                    else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", "../Tools/Messages.aspx?msg=1"), true);

                WFExec.ExecuteRule(RuleId, ref results, PendigEvent, ExecutionResult,
    ExecutedIDs, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);

                break;

            case RulePendingEvents.DoShowForm:
                if (Params.ContainsKey("formid"))
                {
                    if ((Page.Session["CurrentFormID" + results[0].ID] != null) && (Page.Session["CurrentFormID" + results[0].ID].ToString() != Params["formid"].ToString()))
                    {
                        Page.Session.Add("CurrentFormID" + results[0].ID, Params["formid"]);
                        List<Int64> refreshTasks = new List<Int64>() { results[0].TaskId };
                        RefreshTask(results[0], false, ref refreshTasks);
                    }
                }
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
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowBinary", script, true);
                    
                }

                break;
        }
        //Si se cargo el uc de regla cargo sus atributos.
        if (ucrule != null)
        {
            Session[this.TaskID+"CurrentExecution"] = PendigEvent;
            this.pnlUcRules.Controls.Clear();
            this.pnlUcRules.Controls.Add(ucrule);
            ucrule.ID = PendigEvent.ToString();

            IRule iucrule = (IRule)ucrule;
            iucrule.TaskID = this.TaskID;
            iucrule.ContinueExecution += new WFExecution._continueExecution(WFExec.ExecuteRule);


            if (firstload)
            {
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
                if (iucrule.Params.ContainsKey("selectedChecks" + iucrule.RuleID.ToString()))
                    iucrule.Params["selectedChecks" + iucrule.RuleID.ToString()] = hdnChecks.Value;
                else
                    iucrule.Params.Add("selectedChecks" + iucrule.RuleID.ToString(), hdnChecks.Value);
            }
            hdnChecks.Value = string.Empty;
            iucrule.LoadOptions();
        }
        else
        {
            UnblockWebPageInteraction();
        }
    }

    protected void CloseTask(ITaskResult result)
    {
        STasks sTasks = new STasks();
        sTasks.Finalizar(result);
        sTasks = null;

        if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseTask"))
        {
            string js = "$(document).ready(function(){parent.CloseTask(" + result.TaskId.ToString() + ", false);});";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "CloseTask", js, true);
        }
    }


    //Muestra dialogo jquery con el UC de las reglas.
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
            var script = "$(document).ready(function() { $('#openModalIFUcRules').modal();});";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "RuleDialog", script, true);
        }
    }

    private void UnblockWebPageInteraction()
    {
        if (!Page.ClientScript.IsClientScriptBlockRegistered("UnblockWebPageInteraction"))
        {
            string script = "$(document).ready(function() {parent.UnblockWebPageInteraction();});";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "UnblockWebPageInteraction", script, true);
        }
    }

    private void ShowScreenMessage(string msj)
    {
        if (!Page.ClientScript.IsClientScriptBlockRegistered("ScreenMessage"))
        {
            string script = "<script>$(document).ready(function() { alert('" + msj + "') });</script>";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ScreenMessage", script);
        }
    }
}

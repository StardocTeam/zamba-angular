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
using Zamba.Membership;

public partial class Views_WF_WFExecutionForEntryRules : System.Web.UI.Page
{
    #region "Attributes"
    private long _currTaskID;
    private WFExecution WFExec;
    private ITaskResult taskResult;

    List<long> PendingChildRules = new List<long>();
    #endregion

    #region "Properties"
    public long CurrTaskID
    {
        get
        {
            if (_currTaskID == 0)
                if (!Int64.TryParse(hdnCurrTaskID.Value, out _currTaskID))
                    _currTaskID = ListOfTask[0].ExecutionTask.TaskId;

            return this._currTaskID;
        }
        set
        {
            this._currTaskID = value;
            hdnCurrTaskID.Value = this._currTaskID.ToString();
        }
    }

    public string CurrTaskName
    {
        set
        {
            //lblCurrTaskName.Text = value;
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

    public List<IExecutionRequest> ListOfTask
    {
        get
        {
            return (List<IExecutionRequest>)Session["ListOfTask"];
        }
        set
        {
            Session["ListOfTask"] = value;
        }
    }

    public string RuleButtonOk
    {
        get { return hdnRuleName.Value.ToString(); }
        set { hdnRuleName.Value = value; }
    }

    /// <summary>
    /// Almacena las reglas de entrada que quedan pendientes de la tarea almacenada en la posicion 1
    /// (las que son hijas del nodo "Entrada")
    /// </summary>
    public List<long[]> PendingEntryRulesAndTasks
    {
        get
        {
            return (List<long[]>)Session[this.CurrTaskID + "PendigEntryRules"];
        }
        set
        {
            Session[this.CurrTaskID + "PendigEntryRules"] = value;
        }
    }
    #endregion

    #region "Page Events"
    protected void Page_PreLoad(object sender, EventArgs e)
    {
        this.WFExec = new WFExecution((IUser)Session["User"]);
        this.WFExec._haceralgoEvent += HandleWFExecutionPendingEvents;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Si no es postback es porque hay que iniciar una nueva cadena de reglas
        if (!Page.IsPostBack)
        {
            Session["EntryRulesExecution"] = null;
            if (ListOfTask != null && ListOfTask.Count > 0)
            {
                hdnMustHideLoading.Value = "1";
                BeginExecution();
            }
        }
        else
        {
            //Si es postback y hay un evento pendiente vuelvo a regenerar el control.
            if (Session["EntryRulesExecution"] != null)
            {
                LoadUCRuleFromSession();
            }
            else
            {
                if (ListOfTask != null)
                {
                    //Si quedan tareas se continua con su ejecución, sino se cierra el contenedor de reglas
                    if (ListOfTask.Count > 0)
                    {
                        hdnMustHideLoading.Value = "1";
                        BeginExecution();
                    }
                    else
                    {
                        Close();
                    }
                }
            }
        }
    }
    #endregion

    #region "Methods"

    /// <summary>
    /// Obtiene todos los datos de la sesión para cargar la regla pendiente asi puede continuar con su ejecución
    /// </summary>
    private void LoadUCRuleFromSession()
    {
        //Se obtienen todas las variables de sesión para poder continuar la ejecucion
        string taskId = CurrTaskID.ToString();
        Boolean RefreshRule = false;
        RulePendingEvents uctoexecute = (RulePendingEvents)Session["EntryRulesExecution"];
        List<Int64> ExecutedIDs = (List<Int64>)Session[taskId + "ExecutedIDs"];
        Int64 ruleId = (long)Session[taskId + "RuleID"];
        if (ruleId < 0) ruleId *= -1;
        List<ITaskResult> results;
        if (Session[taskId + "results"] != null)
            results = (List<ITaskResult>)Session[taskId + "results"];
        else
            results = (from p in ((List<IExecutionRequest>)Session["ListOfTask"])
                          select p.ExecutionTask).ToList();
        Hashtable Params = (Hashtable)Session[taskId + "Params"];
        List<Int64> pendingChildRules = (List<Int64>)Session[taskId + "PendingChildRules"];

        //Se carga la GUI de la regla
        LoadUCRule(ruleId, results, uctoexecute, RuleExecutionResult.PendingEventExecution, 
            ExecutedIDs, Params, pendingChildRules, ref RefreshRule, TaskIdsToRefresh);
    }

    /// <summary>
    /// Carga la interfaz grafica de una regla con todos sus valores y configuraciones
    /// </summary>
    /// <param name="firstload"></param>
    /// <param name="RuleId"></param>
    /// <param name="results"></param>
    /// <param name="PendigEvent"></param>
    /// <param name="ExecutionResult"></param>
    /// <param name="ExecutedIDs"></param>
    /// <param name="Params"></param>
    /// <param name="PendingChildRules"></param>
    /// <param name="RefreshRule"></param>
    private void LoadUCRule(long RuleId, List<ITaskResult> results,
        RulePendingEvents PendigEvent, RuleExecutionResult ExecutionResult,
        List<Int64> ExecutedIDs, Hashtable Params, List<Int64> PendingChildRules,
        ref  Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        if (ExecutedIDs == null && Session[CurrTaskID + "ExecutedIDs"] != null)
            ExecutedIDs = (List<Int64>)Session[CurrTaskID + "ExecutedIDs"];

        if (ExecutedIDs.Count > 0) RuleId = ExecutedIDs[ExecutedIDs.Count - 1];
        if (RuleId < 0) RuleId *= -1;

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
                    string url = "../UC/WF/Rules/DoExportToPDF.aspx?Content=" + newRan.ToString();
                    if (Params["ReturnFileName"] != null)
                    {
                        url += "&ReturnFileName=" + Params["ReturnFileName"].ToString();
                    }
                    if (Params["CanEditable"] != null)
                    {
                        url += "&CanEditable=" + Params["CanEditable"].ToString();
                    }

                    string script = "$(document).ready(function() { ShowIFrameModal('Exportar a PDF', '" + url + "', 600, 400)});";
                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DoExportHTMLToPDF", script, true);
                }
                break;
            case RulePendingEvents.ShowDoAsociatedForm:
                if (Params["FormID"] != null)
                {
                    if ((bool)Params["HaveSpecificAttributes"])
                    {
                        Session["SpecificAttrubutes" + CurrTaskID.ToString()] = Params["SpecificAttrubutes"];
                    }

                    string script = "AsociateForm('" + Params["FormID"] + "','" + Params["DocID"] + "','" + Params["DocTypeId"] + "','" +
                        CurrTaskID + "','" + Params["ContinueWithCurrentTasks"] + "','" + Params["DontOpenTaskAfterInsert"] +
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
                    url.Append(CurrTaskID.ToString());


                    //"instalamos" el javascript cuando refresca la página.
                    //string script = "var mywindow = window.open('" + url.ToString() + "','Regla_AsociarNuevoDocumento','width=620,height=580,directories=no,status=no,menubar=no,toolbar=no,location=no,resizable=no,toolbar=no');";
                    string script = "$('#IFDialogContent').unbind('load'); parent.ShowInsertAsociated('" + url.ToString() + "');";
                    Page.ClientScript.RegisterStartupScript(typeof(Page), "DoAddAsociatedDocumentScript", script, true);
                }

                //if (Params["DocID"] != null)
                //    Session.Add("Insert_DocTypeId",Params["DocTypeId"]);
                //ucrule = (UC_WF_Rules_UCDoAddAsociatedDocument)LoadControl("~/Views/UC/WF/Rules/UCDoAddAsociatedDocument.ascx");
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

                    Zamba.Core.IResult res = new SResult().GetResult((long)Params["DocID"], (long)Params["DocTypeId"]);
                    
                    string script = string.Format("parent.OpenDocTask2({0},{1},{2},{3},'{4}','{5}',{6});", 0, res.ID, res.DocTypeId, "false", res.Name, url, Session["UserId"]);
                    script += "parent.SelectTabFromMasterPage('tabtasks');";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openTask", script, true);
                }
                break;
            case RulePendingEvents.RuleRefreshTask:
                if (Params["TaskIDToRefresh"] != null && (long)Params["TaskIDToRefresh"] > 0)// &&
                {
                    Session[CurrTaskID +"CurrentExecution"] = null;

                    if (results == null)
                    {
                        results = new List<ITaskResult>();
                        STasks stasks = new STasks();
                        results.Add(stasks.GetTask(this.CurrTaskID));
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

                    //Recupero los results reales. Existe un problema al agregar una DoGetDocAsoc y luego una DoExecuteRule, y
                    //lo que esta pasando es que al terminar la PlayWeb de la DoExecuteRule y pasar por el fin de la ejecución
                    //de la DoGetDocAsoc, esta pisando los results obtenidos con los anteriores. Por eso al ejecutar esta regla
                    //se estaba ejecutando con el listado de results viejos y no los obtenidos por la DoGetDocAsoc.
                    //results = (List<ITaskResult>)Params["TasksToExecute"];

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
                    //Recupero los results reales. Existe un problema al agregar una DoGetDocAsoc y luego una DoExecuteRule, y
                    //lo que esta pasando es que al terminar la PlayWeb de la DoExecuteRule y pasar por el fin de la ejecución
                    //de la DoGetDocAsoc, esta pisando los results obtenidos con los anteriores. Por eso al ejecutar esta regla
                    //se estaba ejecutando con el listado de results viejos y no los obtenidos por la DoGetDocAsoc.
                    results = (List<ITaskResult>)Params["TasksToExecute"];

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
            case RulePendingEvents.ValidateDistribute:
                if ((results.Count == 1) && (Session["Distribuir" + results[0].TaskId] != null))
                {
                    Session.Remove("Distribuir" + results[0].TaskId);
                    System.Collections.Generic.List<Int64> users = UserGroupBusiness.GetUsersIds(results[0].AsignedToId, true);

                    //Verifica si debe o no cerrar la tarea.
                    if ((results[0].AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && new SRights().GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Use, results[0].StepId))
                    {
                        if (results[0].TaskId == CurrTaskID)
                            //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                            RefreshTask(results[0], false, ref TaskIdsToRefresh);
                    }
                    else
                    {
                        RefreshTask(results[0], false, ref TaskIdsToRefresh);
                        CloseTask(results[0]);
                    }

                    if (results[0].TaskId != CurrTaskID)
                    {
                        if (Session["Distribuir" + CurrTaskID] != null)
                        {
                            STasks stasks = new STasks();
                            Zamba.Core.ITaskResult res = stasks.GetTask(CurrTaskID);
                            if (res != null)
                            {
                                Session.Remove("Distribuir" + res.TaskId);
                                users = UserGroupBusiness.GetUsersIds(res.AsignedToId, true);

                                //Verifica si debe o no cerrar la tarea.
                                if ((res.AsignedToId == Zamba.Membership.MembershipHelper.CurrentUser.ID) || (users.Contains(Zamba.Membership.MembershipHelper.CurrentUser.ID)) && new SRights().GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Use, res.StepId))
                                {
                                    //Se refresca la tarea completa incluyendo formularios, estados, acciones de usuario, etc.
                                    RefreshTask(results[0], false, ref TaskIdsToRefresh);
                                }
                                else
                                {
                                    RefreshTask(results[0], false, ref TaskIdsToRefresh); 
                                    CloseTask(results[0]);
                                }
                            }
                            stasks = null;
                        }
                        //Verifico si quedan tareas pendientes de ejecucion
                        else if (ListOfTask != null)
                        {
                            //Si quedan tareas se continua con su ejecución, sino se cierra el contenedor de reglas
                            if (ListOfTask.Count > 0)
                            {
                                ListOfTask.RemoveAt(0);
                                if (ListOfTask.Count > 0)
                                {
                                    hdnMustHideLoading.Value = "1";
                                    BeginExecution();
                                }
                                else
                                    RefreshTask(results[0], true, ref TaskIdsToRefresh);
                            }
                            else
                            {
                                RefreshTask(results[0], true, ref TaskIdsToRefresh);
                            }
                        }
                        else
                            RefreshTask(results[0], true, ref TaskIdsToRefresh);

                    }
                }
                else
                {
                    //Verifico si quedan tareas pendientes de ejecucion
                    if (ListOfTask != null)
                    {
                        //Si quedan tareas se continua con su ejecución, sino se cierra el contenedor de reglas
                        if (ListOfTask.Count > 0)
                        {
                            ListOfTask.RemoveAt(0);
                            if (ListOfTask.Count > 0)
                            {
                                hdnMustHideLoading.Value = "1";
                                BeginExecution();
                            }
                            else
                                RefreshTask(results[0], true, ref TaskIdsToRefresh);
                        }
                        else
                        {
                            RefreshTask(results[0], true, ref TaskIdsToRefresh);
                        }
                    }
                    else
                        RefreshTask(results[0], true, ref TaskIdsToRefresh);
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
                        Zamba.Core.ITaskResult Task = stasks.GetTaskByDocId(newresultID);

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
                        if (bool.Parse(SUserPreferences.getValue("ShowDocAfterInsert", Zamba.Core.Sections.InsertPreferences, "true")) || opendoc)
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
                        else
                        {
                            if (Session["ListOfTask"] == null)
                            {
                                Session["ListOfTask"] = new List<IExecutionRequest>();
                            }

                            IExecutionRequest exec = new ExecutionRequest();
                            exec.ExecutionTask = Task;
                            exec.StartRule = -1;

                            ((List<IExecutionRequest>)Session["ListOfTask"]).Add(exec);
                        }

                        if (!string.IsNullOrEmpty(script))
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "DoOpenTaskScript", script, true);
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
                    switch ((OpenType)Params["openmode"])
                    {
                        case OpenType.NewTab:
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", String.Format("parent.OpenWindow('{0}');", Params["url"].ToString()), true);
                            break;
                        case OpenType.Modal:
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenUrl", "$(document).ready(function(){ShowIFrameModal('','" +  Params["url"].ToString() + "', 800,600);});", true);
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
            case RulePendingEvents.RefreshWfTree:
                
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
            Session["EntryRulesExecution"] = PendigEvent;
            pnlUcRules.Controls.Clear();
            pnlUcRules.Controls.Add(ucrule);
            ucrule.ID = PendigEvent.ToString();

            IRule iucrule = (IRule)ucrule;
            iucrule.TaskID = this._currTaskID;
            iucrule.ContinueExecution += new WFExecution._continueExecution(this.WFExec.ExecuteRule);
            iucrule.NonVisibleTaskWithGuiRules = true;
            iucrule.ExecutionResult = ExecutionResult;
            iucrule.PendingChildRules = PendingChildRules;
            iucrule.ExecutedIDs = ExecutedIDs;
            iucrule.PendigEvent = PendigEvent;
            iucrule.results = results;
            iucrule.Params = (Params != null ? Params : (Hashtable)Session[this._currTaskID.ToString() + "Params"]);
            iucrule.RuleID = RuleId;

            //Se guarda el nombre de la regla para poder cerrar los dialogos.
            RuleButtonOk = PendigEvent.ToString() + "__btnok";

            if (iucrule.Params.ContainsKey("selectedChecks" + iucrule.RuleID.ToString()))
                iucrule.Params["selectedChecks" + iucrule.RuleID.ToString()] = hdnChecks.Value;
            else
                iucrule.Params.Add("selectedChecks" + iucrule.RuleID.ToString(), hdnChecks.Value);
            //Se quita ya que se maneja modal bootstrap desde JS en zamba.js
            showJqueryDialog();

            iucrule.LoadOptions();
        }
        else
        {
            //Si se cancela la ejecucion se limpian las variables de sesión y se cierra el div
            if (!(Session["EntryRulesExecution"] != null && PendigEvent != RulePendingEvents.CancelExecution) &&
                (PendigEvent == RulePendingEvents.CancelExecution || PendingChildRules.Count == 0))
            {
                //Verificar que no queden tareas si ejecutar
                //Si quedan ejecutarlas
                if (PendingEntryRulesAndTasks != null && PendingEntryRulesAndTasks.Count > 0)
                {
                    STasks sTasks = new STasks();
                    //Ejecuta la regla(en la posicion 0) para la tarea pendiente(posicion 1)
                    ExecuteRule(PendingEntryRulesAndTasks[0][0], sTasks.GetTask(PendingEntryRulesAndTasks[0][1]));
                }
                else
                {
                    PendingEntryRulesAndTasks = null;
                    Session["ListOfTask"] = null;
                    Session["EntryRulesExecution"] = null;
                    Close();
                }
            }
        }
    }

    protected void CloseTask(ITaskResult result)
    {
        //TO-DO: TODAVIA NO FUE HECHO EL TEST PARA ESTOS CASOS, SOLAMENTE FUE COPIADO DE UCWFEXECUTION
        STasks sTasks = new STasks();
        sTasks.Finalizar(result);
        sTasks = null;

        if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseTask"))
        {
            string js = "$(document).ready(function(){CloseTask(" + result.TaskId.ToString() + ", false);});";
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "CloseTask", js, true);
        }
    }

    //ML: Se reemplaza el RefreshTask agregando el parametro TaskIDsToRefresh, para refrescar las tareas marcadas para actualizacion en la entrada
    protected void RefreshTask(ITaskResult result, bool DoPostBack, ref List<long> TaskIDsToRefresh)
    {
        //'TaskResult = task
        //'TaskResult.UserRules = GetDoEnableRules()
        //'ucTaskHeader.TaskResult = TaskResult
        //'ucTaskDetail.TaskResult = TaskResult
        //'Dim script As String = "__doPostBack();"
        //'Page.ClientScript.RegisterStartupScript(Page.GetType, "DoPostBack", script, True)

        if (TaskIDsToRefresh == null)
        {
            TaskIDsToRefresh = new List<long>();
        }

        if (!TaskIDsToRefresh.Contains(result.TaskId))
        {
            TaskIDsToRefresh.Add(result.TaskId);
        }

        StringBuilder sbScript = new StringBuilder();
        long taskIdToRefresh;
        List<long> idsRegistereds = new List<long>();
        ITaskResult tempTask;

        long max = TaskIDsToRefresh.Count;

        sbScript.Append("$(document).ready(function(){");
        Zamba.Core.WF.WF.WFTaskBusiness WFTB = new Zamba.Core.WF.WF.WFTaskBusiness();
        for (int i = 0; i <= max - 1; i++)
        {
            taskIdToRefresh = TaskIDsToRefresh[i];

            if (!idsRegistereds.Contains(taskIdToRefresh))
            {
                tempTask = WFTB.GetTask(taskIdToRefresh, null, 0);

                if (tempTask != null)
                {
                    sbScript.Append("parent.RefreshTask(");
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

        string script = sbScript.ToString();
        if (script.Contains("parent.RefreshTask"))
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RefreshTaskScript", script, true);

        script = null;
        sbScript = null;
    }
    
    /// <summary>
    /// Muestra dialogo jquery con el UC de las reglas.
    /// </summary>
    /// <param name="Params"></param>
    /// <param name="RuleName"></param>
    private void showJqueryDialog()
    {
        if (!Page.ClientScript.IsClientScriptBlockRegistered("EntryRuleDialog"))
        {
            //StringBuilder sbJs = new StringBuilder();
            //sbJs.AppendLine("$(document).ready(function(){");
            //sbJs.AppendLine("parent.BlockWebPageInteraction();");
            //sbJs.AppendLine("$(\"#pnlUcRules\").dialog({closeOnEscape:false,autoOpen:true,modal:true,width:600,position:'top',resizable:false});");
            //sbJs.AppendLine("$(\"#pnlUcRules\").dialog().parent().appendTo($(\"form:first\"));");
            //sbJs.AppendLine("FixAndPosition($(\"#pnlUcRules\").parent(),$(\"#pnlUcRules\"));");
            //sbJs.AppendLine("ShowContainer(true);");
            //sbJs.AppendLine("$('.ui-dialog-titlebar').hide();");
            //sbJs.AppendLine("});");
            //ctl00_ContentPlaceHolder_UC_WFExecution_pnlUcRules
            //var script = "$(document).ready(function() {ShowIFrameModal('','../WF/WFExecutionForEntryRules.aspx','800','600');});";
            //var script = "$(document).ready(function() {ShowDivModal('',$('#EntryRulesContent', parent.document),'800','600');});";
            
            var script = "$(document).ready(function() {reloadBootstrap();$('#openModalIFWF', parent.document).modal();});";           
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "EntryRuleDialog", script, true);
        }
    }

    private void BeginExecution()
    {
        if (ListOfTask != null || ListOfTask.Count > 0)
        {
            taskResult = ListOfTask[0].ExecutionTask;
            if (taskResult != null)
            {
                CurrTaskID = taskResult.ID;
                CurrTaskName = taskResult.Name;

                if (ListOfTask[0].StartRule > 0)
                {
                    if (ListOfTask.Count>1)
                        ListOfTask.RemoveAt(0);

                    long ruleId = ListOfTask[0].StartRule;
                    ExecuteRule(ruleId, taskResult);
                   // ListOfTask.RemoveAt(0);
                }
                else
                {
                    SRules sRules = new SRules();

                    //Obtiene todas las reglas de la etapa
                    List<IWFRuleParent> rules = sRules.GetCompleteHashTableRulesByStep(taskResult.StepId);

                    if (rules != null)
                    {
                        List<long> executedIds;
                        List<long> idsToExecute;

                        //Se obtienen los ids de las reglas de entrada
                        var rulesIDs = from rule in rules
                                       where rule.ParentType == TypesofRules.Entrada
                                       select rule.ID;

                        if (PendingEntryRulesAndTasks == null)
                        {
                            PendingEntryRulesAndTasks = new List<long[]>();
                        }

                        //Verifica si debe continuar con una ejecución pendiente
                        if (Session[CurrTaskID + "ExecutedIDs"] != null)
                        {
                            /*
                             * 
                             * CREO QUE ESTE CODIGO NUNCA SE EJECUTA. 
                             * VERIFICAR PARA REGLAS SIN GUI.
                             * 
                             * 
                             * */
                            //Obtiene la última regla con GUI ejecutada
                            executedIds = (List<long>)Session[CurrTaskID + "ExecutedIDs"];
                            Int64 ruleId = executedIds[executedIds.Count - 1];

                            //Obtiene los ids de las reglas hijas a ejecutar
                            idsToExecute = sRules.GetChildRulesIds(ruleId);

                            //Se agregan todos los ids a ejecutar (TODO: faltan, pero por el momento es lo que se pudo hacer...)
                            foreach (long rid in rulesIDs)
                                idsToExecute.Add(rid);

                            //Se quitan los elementos ejecutados
                            foreach (long rid in executedIds)
                                idsToExecute.Remove(rid);
                        }
                        else
                        {
                            idsToExecute = new List<long>();

                            //Se agregan todos los ids a ejecutar (TODO: faltan, pero por el momento es lo que se pudo hacer...)
                            foreach (long rid in rulesIDs)
                            {
                                idsToExecute.Add(rid);
                                PendingEntryRulesAndTasks.Add(new long[] { rid, taskResult.TaskId });
                            }
                        }

                        IEnumerable<long[]> qResults;

                        //Se ejecutan las reglas obtenidas, mientras que no hayan eventos pendientes y la regla este pendiente de ejecucion
                        foreach (long rid in idsToExecute.Where(id => ((Session["EntryRulesExecution"] == null ||
                            (RulePendingEvents)Session["EntryRulesExecution"] == RulePendingEvents.NoPendingEvent) &&
                            PendingEntryRulesAndTasks != null)))
                        {
                            qResults = PendingEntryRulesAndTasks.Where(p => p[0] == rid && p[1] == taskResult.TaskId);
                            if (qResults != null && qResults.Count() > 0)
                                ExecuteRule(rid, taskResult);
                        }

                        //Limpia las variables. No hacer Clear() ya que afecta por referencia.
                        executedIds = null;
                        rulesIDs = null;
                        rules = null;
                    }
                    sRules = null;

                    //Si no hay eventos pendientes se remueve la tarea en ejecución
                    if (Session["EntryRulesExecution"] == null)
                    {
                        if (ListOfTask != null) ListOfTask.RemoveAt(0);
                        Session.Remove(CurrTaskID + "ExecutedIDs");
                    }
                }
            }
            else
            {
                ListOfTask.RemoveAt(0);
            }
        }

        if (ListOfTask == null || ListOfTask.Count == 0)
        {
            //Al finalizar la ejecución de todas las tareas, se cierra el cuadro de espera
            Close();
        }
        else
        {
            if (Session["EntryRulesExecution"] == null)
                BeginExecution(); //Continua con la siguiente tarea
        }
    }

    private void Close()
    {
        pnlUcRules.Controls.Clear();
        CurrTaskID = -1;

        if (!Page.ClientScript.IsClientScriptBlockRegistered("CloseEntryRulePanel"))
        {
            string script = "$(document).ready(function(){parent.CloseEntryRulesPanel();parent.hideLoading();parent.UnblockWebPageInteraction();});";
            ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "CloseEntryRulePanel", script, true);
        }
    }

    private void ExecuteRule(long RuleID, ITaskResult task)
    {
        SRules srules = new SRules();
        try
        {
            RuleExecutionResult ExecutionResult = new RuleExecutionResult();
            RulePendingEvents PendigEvent = new RulePendingEvents();
            Hashtable Params = new Hashtable();
            bool RefreshRule = false;
            List<ITaskResult> tasks = new List<ITaskResult> { task };

            //Se remueve la regla de las pendientes
            if (PendingEntryRulesAndTasks != null)
            {
                long[] pendingExec = PendingEntryRulesAndTasks.Where(p => p[0] == RuleID && p[1] == task.TaskId).SingleOrDefault();
                PendingEntryRulesAndTasks.Remove(pendingExec);
            }

            //Ejecuta las reglas para esa tarea
            this.WFExec.ExecuteRule(RuleID, ref tasks, PendigEvent, ExecutionResult,
                null, Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);

            //Limpia las variables. No hacer Clear() ya que afecta por referencia.
            Params = null;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            //ShowScreenMessage("Ha ocurrido un error en la regla: " + srules.GetRuleName(RuleID));
        }
        finally
        {
            srules = null;
        }
    }

    public void HandleWFExecutionPendingEvents(long RuleId, ref List<ITaskResult> results,
        ref RulePendingEvents PendigEvent, ref RuleExecutionResult ExecutionResult,
        ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules,
        ref  Boolean RefreshRule, List<long> TaskIdsToRefresh)
    {
        if (ExecutionResult == RuleExecutionResult.FailedExecution)
        {
            //ShowScreenMessage("Ha ocurrido un error en la regla");
            Session["ExecutingRule"] = null;
        }
        else if (ExecutionResult == RuleExecutionResult.CorrectExecution)
        {
            Session["ExecutingRule"] = null;
            Close();
        }
        else
        {
            LoadUCRule(RuleId, results, PendigEvent, ExecutionResult, ExecutedIDs, 
                Params, PendingChildRules, ref RefreshRule, TaskIdsToRefresh);
        }
    }

    //private void ShowScreenMessage(string msj)
    //{
    //    if (!Page.ClientScript.IsClientScriptBlockRegistered("Message"))
    //    {
    //        string script = "$(window).load(function() { " +
    //            "$('#spnMsg').text('" + msj + "'); " +
    //            "ShowContainer(true); " +
    //            "$('#" + divMsg.UniqueID + "').dialog({closeOnEscape: false, open: function(event, ui) { $(\".ui-dialog-titlebar\").hide(); }, autoOpen: true, modal: false, width: '100%', height: 150, position: 'top', resizable: false, draggable: false}); " +
    //            "FixAndPosition($('#" + divMsg.UniqueID + "').parent(), $('#" + divMsg.UniqueID + "')); " +
    //            "$('#" + divMsg.UniqueID + "').dialog().parent().appendTo($(\"form:first\")); " +
    //            "$('#" + divMsg.UniqueID + "').dialog('open');" +
    //            " });";

    //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Message", script, true);
    //    }
    //}
    public static IHtmlString GetJqueryCoreScript()
    {
        return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    }
    #endregion
}

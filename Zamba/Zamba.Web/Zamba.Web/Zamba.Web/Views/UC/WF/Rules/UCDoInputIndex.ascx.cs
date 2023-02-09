using System;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services;
using System.Web.UI.WebControls;
using Zamba;
using Zamba.Web.App_Code.Helpers;
using Zamba.Framework;
using System.Collections;

public partial class UC_WF_Rules_UCDoInputIndex : System.Web.UI.UserControl, IRule
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public long IndexId
    {
        get;
        set;
    }

    public string Mensaje
    {
        get { return this.lblmessage.Text; }
        set { this.lblmessage.Text = value; }
    }
    public string ReturnValue
    {
        get { return this.txtvalue.Text; }
        set { this.txtvalue.Text = value; }
    }

    #region IRule Members
    private Int64 _taskID;
    public event WFExecution._continueExecution ContinueExecution;

    public Zamba.Core.RuleExecutionResult ExecutionResult
    {
        get
        {
            return (RuleExecutionResult)Session[this._taskID + "ExecutionResult"];
        }
        set
        {
            Session[this._taskID + "ExecutionResult"] = value;
        }
    }

    public Zamba.Core.RulePendingEvents PendigEvent
    {
        get
        {
            return (RulePendingEvents)Session[this._taskID + "PendigEvent"];
        }
        set
        {
            Session[this._taskID + "PendigEvent"] = value;
        }
    }

    public List<long> ExecutedIDs
    {
        get
        {
            return (List<long>)Session[this._taskID + "ExecutedIDs"];
        }
        set
        {
            Session[this._taskID + "ExecutedIDs"] = value;
        }
    }

    public List<long> PendingChildRules
    {
        get
        {
            return (List<long>)Session[this._taskID + "PendingChildRules"];
        }
        set
        {
            Session[this._taskID + "PendingChildRules"] = value;
        }
    }

    public System.Collections.Hashtable Params
    {
        get
        {
            Hashtable HT = null;
            if (Session[this._taskID + "Params"] != null)
            {
                HT = (Hashtable)Session[this._taskID + "Params"];
            }
            else
            {
                HT = new Hashtable();
                Session[this._taskID + "Params"] = HT;
            }
            return HT;
        }
        set
        {
            Session[this._taskID + "Params"] = value;
        }
    }

    public long RuleID
    {
        get
        {
            return (long)Session[this._taskID + "RuleID"];
        }
        set
        {
            Session[this._taskID + "RuleID"] = value;
        }
    }

    public string Path
    {
        get
        {
            return (string)Session[this._taskID + "Path"];
        }
        set
        {
            Session[this._taskID + "Path"] = value;
        }
    }

    private List<ITaskResult> _results;
    public List<ITaskResult> results
    {
        get
        {
            return (List<ITaskResult>)Session[this._taskID + "results"];
        }
        set
        {
            Session[this._taskID + "results"] = value;
        }
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
            this._results = this.results;
        }
    }

    bool nonVisibleTaskWithGuiRules;

    /// <summary>
    /// Define si se encuentra ejecutando reglas con GUI para tareas cerradas (no visibles).
    /// </summary>
    public bool NonVisibleTaskWithGuiRules
    {
        get
        {
            return nonVisibleTaskWithGuiRules;
        }
        set
        {
            nonVisibleTaskWithGuiRules = value;
        }
    }

    /// <summary>
    /// Limpia el estado de la ejecución dependiendo de si es una ejecución normal
    /// o si es una ejecución de reglas con interfaz gráfica con la tarea cerrada.
    /// </summary>
    public void ClearCurrentExecutionSession()
    {
        if (nonVisibleTaskWithGuiRules)
            Session["EntryRulesExecution"] = null;
        else
            Session[TaskID + "CurrentExecution"] = null;
    }

    #endregion

    public void _btnOk_Click(object sender, EventArgs e)
    {
        ClearCurrentExecutionSession();
        Params.Add("Valor", this.txtvalue.Text);
        Boolean RefreshRule = false;

        var PendigEvent = this.PendigEvent;
        var ExecutionResult = this.ExecutionResult;
        var ExecutedIDs = this.ExecutedIDs;
        var MyParams = this.Params;
        var PendingChildRules = this.PendingChildRules;

        ContinueExecution(this.RuleID, ref this._results, ref PendigEvent, ref ExecutionResult,
            ref ExecutedIDs, ref MyParams, ref PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"], false);
    }
    public void _btnCancel_Click(object sender, EventArgs e)
    {
        ClearCurrentExecutionSession();
        Params.Add("CancelRule", true);
        Boolean RefreshRule = false;
        if (nonVisibleTaskWithGuiRules)
            PendigEvent = RulePendingEvents.CancelExecution;

        var MyPendigEvent = this.PendigEvent;
        var ExecutionResult = this.ExecutionResult;
        var ExecutedIDs = this.ExecutedIDs;
        var MyParams = this.Params;
        var PendingChildRules = this.PendingChildRules;

        ContinueExecution(this.RuleID, ref this._results, ref MyPendigEvent, ref ExecutionResult,
            ref ExecutedIDs, ref MyParams, ref PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"], false);

        //string js = "$(document).ready(function(){  });";
        //Page.ClientScript.RegisterClientScriptBlock(typeof(), "CloseTask", js, true);
    }

    public void LoadOptions()
    {
        Mensaje = "Ingresar valor del indice: " + Params["IndexName"].ToString();
        IndexId = (int)Params["IndexId"];
        ReturnValue = String.Empty;

        SIndex sIndex = new SIndex();

        IIndex index1 = sIndex.GetIndex(IndexId);
        if (this._results != null && this._results.Count > 0 && this._results[0] != null)
        {
            txtvalue = (TextBox)Validators.GetControlWithValidations(index1, txtvalue, WebModuleMode.Result, this._results[0], ZFieldType.RuleField);
        }
        else
        {
            STasks service = new STasks();
            txtvalue = (TextBox)Validators.GetControlWithValidations(index1, txtvalue, WebModuleMode.Result, service.GetTask(TaskID, Zamba.Membership.MembershipHelper.CurrentUser.ID), ZFieldType.RuleField);
            service = null;
        }
    }
}

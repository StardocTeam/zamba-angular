using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

public partial class UC_WF_Rules_UCDoScreenMessage : System.Web.UI.UserControl, IRule
{


    public string Texto
    {
        get { return this._message.InnerText; }
        set { this._message.InnerText = value; }
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
            return (Hashtable)Session[this._taskID + "Params"];
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
            Session[TaskID+ "CurrentExecution"] = null;
    }

    #endregion

    public void _btnOk_Click(object sender, EventArgs e)
    {
        ClearCurrentExecutionSession();

        //if (nonVisibleTaskWithGuiRules)
        //{
        //    //Se marca la regla como ejecutada
        //    List<Int64> executedIds = ExecutedIDs;
        //    executedIds.Add(RuleID);
        //    ExecutedIDs = executedIds;

        //    //Se limpian los parametros para cargar correctamente a la regla hija
        //    if (nonVisibleTaskWithGuiRules && (Params != null || Params.Count > 0))
        //        Params.Clear();
        //}

        Boolean RefreshRule = false;
        ContinueExecution(this.RuleID, ref this._results, this.PendigEvent, this.ExecutionResult,
            this.ExecutedIDs, this.Params, this.PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"]);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void _btnCancel_Click(object sender, EventArgs e)
    {
        if (nonVisibleTaskWithGuiRules)
            PendigEvent = RulePendingEvents.CancelExecution;
    }

    public void LoadOptions()
    {
        Texto = Params["Nuevo Mensaje"].ToString();
    }
}

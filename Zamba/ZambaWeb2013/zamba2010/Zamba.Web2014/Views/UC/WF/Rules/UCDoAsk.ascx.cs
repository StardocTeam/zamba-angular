using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zamba.Core;

public partial class UC_WF_Rules_UCDoAsk : System.Web.UI.UserControl, IRule
{
    private Int64 _taskID;
    private bool _isSingleData;

    public string ask
    {
        get { return this.lblmessage.Text; }
        set { this.lblmessage.Text = value; }
    }

    public string SelectedValue { get; set; }

    //05/07/11:Cambiado a clase base puesto que ahora podrá almacenar dos tipos de datos textbox o listbox
    public object ReturnValue
    {
        get;
        set;
    }
    //05/07/11: Sumada propiedad que permite ver si el valor a mostrar será hecho en un listbox o en un textbox.
    //Como ahora returnValue es un object se casteará segun el correspondiente valor de isSingledata (true=string, false=dataTable).
    public bool isSingleData
    {
        get { return _isSingleData; }
        set
        {
            _isSingleData = value;
        }
    }

    #region IRule Members

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
            Session[TaskID + "CurrentExecution"] = null;
    }

    #endregion

    public void _btnOk_Click(object sender, EventArgs e)
    {
        isSingleData = (bool)Params["singleData"];
        ClearCurrentExecutionSession();
        if (isSingleData)
            this.Params.Add("valor", this.txtvalue.Text);
        else
            this.Params.Add("valor", (String.IsNullOrEmpty(SelectedValue) ? String.Empty : SelectedValue));

        Boolean RefreshRule = false;
        ContinueExecution(this.RuleID, ref this._results, this.PendigEvent, this.ExecutionResult,
            this.ExecutedIDs, this.Params, this.PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"]);
    }

    public void _btnCancel_Click(object sender, EventArgs e)
    {
        ClearCurrentExecutionSession();
        Params.Add("CancelRule", true);
        Boolean RefreshRule = false;

        if (nonVisibleTaskWithGuiRules)
            PendigEvent = RulePendingEvents.CancelExecution;

        ContinueExecution(this.RuleID, ref this._results, this.PendigEvent, this.ExecutionResult,
            this.ExecutedIDs, this.Params, this.PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"]);
    }

    public void LoadOptions()
    {
        ask = Params["mensaje"].ToString();
        ReturnValue = Params["valorPorDefecto"];
        isSingleData = (bool)Params["singleData"];
        SelectedValue = (string)Params["selectedChecks" + this.RuleID];

        if (_isSingleData)
        {
            this.txtvalue.Text = (string)ReturnValue;
            this.txtvalue.Visible = true;
            lbValue.Visible = false;
        }
        else
        {
            //Si se muestra una lista...
            DataTable sourceTable = (DataTable)ReturnValue;
            StringBuilder displayText;
            //Se recorrerán todas las row armando el texto a mostrar (ValorRow1 - ValorRow2 - etc)
            foreach (DataRow currentRow in sourceTable.Rows)
            {
                displayText = new StringBuilder();
                foreach (DataColumn currentColumn in sourceTable.Columns)
                {
                    displayText.Append(currentRow[currentColumn]);
                    displayText.Append(" - ");
                }
                displayText.Remove(displayText.Length - 3, 3);

                lbValue.Items.Add(displayText.ToString());
            }

            this.lbValue.Visible = true;
            txtvalue.Visible = false;
        }
    }
}

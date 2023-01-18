using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Zamba.Core;
using System.Collections.Generic;
using Zamba.Web.App_Code.Helpers;
using Zamba.Framework;

public partial class Views_UC_WF_Rules_UCDoAskDesition : System.Web.UI.UserControl, IRule
{
 private Int64 _taskID;
    string _Ask_Desition;

    protected void Page_Load(object sender, EventArgs e)
    {
        var ScriptFixLookAndFeel = "";
        ScriptFixLookAndFeel += "if($('.fix-size').length==1){";
        ScriptFixLookAndFeel += "timerFixSizeTxtValue = setInterval(";
        ScriptFixLookAndFeel += "function(){";
        ScriptFixLookAndFeel += "if($('.fix-size')[0].scrollHeight!=0){";
        ScriptFixLookAndFeel += "clearInterval(timerFixSizeTxtValue);";
        ScriptFixLookAndFeel += "$('.fix-size')[0].style.height= $('.fix-size')[0].scrollHeight + 'px';"; 
        ScriptFixLookAndFeel += "$('#modalFormTitleUcRules').parent()[0].classList.remove('modal-header');"; 
        ScriptFixLookAndFeel += "$($($('.fix-size')[0]).parent()[0])[0].style='overflow:hidden;height:auto';"; 
        ScriptFixLookAndFeel += "$($('#modalFormTitleUcRules').parent()[0]).parent()[0].classList.remove('modal-content');"; 
        ScriptFixLookAndFeel += "}},1);}";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "fixSizeMessage", "$(document).ready(function(){" + ScriptFixLookAndFeel + "});", true);
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    private void txtValueConfig() {
        try
        {
            this.txtvalue.ReadOnly = true;
            this.txtvalue.TextMode = TextBoxMode.MultiLine;
            int charRows = 0;
            string tbCOntent;
            int chars = 0;
            tbCOntent = this.txtvalue.Text;
            int lineBreaks = this.txtvalue.Text.Where(c => c == '\n').Count();
            this.txtvalue.Columns = 120;
            chars = tbCOntent.Length;
            charRows = chars / this.txtvalue.Columns;
            int remaining = chars - charRows * this.txtvalue.Columns;
            if (remaining == 0)
                this.txtvalue.Rows = charRows;
            else
                this.txtvalue.Rows = charRows + lineBreaks;
        }
        catch (Exception ex)
        {
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "Error al calcular el size de textbox para la DoAskDesition - Exception message: " + ex.Message);
        }
    }

    public string ask
    {
        get { return this.lblmessage.Text; }
        set { this.lblmessage.Text = value; }
    }

    public string ReturnValue
    {
        get { return this.txtvalue.Text; }
        set { this.txtvalue.Text = value; }
    }
     public string Ask_Desition
     {
         get {return _Ask_Desition;}
         set{ _Ask_Desition = value;}

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

    public System.Collections.Generic.List<long> ExecutedIDs
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
        this.Params.Add("valor", "SI");
        Boolean RefreshRule = false;

        var PendigEvent = this.PendigEvent;
        var ExecutionResult = this.ExecutionResult;
        var ExecutedIDs = this.ExecutedIDs;
        var Params = this.Params;
        var PendingChildRules = this.PendingChildRules;

        ContinueExecution(this.RuleID, ref this._results, ref PendigEvent, ref ExecutionResult,
            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"], false);
    }
    public void _btnCancel_Click(object sender, EventArgs e)
    {     
        ClearCurrentExecutionSession();
        this.Params.Add("valor", "NO");
        Boolean RefreshRule = false;

        var PendigEvent = this.PendigEvent;
        var ExecutionResult = this.ExecutionResult;
        var ExecutedIDs = this.ExecutedIDs;
        var Params = this.Params;
        var PendingChildRules = this.PendingChildRules;

        ContinueExecution(this.RuleID, ref this._results, ref PendigEvent, ref ExecutionResult,
            ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, (List<long>)Session["TaskIdsToRefresh"], false);
    }
    public void LoadOptions()
    {
        this.ReturnValue = Params["txtQuestionText"].ToString();
        txtValueConfig();
    }
}



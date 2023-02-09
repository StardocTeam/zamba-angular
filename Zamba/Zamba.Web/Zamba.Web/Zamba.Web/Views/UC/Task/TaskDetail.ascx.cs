
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Membership;

public partial class TaskDetail : System.Web.UI.UserControl
{

    private Zamba.Core.ITaskResult _ITaskResult;
    private Int64 TaskId;
  
    #region "Properties"

    public ITaskResult TaskResult
    {
        get { return _ITaskResult; }
        set { _ITaskResult = value; }
    }

    /// <summary>
    /// Evento que ejecuta reglas
    /// </summary>
    /// <param name="ruleId">ID de la regla a ejecutar</param>
    /// <param name="results">Tareas a ejecutar</param>
    /// <remarks></remarks>
    public event ExecuteRuleEventHandler ExecuteRule;
    public delegate void ExecuteRuleEventHandler(Int64 ruleId, List<Zamba.Core.ITaskResult> results);

    private void ThrowExecuteRule(Int64 ruleId, List<Zamba.Core.ITaskResult> results)
    {
        if (ExecuteRule != null)
        {
            ExecuteRule(ruleId, results);
        }
    }
    #endregion

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Zamba.Membership.MembershipHelper.CurrentUser  != null && TaskResult != null)
        {

            Page.Title = TaskResult.Name;

         

            TaskId = Int64.Parse(Request["TaskId"].ToString());
            this.HiddenTaskId.Value = Request["TaskId"].ToString();

            this.HiddenDocID.Value = TaskResult.ID.ToString();

            if (!IsPostBack)
            {
                ExecuteOpenRules();
            }
            else
            {
                if (string.IsNullOrEmpty(this.HiddenCurrentFormID.Value) == false)
                {
                    TaskResult.CurrentFormID = Int64.Parse(this.HiddenCurrentFormID.Value);
                }
            }

            if (TaskResult.CurrentFormID > 0 || HasForms(TaskResult.DocTypeId))
            {
                this.HiddenCurrentFormID.Value = TaskResult.CurrentFormID.ToString();

                Views_UC_Viewers_FormBrowser viewer = default(Views_UC_Viewers_FormBrowser);
                viewer = (Views_UC_Viewers_FormBrowser)LoadControl("../Viewers/FormBrowser.ascx");
                viewer.IsShowing = true;
                viewer.TaskResult = TaskResult;

                viewer.ExecuteRule -= ThrowExecuteRule;
                viewer.ExecuteRule += ThrowExecuteRule;

                pnlViewer.Controls.Add(viewer);
            }
            else {
                Views_UC_Viewers_DocViewer viewer = default(Views_UC_Viewers_DocViewer);

                viewer = (Views_UC_Viewers_DocViewer)LoadControl("../Viewers/DocViewer.ascx");
                viewer.Result = TaskResult;
                pnlViewer.Controls.Add(viewer);
            }

            Zamba.Services.SZOptBusiness ZOptBusines = new Zamba.Services.SZOptBusiness();
            Page.Title = TaskResult.Name;

            //if (IsPostBack)
            //{
            //    ExecuteOpenRules();
            //}
        }
    }

    public string HiddenCurrentFormIDValue
    {
        get { return this.HiddenCurrentFormID.Value;  }
        set { this.HiddenCurrentFormID.Value = value; }
    }

    public bool HasForms(Int64 docTypeId)
    {
        SForms sForms = new SForms();
        bool result = sForms.HasForms(docTypeId);
        sForms = null;

        return result;
    }

    /// <summary>
    /// Ejecuta las reglas de entrada
    /// </summary>
    /// <remarks></remarks>
    private void ExecuteOpenRules()
    {
        try
        {
            List<ITaskResult> List = new List<ITaskResult>();
            Int64 formId = TaskResult.CurrentFormID;
            TaskResult.CurrentFormID = 0;
            List.Add(TaskResult);

            try
            {
                SRules sRules = new SRules();

                List<IWFRuleParent> rules = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

                if ((rules != null))
                {
                    foreach (IWFRuleParent rule in rules)
                    {
                        if ((rule.RuleType == TypesofRules.AbrirDocumento))
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "TypesofRules.AbrirDocumento ThrowExecuteRule(rule.ID : " + rule.ID.ToString());

                            ThrowExecuteRule(rule.ID, List);
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }

            //Verifica si una DoShowForm no completo el formulario a mostrar
            if ((TaskResult.CurrentFormID == 0))
            {
                TaskResult.CurrentFormID = formId;
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
    public TaskDetail()
    {
        Load -= Page_Load;
        Load += Page_Load;
    }
}

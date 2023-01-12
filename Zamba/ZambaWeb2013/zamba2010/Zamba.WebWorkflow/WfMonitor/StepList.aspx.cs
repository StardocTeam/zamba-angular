using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;
using System.Collections.Generic;

public partial class StepList : System.Web.UI.Page
{
    #region Constantes
    private const String TASK_LIST_URL = "~/TaskList.aspx";
    private const String WORKFLOW_LIST_URL = "~/WorkflowList.aspx";
    private const String SESSION_WORKFLOWS_IDS = "SelectedWorkflowIds";
    private const String SESSION_STEPS_IDS = "SelectedStepIds";
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadSteps();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
    protected void btSelect_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Add(SESSION_STEPS_IDS, ucStepsList.SelectedStepIds);
            Response.Redirect(TASK_LIST_URL);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }



    protected void ucStepsList_ForceRefresh()
    {
        try
        {
            LoadSteps();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucStepsList_SelectedStepChanged(List<Int64> stepIds)
    {

    }

 
    protected void lnkWorkgflowList_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(WORKFLOW_LIST_URL);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    private void LoadSteps()
    {
        if (null != Session[SESSION_WORKFLOWS_IDS] && Session[SESSION_WORKFLOWS_IDS] is List<Int64>)
        {
            List<Int64> WorkflowIds = (List<Int64>)Session[SESSION_WORKFLOWS_IDS];
            ucStepsList.Steps = Zamba.Services.Steps.GetStepsByWorkflows(WorkflowIds);
        }
        else
            Response.Redirect(WORKFLOW_LIST_URL);
    }

   
}

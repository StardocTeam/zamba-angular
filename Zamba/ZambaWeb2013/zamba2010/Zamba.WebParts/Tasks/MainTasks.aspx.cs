using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public partial class Tasks_MainTasks : System.Web.UI.Page
{
    #region Constantes
    private const String STEP_LIST_URL = "~/StepList.aspx";
    private const String SESSION_WORKFLOWS_IDS = "SelectedWorkflowIds";
    #endregion

    #region Constantes
    private const String WORKFLOW_LIST_URL = "~/WorkflowList.aspx";

    private const String SESSION_STEPS_IDS = "SelectedStepIds";
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
            FormsAuthentication.RedirectToLoginPage();

        if (!Page.IsPostBack == true)
        {

        }

        if (null != Session[SESSION_WORKFLOWS_IDS] && Session[SESSION_WORKFLOWS_IDS] is List<Int64>)
        {
            List<Int64> WorkflowIds = (List<Int64>)Session[SESSION_WORKFLOWS_IDS];
            ucStepsList.Steps = Zamba.Services.Steps.GetStepsByWorkflows(WorkflowIds);
        }
        else
            Response.Redirect(WORKFLOW_LIST_URL);
    }

}

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
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
public partial class WorkflowList2 : System.Web.UI.Page
{
    #region Constantes
    private const String STEP_LIST_URL = "~/StepList.aspx";
    private const String SESSION_WORKFLOWS_IDS = "SelectedWorkflowIds";
    #endregion

    #region Eventos
    protected void btSelect_Click(object sender, EventArgs e)
    {
        try
        {
            Session.Add(SESSION_WORKFLOWS_IDS, ucWfList.SelectedWorfklowIds);
            Response.Redirect(STEP_LIST_URL);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!Page.IsPostBack)
        //{
        //    try
        //    {
        //        if (null != Session[SESSION_WORKFLOWS_IDS] && Session[SESSION_WORKFLOWS_IDS] is List<Int64>)
        //            ucWfList.SelectedWorfklowIds = (List<Int64>)Session[SESSION_WORKFLOWS_IDS];
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //    }

        //}
    }

    protected void ucWfList_SelectedWorkflowsChanged(List<Int64> workflowIds)
    {
    }
}

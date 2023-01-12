using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Zamba.Services;
using Zamba.Core;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
            FormsAuthentication.RedirectToLoginPage();

    }

    protected void ucTaskSelector_SelectedTasksChanged(List<Int64> taskIds)
    {
        try
        {
            ucTaskActions.SelectedTaskIds = taskIds;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskSelector_SelectedWorkflowChanged(Int64? workflowId, List<Int64> taskIds)
    {
        try
        {
            ucTaskActions.SelectedTaskIds = taskIds;
            ucTaskActions.SelectedWorkflowId = workflowId;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskSelector_SelectedTaskChanged(Int64? taskId)
    {
        try
        {
            ucTaskActions.Visible = true;
            ucTasksInformation.TaskId = taskId;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void ucTaskActions_TasksChanged()
    {
        try
        {
            ucTaskActions.Clear();
            ucTaskSelector.ReloadSteps();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskActions_StepsChanged()
    {
        try
        {
            ucTaskSelector.ReloadSteps();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskActions_DoAssign(Boolean boolAssign, Int64 userIDAssignedTo)
    {
        if (ucTaskSelector != null)
        {
            foreach (Int64 selectedTaskID in ucTaskActions.SelectedTaskIds)
            {
                Int64 userIDAssignedBy;

                if (null != Session["UserID"])
                {
                    userIDAssignedBy = Convert.ToInt64(Session["UserID"]);
                    if (boolAssign)
                    {
                        WFBusiness.AsignTask(selectedTaskID, userIDAssignedTo, userIDAssignedBy);
                    }
                    else
                    {
                        String userNameAssignedTo = UserBusiness.GetUserNamebyId(Convert.ToInt32(userIDAssignedTo));
                        WFBusiness.UnAssignTask(selectedTaskID, userIDAssignedTo, userNameAssignedTo, userIDAssignedBy, DateTime.Now);
                    }
                }
                else
                {
                    if (Int64.TryParse(Response.Cookies["ZambaSession"].Value, out userIDAssignedBy))
                    {
                        if (boolAssign)
                        {
                            WFBusiness.AsignTask(selectedTaskID, userIDAssignedTo, userIDAssignedBy);
                        }
                        else
                        {
                            String userNameAssignedTo = UserBusiness.GetUserNamebyId(Convert.ToInt32(userIDAssignedTo));
                            WFBusiness.UnAssignTask(selectedTaskID, userIDAssignedTo, userNameAssignedTo, userIDAssignedBy, DateTime.Now);
                        }
                    }
                }
            }
        }


    }

}
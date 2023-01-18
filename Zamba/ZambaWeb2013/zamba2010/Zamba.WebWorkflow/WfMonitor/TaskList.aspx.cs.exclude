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

public partial class TaskList : System.Web.UI.Page
{

    #region Constantes
    private const String WORKFLOW_LIST_URL = "~/WorkflowList.aspx";
    private const String STEP_LIST_URL = "~/StepList.aspx";
    private const String SESSION_STEPS_IDS = "SelectedStepIds";
    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadTasks();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    protected void ucTaskActions_TasksChanged()
    {
        try
        {
            LoadTasks();
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
            LoadTasks();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskActions_DoAssign(Boolean boolAssign, Int64 userIDAssignedTo)
    {
        foreach (Int64 selectedTaskID in ucTaskActions.SelectedTaskIds)
        {
            Int64 userIDAssignedBy;

            if (null != Session["UserID"])
            {
                userIDAssignedBy = Convert.ToInt64(Session["UserID"]);

                if (boolAssign)
                    WFTaskBussines.Asign(selectedTaskID, userIDAssignedTo, userIDAssignedBy,DateTime.Now);
                else
                {
                    String userNameAssignedTo = UserBusiness.GetUserNamebyId(Convert.ToInt32(userIDAssignedTo));
                    WFTaskBussines.UnAssign(selectedTaskID, userIDAssignedTo, userNameAssignedTo, userIDAssignedBy, DateTime.Now);
                }
            }
            else
            {
                if (Int64.TryParse(Response.Cookies["ZambaSession"].Value, out userIDAssignedBy))
                {
                    if (boolAssign)
                        WFTaskBussines.Asign(selectedTaskID, userIDAssignedTo, userIDAssignedBy,DateTime.Now);
                    else
                    {
                        String userNameAssignedTo = UserBusiness.GetUserNamebyId(Convert.ToInt32(userIDAssignedTo));
                        WFTaskBussines.UnAssign(selectedTaskID, userIDAssignedTo, userNameAssignedTo, userIDAssignedBy, DateTime.Now);
                    }
                }
            }
        }

        LoadTasks();
    }

    protected void ucTaskList_SelectedTasksChanged(List<Int64> taskIds)
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
    protected void ucTaskList_SelectedTaskChanged(Int64? taskId)
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
    protected void ucTaskList_SelectedWorkflowChanged(Int64? workflowId, List<Int64> taskIds)
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
    protected void ucTaskList_ForceRefresh()
    {
        try
        {
            LoadTasks();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }

    protected void lnkWorkflowList_Click(object sender, EventArgs e)
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
    protected void lnkStepList_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(STEP_LIST_URL);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    private void LoadTasks()
    {
        if (null != Session[SESSION_STEPS_IDS] && Session[SESSION_STEPS_IDS] is List<Int64>)
        {
            List<Int64> StepIds = (List<Int64>)Session[SESSION_STEPS_IDS];
            ucTaskList.Tasks = Zamba.Services.Tasks.GetTasksBySteps(StepIds);
        }
        else
            Response.Redirect(STEP_LIST_URL);
    }
  
}
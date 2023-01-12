using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using Zamba.Core;
using Zamba.Services;

public partial class TaskSelector : System.Web.UI.UserControl
{
    #region Eventos
    public event ChangedSelectedTasks SelectedTasksChanged;
    public delegate void ChangedSelectedTasks(List<Int64> taskIds);
    public event ChangedSelectedTask SelectedTaskChanged;
    public delegate void ChangedSelectedTask(Int64? taskId);
    public event ChangedSelectedWorkflow SelectedWorkflowChanged;
    public delegate void ChangedSelectedWorkflow(Int64? workflowId, List<Int64> taskIds);

    protected void ucWfList_SelectedWorkflowsChanged(List<Int64> workflowIds)
    {
        ReloadSteps();
    }

    protected void ucStepsList_SelectedStepsChanged(List<Int64> stepIds)
    {
        ReloadTasks();
    }
    protected void ucStepsList_ForceRefresh()
    {
        try
        {
            ucStepsList.Steps = Steps.GetStepsByWorkflows(ucWfList.SelectedWorfklowIds);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void ucTaskList_SelectedTasksChanged(List<Int64> taskIds)
    {
        try
        {
            SelectedTasksChanged(taskIds);
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
            SelectedTaskChanged(taskId);
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
            SelectedWorkflowChanged(workflowId, taskIds);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void ucTaskList_ForceRefresh()
    {
        ReloadTasks();
    }
    #endregion

    public void ReloadWorkflows()
    {
        try
        {
            ucWfList.Refresh();
            SelectedWorkflowChanged(null, ucTaskList.SelectedTaskIds);  
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    public void ReloadTasks()
    {
        try
        {
            ucTaskList.Tasks = Tasks.GetTasksBySteps(ucStepsList.SelectedStepIds);
            SelectedWorkflowChanged(null, ucTaskList.SelectedTaskIds);  
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    public void ReloadSteps()
    {
        try
        {
            ucStepsList.Steps = Steps.GetStepsByWorkflows(ucWfList.SelectedWorfklowIds);

            List<Int64> WorkflowIds = ucTaskList.SelectedWorkflowIds;

            if (null == WorkflowIds || WorkflowIds.Count != 1)
                SelectedWorkflowChanged(null, ucTaskList.SelectedTaskIds);
            else
                SelectedWorkflowChanged((Int64?)WorkflowIds[0], ucTaskList.SelectedTaskIds);

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
}
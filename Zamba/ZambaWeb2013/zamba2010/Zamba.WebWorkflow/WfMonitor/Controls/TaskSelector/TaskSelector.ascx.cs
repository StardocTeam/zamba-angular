using System;
using System.Collections.Generic;
using System.Web.UI;
using Zamba.Core;
using Zamba.Services;
using ASP;

public partial class TaskSelector
    : UserControl
{
    #region Eventos
    public event ChangedSelectedTasks SelectedTasksChanged;
    /// <summary>
    /// Le pasa al control padre las tareas seleccionadas.
    /// </summary>
    public delegate void ChangedSelectedTasks(List<Int64> taskIds);
    public event ChangedSelectedTask SelectedTaskChanged;
    /// <summary>
    /// Le pasa al control la tarea seleccionada para ver su informacion
    /// </summary>
    /// <param name="taskId"></param>
    public delegate void ChangedSelectedTask(Int64? taskId);
    public event ChangedSelectedWorkflow SelectedWorkflowChanged;
    /// <summary>
    /// Le pasa al control padre el WorkflowId y las tareas seleccionadas
    /// </summary>
    /// <param name="workflowId"></param>
    /// <param name="taskIds"></param>
    public delegate void ChangedSelectedWorkflow(Int64? workflowId, List<Int64> taskIds);

    protected void ucWfList_SelectedWorkflowsChanged(List<Int64> workflowIds)
    {
        try
        {
            LoadSteps(workflowIds);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void ucStepsList_SelectedStepsChanged(List<Int64> stepIds)
    {
        try
        {
            LoadTasks(stepIds);
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

    /// <summary>
    /// Recarga el listado de workflows
    /// </summary>
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
    /// <summary>
    /// Recarga el listado de tareas
    /// </summary>
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
    /// <summary>
    /// Recarga el listado de etapas
    /// </summary>
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

    /// <summary>
    /// Carga los workflows especificados
    /// </summary>
    /// <param name="workflowIds"></param>
    private void LoadSteps(List<Int64> workflowIds)
    {
        ucStepsList.Steps = Steps.GetStepsByWorkflows(workflowIds);
    }
    /// <summary>
    /// Carga las etapas especificadas
    /// </summary>
    /// <param name="stepIds"></param>
    private void LoadTasks(List<Int64> stepIds)
    {
        ucTaskList.Tasks = Tasks.GetTasksBySteps(stepIds);
    }

    public TaskSelector()
    {
        ucWfList = (wfmonitor_controls_taskselector_workflowlist_ascx) LoadControl("~/WfMonitor/Controls/TaskSelector/WorkflowList.ascx");
        ucStepsList = (wfmonitor_controls_taskselector_stepslist_ascx)LoadControl("~/WfMonitor/Controls/TaskSelector/StepsList.ascx");
    }
}
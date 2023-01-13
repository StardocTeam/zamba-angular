using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using Zamba.Core;

/// <summary>
/// Es el code behind de la pagina de monireo de Worklows
/// </summary>
public partial class Monitor 
    : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
            FormsAuthentication.RedirectToLoginPage();
    }

    #region Eventos del Control de seleccion de tareas UcTaskSelector
    /// <summary>
    /// Se cambiaron las tareas seleccionadas en UcTaskSelector , le paso la coleccion a UcTaskActions para que adecue su estado
    /// </summary>
    /// <param name="taskIds"></param>
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
    /// <summary>
    /// Se cambio el Workflow y las tareas seleccionadas en UcTaskSelector , le paso ambos a UcTaskActions para que adecue su estado.
    /// El Workflow id es principalmente para las etapas a derivar
    /// </summary>
    /// <param name="workflowId"></param>
    /// <param name="taskIds"></param>
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
    /// <summary>
    /// Se selecciono 1 tarea para ver su estado , se lo paso TaskInformation para que le cargue la informacion.
    /// </summary>
    /// <param name="taskId"></param>
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
    #endregion

    #region Eventos del Control de ejecucion de acciones sobre las tareas seleccionadas
    /// <summary>
    /// Se ejecutaron acciones sobre las tareas seleccionadas en el control UcTaskActions
    /// </summary>
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
    /// <summary>
    /// Se ejecutaron acciones sobre las tareas seleccionadas en el control UcTaskActions
    /// </summary>
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="boolAssign"></param>
    /// <param name="userIDAssignedTo"></param>
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
                        WFTaskBussines.Asign(selectedTaskID, userIDAssignedTo, userIDAssignedBy, DateTime.Now);
                    }
                    else
                    {
                        //String userNameAssignedTo = UserBusiness.GetUserNamebyId(Convert.ToInt32(userIDAssignedTo));
                        WFTaskBussines.UnAssign(selectedTaskID, 0, "Sin Asignar", userIDAssignedBy, DateTime.Now);
                    }
                }
                else
                {
                    if (Int64.TryParse(Response.Cookies["ZambaSession"].Value, out userIDAssignedBy))
                    {
                        if (boolAssign)
                        {
                            WFTaskBussines.Asign(selectedTaskID, userIDAssignedTo, userIDAssignedBy, DateTime.Now);
                        }
                        else
                        {
                            WFTaskBussines.UnAssign(selectedTaskID,0, "Sin Asignar", userIDAssignedBy, DateTime.Now);
                        }
                    }
                }
            }

            ucTaskSelector.ReloadSteps();
        }
    } 
    #endregion

}
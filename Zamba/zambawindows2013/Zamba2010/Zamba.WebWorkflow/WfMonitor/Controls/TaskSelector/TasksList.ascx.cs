using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

/// <summary>
/// Lists a collection of Tasks and allows the client to select any of them.
/// </summary>
public partial class TasksList
    : UserControl
{
    #region Constants
    /// <summary>
    /// Name of the Checkbox in the GridView TemplateField
    /// </summary>
    private const String CHECKBOX_NAME = "chbSelected";
    /// <summary>
    /// Name of the Label with the TaskId in the GridView TemplateField
    /// </summary>
    private const String MESSAGE_NO_TASKS = "No hay tareas para mostrar";
    /// <summary>
    /// Index of the Task id column
    /// </summary>
    private const Int32 TASK_ID_INDEX = 2;
    /// <summary>
    /// Index of the Workflow id Column
    /// </summary>
    private const Int32 WORKFLOW_ID_INDEX = 3;
    /// <summary>
    /// Index of the Task name column
    /// </summary>
    private const Int32 TASK_NAME_INDEX = 4;
    /// <summary>
    /// Index of the Task IsExpired column
    /// </summary>
    private const Int32 TASK_IS_EXPIRED_INDEX = 5;
    /// <summary>
    /// Index of the Task total task count column
    /// </summary>
    private const Int32 MAX_NAME_LENGHT = 20;
    /// <summary>
    /// Message Shown when a Task is Expired
    /// </summary>
    private const String EXPIRED_MESSAGE = "SI";
    /// <summary>
    /// Message Shown when a Task isn't Expired
    /// </summary>
    private const String NOT_EXPIRED_MESSAGE = "NO";
    #endregion

    #region Properties
    /// <summary>
    /// Get or Set every Task in the control
    /// </summary>
    public List<ITaskResult> Tasks
    {
        set
        {
            TaskInfo.Controls.Clear();
            List<Int64> CurrentSelectedTaskIds = SelectedTaskIds;
            gvTasks.DataSource = null;

            gvTasks.DataSource = value;
            gvTasks.DataBind();

            Int32 BaseIndex = gvTasks.PageSize * gvTasks.PageIndex;
            Int32 CurrentIndex;
            ITaskResult CurrentTask = null;
            GridViewRow CurrentRow = null;
            String ShortenedName = null;
            System.Web.UI.HtmlControls.HtmlAnchor  link = null;
            TaskInformation info = null;

            if (value.Count > 0)
            {

                try
                {

                    for (int i = 0; i < gvTasks.PageSize; i++)
                    {
                        CurrentIndex = BaseIndex + i;
                        if (CurrentIndex <= value.Count && i < gvTasks.Rows.Count)
                        {
                            CurrentTask = value[CurrentIndex];
                            CurrentRow = gvTasks.Rows[i];

                            CurrentRow.Cells[TASK_ID_INDEX].Text = CurrentTask.TaskId.ToString();
                            info = new TaskInformation();  // (TaskInformation)LoadControl("~/WfMonitor/Controls/TaskInformation/TaskInformation.ascx");
                            info.TaskId = CurrentTask.TaskId;
                            TaskInfo.Controls.Add(info);

                            link = new System.Web.UI.HtmlControls.HtmlAnchor();
                            link.InnerText = "Info";
                            link.HRef = "#" + info.ClientID;
                            link.Attributes.Add("class", "lightwindow");
                            CurrentRow.Cells[TASK_ID_INDEX].Controls.Add(link);


                            if (CurrentTask.WorkId == 0)
                                CurrentRow.Cells[WORKFLOW_ID_INDEX].Text = CurrentTask.WfStep.WorkId.ToString();
                            else
                                CurrentRow.Cells[WORKFLOW_ID_INDEX].Text = CurrentTask.WorkId.ToString();


                            CurrentRow.Cells[TASK_NAME_INDEX].ToolTip = CurrentTask.Name;
                            if (CurrentTask.Name.Length > MAX_NAME_LENGHT)
                            {
                                ShortenedName = CurrentTask.Name.Substring(0, MAX_NAME_LENGHT - 3);
                                ShortenedName = ShortenedName + "...";
                                CurrentRow.Cells[TASK_NAME_INDEX].Text = ShortenedName;
                            }
                            else
                                CurrentRow.Cells[TASK_NAME_INDEX].Text = CurrentTask.Name;

                            if (CurrentTask.IsExpired)
                            {
                                CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].Text = EXPIRED_MESSAGE;
                                CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].ToolTip = EXPIRED_MESSAGE;
                            }
                            else
                            {
                                CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].Text = NOT_EXPIRED_MESSAGE;
                                CurrentRow.Cells[TASK_IS_EXPIRED_INDEX].ToolTip = NOT_EXPIRED_MESSAGE;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }

            SelectedTaskIds = CurrentSelectedTaskIds;

            ValidateStatus();

            CurrentSelectedTaskIds.Clear();
            CurrentSelectedTaskIds = null;


            SelectedTaskChanged(SelectedTaskId);
        }
        get
        {
            List<Int64> TaskIds = new List<Int64>(gvTasks.Rows.Count);
            Int64 CurrentStepId;

            foreach (GridViewRow CurrentRow in gvTasks.Rows)
            {
                if (Int64.TryParse(CurrentRow.Cells[TASK_NAME_INDEX].Text, out CurrentStepId))
                    TaskIds.Add(CurrentStepId);
            }

            return Zamba.Services.Tasks.GetTasks(TaskIds);
        }
    }
    /// <summary>
    /// Get or Set the selected Tasks ids
    /// </summary>
    public List<Int64> SelectedTaskIds
    {
        set
        {
            if (null != value)
            {
                Control CurrentControl = null;
                CheckBox CurrentCheckBox = null;
                Int64 CurrentTaskId = 0;

                foreach (GridViewRow CurrentRow in gvTasks.Rows)
                {
                    if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out CurrentTaskId))
                    {
                        CurrentControl = CurrentRow.FindControl(CHECKBOX_NAME);
                        if (null != CurrentControl && CurrentControl is CheckBox)
                        {
                            CurrentCheckBox = (CheckBox)CurrentControl;
                            if (value.Contains(CurrentTaskId))
                            {
                                if (!CurrentCheckBox.Checked)
                                    CurrentCheckBox.Checked = true;
                            }
                            else
                            {
                                if (CurrentCheckBox.Checked)
                                    CurrentCheckBox.Checked = false;
                            }
                        }
                    }
                }

                if (null != CurrentControl)
                    CurrentControl.Dispose();
                if (null != CurrentCheckBox)
                    CurrentCheckBox.Dispose();

                SelectedTasksChanged(SelectedTaskIds);

                //if ( gvTasks.SelectedIndex != -1 )
                //    SelectedTaskChanged(sele 
            }
        }
        get
        {
            List<Int64> StepIds = new List<Int64>(gvTasks.Rows.Count);
            Control CurrentControl = null;
            CheckBox CurrentCheckBox = null;
            Int64 CurrentStepId;

            foreach (GridViewRow CurrentRow in gvTasks.Rows)
            {
                CurrentControl = CurrentRow.FindControl(CHECKBOX_NAME);

                if (null != CurrentControl && CurrentControl is CheckBox)
                {
                    CurrentCheckBox = (CheckBox)CurrentControl;

                    if (CurrentCheckBox.Checked)
                    {
                        if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out CurrentStepId))
                            StepIds.Add(CurrentStepId);
                    }
                }
            }

            if (null != CurrentControl)
                CurrentControl.Dispose();
            if (null != CurrentCheckBox)
                CurrentCheckBox.Dispose();

            return StepIds;
        }
    }

    /// <summary>
    /// Devuelve el Id de tarea seleccionado para ver su informacion
    /// </summary>
    private Int64? SelectedTaskId
    {
        get
        {
            Int64? TaskId = null;

            if (gvTasks.SelectedIndex != -1)
            {
                Int64 TryParse;
                if (Int64.TryParse(gvTasks.SelectedRow.Cells[TASK_ID_INDEX].Text, out TryParse))
                    TaskId = (Int64?)TryParse;
            }

            return TaskId;
        }
        set
        {
            gvTasks.SelectedIndex = -1;

            if (value.HasValue)
            {
                Int64 CurrentTaskId;
                foreach (GridViewRow CurrentRow in gvTasks.Rows)
                {
                    if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out CurrentTaskId))
                    {
                        if (CurrentTaskId == value.Value)
                        {
                            gvTasks.SelectedIndex = CurrentRow.RowIndex;
                            break;
                        }
                    }
                }
            }

            SelectedTaskChanged(SelectedTaskId);
        }
    }

    /// <summary>
    /// Get or Set the selected Workflow ids
    /// </summary>
    public List<Int64> SelectedWorkflowIds
    {
        get
        {
            List<Int64> WorkflowIds = new List<Int64>(gvTasks.Rows.Count);
            Control CurrentControl = null;
            CheckBox CurrentCheckBox = null;
            Int64 CurrentWorkflowId;

            #region Cargo los Ids de los workflows
            foreach (GridViewRow CurrentRow in gvTasks.Rows)
            {
                CurrentControl = CurrentRow.FindControl(CHECKBOX_NAME);
                if (null != CurrentControl && CurrentControl is CheckBox)
                {
                    CurrentCheckBox = (CheckBox)CurrentControl;
                    if (CurrentCheckBox.Checked)
                    {
                        if (Int64.TryParse(CurrentRow.Cells[WORKFLOW_ID_INDEX].Text, out CurrentWorkflowId))
                        {
                            if (!WorkflowIds.Contains(CurrentWorkflowId))
                                WorkflowIds.Add(CurrentWorkflowId);
                        }
                    }
                }
            }
            #endregion

            if (null != CurrentControl)
                CurrentControl.Dispose();
            if (null != CurrentCheckBox)
                CurrentCheckBox.Dispose();

            return WorkflowIds;
        }
    }

    #endregion

    #region Eventos
    /// <summary>
    /// Ocurrs when the selected Tasks are changed
    /// </summary>
    public event ChangedSelectedTasks SelectedTasksChanged;
    /// <summary>
    /// Le pasa al control padre el listado de tareas seleccionado
    /// </summary>
    /// <param name="taskIds"></param>
    public delegate void ChangedSelectedTasks(List<Int64> taskIds);
    public event ChangedSelectedTask SelectedTaskChanged;
    /// <summary>
    /// Le pasa al control padre el id de tarea seleccionado para ver su informacion
    /// </summary>
    /// <param name="taskId"></param>
    public delegate void ChangedSelectedTask(Int64? taskId);
    public event ChangedSelectedWorkflow SelectedWorkflowChanged;
    /// <summary>
    /// Le pasa al control padre el WorkflowId seleccionado y el listado de tareas seleccionado. 
    /// Si se deseleccionaron todas las tareas, no hay workflowId seleccionado , por lo tanto se pasa null 
    /// </summary>
    /// <param name="workflowId"></param>
    /// <param name="taskIds"></param>
    public delegate void ChangedSelectedWorkflow(Int64? workflowId, List<Int64> taskIds);

    /// <summary>
    /// Ocurrs when the controls needs to be refilled
    /// </summary>
    public event Refresh ForceRefresh;
    public delegate void Refresh();

    protected void gvTasks_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvTasks.PageIndex = e.NewPageIndex;
            gvTasks.DataBind();

            ForceRefresh();

            SelectedTasksChanged(SelectedTaskIds);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void gvTasks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (null != gvTasks.SelectedRow)
        {
            Int64 TaskId;
            try
            {
                if (Int64.TryParse(gvTasks.SelectedRow.Cells[TASK_ID_INDEX].Text, out TaskId))
                    SelectedTaskChanged(TaskId);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                ValidateStatus();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
    protected void btUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ForceRefresh();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void btView_Click(object sender, EventArgs e)
    {
        try
        {
            List<Int64> SelectedWfIds = SelectedWorkflowIds;

            if (SelectedWfIds.Count == 1)
                SelectedWorkflowChanged((Int64?)SelectedWfIds[0], SelectedTaskIds);
            else
                SelectedWorkflowChanged(null, null);
            //se agrego una llamada a SelectedTaskChanged porque cuando se apretaba este boton
            //se perdian los indices en la sola indice(si es que se estaba visualizando)         
            if(SelectedTaskIds.Count ==1 && SelectedTaskIds.Count != 0)

            SelectedTaskChanged(SelectedTaskIds[0]);
           
        }
         
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void lnkAll_Click(object sender, EventArgs e)
    {
        try
        {
            ChangeCheckBoxesState(true);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void lnkNone_Click(object sender, EventArgs e)
    {
        try
        {
            ChangeCheckBoxesState(false);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    #endregion

    /// <summary>
    /// Shows or Hides this control according to its status
    /// </summary>
    private void ValidateStatus()
    {
        if (gvTasks.Rows.Count == 0)
        {
            lbNoTasks.Visible = true;
            lbNoTasks.Text = MESSAGE_NO_TASKS;
            gvTasks.SelectedIndex = -1;
            gvTasks.Visible = false;
            SelectedWorkflowChanged(null, null);
            SelectedTaskChanged(null);
            lnkAll.Visible = false;
            lnkNone.Visible = false;
            btUpdate.Visible = false;
            btView.Visible = false;
        }
        else
        {
            lbNoTasks.Visible = false;
            gvTasks.Visible = true;
            lnkAll.Visible = true;
            lnkNone.Visible = true;
            btUpdate.Visible = true;
            btView.Visible = true;
        }
    }

    private void ChangeCheckBoxesState(bool check)
    {
        Control CurrentPosibleCheckBox = null;
        CheckBox CurrentCheckBox = null;

        foreach (GridViewRow CurrentRow in gvTasks.Rows)
        {
            CurrentPosibleCheckBox = CurrentRow.FindControl(CHECKBOX_NAME);

            if (null != CurrentPosibleCheckBox && CurrentPosibleCheckBox is CheckBox)
            {
                CurrentCheckBox = (CheckBox)CurrentPosibleCheckBox;
                CurrentCheckBox.Checked = check;
            }
        }

        if (null != CurrentPosibleCheckBox)
            CurrentPosibleCheckBox.Dispose();
        if (null != CurrentCheckBox)
            CurrentCheckBox.Dispose();

        List<Int64> WorkflowIds = SelectedWorkflowIds;

        if (WorkflowIds.Count == 1)
            SelectedWorkflowChanged(WorkflowIds[0], SelectedTaskIds);
        else
            SelectedWorkflowChanged(null, null);
    }

    public TasksList()
    {
        lbNoTasks = new Label();
        lnkAll = new LinkButton();
        lnkNone = new LinkButton();
        gvTasks = new GridView();
        btUpdate = new Button();
        btView = new Button();
    }

}

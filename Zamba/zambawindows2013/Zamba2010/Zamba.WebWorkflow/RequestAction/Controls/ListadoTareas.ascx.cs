using System;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Collections.Generic;
using System.Web.UI;

/// <summary>
/// Lists a collection of Tasks and allows the client to select any of them.
/// </summary>
public partial class ListadoTareas
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
    /// Index of the "View" Button in the Gridview
    /// </summary>
    private const Int32 VIEW_BUTTON_INDEX = 0;
    /// <summary>
    /// Index of the Checkbox in the Gridview 
    /// </summary>
    private const Int32 SELECT_CHECKBOX_INDEX = 1;
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
            if (null != value && value.Count > 0)
            {
                List<Int64> CurrentSelectedTaskIds = SelectedTaskIds;
                gvTasks.DataSource = null;

                gvTasks.DataSource = value;
                gvTasks.DataBind();

                Int32 BaseIndex = gvTasks.PageSize * gvTasks.PageIndex;
                Int32 CurrentIndex;
                ITaskResult CurrentTask = null;
                GridViewRow CurrentRow = null;
                String ShortenedName = null;
                            
                for (int i = 0; i < gvTasks.PageSize; i++)
                {
                    CurrentIndex = BaseIndex + i;
                    if (CurrentIndex <= value.Count && i < gvTasks.Rows.Count)
                    {
                        CurrentTask = value[CurrentIndex];

                        CurrentRow = gvTasks.Rows[i];

                        if (CurrentTask is DeletedTask)
                            ((CheckBox)CurrentRow.FindControl(CHECKBOX_NAME)).Enabled = false; 

                        CurrentRow.Cells[TASK_ID_INDEX].Text = CurrentTask.TaskId.ToString();
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

                SelectedTaskIds = CurrentSelectedTaskIds;

                CurrentSelectedTaskIds.Clear();
                CurrentSelectedTaskIds = null;
            }
            else
            {
                gvTasks.DataSource = null;
                gvTasks.DataBind();
            }

            ValidateStatus();
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
    /// Establece las tablas
    /// </summary>
    public List<Int64> DeletedTasks
    {
        set
        {
            List<ITaskResult> LoadedeTasks = Tasks;

            foreach (Int64 deletedTaskId in value)
                LoadedeTasks.Add((ITaskResult)new DeletedTask(deletedTaskId));

            Tasks = LoadedeTasks;
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
    public event ChangedSelectedTask SelectedTaskChanged;
    public delegate void ChangedSelectedTask(Int64? taskId);

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
                    SelectedTaskChanged((Int64?)TaskId);
                else
                    SelectedTaskChanged(null);
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
   
    #endregion

    #region Constructores
    public ListadoTareas()
    {
        lbNoTasks = new Label();
        gvTasks = new GridView();
    }
    #endregion

    public void Select(Int64? taskId)
    {
        if (taskId.HasValue)
        {
            GridViewRow CurrentRow = null;
            Int64 RowTaskId;

            for (int i = 0; i < gvTasks.Rows.Count; i++)
            {
                CurrentRow = gvTasks.Rows[i];
                if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out RowTaskId))
                {
                    if (RowTaskId == taskId)
                    {
                        gvTasks.SelectedIndex = i;
                        SelectedTaskChanged((Int64?)RowTaskId);

                        break;
                    }
                }
            }

            if (null != CurrentRow)
            {
                CurrentRow.Dispose();
                CurrentRow = null;
            }
        }
        else
        {
            gvTasks.SelectedIndex = -1;
            SelectedTaskChanged(null);
        }
    }

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
            SelectedTaskChanged(null);
        }
        else
        {
            lbNoTasks.Visible = false;
            gvTasks.Visible = true;
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

                if (CurrentCheckBox.Checked != check)
                    CurrentCheckBox.Checked = check;
            }
        }

        if (null != CurrentPosibleCheckBox)
            CurrentPosibleCheckBox.Dispose();
        if (null != CurrentCheckBox)
            CurrentCheckBox.Dispose();
    }

     /// <summary>
     /// Representa una tarea que fue borrada en zamba
     /// </summary>
    private class DeletedTask
        : TaskResult
    {
        public DeletedTask(Int64 taskId)
            : base()
        {
            base.TaskId = taskId;
            base.Name = "Documento borrado";
            base.WorkId = 0;
        }
    }
   
}
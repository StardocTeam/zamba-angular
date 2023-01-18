using System;
using System.Web;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Collections.Generic;
using System.Web.UI;

/// <summary>
/// Lists a collection of Tasks and allows the client to select any of them.
/// </summary>
public partial class TasksList
    : System.Web.UI.UserControl
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

            ValidateStatus();
            ValidateRadioButtonStatus();

            CurrentSelectedTaskIds.Clear();
            CurrentSelectedTaskIds = null;

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

                            CurrentCheckBox.CheckedChanged -= new EventHandler(chbSelected_CheckedChanged);

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

                            CurrentCheckBox.CheckedChanged += new EventHandler(chbSelected_CheckedChanged);
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
    public event ChangedSelectedTasks SelectedTasksChanged;
    public delegate void ChangedSelectedTasks(List<Int64> taskIds);
    public event ChangedSelectedTask SelectedTaskChanged;
    public delegate void ChangedSelectedTask(Int64? taskId);
    public event ChangedSelectedWorkflow SelectedWorkflowChanged;
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
                ValidateRadioButtonStatus();
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
            ValidateRadioButtonStatus();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void chbSelected_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            //SelectedTasksChanged(SelectedTaskIds);
            List<Int64> SelectedStepIds = SelectedWorkflowIds;

            if (SelectedStepIds.Count == 1)
                SelectedWorkflowChanged((Int64?)SelectedStepIds[0], SelectedTaskIds);
            else
                SelectedWorkflowChanged(null, null);

            ValidateRadioButtonStatus();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        ValidateRadioButtonStatus();
    }
    protected void rblSeleccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            switch (rblSeleccion.SelectedValue)
            {
                case "0":
                    ChangeCheckBoxesState(true);
                    break;
                case "1":
                    ChangeCheckBoxesState(false);
                    break;
            }
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
            rblSeleccion.Visible = false;
            gvTasks.SelectedIndex = -1;
            gvTasks.Visible = false;
            fsSeleccionar.Visible = false;
            SelectedWorkflowChanged(null, null);
            SelectedTaskChanged(null);
        }
        else
        {
            lbNoTasks.Visible = false;
            rblSeleccion.Visible = true;
            gvTasks.Visible = true;
            fsSeleccionar.Visible = true;
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

        List<Int64> WorkflowIds = SelectedWorkflowIds;

        if (WorkflowIds.Count == 1)
            SelectedWorkflowChanged(WorkflowIds[0], SelectedTaskIds);
        else
            SelectedWorkflowChanged(null, null);
    }

    private void ValidateRadioButtonStatus()
    {
        Int32 SelectedTaskCount = SelectedTaskIds.Count;
        EventHandler eSelectedIndexChanged = new EventHandler(rblSeleccion_SelectedIndexChanged);
        rblSeleccion.SelectedIndexChanged -= eSelectedIndexChanged;

        if (SelectedTaskCount < gvTasks.Rows.Count && SelectedTaskCount > 0)
            rblSeleccion.SelectedValue = ((Int32)StepSelectionState.Personal).ToString();
        else if (SelectedTaskCount == 0)
            rblSeleccion.SelectedValue = ((Int32)StepSelectionState.Ninguno).ToString();
        else if (SelectedTaskCount == gvTasks.Rows.Count)
            rblSeleccion.SelectedValue = ((Int32)StepSelectionState.Todos).ToString();

        rblSeleccion.SelectedIndexChanged += eSelectedIndexChanged;
    }

    private enum StepSelectionState
    {
        Todos = 0,
        Ninguno = 1,
        Personal = 2
    }
}
//protected void rdbSelectAll_CheckedChanged(object sender, EventArgs e)
//{
//    if (this.rdbSelectAll.Checked)
//    {
//        try
//        {
//            ChangeCheckBoxesState(true);
//            SelectedTasksChanged(SelectedTaskIds);
//        }
//        catch (Exception ex)
//        {
//            ZClass.raiseerror(ex);
//        }
//        this.rdbSelectCustom.Checked = false;
//        this.rdbSelectNothing.Checked = false;
//    }

//}
//protected void rdbSelectNothing_CheckedChanged(object sender, EventArgs e)
//{
//    if (this.rdbSelectNothing.Checked)
//    {
//        try
//        {
//            ChangeCheckBoxesState(false);
//            SelectedTasksChanged(SelectedTaskIds);
//        }
//        catch (Exception ex)
//        {
//            ZClass.raiseerror(ex);
//        }
//        this.rdbSelectAll.Checked = false;
//        this.rdbSelectCustom.Checked = false;
//    }
//}
//protected void rdbSelectCustom_CheckedChanged(object sender, EventArgs e)
//{
//    this.rdbSelectNothing.Checked = false;
//    this.rdbSelectAll.Checked = false;
//}
//protected void btSelect_Click(object sender, EventArgs e)
//{
//    try
//    {
//        List<Int64> StepIds = new List<Int64>();
//        Control CurrentControl = null;
//        CheckBox CurrentCheckBox = null;
//        Int64 CurrentStepId;

//        foreach (GridViewRow CurrentRow in gvTasks.Rows)
//        {
//            CurrentControl = CurrentRow.FindControl(CHECKBOX_NAME);

//            if (null != CurrentControl && CurrentControl is CheckBox)
//            {
//                CurrentCheckBox = (CheckBox)CurrentControl;

//                if (CurrentCheckBox.Checked)
//                {
//                    if (Int64.TryParse(CurrentRow.Cells[TASK_ID_INDEX].Text, out CurrentStepId))
//                        StepIds.Add(CurrentStepId);
//                }
//            }
//        }

//        if (null != CurrentControl)
//            CurrentControl.Dispose();
//        if (null != CurrentCheckBox)
//            CurrentCheckBox.Dispose();

//    }
//    catch (Exception ex)
//    {
//        ZClass.raiseerror(ex);
//    }
//}
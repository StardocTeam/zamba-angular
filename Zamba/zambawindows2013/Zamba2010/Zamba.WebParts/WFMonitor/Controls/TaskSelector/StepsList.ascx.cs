using System;
using System.Web;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Collections.Generic;
using System.Web.UI;

/// <summary>
/// Lists a collection of Steps and allows the client to select any of them.
/// </summary>
public partial class StepsList
    : System.Web.UI.UserControl
{
    #region Constants
    /// <summary>
    /// Name of the Checkbox in the GridView TemplateField
    /// </summary>
    private const String CHECKBOX_NAME = "chbSelected";
    /// <summary>
    /// Message shown when no steps are loaded
    /// </summary>
    private const String MESSAGE_NO_TASKS = "No hay etapas para mostrar";
    /// <summary>
    /// Index of the Step id column
    /// </summary>
    private const Int32 STEP_ID_INDEX = 1;
    /// <summary>
    /// Index of the Step name column
    /// </summary>
    private const Int32 STEP_NAME_INDEX = 2;
    /// <summary>
    /// Index of the Step total task count column
    /// </summary>
    private const Int32 STEP_TOTAL_TASKS_COUNT = 3;
    /// <summary>
    /// Index of the Step expired tasks count column
    /// </summary>
    private const Int32 STEP_EXPIRED_TASKS_COUNT = 4;
    /// <summary>
    /// Represents the max name lenght shown in the gridview
    /// </summary>
    private const Int32 MAX_NAME_LENGHT = 20;
    #endregion

    #region Properties
    /// <summary>
    /// Get or Set the selected Steps
    /// </summary>
    public List<Int64> SelectedStepIds
    {
        get
        {
            List<Int64> Ids = new List<Int64>(gvSteps.Rows.Count);

            Control CurrentPosibleCheckBox = null;
            Int64 CurrentStepId;

            foreach (GridViewRow CurrentRow in gvSteps.Rows)
            {
                CurrentPosibleCheckBox = CurrentRow.FindControl(CHECKBOX_NAME);
                if (null != CurrentPosibleCheckBox && CurrentPosibleCheckBox is CheckBox)
                {
                    if (((CheckBox)CurrentPosibleCheckBox).Checked)
                    {
                        if (Int64.TryParse(CurrentRow.Cells[STEP_ID_INDEX].Text, out CurrentStepId))
                                Ids.Add(CurrentStepId);
                    }
                }
            }

            if (null != CurrentPosibleCheckBox)
                CurrentPosibleCheckBox.Dispose();

            return Ids;
        }
        set
        {
            if (null != value)
            {
                Control CurrentControl = null;
                CheckBox CurrentCheckBox = null;
                Int64 CurrentId = 0;

                foreach (GridViewRow CurrentRow in gvSteps.Rows)
                {
                    if (Int64.TryParse(CurrentRow.Cells[STEP_ID_INDEX].Text, out CurrentId))
                    {
                        CurrentControl = CurrentRow.FindControl(CHECKBOX_NAME);
                        if (null != CurrentControl && CurrentControl is CheckBox)
                        {
                            CurrentCheckBox = (CheckBox)CurrentControl;

                            CurrentCheckBox.CheckedChanged -= new EventHandler(chbSelected_CheckedChanged);

                            if (value.Contains(CurrentId))
                            {
                                if (!CurrentCheckBox.Checked)
                                    CurrentCheckBox.Checked = true;
                            }
                            else
                            {
                                if (CurrentCheckBox.Checked)
                                    CurrentCheckBox.Checked = false;
                            }

                            CurrentCheckBox.CheckedChanged -= new EventHandler(chbSelected_CheckedChanged);
                        }
                    }
                }

                if (null != CurrentControl)
                    CurrentControl.Dispose();
                if (null != CurrentCheckBox)
                    CurrentCheckBox.Dispose();

                SelectedStepChanged(SelectedStepIds);
            }
        }
    }

    /// <summary>
    /// Get or Set all the Steps
    /// </summary>
    public List<IWFStep> Steps
    {
        get
        {
            List<Int64> Ids = new List<Int64>(gvSteps.Rows.Count);
            Int64 CurrentStepId;

            foreach (GridViewRow CurrentRow in gvSteps.Rows)
            {
                if (Int64.TryParse(CurrentRow.Cells[STEP_ID_INDEX].Text, out CurrentStepId))
                    Ids.Add(CurrentStepId);
            }

            return Zamba.Services.Steps.GetSteps(Ids);
        }
        set
        {
            List<Int64> CurrentSelectedStepIds = SelectedStepIds;
            gvSteps.DataSource = null;

            gvSteps.DataSource = value;
            gvSteps.DataBind();

            Int32 BaseIndex = gvSteps.PageSize * gvSteps.PageIndex;
            Int32 CurrentIndex;
            IWFStep CurrentStep = null;
            GridViewRow CurrentRow = null;
            String ShortenedName = null;

            for (int i = 0; i < gvSteps.PageSize; i++)
            {
                CurrentIndex = BaseIndex + i;
                if (CurrentIndex <= value.Count && i < gvSteps.Rows.Count)
                {
                    CurrentStep = value[CurrentIndex];

                    CurrentRow = gvSteps.Rows[i];

                    CurrentRow.Cells[STEP_ID_INDEX].Text = CurrentStep.ID.ToString();

                    CurrentRow.Cells[STEP_NAME_INDEX].ToolTip = CurrentStep.Name;
                    if (CurrentStep.Name.Length > MAX_NAME_LENGHT)
                    {
                        ShortenedName = CurrentStep.Name.Substring(0, MAX_NAME_LENGHT - 3);
                        ShortenedName = ShortenedName + "...";
                        CurrentRow.Cells[STEP_NAME_INDEX].Text = ShortenedName;
                    }
                    else
                        CurrentRow.Cells[STEP_NAME_INDEX].Text = CurrentStep.Name;

                    CurrentRow.Cells[STEP_TOTAL_TASKS_COUNT].Text = CurrentStep.TasksCount.ToString();
                    CurrentRow.Cells[STEP_TOTAL_TASKS_COUNT].ToolTip = CurrentStep.TasksCount.ToString();

                    CurrentRow.Cells[STEP_EXPIRED_TASKS_COUNT].Text = CurrentStep.ExpiredTasksCount.ToString();
                    CurrentRow.Cells[STEP_EXPIRED_TASKS_COUNT].ToolTip = CurrentStep.ExpiredTasksCount.ToString();
                }
            }

            SelectedStepIds = CurrentSelectedStepIds;

            ValidateStatus();
            ValidateRadioButtonStatus();

            CurrentSelectedStepIds.Clear();
            CurrentSelectedStepIds = null;
        }
    }
    #endregion

    #region Events
    public delegate void ChangedSelectedSteps(List<Int64> stepIds);
    /// <summary>
    /// Ocurrs when the selected Steps are changed
    /// </summary>
    public event ChangedSelectedSteps SelectedStepChanged;

    public delegate void Refresh();
    /// <summary>
    /// Ocurrs when this controls needs to be refilled
    /// </summary>
    public event Refresh ForceRefresh;

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
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void gvSteps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvSteps.PageIndex = e.NewPageIndex;
            gvSteps.DataBind();

            ForceRefresh();

            SelectedStepChanged(SelectedStepIds); 
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
            SelectedStepChanged(SelectedStepIds);
            ValidateRadioButtonStatus();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void lkbSelectAll_Click(object sender, EventArgs e)
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
    protected void lkbSelectNone_Click(object sender, EventArgs e)
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
    protected void rblSeleccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            switch (rblSeleccion.SelectedValue)
            {
                case "0"://Todos
                    ChangeCheckBoxesState(true);
                    break;
                case "1": //Ninguno
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
        if (gvSteps.Rows.Count == 0)
        {
            lbNoSteps.Visible = true;
            lbNoSteps.Text = MESSAGE_NO_TASKS;
            gvSteps.Visible = false;
            fsSeleccionar.Visible = false;
        }
        else
        {
            lbNoSteps.Visible = false;
            fsSeleccionar.Visible = true;
            gvSteps.Visible = true;
        }
    }

    private void ChangeCheckBoxesState(bool check)
    {
        Control CurrentPosibleCheckBox = null;
        CheckBox CurrentCheckBox = null;

        foreach (GridViewRow CurrentRow in gvSteps.Rows)
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

        SelectedStepChanged(SelectedStepIds);
    }

    private void ValidateRadioButtonStatus()
    {
      
        Int32 SelectedTaskCount = SelectedStepIds.Count;
        
        EventHandler eSelectedIndexChanged = new EventHandler(rblSeleccion_SelectedIndexChanged);
        rblSeleccion.SelectedIndexChanged -= eSelectedIndexChanged;
        /*
        if (SelectedTaskCount < gvSteps.Rows.Count && SelectedTaskCount > 0)
            rblSeleccion.SelectedValue = ((Int32)TaskSelectionState.Personal).ToString();
        else if (SelectedTaskCount == 0)
            rblSeleccion.SelectedValue = ((Int32)TaskSelectionState.Ninguno).ToString();
        else if (SelectedTaskCount == gvSteps.Rows.Count)
            rblSeleccion.SelectedValue = ((Int32)TaskSelectionState.Todos).ToString();
        */
        rblSeleccion.SelectedIndexChanged += eSelectedIndexChanged;
        
    }

    private enum TaskSelectionState
    {
        Todos = 0,
        Ninguno = 1,
        Personal = 2
    }
}
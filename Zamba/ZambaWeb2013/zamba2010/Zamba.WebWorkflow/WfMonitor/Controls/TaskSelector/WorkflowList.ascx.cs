using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

/// <summary>
/// Lists a collection of Workflows and allows the client to select any of them.
/// </summary>
public partial class WorkflowList 
    : UserControl
{
    #region Constantes
    /// <summary>
    /// Name of the Checkbox in the GridView TemplateField
    /// </summary>
    private const String CHECKBOX_NAME = "chbSelected";
    /// <summary>
    /// Message shown when no workflows are loaded
    /// </summary>
    private const String MESSAGE_NO_WORKFLOWS = "No hay workflows para mostrar";
    /// <summary>
    /// Index of the Workflow id column
    /// </summary>
    private const Int32 WORKFLOW_ID_INDEX = 1;
    /// <summary>
    /// Index of the Workflow name column
    /// </summary>
    private const Int32 WORKFLOW_NAME_INDEX = 2;
    /// <summary>
    /// Index of the Workflow total task count column
    /// </summary>
    private const Int32 WORKFLOW_TOTAL_TASKS_COUNT = 3;
    /// <summary>
    /// Index of the Workflow expired tasks count column
    /// </summary>
    private const Int32 WORKFLOW_EXPIRED_TASKS_COUNT = 4;
    /// <summary>
    /// Represents the max name lenght shown in the gridview
    /// </summary>
    private const Int32 MAX_NAME_LENGHT = 20;
    #endregion

    #region Propiedades
    /// <summary>
    /// The selected Workflow Id. If no Workflow is selected the value is null.
    /// </summary>
    public List<Int64> SelectedWorfklowIds
    {
        get
        {
            List<Int64> Ids = new List<Int64>();

            Control CurrentPosibleCheckBox = null;
            Control CurrentPosibleLabel = null;
            Int64 CurrentId;

            foreach (GridViewRow CurrentRow in gvWorkflows.Rows)
            {
                CurrentPosibleCheckBox = CurrentRow.FindControl(CHECKBOX_NAME);
                if (null != CurrentPosibleCheckBox && CurrentPosibleCheckBox is CheckBox)
                {
                    if (((CheckBox)CurrentPosibleCheckBox).Checked)
                    {
                        if (Int64.TryParse(CurrentRow.Cells[WORKFLOW_ID_INDEX].Text, out CurrentId))
                            Ids.Add(CurrentId);
                    }
                }
            }

            if (null != CurrentPosibleCheckBox)
                CurrentPosibleCheckBox.Dispose();
            if (null != CurrentPosibleLabel)
                CurrentPosibleLabel.Dispose();

            return Ids;
        }
        set
        {
            if (null != value)
            {
                Control CurrentPosibleCheckBox = null;
                Control CurrentPosibleWokflowIdLabel = null;
                CheckBox CurrentCheckBox = null;
                Int64 CurrentIndex = 0;

                foreach (GridViewRow CurrentRow in gvWorkflows.Rows)
                {
                    if (Int64.TryParse(CurrentRow.Cells[WORKFLOW_ID_INDEX].Text, out CurrentIndex))
                    {
                        CurrentPosibleCheckBox = CurrentRow.FindControl(CHECKBOX_NAME);

                        if (null != CurrentPosibleCheckBox && CurrentPosibleCheckBox is CheckBox)
                        {
                            CurrentCheckBox = (CheckBox)CurrentPosibleCheckBox;

                            if (value.Contains(CurrentIndex))
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
                
                if (null != CurrentPosibleCheckBox)
                    CurrentPosibleCheckBox.Dispose();
                if (null != CurrentPosibleWokflowIdLabel)
                    CurrentPosibleWokflowIdLabel.Dispose();
                if (null != CurrentCheckBox)
                    CurrentCheckBox.Dispose();

                ValidateStatus();
            }
        }
    }
    #endregion

    #region Events
    /// <summary>
    /// Ocurrs when the selected Workflows are changed
    /// </summary>
    public event ChangedSelectedWorkflows SelectedWorkflowChanged;
    public delegate void ChangedSelectedWorkflows(List<Int64> workflowIds);

    protected void btUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            Refresh();
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
            SelectedWorkflowChanged(SelectedWorfklowIds);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadWorkflows();
                ValidateStatus();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
    protected void gvWorkflows_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvWorkflows.PageIndex = e.NewPageIndex;
            gvWorkflows.DataBind();

            LoadWorkflows();

            SelectedWorkflowChanged(SelectedWorfklowIds);
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
    /// Loads every Workflow in the ListBox lstWorkflows
    /// </summary>
    private void LoadWorkflows()
    {
        Int32 BaseIndex = gvWorkflows.PageSize * gvWorkflows.PageIndex;
        Int32 CurrentIndex;

        List<IWorkFlow> WorkflowsList = Workflow.GetWorkflows();

        gvWorkflows.DataSource = WorkflowsList;
        gvWorkflows.DataBind();

        IWorkFlow CurrentWorklow = null;
        GridViewRow CurrentRow = null;
        String ShortenedName = null;

        if (WorkflowsList.Count > 0)
        {
            for (int i = 0; i < gvWorkflows.PageSize; i++)
            {
                CurrentIndex = BaseIndex + i;
                if (CurrentIndex <= WorkflowsList.Count && i < gvWorkflows.Rows.Count)
                {
                    CurrentWorklow = WorkflowsList[CurrentIndex];

                    CurrentRow = gvWorkflows.Rows[i];

                    CurrentRow.Cells[WORKFLOW_ID_INDEX].Text = CurrentWorklow.ID.ToString();

                    if (CurrentWorklow.Name.Length > MAX_NAME_LENGHT)
                    {
                        ShortenedName = CurrentWorklow.Name.Substring(0, MAX_NAME_LENGHT - 3);
                        ShortenedName = ShortenedName + "...";
                        CurrentRow.Cells[WORKFLOW_NAME_INDEX].Text = ShortenedName;
                    }
                    else
                        CurrentRow.Cells[WORKFLOW_NAME_INDEX].Text = CurrentWorklow.Name;

                    CurrentRow.Cells[WORKFLOW_NAME_INDEX].ToolTip = CurrentWorklow.Name;

                    CurrentRow.Cells[WORKFLOW_TOTAL_TASKS_COUNT].Text = CurrentWorklow.TasksCount.ToString();
                    CurrentRow.Cells[WORKFLOW_TOTAL_TASKS_COUNT].ToolTip = CurrentWorklow.TasksCount.ToString();

                    CurrentRow.Cells[WORKFLOW_EXPIRED_TASKS_COUNT].Text = CurrentWorklow.ExpiredTasksCount.ToString();
                    CurrentRow.Cells[WORKFLOW_EXPIRED_TASKS_COUNT].ToolTip = CurrentWorklow.ExpiredTasksCount.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Shows or Hides this control according to its status
    /// </summary>
    private void ValidateStatus()
    {
        if (gvWorkflows.Rows.Count == 0)
        {
            lbNoWorklows.Visible = true;
            lbNoWorklows.Text = MESSAGE_NO_WORKFLOWS;
            gvWorkflows.Visible = false;
            lnkAll.Visible = false;
            lnkNone.Visible = false;
            btActualizar.Visible =  false;
            btView.Visible = false;
        }
        else
        {
            lbNoWorklows.Visible = false;
            gvWorkflows.Visible = true;
            lnkAll.Visible = true;
            lnkNone.Visible = true;
            btActualizar.Visible = true;
            btView.Visible = true;
        }
    }

    /// <summary>
    /// Refreshes the list of Workflows
    /// </summary>
    public void Refresh()
    {
        LoadWorkflows();
    }

    /// <summary>
    /// Checks or Unchecks every checkbox in gvTask
    /// </summary>
    /// <param name="check"></param>
    private void ChangeCheckBoxesState(Boolean check)
    {
        Control CurrentPosibleCheckBox = null;
        CheckBox CurrentCheckBox = null;

        foreach (GridViewRow CurrentRow in gvWorkflows.Rows)
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

        SelectedWorkflowChanged(SelectedWorfklowIds);
    }


    public WorkflowList()
    {
        lbNoWorklows = new Label();
        lnkAll = new LinkButton();
        lnkNone = new LinkButton();
        gvWorkflows = new GridView();
        btActualizar = new Button();
        btView = new Button();
    }
} 
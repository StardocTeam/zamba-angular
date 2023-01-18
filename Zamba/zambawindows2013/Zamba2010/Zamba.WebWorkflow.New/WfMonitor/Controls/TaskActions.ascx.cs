using System;
using System.Data;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic ;
using Zamba.Core;
using Zamba.Services;

public partial class TaskActions
    : UserControl
{
    #region Constantes
    private static char SEPARATOR = '-';
    #endregion

    #region Propiedades

    /// <summary>
    /// Gets or Sets the selected task ids
    /// </summary>
    public List<Int64> SelectedTaskIds
    {
        get
        {
            String[] ParsedTaskIds = hfTaskIds.Value.Split(SEPARATOR);
            List<Int64> TaskIds = new List<Int64>(ParsedTaskIds.Length);

            Int64 CurrentTaskId;
            foreach (String ParsedTaskId in ParsedTaskIds)
            {
                CurrentTaskId = 0;
                if (Int64.TryParse(ParsedTaskId, out CurrentTaskId))
                    TaskIds.Add(CurrentTaskId);
            }


            return TaskIds;
        }
        set
        {
            hfTaskIds.Value = String.Empty;

            if (null != value && value.Count > 0)
            {
                StringBuilder ParsedValue = new StringBuilder();
                foreach (Int64 CurrentTaskId in value)
                {
                    ParsedValue.Append(CurrentTaskId.ToString());
                    ParsedValue.Append(SEPARATOR);
                }
                ParsedValue.Remove(ParsedValue.Length - 1, 1);

                hfTaskIds.Value = ParsedValue.ToString();

                ParsedValue.Remove(0, ParsedValue.Length);
                ParsedValue = null;
            }
        }
    }

    /// <summary>
    /// Get or Sets the selected Workflow id
    /// </summary>
    public Int64? SelectedWorkflowId
    {
        set
        {
            if (value.HasValue)
            {
                ChangeStatus(true);

                hfStepId.Value = value.Value.ToString();

                List<IWFStep> StepList = Steps.GetStepsByWorkflow(value.Value);

                ddlDerivarEtapas.Items.Clear();

                foreach (IWFStep CurrentStep in StepList)
                    ddlDerivarEtapas.Items.Add(new ListItem(CurrentStep.Name, CurrentStep.ID.ToString()));

                StepList.Clear();
                StepList = null;

                LoadUsers();
            }
            else
                ChangeStatus(false);
        }
        get
        {
            Int64 TryValue ;
            Int64? Value = null ;

            if (Int64.TryParse(hfStepId.Value, out TryValue))
                Value = TryValue;

            return Value ;
        }
    }
    #endregion

    #region Eventos
    public event RefreshTasks TasksChanged;
    public delegate void RefreshTasks();
    public event RefreshSteps StepsChanged;
    public delegate void RefreshSteps();
    public delegate void AssignTask(Boolean boolAssign, Int64 selectedUserID);
    public event AssignTask DoAssign;

    protected void btDerivarEtapas_Click(object sender, EventArgs e)
    {
        List<Int64> TaskIds = null;
        String UserName = null;
        try
        {
            Int64 StepId;
            if (Int64.TryParse(ddlDerivarEtapas.SelectedValue, out StepId))
            {
                TaskIds = SelectedTaskIds;
                UserName = "Sin Asignar";
                Int64 UserId = -1;

                foreach (Int64 TaskId in TaskIds)
                    Tasks.DeriveTask(TaskId, StepId, 0, UserName, UserId, DateTime.Now, false);

                TasksChanged();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            if (null != TaskIds)
            {
                TaskIds.Clear();
                TaskIds = null;
            }

            UserName = null;
        }
    }
    protected void btAsignarUsuarios_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlAsignarUsuarios.SelectedValue))
        {
            Int64 selectedUserID = Int64.Parse(ddlAsignarUsuarios.SelectedValue);
            DoAssign(true, selectedUserID);
        }
    }
    protected void btQuitar_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (Int64 taskID in SelectedTaskIds)
                WFTaskBussines.Remove(taskID, chBorrarDocumento.Checked);

            TasksChanged();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void btDesasignarUsuarios_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(ddlAsignarUsuarios.SelectedValue))
        {
            Int64 selectedUserID = Int64.Parse(ddlAsignarUsuarios.SelectedValue);
            DoAssign(false, selectedUserID);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (SelectedWorkflowId.HasValue)
                    ChangeStatus(true);
                else
                    ChangeStatus(false);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }
    #endregion

    /// <summary>
    /// Sets the status of the control
    /// </summary>
    /// <param name="enable"></param>
    private void ChangeStatus(Boolean enable)
    {
        tbcTaskActions.Enabled = enable;

        if (!enable)
        {
            ddlDerivarEtapas.Items.Clear();
            ddlAsignarUsuarios.Items.Clear();
        }
    }

    /// <summary>
    /// Clear all loaded Controls
    /// </summary>
    public void Clear()
    {
        ChangeStatus(false);
    }

    private void LoadUsers()
    {
        ICollection UserNamesCollection = null;
        try
        {
            UserNamesCollection = UserBusiness.GetUsersNamesAsICollection();

            if (null != UserNamesCollection)
            {
                ddlAsignarUsuarios.Items.Clear();

                ddlAsignarUsuarios.DataSource = UserNamesCollection;
                ddlAsignarUsuarios.DataTextField = "NombreCompleto";
                ddlAsignarUsuarios.DataValueField = "ID";

                ddlAsignarUsuarios.DataBind();

                ddlAsignarUsuarios.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally
        {
            UserNamesCollection = null;
        }
    }
 
}

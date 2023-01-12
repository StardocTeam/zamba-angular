using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;
using System.Data;

public partial class TaskActions
    : UserControl
{
    #region Constantes
    /// <summary>
    /// Caracter que se usa para guardar los ids de tareas seleccionados en CSV
    /// </summary>
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
    /// <summary>
    /// Le dice al control padre que debe recargar las tareas para actualizar el estado de todas sus tareas
    /// </summary>
    public delegate void RefreshTasks();
    public event RefreshSteps StepsChanged;
    /// <summary>
    /// Le dice al control padre que debe recargar las etapas para actualizar el estado de todas sus etapas
    /// </summary>
    public delegate void RefreshSteps();
    /// <summary>
    /// Le dice al control padre que debe asignar o desasignar las tareas seleccionadas a el usuario especificado.
    /// </summary>
    /// <param name="boolAssign"></param>
    /// <param name="selectedUserID"></param>
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

                TaskResult Task = null ;
                IWFStep Step = Steps.GetStep(StepId);
                foreach (Int64 TaskId in TaskIds)
                {
                    Task = (TaskResult)Tasks.GetTask(TaskId);
                    DataSet ds = WFRulesBussines.GetRulesByStepId(Step.ID);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if ((TypesofRules)(Int32.Parse(row["ParentType"].ToString())) == TypesofRules.Entrada)
                            {
                                WFRulesBussines WFRB = new WFRulesBussines();
                                WFRB.Execute(Int64.Parse(row[0].ToString()), StepId, SelectedTaskIds, Server.MapPath("~/bin/"));
                            }
                        }

                        WFBusiness.DistributeTask(ref Task, Step.ID);
                    }
                }

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

    /// <summary>
    /// Carga los usuarios en el control.
    /// </summary>
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

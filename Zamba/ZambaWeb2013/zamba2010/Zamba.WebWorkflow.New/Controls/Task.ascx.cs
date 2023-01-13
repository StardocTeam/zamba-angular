using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Services;
using Zamba.Core;

public partial class Task : UserControl
{
    #region Propiedades
    /// <summary>
    /// Gets or Sets the current Task Id
    /// </summary>
    public Int64? TaskId
    {
        get
        {
            Int64? NullableValue;
            Int64 Value;

            if (Int64.TryParse(hfTaskId.Value, out Value))
                NullableValue = Value;
            else
                NullableValue = null;

            return NullableValue;
        }
        set
        {
            if (value.HasValue)
            {
                LoadTask(value.Value);
                hfTaskId.Value = value.Value.ToString();
            }
            else
            {
                Clear();
                hfTaskId.Value = String.Empty;
            }
        }
    }
    #endregion

    #region Eventos
    protected void btActualizar_Click(object sender, EventArgs e)
    {
        try
        {
            if (TaskId.HasValue)
                LoadTask(TaskId.Value);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region Constructores
    public Task()
    {
        hfTaskId = new HiddenField();

        tblTaskInformation = new HtmlTable();

        lbAsignedBy = new Label();
        lbAsignedByValue = new Label();
        lbAsignedTo = new Label();
        lbAsignedToValue = new Label();
        lbCheckIn = new Label();
        lbCheckInValue = new Label();
        lbExpirationDate = new Label();
        lbExpirationDateValue = new Label();
        lbIsExpired = new Label();
        lbIsExpiredValue = new Label();
        lbState = new Label();
        lbStateValue = new Label();
        lbTaskState = new Label();
        lbTaskStateValue = new Label();
    }
    #endregion

    private void LoadTask(Int64 taskId)
    {
        ITaskResult CurrentTask = Tasks.GetTask(taskId);

        if (null != CurrentTask)
        {

            hfTaskId.Value = taskId.ToString();

            using (IUser AsignedBy = UserBusiness.GetUserById(CurrentTask.AsignedById))
            {
                if (null != AsignedBy)
                {
                    lbAsignedByValue.Text = AsignedBy.Nombres;
                    lbAsignedBy.ToolTip = AsignedBy.Nombres;
                }
                else
                {
                    lbAsignedByValue.Text = "NADIE";
                    lbAsignedByValue.ToolTip = "NADIE";
                }
            }

            using (IUser AsignedTo = UserBusiness.GetUserById(CurrentTask.AsignedToId))
            {
                if (null != AsignedTo)
                {
                    lbAsignedToValue.Text = AsignedTo.Nombres;
                    lbAsignedToValue.ToolTip = AsignedTo.Nombres;
                }
                else
                {
                    lbAsignedToValue.Text = "NADIE";
                    lbAsignedToValue.ToolTip = "NADIE";
                }
            }

            lbCheckInValue.Text = CurrentTask.CheckIn.ToString();
            lbCheckInValue.ToolTip = CurrentTask.CheckIn.ToString();

            lbExpirationDateValue.Text = CurrentTask.ExpireDate.ToString();
            lbExpirationDateValue.ToolTip = CurrentTask.ExpireDate.ToString();

            if (CurrentTask.IsExpired)
            {
                lbIsExpiredValue.Text = "SI";
                lbIsExpiredValue.ToolTip = "SI";
            }
            else
            {
                lbIsExpiredValue.Text = "NO";
                lbIsExpiredValue.ToolTip = "NO";
            }

            if (null != CurrentTask.State && !String.IsNullOrEmpty(CurrentTask.State.Name))
            {
                lbStateValue.Text = CurrentTask.State.Name;
                lbStateValue.ToolTip = CurrentTask.State.Name;
            }
            else
            {
                lbStateValue.Text = "NINGUNO";
                lbStateValue.ToolTip = "NINGUNO";
            }

            lbTaskStateValue.Text = CurrentTask.TaskState.ToString();
            lbTaskStateValue.ToolTip = CurrentTask.TaskState.ToString();
        }
        else
            Clear();

        CurrentTask.Dispose();
        CurrentTask = null;

        Visible = true;
    }

    /// <summary>
    /// Clear the Task information
    /// </summary>
    public void Clear()
    {
        lbAsignedByValue.Text = String.Empty;
        lbAsignedToValue.Text = String.Empty;
        lbCheckInValue.Text = String.Empty;
        lbExpirationDateValue.Text = String.Empty;
        lbIsExpiredValue.Text = String.Empty;
        lbStateValue.Text = String.Empty;
        lbTaskStateValue.Text = String.Empty;
    }

}
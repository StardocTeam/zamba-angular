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

public partial class Task 
    : UserControl
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
    //protected void btActualizar_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (TaskId.HasValue)
    //            LoadTask(TaskId.Value);
    //    }
    //    catch (Exception ex)
    //    {
    //        ZClass.raiseerror(ex);
    //    }
    //}
    #endregion

    private void LoadTask(Int64 taskId)
    {
        ITaskResult CurrentTask = Tasks.GetTask(taskId);

        if (null != CurrentTask)
        {

            hfTaskId.Value = taskId.ToString();

            if (CurrentTask.AsignedById != 0)
                lbAsignedByValue.Text = Users.GetUserorGroupNamebyId(CurrentTask.AsignedById);
            else
                lbAsignedByValue.Text = String.Empty;

            if (CurrentTask.AsignedToId != 0)
                lbAsignedToValue.Text = Users.GetUserorGroupNamebyId(CurrentTask.AsignedToId);
            else
                lbAsignedToValue.Text = String.Empty;

            lbCheckInValue.Text = CurrentTask.CheckIn.ToString();
            lbExpirationDateValue.Text = CurrentTask.ExpireDate.ToString();

            if (CurrentTask.IsExpired)
                lbIsExpiredValue.Text = "SI";
            else
                lbIsExpiredValue.Text = "NO";

            if (null != CurrentTask.State)
                lbStateValue.Text = CurrentTask.State.Name;

            lbTaskStateValue.Text = CurrentTask.TaskState.ToString();
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
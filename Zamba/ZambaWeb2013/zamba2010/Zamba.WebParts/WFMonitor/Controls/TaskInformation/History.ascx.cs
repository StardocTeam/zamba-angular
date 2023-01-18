using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;
using Zamba.Services;

public partial class History 
    : UserControl
{
    #region Propiedades
    /// <summary>
    /// Gets or Sets the Task Id
    /// </summary>
    public Int64 TaskId
    {
        get
        {
            Int64 Value = -1;

            Int64.TryParse(hfTaskId.Value, out Value);

            return Value;
        }
        set
        {
            LoadHistory(value);
            hfTaskId.Value = value.ToString();
        }
    }

    #endregion

    #region Eventos
    //protected void btRefresh_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        LoadHistory(TaskId);
    //    }
    //    catch (Exception ex)
    //    {
    //        ZClass.raiseerror(ex);
    //    }
    //}
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHistory.PageIndex = e.NewPageIndex;
            LoadHistory(TaskId);
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    private void LoadHistory(Int64 taskId)
    {

        gvHistory.DataSource = null;
        gvHistory.DataSource = Tasks.GetTaskHistory(taskId);
        gvHistory.DataBind();
    }

    /// <summary>
    /// Clears the inner Controls
    /// </summary>
    public void Clear()
    {
        gvHistory.DataSource = null;
        gvHistory.DataBind();
    }

}
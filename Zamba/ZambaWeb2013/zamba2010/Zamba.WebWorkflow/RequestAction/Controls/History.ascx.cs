using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;

public partial class History 
    : UserControl
{

    #region Constantes
    private const String NO_HISTORY_MESSAGE = "No hay historial para mostrar";
    #endregion

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
            try
            {
                LoadHistory(value);
                hfTaskId.Value = value.ToString();
            }
            catch (Exception ex)
            { HandleException(ex); }
        }
    }

    #endregion

    #region Eventos
    protected void btRefresh_Click(object sender, EventArgs e)
    {
        try
        {
            LoadHistory(TaskId);
        }
        catch (Exception ex)
        {
            HandleException(ex);
            ZClass.raiseerror(ex);
        }
    }
    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvHistory.PageIndex = e.NewPageIndex;
            LoadHistory(TaskId);
        }
        catch (Exception ex)
        {
            HandleException(ex);
            ZClass.raiseerror(ex);
        }
    }
    #endregion

    #region Constantes
    public History()
    {
        hfTaskId = new HiddenField();
        gvHistory = new GridView();
    }
    #endregion

    private void LoadHistory(Int64 taskId)
    {
        gvHistory.DataSource = null;
        DataSet DsHistory = Tasks.GetTaskHistory(taskId);

        if (null == DsHistory || DsHistory.Tables.Count == 0 || DsHistory.Tables[0].Rows.Count == 0)
        {
            gvHistory.Visible = false;

            lbNoHistory.Visible = true;
            lbNoHistory.Text = NO_HISTORY_MESSAGE;
        }
        else
        {
            lbNoHistory.Visible = false;
            gvHistory.Visible = true;

            gvHistory.DataSource = Tasks.GetTaskHistory(taskId);
            gvHistory.DataBind();
        }
    }

    /// <summary>
    /// Clears the inner Controls
    /// </summary>
    public void Clear()
    {
        gvHistory.DataSource = null;
        gvHistory.DataBind();
    }

    private void HandleException(Exception ex)
    {
        //pnlHistory.Visible = false;
        //UcError.Visible = true;
        //UcError.Error = ex;

    }
}
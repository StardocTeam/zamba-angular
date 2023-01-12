using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Zamba.Core;

public partial class Indexs 
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

            if (Int64.TryParse(hdTaskId.Value, out Value))
                NullableValue  = Value;
            else
                NullableValue = null;

            return Value;
        }
        set
        {
            if (value.HasValue)
            {
                LoadIndexs(value.Value);
                hdTaskId.Value = value.Value.ToString();
            }
            else
            {
                Clear();
                hdTaskId.Value = String.Empty;
            }

        }
    }
    #endregion

    private void LoadIndexs(Int64 taskId)
    {
        Visible = true;

        hdTaskId.Value = taskId.ToString();

        List<IIndex> TaskIndexs = Zamba.Services.Index.GetIndexByTaskId(taskId);

        tblIndices.Controls.Clear();

        TableRow CurrentRow = null;
        TableCell TcIndexName = null;
        TableCell TcIndexValue = null;
        Label LbValue = null;

        foreach (IIndex CurrentIndex in TaskIndexs)
        {
            TcIndexName = new TableCell();
            TcIndexName.Text = CurrentIndex.Name;
            TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");

            LbValue = new Label();
            LbValue.Enabled = false;
            LbValue.Text = CurrentIndex.Data;
            LbValue.ToolTip = CurrentIndex.Data;

            TcIndexValue = new TableCell();
            TcIndexValue.Controls.Add(LbValue);

            CurrentRow = new TableRow();
            CurrentRow.Cells.Add(TcIndexName);
            CurrentRow.Cells.Add(TcIndexValue);

            tblIndices.Rows.Add(CurrentRow);
        }

        #region Dispose
        if (null != CurrentRow)
        {
            CurrentRow.Dispose();
            CurrentRow = null;
        }

        if (null != TcIndexName)
        {
            TcIndexName.Dispose();
            TcIndexName = null;
        }
        if (null != TcIndexValue)
        {
            TcIndexValue.Dispose();
            TcIndexValue = null;
        }
        if (null != LbValue)
        {
            LbValue.Dispose();
            LbValue = null;
        }
        if (null != TaskIndexs)
        {
            TaskIndexs.Clear();
            TaskIndexs = null;
        }

        #endregion
    }

    /// <summary>
    /// Clears the inner controls 
    /// </summary>
    public void Clear()
    {
        tblIndices.Controls.Clear();
    }

    #region Constructores
    public Indexs()
    {
        tblIndices = new Table();
        lbTaskId = new Label();
        hdTaskId = new HiddenField();
    } 
    #endregion
 
}
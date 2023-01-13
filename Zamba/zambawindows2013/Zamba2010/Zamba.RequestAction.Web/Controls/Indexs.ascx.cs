using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;
using System.Collections.Generic;

public partial class Indexs : UserControl
{
   #region Constantes
   private String LABEL_CSS_STYLE = "Label";
   #endregion

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
            NullableValue = Value;
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

   #region Eventos
   protected void btUndoChanges_Click(object sender, EventArgs e)
   {
      try
      {
         if (TaskId.HasValue)
            LoadIndexs(TaskId.Value);

      }
      catch (Exception ex)
      {
         ZClass.raiseerror(ex);
      }
   }

   #endregion

   #region Constructor
   public Indexs()
   {
      hdTaskId = new HiddenField();
      lbTaskId = new Label();
      tblIndices = new Table();
   }
   #endregion

   private void LoadIndexs(Int64 taskId)
   {
      this.Visible = true;
      hdTaskId.Value = taskId.ToString();

      tblIndices.Controls.Clear();

      List<IIndex> TaskIndexs = Zamba.Services.Index.GetIndexByTaskId(taskId);

      TableRow CurrentRow = null;
      TableCell TcIndexName = null;
      TableCell TcIndexValue = null;
      Label LbValue = null;
      Boolean IsFirstRow = true;
      foreach (IIndex CurrentIndex in TaskIndexs)
      {
         if (!String.IsNullOrEmpty(CurrentIndex.Name))
         {

            #region Index Name
            TcIndexName = new TableCell();
            TcIndexName.Text = CurrentIndex.Name;
            TcIndexName.ToolTip = CurrentIndex.Name;
            TcIndexName.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
            TcIndexName.Controls.Add(lbTaskId);
            #endregion

            #region Index Value
            LbValue = new Label();
            LbValue.Enabled = false;
            LbValue.CssClass = LABEL_CSS_STYLE;

            if (String.IsNullOrEmpty(CurrentIndex.Data))
            {
               LbValue.Text = "NINGUNO";
               LbValue.ToolTip = "NINGUNO";
            }
            else
            {
               LbValue.Text = CurrentIndex.Data;
               LbValue.ToolTip = CurrentIndex.Data;
            }

            TcIndexValue = new TableCell();
            TcIndexValue.Controls.Add(LbValue);
            #endregion

            if (IsFirstRow)
               CurrentRow = new TableRow();
            else
            {
               CurrentRow.Cells.Add(TcIndexName);
               CurrentRow.Cells.Add(TcIndexValue);
               tblIndices.Rows.Add(CurrentRow);
            }

            CurrentRow.Cells.Add(TcIndexName);
            CurrentRow.Cells.Add(TcIndexValue);

            IsFirstRow = !IsFirstRow;
         }
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
}
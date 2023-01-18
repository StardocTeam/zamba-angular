using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Zamba.Services;
using Zamba.Core;
using Zamba.Web.App_Code.Helpers;

public partial class Views_UC_Grid_ucDocAssociatedGrid : System.Web.UI.UserControl
{
    private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
    private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";
    List<string> columnsNonVisibles ;

    public void loadAssociatedDocs(ITaskResult task)
    {
        loadAssociatedDocs((IResult)task);
    }
    public void loadAssociatedDocs(IResult result)
    {
        try
        {
            DataTable dt = new STasks().getAsociatedDTResultsFromResult(result, 0, false, Zamba.Membership.MembershipHelper.CurrentUser, true);
            FormatGridview();
            generateGridColumns(dt);
            FilterColumnsVisibility(dt, result);
            BindGrid(dt);
        }
        catch (Exception ex)
        {
            lblMsg.Text = "No se han podido cargar los asociados.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Font.Size = 10;
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void FormatGridview()
    {
        try
        {
            grdDocAssociated.AutoGenerateColumns = false;
            grdDocAssociated.ShowFooter = false;

            String[] a = { "DOC_TYPE_ID", "DOC_ID", "Task_Id" };

            grdDocAssociated.Columns.Clear();

            HyperLinkField colver = new HyperLinkField
            {
                ShowHeader = true,
                HeaderText = "Ver",
                Target = "_blank",
                Text = "Ver",
                DataTextFormatString = "<img src=\"../../Tools/icono.aspx?id={0}\" border=0/ style=\"Height:24px\">",
                DataTextField = "ICON_ID"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"../../WF/TaskSelector.ashx?doctype={0}&docid={1}&taskid={2}&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

            grdDocAssociated.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
    protected void grdDocAssociated_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#ceedfc'");
        //    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=''");
        //    e.Row.Attributes.Add("style", "cursor:pointer;");
        //    e.Row.Attributes.Add("onclick", "location='patron_detail.aspx?id=" + e.Row.Cells[0].Text + "'");
        //}
        if(e.Row.RowType==DataControlRowType.Header)
        {
            e.Row.Attributes.Add("style", "padding:25px;text-align:center");
        }
        else
        {
            e.Row.Attributes.Add("style", "padding:20px");
        }
        
    }

    private void generateGridColumns(DataTable _dt)
    {
        try
        {
            if (_dt != null && _dt.Columns.Count > 0)
            {
                string _caption;
                foreach (DataColumn c in _dt.Columns)
                {
                    if (c.ColumnName != "THUMB")
                    {
                        _caption = c.Caption.Replace(" ", "_").Replace("-", "_").Replace("%", "").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace("__", "_");

                        BoundField f = new BoundField
                        {
                            //DataField = c.Caption.Replace(" ", "_"),
                            DataField = _caption,
                            ShowHeader = true,
                            //HeaderText = c.Caption.Replace(" ","_"),
                            HeaderText = _caption,
                            //SortExpression = c.Caption.Replace(" ", "_") + " ASC"
                            SortExpression = $"{_caption} ASC"
                        };

                        grdDocAssociated.Columns.Add(f);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void formatValues(DataTable _dt)
    {
        string headerText;
        DateTime date;

        if (_dt.Rows.Count > 0)
        {
            for (int col = 0; col < grdDocAssociated.Columns.Count; col++)
            {
                string colname = grdDocAssociated.Columns[col].HeaderText;

                if (colname.ToLower() == "Original")
                    grdDocAssociated.Columns[col].Visible = false;

                if (GridHelper.GetVisibility(colname.ToLower(), GridHelper.GridType.Task) == false)
                {
                    grdDocAssociated.Columns[col].Visible = false;
                }
                else
                {
                    if (_dt.Columns.Contains(colname))
                    {
                        if (_dt.Columns[colname].DataType == Type.GetType("System.DateTime"))
                        {
                            for (int row = 0; row < grdDocAssociated.Rows.Count; row++)
                            {
                                if (string.IsNullOrEmpty(grdDocAssociated.Rows[row].Cells[col].Text))
                                    continue;

                                string value = grdDocAssociated.Rows[row].Cells[col].Text;

                                if (DateTime.TryParse(value, out date))
                                    grdDocAssociated.Rows[row].Cells[col].Text = date.ToShortDateString();
                            }
                        }
                    }
                }
            }

            foreach (DataControlField col in grdDocAssociated.Columns)
            {
                headerText = col.HeaderText;

                if (headerText.StartsWith("I") && GridHelper.IsNumeric(headerText.Substring(1, headerText.Length - 1)))
                    col.Visible = false;
            }
        }

        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "No hay registros para mostrar.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Font.Size = 10;
        }
    }

    private void BindGrid(DataTable dt)
    {
        try
        {
            grdDocAssociated.DataSource = dt;
            grdDocAssociated.DataBind();

            formatValues(dt);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void FilterColumnsVisibility(DataTable dt, IResult result)
    {
        //Creo esta lista porque no puedo eliminar columnas el vuelo (columnas que no deben verse)
        List<string> ColumnsToRemove = new List<string>();
        foreach (DataControlField c in grdDocAssociated.Columns)
        {
            if ((c.HeaderText.ToLower().StartsWith("i") && IsNumeric(c.HeaderText.Remove(0, 1))) || (GridColumns.ColumnsVisibility.ContainsKey(c.HeaderText.ToLower()) && GridColumns.ColumnsVisibility[c.HeaderText.ToLower()] == false))
                c.Visible = false;

            if (c.HeaderText == "DOC_ID" || c.HeaderText == "DOC_TYPE_ID" || c.HeaderText == "THUMB" || c.HeaderText == "FULLPATH" || c.HeaderText == "ICON_ID" || c.HeaderText == "ASIGNEDTO" || c.HeaderText == "EXECUTION" || c.HeaderText == "DO_STATE_ID" || c.HeaderText == "TASK_ID" || c.HeaderText == "STATE" || c.HeaderText == "RN" || c.HeaderText == "ENTIDAD" || c.HeaderText == "LEIDO" || c.HeaderText == "STEP_ID" || c.HeaderText == "ORIGINAL" || c.HeaderText == "WORKFLOW" || c.HeaderText == "STEP")
                c.Visible = false;
            if (c.HeaderText == "NAME")
                c.HeaderText = "TAREA";
        }

        foreach (DataColumn c in dt.Columns)
        {
            //c.ColumnName = c.ColumnName.Replace(" ", "_").Replace("-", "_");
            c.ColumnName = c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace("__", "_");
        }

    }

    
    


    private bool IsNumeric(string value)
    {
        decimal testDe;
        double testDo;
        int testI;

        if (decimal.TryParse(value, out testDe) || double.TryParse(value, out testDo) || int.TryParse(value, out testI))
            return true;

        return false;
    }
}

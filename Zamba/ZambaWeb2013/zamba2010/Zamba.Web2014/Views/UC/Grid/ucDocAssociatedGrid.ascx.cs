using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Zamba.Services;
using Zamba.Core;

public partial class Views_UC_Grid_ucDocAssociatedGrid : System.Web.UI.UserControl
{
    private const string DOC_TYPE_NAME_COLUMNNAME = "doc_type_name";
    private const string DOC_TYPE_ID_COLUMNNAME = "DOC_TYPE_ID";

    public void loadAssociatedDocs(ITaskResult task)
    {
        loadAssociatedDocs((IResult)task);
    }
    public void loadAssociatedDocs(IResult result)
    {
        try
        {
            DataTable dt = new STasks().getAsociatedDTResultsFromResult(result, 0, false, Zamba.Membership.MembershipHelper.CurrentUser,true);
            FormatGridview();
            generateGridColumns(dt);
            BindGrid(dt);
        }
        catch
        {
            lblMsg.Text = "No se han podido cargar los asociados.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Font.Size = 10;
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
                DataTextFormatString = "<img src=\"../../Tools/icono.aspx?id={0}\" border=0/>",
                DataTextField = "ICON_ID"
            };

            colver.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

            colver.DataNavigateUrlFields = a;
            colver.DataNavigateUrlFormatString = @"../../WF/TaskSelector.ashx?doctype={0}&docid={1}&taskid={2}";

            grdDocAssociated.Columns.Add(colver);
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }

    private void generateGridColumns(DataTable _dt)
    {
        try
        {
            if (_dt != null && _dt.Columns.Count > 0)
            {
                foreach (DataColumn c in _dt.Columns)
                {
                    BoundField f = new BoundField
                    {
                        DataField = c.Caption,
                        ShowHeader = true,
                        HeaderText = c.Caption,
                        SortExpression = c.Caption + " ASC"
                    };

                    grdDocAssociated.Columns.Add(f);
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

                if (colname.ToLower() == "nombre original")
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
            lblMsg.Text = "La tarea no posee documentos asociados o usted no tiene permiso para visualizarlos.";            
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
}

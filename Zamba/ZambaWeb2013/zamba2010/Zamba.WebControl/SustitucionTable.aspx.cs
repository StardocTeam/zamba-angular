using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    private Int32 IdDocType;
    private Int32 IdIndex;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["IdDocType"] != null && string.Compare(Request.QueryString["IdDocType"], string.Empty) != 0)
        {
            IdDocType = Int32.Parse(Request.QueryString["IdDocType"]);
            if (Request.QueryString["IdIndex"] != null && string.Compare(Request.QueryString["IdIndex"], string.Empty) != 0)
            {
                IdIndex = Int32.Parse(Request.QueryString["IdIndex"]);
                createTable();
            }
        }
    }

    private void createTable()
    {
        DataTable dt = AutoSubstitutionBussines.GetIndexData(IdIndex, false);

        System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();
        row.EnableViewState = true;

        System.Web.UI.WebControls.TableCell cellCodigo = new System.Web.UI.WebControls.TableCell();
        cellCodigo.EnableViewState = true;
        cellCodigo.HorizontalAlign = HorizontalAlign.Center;
        cellCodigo.Text = "Código";
        cellCodigo.BackColor = System.Drawing.Color.Moccasin;
        cellCodigo.Font.Bold = true;
        
        System.Web.UI.WebControls.TableCell cellName = new System.Web.UI.WebControls.TableCell();
        cellName.EnableViewState = true;
        cellName.HorizontalAlign = HorizontalAlign.Left;
        cellName.Text = "Descripción";
        cellName.BackColor = System.Drawing.Color.Moccasin;
        cellName.Font.Bold = true;

        row.Cells.Add(cellCodigo);
        row.Cells.Add(cellName);
        
        this.TblIndex.Rows.Add(row);

        foreach (DataRow dr in dt.Rows)
            addItem(this.TblIndex,dr);
    }

    private void addItem(System.Web.UI.WebControls.Table table,
                                                DataRow dr)
    {
        System.Web.UI.WebControls.TableRow row = new System.Web.UI.WebControls.TableRow();
        row.EnableViewState = true;
        
        System.Web.UI.WebControls.TableCell cellCodigo = new System.Web.UI.WebControls.TableCell();
        cellCodigo.EnableViewState = true;
        cellCodigo.HorizontalAlign = HorizontalAlign.Right;

        LinkButton lnkCodigo = new LinkButton();
        lnkCodigo.Click += new EventHandler(link_click);
        lnkCodigo.ID = "Cod-" + dr[0].ToString() + "-" + dr[1].ToString();
        lnkCodigo.Text = dr[0].ToString();
        lnkCodigo.Font.Underline = false;
        
        cellCodigo.Width = lnkCodigo.Width;
        cellCodigo.Controls.Add(lnkCodigo);

        System.Web.UI.WebControls.TableCell cellName = new System.Web.UI.WebControls.TableCell();
        cellName.EnableViewState = true;
        cellName.HorizontalAlign = HorizontalAlign.Left;

        LinkButton lnkDescrip = new LinkButton();
        lnkDescrip.Click += new EventHandler(link_click);
        lnkDescrip.ID = "Desc-" + dr[0].ToString() + "-" + dr[1].ToString();
        lnkDescrip.Text = dr[1].ToString();
        lnkDescrip.Font.Underline = false;
        
        cellName.Controls.Add(lnkDescrip);

        row.Cells.Add(cellCodigo);
        row.Cells.Add(cellName);
        table.Rows.Add(row);
    }
    
    private void link_click(object sender, EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Char mander = Char.Parse("-");
        Session.Add(IdIndex.ToString(), link.ID.Split(mander)[2]);
        Response.Redirect("../Zamba.WebControl/IndexViewer.aspx?IdDocType=" + IdDocType);
    }
}

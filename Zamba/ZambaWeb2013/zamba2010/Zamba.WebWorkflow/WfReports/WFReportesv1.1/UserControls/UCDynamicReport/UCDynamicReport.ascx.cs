using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

// -------------------------------------------------------------------------------------
// -------------------------------------------------------------------------------------

//  WebControl "DynamicReport" utilizado para contener un reporte dinámico (un gridview)     

//  [Gaston]   Created  21/11/08

// -------------------------------------------------------------------------------------
// -------------------------------------------------------------------------------------

public partial class UCDynamicReport : System.Web.UI.UserControl
{
    /// <summary>
    /// Propiedad que recibe un datatable que se usa para armar el GridView
    /// </summary>
    public DataTable Table
    {
        set
        {
            if (value.Rows.Count > 0)
            {
                lblNothingResults.Visible = false;
                gvDynamicReport.Visible = true;

                foreach (DataColumn c in value.Columns)
                {
                    BoundField lobColumnBound = new BoundField();
                    lobColumnBound.DataField = c.ColumnName;
                    lobColumnBound.HeaderText = c.ColumnName;
                    gvDynamicReport.Columns.Add(lobColumnBound);
                }

                gvDynamicReport.DataSource = value;
                gvDynamicReport.DataBind();
            }
            else
            {
                lblNothingResults.Text = "No se encontraron resultados";
                lblNothingResults.Visible = true;
                gvDynamicReport.Visible = false;
            }
        }
    }

    /// <summary>
    /// Propiedad que recibe un string que se usa para colocar el título del reporte dinámico
    /// </summary>
    public string Title
    {
        set
        {
            lblTitle.Text = value;
        }
    }

    /// <summary>
    /// Propiedad que recibe un string que se usa para colocarlo en la propiedad Text del label lblNothingResults y activar el propio label
    /// </summary>
    public string Error
    {
        set
        {
            lblNothingResults.Text = value;
            lblNothingResults.Visible = true;
            gvDynamicReport.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
}

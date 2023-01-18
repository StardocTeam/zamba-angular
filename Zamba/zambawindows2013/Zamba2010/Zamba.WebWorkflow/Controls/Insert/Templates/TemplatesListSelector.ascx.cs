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
using System.IO;
using Zamba.Core;
public partial class Controls_Insert_Templates_TemplatesListSelector : System.Web.UI.UserControl
{
    DataSet DsTemplates;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDBTemplates();
        }
                }

    public void GetDBTemplates()
    {
        try
        {

            DsTemplates = TemplatesBusiness.GetTemplatesWithIcon();
            LoadGridview(DsTemplates.Tables[0]);
           
        }
        catch (Exception ex)
        {

        }
     }
  
    public void LoadGridview(DataTable dt)
    {
        try
        {
            //Int32 resultcount = Int32.Parse(Session["ResultsCount"].ToString());
            //Int32 UserId = Int32.Parse(Session["UserId"].ToString());
            //Int16 PageId = Int16.Parse(Session["PagingId"].ToString());
            //Int16 PageSize = Int16.Parse(Session["PageSize"].ToString());
            gvDocuments.Columns.Clear();
            gvDocuments.ShowHeader = false;

            ImageField Imagen_doc = new ImageField();
            Imagen_doc.ShowHeader = true;
            Imagen_doc.HeaderText = "Icono";
            Imagen_doc.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
            Imagen_doc.ItemStyle.VerticalAlign = VerticalAlign.Middle;
            Imagen_doc.DataImageUrlField = "Icono";
            Imagen_doc.DataImageUrlFormatString = "../../../icono.aspx?id={0}";

            this.gvDocuments.Columns.Add(Imagen_doc);
            //-------------------------------------------------------------------
            foreach (DataColumn c in dt.Columns)
            {
                if (string.Compare(c.Caption.ToUpper(),"NAME") == 0)
                {
                  
                                BoundField f = new BoundField();
                f.DataField = c.Caption;
                f.ShowHeader = true;
                f.HeaderText = c.Caption;
                f.SortExpression = c.Caption + " ASC";
                if (c.Caption == "Icono")
                {
                    f.Visible = false;
                }
                this.gvDocuments.Columns.Add(f);
                }
            }

            this.gvDocuments.DataSource = dt;
            this.gvDocuments.DataBind();

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

}

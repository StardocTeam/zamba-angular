using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Text;
using Zamba.Core;

public partial class DoExportToPDF : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                ZOptBusiness zopt = new ZOptBusiness();

                string title = zopt.GetValue("WebViewTitle");
                zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";

            }
            catch { }
            if (Request.QueryString["Content"] != null)
            {
                if (Session[Request.QueryString["Content"]] != null)
                {
                    RadEditor1.Content = Session[Request.QueryString["Content"]].ToString();
                    Session.Remove(Request.QueryString["Content"]);
                }
                else
                {
                    lblError.Text = "El trabajo de exportación ha expirado, por favor vuelva a generalo.";
                    lblError.Visible = true;
                    btnExport.Visible = false;
                }
            }
            if (Request.QueryString["CanEditable"] == null ||
                !bool.Parse(Request.QueryString["CanEditable"]))
            {
                RadEditor1.Enabled = false;
            }

            if (Request.QueryString["ReturnFileName"] != null)
            {
                RadEditor1.ExportSettings.FileName = Request.QueryString["ReturnFileName"];
            }
        }
    }

    protected void Export(object sender, EventArgs e)
    {
        RadEditor1.ExportToPdf();
    }


    protected void ExportContent(object sender, EditorExportingArgs e)
    {
        
    }
}

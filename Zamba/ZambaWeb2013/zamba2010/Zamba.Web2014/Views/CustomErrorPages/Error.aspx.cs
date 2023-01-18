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
using Zamba.Core;

public partial class Views_CustomErrorPages_Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            string title = zopt.GetValue("WebViewTitle");
            zopt = null;
            this.Title = (string.IsNullOrEmpty(title))? "Zamba Software - Error" : title + " - Zamba Software - Error";
        }
        catch { }
    }
}

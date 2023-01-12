using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Notificatios_SendMails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                Zamba.Core.ZOptBusiness Zopt = new Zamba.Core.ZOptBusiness();
                string title = Zopt.GetValue("WebViewTitle");
                Zopt = null;
                this.Title = (string.IsNullOrEmpty(title)) ? "Zamba Software" : title + " - Zamba Software";
            }
            catch { }
        }
    }
}

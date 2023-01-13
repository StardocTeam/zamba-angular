using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string command = HttpContext.Current.Request.QueryString["Report"];
            if (!string.IsNullOrEmpty(command))
            {
                Int64 reportID;
                if (Int64.TryParse(command,out reportID) == true)
                {
                    Control ctrToLoad = LoadControl("~/ReportBuilder.ascx");
                    if (ctrToLoad != null)
                    {
                        ctrToLoad.ID = "Report" + command;
                        this.divContainer.Controls.Add(ctrToLoad);
                    }
                }
                else
                {
                    Control ctrToLoad = LoadControl("~/" + command + ".ascx");
                    if (ctrToLoad != null)
                    {
                        ctrToLoad.ID = "Report" + command;
                        this.divContainer.Controls.Add(ctrToLoad);
                    }
                }
            }
        }
    }
}
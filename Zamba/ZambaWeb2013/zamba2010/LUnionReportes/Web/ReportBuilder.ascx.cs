using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using Zamba.ReportBuilder.Business;
using Telerik.Web.UI;

namespace Web
{
    public partial class ReportBuilder : System.Web.UI.UserControl
    {
        public string ReportName
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ReportName = string.Empty;

            ReportBuilderFactory rep = new ReportBuilderFactory();
            string command = HttpContext.Current.Request.QueryString["Report"];
            Int32 reportID;
            if (Int32.TryParse(command, out reportID) == true)
            {
                try
                {
                    ReportName = rep.GetName(reportID);
                    RadGrid1.DataSource = rep.RunQueryBuilder(reportID, true);
                    RadGrid1.DataBind();
                }
                catch (Exception ex)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "errMsg", "$(document).ready(function () {showError();  });", true);                    
                }
            }

            RadAjaxManager ajM = RadAjaxManager.GetCurrent(this.Page);
            ajM.AjaxSettings.Add(new AjaxSetting("RadGrid1"));
        }

        protected void RadGrid1_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == Telerik.Web.UI.RadGrid.ExportToExcelCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToWordCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToCsvCommandName ||
                e.CommandName == Telerik.Web.UI.RadGrid.ExportToPdfCommandName)
            {
                ConfigureExport();
            }
        }

        public void ConfigureExport()
        {
            RadGrid1.ExportSettings.ExportOnlyData = true;
            RadGrid1.ExportSettings.IgnorePaging = CheckBox2.Checked;
            RadGrid1.ExportSettings.OpenInNewWindow = true;
        }
    }
}
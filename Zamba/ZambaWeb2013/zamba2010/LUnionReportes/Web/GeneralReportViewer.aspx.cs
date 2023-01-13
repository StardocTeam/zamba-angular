using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Web
{
    public partial class GeneralReportViewer : System.Web.UI.Page
    {
        const string REPORTVIEWERURL = "ReportViewer.aspx?Report={0}";

        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void ClearReport(object sender, EventArgs e)
        {
            this.DivReports.Controls.Clear();
            //this.udpreports.Controls.Clear();

            //this.DivReports = new System.Web.UI.HtmlControls.HtmlGenericControl("<div style=" + (char)34 + "float: left; height:700px; width:1000px" + (char)34 + " runat=" + (char)34 + "server" + (char)34 + " id=" + (char)34 + "DivReports" + (char)34 + " ></div>");
            //udpreports.Controls.Add(this.DivReports);
        }

        protected void ShowReport(object sender, EventArgs e)
        {
            var ReportName = sender.ToString();
            var control = Page.FindControl(ReportName);

            if (control == null)
            {
                HtmlGenericControl divReportToLoad = new HtmlGenericControl("div class='col-md-6 col-xs-12'");
                divReportToLoad.ID = ReportName;
//                divReportToLoad.Style.Add("display", "inline");
                HtmlGenericControl ifReportToLoad = new HtmlGenericControl("iframe");
                ifReportToLoad.ID = "if" + ReportName;
                ifReportToLoad.Attributes.Add("onload", "setIframeHeight(this.id);");
                ifReportToLoad.Style.Add("border-width", "0px");
                ifReportToLoad.Attributes.Add("frameborder", "0");

                #region Load Scr Iframe
                switch (ReportName)
                {
                    case "UserDistribution":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "UserDistribution"));
                        break;
                    case "TasksByWorkflow":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "TasksByWorkflow"));
                        break;
                    case "Reclamos":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "Reclamos"));
                        break;
                    case "ReclamosXDia":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "ReclamosXDia"));
                        break;
                    case "Indemnizaciones":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "Indemnizaciones"));
                        break;
                    case "IndemnizacionesXImporte":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "IndemnizacionesXImporte"));
                        break;
                    case "Siniestros":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "Siniestros"));
                        break;
                    case "Consultas":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "Consultas"));
                        break;
                    case "DocumentacionFaltante":
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, "DocumentacionFaltante"));
                        break;
                    default:
                        ifReportToLoad.Attributes.Add("src", string.Format(REPORTVIEWERURL, ReportName));
                        break;
                }
                #endregion

                divReportToLoad.Controls.Add(ifReportToLoad);
                DivReports.Controls.Add(divReportToLoad);
                this.udpreports.Update();
            }
        }
    }
}

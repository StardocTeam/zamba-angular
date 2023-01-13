using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;

namespace IntranetMarsh
{
    public partial class IntraMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("Es-Es");
            this.DateLabel.Text = ci.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek) + ", " + DateTime.Now.Date.Day.ToString() + " de " + ci.DateTimeFormat.GetMonthName(DateTime.Now.Month) + " de " + DateTime.Now.Date.Year.ToString();
        }

        protected void imgbtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbSearch.Text))
            {
                Response.Redirect("Documents.aspx?Search=" + tbSearch.Text);
            }
        }
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace IntranetMarsh
{
    public partial class Suggestions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Marsh Intranet - Buzon de sugerencias";
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            this.TxtTem.Text = String.Empty;
            this.TextBox1.Text = String.Empty;
            this.TextBox2.Text = String.Empty;
            this.TextBox3.Text = String.Empty;
            this.TextBox4.Text = String.Empty;
            Label5.Text = "Su sugerencia ha sido enviada";
        }
    }
}

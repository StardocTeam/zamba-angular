using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ucCurrentTime : System.Web.UI.UserControl
{
    protected void btCurrentTime_Click(object sender, EventArgs e)
    {
        lbCurrentTime.Text = DateTime.Now.ToString();
    }
}

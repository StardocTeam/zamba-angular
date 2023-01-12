using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Zamba.AgentServer.Controls
{
    public partial class UCClients : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Pages/UCMHistory.aspx?Cliente=" + this.DropDownList1.SelectedItem);
            }
            catch (Exception)
            {
               
                throw;
            }
        }

        protected void BtnSendReportByMail_Click(object sender, EventArgs e)
        {
            Zamba.AgentServer.WS.UCMService US = new WS.UCMService();
            US.SendResume(this.TextBox1.Text, null);
        }
    }
}
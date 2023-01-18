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
using WebLoginBusiness;

namespace WebLogin
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                this.lblBienvenido.Text = "Bienvenido: " + Session["User"].ToString();
            }
            else
            {
                Response.Redirect("LogIn.aspx?ReturnUrl=Welcome.aspx");
            }  
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["User"] != null)
                {
                    if (LoginBusiness.LogOut(Session["User"].ToString(), Request.ServerVariables["REMOTE_ADDR"]))
                    {
                        Session["User"] = null;
                        Response.Redirect("LogOut.aspx");
                    }
                }
                else
                {
                    this.lblMessage.Text = "El usuario se encuentra desconectado actualmente";
                }
            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }
        }
    }
}

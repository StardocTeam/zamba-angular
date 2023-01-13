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
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["User"] != null)
            {
                Response.Redirect("Welcome.aspx");
            }
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {     
                Session["User"] = LoginBusiness.LogIn(this.txtUserName.Text, this.txtPassword.Text, Request.ServerVariables["REMOTE_ADDR"]);

                string return_query = Request.QueryString["ReturnUrl"];
                if (!String.IsNullOrEmpty(return_query))
                {
                    Server.Transfer(return_query);
                }
                else
                {
                    Response.Redirect("Welcome.aspx");
                }
            }
            catch (Exception ex)
            {
                this.lblMessages.Text = ex.Message;
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
                    this.lblMessages.Text = "El usuario se encuentra desconectado actualmente";
                }
            }
            catch (Exception ex)
            {
                this.lblMessages.Text = ex.Message;
            }
        }
    }
}

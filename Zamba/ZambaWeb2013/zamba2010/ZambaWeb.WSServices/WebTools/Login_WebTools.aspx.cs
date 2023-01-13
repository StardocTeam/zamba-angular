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

namespace ZambaWeb.WSServices.WebTools
{
    public partial class Login_WebTools : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ShowToolsToUser()
        {
            string script = " $(document).ready(function() { $('#ValidateUser_Div').css('display', 'none'); $('#dvTools').css('display', 'block');});";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "ShowZQ", script, true);            
        }

        protected void ValidateUser()
        {
            if (userName.Text == "" || UserPass.Text == "")
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Ingrese usuario y password";
                return;
            }

            if (userName.Text == "Zamba1234567" && UserPass.Text == "1234567")
            {
                ShowToolsToUser();
                if (Session["userTools"] == null)
                {
                    Session["userTools"] = true;
                }
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Usuario o password incorrectos";
            }
        }

        protected string MockValidateInAD(string UserName, string UserPass)
        {
            return string.Empty;
        }

        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            ValidateUser();
        }
    }
}


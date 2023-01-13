using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Zamba.Core;

public partial class LogIn : System.Web.UI.Page
{
    private const String QUERY_STRING_ORIGINAL_URL = "ReturnUrl";
    private const String QUERY_STRING_NODE_USER_ID = "UserId";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                LoadUser();

                this.Title = "Autenticación";
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }
    }

    /// <summary>
    /// Loads the user name from the QueryString
    /// </summary>
    private void LoadUser()
    {
        String ReturnURL = Request.QueryString[QUERY_STRING_ORIGINAL_URL];

        String[] asd = ReturnURL.Split('&');

        foreach (String Value in asd)
        {
            if (Value.Contains(QUERY_STRING_NODE_USER_ID))
            {
                String[] Values = Value.Split('=');
                if (Values.Length == 2)
                {
                    Int64 UserId;
                    if (Int64.TryParse(Values[1], out UserId))
                    {
                        IUser CurrentUser = UserBusiness.GetUserById(UserId);
                        if (null != CurrentUser && !String.IsNullOrEmpty(CurrentUser.Name))
                        {
                            txtUserName.Text = CurrentUser.Name;
                            txtUserName.Enabled = false;

                            CurrentUser.Dispose();
                            CurrentUser = null;
                        }
                    }
                }

                Array.Clear(Values, 0, Values.Length );
                Values = null;

                break;
            }
        }
    }

    protected void Logon_Click(object sender, EventArgs e)
    {

        if (IsUserValid())
            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, Persist.Checked);
        else
            Msg.Text = "Usuario o Contraseña incorrectos. Ingrésela nuevamente";
    }

    private Boolean IsUserValid()
    {
        Boolean IsValidUser = true;

        //Aqui se hacen todas las llamadas pertinentes para
        //validar el usuario y se setea el booleano IsValidUser
        try
        {
            if (Zamba.Data.UserFactory.validateUser(txtUserName.Text, txtUserPass.Text) == null)
                IsValidUser = false;
            else
                IsValidUser = true;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        return IsValidUser;
    }
}

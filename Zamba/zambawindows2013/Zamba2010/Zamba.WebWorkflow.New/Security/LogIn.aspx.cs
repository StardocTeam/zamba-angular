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
using Zamba.Data;

public partial class Login : System.Web.UI.Page
{
    #region Attributes

    HtmlInputHidden varUserName;

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loginBody.Attributes["onload"] = "showuser()";
            LoadAspect(true);
            Session["flagIsFirstLoad"] = true;
        }
        else
        {
            loginBody.Attributes["onload"] = null;
            LoadAspect(false);
        }
    }

    protected void Submit1_Click(object sender, EventArgs e)
    {
        if (chkWindowsLogIn.Checked)
        {
            LoadAspect(false);
        }
        else
        {
            if (IsLoadWindowsUserTrue())
            {
                DoLogin(true);
            }
            else
            {
                Session["flagIsFirstLoad"] = false;
                LoadAspect(false);
            }
        }
    }

    protected void Submit2_Click(object sender, EventArgs e)
    {
        if (IsFormFieldsRigth())
        {
            if (IsUserValid())
            {
                DoLogin(false);
            }
        }
    }

    #endregion

    #region Methods

    private String ClientName()
    {
        return System.Web.Configuration.WebConfigurationManager.AppSettings["ClientName"];
    }

    private void DoLogin(Boolean blnWindowsLogin)
    {
        try
        {
            if (blnWindowsLogin)
            {
                Session["UserId"] = Zamba.Core.UserBusiness.GetUserID(varUserName.Value);
                FormsAuthentication.RedirectFromLoginPage(varUserName.Value, false);
            }
            else
            {
                Session["UserId"] = Zamba.Core.UserBusiness.GetUserID(txtUserName.Text.Trim());
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    private Boolean IsUserValid()
    {
        try
        {
            if (null != UserBusiness.Rights.ValidateLogIn(txtUserName.Text, txtPassword.Text))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblLoginWindows.Text = String.Empty;
            return false;
        }
    }

    protected Boolean IsLoadWindowsUserTrue()
    {
        if (Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["LoadWindowsUser"]))
        {
            varUserName = (HtmlInputHidden)FindControl("vUserName");

            if (null != varUserName)
            {
                if (string.IsNullOrEmpty(varUserName.Value) == false)
                {
                    if (null != UserBusiness.GetUserByName(varUserName.Value))
                        return true;
                }
            }
        }
        return false;
    }

    protected void LoadAspect(Boolean loadWindowsUserAspect)
    {
        if (loadWindowsUserAspect)
        {
            lblUserName.Visible = false;
            txtUserName.Visible = false;
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            RequiredFieldValidator1.Visible = false;
            RequiredFieldValidator2.Visible = false;
            Submit2.Visible = false;
            Submit1.Visible = true;
            chkWindowsLogIn.Visible = true;
            tblBtnIngresar.CellSpacing = 30;
            this.Title = ClientName() + " - Reportes de Workflow [Ingresar]";
            lblLoginWindows.Text = "Bienvenido, haga click en el botón para ingresar sistema.";
            //hylLoginWindows.Text = " click aquí para ingresar con otro usuario de Zamba.";


            //((System.Web.UI.WebControls.WebControl)(lblLoginWindows)).Font.Bold = false;
            //((System.Web.UI.WebControls.BaseValidator)(RequiredFieldValidator1)).Enabled = false;
        }
        else
        {
            lblUserName.Visible = true;
            txtUserName.Visible = true;
            lblPassword.Visible = true;
            txtPassword.Visible = true;
            lvlInvisible.Height = 140;
            RequiredFieldValidator1.Visible = false;
            RequiredFieldValidator2.Visible = false;
            Submit2.Visible = true;
            Submit1.Visible = false;
            chkWindowsLogIn.Visible = false;
            tblBtnIngresar.CellSpacing = 5;
            this.Title = ClientName() + " - Reportes de Workflow [Iniciar Sesión]";
            String case2 = "El usuario de Windows no existe en la Base de Usuarios. Favor de ingresar otro usuario de Zamba.";

            if (lblLoginWindows.Text.EndsWith("con otro usuario de Zamba."))
            {
                if (!chkWindowsLogIn.Checked)
                    lblLoginWindows.Text = case2;
                else
                    lblLoginWindows.Text = "Favor de ingresar el usuario de Zamba.";
            }
            else if (String.Compare(case2, lblLoginWindows.Text) == 0)
            {
                if (!String.IsNullOrEmpty(txtPassword.Text.Trim()) && !String.IsNullOrEmpty(txtUserName.Text.Trim()))
                    lblLoginWindows.Text = String.Empty;
            }

            //((System.Web.UI.WebControls.WebControl)(lblLoginWindows)).Font.Bold = true;
            //((System.Web.UI.WebControls.BaseValidator)(RequiredFieldValidator1)).Enabled = true;
        }
    }

    protected void txtUserName_OnTextChanged(object sender, EventArgs a)
    {
        if (RequiredFieldValidator1.Visible)
        {
            if (!String.IsNullOrEmpty(txtUserName.Text.Trim()))
                RequiredFieldValidator1.Text = String.Empty;
        }
    }
    protected void txtPassword_OnTextChanged(object sender, EventArgs a)
    {
        if (RequiredFieldValidator2.Visible)
        {
            if (!String.IsNullOrEmpty(txtPassword.Text.Trim()))
                RequiredFieldValidator2.Text = String.Empty;
        }
    }

    protected Boolean IsFormFieldsRigth()
    {
        Boolean flagCorrect = true;

        if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
        {
            RequiredFieldValidator1.Visible = true;
            flagCorrect = false;
        }
        if (String.IsNullOrEmpty(txtPassword.Text.Trim()))
        {
            RequiredFieldValidator2.Visible = true;
            flagCorrect = false;
        }

        if ((RequiredFieldValidator1.Visible || RequiredFieldValidator2.Visible) && String.IsNullOrEmpty(lblError.Text) && String.IsNullOrEmpty(lblLoginWindows.Text))
        {
            lblLoginWindows.Text = "Favor de completar todos los campos.";
        }

        return flagCorrect;
    }

    #endregion

}

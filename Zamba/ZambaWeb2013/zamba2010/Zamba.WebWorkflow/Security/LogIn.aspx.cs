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
[assembly: System.Security.AllowPartiallyTrustedCallers]
public partial class Login : System.Web.UI.Page
{
    #region Attributes

    HtmlInputHidden varUserName;
    HtmlInputHidden varComputerName;

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

    /// <summary>
    /// M�todo que sirve para permitir el login e insertar un usuario en ucm y redirigir a otra p�gina
    /// </summary>
    /// <history>
    ///     [Gaston]  08/01/2009  Modified
    ///     [Gaston]  09/01/2009  Modified  Llamada a un m�todo u otro m�todo seg�n el tipo de login
    /// </history>
    private void DoLogin(Boolean blnWindowsLogin)
    {
        try
        {
            if (blnWindowsLogin)
                userWindowsLogin();
            else
                commonLogin();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    /// <summary>
    /// M�todo que sirve para loguearse como usuario de windows
    /// </summary>
    /// <history>
    ///     [Gaston]  09/01/2009  Created   C�digo original del m�todo DoLogin
    ///     [Gaston]  14/01/2009  Modified  Llamada al m�todo "login"
    /// </history>
    private void userWindowsLogin()
    {  
        Session["UserId"] = UserBusiness.GetUserID(varUserName.Value);
        Session["CurrentUser"] = UserBusiness.GetUserById(Int64.Parse(Session["UserId"].ToString()));
        // Usuario de Windows
        login(vUserName.Value, true);
    }

    /// <summary>
    /// M�todo que sirve para loguearse ingresando un nombre de usuario y contrase�a
    /// </summary>
    /// <history>
    ///     [Gaston]  09/01/2009  Created   
    ///     [Gaston]  14/01/2009  Modified  Llamada al m�todo "login"
    /// </history>
    private void commonLogin()
    { 
        Session["UserId"] = UserBusiness.GetUserID(txtUserName.Text.Trim());
        // Usuario colocado en la caja de texto
        login(txtUserName.Text, false);
    }

    /// <summary>
    /// M�todo que sirve para registrar al usuario en la tabla UCM
    /// </summary>
    /// <param name="userName">Nombre del usuario, ya sea de windows o el ingresado en la caja de texto</param>
    /// <history>
    ///     [Gaston]  14/01/2009  Created    C�digo original del m�todo DoLogin m�s algunas modificaciones como el agregado de la computadora del usuario
    ///     [Gaston]  15/01/2009  Modified   Si no es un usuario de windows entonces se obtiene la direcci�n IP
    /// </history>
    private void login(string userName, Boolean isUserWindows)
    {
        string computerNameOrIP;

        // Si es un usuario windows
        if (isUserWindows)
            // Se obtiene el nombre de la computadora del usuario gracias al componente activeX
            computerNameOrIP = vComputerName.Value;
        else
            // Se obtiene la direcci�n IP de la computadora del usuario
            computerNameOrIP = Request.UserHostAddress;
        
        try
        {
            // Se agrega una nueva pc a la tabla UCM
            // Par�metros: id de usuario actual | usuario de windows | nombre o IP de la computadora del usuario | timeOut | WFAvailable = valor false 
            // para licencia documental
            Session["ConnectionId"] = Ucm.NewConnection(Convert.ToInt32(Session["UserId"]), userName, computerNameOrIP, Int16.Parse(UserPreferences.getValue("TimeOut", UserPreferences.Sections.UserPreferences, 30)), 0);
        }
        catch (Exception ex)
        {
            lblError.Text = "Maximo de Licencias conectadas, cont�ctese con su proveedor para adquirir nuevas licencias";
            Session["ConnectionId"] = null;
            ZClass.raiseerror(ex);
        }

        if (Session["ConnectionId"] != null)
        {
            // Se guarda la computadora del usuario. Es necesario por si el usuario presiona el bot�n "Cerrar Sesi�n" para que �ste pueda eliminarse de
            // la tabla UCM
            Session["ComputerNameOrIP"] = computerNameOrIP;
            FormsAuthentication.RedirectFromLoginPage(userName, false);
        }
    }

    private Boolean IsUserValid()
    {
        try
        {
            IUser user = UserBusiness.Rights.ValidateLogIn(txtUserName.Text, txtPassword.Text);
            if (null != user)
            {
                Session["CurrentUser"] = user;
                return true;
            }
            else
            { return false; }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            lblLoginWindows.Text = String.Empty;
            return false;
        }
    }

    /// <summary>
    /// M�todo que sirve para comprobar si la propiedad LoadWindowsUser est� en true. Si est� en true, entonces se obtiene el nombre de la cuenta de 
    /// usuario de Windows y el nombre de su computadora (si es que no se encuentran vac�os ni sus controles est�n en nothing). Se verifica que el 
    /// nombre de cuenta de usuario de windows exista en la base de datos
    /// </summary>
    /// <history>
    ///     [Gaston]  08/01/2009  Modified
    /// </history>
    protected Boolean IsLoadWindowsUserTrue()
    {
        if (Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["LoadWindowsUser"]))
        {
            varUserName = (HtmlInputHidden)FindControl("vUserName");
            varComputerName = (HtmlInputHidden)FindControl("vComputerName");

            if ((varUserName != null) && (varComputerName != null))
            {
                if ((String.IsNullOrEmpty(varUserName.Value) == false) && (String.IsNullOrEmpty(varComputerName.Value) == false))
                {
                    if (UserBusiness.GetUserByname(varUserName.Value) != null)
                        return (true);
                }
            }
        }

        return (false);
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
            this.Title = ClientName() + " - Zamba Web [Ingresar]";
            lblLoginWindows.Text = "Bienvenido, haga click en el bot�n para ingresar sistema.";
            //hylLoginWindows.Text = " click aqu� para ingresar con otro usuario de Zamba.";
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
            this.Title = ClientName() + " - Zamba Web [Ingresar]";
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
                if (!String.IsNullOrEmpty(txtUserName.Text.Trim()))
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

    protected Boolean IsFormFieldsRigth()
    {
        Boolean flagCorrect = true;

        if (String.IsNullOrEmpty(txtUserName.Text.Trim()))
        {
            RequiredFieldValidator1.Visible = true;
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



using System;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using App_Code;
using Zamba.Services;
using Zamba.Core;
using System.Web;
using Zamba.Membership;

public partial class Login : System.Web.UI.Page
{
    #region Attributes

    HtmlInputHidden _varComputerName;


    #endregion

    #region Events

    bool _allowZambaLogin = false;
    bool _loadWindowsUser = false;
    
    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Zamba.Servers.Server.ConInitialized == false)
        {
            Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
            ZC.InitializeSystem("Zamba.Web");
        }

        Zamba.Core.UserPreferences.LoadAllMachineConfigValues();
        Zamba.Membership.MembershipHelper.OptionalAppTempPath = Zamba.Core.UserPreferences.getValue("AppTempPath", Zamba.Core.Sections.UserPreferences, String.Empty);

        ZOptBusiness zoptb = new ZOptBusiness();
        Page.Theme = zoptb.GetValue("CurrentTheme");
        Session["CurrentTheme"] = Page.Theme;
        zoptb = null;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            bool.TryParse(WebConfigurationManager.AppSettings["AllowLoginZambaUser"], out _allowZambaLogin);
            bool.TryParse(WebConfigurationManager.AppSettings["LoadWindowsUser"], out _loadWindowsUser);

            LoadAspect();
            Session["flagIsFirstLoad"] = true;

            ZOptBusiness zoptb = new ZOptBusiness();
            String CurrentTheme = zoptb.GetValue("CurrentTheme");
            zoptb = null;

            lnkWebIcon.Attributes.Add("href", "~/App_Themes/" + CurrentTheme + "/Images/WebIcon.ico");
        }
    }

    protected void btnLoginWindows_Click(object sender, EventArgs e)
    {
        if (chkZambaLogIn.Checked)
        {
            LoadAspect();
            pnlZambaLogin.Visible = true;
            pnlWindowsLogin.Visible = false;
            lblMensajeLogin.Text = "Por favor ingrese su usuario y clave para ingresar";
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
                LoadAspect();
            }
        }
    }

    protected void btnLoginZamba_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsFormFieldsRigth())
                return;

            //Se agrega linea para validar login con AD y Zamba.
            ZOptBusiness zopt = new ZOptBusiness();
            string useADLogin = zopt.GetValue("UseADLogin");
            zopt = null;

            if (!string.IsNullOrEmpty(useADLogin) && bool.Parse(useADLogin))
            {
                DoADLogin();
            }
            else
            {
                SRights sRights = new SRights();
                Zamba.Core.IUser user = sRights.ValidateLogIn(txtUserName.Value, txtPassword.Value, ClientType.Web);
                sRights = null;

                if (user != null)
                {
                    Session["User"] = user;
                    DoLogin(false);
                }
                else
                {
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage(ex.Message);
        }
    }

    #endregion

    #region Methods

    private static String ClientName()
    {
        SZOptBusiness zOptBusines = new SZOptBusiness();
        string webViewTitle = zOptBusines.GetValue("WebViewTitle");
        zOptBusines = null;
        return string.IsNullOrEmpty(webViewTitle) ? "Zamba Software" : webViewTitle;
    }

    private void DoLogin(Boolean blnWindowsLogin)
    {
        SUsers sUsers = null;
        SRights sRights = null;

        try
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando base y usuario");
            if (UserBusiness.ValidateDataBase() == true)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Base validada");
                sUsers = new SUsers();
                sRights = new SRights();

                if (MembershipHelper.CurrentUser != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario: " + MembershipHelper.CurrentUser.Name);
                    int cacheTimeOut;
                    if (!int.TryParse(WebConfigurationManager.AppSettings["CacheTimeOut"], out cacheTimeOut))
                        cacheTimeOut = 60;

                    bool isLicenceOk = DoConsumeLicense(blnWindowsLogin, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.ID);

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando licencias");
                    if (isLicenceOk)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Licencias validadas");
                        Boolean wf = sRights.IsWfLicence();
                        MembershipHelper.CurrentUser.WFLic = wf;
                        MembershipHelper.CurrentUser.ConnectionId = (int)Session["ConnectionId"];
                        MembershipHelper.CurrentUser.puesto = (string)Session["ComputerNameOrIP"];

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando permisos de acceso a web");

                        //11/06/12: Se agrega validación que tenga activo el módulo Web.
                        bool isWebEnable = UserBusiness.Rights.GetUserRights(MembershipHelper.CurrentUser, ObjectTypes.WebModule, RightsType.Use, -1, false);
                        
                        if (isWebEnable)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Acceso validado");
                            Session["UserId"] = MembershipHelper.CurrentUser.ID.ToString();

                            Session["User"] = MembershipHelper.CurrentUser;
                            Session["ConsumeLicense"] = true;
                            Session["IsWindowsUser"] = blnWindowsLogin;
                            Session["CacheTimeOut"] = cacheTimeOut;

                            FormsAuthentication.RedirectFromLoginPage(MembershipHelper.CurrentUser.Name, false);
                        }
                        else
                        {
                            ShowErrorMessage("El usuario no posee acceso al módulo de Servidor Web. Contáctese con el administrador de sistema.");
                        }
                    }
                    else
                    {
                        SetSessionToNull();
                    }
                }
                else
                {
                    ShowErrorMessage("Usuario no encontrado en Zamba.");
                }
            }
            else
            {
                ShowErrorMessage("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.");
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Ha ocurrido un error.");
            Zamba.AppBlock.ZException.Log(ex);
        }
        finally
        {
            sUsers = null;
            sRights = null;
        }
    }

    private void SetSessionToNull()
    {
        Session["UserId"] = null;
        Session["User"] = null;
        Session["IsWindowsUser"] = null;
        Session["CacheTimeOut"] = null;
        Session["ConnectionId"] = null;
        Session["ComputerNameOrIP"] = null;
    }

    private void DoADLogin()
    {
        SUsers susers = null;
        SRights Rights = null;

        try
        {
            string validateADResult = ValidateInAD(txtUserName.Value, txtPassword.Value);

            //Activar el mock cuando se está en entornos de Stardoc y no haya AD.
            //string validateADResult = MockValidateInAD(txtUserName.Text, txtPassword.Text);

            if (string.IsNullOrEmpty(validateADResult))
            {
                Rights = new SRights();
                susers = new SUsers();
                Int64 UserId = susers.GetUserID(txtUserName.Value);

                if (UserId > 0)
                {
                    IUser User = Rights.ValidateLogIn(UserId, ClientType.Web);
                    if (User != null)
                    {
                        int cacheTimeOut;
                        if (!int.TryParse(WebConfigurationManager.AppSettings["CacheTimeOut"], out cacheTimeOut))
                            cacheTimeOut = 60;

                        bool isLicenceOk = DoConsumeLicense(true, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.ID);

                        if (isLicenceOk)
                        {
                            bool wf = Rights.IsWfLicence();
                            MembershipHelper.CurrentUser.WFLic = wf;
                            MembershipHelper.CurrentUser.ConnectionId = (int)Session["ConnectionId"];
                            MembershipHelper.CurrentUser.puesto = (string)Session["ComputerNameOrIP"];

                            //11/06/12: Se agrega validación que tenga activo el módulo Web.
                            bool isWebEnable = UserBusiness.Rights.GetUserRights(MembershipHelper.CurrentUser, ObjectTypes.WebModule, RightsType.Use, -1, false);

                            if (isWebEnable)
                            {
                                Session["UserId"] = MembershipHelper.CurrentUser.ID.ToString();
                                Session["User"] = MembershipHelper.CurrentUser;
                                Session["ConsumeLicense"] = true;
                                Session["IsWindowsUser"] = false;
                                Session["CacheTimeOut"] = cacheTimeOut;
                                
                                FormsAuthentication.RedirectFromLoginPage(MembershipHelper.CurrentUser.Name, false);
                            }
                            else
                            {
                                ShowErrorMessage("El usuario no posee acceso al módulo de Servidor Web. Contáctese con el administrador de sistema.");
                            }
                        }
                        else
                        {
                            SetSessionToNull();
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Usuario no encontrado en Zamba.");
                    }
                }
                else
                {
                    ShowErrorMessage("Usuario no encontrado en Zamba.");
                }
            }
            else
            {
                ShowErrorMessage(validateADResult);
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage("Ha ocurrido un error.");
            Zamba.AppBlock.ZException.Log(ex);
        }
        finally
        {
            susers = null;
            Rights = null;
        }
    }

    private void ShowErrorMessage(string error)
    {
        lblError.InnerText = error;
        lblError.Visible = true;
    }

    private bool DoConsumeLicense(Boolean blnWindowsLogin, string userName, Int64 userId)
    {
        string computerNameOrIp = string.Empty;

        // Si es un usuario windows
        if (blnWindowsLogin)
        {
            // Se obtiene el nombre de la computadora del usuario gracias al componente activeX
            _varComputerName = (HtmlInputHidden)FindControl("vComputerName");

            if (null != _varComputerName && string.IsNullOrEmpty(_varComputerName.Value) == false)
                    computerNameOrIp = _varComputerName.Value;
            else
                computerNameOrIp = GetUserIP();
        }
        else
            // Se obtiene la dirección IP de la computadora del usuario
            computerNameOrIp = GetUserIP();

        SRights sRights = null;
        try
        {
            // Se agrega una nueva pc a la tabla UCM
            // Parámetros: id de usuario actual | usuario de windows | nombre o IP de la computadora del usuario | timeOut | WFAvailable = valor false 
            // para licencia documental
             sRights = new SRights();
             Boolean wf = sRights.IsWfLicence();

            int type = wf ? 1 : 0;

            SUserPreferences SUserPreferences = new SUserPreferences();
            Session["ConnectionId"] = Ucm.NewConnection(userId, userName, computerNameOrIp + "/" + Session.SessionID, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type, false);

            if (Int64.Parse(Session["ConnectionId"].ToString()) > 0)
            {
                SUserPreferences = null;

                // Se guarda la computadora del usuario. Es necesario por si el usuario presiona el botón "Cerrar Sesión" 
                // para que éste pueda eliminarse de la tabla UCM
                if (Session["ConnectionId"] != null)
                    Session["ComputerNameOrIP"] = computerNameOrIp + "/" + Session.SessionID;

                return true;
            } 
            else
            {
                ZClass.raiseerror(new Exception("No se ha podido establecer la conexion a Zamba: " + computerNameOrIp));
                ShowErrorMessage("No se ha podido establecer la conexion a Zamba");
                Session.Abandon();
                Session.Clear();
                sRights = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            ShowErrorMessage("Máximo de licencias conectadas, contáctese con su soporte técnico para adquirir nuevas licencias.");
            Session.Abandon();
            Session.Clear();
            sRights = null;
            return false;
        }
    }

    private string GetUserIP()
    {
        return  Request.UserHostAddress;
    }


    private Boolean IsLoadWindowsUserTrue()
    {
        string userName = getWindowsUser();
        SUsers Users = new SUsers();
        bool exists = !(Users.GetUserByname(userName) == null);        
   
        if (!string.IsNullOrEmpty(userName))
        {
            if (exists)
                return true;
        }
        return false;
    }

    private string getWindowsUser()
    {
        string loggedUser = Request.ServerVariables["LOGON_USER"];

        try
        {
            int pos = 0;
            if (loggedUser.Contains("\\"))
            {
                pos = loggedUser.LastIndexOf("\\");
                loggedUser = loggedUser.Remove(0, pos + 1);
            }

            if (loggedUser.Contains("@"))
            {
                pos = loggedUser.LastIndexOf("@");
                loggedUser = loggedUser.Remove(pos);
            }
        }
        catch
        {
        } 

        return loggedUser;
    }

    private void LoadAspect()
    {
        this.Title = ClientName() + " - Zamba";

        if (_loadWindowsUser)
        {
            string windows_user = getWindowsUser();

            pnlZambaLogin.Visible = false;            
            pnlWindowsLogin.Visible = true;

            chkZambaLogIn.Visible = _allowZambaLogin;
            tblBtnIngresar.CellSpacing = 30;

            lblMensajeLogin.Text = "Bienvenido " + windows_user + ", haga click en el botón para ingresar al sistema.";

            if (!IsLoadWindowsUserTrue())
            {
                lblMensajeLogin.Visible = false;
                lblError.InnerText = "El usuario \"" + windows_user + "\" no es un usuario válido de Zamba Software.";
                btnLoginWindows.Visible = false;
            }
        }
        else
        {
            pnlWindowsLogin.Visible = false;
            pnlZambaLogin.Visible = _allowZambaLogin;
            tblBtnIngresar.CellSpacing = 5;
            lblMensajeLogin.Text = "Por favor ingrese su usuario y password para ingresar";
            txtUserName.Focus();
        }               
    }

    protected void txtUserName_OnTextChanged(object sender, EventArgs a)
    {
        if (!RequiredFieldValidator1.Visible) 
            return;

        if (!String.IsNullOrEmpty(txtUserName.Value.Trim()))
            RequiredFieldValidator1.Text = String.Empty;
    }

    private Boolean IsFormFieldsRigth()
    {
        Boolean flagCorrect = true;

        if (String.IsNullOrEmpty(txtUserName.Value.Trim()))
        {
            RequiredFieldValidator1.Visible = true;
            flagCorrect = false;
        }

        if ((RequiredFieldValidator1.Visible || RequiredFieldValidator2.Visible) && String.IsNullOrEmpty(lblError.InnerText) && String.IsNullOrEmpty(lblMensajeLogin.Text))
        {
            lblMensajeLogin.Text = "Favor de completar todos los campos.";
        }

        return flagCorrect;
    }

    protected string ValidateInAD(string UserName, string UserPass)
    {
        try
        {
            MembershipProvider domainProvider;
            domainProvider = Membership.Providers["ADMembershipProvider"];
            if (domainProvider == null)
            {
                throw new Exception("Error al obtener el provider de AD");
            }

            MembershipUser userMatch = domainProvider.GetUser(UserName, false);
            if (userMatch != null)
            {
                if (userMatch.IsApproved)
                {
                    if (userMatch.IsLockedOut)
                        return "Usuario bloqueado";
                    else
                    {
                        if (domainProvider.ValidateUser(UserName, UserPass))
                        {
                            return string.Empty;
                        }
                        else
                            return "Contraseña incorrecta";
                    }
                }
                else
                    return "Usuario no disponible";
            }
            else
                return "Usuario incorrecto.";
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            return "Error al validar usuario.";
        }
    }

    protected string MockValidateInAD(string UserName, string UserPass)
    {
        return string.Empty;
    }

    public static IHtmlString GetJqueryCoreScript() 
    {
        return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
    }

      public static IHtmlString  RegisterThemeBundles() 
      {
          return Tools.RegisterThemeBundles(HttpContext.Current.Request);
    }


#endregion

}
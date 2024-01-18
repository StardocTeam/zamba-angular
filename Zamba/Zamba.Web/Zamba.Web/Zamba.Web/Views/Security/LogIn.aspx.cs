﻿
using System;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using Zamba.Services;
using Zamba.Core;
using System.Web;
using Zamba.Membership;
using Zamba.Web.Helpers;
using System.Web.UI;
using Zamba;
using Zamba.Web;
using System.Web.UI.WebControls;
using Zamba.FAD;
using System.Collections.Generic;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

public partial class Login : System.Web.UI.Page
{
    #region Attributes

    HtmlInputHidden _varComputerName;
    public HiddenField hiddenCon
    {
        get
        {
            return this.hdnConnectionId;
        }
    }

    #endregion

    #region Events

    bool _allowZambaLogin = false;
    bool _loadWindowsUser = false;
    UserPreferences UP = new UserPreferences();
    UserGroupBusiness UserGroupBusiness = new UserGroupBusiness();
    UserBusiness UB = new UserBusiness();

    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            ZCore ZC = new ZCore();
            if (Zamba.Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.Web");

            ZC.VerifyFileServer(); //Verifica el servidor de archivos, si no esta disponible o configurado lanza exception.

            ZOptBusiness zoptb = new ZOptBusiness();
            Page.Theme = zoptb.GetValue("CurrentTheme");
            if (Page.Theme == null)
            {
                Page.Theme = "Basic";
                Response.Write("Para utilizar Zamba, se debe configurar el Tema a utilizar");
            }
            else
            {
                Session["CurrentTheme"] = Page.Theme;
                zoptb = null;
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("ORACLE not available") || ex.Message.Contains("Conexion no Inicializada correctamente"))
                Response.Redirect("views/CustomErrorPages/databasenotavaliable.html");
            else
            {
                ZClass.raiseerror(ex);
                throw;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack && Request.HttpMethod != "POST")
            {
                FormsAuthentication.SignOut();
                bool.TryParse(WebConfigurationManager.AppSettings["AllowLoginZambaUser"], out _allowZambaLogin);
                bool.TryParse(WebConfigurationManager.AppSettings["LoadWindowsUser"], out _loadWindowsUser);

                //Habilitar para emular AD
                // _loadWindowsUser = true;
                //END Emulacion AD

                Session["flagIsFirstLoad"] = true;
                ZOptBusiness zoptb = new ZOptBusiness();
                String CurrentTheme = zoptb.GetValue("CurrentTheme");
                zoptb = null;

                lnkWebIcon.Attributes.Add("href", "~/App_Themes/" + CurrentTheme + "/Images/webicon.jpg");

                this.Title = ClientName() + " - Zamba";

                if (_loadWindowsUser)
                {
                    btnLogin.Visible = false;
                    pnlZambaLogin.Visible = false;
                    pnlWindowsLogin.Visible = true;
                    btnLoginWindows_Click(null, null);
                }
                else
                {
                    btnLogin.Visible = true;
                    pnlZambaLogin.Visible = true;
                    pnlWindowsLogin.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("ORACLE not available"))
            {
                Server.Transfer("../CustomErrorPages/databasenotavaliable.html");
            }
            else
            {
                ZClass.raiseerror(ex);
                throw;
            }
        }

        bool AuhtenticationMultiple = false;
        string strAuhtenticationMultiple = ConfigurationManager.AppSettings["AllowMultipleAuthentication"].ToString();

        if (!String.IsNullOrEmpty(strAuhtenticationMultiple))
            AuhtenticationMultiple = Boolean.Parse(strAuhtenticationMultiple);

        btnLoginWithOkta.Visible = AuhtenticationMultiple;

        if (AuhtenticationMultiple)
        {
            btnLogin.Visible = true;
        }
    }

    public void btnLoginWindows_Click(object sender, EventArgs e)
    {
        SUsers Users = new SUsers();
        string userName = getWindowsUser();
        //Solo para emular AD
        //userName = "stardoc";
        //END Emulacion AD
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario de Windows: " + userName);
        if (userName.Length > 0)
        {
            ADResources adlogin = new ADResources();
            var userRoles = adlogin.GetRolesForUser(userName);
            IUser newuser = new User();
            IUser LastAdUser = new User();
            try
            {
                if (userRoles != null && userRoles.Count > 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Perfiles en AD del usuario" + userRoles.Count);

                    UserBusiness UB = new UserBusiness();
                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"WinUserName: {userName}");
                    IUser currentWinUser = UB.GetUserByname(userName, true);
                    var UserProperties = adlogin.GetUserProperties(userName);

                    if (currentWinUser != null)
                    {

                        short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                        string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                        bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));
                        if (UserProperties != null)
                        {
                            if (UserProperties.ContainsKey("EMAIL"))
                            {
                                currentWinUser.eMail = new Correo()
                                {
                                    Mail = UserProperties["EMAIL"].ToString(),
                                    UserName = UserProperties["EMAIL"].ToString(),
                                    EnableSsl = enablessl,
                                    ProveedorSMTP = smtpProvider,
                                    Type = MailTypes.NetMail,
                                    Puerto = mailPort
                                };
                            }

                            LastAdUser.Name = userName;
                            LastAdUser.ID = currentWinUser.ID;
                            LastAdUser.eMail = currentWinUser.eMail;
                            if (UserProperties.ContainsKey("NOMBRE"))
                                LastAdUser.Nombres = UserProperties["NOMBRE"].ToString();
                            else
                                LastAdUser.Nombres = userName;

                            if (UserProperties.ContainsKey("APELLIDO"))
                                LastAdUser.Apellidos = UserProperties["APELLIDO"].ToString();
                            else
                                LastAdUser.Apellidos = userName;

                            try
                            {
                                if (UserProperties.ContainsKey("ThumbNailPhoto") && UserProperties["ThumbNailPhoto"] != null)
                                    LastAdUser.ThumbNailPhoto = UserProperties["ThumbNailPhoto"].ToString();
                                else
                                    LastAdUser.ThumbNailPhoto = string.Empty;

                            }
                            catch (Exception ex)
                            {
                                //                                ZClass.raiseerror(ex);
                            }
                            UB.UpdateUser(LastAdUser);
                            UB.UpdateAllById(currentWinUser.ID, currentWinUser.Name, currentWinUser.Password, currentWinUser.eMail.ProveedorSMTP, currentWinUser.eMail.Puerto, currentWinUser.eMail.Mail, null, Convert.ToInt16(currentWinUser.Type));
                        }
                        UserPreferences UP = new UserPreferences();
                        Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, currentWinUser.ID));
                        currentWinUser.TraceLevel = TraceLevel;
                        Zamba.Membership.MembershipHelper.SetCurrentUser(currentWinUser);
                    }
                    else
                    {

                        if (UserProperties != null)
                        {
                            newuser.Name = userName;
                            if (UserProperties.ContainsKey("NOMBRE"))
                                newuser.Nombres = UserProperties["NOMBRE"].ToString();
                            else
                                newuser.Nombres = userName;

                            if (UserProperties.ContainsKey("APELLIDO"))
                                newuser.Apellidos = UserProperties["APELLIDO"].ToString();
                            else
                                newuser.Apellidos = userName;

                            short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                            string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                            bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));

                            if (UserProperties.ContainsKey("EMAIL"))
                            {
                                newuser.eMail = new Correo()
                                {
                                    Mail = UserProperties["EMAIL"].ToString(),
                                    UserName = UserProperties["EMAIL"].ToString(),
                                    EnableSsl = enablessl,
                                    ProveedorSMTP = smtpProvider,
                                    Type = MailTypes.NetMail,
                                    Puerto = mailPort
                                };
                            }

                            newuser.ID = Zamba.Data.CoreData.GetNewID(IdTypes.USERTABLEID);

                            UB.AddUser(newuser);
                            UB.SetNewUser(ref newuser);
                            UserPreferences UP = new UserPreferences();
                            Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, newuser.ID));
                            newuser.TraceLevel = TraceLevel;
                            MembershipHelper.SetCurrentUser(newuser);
                        }
                        else
                        {
                            //newuser.Name = userName;
                            //newuser.Nombres = userName;
                            //newuser.Apellidos = userName;
                            //newuser.ID = Zamba.Data.CoreData.GetNewID(Zamba.Core.IdTypes.USERTABLEID);

                            //UB.AddUser(newuser);
                            //Zamba.Membership.MembershipHelper.SetCurrentUser(newuser);

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "el usuario no  contiene al menos un rol de zamba, se muestran labels de autenticacion");

                            btnLogin.Visible = true;
                            pnlZambaLogin.Visible = true;
                            pnlWindowsLogin.Visible = false;
                            return;
                        }
                    }


                    var user = Zamba.Membership.MembershipHelper.CurrentUser;
                    //List<IUserGroup> GruposZamba = UserGroupBusiness.GetUserGroups(user.ID);
                    ArrayList GruposZamba = UserGroupBusiness.getUserGroups(user.ID);
                    var Finded = false;
                    bool OneZambaRole = false;

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Perfiles en AD del usuario" + userRoles.Count);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupos en Zamba del usuario" + GruposZamba.Count);

                    foreach (string role in userRoles)
                    {
                        //trae solo roles 
                        if (role.ToLower().Contains("zamba"))
                        {
                            foreach (IUserGroup group in GruposZamba)
                            {
                                if (role.ToLower() == group.Name.ToLower())
                                {
                                    Finded = true;
                                    break;
                                }
                                else
                                {
                                    Finded = false;
                                }
                            }

                            if (Finded == false)
                            {
                                long groupId = UserGroupBusiness.GetUserorGroupIdbyName(role);
                                if (groupId != 0)
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se asigna grupo " + role + " " + groupId);

                                    UB.AssignGroup(user.ID, groupId);

                                }
                            }
                            OneZambaRole = true;
                        }
                    }

                    //desasigna
                    //trae solo roles 

                    foreach (IUserGroup group in GruposZamba)
                    {
                        if (group.Name.ToLower().Contains("zamba"))
                        {
                            Finded = false;
                            foreach (string role in userRoles)
                            {
                                if (role.ToLower() == group.Name.ToLower())
                                {
                                    Finded = true;
                                    break;
                                }
                            }

                            if (Finded == false)
                            {
                                long groupId = UserGroupBusiness.GetUserorGroupIdbyName(group.Name.ToLower());
                                if (groupId != 0)
                                {
                                    IUserGroup Igroup = UserGroupBusiness.GetUserGroup(groupId);
                                    UserGroupBusiness.RemoveUser(Igroup, Zamba.Membership.MembershipHelper.CurrentUser);
                                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se procede ah desasignar grupo " + group.Name + " " + groupId);
                                }
                            }
                        }


                    }

                    bool ForceGroupMatch = bool.Parse(ZOptBusiness.GetValueOrDefault("ForceGroupMatch", "True"));
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "ForceGroupMatch esta en : " + ForceGroupMatch.ToString());
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "OneZambaRole esta en : " + OneZambaRole.ToString());

                    if (OneZambaRole || ForceGroupMatch == false)
                    {
                        DoLogin(true);
                    }
                    else
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "el usuario no  contiene al menos un rol de zamba, se muestran labels de autenticacion");

                        btnLogin.Visible = true;
                        pnlZambaLogin.Visible = true;
                        pnlWindowsLogin.Visible = false;
                    }
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "el usuario no tiene roles");

                    //si no tiene roles mostrar login comun
                    btnLogin.Visible = true;
                    pnlZambaLogin.Visible = true;
                    pnlWindowsLogin.Visible = false;
                }
            }




            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo autenticar el usuario en AD");
                btnLogin.Visible = true;
                pnlZambaLogin.Visible = true;
                pnlWindowsLogin.Visible = false;
            }
        }
        else
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "AD no Disponible");

            btnLogin.Visible = true;
            pnlZambaLogin.Visible = true;
            pnlWindowsLogin.Visible = false;
        }
    }



    public string timeOutLogin(IUser user, int connectionId)
    {
        try
        {
            ZOptBusiness zopt = new ZOptBusiness();
            zopt = null;

            SRights sRights = new SRights();

            sRights = null;
            user.ConnectionId = connectionId;
            if (user != null)
            {
                //MembershipHelper.CurrentUser = user;
                SUsers sUsers = null;
                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando base y usuario");
                    if (UB.ValidateDataBase() == true)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Base validada");
                        sUsers = new SUsers();
                        sRights = new SRights();
                        if (Zamba.Membership.MembershipHelper.CurrentUser != null)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario: " + Zamba.Membership.MembershipHelper.CurrentUser.Name);
                            bool isLicenceOk = DoConsumeLicense(false, Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.ID, connectionId);

                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando licencias");
                            if (isLicenceOk)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Licencias validadas");
                                Zamba.Membership.MembershipHelper.CurrentUser.puesto = user.puesto;
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando permisos de acceso a web");


                                ZOptBusiness zoptb = new ZOptBusiness();

                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Acceso validado");

                                HttpContext.Current.Session["ConsumeLicense"] = true;
                                HttpContext.Current.Session["IsWindowsUser"] = false;


                                SRights rights = new SRights();
                                Int32 type = 0;
                                if (Zamba.Membership.MembershipHelper.CurrentUser.WFLic)
                                {
                                    type = 1;
                                }
                                Ucm ucm = new Ucm();
                                ucm.UpdateOrInsertActionTime
                                (Zamba.Membership.MembershipHelper.CurrentUser.ID, Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.puesto, Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId, Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "1")), type);

                                return "loginok";
                            }
                            else
                            {
                                return ("El usuario no posee acceso al módulo de Servidor Web. Contáctese con el administrador de sistema.");
                            }
                        }
                        else
                        {
                            return ("Usuario no encontrado en Zamba.");
                        }
                    }
                    else
                    {
                        return ("Su instalacion no se encuentra registrada, por favor contactese con Stardoc Argentina S.A.");
                    }
                }
                catch (Exception ex)
                {
                    Zamba.AppBlock.ZException.Log(ex);
                    return ("Ha ocurrido un error.");
                }
                finally
                {
                    sUsers = null;
                    sRights = null;
                }
            }

            else
            {
                return "Usuario no registrado";
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    protected void btnLoginZamba_Click(object sender, EventArgs e)

    {
        try
        {
            lblError.InnerText = "";
            if (!IsFormFieldsRigth())
                return;

            if (!validateCredencialsInputChars())
                throw new Exception("Caracteres invalidos");
            SRights sRights = new SRights();
            Zamba.Core.IUser user = sRights.ValidateLogIn(txtUserName.Value, txtPassword.Value, ClientType.Web);

            sRights = null;
            if (user != null)
            {
                clearExpiredAttempsByUsername(user.Name);
                DoLogin(false);
            }
            else
            {
                logAuthAttemps(txtUserName.Value);
                return;
            }
            //}
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("incorrecto") || ex.Message.Contains("incorrecta"))
            {
                logAuthAttemps(txtUserName.Value);
                ShowErrorMessage("Usuario o Clave incorrectas");
                Toastr("Usuario o Clave incorrectas.", "error", "loginValidationError");
            }
            else if (ex.Message.Contains("bloqueado"))
            {
                ZClass.raiseerror(ex);
                ShowErrorMessage("El usuario esta bloqueado, por favor contacte a su administrador de sistema.");
                Toastr("El usuario esta bloqueado, por favor contacte a su administrador de sistema.", "error", "loginError");
            }
            else if (ex.Message.Contains("invalido"))
            {
                ZClass.raiseerror(ex);
                ShowErrorMessage("Se han ingresado caracteres invalidos.");
                Toastr("Se han ingresado caracteres invalidos.", "error", "loginErrorInvalidChars");
            }
            else
            {
                ZClass.raiseerror(ex);
                ShowErrorMessage("Ocurrio un error al validar el usuario.");
                Toastr("Ocurrio un error al validar el usuario.", "error", "loginError");
            }
        }
    }

    #endregion

    #region Methods
    private Boolean validateCredencialsInputChars()
    {
        List<char> invalidChars = new List<char>();
        char[] charList = { '<', '>', ';', '\'', '\"', '\\' };
        invalidChars.AddRange(charList);

        if (txtUserName.Value.ToCharArray().Any(c => invalidChars.Contains(c)))
            return false;
        if (txtPassword.Value.ToCharArray().Any(c => invalidChars.Contains(c)))
            return false;
        return true;
    }
    private void logAuthAttemps(string username)
    {
        if (ConfigurationManager.AppSettings.Get("LockAccountAfterMultipleAttemps") == null)
            return;

        int rangeMinutes = Int32.Parse(ConfigurationManager.AppSettings.Get("LockAccountAfterMultipleAttempsRangeMinutes"));
        int maxAttemps = Int32.Parse(ConfigurationManager.AppSettings.Get("LockAccountAfterMultipleAttemps"));

        if (maxAttemps > 0)
        {

            IUser CurrentUser = new UserBusiness().GetUserByname(username, false);

            if (CurrentUser == null)
                return;
            if (CurrentUser.State == UserState.Bloqueado)
                return;

            string jsonAuthAttemps = "";
            Dictionary<string, List<DateTime>> authAttemps = new Dictionary<string, List<DateTime>>();
            if (HttpContext.Current.Application["AuthAttemps"] != null)
            {

                jsonAuthAttemps = HttpContext.Current.Application["AuthAttemps"].ToString();
                authAttemps = JsonConvert.DeserializeObject<Dictionary<string, List<DateTime>>>(jsonAuthAttemps);

                authAttemps = clearExpiredAttemps(rangeMinutes, authAttemps);
            }
            KeyValuePair<string, List<DateTime>> item = authAttemps.Where(a => a.Key == username).FirstOrDefault();
            List<DateTime> attemptDates = new List<DateTime>();
            if (item.Key == null)
            {
                attemptDates.Add(DateTime.Now);
                authAttemps.Add(username, attemptDates);
            }
            else
            {
                attemptDates = item.Value;

                attemptDates.Add(DateTime.Now);

                KeyValuePair<string, List<DateTime>> newKeyValuePair = new KeyValuePair<string, List<DateTime>>(username, attemptDates);

                authAttemps.Remove(username);
                authAttemps.Add(username, attemptDates);

            }

            if (maxAttemps == authAttemps[username].Count())
            {
                authAttemps.Remove(username);
                new UserBusiness().LockUserByName(username);
            }

            HttpContext.Current.Application["AuthAttemps"] = JsonConvert.SerializeObject(authAttemps);
        }

    }

    private Dictionary<string, List<DateTime>> clearExpiredAttemps(int rangeMinutes, Dictionary<string, List<DateTime>> authAttemps)
    {
        Dictionary<string, List<DateTime>> auxAuthAttemps = new Dictionary<string, List<DateTime>>();
        try
        {
            foreach (KeyValuePair<string, List<DateTime>> item in authAttemps)
            {
                List<DateTime> auxAttemps = item.Value;
                auxAttemps = auxAttemps.Where(a => a.AddMinutes(rangeMinutes) > DateTime.Now).ToList();

                if (auxAttemps.Count > 0)
                    auxAuthAttemps.Add(item.Key, auxAttemps);

            }
            return auxAuthAttemps;
        }
        catch (Exception)
        {
            return authAttemps;
        }
    }

    private void clearExpiredAttempsByUsername(string username)
    {
        try
        {
            Dictionary<string, List<DateTime>> authAttemps = new Dictionary<string, List<DateTime>>();

            if (HttpContext.Current.Application["AuthAttemps"] != null)
            {
                string jsonAuthAttemps = HttpContext.Current.Application["AuthAttemps"].ToString();
                authAttemps = JsonConvert.DeserializeObject<Dictionary<string, List<DateTime>>>(jsonAuthAttemps);
                if (authAttemps.ContainsKey(username))
                {
                    authAttemps.Remove(username);
                    HttpContext.Current.Application["AuthAttemps"] = JsonConvert.SerializeObject(authAttemps);
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private static String ClientName()
    {
        SZOptBusiness zOptBusines = new SZOptBusiness();
        string webViewTitle = zOptBusines.GetValue("WebViewTitle");
        zOptBusines = null;
        return string.IsNullOrEmpty(webViewTitle) ? "Zamba " : webViewTitle;
    }
    private void DoLogin(Boolean blnWindowsLogin)
    {
        SUsers sUsers = null;
        SRights sRights = null;
        RightsBusiness RiB = new RightsBusiness();

        try
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando base y usuario");
            if (UB.ValidateDataBase() == true)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Base validada");
                sUsers = new SUsers();
                sRights = new SRights();

                if (Zamba.Membership.MembershipHelper.CurrentUser != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario: " + Zamba.Membership.MembershipHelper.CurrentUser.Name);

                    bool isLicenceOk = DoConsumeLicense(blnWindowsLogin, Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.ID, 0);

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando licencias");
                    if (isLicenceOk)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Licencias validadas");

                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando permisos de acceso a web");


                        IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                        SRights rights = new SRights();
                        UserPreferences UP = new UserPreferences();
                        try
                        {

                            if (!user.WFLic)
                            {
                                Ucm ucm = new Ucm();
                                ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "30")), 1);
                                Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
                                (Zamba.Membership.MembershipHelper.CurrentUser).WFLic = true;
                                user.WFLic = true;
                            }
                            rights = null;
                        }
                        catch (Exception ex)
                        {
                            Zamba.AppBlock.ZException.Log(ex);
                        }
                        finally
                        {
                            user = null;
                            rights = null;
                            UP = null;
                        }


                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Acceso validado");

                        Session["ConsumeLicense"] = true;
                        Session["IsWindowsUser"] = blnWindowsLogin;

                        try
                        {
                            if (Zamba.Membership.MembershipHelper.CurrentUser.Name.Length > 0)
                            {
                                ADResources adlogin = new ADResources();
                                if (bool.Parse(ZOptBusiness.GetValueOrDefault("FileUserPropertyForAd", "True")))
                                {
                                    try
                                    {
                                        var UserProperties = adlogin.GetUserProperties(Zamba.Membership.MembershipHelper.CurrentUser.Name);

                                        short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                                        string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                                        bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));

                                        if (UserProperties != null && UserProperties.Count > 0)
                                        {
                                            if (UserProperties.ContainsKey("EMAIL"))
                                            {
                                                Zamba.Membership.MembershipHelper.CurrentUser.eMail = new Correo()
                                                {
                                                    Mail = UserProperties["EMAIL"].ToString(),
                                                    UserName = UserProperties["EMAIL"].ToString(),
                                                    EnableSsl = enablessl,
                                                    ProveedorSMTP = smtpProvider,
                                                    Type = MailTypes.NetMail,
                                                    Puerto = mailPort
                                                };
                                            }


                                            if (UserProperties.ContainsKey("NOMBRE"))
                                                Zamba.Membership.MembershipHelper.CurrentUser.Nombres = UserProperties["NOMBRE"].ToString();

                                            if (UserProperties.ContainsKey("APELLIDO"))
                                                Zamba.Membership.MembershipHelper.CurrentUser.Apellidos = UserProperties["APELLIDO"].ToString();


                                            UB.UpdateUser(Zamba.Membership.MembershipHelper.CurrentUser);
                                            UB.UpdateAllById(Zamba.Membership.MembershipHelper.CurrentUser);

                                            try
                                            {
                                                if (UserProperties.ContainsKey("ThumbNailPhoto"))
                                                {
                                                    byte[] ThumbNailPhoto = (Byte[])UserProperties["ThumbNailPhoto"];
                                                    if (ThumbNailPhoto != null && ThumbNailPhoto.Length > 0)
                                                    {
                                                        // Zamba.Membership.MembershipHelper.CurrentUser.ThumbNailPhoto = Convert.ToBase64String(ThumbNailPhoto);

                                                        ZOptBusiness zopt = new ZOptBusiness();
                                                        string ThumbDirectory = zopt.GetValue("ThumbStoreDirectory");

                                                        if (ThumbDirectory == string.Empty)
                                                            throw new Exception("Thumbs Directory For File System is not configured in ZOPT for ThumbStoreDirectory");

                                                        String thumbpath = Path.Combine(ThumbDirectory, "users");
                                                        thumbpath = Path.Combine(thumbpath, Zamba.Membership.MembershipHelper.CurrentUser.ID + ".jpg");

                                                        MemoryStream ms = new MemoryStream(ThumbNailPhoto);
                                                        System.Drawing.Image i = System.Drawing.Image.FromStream(ms);
                                                        i.Save(thumbpath);
                                                    }
                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                if (!ex.Message.Contains("GDI"))
                                                    ZClass.raiseerror(ex);
                                            }
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        ZClass.raiseerror(ex);
                                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo autenticar el usuario en AD");
                                        btnLogin.Visible = true;
                                        pnlZambaLogin.Visible = true;
                                        pnlWindowsLogin.Visible = false;
                                    }
                                }
                            }

                        }
                        catch (Exception)
                        {
                        }

                        var ScriptToDoLogin = string.Empty;
                        try
                        {
                            // var connectionid = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                            // var userid = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                            //var token = UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, GetComputerNameOrIp(false) + HttpContext.Current.Session.SessionID);

                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "timeout", "$(document).ready(function () { if (window.localStorage)  window.localStorage.setItem('ConnId', '" + connectionid + "'); });", true);
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "timeout2", "$(document).ready(function () {  if (window.localStorage) window.localStorage.setItem('UserId', '" + userid + "'); });", true);
                            //ScriptManager.RegisterClientScriptBlock(this, GetType(), "authorizationToken", "$(document).ready(function () {  if (window.localStorage) window.localStorage.setItem('authorizationData', '" + token + "');});", true);

                            //ScriptToDoLogin = "$(document).ready(function () { " +
                            //   " if (window.localStorage){ " +
                            //   " window.localStorage.setItem('ConnId', '" + connectionid + "');" +
                            //   " window.localStorage.setItem('UserId', '" + userid + "');" +
                            //   " window.localStorage.setItem('authorizationData', '" + token + "');" +
                            //   " }});";

                        }
                        catch (Exception ex)
                        {
                            Zamba.AppBlock.ZException.Log(ex);
                        }

                        //if (Page.ClientQueryString.ToLower().Contains("search.html"))
                        //{
                        //    string querystring = string.Empty;

                        //    try
                        //    {
                        //        foreach (var item in Page.ClientQueryString.Split('&'))
                        //        {
                        //            if (!item.ToLower().Contains("user") && !item.ToLower().Contains("search.html"))
                        //                querystring += '&' + item;
                        //        }

                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ZClass.raiseerror(ex);
                        //    }
                        //    if (querystring.ToLower().Contains("user"))
                        //    {
                        //        if (querystring.StartsWith("&")) querystring = querystring.Remove(0, 1);

                        //        string userstring = querystring.Split('&')[0];
                        //        querystring.Replace(userstring, "user=" + Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        //    }

                        //    var connectionid = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                        //    var userid = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

                        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "HttpContext.Current.Request.UserHostAddress.Replace(::1, 127.0.0.1) : " + HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1"));
                        //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Request.Url.Scheme + :// + Request.Url.OriginalString.Split('/')[2] + Request.Url.Scheme + ://" + Request.Url.OriginalString.Split('/')[2] + ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault());
                        //    string token = string.Empty;
                        //    string tokenstr = string.Empty;

                        //    TokenInfo ti = null;
                        //    try
                        //    {

                        //        token = UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1"), Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault());

                        //        if (token.Length > 0)
                        //        {
                        //            try
                        //            {
                        //                ti = JsonConvert.DeserializeObject<TokenInfo>(token);
                        //                tokenstr = ti.token;
                        //            }
                        //            catch (Exception ex)
                        //            {
                        //                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                        //                ti = new TokenInfo();
                        //                ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        //                ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                        //                ti.refreshToken = "";
                        //                ti.useRefreshTokens = false;
                        //                ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                        //            }
                        //        }
                        //        else
                        //        {
                        //            ti = new TokenInfo();
                        //            ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        //            ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                        //            ti.refreshToken = "";
                        //            ti.useRefreshTokens = false;
                        //            ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                        //        }

                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                        //        ti = new TokenInfo();
                        //        ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                        //        ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                        //        ti.refreshToken = "";
                        //        ti.useRefreshTokens = false;
                        //        ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                        //    }

                        //    if (Convert.ToBoolean(Request.QueryString["showModal"]) == true)
                        //    {

                        //        string isReloadWeb = Convert.ToBoolean(Request.QueryString["reloadLogin"]) == true ? "parent.location.reload();" : "CloseModalLogin();";

                        //        ScriptToDoLogin += "var loginContainer = $('#loginContainer');if(loginContainer != undefined){loginContainer.hide();};";
                        //        string DomainName = GetWebConfigElement("ThisDomain");
                        //        FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);

                        //        var script = "$(document).ready(function(){window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "'); window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "'); ExecutePostLoginActions(); " + " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "'); " + isReloadWeb + "}); ";
                        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);

                        //    }
                        //    else
                        //    {
                        //        // string DomainName = hdnthisdomian.Value;
                        //        ScriptToDoLogin += "var loginContainer = $('#loginContainer');if(loginContainer != undefined){loginContainer.hide();};";
                        //        string DomainName = GetWebConfigElement("ThisDomain");
                        //        FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);
                        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", ScriptToDoLogin + @"    
                        //      window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "'); ExecutePostLoginActions();" +
                        //       " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "');" +
                        //          " " +
                        //          " var rurl = location.origin.trim()+ '" + DomainName + "/globalsearch/search/search.html?t=" + tokenstr + "&user=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + querystring + "'; window.location.href =  rurl;", true);
                        //        //                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", @"$(document).ready(function() { var rurl = 'location.origin.trim()+ '" + dominianname + "/../../globalsearch/search/search.html?User=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + querystring + "'; $(location).attr('href', rurl);});", true);
                        //        //Response.Redirect(Page.ClientQueryString.Replace("ReturnUrl=","") + "?User=" + Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        //    }
                        //}
                        if (Page.ClientQueryString != "")
                        {
                            Boolean newCode = true;

                            if (newCode)
                            {
                                Boolean showModal = false;
                                string DomainName = GetWebConfigElement("ThisDomain");
                                string NewQueryString = "";
                                string ReturnURL = "";
                                foreach (String itemQueryString in Request.QueryString)
                                {
                                    if (itemQueryString.ToLower() == "showmodal")
                                    {
                                        showModal = Convert.ToBoolean(Request.QueryString["showModal"]) == true;
                                    }
                                    else if (itemQueryString.ToLower() == "returnurl")
                                    {
                                        ReturnURL = Request.QueryString["ReturnURL"].ToString();
                                    }
                                    else if (itemQueryString.ToLower() == "initsesion")
                                    {

                                    }
                                }
                                if (ReturnURL == "")
                                {
                                    ReturnURL = DomainName + "/globalsearch/search/search.html";
                                }
                                string queryStringFromReturnURL = "";
                                if (ReturnURL.Split('?').Length > 1)
                                {
                                    queryStringFromReturnURL = ReturnURL.Split('?').Last();
                                }
                                String[] ExcludedKeys = { "logout", "token", "t", "u", "user", "userid", "user_id", "" };
                                foreach (String itemQueryString in queryStringFromReturnURL.Split('&'))
                                {
                                    String key = itemQueryString.Split('=').First();
                                    String Value = itemQueryString.Split('=').Last();
                                    if (!ExcludedKeys.Contains(key))
                                    {
                                        NewQueryString = NewQueryString + "&" + key + "=" + Value;
                                    }
                                }





                                var connectionid = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                                var userid = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                                string token = string.Empty;
                                string tokenstr = string.Empty;
                                TokenInfo ti = null;

                                try
                                {

                                    token = UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1"), Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault());
                                    if (token.Length > 0)
                                    {
                                        try
                                        {
                                            ti = JsonConvert.DeserializeObject<TokenInfo>(token);
                                            tokenstr = ti.token;
                                        }
                                        catch (Exception ex)
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());

                                            ti = new TokenInfo();
                                            ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                            ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                            ti.refreshToken = "";
                                            ti.useRefreshTokens = false;
                                            ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");

                                        }
                                    }
                                    else
                                    {
                                        ti = new TokenInfo();
                                        ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                        ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                        ti.refreshToken = "";
                                        ti.useRefreshTokens = false;
                                        ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                    }

                                }
                                catch (Exception ex)
                                {

                                    ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                                    ti = new TokenInfo();
                                    ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                    ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                    ti.refreshToken = "";
                                    ti.useRefreshTokens = false;
                                    ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                }
                                FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);
                                NewQueryString += "&t=" + ti.token + "&user=" + ti.UserId;
                                if (NewQueryString.StartsWith("&"))
                                    NewQueryString = NewQueryString.Substring(1, NewQueryString.Length - 1);

                                if (showModal)
                                {
                                    string isReloadWeb = Convert.ToBoolean(Request.QueryString["reloadLogin"]) == true ? "parent.location.reload();" : "CloseModalLogin();";
                                    var script = "$(document).ready(function(){window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');ExecutePostLoginActions(); " + " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "'); " + isReloadWeb + " }); ";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);
                                }
                                else

                                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", @" 
                             window.localStorage.removeItem('ConnId'); localStorage.setItem('authMethod', 'Zamba'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');  ExecutePostLoginActions();" +
                                   " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "');" +
                                      "  " + " var rurl = location.origin.trim()+ '" + ReturnURL + "?" + NewQueryString + "'; window.location.href =  rurl;", true);




















                            }
                            else
                            {
                                ///////////////////////////////// // desde aqui //////////////////////////////////////////////////////
                                string returnURL = "";
                                Boolean ModalSesion = false;
                                foreach (var item in Page.ClientQueryString.Split('&'))
                                {
                                    if (item.ToLower().StartsWith("returnurl"))
                                    {
                                        returnURL = System.Web.HttpUtility.UrlDecode(item.Split('=').Last());
                                    }
                                    if (item.ToLower().StartsWith("showModal"))
                                    {
                                        ModalSesion = Convert.ToBoolean(Request.QueryString["showModal"]) == true;
                                    }
                                }
                                string querystring = System.Web.HttpUtility.UrlDecode(Page.ClientQueryString.Replace("ReturnUrl=", ""));

                                String NewQueryString = string.Empty;
                                try
                                {
                                    foreach (var item in querystring.Split('&'))
                                    {
                                        if (!item.ToLower().Contains("user") && !item.ToLower().Contains("initsession") && !item.ToLower().Contains("logout"))
                                        {
                                            NewQueryString += '&' + item;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    ZClass.raiseerror(ex);
                                }
                                if (NewQueryString.ToLower().Contains("user"))
                                {
                                    string userstring = NewQueryString.Split('&')[0];
                                    NewQueryString.Replace(userstring, "user=" + Zamba.Membership.MembershipHelper.CurrentUser.ID);
                                }


                                //string DomainName = hdnthisdomian.Value;

                                string DomainName = GetWebConfigElement("ThisDomain");
                                var connectionid = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                                var userid = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                                string token = string.Empty;
                                string tokenstr = string.Empty;
                                TokenInfo ti = null;

                                try
                                {

                                    token = UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1"), Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault());
                                    if (token.Length > 0)
                                    {
                                        try
                                        {
                                            ti = JsonConvert.DeserializeObject<TokenInfo>(token);
                                            tokenstr = ti.token;
                                        }
                                        catch (Exception ex)
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());

                                            ti = new TokenInfo();
                                            ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                            ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                            ti.refreshToken = "";
                                            ti.useRefreshTokens = false;
                                            ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");

                                        }
                                    }
                                    else
                                    {
                                        ti = new TokenInfo();
                                        ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                        ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                        ti.refreshToken = "";
                                        ti.useRefreshTokens = false;
                                        ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                    }

                                }
                                catch (Exception ex)
                                {

                                    ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                                    ti = new TokenInfo();
                                    ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                    ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                    ti.refreshToken = "";
                                    ti.useRefreshTokens = false;
                                    ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                }
                                FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);

                                String url = NewQueryString + "&t=" + tokenstr + "&user=" + Zamba.Membership.MembershipHelper.CurrentUser.ID;

                                if (url.StartsWith("&"))
                                    url = url.Substring(1, url.Length - 1);

                                if (ModalSesion)
                                {
                                    string isReloadWeb = Convert.ToBoolean(Request.QueryString["reloadLogin"]) == true ? "parent.location.reload();" : "CloseModalLogin();";
                                    var script = "$(document).ready(function(){window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');ExecutePostLoginActions(); " + " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "'); " + isReloadWeb + " }); ";
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);
                                }
                                else
                                {
                                    if (!String.IsNullOrEmpty(returnURL))
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", @" 
                             window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');  ExecutePostLoginActions();" +
                                       " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "');" +
                                          "  " + " var rurl = location.origin.trim()+ '" + url + "'; window.location.href =  rurl;", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", @" 
                             window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');  ExecutePostLoginActions();" +
                                       " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "');" +
                                          "  " + " var rurl = location.origin.trim()+ '" + DomainName + "/globalsearch/search/search.html?+ " + url + "'; window.location.href =  rurl;", true);
                                    }
                                }


                            }
                        }
                        //////////////////////////////////////// hasta aqui //////////////////////////////////////// 












































































                        else
                        {


                            var connectionid = Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId.ToString();
                            var userid = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();

                            var token = string.Empty;
                            string tokenstr = string.Empty;
                            TokenInfo ti = null;

                            try
                            {
                                token = UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1"), Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + ConfigurationManager.AppSettings.GetValues("RestApiUrl").FirstOrDefault());
                                if (token.Length > 0)
                                {
                                    try
                                    {
                                        ti = JsonConvert.DeserializeObject<TokenInfo>(token);
                                        tokenstr = ti.token;
                                    }
                                    catch (Exception ex)
                                    {
                                        ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                                        ti = new TokenInfo();
                                        ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                        ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                        ti.refreshToken = "";
                                        ti.useRefreshTokens = false;
                                        ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                    }
                                }
                                else
                                {
                                    ti = new TokenInfo();
                                    ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                    ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                    ti.refreshToken = "";
                                    ti.useRefreshTokens = false;
                                    ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                                }

                            }
                            catch (Exception ex)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
                                ti = new TokenInfo();
                                ti.UserId = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                                ti.userName = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                                ti.refreshToken = "";
                                ti.useRefreshTokens = false;
                                ti.tokenExpire = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy HH:mm:ss");
                            }
                            ScriptToDoLogin += "var loginContainer = $('#loginContainer');if(loginContainer != undefined){loginContainer.hide();};";
                            // string DomainName = hdnthisdomian.Value;
                            string DomainName = GetWebConfigElement("ThisDomain");

                            if (Convert.ToBoolean(Request.QueryString["showModal"]) == true)
                            {
                                string isReloadWeb = Convert.ToBoolean(Request.QueryString["reloadLogin"]) == true ? "parent.location.reload();" : "CloseModalLogin();";

                                FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);
                                var script = "$(document).ready(function(){ window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "'); window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "'); ExecutePostLoginActions(); " + " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "'); " + isReloadWeb + "}); ";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "showModalLogin", script, true);

                            }
                            else
                            {

                                FormsAuthentication.SetAuthCookie(Zamba.Membership.MembershipHelper.CurrentUser.Name, true);
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "OpenRequestURL", ScriptToDoLogin + @"    
                              window.localStorage.removeItem('ConnId'); window.localStorage.setItem('ConnId', '" + connectionid + "');  window.localStorage.removeItem('UserId'); window.localStorage.setItem('UserId', '" + userid + "');  ExecutePostLoginActions();" +
                                " window.localStorage.removeItem('authorizationData'); window.localStorage.setItem('authorizationData', '" + token + "');" +
                                   " " +
                                   " var rurl = location.origin.trim()+ '" + DomainName + "/globalsearch/search/search.html?t=" + tokenstr + "&user=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "'; window.location.href =  rurl;", true);

                            }


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
        //Zamba.Membership.MembershipHelper.CurrentUser = null;

        Session["IsWindowsUser"] = null;
        Session["ConnectionId"] = null;
        Session["ComputerNameOrIP"] = null;
    }
    private void ShowErrorMessage(string error)
    {
        lblError.InnerText = error;
        lblError.Visible = true;
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "hideSpinnerLogin", "window.onload = function(){ hideSpinnerLogin();}", true);
    }
    private bool DoConsumeLicense(Boolean blnWindowsLogin, string userName, Int64 userId, int connectionId)
    {
        string computerNameOrIp = GetComputerNameOrIp(blnWindowsLogin);

        SRights sRights = null;

        try
        {
            // Se agrega una nueva pc a la tabla UCM
            // Parámetros: id de usuario actual | usuario de windows | nombre o IP de la computadora del usuario | timeOut | WFAvailable = valor false 
            // para licencia documental
            sRights = new SRights();
            Int32 timeout = 180;
            Int32.TryParse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "180"), out timeout);
            if (timeout == 0) timeout = 180;
            Ucm ucm = new Ucm();
            var ConnectionID = ucm.NewConnection(userId, userName, computerNameOrIp + "/" + HttpContext.Current.Session.SessionID, timeout, 0, false).ToString();

            //if (!Page.IsPostBack)
            //{
            //    ConnectionID = connectionId.ToString();
            //}

            Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId = Convert.ToInt32(ConnectionID);

            if (Int64.Parse(ConnectionID) > 0)
            {

                // Se guarda la computadora del usuario. Es necesario por si el usuario presiona el botón "Cerrar Sesión" 
                // para que éste pueda eliminarse de la tabla UCM
                if (ConnectionID != null)
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
    private string GetComputerNameOrIp(bool blnWindowsLogin)
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
        return computerNameOrIp;
    }
    private string GetUserIP()
    {
        return HttpContext.Current.Request.UserHostAddress;
    }
    //public static string DetermineCompName(string IP)
    //{
    //    IPAddress myIP = IPAddress.Parse(IP);
    //    IPHostEntry GetIPHost = Dns.GetHostEntry(myIP);
    //    List<string> compName = GetIPHost.HostName.ToString().Split('.').ToList();
    //    return compName.First();
    //}
    private string getWindowsUser()
    {
        string userName = System.Web.HttpContext.Current.User.Identity.Name;
        if (userName.Contains("\\"))
        {
            userName = userName.Split(Char.Parse("\\"))[1];
        }
        //string userName = "stardoc";
        ZTrace.WriteLineIf(ZTrace.IsInfo, " Se obtiene el usuario de windows :  " + userName);
        try
        {
            if (userName.Length == 0)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error al obtener usuario de windows");

                userName = HttpContext.Current.Request.LogonUserIdentity.Name;
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se intenta obtener Usuario de windows alternativo:  " + userName);
            }

            int pos = 0;
            if (userName.Contains("\\"))
            {
                pos = userName.LastIndexOf("\\");
                userName = userName.Remove(0, pos + 1);
            }

            if (userName.Contains("@"))
            {
                pos = userName.LastIndexOf("@");
                userName = userName.Remove(pos);
            }
        }
        catch (Exception ex)
        {

            ZClass.raiseerror(ex);
        }

        return userName;
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
            //txtUserName.Style.Value;
            //Toastr("Faltan campos por llenar.", "warning", "validateData");
            flagCorrect = false;
        }

        //if ((RequiredFieldValidator1.Visible || RequiredFieldValidator2.Visible) && String.IsNullOrEmpty(lblError.InnerText) && String.IsNullOrEmpty(lblMensajeLogin.Text))
        if ((RequiredFieldValidator1.Visible || RequiredFieldValidator2.Visible))
        {
            Toastr("Favor de completar todos los campos.", "warning", "fillData");
            //lblMensajeLogin.Text = "Favor de completar todos los campos.";
        }

        return flagCorrect;
    }

    private void Toastr(string text, string toastrType, string scriptName)
    {
        StringBuilder SB = new StringBuilder();

        SB.Append("<script type='text/javascript'>");
        SB.Append("toastr." + toastrType.ToLower() + "('" + text + "');");
        SB.Append("</script>");

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), scriptName, SB.ToString(), false);
    }


    protected string MockValidateInAD(string UserName, string UserPass)
    {
        return string.Empty;
    }
    public static IHtmlString RegisterThemeBundles()
    {
        return Tools.RegisterThemeBundles(HttpContext.Current.Request);
    }

    public string GetWebConfigElement(string element)
    {
        string item = System.Configuration.ConfigurationManager.AppSettings[element]; ;
        if (item == null)
        {
            throw new NullReferenceException("La referencia en el archivo Web Config es nula.");
        }
        return item;
    }
    #endregion
}
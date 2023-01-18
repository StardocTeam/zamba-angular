using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Zamba.AppBlock;
using Zamba.Core;
using Zamba.Help.Models;
using Zamba.Membership;
using Zamba.Services;

namespace Zamba.Help.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == false)
                {
                    Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                    ZC.InitializeSystem("Zamba.Web");
                    // IUser currentUser = UserBusiness.Rights.ValidateLogIn(22357, ClientType.Web);
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
        }

        string _varComputerName;
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model,string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.password)) model.password = string.Empty;

                    if (validateUser(model))
                    {
                        FormsAuthentication.SetAuthCookie(model.user,true);
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            //return View(Redirect(Url.Action("Index", "Home")).Url);
                           return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
            return View();
        }

        private bool validateUser(LoginViewModel model)
        {
            try
            {
                ZOptBusiness zopt = new ZOptBusiness();
                string useADLogin = zopt.GetValue("UseADLogin");

                bool useLogin = false;

                if (!string.IsNullOrEmpty(useADLogin)) useLogin = bool.Parse(useADLogin);

                zopt = null;

                if (!(useLogin && DoADLogin(model)))
                {
                    SRights sRights = new SRights();
                    Zamba.Core.IUser user = sRights.ValidateLogIn(model.user, model.password, ClientType.Web);
                    sRights = null;

                    if (user != null)
                    {
                        Session["UserHelp"] = user;
                        DoLogin(false);
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
            return false;
        }

        private bool DoLogin(Boolean blnWindowsLogin)
        {
            SUsers sUsers = null;
            SRights sRights = null;
            try
            {
                if (UserBusiness.ValidateDataBase() && MembershipHelper.CurrentUser != null)
                {
                    sUsers = new SUsers();
                    sRights = new SRights();

                    int cacheTimeOut;

                    if (!int.TryParse(WebConfigurationManager.AppSettings["CacheTimeOut"], out cacheTimeOut))
                        cacheTimeOut = 60;

                    bool isLicenceOk = DoConsumeLicense(blnWindowsLogin, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.ID, 0);

                    bool validateUserAndServer = RightsBusiness.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.WebModule, RightsType.Use, -1) || Zamba.Servers.Server.AppConfig.SERVER.ToLower().Contains("www.stardoc") || Zamba.Servers.Server.AppConfig.SERVER.ToLower().Contains("yoda");

                    if (isLicenceOk && (validateUserAndServer))
                    {

                        Session["UserId"] = MembershipHelper.CurrentUser.ID.ToString();
                        Session["User"] = MembershipHelper.CurrentUser;
                        Session["UserName"] = MembershipHelper.CurrentUser.Name.ToString();
                        Session["ConsumeLicense"] = true;
                        Session["IsWindowsUser"] = blnWindowsLogin;
                        //Session["CacheTimeOut"] = cacheTimeOut;

                        return true;

                    }

                }

            }
            catch (Exception ex)
            {
                ZException.Log(ex);
            }
            finally
            {
                sUsers = null;
                sRights = null;
            }
            return false;
        }
        private bool DoADLogin(LoginViewModel model)
        {
            SUsers susers = null;
            SRights Rights = null;

            //Activar el mock cuando se está en entornos de Stardoc y no haya AD.
            //string validateADResult = MockValidateInAD(txtUserName.Text, txtPassword.Text);
            try
            {
                if (string.IsNullOrEmpty(ValidateInAD(model.user, model.password)))
                {
                    Rights = new SRights();
                    susers = new SUsers();
                    Int64 UserId = susers.GetUserID(model.user);
                    IUser User;
                    if (UserId > 0 && (User = Rights.ValidateLogIn(UserId, ClientType.Web)) != null)
                    {
                        int cacheTimeOut;
                        if (!int.TryParse(WebConfigurationManager.AppSettings["CacheTimeOut"], out cacheTimeOut))
                            cacheTimeOut = 60;


                        if (DoConsumeLicense(true, MembershipHelper.CurrentUser.Name, MembershipHelper.CurrentUser.ID, 0))
                        {
                            MembershipHelper.CurrentUser.ConnectionId = (int)Session["ConnectionId"];
                            MembershipHelper.CurrentUser.puesto = (string)Session["ComputerNameOrIP"];

                            //11/06/12: Se agrega validación que tenga activo el módulo Web.
                            bool isWebEnable = RightsBusiness.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.WebModule, RightsType.Use, -1);

                            if (isWebEnable || Zamba.Servers.Server.AppConfig.SERVER.ToLower().Contains("www.stardoc") || Zamba.Servers.Server.AppConfig.SERVER.ToLower().Contains("yoda"))

                            {
                                Session["UserId"] = MembershipHelper.CurrentUser.ID.ToString();
                                Session["User"] = MembershipHelper.CurrentUser;
                                Session["ConsumeLicense"] = true;
                                Session["IsWindowsUser"] = false;
                                Session["CacheTimeOut"] = cacheTimeOut;

                                return true;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            return false;
        }


        protected string ValidateInAD(string UserName, string UserPass)
        {
            MembershipProvider domainProvider;
            domainProvider = System.Web.Security.Membership.Providers["ADMembershipProvider"];
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



        private bool DoConsumeLicense(Boolean blnWindowsLogin, string userName, Int64 userId, int connectionId)
        {
            string computerNameOrIp = string.Empty;

            // Si es un usuario windows
            if (blnWindowsLogin)
            {
                // Se obtiene el nombre de la computadora del usuario gracias al componente activeX
                _varComputerName = HttpContext.User.Identity.Name;

                if (null != _varComputerName && string.IsNullOrEmpty(_varComputerName) == false)
                    computerNameOrIp = _varComputerName;
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
                UserPreferences UserPreferences = new UserPreferences();

                Int32 ConnectionID = Ucm.NewConnection(userId, userName, computerNameOrIp + "/" + HttpContext.Session.SessionID, Int16.Parse(UserPreferences.getValue("TimeOut", Zamba.Sections.UserPreferences, "180")), 0, false);

                MembershipHelper.CurrentUser.ConnectionId = (ConnectionID);


                if (ConnectionID > 0)
                {
                    UserPreferences = null;

                    // Se guarda la computadora del usuario. Es necesario por si el usuario presiona el botón "Cerrar Sesión" 
                    // para que éste pueda eliminarse de la tabla UCM
                    Session["ComputerNameOrIP"] = computerNameOrIp + "/" + Session.SessionID;

                    return true;
                }
                else
                {
                    ZClass.raiseerror(new Exception("No se ha podido establecer la conexion a Zamba: " + computerNameOrIp));
                    Session.Abandon();
                    Session.Clear();
                    sRights = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                Session.Abandon();
                Session.Clear();
                sRights = null;
                return false;
            }
        }
        private string GetUserIP()
        {
            return HttpContext.Request.UserHostAddress;
        }
    }
}
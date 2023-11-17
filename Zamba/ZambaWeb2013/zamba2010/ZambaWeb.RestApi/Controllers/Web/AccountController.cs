using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using Newtonsoft.Json;
using System.Linq;
using Zamba.Core.Searchs;
using Zamba;
using System.Net.Http;
using Nelibur.ObjectMapper;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security;
using System.Globalization;
using System.Net;
using Zamba.Services;
using System.Web;
using ZambaWeb.RestApi.ViewModels;
using Microsoft.Owin.Cors;
using System.Web.Script.Serialization;
using Zamba.Framework;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.Membership;
using Zamba.FAD;
using ZambaWeb.RestApi.AuthorizationRequest;
using System.Configuration;
using System.IO;
using System.Text;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Account")]
    [RequestResponseController]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class AccountController : ApiController
    {
        #region Contructor&ClassHelper
        public AccountController()
        {

            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        private void SaveZss(Zss zss)
        {
            var zssFactory = new ZssFactory();
            zssFactory.SetZssValues(zss);
        }

        #endregion

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("Login")]
        [OverrideAuthorization]
        public IHttpActionResult Login(LoginVM l)
        {
            if (l.UserName == null || l.UserName == string.Empty)
                return StatusCode(HttpStatusCode.BadRequest);
            try
            {
                IUser user;
                try
                {
                    UserBusiness UB = new UserBusiness();
                    if (l.Password == null) l.Password = string.Empty;
                    l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1","127.0.0.1");
                    user = UB.ValidateLogIn(l.UserName, l.Password ?? string.Empty, ClientType.Service);
                    UB = null;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    //JS y C# reconoce Unauthorized como error 404 - No sirve para detectar error especifico        
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.ValidUserError)));
                }
                ZTrace.WriteLineIf(ZTrace.IsInfo, $"login By VM: {user.ID}");

                var tokenString = GetTokenString(l, user);
                ZTrace.WriteLineIf(ZTrace.IsInfo, $"t: {tokenString.ToString()}");
                var tI = new TokenInfo
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userName = user.Name,
                    UserId = user.ID
                };
                user.ConnectionId = int.Parse(tI.connectionId);
                UserPreferences UP = new UserPreferences();
                Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, user.ID));
                user.TraceLevel = TraceLevel;
                Zamba.Membership.MembershipHelper.SetCurrentUser(user);
                return Ok(tI);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        private JObject GetTokenString(LoginVM l, IUser user, string OktaAccessToken = "", string OktaIdToken = "")
        {
            try
            {
                //Obtengo token si todavia no caduco
                UserToken uToken = new UserToken();
                JObject tokenInfo = uToken.GetTokenIfIsValid(user);
                if (tokenInfo != null)
                {
                    return (tokenInfo);
                }
                else
                {
                    tokenInfo = uToken.GenerateToken(user);
                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                    l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");
                    Ucm ucm = new Ucm();
                    Int32 timeOut = Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, 30, user.ID));
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("TimeOut: {0}", timeOut.ToString()));
                    user.ConnectionId = Int32.Parse(ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());
                    Zss zss = new Zss()
                    {
                        UserId = user.ID,
                        ConnectionId = user.ConnectionId,
                        CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
                        TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
                        Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
                        OktaAccessToken = OktaAccessToken,
                        OktaIdToken = OktaIdToken
                    };
                    SaveZss(zss);
                    HttpContext.Current.Session["TokenExpires"] = DateTime.Now.AddDays(1).ToString();
                    HttpContext.Current.Session["BearerToken"] = zss.Token;
                    UserBusiness UB = new UserBusiness();
                    UB.ValidateLogIn(user.ID, ClientType.Web);
                    return (tokenInfo);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
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

        [AcceptVerbs("GET", "POST")]
        [Route("LoginImap")]
        public IHttpActionResult LoginImap(LoginVM loginVM, HttpRequestMessage Request)
        {
            try
            {
                //string ip = HttpRequestMessageExtensions.GetClientIpAddress(Request);
                //loginVM.ComputerNameOrIp = ip;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo Login");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.UserName);
                loginVM.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.ComputerNameOrIp);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            }

            if (loginVM.UserName == null || loginVM.UserName == string.Empty)
            {
                ZClass.raiseerror(new Exception("El usuario es nulo"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("El usuario es nulo")));
            }
            if (loginVM.Password == null || loginVM.Password == string.Empty)
            {
                ZClass.raiseerror(new Exception("La clave es nula"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("La clave es nula")));
            }


            try
            {
                IUser user = null;
                try
                {
                    UserBusiness UB = new UserBusiness();
                    user = UB.GetUserByname(loginVM.UserName, false);
                    //HttpRequestMessageExtensions.GetClientIpAddress();
                    if (user == null)
                    {

                        IUser newuser = new User();
                        newuser.Name = loginVM.UserName;
                        newuser.Password = loginVM.Password;

                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController Creacion de Usuario nuevo");
                            if (loginVM.name != null && loginVM.name != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.name);
                                newuser.Nombres = loginVM.name;
                            }
                            else
                                newuser.Nombres = loginVM.UserName;

                            if (loginVM.lastName != null && loginVM.lastName != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.lastName);
                                newuser.Apellidos = loginVM.lastName;
                            }
                            else
                                newuser.Apellidos = loginVM.UserName;

                            if (loginVM.eMail != null && loginVM.eMail != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.eMail);

                                short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                                string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                                bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));

                                newuser.eMail = new Correo()
                                {
                                    Mail = loginVM.eMail,
                                    UserName = loginVM.eMail,
                                    EnableSsl = enablessl,
                                    ProveedorSMTP = smtpProvider,
                                    Type = MailTypes.NetMail,
                                    Puerto = mailPort
                                };
                            }



                            newuser.ID = Zamba.Data.CoreData.GetNewID(IdTypes.USERTABLEID);
                            UserPreferences UP = new UserPreferences();
                            Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, user.ID));
                            user.TraceLevel = TraceLevel;
                            MembershipHelper.SetCurrentUser(newuser);


                            user = UB.ValidateLogIn(loginVM.UserName, loginVM.Password ?? string.Empty, ClientType.Service);
                            UB = null;

                            ZOptBusiness zopt = new ZOptBusiness();

                            string GroupIds = ZOptBusiness.GetValueOrDefault("DefaultGroupForExternals", "11526386");

                            try
                            {
                                long groupId = Int64.Parse(GroupIds);
                                if (groupId != 0)
                                {
                                    UB.AssignGroup(user.ID, groupId);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        catch (Exception e)
                        {
                            ZClass.raiseerror(e);
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al dar de alta el usuario");
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(e.ToString())));
                        }
                    }
                    else
                    {
                        user = UB.ValidateLogIn(loginVM.UserName, loginVM.Password ?? string.Empty, ClientType.Service);
                        UB = null;
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
                }


                var tokenString = GetTokenString(loginVM, user);

                var userInfo = new
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userID = EncriptString(user.ID.ToString())
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        private string EncriptString(string value)
        {
            try
            {
                Zamba.Tools.Encryption encript = new Zamba.Tools.Encryption();
                return encript.EncryptNewStringNonShared(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //private JObject GetTokenString(LoginVM l, IUser user)
        //{
        //    //Obtengo token si todavia no caduco
        //    UserToken uToken = new UserToken();
        //    JObject tokenInfo = uToken.GetTokenIfIsValid(user);
        //    if (tokenInfo != null)
        //    {
        //        ZTrace.WriteLineIf(ZTrace.IsInfo, "Token existente Valido");
        //        return (tokenInfo);
        //    }
        //    else
        //    {
        //        ZTrace.WriteLineIf(ZTrace.IsError, "Generando Token");
        //        tokenInfo = uToken.GenerateToken(user);
        //        Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
        //        l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");

        //        Ucm ucm = new Ucm();
        //        ZTrace.WriteLineIf(ZTrace.IsVerbose, "New Connection");

        //        Int32 timeOut = Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, 30, user.ID));
        //        ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("TimeOut: {0}", timeOut.ToString()));
        //        user.ConnectionId = Int32.Parse(ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());

        //        ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Connection: {0}", user.ConnectionId.ToString()));
        //        Zss zss = new Zss()
        //        {
        //            UserId = user.ID,
        //            ConnectionId = user.ConnectionId,
        //            CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
        //            TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
        //            Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
        //        };
        //        SaveZss(zss);
        //        HttpContext.Current.Session["TokenExpires"] = zss.TokenExpireDate;
        //        HttpContext.Current.Session["BearerToken"] = zss.Token;
        //        UserBusiness UB = new UserBusiness();
        //        UB.ValidateLogIn(user.ID, ClientType.Web);
        //        return (tokenInfo);
        //    }
        //}

        //private void SaveZss(Zss zss)
        //{
        //    var zssFactory = new ZssFactory();
        //    zssFactory.SetZssValues(zss);
        //}

        //private static string TokenExpires()
        //{
        //    var date = HttpContext.Current.Session["TokenExpires"];
        //    return date == null ? "" : date.ToString();
        //}

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("LoginById")]
        public IHttpActionResult LoginById(Int64 userId)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, $"loginById: {userId}");
                UserBusiness UserBusiness = new UserBusiness();
                IUser user = UserBusiness.GetUserById(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                var l = new LoginVM
                {
                    UserName = user.Name,
                    Password = user.Password,
                    ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1","127.0.0.1")
            };
                var tokenString = GetTokenString(l, user);
                var tI = new TokenInfo
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userName = user.Name,
                };
                user.ConnectionId = int.Parse(tI.connectionId);
                UserPreferences UP = new UserPreferences();
                Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, user.ID));
                user.TraceLevel = TraceLevel;
                Zamba.Membership.MembershipHelper.SetCurrentUser(user);
                var js = JsonConvert.SerializeObject(tI);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [System.Web.Http.AcceptVerbs("GET")]
        // [AllowAnonymous]
        [Route("User")]
        public IHttpActionResult GetUser()
        {
            try
            {
              
                var user = GetUser(null);
                return Ok(user);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("Password")]
        public string ChangePassword(UserChangePassword user)
        {
            string errormessage;
            try
            {
                UserBusiness UserBusiness = new UserBusiness();
                IUser User = UserBusiness.GetUserById(user.Userid);

                if (String.Compare(user.CurrentPassword.Trim(), User.Password) == 0)
                {


                    if (String.Compare(user.NewPassword.Trim(), user.NewPassword2.Trim(), true) == 0)
                    {
                        //valido CaseSensitive
                        if (String.Compare(user.NewPassword.Trim(), user.NewPassword2.Trim(), false) == 0)
                        {

                            User.Password = user.NewPassword;
                            UserBusiness.UpdateUserPassword(User);
                            errormessage = "Contraseña actualizada satisfactoriamente";
                            return errormessage;
                        }
                        else
                        {
                            errormessage = "Las claves no coindicen. Recuerde que el Password es CaseSensitive";
                            return errormessage;

                        }

                    }
                    else
                    {
                        errormessage = "La confirmación de la nueva contraseña es incorrecta.";
                        return errormessage;
                    }
                }
                else
                {
                    errormessage = "Contraseña de usuario incorrecta.";
                    return errormessage;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("removeZssUser")]
        public void removeZssUser(int Userid)
        {
            try
            {
                if (Userid != null && Userid > 0)
                {
                    var zssFactory = new ZssFactory();
                    zssFactory.RemoveZss(Userid);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("RemoveConnection")]
        public void RemoveConnectionFromWeb(int ConnId)
        {
            try
            {
                SRights sr = new SRights();

                sr.RemoveConnectionFromWeb(ConnId);
                Zamba.Membership.MembershipHelper.SetCurrentUser(null);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("IsConnectionActive")]
        public bool IsConnectionActive()
        {
            try
            {
                 var IsValid = MembershipHelper.CurrentUser == null  ? false : true;
                return (IsValid);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("CheckToken")]
        public bool CheckToken(int UserId, string Token)
        {
            try
            {
                ZssFactory zss = new ZssFactory();
                if (!Validator.FindInvalidChars(Token))
                {
                    var IsValid = zss.ChekTokenInDatabase(UserId, Token);
                    return (IsValid);
                }
                else {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetDefaultMainMenuItem")]
        public string GetDefaultMainMenuItem(long UserId)
        {
            try
            {
                var Data = "";
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                Data = UP.getValue("DefaultMainMenuItem", Zamba.UPSections.UserPreferences, "Home");
                return Data;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SetDefaultMainMenuItem")]
        public void SetDefaultMainMenuItem(long UserId, string View)
        {
            try
            {
                UserPreferences.setValue("DefaultMainMenuItem", View, Zamba.UPSections.UserPreferences);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLastMainMenuItem")]
        public string GetLastMainMenuItem(long UserId)
        {
            try
            {
                var Data = "";
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                Data = UP.getValue("LastMainMenuItem", Zamba.UPSections.UserPreferences, "Home");
                return Data;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SetLastMainMenuItem")]
        public void SetLastMainMenuItem(long UserId, string View)
        {
            try
            {
                UserPreferences.setValue("LastMainMenuItem", View, Zamba.UPSections.UserPreferences);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SetLastSidebarLockedState")]
        public void SetLastSidebarLockedState(long UserId, string LockedState)
        {
            try
            {
                UserPreferences.setValue("LastSidebarLockedState", LockedState, Zamba.UPSections.UserPreferences);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetLastSidebarLockedState")]
        public string GetLastSidebarLockedState(long UserId)
        {
            try
            {
                var Data = "";
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                              Data = UP.getValue("LastSidebarLockedState", Zamba.UPSections.UserPreferences, "0");
                return Data;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SetDefaultTabsHome")]
        public void SetDefaultTabsHome(long UserId, string Mode)
        {
            try
            {
                UserPreferences.setValue("DefaultTabsHome", Mode, Zamba.UPSections.UserPreferences);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetDefaultTabsHome")]
        public string GetDefaultTabsHome(long UserId)
        {
            try
            {
                var Data = "";
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                Data = UP.getValue("DefaultTabsHome", Zamba.UPSections.UserPreferences, "HomeMain");
                return Data;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }


        


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetConfig")]
        public string GetConfig(long UserId, string ConfigName)
        {
            try
            {
                Zamba.Core.UserPreferences userConfig = new Zamba.Core.UserPreferences();
                var result = userConfig.getEspecificUserValue(ConfigName, UPSections.UserPreferences, "", UserId);
                return result;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [isGenericRequest]
        [Route("getUserPreferences")]
        public IHttpActionResult getUserPreferences(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                    if (paramRequest.Params != null)
                    {
                        var PreferenceName = paramRequest.Params["PreferenceName"].ToString();

                        Zamba.Core.UserPreferences userConfig = new Zamba.Core.UserPreferences();
                        var result = userConfig.getEspecificUserValue(PreferenceName, UPSections.UserPreferences, "", paramRequest.UserId);
                        return Ok(result);
                    }
                    return Ok();
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("getSystemPreferences")]
        public IHttpActionResult getSystemPreferences(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest != null)
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                    if (paramRequest.Params != null)
                    {
                        var PreferenceName = paramRequest.Params["PreferenceName"].ToString();

                        Zamba.Core.ZOptBusiness userConfig = new Zamba.Core.ZOptBusiness();
                        var result = userConfig.GetValue(PreferenceName);
                        return Ok(result);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ;
            }
        }
        public Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("User"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
                }


                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }


        [AcceptVerbs("GET")]
        [Route("IsPasswordExpired/{userId}")]
        public IHttpActionResult IsPasswordExpired(long userId)
        {
            try
            {
                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                UserBusiness UB = new UserBusiness();
                bool isExpired = UB.IsPasswordExpired(userId);

                return Ok(isExpired);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET")]
        [Route("ClearUserCache/{userId}")]
        public IHttpActionResult ClearUserCache(long userId)
        {
            try
            {
                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                UserBusiness UB = new UserBusiness();
                UB.ClearUserCache(userId);

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [isGenericRequest]
        public IHttpActionResult GetUserRights(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));


                RightsBusiness RiB = new RightsBusiness();
                bool HasRightToSearchWeb = RiB.GetUserRights(user.ID, ObjectTypes.Users, RightsType.Admin, -1);

                var result = JsonConvert.SerializeObject(HasRightToSearchWeb, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("GetUserRight")]
        public IHttpActionResult GetUserRight(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params != null && paramRequest.Params.ContainsKey("userRightKey"))
                {
                    RightsType right = (RightsType)int.Parse(paramRequest.Params["userRightKey"]);
                    RightsBusiness RiB = new RightsBusiness();
                    bool hasRight = RiB.GetUserRights(user.ID, ObjectTypes.Users, right, -1);

                    var result = JsonConvert.SerializeObject(hasRight, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(result);
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("NewGetUserRight")]
        [isGenericRequest]
        public IHttpActionResult NewGetUserRight(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params != null && paramRequest.Params.ContainsKey("RightType") && paramRequest.Params.ContainsKey("ObjectId"))
                {
                    ObjectTypes ObjectId;
                    RightsType RightType;
                    try
                    {
                         ObjectId = (ObjectTypes)int.Parse(paramRequest.Params["ObjectId"]);
                         RightType = (RightsType)int.Parse(paramRequest.Params["RightType"]);

                    }
                    catch (Exception)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                    }
                    
                    bool hasRight = new RightsBusiness().GetUserRights(user.ID, ObjectId, RightType, -1);

                    var result = JsonConvert.SerializeObject(hasRight, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(result);
                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

        }

        [AcceptVerbs("GET")]
        [Route("MigrateInfoUserAdInZamba/{userId}")]
        public IHttpActionResult MigrateInfoUserAdInZamba(long userId)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "inicio proceso de migracion usuario:" + userId);
                var user = GetUser(userId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                ADResources adlogin = new ADResources();
                WFBusiness WFB = new WFBusiness();
                var urs = WFB.GetAllUsers();
                var UserName = string.Empty;
                foreach (DictionaryEntry item in urs)
                {
                    try
                    {
                        Zamba.Core.User InfoUser = (User)item.Value;
                        UserName = InfoUser.Name;
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "usuario a insertar info de ad en zamba:" + UserName);
                        if (!UserName.Contains("Ninguno"))
                            adlogin.GetUserProperties(UserName);
                    }
                    catch (Exception)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsError, "fallo la inserccion para el usuario" + UserName);
                    }
                   
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetIsMultipleSesion")]
        [OverrideAuthorization]
        public IHttpActionResult GetIsMultipleSesion()
        {
            bool AuhtenticationMultiple = false;
            try
            {
                string strAuhtenticationMultiple = ConfigurationManager.AppSettings["AllowMultipleAuthentication"].ToString();
                if (!String.IsNullOrEmpty(strAuhtenticationMultiple))
                    AuhtenticationMultiple = Boolean.Parse(strAuhtenticationMultiple);
            }
            catch (Exception)
            {
            }
            return Ok(AuhtenticationMultiple);
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("GetOktaInformation")]
        [OverrideAuthorization]
        public IHttpActionResult GetOktaInformation()
        {
            OktaPublicInformation oktaInformation = new OktaPublicInformation();
            var js = JsonConvert.SerializeObject(oktaInformation);
            return Ok(js);
        }
        public class OktaPrivateInformation : OktaPublicInformation
        {
            public string ClientSecret { get { return System.Web.Configuration.WebConfigurationManager.AppSettings["OktaClientSecret"]; } }
            public string BasicAuthenticacion
            {
                get
                {
                    return "Basic " + Convert.ToBase64String((this.clientId + ":" + this.ClientSecret)
                                            .ToCharArray()
                                            .Select(c => (byte)c)
                                            .ToArray());
                }
            }
        }
        public class OktaPublicInformation
        {
            public string baseUrl { get { return System.Web.Configuration.WebConfigurationManager.AppSettings["OktaBaseUrl"]; } }
            public string clientId { get { return System.Web.Configuration.WebConfigurationManager.AppSettings["OktaClientId"]; } }
            public string redirectURL { get { return System.Web.Configuration.WebConfigurationManager.AppSettings["OktaURLRedirect"]; } }
        }
        [AllowAnonymous]
        [HttpPost]
        [OverrideAuthorization]
        [Route("generateOKTAStateValue")]
        public IHttpActionResult generateOKTAStateValue()
        {
            StateOkta itemState = new StateOkta();
            Guid guid = Guid.NewGuid();
            itemState.state = Guid.NewGuid().ToString();
            itemState.expiration = DateTime.Now.AddMinutes(2);
            ListStatesOkta.Add(itemState);
            return Ok(itemState.state);
        }





        [AllowAnonymous]
        [Route("LoginOKTA")]
        [OverrideAuthorization]
        public IHttpActionResult LoginOKTA(String access_token, String id_token, String code)
        {
            try
            {
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se solicita access_token con code=" + code);
                if (String.IsNullOrEmpty(access_token))
                    access_token = GetOktaAccessToken(code).access_token;
                // GetOktaRefreshToken(access_token);
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se obtuvo access_token =" + access_token);
                OktaPrivateInformation OktaInformation = new OktaPrivateInformation();
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se usa access_token para buscar userintrospection");
                OktaUserIntrospection oktaUserIntrospection = GetOktaIntrospectUser(access_token);
                OktaUserInformation oktaUserInformation = GetOktaUserInfo(access_token);
                SUsers sUsers = new SUsers();
                String username = oktaUserIntrospection.username.Split('@').First();
                //username = "15116";
                //username = "1248518";
                //username = "eseleme";
                String UserNameIntrospectWithU = "U" + username;
                String MailUserInfo = oktaUserInformation.email;
                string MailUserInfoSinDominio = MailUserInfo.Split('@').First();
                var user = sUsers.GetUserByPeopleId(username);
                if (user == null)
                {
                    user = sUsers.GetUserByname(username, false);
                    if (user == null)
                    {
                        user = sUsers.GetUserByname(UserNameIntrospectWithU, false);
                        if (user == null)
                        {
                            user = sUsers.GetUserByMail(MailUserInfo, false);
                            if (user == null)
                            {
                                user = sUsers.GetUserByname(MailUserInfo, false);
                                if (user == null)
                                {
                                    user = sUsers.GetUserByname(MailUserInfoSinDominio, false);
                                    if (user == null)
                                    {
                                        ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "El usuario" + username + " no existe en zamba");
                                        return Unauthorized();
                                    }
                                }
                            }
                        }
                    }
                }
                if (user.State == UserState.Bloqueado)
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "El usuario" + username + " esta bloqueado");
                    return Unauthorized();
                }
                var l = new LoginVM
                {
                    UserName = user.Name,
                    Password = user.Password,
                    ComputerNameOrIp = ""
                };
                var tokenString = GetTokenString(l, user, access_token);
                var UrlRedirect = GetWebConfigElement("ThisDomain") + "/globalsearch/search/search.html?";
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se aprueba la autenticacion");
                var oktaRedirectLogout = (new OktaPublicInformation()).baseUrl + "/logout?id_token=" + id_token + @"&post_logout_redirect_uri= " + UrlRedirect;
                oktaRedirectLogout = oktaRedirectLogout
                    .Replace("/", "%2F")
                    .Replace(":", "%3A")
                    .Replace("?", "%3F")
                    .Replace("&", "%26")
                    ;
                var tokenInfo = new TokenInfo
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    userName = user.Name,
                    UserId = user.ID,
                    oktaUrlSignOut = "",                    
                    oktaIdToken = id_token,
                    oktaAccessToken = access_token,
                    oktaRedirectLogout = oktaRedirectLogout
                };
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se retorna la informacion para redireccionar");
                ResponseLoginOkta responseLoginOkta = new ResponseLoginOkta();
                responseLoginOkta.tokenInfo = tokenInfo;
                responseLoginOkta.UrlRedirect = UrlRedirect;
                var js = JsonConvert.SerializeObject(responseLoginOkta);
                UpdateLastUserAction(user.ID);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        private void UpdateLastUserAction(Int64 user)
        {
            UserBusiness ub = new UserBusiness();
            DateTime thisDay = DateTime.Today;
            ub.setValueLastUser(1, ObjectTypes.Users, RightsType.LogIn, thisDay.ToString("d"), Convert.ToInt32(user));
            //UserPreferences.setValue("LastUserAction", thisDay.ToString("d"), UPSections.UserPreferences);
        }

        public OktaResponseGetAccessToken GetOktaAccessToken(String code)
        {
            OktaResponseGetAccessToken ret = new OktaResponseGetAccessToken();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            OktaPrivateInformation OktaInformation = new OktaPrivateInformation();
            string LoginEncondedBase64 = Convert.ToBase64String((OktaInformation.clientId + ":" + OktaInformation.ClientSecret)
            .ToCharArray()
            .Select(c => (byte)c)
            .ToArray()
        );
            string HeaderAuthorization = OktaInformation.BasicAuthenticacion;
            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/json");
            client.Headers.Add("Content-type", "application/x-www-form-urlencoded");
            client.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            client.Headers.Add("Accept", "*/*");
            String redirectUri = OktaInformation.redirectURL;
            redirectUri = OktaInformation.redirectURL;
            redirectUri = redirectUri.Replace("/", "%2F")
                .Replace(":", "%3A")
                .Replace("?", "%3F")
                .Replace("&", "%26");
            var baseAddress = OktaInformation.baseUrl + "/oauth2/v1/token";
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.ContentType = "application/x-www-form-urlencoded";
            http.UserAgent = "postmanRuntime/7.24.1";
            http.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            http.Headers.Add("Cache-Control", "no-cache");
            http.KeepAlive = true;
            http.Method = "POST";
            var postData = "grant_type=authorization_code"
                            + "&code=" + code
                            + "&redirect_uri=" + redirectUri
                            + "&client_id=" + OktaInformation.clientId
                            + "&client_secret=" + OktaInformation.ClientSecret;
            var data = Encoding.ASCII.GetBytes(postData);
            http.ContentLength = data.Length;
            http.Accept = "*/*";
            CookieContainer cookieContainer = new CookieContainer();
            http.CookieContainer = cookieContainer;
            http.Referer = baseAddress;
            http.ServicePoint.Expect100Continue = false;
            using (var stream = http.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            String json;
            var resp = http.GetResponse();
            using (var s = resp.GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    json = sr.ReadToEnd();
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtiene token");
                    ret = JsonConvert.DeserializeObject<OktaResponseGetAccessToken>(json);
                }
            }
            return ret;
        }
        public class ResponseLoginOkta
        {
            public TokenInfo tokenInfo;
            public String UrlRedirect;
        }
        private OktaUserInformation GetOktaUserInfo(String access_token)
        {
            OktaUserInformation userInformation = new OktaUserInformation();
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                OktaPrivateInformation OktaInformation = new OktaPrivateInformation();
                string HeaderAuthorization = "Bearer " + access_token;
                WebClient client = new WebClient();
                client.Headers.Add("Authorization", HeaderAuthorization);
                var baseAddress = OktaInformation.baseUrl + "/oauth2/v1/userinfo";
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Headers.Add("authorization", HeaderAuthorization);
                http.ContentType = "application/json";
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Se usa access_token para buscar userinfo");
                using (var s = http.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        var json = sr.ReadToEnd();
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "informacion de usuario:" + json);
                        userInformation = JsonConvert.DeserializeObject<OktaUserInformation>(json);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userInformation;
        }
        public class OktaResponseGetRefreshAccessToken
        {
            public String token_type;
            public Int64 expires_in;
            public String refresh_token;
            public String scope;
            public String id_token;
        }
        public class OktaResponseGetAccessToken
        {
            public String token_type;
            public Int64 expires_in;
            public String access_token;
            public String scope;
            public String id_token;
        }
        public OktaUserIntrospection GetOktaIntrospectUser(String access_token)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            OktaUserIntrospection ret = new OktaUserIntrospection();
            OktaPrivateInformation OktaInformation = new OktaPrivateInformation();
            WebClient client = new WebClient();
            client.Headers.Add("Authorization", OktaInformation.BasicAuthenticacion);
            var baseAddress = OktaInformation.baseUrl + "/oauth2/v1/introspect";
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Method = "POST";
            http.Accept = "*/*";
            http.ContentType = "application/x-www-form-urlencoded";
            CookieContainer cookieContainer = new CookieContainer();
            http.Headers.Add("Authorization", OktaInformation.BasicAuthenticacion);
            http.CookieContainer = cookieContainer;
            http.Referer = baseAddress;
            http.ServicePoint.Expect100Continue = false;
            var postData = "token=" + access_token;
            var data = Encoding.ASCII.GetBytes(postData);
            http.ContentLength = data.Length;
            using (var stream = http.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            using (var s = http.GetResponse().GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    var json = sr.ReadToEnd();
                    ret = JsonConvert.DeserializeObject<OktaUserIntrospection>(json);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, json);
                }
            }
            return ret;
        }
        public OktaResponseGetRefreshAccessToken GetOktaRefreshToken(String access_token)
        {
            OktaResponseGetRefreshAccessToken ret = new OktaResponseGetRefreshAccessToken();
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            OktaPrivateInformation OktaInformation = new OktaPrivateInformation();
            string HeaderAuthorization = OktaInformation.BasicAuthenticacion;
            WebClient client = new WebClient();
            client.Headers.Add("accept", "application/json");
            client.Headers.Add("Content-type", "application/x-www-form-urlencoded");
            client.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            client.Headers.Add("Accept", "*/*");
            String redirectUri = OktaInformation.redirectURL;
            redirectUri = OktaInformation.redirectURL;
            redirectUri = redirectUri.Replace("/", "%2F")
                .Replace(":", "%3A")
                .Replace("?", "%3F")
                .Replace("&", "%26");
            var baseAddress = OktaInformation.baseUrl + "/oauth2/v1/token";
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.ContentType = "application/x-www-form-urlencoded";
            http.UserAgent = "postmanRuntime/7.24.1";
            http.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            http.Headers.Add("Cache-Control", "no-cache");
            http.KeepAlive = true;
            http.Method = "POST";
            var postData = "grant_type=refresh_token"
                            + "&redirect_uri=" + redirectUri
                            + "&scope=openid"
                            + "&refresh_token=" + access_token;
            var data = Encoding.ASCII.GetBytes(postData);
            http.ContentLength = data.Length;
            http.Accept = "*/*";
            CookieContainer cookieContainer = new CookieContainer();
            http.CookieContainer = cookieContainer;
            http.Referer = baseAddress;
            http.ServicePoint.Expect100Continue = false;
            using (var stream = http.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            String json;
            var resp = http.GetResponse();
            using (var s = resp.GetResponseStream())
            {
                using (var sr = new StreamReader(s))
                {
                    json = sr.ReadToEnd();
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "refresca el token");
                    ret = JsonConvert.DeserializeObject<OktaResponseGetRefreshAccessToken>(json);
                }
            }
            return ret;
        }

        public class StateOkta
        {
            public string state;
            public DateTime expiration;
        }
        public static List<StateOkta> ListStatesOkta = new List<StateOkta>();



    }

    class OktaUserInformation
    {
        public String sub;
        public string email;
        public Boolean email_verified;
    }
    public class OktaUserIntrospection
    {
        public Boolean active;
        public string aud;
        public string client_id;
        public Int64 exp;
        public Int64 iat;
        public string iss;
        public string jti;
        public string scope;
        public string sub;
        public string token_type;
        public string uid;
        public string username;
    }
}

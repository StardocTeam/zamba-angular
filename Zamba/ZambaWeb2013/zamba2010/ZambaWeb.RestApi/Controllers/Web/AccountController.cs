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

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Account")]
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
                    UserId = user.ID.ToString()
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

        private JObject GetTokenString(LoginVM l, IUser user)
        {
            try
            {

                //Obtengo token si todavia no caduco
                UserToken uToken = new UserToken();
                JObject tokenInfo = uToken.GetTokenIfIsValid(user);
                if (tokenInfo != null)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Token valio");
                    return (tokenInfo);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando Token");
                    tokenInfo = uToken.GenerateToken(user);
                    ZTrace.WriteLineIf(ZTrace.IsInfo, $"t: {tokenInfo.ToString()}");
                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                    l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1","127.0.0.1");
                    Ucm ucm = new Ucm();
                    Int32 timeOut = Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, 30, user.ID));
                    ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("TimeOut: {0}", timeOut.ToString()));
                    user.ConnectionId = Int32.Parse(ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());
                    ZTrace.WriteLineIf(ZTrace.IsInfo, string.Format("ConnectionId: {0}", user.ConnectionId.ToString()));
                    
                    Zss zss = new Zss()
                    {
                        UserId = user.ID,
                        ConnectionId = user.ConnectionId,
                        CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
                        TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
                        Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
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
                throw ex;
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
                throw ex;
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
                var IsValid = zss.ChekTokenInDatabase(UserId, Token);
                return (IsValid);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
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
                throw ex;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
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
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
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
                throw ex;
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
        public IHttpActionResult GetUserRights(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

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
        public IHttpActionResult NewGetUserRight(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params != null && paramRequest.Params.ContainsKey("RightType") && paramRequest.Params.ContainsKey("ObjectId"))
                {
                    ObjectTypes ObjectId = (ObjectTypes)int.Parse(paramRequest.Params["ObjectId"]);
                    RightsType RightType = (RightsType)int.Parse(paramRequest.Params["RightType"]);

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



    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zamba.Core;
using Zamba.Framework;
using ZambaWeb.RestApi.ViewModels;

namespace ZambaWeb.RestApi.Controllers.Dashboard.DB
{
    public class ZambaTokenDatabase
    {

        public UserInfo GetZambaToken(string username, string password)
        {
            IUser user = null;
            UserInfo userInfo = new UserInfo();
            try
            {
                LoginVM loginVM = new LoginVM()
                {
                    UserName = username,
                    Password = password,
                };
                UserBusiness UB = new UserBusiness();
                user = UB.GetUserByname(username, false);
                user = UB.ValidateLogIn(username, password ?? string.Empty, ClientType.Service);
                UB = null;

                var tokenString = GetTokenString(loginVM, user);

                userInfo = new UserInfo
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userID = EncriptString(user.ID.ToString())
                };

                return userInfo;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return userInfo;
            }

        }

        public class UserInfo
        {
            public string token { get; set; }
            public string tokenExpire { get; set; }
            public string connectionId { get; set; }
            public string userID { get; set; }

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

        private JObject GetTokenString(LoginVM l, IUser user)
        {
            //Obtengo token si todavia no caduco
            UserToken uToken = new UserToken();
            JObject tokenInfo = uToken.GetTokenIfIsValid(user);
            if (tokenInfo != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Token existente Valido");
                return (tokenInfo);
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Generando Token");
                tokenInfo = uToken.GenerateToken(user);
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");

                Ucm ucm = new Ucm();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "New Connection");

                Int32 timeOut = Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, 30, user.ID));
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("TimeOut: {0}", timeOut.ToString()));
                user.ConnectionId = Int32.Parse(ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());

                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Connection: {0}", user.ConnectionId.ToString()));
                Zss zss = new Zss()
                {
                    UserId = user.ID,
                    ConnectionId = user.ConnectionId,
                    CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
                    TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
                    Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
                };
                SaveZss(zss);
                HttpContext.Current.Session["TokenExpires"] = zss.TokenExpireDate;
                HttpContext.Current.Session["BearerToken"] = zss.Token;
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(user.ID, ClientType.Web);
                return (tokenInfo);
            }
        }
        private void SaveZss(Zss zss)
        {
            var zssFactory = new ZssFactory();
            zssFactory.SetZssValues(zss);
        }
    }
}
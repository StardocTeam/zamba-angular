using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zamba.Core;
using Zamba.Framework;

namespace Zamba.FUS
{
  public  class UserSecurity
    {

        public TokenInfo LoginAndgetToken(LoginVM l)
        {
            IUser user;

            UserBusiness UB = new UserBusiness();
            if (l.Password == null) l.Password = string.Empty;
            user = UB.ValidateLogIn(l.UserName, l.Password ?? string.Empty, ClientType.Service);
            UB = null;
            JObject tokenString = GetTokenString(l, user);
            var tI = new TokenInfo
            {
                token = tokenString,
                tokenExpire = TokenExpires(),
                userName = user.Name,
            };
            return tI;
        }


        private JObject GetTokenString(LoginVM l, IUser user)
        {
            //Obtengo token si todavia no caduco
            UserToken uToken = new UserToken();
            JObject tokenInfo = uToken.GetTokenIfIsValid(user);
            if (tokenInfo == null)
            //&& HttpContext.Current.Session["BearerToken"] != null)
            //{
            //    return (tokenInfo.SelectToken(@"access_token").Value<string>());
            //}
            //else
            {
                tokenInfo = uToken.GenerateToken(user);
                UserPreferences UP = new UserPreferences();
                Int16 timeOut = Int16.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, "180"));
                user.ConnectionId = Int32.Parse(Ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());
                Zss zss = new Zss()
                {
                    UserId = user.ID,
                    ConnectionId = user.ConnectionId,
                    CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
                    TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
                    Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
                };
                SaveZss(zss);
                //  HttpContext.Current.Session["TokenExpires"] = DateTime.Now.AddDays(1).ToString();
                //  HttpContext.Current.Session["BearerToken"] = zss.Token;
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(user.ID, ClientType.Web);
                return tokenInfo;
            }
        }

    }
}

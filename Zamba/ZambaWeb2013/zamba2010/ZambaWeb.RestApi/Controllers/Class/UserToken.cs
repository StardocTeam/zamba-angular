using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Zamba.Core;

namespace ZambaWeb.RestApi.Controllers
{
    public class UserToken
    {
        public UserToken()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        public  JObject GetTokenIfIsValid(Zamba.Core.IUser user)
        {
            ZTrace.WriteLineIf(ZTrace.IsWarning, "Verificando Token");
            ZssFactory zssF = new ZssFactory();
            JObject tokenInfo = zssF.GetTokenIfIsValid(user);
            zssF = null;
            return tokenInfo;
        }
        public  JObject GenerateToken(Zamba.Core.IUser user)
        {           
            return GenerateLocalAccessTokenResponse(user);
        }
        private  JObject GenerateLocalAccessTokenResponse(Zamba.Core.IUser user)
        {
            var userName = user.Name;
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
            var tokenExpiration = TimeSpan.FromMinutes(Int16.Parse(UP.getValue("TokenExpireMinutes", Zamba.UPSections.WFService, 1440, user.ID)));
            UP = null;

            ClaimsIdentity identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, userName));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var ticket = new AuthenticationTicket(identity, props);

            var accessToken = Startup.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);

            JObject tokenResponse = new JObject(
                                        new JProperty("userName", userName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty("issued", ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty("expiredate", ticket.Properties.ExpiresUtc.ToString())
        );

            return tokenResponse;
        }




    }
}
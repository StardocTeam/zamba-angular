using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Zamba.Core;
using Zamba;
using ZambaWeb.RestApi.Auth;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

namespace ZambaWeb.RestApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public SimpleAuthorizationServerProvider()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.WebRS");
            }
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserBusiness UB = new UserBusiness();

            IUser userZamba;
            try
            {

                userZamba = UB.ValidateLogIn(context.UserName, context.Password, ClientType.Service);
                #region EF para tratar de persistir usuario logueado en CodeBehind                
                //Usa EF


                var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
                ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                    CookieAuthenticationDefaults.AuthenticationType);
                //AuthenticationProperties properties = CreateProperties(user.UserName);
                //AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
                //context.Validated(ticket);
                //context.Request.Context.Authentication.SignIn(cookiesIdentity);
                #endregion
            }
            catch
            {
                userZamba = null;
            }
            finally
            {
                UB = null;
            }
            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    IdentityUser user = await _repo.FindUser();
            if (userZamba == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
            //}
            //var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim("role", "user"));
            //context.Validated(identity);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("sub", context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(identity);
        }


        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
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
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Core.Searchs;
using Nelibur.ObjectMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;
using System.Security.Principal;
using Newtonsoft.Json.Linq;

namespace ZambaWeb.RestApi.Controllers
{
    public class TokenHelper
    {
        public static Zamba.Core.IUser GetUser(IIdentity userIdentity)
        {
            UserBusiness UB = new UserBusiness();
            var userName = userIdentity.Name;
            if (userName.Length > 0)
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "userIdentity.Name: " + userName);
                UserBusiness UserBusiness = new UserBusiness();
                Zamba.Core.IUser user = userName != string.Empty ? UserBusiness.GetUserByname(userName, true) : null;
                var zssFactory = new ZssFactory();
                var tokenString = zssFactory.GetTokenIfIsValid(user);
                if (tokenString == null) return null;
                var tI = new TokenInfo
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userName = user.Name,
                    userid = user.ID
                };
                user.ConnectionId = int.Parse(tI.connectionId);
                return UB.ValidateLogIn(user, ClientType.WebApi);
            }
            else
            {
                return null;
            }
        }
    }
}
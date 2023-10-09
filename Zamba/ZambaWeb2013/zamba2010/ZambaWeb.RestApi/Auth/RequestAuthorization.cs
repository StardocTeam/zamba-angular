using System.Web.Http.Controllers;
using System.Text;
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
using System.IO;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace ZambaWeb.RestApi.Controllers
{
    public class RestAPIAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!Authorize(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.Response == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            actionContext.Response.Content = new StringContent("");
            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            Boolean UsuarioAutorizado = false;
            try
            {
                HttpRequestMessage request = actionContext.Request;

                if (request.Headers.Authorization == null)
                    return false;

                if (request.Headers.Authorization.Scheme == "Bearer")
                {
                    string Url = request.RequestUri.AbsoluteUri;
                    string Authorization = request.Headers.Authorization.Parameter;
                    List<String> SplitAuthorization =
                        ASCIIEncoding.ASCII.GetString(
                        Convert.FromBase64String(Authorization))
                        .Split(':')
                        .ToList<String>();
                    int user_id = Convert.ToInt32(SplitAuthorization.First());
                    IUser user;
                    UserBusiness userBusiness = new UserBusiness();
                    user = userBusiness.GetUserById(user_id);
                    userBusiness.ValidateLogIn(user.ID, ClientType.WebApi);
                    string token = SplitAuthorization.Last();
                    Zamba.Core.ZssFactory zssFactory = new Zamba.Core.ZssFactory();
                    UsuarioAutorizado = zssFactory.CheckTokenInDatabase(user_id, token, false);
                    if (!UsuarioAutorizado)
                        ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Usuario intento usar un recurso sin autorizacion." + System.Environment.NewLine + "url:" + Url + System.Environment.NewLine + "user:" + user_id.ToString() + System.Environment.NewLine + "token:" + token);
                }
            }
            catch (Exception e)
            {
                return UsuarioAutorizado = false;
            }
            return UsuarioAutorizado;
        }
    }

    public class globalControlRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        //public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OnActionExecuting:");

            if (string.IsNullOrEmpty(actionContext.Request.Headers.Host))            
                ZTrace.WriteLineIf(ZTrace.IsVerbose, actionContext.Request.Headers.Host);            
            else            
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Host: NO HAY HOST");


            if (actionContext.Request.Headers.Contains("Origin"))            
                ZTrace.WriteLineIf(ZTrace.IsVerbose, actionContext.Request.Headers.GetValues("Origin").ToString());            
            else            
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Origin: NO EXISTE en los HEADERS");    
            

            if (!ValidateRequest(actionContext))
            {
                actionContext.Response = new HttpResponseMessage();
                actionContext.Response.Content = new StringContent("");
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Bad request enOnActionExecuting");
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "OnActionExecuted:");

            //AddHeaderCSP(ref actionExecutedContext);
            HttpResponseMessage r = actionExecutedContext.Response;
            r.Headers.Remove("Server");
            actionExecutedContext.Response = r;
            base.OnActionExecuted(actionExecutedContext);
        }

        public void AddHeaderCSP(ref HttpActionExecutedContext actionExecutedContext)
        {
            string HeaderCSP = System.Web.Configuration.WebConfigurationManager.AppSettings["CSPNotUnsafeInline"].ToString();

            StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("default-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            stringBuilder.Append("default-src 'self';");
            //stringBuilder.Append("base-uri 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("frame-src blob: data:  'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;  ");
            //stringBuilder.Append("frame-ancestors 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("script-src 'self';");
            //stringBuilder.Append("style-src 'self';");
            stringBuilder.Append("img-src 'self' blob: data: http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("connect-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;   ");
            //stringBuilder.Append("font-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("object-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");

            //stringBuilder.Append("default-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("base-uri 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("frame-src blob: data:  'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;  ");
            //stringBuilder.Append("frame-ancestors 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("script-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("style-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi; ");
            //stringBuilder.Append("img-src 'self' blob: data: http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
            //stringBuilder.Append("connect-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;   ");
            //stringBuilder.Append("font-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi; ");
            //stringBuilder.Append("object-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");

            actionExecutedContext.Response.Headers.Add("Content-Security-Policy", HeaderCSP);
            //actionExecutedContext.Response.Headers.Add("Content-Security-Policy", stringBuilder.ToString());
        }

        public Boolean ValidateRequest(HttpActionContext actionContext)
        {
            Boolean isValid = true;
            if (!ValidateOrigin(actionContext))
                isValid = false;

            return isValid;
        }

        private Boolean ValidateOrigin(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            string strOrigin = "";

            if (request.Headers.Contains("Origin"))
                strOrigin = request.Headers.GetValues("Origin").FirstOrDefault();
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, strOrigin);
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, request.RequestUri.Scheme + "://" + request.RequestUri.Authority);
            if (string.IsNullOrEmpty(strOrigin))
                return true;
            else if (strOrigin == request.RequestUri.Scheme + "://" + request.RequestUri.Authority)
                return true;
            else
            {
                return false;
            }

        }
    }

    public class isGenericRequestAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!Authorize(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.Response == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            actionContext.Response.Content = new StringContent("");
            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            return ValidateGenericRequest(actionContext.Request);
        }


        private bool ValidateGenericRequest(HttpRequestMessage request)
        {
            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;

            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;


            if (jsonString == "")
                return false;

            // Navegar por el documento JSON utilizando JToken
            JToken token = JToken.Parse(jsonString);

            try
            {
                foreach (JProperty item in token)
                {
                    if (item.Name.ToLower() != "params" && item.Name.ToLower() != "userid")
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
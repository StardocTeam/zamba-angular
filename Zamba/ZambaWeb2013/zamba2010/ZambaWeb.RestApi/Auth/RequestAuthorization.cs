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
    public class RestAPIAuthorizeAttribute : AuthorizeAttribute    {
        
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

    public class globalControlRequestFilter: ActionFilterAttribute
    {
        public Boolean isGenericRequest { get; set; } = false;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
            if (!ValidateRequest(actionContext))
            {
                actionContext.Response = new HttpResponseMessage();
                actionContext.Response.Content = new StringContent("");
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
            }            
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            AddHeaderCSP(ref actionExecutedContext);
        }

        public void AddHeaderCSP(ref HttpActionExecutedContext actionExecutedContext)
        {
            string HeaderCSP = System.Web.Configuration.WebConfigurationManager.AppSettings["CSPNotUnsafeInline"].ToString();
            actionExecutedContext.Response.Headers.Add("Content-Security-Policy", HeaderCSP);
        }

        public Boolean ValidateRequest(HttpActionContext actionContext)
        {
            Boolean isValid = true;
            if (!ValidateOrigin(actionContext))
                isValid = false;
            if (isGenericRequest)
            {
                if (!ValidateGenericRequest(actionContext.Request))
                {
                    isValid = false;
                }
            }
            return isValid;
        }


        private Boolean ValidateOrigin(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            string strOrigin = "";

            if (request.Headers.Contains("Origin"))
                strOrigin = request.Headers.GetValues("Origin").FirstOrDefault();

            if (string.IsNullOrEmpty(strOrigin))
                return true;
            else if (strOrigin == request.RequestUri.Scheme + "://" + request.RequestUri.Authority)
                return true;
            else
            {
                return false;
            }

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
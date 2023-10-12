using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using Zamba.Core;
using System.IO;
using Zamba.Membership;
using System.Net;
using System.Web.Http.Results;
using System.Text;
using System.Configuration;
using System.Net.Http;
using Microsoft.Owin;
using System.Web.Configuration;
//using Zamba.PreLoad;

namespace Zamba.Web
{
    public class Global : HttpApplication
    {
        //public override void Init()//Para acceder a session en MVC
        //{
        //    this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
        //    base.Init();
        //}

        //void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)//Para acceder a session en MVC
        //{
        //    System.Web.HttpContext.Current.SetSessionStateBehavior(
        //        SessionStateBehavior.Required);
        //}
        protected void Application_PostAuthorizeRequest()
        {
            // System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            //this.DefinirRutas();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ZCore ZC = new ZCore();

            if (Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.Web - Application");

            ZC.VerifyFileServer();
            // checkActions = new System.Timers.Timer(60000);
            // checkActions.Elapsed += CheckInactiveSessionsHandler;
            //  checkActions.Enabled = true;
            //if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
            //{
            //    ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
            //}

        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority);
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization");

            var errorMessage = HttpContext.Current.Items["ErrorMessage"] != null ? HttpContext.Current.Items["ErrorMessage"].ToString() : String.Empty;
            if (errorMessage.Contains("Redirect"))
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Propiedad 'errorMessage': " + errorMessage.ToString());
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.StatusDescription = "Not Found";
                HttpContext.Current.Response.End();
            }
        }

        private Boolean ValidateUrl()
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ZambaWeb - GlobalAsax.cs: Comienza validacion de URL...");


            var scheme = System.Web.Configuration.WebConfigurationManager.AppSettings["Scheme"];

            if (scheme == null)
            {
                scheme = "http";
            }
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad 'scheme': " + scheme.ToString());

            if (Request.UrlReferrer != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Validando propiedad 'Request.UrlReferrer.Host': " + Request.UrlReferrer.Host.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad 'Request.Url.Host': " + Request.Url.Host.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "if (Request.UrlReferrer.Host != Request.Url.Host)");

                if (Request.UrlReferrer.Host != Request.Url.Host)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: FALSE");
                    return false;
                }
            }

            scheme = scheme.ToLower();
            var RequestScheme = Request.Url.Scheme.ToLower();

            if (Request.Url.Scheme.ToLower() != scheme)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad 'RequestScheme': " + RequestScheme.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: FALSE");
                return false;
            }

            if (Request.UrlReferrer != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad 'Request.UrlReferrer.Scheme': " + Request.UrlReferrer.Scheme.ToString());

                if (Request.UrlReferrer.Scheme.ToLower() != scheme)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: FALSE");
                    return false;
                }
            }


            if (!(Request.Headers["Host"] == Request.Url.Host + ":" + Request.Url.Port.ToString() || Request.Headers["Host"] == Request.Url.Host))
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad 'Request.Headers[\"Host\"]': " + Request.Headers["Host"].ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "'Request.Url.Host + \":\" + Request.Url.Port.ToString()': " + Request.Url.Host + ":" + Request.Url.Port.ToString());
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Resultado: FALSE");
                return false;
            }

            string strOrigin = "";

            if (HttpContext.Current.Request.Headers["Origin"] != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad: 'HttpContext.Current.Request.Headers[\"Origin\"]': " + HttpContext.Current.Request.Headers["Origin"].ToString());
                strOrigin = HttpContext.Current.Request.Headers.GetValues("Origin").FirstOrDefault();
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Propiedad: 'strOrigin': " + strOrigin.ToString());
            }

            if (string.IsNullOrEmpty(strOrigin))
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "'HttpContext.Current.Request.Url.Scheme + HttpContext.Current.Request.Url.Authority': " + HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority.ToString());
                return true;
            }
            else if (strOrigin == HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //string ApplicationPath = HttpContext.Current.Request.ApplicationPath; // El identificador de usuario que deseas almacenar
            //var cookie = new HttpCookie("Cookie", ApplicationPath);
            //cookie.SameSite = SameSiteMode.None; // Opciones: None, Lax, Strict
            //cookie.Secure = true; // Asegúrate de que Secure esté configurado si utilizas HTTPS
            //Response.Cookies.Add(cookie);

            // var request = ((System.Web.HttpApplication)sender).Request.Params;
            if (!ValidateUrl())
            {
                Response.StatusCode = 401;
                return;
            }
            var request = HttpContext.Current.Request;


            if (HttpContext.Current.Request.HttpMethod == "GET" && Request.Url.Segments.Length == 2)
            {
                //    Server.Transfer("views/security/Login.aspx");
                Response.Redirect(request.Url.LocalPath + "views/security/Login.aspx");
            }
            //string param1 = request.QueryString["userId"];

            if (request.Form["userId"] != null)
            {
                try
                {
                    Int32.Parse(request.Form["userId"]);
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    HttpContext.Current.Response.End();
                }
            }

            if (request.Form["userLocalStorage"] != null)
            {
                try
                {
                    Int32.Parse(request.Form["userLocalStorage"]);
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    HttpContext.Current.Response.End();
                }
            }
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }


        //System.Timers.Timer checkActions;
        //public  string ZambaVersion = "2.9.3.0";

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            try
            {
                if (Zamba.Servers.Server.ConInitialized == true)
                {
                    Ucm ucm = new Ucm();
                    ucm.CheckInactiveSessions();
                    ucm = null;
                    ActionsBusiness.CleanExceptions();
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                Exception ex = Server.GetLastError();

                var ExMEssage = "";
                var InnerException = "";

                if (ex != null)
                {
                    ExMEssage = ex.Message == null ? "" : ex.Message.ToString();
                    ZTrace.WriteLineIf(ZTrace.IsError, "Application_Error: " + ExMEssage);

                    InnerException = ex.InnerException.Message == null ? "" : ex.InnerException.Message.ToString();
                    ZTrace.WriteLineIf(ZTrace.IsError, "Application_Error: " + InnerException);

                    String BaseURLZambaWeb = Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + System.Web.Configuration.WebConfigurationManager.AppSettings["ThisDomain"].ToString();

                    Zamba.AppBlock.ZException.Log(ex);

                    HttpContext.Current.Response.Redirect(BaseURLZambaWeb + "/views/CustomErrorPages/Error.html?e=" + ex.Message);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Application_Error: 'Server.GetLastError()': devuelve nulo, por lo tanto no hay objeto Exception que revisar.");
                }

                Server.ClearError();
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Items["ErrorMessage"] = "Redirect - 404 Not Found";
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.StatusDescription = "Not Found";

                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started 

            //if (EnablePreload && Session != null && Zamba.Membership.MembershipHelper.CurrentUser  != null)
            //{
            //   EnablePreload = false;
            //       PreLoadObjects(((Int64)Zamba.Membership.MembershipHelper.CurrentUser.ID));
            // }

        }

        protected void Application_EndRequest()
        {

            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");



            if (Response.StatusCode == 404 || Response.StatusCode == 403)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.StatusCode = 404;
                HttpContext.Current.Response.StatusDescription = "Not Found";
                Response.Redirect("~/Views/Security/views/CustomErrorPages/404.aspx");
                HttpContext.Current.Response.End();

            }
            if (Response.StatusCode == 500)
            {
                String myhtml = "\"Message\": \"An error has occurred.";
                byte[] misbytes = Encoding.ASCII.GetBytes(myhtml);
                HttpContext.Current.Response.Cookies.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.BinaryWrite(misbytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            try
            {
                if (Zamba.Servers.Server.ConInitialized == true)
                {
                    Ucm ucm = new Ucm();
                    ucm.CheckInactiveSessions();
                    ucm = null;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
            //CheckInactiveSessions();
            //  ActionsBusiness.CleanExceptions();
        }

        private void CheckInactiveSessionsHandler(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == true)
                {
                    Ucm ucm = new Ucm();
                    ucm.CheckInactiveSessions();
                    ucm = null;
                }

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }




    }
}
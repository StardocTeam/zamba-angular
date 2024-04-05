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
using Microsoft.AspNet.FriendlyUrls;
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
        public Boolean ValidateOktaState(String state, String Domain)
        //public Boolean ValidateOktaState(String state, string Domain)
        {
            if (Domain.StartsWith("https"))
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            }
            try
            {
                WebClient client = new WebClient();
                string url = Domain + System.Web.Configuration.WebConfigurationManager.AppSettings["RestApiUrl"] + "/api";
                var baseAddress = url + "/Account/validateOktaStateValue?state=" + state;
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Method = "POST";
                http.Accept = "*/*";
                http.ContentType = "application/x-www-form-urlencoded";
                //CookieContainer cookieContainer = new CookieContainer();
                http.Referer = baseAddress;
                var postData = "";
                var data = Encoding.ASCII.GetBytes(postData);
                http.ContentLength = data.Length;
                try
                {
                    using (var stream = http.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                        stream.Close();
                    }
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Ocurrio un error en GetRequestStream(); " + ex.Message.ToString());
                    throw ex;
                }
                try
                {
                    using (var s = http.GetResponse().GetResponseStream())
                    {
                        using (var sr = new StreamReader(s))
                        {
                            var json = sr.ReadToEnd();
                            if (json == "true")
                                return true;
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "error en GetResponseStream, fallo el metodo validateOktaStateValue");
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "error en global.asax, fallo el metodo ValidateOktaState" + ex.Message);
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
            if(Request.AppRelativeCurrentExecutionFilePath == "~/Views/Security/OktaAuthentication.html")
            {
                string NewUrl = Request.Url.AbsoluteUri.Replace("Views/Security/", "Views/Security/Okta/");
                Response.Redirect(NewUrl);
                ;
                //Server.Transfer ("/Views/Security/Okta/OktaAuthentication.html");
            }
            ;
            if (!ValidateUrl())
            {
                Response.StatusCode = 401;
                return;
            }
            var request = HttpContext.Current.Request;

            #region TODO: MultipleSession

            //Session multiple inicio
            Boolean multipleSession = false;
            Boolean LoginWithOkta = false;
            Boolean initSession = true;
            Boolean showModal = false;
            Boolean Logout = false;

            string code = Request.QueryString["code"];
            string state = Request.QueryString["state"];

            if (!String.IsNullOrEmpty(code))
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, "ValidateOktaAuthenticacionHTML - ejecuta ValidateOktaState porque CODE tiene un valor");
                if (!ValidateOktaState(state, Request.Url.Scheme + "://" + Request.Url.Authority + "/"))
                {
                    Response.StatusCode = 401;
                    return;
                }
            }



            string authMethod = "";

            if (!String.IsNullOrEmpty(Request.QueryString["logout"]))
            {
                Logout = Boolean.Parse((Request.QueryString["logout"].ToString()));
            }
            if(!String.IsNullOrEmpty(Request.QueryString["showModal"]))
            {
                showModal = Boolean.Parse((Request.QueryString["showModal"].ToString()));
            }

            if (System.Web.Configuration.WebConfigurationManager.AppSettings["AllowMultipleAuthentication"] != null)
            {
                multipleSession = Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["AllowMultipleAuthentication"].ToString());
            }

            if (System.Web.Configuration.WebConfigurationManager.AppSettings["LoadOktaUser"] != null)
            {
                LoginWithOkta = Boolean.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["LoadOktaUser"].ToString());
            }

            if (!String.IsNullOrEmpty(Request.QueryString["initSession"]))
            {
                initSession = Boolean.Parse((Request.QueryString["initSession"].ToString()));
            }

            if (!String.IsNullOrEmpty(Request.QueryString["AuthMethod"]))
            {
                authMethod = Request.QueryString["AuthMethod"].ToString();
            }
            if (Logout || showModal)
            {
                initSession = false;
            }
            string urlPage = request.Url.Segments.Last().ToLower();

            if (urlPage == "login.aspx" ||
                urlPage == "login" ||
                urlPage == "oktaauthentication.html" ||
                urlPage == "~/")
            {
                if (showModal)
                {
                    return;
                }
                else if (initSession)
                {
                    if (multipleSession)
                    {
                        if (urlPage != "oktaauthentication.html")
                        {
                            Response.Redirect("~/Views/Security/Okta/OktaAuthentication.html?initSession=false");
                        }
                    }
                    else
                    // Solo autentiacion zamba
                    {
                        Response.Redirect("~/Views/Security/Login.aspx?initSession=false");
                    }                
                }
                else
                {
                    if (multipleSession)
                    {

                    }
                    else
                    {
                        if(urlPage == "oktaauthentication.html")
                        {
                            Response.Redirect("~/Views/Security/Login.aspx?initSession=false");
                        }
                    }
                }
            }




            //if (urlPage == "login.aspx" ||
            //    urlPage == "login" ||
            //    urlPage == "oktaauthentication.html" ||
            //    urlPage == "~/")
            //{
            //    string queryString = "initSession=false";

            //    System.Collections.Specialized.NameValueCollection queryParameters = HttpContext.Current.Request.QueryString;
            //    // Verificamos si hay parámetros en la consulta
            //    if (queryParameters.HasKeys())
            //    {
            //        // Recorremos los parámetros de la consulta
            //        foreach (string key in queryParameters.AllKeys)
            //        {
            //            string value = queryParameters[key];

            //            if (key != "AuthMethod")
            //            {
            //                queryString += "&" + key + "=" + value;
            //            }
            //        }
            //    }/*
            //        multipleSession:    Habilita session multiple entre Okta y Zamba. 
            //        LoginWithOkta:      Habilita la Vista de Okta.
            //        initSession:        Es true cuando ingresa a la web.
            //        authMethod:         Parametro de QueryString Que indica el metodo de autenticacion.
            //        urlPage:            La web solicitada.       
            //      */



            //    //if (!String.IsNullOrEmpty(Request.QueryString["AuthMethod"]))
            //    //{
            //    //    string authMethod = Request.QueryString["AuthMethod"].ToString();

            //    //    if (authMethod.ToLower() == "zamba")
            //    //    {
            //    //        Response.Redirect("~/Views/Security/Login.aspx?" + queryString);
            //    //    }
            //    //    else if (authMethod.ToLower() == "okta")
            //    //    {
            //    //        Response.Redirect("~/Views/Security/Okta/OktaAuthentication.html?" + queryString);
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    if (!multipleSession && !initSession)
            //    //    {
            //    //        Response.Redirect("~/Views/Security/Login.aspx?" + queryString);
            //    //    }

            //    //    if (multipleSession && LoginWithOkta)
            //    //    {
            //    //        if (true)
            //    //        {

            //    //        }


            //    //        if (urlPage == "login.aspx" ||
            //    //            urlPage == "login" ||
            //    //            urlPage == "~/")
            //    //        {
            //    //            Response.Redirect("~/Views/Security/Okta/OktaAuthentication.html?" + queryString);
            //    //        }
            //    //    }
            //    //}
            //}



            ////Session multiple fin

            #endregion

            if (HttpContext.Current.Request.HttpMethod == "GET" && Request.Url.Segments.Length == 2)
            {
                //    Server.Transfer("views/security/Login.aspx");
                Response.Redirect(request.Url.LocalPath.TrimEnd('/') + "/views/security/Login.aspx");
                //Response.Redirect(request.Url.LocalPath.TrimEnd('/') + "/views/security/okta/OktaAuthentication.html");
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

                    //HttpContext.Current.Response.Redirect(BaseURLZambaWeb + "/views/CustomErrorPages/Error.html?e=" + ex.Message);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Application_Error: 'Server.GetLastError()': devuelve nulo, por lo tanto no hay objeto Exception que revisar.");
                }

                Server.ClearError();
                //HttpContext.Current.Response.Clear();
                //HttpContext.Current.Items["ErrorMessage"] = "Redirect - 404 Not Found";
                //HttpContext.Current.Response.StatusCode = 404;
                //HttpContext.Current.Response.StatusDescription = "Not Found";

                //HttpContext.Current.Response.End();
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
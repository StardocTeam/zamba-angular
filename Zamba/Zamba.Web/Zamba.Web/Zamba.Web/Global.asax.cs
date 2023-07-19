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
using Microsoft.AspNet.FriendlyUrls.Resolvers;
using System.Text;
using BundleTransformer.Core.Resources;
using Spire.Pdf.Exporting.XPS.Schema;
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

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (Request.Url.Scheme == "https")
            {
                Response.Headers.Add("Strict-Transport-Security", "max-age-31536000");
            }                
            if(Request.Url.Segments.Last().ToString() =="404" || 
                Request.Url.Segments.Last().ToString() == "404.aspx" ||
                Request.Url.Segments.Last().ToString() == "getValueFromWebConfig"


                )
            {
                //Limpia las coockies para resolver la vulnerabilidad 'AntiForgeryToken' (CSRF) de cookies inseguras
                Response.Cookies.Clear();
            }
                    
                


            HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
            HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
            HttpContext.Current.Response.Headers.Remove("Server");
            //HttpContext.Current.Request.Headers.Add("Content-Security-Policy", "default-src 'self';");
            //HttpContext.Current.Response.Headers.Add("X-XSS-Protection", "1");
            HttpContext.Current.Response.Headers.Remove("X-Powered-By");
            HttpContext.Current.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            //HttpContext.Current.Response.Headers.Remove("X-Frame-Options");
            //HttpContext.Current.Response.AddHeader("X-Frame-Options", "SAMEORIGIN");
            //Response.Headers.Add("AntiForgeryToken", "abc123cba321");

            //if (GetListResourcesForAntiForgerytoken().Contains(Request.Url.Segments.Last().ToLower()))
            //{
            //    string Root = Request.PhysicalPath;
            //    string BodyHtml = "";
            //    if (!File.Exists(Root))
            //        if (File.Exists(Root + ".aspx"))
            //            Root = Root + ".aspx";


            //    using (StreamReader sr = new StreamReader(Root))
            //    {
            //        BodyHtml = sr.ReadToEnd();
            //        BodyHtml = BodyHtml.Replace("#AntiForgeryToken", getRandomAntiForgeryToken());

            //        byte[] BodyBytes = Encoding.ASCII.GetBytes(BodyHtml);
            //        HttpContext.Current.Response.ClearContent();
            //        HttpContext.Current.Response.BinaryWrite(BodyBytes);
            //        HttpContext.Current.Response.Flush();
            //        HttpContext.Current.Response.End();
            //    }
            //}

            //if (Request.Url.Segments.Last() == "getValueFromWebConfig")
            //{
            //    //if (Request.Params["__RequestVerificationToken"] != null)
            //    //{                    
            //    //    string token = Request.Params["__RequestVerificationToken"].ToString();

            //    //    if (!ValidateAntiForgeryToken(token))
            //    //    {
            //    //        Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    Response.StatusCode = (int)HttpStatusCode.BadRequest;                    
            //    //}
            //}

            //String myhtml = "<html><body>hola mundo</body><html>";
            //byte[] misbytes = Encoding.ASCII.GetBytes(myhtml);
            //HttpContext.Current.Response.Clear();
            //System.IO.StreamReader sr = new StreamReader(HttpContext.Current.Response.OutputStream);
            //string output = sr.ReadToEnd();


            //HttpContext.Current.Response.BinaryWrite(misbytes);



        }
        protected void Application_EndRequest()
        {
            if (Response.StatusCode == 404 || Response.StatusCode == 403)
            {
                Response.Redirect("~/Views/Security/views/CustomErrorPages/404.aspx");
            }
            //String myhtml = "<html><body>hola mundo</body><html>";
            //byte[] misbytes = Encoding.ASCII.GetBytes(myhtml);
            //HttpContext.Current.Response.BinaryWrite(misbytes);
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Código que se ejecuta al iniciar la aplicación
            //this.DefinirRutas();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MvcHandler.DisableMvcResponseHeader = true;            
            Application["AntiForgeryTokens"] = generateAntiForgeryTokens();

            ZCore ZC = new ZCore();

            if (Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.Web - Application");

            ZC.VerifyFileServer();
            Application["okta_states"] = new List<string>();

            // checkActions = new System.Timers.Timer(60000);
            // checkActions.Elapsed += CheckInactiveSessionsHandler;
            //  checkActions.Enabled = true;
            //if (ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12) == false)
            //{
            //    ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
            //}

        }




        protected void Application_BeginRequest(object sender, EventArgs e)
        {
 
            
            if (Request.Params.Count > 0)
            {
                if (Request.Params.AllKeys[0] == "zamba:\\\\DT")
                {
                    String URL = Request.Url.OriginalString;
                    URL = URL.Replace("zamba:%5C%5C", "");
                    Response.Redirect(URL);
                }
            }
            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                //Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl);
            }
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (!RequestAuthorization(Request))
            {
                //Context.Response.StatusCode = 401; // Unauthorized

                FormsAuthentication.RedirectToLoginPage();
                //Context.Response.End();

                //throw new HttpNotFoundException();
            }
            var app = sender as HttpApplication;
            if (app != null && app.Context != null)
            {
                app.Context.Response.Headers.Remove("Server");
            }


            Response.AppendHeader("Cache-Control", "no-cache,no-store,must-revalidate");
            Response.AppendHeader("Pragma", "no-cache");
            Response.AppendHeader("Expires", "0");




            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Methods");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
        private string[] GetResourcesSenitive()
        {
            List<String> RecursosSensibles = new List<string>();
            RecursosSensibles.Add("services");
            RecursosSensibles.Add("config");
            RecursosSensibles.Add("test");
            RecursosSensibles.Add("src");
            RecursosSensibles.Add("package.json");
            RecursosSensibles.Add("bower.json");
            RecursosSensibles.Add("gulpfile.js");
            return RecursosSensibles.ToArray();
        }
        private string[] GetListAuthorizationResources()
        {
            //Agregue aquellos recursos que desea autorizar donde la autenticacion
            List<String> WebResourcesAndMethods = new List<string>();
            WebResourcesAndMethods.Add("taskviewer");
            WebResourcesAndMethods.Add("docviewer");
            WebResourcesAndMethods.Add("taskviewer.aspx");
            WebResourcesAndMethods.Add("docviewer.aspx");
            WebResourcesAndMethods.Add("search.html");
            WebResourcesAndMethods.Add("GetDocFile.ashx");
            WebResourcesAndMethods.Add("TaskService.asmx/getUsedFilters");
            WebResourcesAndMethods.Add("TaskService.asmx/CloseAllAsignedTask");
            WebResourcesAndMethods.Add("TaskService.asmx/GetUserFeeds");
            WebResourcesAndMethods.Add("TaskService.asmx/SetNewRuleExecution");
            WebResourcesAndMethods.Add("TaskService.asmx/filterdata");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearAllCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearUserCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearRightsCache");
            WebResourcesAndMethods.Add("TaskService.asmx/KeepSessionAlive");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearRulesCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearStructureCache");
            WebResourcesAndMethods.Add("TaskService.asmx/CheckTimeOut");
            WebResourcesAndMethods.Add("TaskService.asmx/deleteFilter");
            return WebResourcesAndMethods.Select(n => n.ToLower()).ToArray();
        }

        private string[] GetListResourcesForAntiForgerytoken()
        {
            //Agregue aquellos recursos que desea autorizar donde la autenticacion
            List<String> WebResourcesAndMethods = new List<string>();
            WebResourcesAndMethods.Add("taskviewer");
            WebResourcesAndMethods.Add("docviewer");
            WebResourcesAndMethods.Add("taskviewer.aspx");
            WebResourcesAndMethods.Add("docviewer.aspx");
            WebResourcesAndMethods.Add("search.html");
            WebResourcesAndMethods.Add("GetDocFile.ashx");
            WebResourcesAndMethods.Add("OktaAuthentication.html");
            WebResourcesAndMethods.Add("Login.aspx");
            WebResourcesAndMethods.Add("HomePage.aspx");
            WebResourcesAndMethods.Add("HomePage");
            WebResourcesAndMethods.Add("404.aspx");
            WebResourcesAndMethods.Add("404");
            WebResourcesAndMethods.Add("TaskService.asmx/getUsedFilters");
            WebResourcesAndMethods.Add("TaskService.asmx/CloseAllAsignedTask");
            WebResourcesAndMethods.Add("TaskService.asmx/GetUserFeeds");
            WebResourcesAndMethods.Add("TaskService.asmx/SetNewRuleExecution");
            WebResourcesAndMethods.Add("TaskService.asmx/filterdata");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearAllCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearUserCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearRightsCache");
            WebResourcesAndMethods.Add("TaskService.asmx/KeepSessionAlive");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearRulesCache");
            WebResourcesAndMethods.Add("TaskService.asmx/ClearStructureCache");
            WebResourcesAndMethods.Add("TaskService.asmx/CheckTimeOut");
            WebResourcesAndMethods.Add("TaskService.asmx/deleteFilter");
            return WebResourcesAndMethods.Select(n => n.ToLower()).ToArray();
        }

        private Boolean RequestAuthorization(HttpRequest Request)
        {
            //return true;
            //if (Request.UrlReferrer != null)
            //{
            //    if (Request.UrlReferrer.Host != Request.Url.Host)
            //        throw new HttpException(401, "Unauthorized");
            //}
            //if (!ValidateOktaAuthenticacionHTML(Request))
            //    throw new HttpException(401, "Unauthorized");


            string PhysicalPath = Request.PhysicalPath.ToLower();


            string[] SubscriptionResourcesSensitive = GetResourcesSenitive();
            string[] SubscriptionResourcesAuthentication = GetListAuthorizationResources();
            Boolean AutenticationIsValid = true;
            try
            {
                int userid = 0;
                string token = "";
                List<String> WebMethods = new List<string>();
                List<String> Resources = new List<string>();
                WebMethods = SubscriptionResourcesAuthentication.Where(n => n.Contains("/")).Select(m => m.ToLower()).ToList();
                Resources = SubscriptionResourcesAuthentication.Where(n => !(n.Contains("/"))).Select(m => m.ToLower()).ToList();
                String Url = Request.AppRelativeCurrentExecutionFilePath;

                List<String> SplitUrl = Url.Split('/').ToList();
                String PossibleWebMethod = "";
                String PossibleResource = "";
                if (SplitUrl.Count > 1)
                {
                    PossibleWebMethod = (SplitUrl.ElementAt(SplitUrl.Count - 2) + "/" + SplitUrl.ElementAt(SplitUrl.Count - 1)).ToLower();
                }
                PossibleResource = SplitUrl.ElementAt(SplitUrl.Count - 1).ToLower();
                if (SubscriptionResourcesSensitive.Contains(PossibleResource))
                    return false;

                if (PossibleResource == "login.aspx")
                {

                    bool OktaAuthentication;
                    bool.TryParse(System.Web.Configuration.WebConfigurationManager.AppSettings["LoadOktaUser"], out OktaAuthentication);
                    if (OktaAuthentication)
                    {
                        if (!String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                        {
                            string QueryStringUrl = Request.QueryString["ReturnUrl"].Split('?').First();
                            string QueryStringParams = Request.QueryString["ReturnUrl"].Split('?').Last();
                            QueryStringParams = String.Join("&", QueryStringParams.Split('&').Where(n => n.Split('=').First() != "token" && n.Split('=').First() != "userid").AsEnumerable());
                            QueryStringParams = (QueryStringUrl + "?" + QueryStringParams)
                                .Replace("/", "%2F")
                                .Replace(":", "%3A")
                                .Replace("?", "%3F")
                                .Replace("=", "%3D")
                                .Replace("&", "%26");

                            Response.Redirect("~/Views/Security/OktaAuthentication.html?ReturnUrl=" + QueryStringParams);
                        }
                        else
                        {
                            Response.Redirect("~/Views/Security/OktaAuthentication.html");
                        }
                    }
                    // Response.Redirect("~/Views/Security/Okta2.html");


                }
                if (SubscriptionResourcesAuthentication.Contains(PossibleResource) || SubscriptionResourcesAuthentication.Contains(PossibleWebMethod))
                {
                    if (Request.Headers.GetValues("Authorization") != null)
                    {
                        List<string> Authorization = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Request.Headers.GetValues("Authorization").First().Split(' ').Last())).Split(':').ToList<String>();
                        userid = Convert.ToInt32(Authorization.First());
                        token = Authorization.Last();
                    }
                    else
                    {
                        var useridstring = "";

                        if (Request.QueryString["userid"] == null)
                            useridstring = "0";
                        else
                            useridstring = Request.QueryString["userid"].ToString().Split(',')[0];
                        userid = Convert.ToInt32(useridstring);
                        token = Request.QueryString["token"];
                    }
                    ZssFactory zssFactory = new ZssFactory();
                    AutenticationIsValid = zssFactory.CheckTokenInDatabase(userid, token, true);
                }
                if (AutenticationIsValid)
                {
                    if (Membership.MembershipHelper.CurrentUser == null)
                    {
                        Zamba.Services.SUsers sUsers = new Zamba.Services.SUsers();
                        Zamba.Membership.MembershipHelper.SetCurrentUser(sUsers.GetUser(userid));
                    }
                }
                else
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Usuario intento usar un recurso sin autorizacion." + System.Environment.NewLine + "url:" + Url + System.Environment.NewLine + "user:" + userid + System.Environment.NewLine + "token:" + token);
                }
                return AutenticationIsValid;
            }
            catch (Exception e)
            {
                //Request.AppRelativeCurrentExecutionFilePath + " (" + e.Message + ")";
                AutenticationIsValid = false;
            }

            return AutenticationIsValid;
        }

        private string generateAntiForgeryTokens()
        {
            string tokens = "";
            for (int i = 0; i <= 200; i++)
            {
                Guid newGuid = Guid.NewGuid();
                tokens += newGuid.ToString() + ";";
            }
            return tokens.TrimEnd(';');
        }

        private string getRandomAntiForgeryToken()
        {
            string token = "";
            var random = new Random();
            var indexRandom = random.Next(0, 200);
            string[] arrTokens = Application["AntiForgeryTokens"].ToString().Split(';').ToArray();
            token = arrTokens[indexRandom];
            return token;
        }

        private Boolean ValidateAntiForgeryToken(string token)
        {
            string[] arrTokens = Application["AntiForgeryTokens"].ToString().Split(';').ToArray();
            return arrTokens.Contains(token);
        }

        public Boolean ValidateOktaState(String state, String Domain)
        //public Boolean ValidateOktaState(String state, string Domain)
        {
            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                WebClient client = new WebClient();

                string url = Domain + System.Web.Configuration.WebConfigurationManager.AppSettings["RestApiUrl"] + "/api";
                var baseAddress = url + "/Account/validateOktaStateValue?state=" + state;
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Method = "POST";
                http.Accept = "*/*";
                http.ContentType = "application/x-www-form-urlencoded";
                CookieContainer cookieContainer = new CookieContainer();
                http.Referer = baseAddress;
                var postData = "";
                var data = Encoding.ASCII.GetBytes(postData);
                http.ContentLength = data.Length;
                using (var stream = http.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                using (var s = http.GetResponse().GetResponseStream())
                {
                    using (var sr = new StreamReader(s))
                    {
                        var json = sr.ReadToEnd();
                        ZTrace.WriteLineIf(ZTrace.IsInfo, json);
                        if (json == "true")
                            return true;
                        else
                            return false;
                    }
                }
            }
            catch (Exception)
            {

                return false;
            }


        }
        private Boolean ValidateOktaAuthenticacionHTML(HttpRequest Request)
        {

            var url = Request.AppRelativeCurrentExecutionFilePath.ToString().Split('/').ToList().Last();
            if (url == "OktaAuthentication.html")
            {
                string code = Request.QueryString["code"];
                string state = Request.QueryString["state"];
                string retururl = Request.QueryString["returnurl"];
                string logout = Request.QueryString["logout"];
                if (Request.QueryString.Count == 0)
                {
                    return true; // sin parametros
                }
                if (!String.IsNullOrEmpty(logout) && Request.QueryString.Count != 1)
                {
                    return false; //logout y otros parametros
                }
                if (!String.IsNullOrEmpty(logout) && Request.QueryString.Count == 1)
                {
                    if (logout == "true")
                        return true;
                    else
                        return false;
                    // solo logout
                }
                if (!String.IsNullOrEmpty(code) && String.IsNullOrEmpty(state) && Request.QueryString.Count != 2)
                {
                    return false; // code + state + otros parametros
                };
                return ValidateOktaState(state, Request.Url.Scheme + "://" + Request.Url.Authority + "/");

            }
            // valido state
            return true;
        }


        //System.Timers.Timer checkActions;
        //public  string ZambaVersion = "2.9.3.0";

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
            // CheckInactiveSessions();
        }

        void Application_Error(object sender, EventArgs e)
        {
            //Response.StatusCode = 401;
            //return;
            try
            {
                Exception ex = Server.GetLastError();
                Server.ClearError();
                HttpContext.Current.Response.Clear();
                //                ((System.CodeDom.Compiler.CompilerError)(new System.Linq.SystemCore_EnumerableDebugView(((System.Web.HttpCompileException)((System.Web.HttpApplication)sender).LastError).ResultsWithoutDemand.Errors).Items[0])).errorText

                //HttpContext.Current.Response.Redirect(".//views/CustomErrorPages/Error.html?e=" + ex.Message);
            }
            catch (Exception)
            {
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

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
            //CheckInactiveSessions();
            //  ActionsBusiness.CleanExceptions();
        }

        private void CheckInactiveSessionsHandler(Object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == true)
                {

                }


                //  CheckInactiveSessions();

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }




    }
}
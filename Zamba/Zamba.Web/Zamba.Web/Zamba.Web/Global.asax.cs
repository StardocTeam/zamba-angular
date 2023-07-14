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




        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Params["userId"] != null)
            {
                try
                {
                    Int32.Parse(Request.Params["userId"]);
                }
                catch (Exception)
                {
                    Response.StatusCode = 400;
                    HttpContext.Current.Response.End();
                }
            }

            if (Request.Params["userLocalStorage"] != null)
            {
                try
                {
                    Int32.Parse(Request.Params["userLocalStorage"]);
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
                Server.ClearError();
                String BaseURLZambaWeb = Request.Url.Scheme + "://" + Request.Url.OriginalString.Split('/')[2] + System.Web.Configuration.WebConfigurationManager.AppSettings["ThisDomain"].ToString();

                HttpContext.Current.Response.Clear();
                //                ((System.CodeDom.Compiler.CompilerError)(new System.Linq.SystemCore_EnumerableDebugView(((System.Web.HttpCompileException)((System.Web.HttpApplication)sender).LastError).ResultsWithoutDemand.Errors).Items[0])).errorText

                Zamba.AppBlock.ZException.Log(ex);
                HttpContext.Current.Response.Redirect( BaseURLZambaWeb + "/views/CustomErrorPages/Error.html?e=" + ex.Message);
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

        protected void Application_EndRequest() {

            if (Response.StatusCode == 404 || Response.StatusCode == 403)
            {
                Response.Redirect("~/Views/Security/views/CustomErrorPages/404.aspx");
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
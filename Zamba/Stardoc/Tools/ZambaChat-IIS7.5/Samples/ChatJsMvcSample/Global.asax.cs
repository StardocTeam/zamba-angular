using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ChatJsMvcSample.App_Start;
using ChatJsMvcSample.Code.SignalR;
using System.Data.Entity;
using ChatJsMvcSample.Models;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin;

using System.Configuration;
using System;
using System.Diagnostics;
using Microsoft.Owin.Cors;
using System.Web;
//using Microsoft.AspNet.SignalR.Core;

namespace ChatJsMvcSample
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            #region Si hay error al compilar
            // si hay error de owin library
            //Update-Package Owin -Reinstall 
            // Install-Package Microsoft.Owin
            //Install-Package Microsoft.Owin.Host.SystemWeb
            #endregion
            //ya esta en startup.cs
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //colocar solo esta linea de abajo
            // RouteTable.Routes.MapHubs(new HubConfiguration() { EnableCrossDomain = true });
          

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<ChatEntities>(null);// reinicia db
                                                        // ChatEntities db = new ChatEntities();
           GlobalConfiguration.Configuration.EnsureInitialized();
            ChatHub ChatHub = new ChatHub();
            ChatHub.LoadUsersFromDB();
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }

    }      
}


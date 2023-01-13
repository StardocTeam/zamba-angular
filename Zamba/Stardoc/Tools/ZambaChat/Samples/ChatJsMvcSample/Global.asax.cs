using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ChatJsMvcSample.App_Start;
using ChatJsMvcSample.Code.SignalR;
using System.Data.Entity;
using ChatJsMvcSample.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

using System.Configuration;
using System;
using System.Diagnostics;

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
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //colocar solo esta linea de abajo
            RouteTable.Routes.MapHubs(new HubConfiguration() { EnableCrossDomain = true });
            // RouteTable.Routes.MapHubs();
            //if (double.Parse(ConfigurationSettings.AppSettings["iis"].ToString()) < 7)
            //    RouteConfig.RegisterRoutesIIS6OrLess(RouteTable.Routes);
            //else
              RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer<ChatEntities>(null);// reinicia db
            // ChatEntities db = new ChatEntities();
               ChatHub.LoadUsersFromDB();
        }
    }
}
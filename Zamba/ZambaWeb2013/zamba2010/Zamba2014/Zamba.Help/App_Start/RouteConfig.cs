using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Zamba.Help
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                      name: "DefaultNew",
                      url: "lista",
                      defaults: new { controller = "home", action = "List" }
                  );
            routes.MapRoute(
                          name: "DefaultId",
                          url: "{id}",
                          defaults: new { controller = "Viewer", action = "FullContent", id = "" }
                      );

            routes.MapRoute(
                "Default",
                 "{controller}/{action}/{id}",
                new { controller = "Viewer", action = "FullContent", id = UrlParameter.Optional }               
            );
        
        }
    }
}

using System.Web.Mvc;
using System.Web.Routing;

namespace ChatJsMvcSample.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
        //public static void RegisterRoutesIIS6OrLess(RouteCollection routes)
        //{
        //    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        //    //IIS6
        //    routes.MapRoute(
        //        "Default",
        //        "{controller}.aspx/{action}/{id}",
        //        new { action = "Index", id = "" }
        //      );
        //    routes.MapRoute(
        //           "Root",
        //           "",
        //   new { controller = "Home", action = "Index", id = "" }
        // );
        //}
    }
}
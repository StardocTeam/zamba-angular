using System.Web.Http;
using System.Web.Http.Cors;
//using Microsoft.AspNet.Cors;

namespace ChatJsMvcSample.App_Start
{
    public static class WebApiConfig
    {
        //public static void Register(HttpConfiguration config)
        //{
        //    config.Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}

        public static void Register(HttpConfiguration config)
        {

            //EnableCrossSiteRequests(config);
            AddRoutes(config);
        }

        private static void AddRoutes(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
             routeTemplate: "api/{controller}/{id}",
             defaults: new { id = RouteParameter.Optional }
            );
        }

        private static void EnableCrossSiteRequests(HttpConfiguration config)
        {
            //var cors = new EnableCorsAttribute(
            //    origins: "*",
            //    headers: "*",
            //    methods: "*");

            //config.EnableCors(cors);
        }
    }
}

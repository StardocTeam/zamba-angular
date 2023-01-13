using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace ZambaWeb.RestApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
           // var cors = new EnableCorsAttribute("*", "*", "*");
           // config.EnableCors(cors);

            //Para habilitar session
            var httpControllerRouteHandler = typeof(HttpControllerRouteHandler).GetField("_instance",
      System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (httpControllerRouteHandler != null)
            {
                httpControllerRouteHandler.SetValue(null,
                    new Lazy<HttpControllerRouteHandler>(() => new SessionHttpControllerRouteHandler(), true));
            }

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //agregado
            config.Routes.MapHttpRoute(
            name: "DefaultApiWithAction",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { action = "GET" }
        );
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
        }
    }
    public class SessionControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public SessionControllerHandler(RouteData routeData)
            : base(routeData)
        { }
    }

    public class SessionHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new SessionControllerHandler(requestContext.RouteData);
        }
    }
}

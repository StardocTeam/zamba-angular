using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Zamba.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
           
            // Web API configuration and services
          //  var cors = new EnableCorsAttribute("*", "*", "*");
         //   config.EnableCors(cors);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}

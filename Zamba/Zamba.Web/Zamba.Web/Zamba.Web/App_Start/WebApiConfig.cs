using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;

using Zamba.Core;
using System.Reflection;

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
            config.MessageHandlers.Add(new NotFoundHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }

        public class NotFoundHandler : DelegatingHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(
                        HttpRequestMessage request,
                        CancellationToken cancellationToken)
            {
                var response = await base.SendAsync(request, cancellationToken);

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "* Respuesta capturada en 'SendAsync' (WebApiconfig):");
                ZTrace.WriteLineIf(ZTrace.IsInfo, response.ToString());

                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.InternalServerError ||
                    response.StatusCode == HttpStatusCode.Unauthorized ||
                    response.StatusCode == HttpStatusCode.BadRequest)
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "Datelles de la respuesta: ");
                    System.Net.Http.ObjectContent<System.Web.Http.HttpError> ResponseContent = (System.Net.Http.ObjectContent<System.Web.Http.HttpError>)response.Content;
                    HttpError httpError = (HttpError)ResponseContent.Value;

                    foreach (KeyValuePair<string, object> item in httpError)
                    {

                        ZTrace.WriteLineIf(ZTrace.IsError, item.Key + ": " + item.Value);
                    }

                    response = request.CreateResponse(HttpStatusCode.NotFound,
                        new { Error = "" },
                        "application/json");
                    response.Content = new StringContent("");
                }

                return response;
            }
        }
    }
}

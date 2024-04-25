using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;

using Zamba.Core;
using System.Reflection;

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

            config.MessageHandlers.Add(new NotFoundHandler());

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
    public class NotFoundHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "* Respuesta capturada en 'SendAsync' (WebApiconfig):");
            ZTrace.WriteLineIf(ZTrace.IsInfo, response.ToString());

            //Codigo para devolver 401 al Zamba Dashboard RRHH - para desloguear
            if (response.Content != null)
            {
                string content = await response.Content.ReadAsStringAsync();
                if (content.Contains("ZambaRRHH")) {
                    return response;
                }
            }

            if (response.StatusCode == HttpStatusCode.NotFound ||
                response.StatusCode == HttpStatusCode.InternalServerError ||
                response.StatusCode == HttpStatusCode.Unauthorized ||
                response.StatusCode == HttpStatusCode.BadRequest)
            {

                ZTrace.WriteLineIf(ZTrace.IsError, "error en WebApiConfig, se rechazo el request con error nro" + response.StatusCode.ToString() + ".");
                ZTrace.WriteLineIf(ZTrace.IsError, "Datelles de la respuesta: ");

                //try
                //{
                //    System.Net.Http.ObjectContent<System.Web.Http.HttpError> ResponseContent = (System.Net.Http.ObjectContent<System.Web.Http.HttpError>)response.Content;
                //    HttpError httpError = (HttpError)ResponseContent.Value;

                //    foreach (KeyValuePair<string, object> item in httpError)
                //    {
                //        ZTrace.WriteLineIf(ZTrace.IsError, item.Key + ": " + item.Value);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    ZTrace.WriteLineIf(ZTrace.IsError, "Error: "+ ex.Message);
                //}

                response = request.CreateResponse(HttpStatusCode.NotFound,
                    new { Error = "" },
                    "application/json");
                response.Content = new StringContent("");
            }

            return response;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Zamba.Framework;
using System.Web.Http.Filters;
using ZambaWeb.RestApi.Controllers.Class;
using System.Reflection;
using System.Web;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Zamba.Core;
using Newtonsoft.Json.Linq;
using ZambaWeb.RestApi.Controllers;

namespace ZambaWeb.RestApi.AuthorizationRequest
{

    public class RestAPIAuthorizeAttribute : AuthorizeAttribute
    {
        public bool isNewsPostDto { get; set; }
        public bool isEmailData { get; set; }
        public bool isSearchDto { get; set; }
        public bool SelectedEntitiesIds { get; set; }

        
        private bool Authorize(HttpActionContext actionContext)
        {
            Boolean UsuarioAutorizado = false;
            try
            {
                HttpRequestMessage request = actionContext.Request;
                if (request.Headers.Authorization == null)
                    return false;
                if (request.Headers.Authorization.Scheme == "Bearer")
                {
                    string Url = request.RequestUri.AbsoluteUri;
                    string Authorization = request.Headers.Authorization.Parameter;
                    List<String> SplitAuthorization =
                        ASCIIEncoding.ASCII.GetString(
                        Convert.FromBase64String(Authorization))
                        .Split(':')
                        .ToList<String>();
                    int user_id = Convert.ToInt32(SplitAuthorization.First());
                    IUser user;
                    UserBusiness userBusiness = new UserBusiness();
                    user = userBusiness.GetUserById(user_id);
                    userBusiness.ValidateLogIn(user.ID, ClientType.WebApi);
                    string token = SplitAuthorization.Last();
                    Zamba.Core.ZssFactory zssFactory = new Zamba.Core.ZssFactory();
                    UsuarioAutorizado = zssFactory.CheckTokenInDatabase(user_id, token, false);
                    if (!UsuarioAutorizado)
                        ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Usuario intento usar un recurso sin autorizacion." + System.Environment.NewLine + "url:" + Url + System.Environment.NewLine + "user:" + user_id.ToString() + System.Environment.NewLine + "token:" + token);
                }
            }
            catch (Exception e)
            {
                return UsuarioAutorizado = false;
            }
            return UsuarioAutorizado;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (isNewsPostDto)
            {
                if (!ValidateNewsPostDto(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            if (isEmailData)
            {
                if (!ValidateMailData(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            if (isSearchDto)
            {
                if (!ValidateSearchDto(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            if (SelectedEntitiesIds)
            {
                if (!ValidateSelectedEntitiesIds(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            //ESTO ES DE OKTA
            //if (!Authorize(actionContext))
            //{
            //    HandleUnauthorizedRequest(actionContext);
            //}
        }
        private bool ValidateNewsPostDto(HttpRequestMessage request)
        {

            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;

            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;

            var properties = System.Text.Json.JsonDocument.Parse(jsonString);


            // Deserializar la cadena JSON en un objeto C#
            try
            {
                foreach (var item in properties.RootElement.EnumerateObject())
                {
                    if (item.Name.ToLower() != "gridtype" && item.Name.ToLower() != "userid" && item.Name.ToLower() != "searchtype")
                        return false;

                    if (item.Name.ToLower() == "userid")
                    {
                        try
                        {
                            Int32.Parse(item.Value.ToString());
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public class Params
        {
            public long doctypeId { get; set; }
            public string filterType { get; set; }
        }

        public class RootObject
        {
            public int UserId { get; set; }
            public Params Params { get; set; }
        }

        private bool ValidateMailData(HttpRequestMessage request)
        {
            List<string> emailDataProperties = new List<string>();
            Type emailDataType = typeof(EmailData);
            PropertyInfo[] properties = emailDataType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                emailDataProperties.Add(property.Name.ToLower());
            }

            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;

            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;

            var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString);

            try
            {

                foreach (var property in jsonDocument.RootElement.EnumerateObject())
                {
                    if (!emailDataProperties.Contains(property.Name.ToLower()))
                        return false;
                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool ValidateSelectedEntitiesIds(HttpRequestMessage request)
        {

            HttpContent httpContent = request.Content;
            string jsonString = httpContent.ReadAsStringAsync().Result;

            try
            {
                return validateInt64Values(jsonString);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool validateInt64Values(string inputString)
        {
            try
            {
                List<long> int64List = JsonConvert.DeserializeObject<List<long>>(inputString);

                foreach (long number in int64List)
                {
                    if (number.GetType() != typeof(long))
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private bool ValidateSearchDto(HttpRequestMessage request)
        {
            List<string> searchDTODataProperties = new List<string>();
            Type searchDTODataType = typeof(SearchDto);
            PropertyInfo[] properties = searchDTODataType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                searchDTODataProperties.Add(property.Name.ToLower());
            }

            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;

            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;

            var jsonDocument = System.Text.Json.JsonDocument.Parse(jsonString);

            try
            {

                foreach (var property in jsonDocument.RootElement.EnumerateObject())
                {
                    if (!searchDTODataProperties.Contains(property.Name.ToLower()))
                    {
                        //Se ponen aca las property para evitar ponerlas en el SearchDto
                        if (property.Name != "GroupsIds" && property.Name != "SearchResults" &&
                            property.Name != "lastFiltersByView" && property.Name != "lastSearchEntitiesNodes" &&
                            property.Name != "CreatedTodayCount" && property.Name != "stateID" &&
                            property.Name != "OpenTaskOnOneResult" && property.Name != "HasResults" &&
                            property.Name != "SearchResultsObject" && property.Name != "UsedZambafilters" &&
                            property.Name != "currentMode" && property.Name != "ExpirationDate" && 
                            property.Name != "lastNodeSelected" && property.Name != "filter"
                            )
                        {
                            return false;
                        }

                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.Response == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            actionContext.Response.Content = new StringContent("");
            base.HandleUnauthorizedRequest(actionContext);
        }
        private bool AuthorizeRequest(HttpRequestMessage request)
        {
            return true;
        }
        //private bool Authorize(HttpActionContext actionContext)
        //{
        //    Boolean UsuarioAutorizado = false;
        //    try
        //    {
        //        HttpRequestMessage request = actionContext.Request;
        //        if (request.Headers.Authorization.Scheme == "Bearer")
        //        {
        //            string Url = request.RequestUri.AbsoluteUri;
        //            string Authorization = request.Headers.Authorization.Parameter;
        //            List<String> SplitAuthorization =
        //                ASCIIEncoding.ASCII.GetString(
        //                Convert.FromBase64String(Authorization))
        //                .Split(':')
        //                .ToList<String>();
        //            int user_id = Convert.ToInt32(SplitAuthorization.First());
        //            IUser user;
        //            UserBusiness userBusiness = new UserBusiness();
        //            user = userBusiness.GetUserById(user_id);
        //            userBusiness.ValidateLogIn(user.ID, ClientType.WebApi);
        //            string token = SplitAuthorization.Last();
        //            Zamba.Core.ZssFactory zssFactory = new Zamba.Core.ZssFactory();
        //            UsuarioAutorizado = zssFactory.CheckTokenInDatabase(user_id, token, false);
        //            if (!UsuarioAutorizado)
        //                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Usuario intento usar un recurso sin autorizacion." + System.Environment.NewLine + "url:" + Url + System.Environment.NewLine + "user:" + user_id.ToString() + System.Environment.NewLine + "token:" + token);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return UsuarioAutorizado = false;
        //    }
        //    return UsuarioAutorizado;
        //}


    }


    public class RequestResponseControllerAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!ValidateUrl())
            {
                actionContext.Response = new HttpResponseMessage();
                actionContext.Response.Content = new StringContent("");
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
                return;
            }
            // Lógica a ejecutar antes de que la acción del controlador se ejecute
            // Puedes acceder al contexto de la acción (actionContext) para obtener información sobre la solicitud actual, etc.
        }

        private Boolean ValidateUrl()
        {
            var scheme = System.Web.Configuration.WebConfigurationManager.AppSettings["Scheme"];
            if (scheme == null)
            {
                scheme = "http";
            }
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if (HttpContext.Current.Request.UrlReferrer.Host != HttpContext.Current.Request.Url.Host)
                    return false;
            }
            scheme = scheme.ToLower();
            var RequestScheme = HttpContext.Current.Request.Url.Scheme.ToLower();
            if (HttpContext.Current.Request.Url.Scheme.ToLower() != scheme)
            {
                return false;
            }
            if (HttpContext.Current.Request.UrlReferrer != null)
            {
                if (HttpContext.Current.Request.UrlReferrer.Scheme.ToLower() != scheme)
                {
                    return false;
                }
            }
            if (!(HttpContext.Current.Request.Headers["Host"] == HttpContext.Current.Request.Url.Host + ":" + HttpContext.Current.Request.Url.Port.ToString() || HttpContext.Current.Request.Headers["Host"] == HttpContext.Current.Request.Url.Host))
            {
                return false;
            }

            string strOrigin = "";
            if (HttpContext.Current.Request.Headers["Origin"] != null)
                strOrigin = HttpContext.Current.Request.Headers.GetValues("Origin").FirstOrDefault();
            if (string.IsNullOrEmpty(strOrigin))
                return true;
            else if (strOrigin == HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority)
                return true;
            else
            {
                return false;
            }


        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            if (actionExecutedContext.Response.StatusCode == HttpStatusCode.InternalServerError)
            {
                HttpResponseMessage response = actionExecutedContext.Response;

                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                response.Content = new StringContent("");

                actionExecutedContext.Response = response;

                base.OnActionExecuted(actionExecutedContext);
            }
        }
    }

    public class Validator
    {
        public static Boolean FindInvalidChars(string value)
        {
            List<char> invalidChars = new List<char>();
            char[] charList = { '<', '>', ';', '\'', '\"', '\\', '+' };
            invalidChars.AddRange(charList);
            return value.ToCharArray().Any(c => invalidChars.Contains(c));
        }
    }

    public class globalControlRequestFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        //public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!ValidateRequest(actionContext))
            {
                actionContext.Response = new HttpResponseMessage();
                actionContext.Response.Content = new StringContent("");
                actionContext.Response.StatusCode = HttpStatusCode.BadRequest;
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Bad request enOnActionExecuting");
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            AddHeaderCSP(ref actionExecutedContext);
            HttpResponseMessage r = actionExecutedContext.Response;
            r.Headers.Remove("Server");
            actionExecutedContext.Response = r;
            base.OnActionExecuted(actionExecutedContext);
        }
        public void AddHeaderCSP(ref HttpActionExecutedContext actionExecutedContext)
        {
            if (System.Web.Configuration.WebConfigurationManager.AppSettings["CSPNotUnsafeInline"] != null) {
                string HeaderCSP = System.Web.Configuration.WebConfigurationManager.AppSettings["CSPNotUnsafeInline"].ToString();
                StringBuilder stringBuilder = new StringBuilder();
                //stringBuilder.Append("default-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                stringBuilder.Append("default-src 'self';");
                //stringBuilder.Append("base-uri 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("frame-src blob: data:  'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;  ");
                //stringBuilder.Append("frame-ancestors 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("script-src 'self';");
                //stringBuilder.Append("style-src 'self';");
                stringBuilder.Append("img-src 'self' blob: data: http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("connect-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;   ");
                //stringBuilder.Append("font-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("object-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("default-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("base-uri 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("frame-src blob: data:  'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;  ");
                //stringBuilder.Append("frame-ancestors 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("script-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("style-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi; ");
                //stringBuilder.Append("img-src 'self' blob: data: http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                //stringBuilder.Append("connect-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;   ");
                //stringBuilder.Append("font-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi; ");
                //stringBuilder.Append("object-src 'self' http://localhost:44301/Zamba.Web http://localhost:44301/ZambaWeb.RestApi;");
                actionExecutedContext.Response.Headers.Add("Content-Security-Policy", HeaderCSP);
                //actionExecutedContext.Response.Headers.Add("Content-Security-Policy", stringBuilder.ToString());
            }

        }
        public Boolean ValidateRequest(HttpActionContext actionContext)
        {
            Boolean isValid = true;
            if (!ValidateOrigin(actionContext))
                isValid = false;
            return isValid;
        }
        private Boolean ValidateOrigin(HttpActionContext actionContext)
        {
            HttpRequestMessage request = actionContext.Request;
            string strOrigin = "";
            if (request.Headers.Contains("Origin"))
                strOrigin = request.Headers.GetValues("Origin").FirstOrDefault();
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, strOrigin);
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, request.RequestUri.Scheme + "://" + request.RequestUri.Authority);
            if (string.IsNullOrEmpty(strOrigin))
                return true;
            else if (strOrigin == request.RequestUri.Scheme + "://" + request.RequestUri.Authority)
                return true;
            else
            {
                return false;
            }
        }
    }

    public class isGenericRequestAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!Authorize(actionContext))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.Response == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            actionContext.Response.Content = new StringContent("");
            base.HandleUnauthorizedRequest(actionContext);
        }
        private bool Authorize(HttpActionContext actionContext)
        {
            return ValidateGenericRequest(actionContext.Request);
        }
        private bool ValidateGenericRequest(HttpRequestMessage request)
        {
            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;
            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;
            if (jsonString == "")
                return false;
            // Navegar por el documento JSON utilizando JToken
            JToken token = JToken.Parse(jsonString);
            try
            {
                foreach (JProperty item in token)
                {
                    if (item.Name.ToLower() != "params" && item.Name.ToLower() != "userid")
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

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

namespace ZambaWeb.RestApi.AuthorizationRequest
{

    public class RestAPIAuthorizeAttribute : AuthorizeAttribute
    {
        public bool isGenericRequest { get; set; }
        public bool isNewsPostDto { get; set; }
        public bool isEmailData { get; set; }
        public bool isSearchDto { get; set; }


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (isGenericRequest)
            {
                if (!ValidateGenericRequest(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
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
            if (isSearchDto) {
                if (!ValidateSearchDto(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            return;
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

                    if (item.Name.ToLower() == "userid") {
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
        private bool ValidateGenericRequest(HttpRequestMessage request)
        {

            // Obtener el contenido de la respuesta HTTP
            HttpContent httpContent = request.Content;

            // Leer el contenido como una cadena JSON
            string jsonString = httpContent.ReadAsStringAsync().Result;

            var properties = System.Text.Json.JsonDocument.Parse(jsonString);


            // Deserializar la cadena JSON en un objeto C#
            try
            {
                //var jsonObjectAnalize = JsonConvert.DeserializeObject<object>(jsonString);
                //foreach (var item in properties.RootElement.EnumerateObject())
                //{
                //    if (item.Name.ToLower() != "params" && item.Name.ToLower() != "userid")
                //        return false;
                //}

                //var jsonObject = JsonConvert.DeserializeObject<genericRequest>(jsonString);

                foreach (var item in properties.RootElement.EnumerateObject())
                {
                    if (item.Name.ToLower() != "params" && item.Name.ToLower() != "userid")
                        return false;
                }


                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool ValidateMailData(HttpRequestMessage request)
        {
            List<string> emailDataProperties = new List<string>();
            Type emailDataType = typeof(EmailData);
            PropertyInfo[] properties = emailDataType.GetProperties();

            foreach (PropertyInfo property in properties) {
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
                        return false;
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
            // Lógica a ejecutar antes de que la acción del controlador se ejecute
            // Puedes acceder al contexto de la acción (actionContext) para obtener información sobre la solicitud actual, etc.
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response == null) { 
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            if (actionExecutedContext.Response.StatusCode == HttpStatusCode.InternalServerError) {
                HttpResponseMessage response = actionExecutedContext.Response;

                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

                response.Content = new StringContent("");

                actionExecutedContext.Response = response;

                base.OnActionExecuted(actionExecutedContext);
            }
        }
    }
}

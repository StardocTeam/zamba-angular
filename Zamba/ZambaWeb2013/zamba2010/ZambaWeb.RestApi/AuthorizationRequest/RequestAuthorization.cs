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

namespace ZambaWeb.RestApi.AuthorizationRequest
{
    public class RestAPIAuthorizeAttribute : AuthorizeAttribute
    {
        public bool isGenericRequest { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (isGenericRequest)
            {
                if (!ValidateGenericRequest(actionContext.Request))
                {
                    HandleUnauthorizedRequest(actionContext);
                }

            }
            return;
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
                var jsonObject = JsonConvert.DeserializeObject<genericRequest>(jsonString);

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
}

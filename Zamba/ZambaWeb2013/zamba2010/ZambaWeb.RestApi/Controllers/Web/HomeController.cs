using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba.Core;
using Zamba.Servers;
using Zamba.Framework;
using System.Text;
using System.Web.Http.Controllers;
using System.Threading.Tasks;
using System.Text.Json;
using ZambaWeb.RestApi.AuthorizationRequest;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Home")]
    [RequestResponseController]
    public class HomeController : ApiController
    {
        public HomeController()
        {
            if (Server.ConInitialized == false)
            {
                ZCore ZC = new ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        private Zamba.Core.IUser GetUser(long? userId)
        {
            try
            {

                var user = TokenHelper.GetUser(User.Identity);

                UserBusiness UBR = new UserBusiness();

                if (userId.HasValue && userId > 0 && user == null)
                {
                    user = UBR.ValidateLogIn(userId.Value, ClientType.WebApi);
                }

                if (user == null && Request != null && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0)
                {
                    Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                    if (UserId > 0)
                    {
                        user = UBR.ValidateLogIn(UserId, ClientType.WebApi);
                    }
                }

                if (user == null)
                {
                    string fullUrl = Request.Headers.GetValues("Referer").FirstOrDefault();
                    string[] urlInPieces = fullUrl.Split('&')[0].Split('/');
                    string dataItem = null;
                    foreach (string item in urlInPieces)
                    {
                        if (item.Contains("User"))
                        {
                            dataItem = item;
                        }
                    }


                    string urlPart = dataItem != null ? dataItem.Split('&')[0].Split('=')[1] : "0";

                    if (user == null && Request != null && Int64.Parse(urlPart) > 0) // && Request.Headers.Count() > 0 && Request.Headers.Contains("UserId") && Request.Headers.GetValues("UserId").Count() > 0
                    {
                        //Int64 UserId = Int64.Parse(Request.Headers.GetValues("UserId").FirstOrDefault());
                        Int64 UserIdFromUrl = Int64.Parse(urlPart);
                        if (UserIdFromUrl > 0)
                        {
                            user = UBR.ValidateLogIn(UserIdFromUrl, ClientType.WebApi);
                        }
                    }
                }


                UBR = null;

                return user;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        [HttpGet, HttpPost]
        [Route("GetHomeTabs")]
        [isGenericRequest]
        public IHttpActionResult GetHomeTabs(genericRequest paramRequest)
        {
            try
            {
                IUser user = null;
                var tabs = new List<string>();
                if (paramRequest != null)
                {
                    user = GetUser(paramRequest.UserId);
                    if (user == null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                                new HttpError(StringHelper.InvalidUser)));
                    }



                    var tabsValue = new Zamba.Core.ZOptBusiness().GetValueOrDefaultNonShared("HomeWebTabs", string.Empty);


                    if (string.IsNullOrEmpty(tabsValue))
                    {
                        tabs.Add("Acciones");
                    }
                    else
                    {
                        tabsValue = tabsValue.Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Trim();
                        foreach (var tab in tabsValue.Split(','))
                        {
                            tabs.Add(tab.Trim());
                        }
                    }

                    return Ok(tabs);
                }
                else {
                    return InternalServerError(new Exception("Error al obtener el listado de novedades"));
                }

                
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Error al obtener el listado de novedades"));
            }
        }

    }

   
}

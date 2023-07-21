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

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Templates")]
    [RestAPIAuthorize ]
    //[Authorize]
    public class TemplatesController : ApiController
    {
        public TemplatesController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("ZambaWeb.RestApi");
            }
        }
        public Zamba.Core.IUser GetUser(long? userId)
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

        [HttpGet]
        [Route("GetTemplates")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        public IHttpActionResult GetTemplates()
        {
            var user = GetUser(null);
            if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError("Usuario invalido o sesion expirada")));
            DataSet ds = new DataSet();
            string query = "select * from ZTempl";
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);
            
            var js = JsonConvert.SerializeObject(ds.Tables[0]);
            return Ok(js);
        }

        [HttpGet]
        [Route("DownloadTemplate")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        public HttpResponseMessage DownloadTemplate(string path)
        {
            var user = GetUser(null);

            if (!File.Exists(path)) return null;

            HttpResponseMessage result = null;
            // Serve the file to the client
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);

            return result;
        }
    }
}

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Framework;
using Zamba.Data;
using ZambaWeb.RestApi.Controllers.Web;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.FileTools;
using System.IO;
using Zamba.Membership;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Web.Security;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class PushNotificationController : ApiController
    {
        Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();

        [Route("api/PushNotification/GetPlayerID")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        [OverrideAuthorization]
        public IHttpActionResult SetPlayerId(int user_id, string player_id)
        {
            try
            {
                string queryPushNotificationPlayerId = "delete from push_notification where user_id=" + user_id ;
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryPushNotificationPlayerId);
                queryPushNotificationPlayerId = "insert into push_notification(user_id,player_id) values(" + user_id + ",'" + player_id + "')";
                Zamba.Servers.Server.get_Con().ExecuteNonQuery(CommandType.Text, queryPushNotificationPlayerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok(ex.Message );
            }
        }
        [Route("api/PushNotification/Sendmessage")]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [OverrideAuthorization]
        [HttpPost, HttpGet]
        private void Sendmessage(String Titulo, String Contenido, List<string> players_id, string imageUrl)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("authorization", "Basic "  + zopt.GetValue("push_notification_authorization"));
            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = zopt.GetValue("push_notification_app_id"),
                headings = new { en = Titulo },
                contents = new { en = Contenido },
                //included_segments = new string[] { "Active Users" },
                include_player_ids = players_id.ToArray(),
                chrome_web_image = imageUrl
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);
            string responseContent = null;
            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }
            System.Diagnostics.Debug.WriteLine(responseContent);
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
                        if (item.Contains("user"))
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
    }
}






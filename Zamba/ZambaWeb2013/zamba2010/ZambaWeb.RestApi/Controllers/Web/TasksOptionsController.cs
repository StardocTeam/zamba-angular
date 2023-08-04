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

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/TasksOptions")]
    //[Authorize]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class TasksOptionsController : ApiController
    {
        
        #region Init&ClassHelpers
        public TasksOptionsController()
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

        #endregion

        [HttpGet]
        [Route("GetRecentTasks")]
        [OverrideAuthorization]
        public IHttpActionResult GetRecentTasks()
        {
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
            WFTaskBusiness WFTB = new WFTaskBusiness();
            try
            {
                var user = GetUser(null);
                if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
                if (Convert.ToBoolean(UP.getValue("OpenRecentTask", UPSections.WorkFlow, false, Zamba.Membership.MembershipHelper.CurrentUser.ID)))
                {
                    String thisDomain = Zamba.Membership.MembershipHelper.AppUrl;
                    if (!string.IsNullOrEmpty(thisDomain))
                    {
                        DataTable lTasks = WFTB.GetUserOpenedTasks(GetUser(null).ID);
                        var js = JsonConvert.SerializeObject(lTasks);
                        return Ok(js);
                    }
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent, new HttpError(StringHelper.RecentTaskNotFound)));

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
            finally
            {
                WFTB = null;
                UP = null;
            }
        }
   
    }
}

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
using static ZambaWeb.RestApi.Controllers.SearchController;
using ZambaWeb.RestApi.Controllers;

namespace Zamba.Web.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/TaskOptions")]
    public class TaskOptionsController : ApiController
    {
        // GET: TaskOptions
        public TaskOptionsController()
        {
            try
            {
                if (Zamba.Servers.Server.ConInitialized == false)
                {
                    Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                    ZC.InitializeSystem("ZambaWeb.RestApi");
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }
        }

        [HttpPost]
        [Route("UpdateFavorite")]
        public void UpdateFavorite(genericRequest paramRequest)
        {          
            var user = GetUser(paramRequest.UserId);

            Int64 docId = Int64.Parse(paramRequest.Params["docId"]);
            Int64 docTypeId = Int64.Parse(paramRequest.Params["docTypeId"]);
            Boolean val = Boolean.Parse(paramRequest.Params["val"]);

            DocumentLabelsData DocumentLabelsData = new DocumentLabelsData();
            DocumentLabelsData.UpdateFavoriteLabels(docTypeId, docId, val, paramRequest.UserId);
        }

        [HttpPost]
        [Route("UpdateImportant")]
        public void UpdateImportant(genericRequest paramRequest)
        {
            var user = GetUser(paramRequest.UserId);

            Int64 docId = Int64.Parse(paramRequest.Params["docId"]);
            Int64 docTypeId = Int64.Parse(paramRequest.Params["docTypeId"]);
            Boolean val = Boolean.Parse(paramRequest.Params["val"]);
            Int64 userId = paramRequest.UserId;  


            DocumentLabelsData DocumentLabelsData = new DocumentLabelsData();
            DocumentLabelsData.UpdateImportantLabels(docTypeId, docId, val, userId);
        }

        [HttpPost]
        [Route("AddNews")]
        public void AddNews(Int64 docId, int docTypeId, int userId, string val)
        {
            Transaction t = new Transaction();
            try
            {
                Int64 id = CoreData.GetNewID(IdTypes.News);

                var zNewsQuery = String.Format("INSERT INTO ZNews (NEWSID, DOCID, DOCTYPEID, VALUE, CRDATE) VALUES ({0}, {1}, {2}, '{3}', '{4}')", id, docId, docTypeId, val, DateTime.Now);
                var zNewsUserQuery = String.Format("INSERT INTO ZNewsUsers (NEWSID, USERID, STATUS) VALUES ({0}, {1}, {2})", id, userId, 0);

                t.Con.ExecuteNonQuery(t.Transaction, System.Data.CommandType.Text, zNewsQuery);
                t.Con.ExecuteNonQuery(t.Transaction, System.Data.CommandType.Text, zNewsUserQuery);
                t.Commit();
            }
            catch (Exception ex)
            {
                if (t != null && t.Transaction != null && t.Transaction.Connection.State != System.Data.ConnectionState.Closed)
                {
                    t.Rollback();
                }
                ZClass.raiseerror(ex);
            }
            finally
            {
                if (t != null)
                {
                    if (t.Con != null)
                    {
                        t.Con.Close();
                        t.Con.dispose();
                        t.Con = null;
                    }
                    t.Dispose();
                    t = null;
                }
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

    }
}
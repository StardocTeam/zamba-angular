using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using Zamba.Core;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Searchs;
using Nelibur.ObjectMapper;
using Zamba.Framework;
using Zamba.Core.Cache;
using ZambaWeb.RestApi.AuthorizationRequest;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/SearchWeb")]
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    //[Authorize]
    public class SearchWebController : ApiController
    {
        #region Constructor&ClassHelpers
        public SearchWebController()
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

        //[Route("GetTree")]
        //[HttpGet]
        //public IHttpActionResult GetTree()
        //{
        //    try
        //    {
        //        var user = GetUser(null);
        //        if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
        //        var result = GetTree((int)user.ID);
        //        return Ok(result);

        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
        //    }
        //}

       

      

        /// <summary>
        /// Obtiene lista de Indices para grilla de busqueda en web
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        /// [System.Web.Http.AcceptVerbs("GET", "POST")]
        /// 
        [Route("GetIndexs")]
        [OverrideAuthorization]
        [HttpGet]
        public IHttpActionResult GetIndexs(string indexs = "")
        {
            if (indexs == string.Empty) return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, StringHelper.IndexExpected));
            IndexsBusiness IB = new IndexsBusiness();
            try
            {
                indexs = indexs.Replace("[", "").Replace("]", "");
                var indexList = indexs.Split(',').Select(Int64.Parse).ToList();
                List<IIndex> Indexs = new List<IIndex>();
                if (indexs != null)
                {
                    Indexs = IB.GetIndexsSchema(Zamba.Membership.MembershipHelper.CurrentUser.ID, indexList);
                }
                var js = JsonConvert.SerializeObject(Indexs);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
            finally
            {
                IB = null;

            }

        }

        //[Route("GetResults")]
        //[HttpPost]
        //public IHttpActionResult GetResults(SearchDto searchDto)
        //{
        //    DocTypesBusiness DTB = new DocTypesBusiness();
        //    try
        //    {
        //        var user = GetUser(searchDto.UserId);
        //        if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

        //        TinyMapper.Bind<SearchDto, Search>();
        //        var search = TinyMapper.Map<Search>(searchDto);

        //        if (searchDto.DoctypesIds == null)
        //            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(StringHelper.IndexExpected)));

        //        foreach (Int64 EntityId in searchDto.DoctypesIds)
        //        {
        //            IDocType Entity = DTB.GetDocType(EntityId);
        //            search.AddDocType(Entity);
        //        }

        //        search.Indexs = new List<IIndex>();
        //        var searchDtoIndexs = searchDto.Indexs;

        //        foreach (object searchDtoIndex in searchDto.Indexs)
        //        {
        //            var index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
        //            if (index.Data != string.Empty || index.dataDescription != string.Empty)
        //                search.AddIndex(index);
        //        }

        //        ModDocuments MD = new ModDocuments();
        //        Int64 TotalCount = 0;
        //        var results = MD.DoSearch(ref search, user.ID, 0, 100, false, false, true, ref TotalCount, false);
        //        var js = JsonConvert.SerializeObject(results);
        //        return Ok(js);
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
        //    }
        //    finally { DTB = null; }

        //}

    }
}

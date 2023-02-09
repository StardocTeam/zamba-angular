using System.Collections.Generic;
using System.Web.Http;
using System.Data;
using System.Collections;
//using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Web.Http.Routing;
using System.Net.Http;
using System.Net;
using Zamba;
using Newtonsoft.Json;
using Zamba.Core.Searchs;
using Nelibur.ObjectMapper;
using Zamba.Web.Helpers;
using System.Linq;
using Zamba.Web.Models;
using Zamba.Framework;

namespace Zamba.Web.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SearchWebController : ApiController
    {


        public SearchWebController()
        {
            ZCore ZC = new ZCore();
            if (Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.Web");

           ZC.VerifyFileServer();
        }

        [Route("api/SearchWeb/Suggestions")]
        public IEnumerable<Dictionary<string, object>> GetSuggestions(string text = "")
        {
            SearchSuggestions ss;
            DataSet ds = null;
            try
            {
                ss = new SearchSuggestions();
                ds = ss.getData(text);
                List<Dictionary<string, object>> lstData = ss.GetJson(ds.Tables[0]);
                return lstData;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener las sugerencias" + ex.ToString());
            }
            finally
            {
                ss = null;
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/SearchWeb/Results")]
        public DataTable Results(Filter parameters)
        {
            if (parameters != null)
            {
                UserBusiness UB = new UserBusiness();
                IUser User = UB.ValidateLogIn(parameters.UserId, ClientType.Web);
                if (User != null)
                {
                    SearchHelper sh = new SearchHelper();
                    ModDocuments md = new ModDocuments();
                    try
                    {
                        var search = sh.GetSearch(ref parameters);
                        search.UserId = parameters.UserId;
                        Int64 TotalCount = 0;
                        var dt = md.DoSearch(ref search, User.ID, 0, 100, false, false, true, ref TotalCount);
                        // DataTable dt = md.DoSearch(search, search.UserId, 1, 10, false, true, false);

                        return dt;
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);

                        return new DataTable("Error al obtener los resultados " + ex.ToString());
                    }
                    finally
                    {
                        sh = null;
                        md = null;
                        UB = null;
                    }
                }
            }
            return null;
        }

        [Route("api/searchweb/Entities")]
        public List<Entity> GetEntities(int? userId)
        {
            UserBusiness UB = new UserBusiness();

            var user = UB.ValidateLogIn(userId.Value, ClientType.Web);

            if (user == null)
                throw new Exception(StringHelper.InvalidUser);


            RightsSchema rightsSchema = new RightsSchema(user.ID);

            try
            {
                return rightsSchema.GetEntities();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                throw new Exception("Error al obtener las entidades y sus atributos");
            }
            finally
            {
                rightsSchema = null;
                UB = null;
            }
        }


        [Route("api/SearchWeb/Indexs")]
        [HttpPost, HttpGet]
        public string Indexs(List<Int64> SelectedEntitiesIds)
        {
            Zamba.Core.IndexsBusiness IB;
            try
            {
                IB = new Zamba.Core.IndexsBusiness();
                List<IIndex> Indexs = new List<IIndex>();
                if (SelectedEntitiesIds != null)
                    Indexs = IB.GetIndexsSchema(Zamba.Membership.MembershipHelper.CurrentUser.ID, SelectedEntitiesIds);

                var newresults = JsonConvert.SerializeObject(Indexs, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
                IB = null;
            }
        }


        [Route("api/SearchWeb/FillIndex")]
        [HttpPost]
        public string FillIndex(string IndexId)
        {
            Zamba.Core.IndexsBusiness IndexB;
            try
            {
                IndexB = new Zamba.Core.IndexsBusiness();

                var Indexs = IndexB.GetIndexById(Int64.Parse(IndexId));
                var newresults = JsonConvert.SerializeObject(Indexs, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
                IndexB = null;
            }
        }


        public class searchList
        {
            public Int64 IndexId { get; set; }
            public string Value { get; set; }
            public Int64 LimitTo { get; set; }

        }

        [Route("api/SearchWeb/ListOptions")]
        [HttpPost]
        public string ListOptions(searchList searchlist)
        {
            try
            {
                List<IIndexList> List = new List<IIndexList>();

                if (searchlist.IndexId > 0)
                {
                    if (searchlist.Value == null) searchlist.Value = string.Empty;

                    IndexsBusiness IB = new IndexsBusiness();
                    IIndex index = IB.GetIndex(searchlist.IndexId);
                    List = IB.GetIndexList(searchlist.IndexId, index.DropDown, searchlist.Value, searchlist.LimitTo);
                }

                var newresults = JsonConvert.SerializeObject(List, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener la lista: " + ex.ToString());
            }
            finally
            {
            }
        }

        public class SearchDto
        {
            public Int64 SearchId { get; set; }
            public Int64 OrganizationId { get; set; }
            public List<Int64> DoctypesIds { get; set; }
            public List<Object> Indexs { get; set; }
            public Boolean blnSearchInAllDocsType { get; set; }
            public string TextSearchInAllIndexs { get; set; }
            public Boolean RaiseResults { get; set; }
            public string ParentName { get; set; }
            public Boolean CaseSensitive { get; set; }
            public Int64 MaxResults { get; set; }
            public Boolean ShowIndexOnGrid { get; set; }
            public Boolean UseVersion { get; set; }
            public Int64 UserId { get; set; }
            public Int64 StepId { get; set; }
            public Int64 StepStateId { get; set; }
            public Int64 TaskStateId { get; set; }
            public Int64 WorkflowId { get; set; }
            public string NotesSearch { get; set; }
            public string Textsearch { get; set; }
            public string Restriction { get; set; }

        }

        public class SearchDtoIndex
        {

        }


        //[Route("api/SearchWeb/DoSearch")]
        //[HttpPost]
        //public string DoSearch(SearchDto searchDto)
        //{
        //    UserBusiness UB;
        //    DocTypesBusiness DTB;
        //    try
        //    {
        //        UB = new UserBusiness();
        //        DTB = new DocTypesBusiness();
        //        IUser User = UB.ValidateLogIn(searchDto.UserId, ClientType.Web);
        //        if (User != null)
        //        {
        //            TinyMapper.Bind<SearchDto, Search>();
        //            var search = TinyMapper.Map<Search>(searchDto);

        //            foreach (Int64 EntityId in searchDto.DoctypesIds)
        //            {
        //                IDocType Entity = DTB.GetDocType(EntityId);
        //                search.AddDocType(Entity);
        //            }

        //            search.Indexs = new List<IIndex>();
        //            var searchDtoIndexs = searchDto.Indexs;

        //            foreach (object searchDtoIndex in searchDto.Indexs)
        //            {
        //                var index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
        //                if (index.Data != string.Empty || index.dataDescription != string.Empty)
        //                    search.AddIndex(index);
        //            }

        //            ModDocuments MD = new ModDocuments();
        //            Int64 TotalCount = 0;
        //            var results = MD.DoSearch(ref search, User.ID, 0, 100, false, false, true, ref TotalCount);
        //            var newresults = JsonConvert.SerializeObject(results, Formatting.Indented,
        //            new JsonSerializerSettings
        //            {
        //                PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //            });
        //            return newresults;
        //        }
        //        return string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        ZClass.raiseerror(ex);
        //        throw new Exception("Error al obtener la lista: " + ex.ToString());
        //    }
        //    finally
        //    {
        //        UB = null;
        //        DTB = null;
        //    }
        //}


        public class ResultDto
        {
            public Int64 DOC_ID { get; set; }//Id
            public Int64 DOC_TYPE_ID { get; set; }//EntityId
            public Int64 UserId { get; set; }
        }

        const string _URLFORMAT = "/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&userid={2}&token={3}";

        [Route("api/search/GetFileUrl")]
        [HttpPost]
        public string GetFileUrl(ResultDto resultDto)
        {
            try
            {
                String token = new ZssFactory().GetZss(Zamba.Membership.MembershipHelper.CurrentUser).Token;
                var url = string.Format(_URLFORMAT, resultDto.DOC_TYPE_ID, resultDto.DOC_ID, resultDto.UserId,token);
                return url;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener la lista: " + ex.ToString());
            }
            finally
            {
            }
        }


        [Route("api/SearchWeb/GetIndexData")]
        [HttpPost]
        public string GetIndexData(ResultDto resultDto)
        {
            Zamba.Core.Results_Business RB;
            try
            {
                RB = new Zamba.Core.Results_Business();
                List<IIndex> Indexs = new List<IIndex>();
                if (resultDto != null)
                    Indexs = RB.FillIndexData(resultDto.DOC_TYPE_ID, resultDto.DOC_ID, Indexs);

                var newresults = JsonConvert.SerializeObject(Indexs, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
                RB = null;
            }
        }


        [Route("api/SearchWeb/GetFile")]
        [HttpPost]
        public byte[] GetFile(ResultDto resultDto)
        {
            try
            {
                Results_Business rb = new Results_Business();
                var file = rb.GetBlob(resultDto.DOC_ID, resultDto.DOC_TYPE_ID, resultDto.UserId);
                if (file != null)
                {
                    //var newresults = JsonConvert.SerializeObject(file, Formatting.Indented,
                    //              new JsonSerializerSettings
                    //              {
                    //                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    //              });
                    //return newresults;
                    return file;
                }
                return null;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener la lista: " + ex.ToString());
            }
            finally
            {
            }
        }


        [Route("api/SearchWeb/Tree")]
        public HttpResponseMessage GetTree(string token, int currentuserid)
        {
            Zamba.Core.UserPreferences UP;
            try
            {
                UP = new Zamba.Core.UserPreferences();
                var lastselectednodeid = UP.getEspecificUserValue("WebSearchLastNodes", Zamba.UPSections.Viewer, string.Empty, currentuserid);
                var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON select " + Zamba.Servers.Server.DBUser + ".qfn_XmlToJson((select tree from " + Zamba.Servers.Server.DBUser + ".zsp_search_100_gettree({0},'{1}')))", currentuserid, lastselectednodeid));
                return Request.CreateResponse<string>(HttpStatusCode.OK, JsonTree.ToString(), Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
                UP = null;
            }
        }

        public class LastNodeObj
        {
            public LastNodeObj()
            {
            }
            public string LastNodes { get; set; }
            public int UserId { get; set; }
        }

        [Route("api/SearchWeb/SetLastNodes")]
        [HttpPost]
        public string SetLastNodes(LastNodeObj lastNodeObj)
        {
            Zamba.Core.UserPreferences UP;
            try
            {
                UP = new Zamba.Core.UserPreferences();
                UP.setEspecificUserValue("WebSearchLastNodes", lastNodeObj.LastNodes, Zamba.UPSections.Viewer, lastNodeObj.UserId);
                var newresults = JsonConvert.SerializeObject("OK", Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al guardar nodos: " + ex.ToString());
            }
            finally
            {
                UP = null;
            }
        }

        [Route("api/SearchWeb/GetLastNodes")]
        [HttpPost]
        public string GetLastNodes(string LastNodes, int currentuserid)
        {
            Zamba.Core.UserPreferences UP;
            try
            {
                UP = new Zamba.Core.UserPreferences();
                return UP.getEspecificUserValue("WebSearchLastNodes", Zamba.UPSections.Viewer, string.Empty, currentuserid);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                return string.Empty;
            }
            finally
            {
                UP = null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/SearchWeb/SuggestionsList")]
        public IEnumerable<Dictionary<string, object>> SuggestionsList(string index, string entities, string word)
        {
            SearchSuggestions ss;
            DataSet ds = null;
            var ind = index.Split(',').Select(x => Int32.Parse(x)).ToArray();
            var ent = entities.Split(',').Select(x => Int32.Parse(x)).ToArray();

            try
            {
                ss = new SearchSuggestions();
                ds = ss.SuggestionsList(ind, ent, word.Split(','));
                List<Dictionary<string, object>> lstData = ss.GetJson(ds.Tables[0]);
                return lstData;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;

            }
            finally
            {
                ss = null;
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }

    }
}

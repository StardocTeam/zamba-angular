using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Core.Search;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Zamba.Core.Searchs;
using Zamba;
using System.Net.Http;
using Nelibur.ObjectMapper;
using ZambaWeb.RestApi.ViewModels;
using System.Net;
using Zamba.Core.WF.WF;
using Zamba.Framework;
using Zamba.Data;
using Zamba.Services;
using Zamba.Core.Access;
using System.Globalization;
using System.Collections;
using System.Net.Http.Headers;
using Zamba.Membership;
using System.Web.Script.Serialization;
using Zamba.FileTools;
using Microsoft.Ajax.Utilities;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]    
    [RestAPIAuthorize]
    [globalControlRequestFilter]

    public class SearchController : ApiController
    {
        public SearchController()
        {
            ZCore ZC = new ZCore();

            if (Zamba.Servers.Server.ConInitialized == false)
                ZC.InitializeSystem("Zamba.WebRS");

            ZC.VerifyFileServer();




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
                        if (item.ToLower().Contains("user"))
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetUserName")]
        [AllowAnonymous]
        [OverrideAuthorization]
        public string GetUserName(int userId)
        {
            Zamba.Core.UserBusiness UB;
            try
            {
                UB = new Zamba.Core.UserBusiness();
                var user = GetUser(userId);
                var userName = user.Name;
                return userName;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return string.Empty;
            }
            finally
            {
                UB = null;
            }

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetUserInfoForName")]
        [AllowAnonymous]
        public string GetUserInfoForName(string UserName)
        {
            var userName = UserName;
            UserBusiness UserBusiness = new UserBusiness();
            IUser user = UserBusiness.GetUserByname(userName, true);

            if (UserName != null)
            {
                try
                {
                    List<string> RP = new List<string>();
                    RP.Add(user.ID.ToString());
                    RP.Add(user.Nombres);
                    RP.Add(user.Apellidos);
                    RP.Add(user.Name);
                    RP.Add(user.puesto);
                    RP.Add(user.telefono);


                    var newresults = JsonConvert.SerializeObject(RP, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                DateFormatString = "yyyy-MM-dd",
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                            });

                    return newresults;
                }


                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;

        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetUserData")]
        public string GetUserData(int userId)
        {
            var user = GetUser(userId);
            if (user != null)
            {
                try
                {
                    List<string> RP = new List<string>();
                    RP.Add(user.Nombres);
                    RP.Add(user.Apellidos);
                    RP.Add(user.Name);
                    RP.Add(user.puesto);
                    RP.Add(user.telefono);

                    var newresults = JsonConvert.SerializeObject(RP, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                DateFormatString = "yyyy-MM-dd",
                                PreserveReferencesHandling = PreserveReferencesHandling.Objects
                            });

                    return newresults;
                }


                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/Test")]
        [OverrideAuthorization] 
        //[Authorize]
        public string Test()
        {
            try
            {
                string result = "Hi, I'm Zamba";
                var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al realizar el Test: " + ex.ToString());
            }
            finally
            {
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/BasicTest")]
        [OverrideAuthorization] 
        public string BasicTest()
        {
            try
            {
                string result = "Hi, I'm Zamba";
                var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al realizar el Test: " + ex.ToString());
            }
            finally
            {
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/TestParam")]
        [OverrideAuthorization]
        public string TestParam(string val)
        {
            try
            {
                string result = "Hi, I'm Zamba" + val;
                var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al realizar el Test: " + ex.ToString());
            }
            finally
            {
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/Suggestions")]
               
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        // [System.Web.Http.HttpGet]
        [Route("api/search/SuggestionsByIndex")]       //no hubo forma de pasar desde AJAX lista de entidades
        [OverrideAuthorization]
        public IEnumerable<Dictionary<string, object>> SuggestionsByIndex(int index, string entities, string word)
        {
            SearchSuggestions ss;
            DataSet ds = null;
            var ent = entities.Split(',').Select(x => Int32.Parse(x)).ToArray();
            try
            {
                ss = new SearchSuggestions();
                ds = ss.SuggestionsByIndex(index, ent, word);
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/SuggestionsList")]
        [OverrideAuthorization]
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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/SuggestionsAdvEnt")]
        [OverrideAuthorization]
        public IEnumerable<Dictionary<string, object>> SuggestionsAdvEnt(int entity, string entWord, string index, string word, string filter, string filterword)
        {
            SearchSuggestions ss;
            DataSet ds = null;

            var ind = new int[] { };
            if (!(index == null || string.IsNullOrEmpty(index)))
                ind = index.Split(',').Select(x => Int32.Parse(x)).ToArray();

            var w = new string[] { };
            if (!(index == null || string.IsNullOrEmpty(index)))
                w = word.Split(',');

            try
            {
                ss = new SearchSuggestions();
                ds = ss.SuggestionsAdvEnt(entity, entWord ?? "", ind, w, filter, filterword);
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/SuggestionsAdvInd")]
        [OverrideAuthorization]
        public IEnumerable<Dictionary<string, object>> SuggestionsAdvInd(int index, string indWord, string entities, string filter, string filterword)
        {
            SearchSuggestions ss;
            DataSet ds = null;
            var ent = entities.Split(',').Select(x => Int32.Parse(x)).ToArray();

            try
            {
                ss = new SearchSuggestions();
                ds = ss.SuggestionsAdvInd(index, indWord, ent, filter, filterword);
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

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/Results")]
        [OverrideAuthorization]
        public IHttpActionResult FilterResults(Filter parameters)
        {
            if (parameters != null)
            {
                var user = GetUser(parameters.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                SearchHelper sh = new SearchHelper();
                ModDocuments md = new ModDocuments();
                try
                {
                    var search = sh.GetSearch(ref parameters);
                    search.UserId = parameters.UserId;
                    //SizePage: LastPage =0 PageSize =25 trae 1-25 reg // LastPage =1 PageSize =25 trae 26-50 reg
                    var lp = parameters.SizePage.LastPage;
                    var pz = parameters.SizePage.PageSize;
                    Int64 TotalCount = 0;
                    var results = md.DoSearch(ref search, user.ID, lp, pz, false, false, true, ref TotalCount);

                    searchResult sr = new searchResult();

                    //Creo esta lista porque no puedo eliminar columnas el vuelo (columnas que no deben verse)
                    List<string> ColumnsToRemove = new List<string>();
                    foreach (DataColumn c in results.Columns)
                    {
                        if ((c.ColumnName.ToLower().StartsWith("i") && IsNumeric(c.ColumnName.Remove(0, 1))) || (GridColumns.ColumnsVisibility.ContainsKey(c.ColumnName.ToLower()) && GridColumns.ColumnsVisibility[c.ColumnName.ToLower()] == false))
                        {
                            ColumnsToRemove.Add(c.ColumnName);
                        }
                    }
                    //Remuevo las columnas
                    if (ColumnsToRemove.Count > 0)
                    {
                        foreach (string colName in ColumnsToRemove)
                            results.Columns.Remove(colName);
                    }

                    // Cambia el nombre por el alias para mostrar en la grilla
                    foreach (var item in GridColumns.ZambaColumns)
                    {
                        if (results.Columns.Contains(item.Value))
                            results.Columns[item.Value].ColumnName = item.Key;
                    }

                    foreach (DataColumn c in results.Columns)
                    {
                        c.ColumnName = c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace("__", "_");
                        sr.columns.Add(c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace("__", "_"));
                    }

                    results.AcceptChanges();

                    sr.data = results;
                    sr.total = results.Rows.Count;

                    return Ok(sr);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                    // return new DataTable("Error al obtener los resultados " + ex.ToString());
                }
                finally
                {
                    sh = null;
                    md = null;
                }
            }
            return null;
        }

        [Route("api/search/Entities")]
        
        //[OverrideAuthorization]
        [System.Web.Http.HttpGet]
        //    [Authorize]
        public IHttpActionResult GetEntities(int? userId)//Int64 userId
        {
            var user = GetUser(userId);

            if (user == null)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                    new HttpError(StringHelper.InvalidUser)));


            RightsSchema rightsSchema = new RightsSchema();

            try
            {
                var entities = rightsSchema.GetEntities(user.ID, FillTypes.WithIndexs);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;

            }
            finally
            {
                rightsSchema = null;
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/Indexs")]
        [HttpPost, HttpGet]
        [RestAPIAuthorize]
        public string Indexs(List<Int64> SelectedEntitiesIds)
        {
            try
            {
                IUser user = Zamba.Membership.MembershipHelper.CurrentUser;
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser))).ToString();

                List<IIndex> Indexs = new List<IIndex>();

                if (SelectedEntitiesIds != null)
                {
                    IndexsBusiness IB = new IndexsBusiness();
                    Indexs = IB.GetIndexsSchema(user.ID, SelectedEntitiesIds);
                    IB = null;
                }
                if (Indexs != null)
                {
                    var newresults = JsonConvert.SerializeObject(Indexs, Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                    return newresults;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
            }
        }

        [Route("api/search/Index")]
        [OverrideAuthorization]
        [HttpPost, HttpGet]
        public string Index(long IndexId)
        {
            try
            {
                Zamba.Core.IndexsBusiness IndexB = new Zamba.Core.IndexsBusiness();

                var Indexs = IndexB.GetIndexById(IndexId);
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
            }
        }

        /// <summary>
        /// En la busqueda de search.html para los slst (la lupa) trae resultados con un parametro de tope/// 
        /// </summary>
        /// <param name="searchlist">
        /// parametros de busqueda
        /// </param>
        /// <returns></returns>
        [Route("api/search/ListOptions")]
        [OverrideAuthorization]
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
                    List = IB.GetIndexList(searchlist.IndexId, index.DropDown, searchlist.Value, searchlist.LimitTo + 1);
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

        public class Column
        {
            public string field { get; set; }
            public string title { get; set; }
        }

        private class searchResult
        {
            public DataTable data { get; set; }
            public List<string> columns { get; set; } = new List<string>();
            public List<IIndex> filterIndexs { get; set; } = new List<IIndex>();

            public string columnsStringAssociated { get; set; }
            public string columnsStringMyTasks { get; set; }
            public string columnsStringTeam { get; set; }
            public string columnsStringAll { get; set; }
            public List<EntityDto> entities { get; set; } = new List<EntityDto>();
            public List<Filters> filters { get; set; } = new List<Filters>();
            public long total { get; set; } = 0;

            public string OrderBy { get; set; }
            public string VirtualEntities { get; internal set; }

            public class Filters
            {
                public string Name { get; set; }
                public string Value { get; set; }
            }

            public List<IIndex> Index { get; set; } = new List<IIndex>();
            public List<ViewDto> views { get; set; } = new List<ViewDto>();
            public List<StepDTO> Steps { get; set; } = new List<StepDTO>();

        }

        private class searchResultList
        {
            public object value { get; set; }
            public long Type { get; set; }
            public long Len { get; set; }
            public List<String> DropDownList { get; set; }
            public int DropDown { get; set; }
            public string Column { get; set; }
            public int IconId { get; set; }
            public long ID { get; set; }
            public string Name { get; set; }
            public bool Required { get; set; }
            public string DefaultValue { get; set; }
        }


        public void ResetCountsOfResults(List<EntityDto> List)
        {
            foreach (var item in List)
            {
                item.ResultsCount = 0;
            }
        }
        public String SinCaracteresEspeciales(String nombre)
        {
            for (int i = 0; i < nombre.Length - 1; i++)
            {
                nombre = nombre
                    .Replace(" ", "_").Replace("-", "_").Replace("%", "_").Replace("/", "_")
                    .Replace("._", "_").Replace("*", "_").Replace(".", "_").Replace("?", "_").Replace("¿", "_")
                    .Replace("+", "_").Replace("/", "_").Replace("&", "_").Replace("-", "_").Replace("\\", "_")
                    .Replace("%", "_").Replace(")", "_").Replace("(", "_").Replace("#", "_").Replace("$", "_")
                    .Replace("+", "_").Replace("°", "_").Replace("__", "_");
            }
            return nombre;
        }




        [Route("api/search/DoSearch")]
        [HttpPost]
        [HttpGet]

        public string DoSearch(SearchDto searchDto)
        {
            DocTypesBusiness UB = new DocTypesBusiness();
            UserBusiness UBR = new UserBusiness();
            try
            {
                IUser User = UBR.ValidateLogIn(searchDto.UserId, ClientType.WebApi);
                //IUser User = Zamba.Membership.MembershipHelper.CurrentUser;
                if (User != null)
                {
                    Search search = null;
                    try
                    {
                        try
                        {
                            TinyMapper.Bind<SearchDto, Search>();
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                        search = TinyMapper.Map<Search>(searchDto);
                    }
                    catch (InvalidCastException ex)
                    {
                        ZClass.raiseerror(ex);
                        TinyMapper.Bind<SearchDto, Search>();
                        search = TinyMapper.Map<Search>(searchDto);
                    }

                    searchResult sr = new searchResult();

                    DataTable results = null;
                    string tempOrderBy = string.Empty;
                    string sortOp = string.Empty;
                    Int64 TotalCount = 0;
                    if (search.View == null)
                        search.View = "";
                    if (search.View != string.Empty && search.View.StartsWith("reportid"))
                    {
                        Int64 reportId = Int64.Parse(search.View.Replace("reportid", ""));
                        Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();
                        DataSet resultsDS = RB.RunQueryBuilderReporteGeneral(reportId, null);
                        if (resultsDS != null && resultsDS.Tables.Count > 0)
                        {
                            results = resultsDS.Tables[0];

                            if (search.ResultsCount.ContainsKey((Int64)0))
                                search.ResultsCount[0] = (Int64)results.Rows.Count;
                            else
                                search.ResultsCount.Add((Int64)0, (Int64)results.Rows.Count);

                            TotalCount = results.Rows.Count;
                        }
                    }
                    else
                    {

                        if (searchDto.StepId > 0)
                            search.StepId = searchDto.StepId;

                        if (searchDto.AsignedTasks)
                        {
                            search.SearchType = SearchTypes.AsignedTasks;
                            //search.SearchType = SearchTypes.CommonSearch;
                            if (search.View == null)
                                search.View = string.Empty;

                            DocTypesBusiness DTB = new DocTypesBusiness();
                            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();


                            List<IDocType> Doctypes = DTB.GetDocTypesbyUserRights(User.ID, RightsType.View).ToList<IDocType>();
                            string MyTaskEntities = UP.getValue("MyTasksEntities", UPSections.UserPreferences, "", searchDto.UserId);
                            string IdsAllTasks = UP.getValue("IdsAllTasks", UPSections.UserPreferences, "", searchDto.UserId);

                            if (MyTaskEntities.Length > 0)
                            {
                                MyTaskEntities = MyTaskEntities.Replace(" ", "");
                            }

                            if (IdsAllTasks.Length > 0)
                            {
                                IdsAllTasks = IdsAllTasks.Replace(" ", "");
                            }

                            sr.columnsStringMyTasks = UP.getValue("columnStringMyTasks", UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);
                            sr.columnsStringTeam = UP.getValue("columnStringTeam", UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);
                            sr.columnsStringAll = UP.getValue("columnStringAll", UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);


                            if (search.Doctypes == null)
                                search.Doctypes = new List<IDocType>();

                            List<string> MyTaskEntitiesList = new List<string>();
                            MyTaskEntitiesList.AddRange(MyTaskEntities.Trim().Split(new char[] { char.Parse(",") }, StringSplitOptions.RemoveEmptyEntries));

                            List<string> IdsAllTasksList = new List<string>();
                            IdsAllTasksList.AddRange(IdsAllTasks.Trim().Split(new char[] { char.Parse(",") }, StringSplitOptions.RemoveEmptyEntries));

                            foreach (IDocType Doctype in Doctypes)
                            {
                                if (MyTaskEntitiesList.Count == 0 || MyTaskEntitiesList.Contains(Doctype.ID.ToString()))
                                {
                                    if (search.View.Contains("ViewAllMy") && IdsAllTasksList.Count > 0)
                                    {
                                        if (IdsAllTasksList.Contains(Doctype.ID.ToString()))
                                        {
                                            if (searchDto.entities.Count == 0)
                                            {
                                                search.Doctypes.Add(Doctype);
                                                sr.entities.Add(new EntityDto() { id = Doctype.ID, name = Doctype.Name, enabled = true });
                                            }
                                            else
                                            {
                                                EntityDto E = searchDto.entities.Find(x => x.id == Doctype.ID);
                                                if (E.enabled)
                                                {
                                                    search.Doctypes.Add(Doctype);
                                                }
                                                sr.entities.Add(E);

                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (searchDto.entities.Count == 0)
                                        {
                                            search.Doctypes.Add(Doctype);
                                            sr.entities.Add(new EntityDto() { id = Doctype.ID, name = Doctype.Name });
                                        }
                                        else
                                        {
                                            EntityDto E = searchDto.entities.Find(x => x.id == Doctype.ID);
                                            if (E.enabled)
                                            {
                                                search.Doctypes.Add(Doctype);
                                            }
                                            sr.entities.Add(E);

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (Int64 EntityId in searchDto.DoctypesIds)
                            {
                                IDocType Entity = UB.GetDocType(EntityId);
                                search.AddDocType(Entity);
                                sr.entities.Add(new EntityDto() { id = Entity.ID, name = Entity.Name });
                            }
                        }

                        search.Indexs = new List<IIndex>();
                        var searchDtoIndexs = searchDto.Indexs;

                        //                    if (searchDto.Indexs != null && search.SearchType != SearchTypes.AsignedTasks)
                        if (searchDto.Indexs != null)
                        {
                            //foreach (object searchDtoIndex in searchDto.Indexs)
                            //{
                            //    var index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
                            //    if ((index.Data != null && index.Data != string.Empty) || (index.dataDescription != null && index.dataDescription != string.Empty))
                            //        search.AddIndex(index);
                            //}

                            foreach (object searchDtoIndex in searchDto.Indexs)
                            {

                                var index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());

                                if (index.DropDown.ToString() == "AutoSustitución")
                                {
                                    AutoSubstitutionBusiness Asb = new AutoSubstitutionBusiness();
                                    var descripcion = string.Empty;

                                    if (!string.IsNullOrEmpty(index.Data) && index.dataDescription != "")
                                    {
                                        var indice = index.Column.Replace("I", "");
                                        descripcion = Asb.getDescription(index.Data.ToString(), int.Parse(indice));
                                    }

                                    if (string.IsNullOrEmpty(descripcion) && index.Data != "")
                                    {
                                        index.Data = string.Empty;
                                    }
                                }

                                if ((index.Data != null && index.Data != string.Empty) || (index.dataDescription != null && index.dataDescription != string.Empty))
                                    search.AddIndex(index);
                            }
                        }

                        search.Filters = new List<ikendoFilter>();
                        var searchDtoFilters = searchDto.Filters;

                        foreach (object searchDtoFilter in searchDto.Filters)
                        {
                            var filter = JsonConvert.DeserializeObject<kendoFilter>(searchDtoFilter.ToString());
                            if (filter.Field != string.Empty || filter.Value != string.Empty || filter.Operator != string.Empty)
                                search.AddFilter(filter);
                        }

                        UB = null;
                        UBR = null;

                        if (string.IsNullOrEmpty(searchDto.OrderBy))
                        {
                            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                            var customUserTasksOrderBy = UP.getValue("custonUserTasksOrderBy", UPSections.UserPreferences, "", searchDto.UserId);
                            if (customUserTasksOrderBy != string.Empty)
                            {
                                searchDto.OrderBy = customUserTasksOrderBy;
                                sr.OrderBy = customUserTasksOrderBy;
                                search.OrderBy = customUserTasksOrderBy;
                            }
                        }


                        if (!string.IsNullOrEmpty(searchDto.OrderBy))
                        {
                            sr.OrderBy = searchDto.OrderBy;
                            search.OrderBy = searchDto.OrderBy;


                            if (search.OrderBy.ToLower().Contains(" desc"))
                                sortOp = "desc";
                            else
                                sortOp = "asc";

                            var indexof = search.OrderBy.IndexOf(sortOp);
                            if (indexof == -1) indexof = search.OrderBy.Length;
                            tempOrderBy = search.OrderBy.Substring(0, indexof).ToString().Trim();
                            tempOrderBy = GridColumns.GetColumnNameByAliasName(tempOrderBy);

                            if (tempOrderBy.Contains("_"))
                            {
                                tempOrderBy = tempOrderBy.Replace("_", " ");

                                if (tempOrderBy.Split().Length > 1)
                                    tempOrderBy = string.Format("{0}", tempOrderBy.Trim());
                            }

                            if (Zamba.Servers.Server.isOracle)
                            {
                                search.OrderBy = string.Format("\"{0}\" {1}", tempOrderBy, sortOp);
                            }
                            else
                            {
                                search.OrderBy = string.Format("\'{0}\' {1}", tempOrderBy, sortOp);
                            }
                        }


                        ModDocuments MD = new ModDocuments();
                        results = MD.DoSearch(ref search, User.ID, search.LastPage, search.PageSize, false, false, true, ref TotalCount);

                    }



                    //Creo esta lista porque no puedo eliminar columnas el vuelo (columnas que no deben verse)
                    List<string> ColumnsToRemove = new List<string>();


                    // Cambia el nombre por el alias para mostrar en la grilla
                    foreach (var item in GridColumns.ZambaColumns)
                    {
                        if (results.Columns.Contains(item.Value))
                            results.Columns[item.Value].ColumnName = item.Key;
                    }

                    Boolean OrderbyContainsColumn = false;
                    IndexsBusiness IB = new IndexsBusiness();
                    foreach (DataColumn c in results.Columns)
                    {
                        if ((c.ColumnName.ToLower().StartsWith("i") && IsNumeric(c.ColumnName.Remove(0, 1))) || (GridColumns.ColumnsVisibility.ContainsKey(c.ColumnName.ToLower()) && GridColumns.ColumnsVisibility[c.ColumnName.ToLower()] == false))
                        {
                            ColumnsToRemove.Add(c.ColumnName);
                        }
                        else
                        {
                            if (search.OrderBy.Contains(c.ColumnName))
                                OrderbyContainsColumn = true;

                            try
                            {
                                Int64 indexId = IndexsBusiness.GetIndexIdByName(c.ColumnName);
                                if (indexId != 0)
                                {
                                    IIndex i = IB.GetIndex(indexId);
                                    sr.filterIndexs.Add(i);
                                }
                            }
                            catch (Exception)
                            {
                            }

                            string newColName = c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "_").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace(".", "_").Replace("?", "_").Replace("¿", "_").Replace("+", "_").Replace("/", "_").Replace("&", "_").Replace("-", "_").Replace("\\", "_").Replace("%", "_").Replace(")", "_").Replace("(", "_").Replace("#", "_").Replace("$", "_").Replace("+", "_").Replace("°", "_").Replace("__", "_");
                            //string newColName = c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "_").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace(".", "_").Replace("?", "_").Replace("¿", "_").Replace("+", "_").Replace("/", "_").Replace("&", "_").Replace("-", "_").Replace("\\", "_").Replace("%", "_").Replace(")", "_").Replace("(", "_").Replace("#", "_").Replace("$", "_").Replace("+", "_").Replace("°", "_").Replace("ó", "_").Replace("ú", "_").Replace("__", "_");
                            c.ColumnName = newColName;
                            sr.columns.Add(newColName);

                        }
                    }
                    IB = null;

                    //Remuevo las columnas
                    if (ColumnsToRemove.Count > 0)
                    {
                        foreach (string colName in ColumnsToRemove)
                            results.Columns.Remove(colName);
                    }




                    foreach (Zamba.Core.Index c in search.Indexs)
                    {
                        if (c != null)
                        {
                            searchResult.Filters filter = new searchResult.Filters();

                            filter.Name = c.Name;
                            if (c.dataDescription == null)
                            {
                                filter.Value = c.Data;
                            }
                            else
                            {
                                filter.Value = c.dataDescription.Length > 0 ? c.dataDescription : c.Data;
                            }
                            sr.filters.Add(filter);
                        }
                    }

                    if (search.OrderBy == string.Empty)
                    {
                        search.OrderBy = "DOC_ID desc";
                    }

                    try
                    {

                        if (searchDto.FiltersResetables)
                        {
                            ResetCountsOfResults(sr.entities);

                            //foreach (var item in sr.entities)
                            //{
                            //    item.ResultsCount = 0;
                            //}
                        }

                        if (search.ResultsCount != null && search.ResultsCount.Count > 0)
                        {
                            foreach (Int64 c in search.ResultsCount.Keys)
                            {
                                var e = sr.entities.Find(x => x.id == c);
                                if (e != null)
                                {
                                    e.ResultsCount = Int64.Parse(search.ResultsCount[c].ToString());
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }
                    //if (results.Rows.Count > 0)
                    //{
                    //    results.DefaultView.Sort = search.OrderBy; 
                    //}

                    //Se pregunta si el result tiene filas y si esta filtrando por columna
                    if (Zamba.Servers.Server.isOracle)
                    {
                        search.OrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                    }
                    else
                    {
                        search.OrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                    }
                    if ((search.OrderBy.Trim().Split(' ').Length > 0))
                    {
                        // esto resuelve los casos de caracteres especiales que no puede resolver naturalmente
                        var OrderBySinCaracteresEspeciales = SinCaracteresEspeciales(search.OrderBy.Split(' ')[0]);
                        var OrderByConCaracteresEspeciales = search.OrderBy.Split(' ')[0];
                        var criterioOrdenamiento = search.OrderBy.Split(' ')[1];
                        if (!results.Columns.Contains(OrderByConCaracteresEspeciales))
                        {
                            foreach (DataColumn Columna in results.Columns)
                            {
                                var ColumnaSinCaracteresEspeciales = SinCaracteresEspeciales(Columna.ColumnName);
                                if (ColumnaSinCaracteresEspeciales.ToLower() == OrderBySinCaracteresEspeciales.ToLower())
                                {
                                    search.OrderBy = Columna.ColumnName + " " + criterioOrdenamiento;
                                }
                            }
                        }
                    }
                    if (results.Rows.Count > 0 && searchDto.columnFiltering && OrderbyContainsColumn)
                    {
                        results.DefaultView.Sort = search.OrderBy;
                        results.AcceptChanges();
                        sr.data = results.DefaultView.ToTable();
                    }
                    else if (results.Rows.Count > 0 && OrderbyContainsColumn)
                    {
                        results.DefaultView.Sort = search.OrderBy;
                        results.AcceptChanges();
                        sr.data = results.DefaultView.ToTable();
                    }
                    else
                    {
                        results.AcceptChanges();
                        sr.data = results;
                    }
                    Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();

                    sr.VirtualEntities = zopt.GetValue("VirtualEntities");

                    sr.total = TotalCount;

                    if (search.View == null)
                        search.View = "";

                    try
                    {
                        Int64 viewCount = 0;
                        if (search.View != string.Empty && search.View.StartsWith("reportid"))
                        {
                            viewCount = TotalCount;
                        }
                        DataSet ViewList = new DataSet();
                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Realizando select a la zvw");
                            ViewList = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, "select * from zvw");
                        }
                        catch (Exception ex)
                        {
                            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en el select a la zvw");
                            ZClass.raiseerror(ex);
                        }

                        if (ViewList != null && ViewList.Tables.Count > 0)
                        {
                            foreach (DataRow v in ViewList.Tables[0].Rows)
                            {
                                if (new RightsBusiness().GetUserRights(search.UserId, ObjectTypes.Views, RightsType.View, Int64.Parse(v["ID"].ToString())))
                                {
                                    Boolean enabled = false;

                                    if (search.View == null)
                                        search.View = "";
                                    if (search.View != string.Empty && search.View.StartsWith("reportid"))
                                    {
                                        Int64 reportId = Int64.Parse(search.View.Replace("reportid", ""));
                                        if (reportId == Int64.Parse(v["RID"].ToString()))
                                        {
                                            enabled = true;
                                        }
                                    }

                                    //                                    Boolean.TryParse(v["ENABLED"].ToString(), out enabled);
                                    sr.views.Add(new ViewDto() { id = Int64.Parse(v["ID"].ToString()), reportId = Int64.Parse(v["RID"].ToString()), enabled = enabled, name = v["NAME"].ToString(), viewClass = v["CLASS"].ToString(), ResultsCount = viewCount });
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                    var newresults = JsonConvert.SerializeObject(sr, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return newresults;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw;
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetStepsCount")]
        [AllowAnonymous]
        public string GetStepsCount(string userId)
        {

            try
            {
                List<StepDTO> steps = new List<StepDTO>();
                WFStepBusiness wFStepBusiness = new WFStepBusiness();
                DataTable stepsTable = wFStepBusiness.GetWFsAndStepsAndCountByUser(Int64.Parse(userId));

                stepsTable.DefaultView.Sort = "wfname ASC";
                stepsTable = stepsTable.DefaultView.ToTable();

                foreach (DataRow row in stepsTable.Rows)
                {
                    steps.Add(new StepDTO
                    {
                        ID = long.Parse(row["wfstepid"].ToString()),
                        Name = row["wfstepName"].ToString(),
                        Count = int.Parse(row["cantidad"].ToString()),
                        WFID = long.Parse(row["wfid"].ToString()),
                        WFName = row["wfname"].ToString()
                    });
                }

                var newresults = JsonConvert.SerializeObject(steps, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return newresults;

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }

        }

        /// <summary>
        /// Exporta a un archivo Excel el resultado actual de la Grilla de resultados.
        /// </summary>
        /// <param name="searchDto">SearchDto</param>
        [Route("api/search/ExportToExcel")]
        [HttpPost, HttpGet]
        [OverrideAuthorization]
        public IHttpActionResult ExportToExcel(SearchDto searchDto)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                string NombreArchivo = "Zamba - Grilla de Resultados" + " - ";
                string FechaYHora = ArmDateTime_ToString();
                string Extencion = ".xlsx";

                string Formatocompleto_NombreArchivo = NombreArchivo + FechaYHora + Extencion;

                JsonDto dataGridResults = JsonConvert.DeserializeObject<JsonDto>(DoSearch(searchDto));
                searchDto.Lista_ColumnasFiltradas.ForEach(n => dataGridResults.data.Columns.Remove(n));

                //El siguiente codigo ejecuta un metodo de Spire que en realidad no pertenece al DLL, fue agregado manualmente.
                if (new SpireTools().ExportToXLSx(dataGridResults.data, MembershipHelper.AppTempDir("\\temp") + "\\" + Formatocompleto_NombreArchivo))
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicio la exportacion a Excel...");
                    SZOptBusiness zOptBusiness = new SZOptBusiness();
                    var rutaRelativa = MembershipHelper.AppTempDir("\\temp") + "\\" + Formatocompleto_NombreArchivo;
                    byte[] docBytes = File.ReadAllBytes(rutaRelativa);
                    MemoryStream ms = new MemoryStream();
                    ms.Write(docBytes, 0, docBytes.Length);
                    Stream stream = new FileStream(rutaRelativa, FileMode.Open, FileAccess.Read);
                    String ret = Convert.ToBase64String(docBytes);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Exportacion a Excel de la Grilla de Resultados Exitosa");
                    return Ok(ret);
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]: Fallo la exportacion a Excel de la Grilla de Resultados");
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                if (ex is HttpResponseException)
                    return InternalServerError();
            }
            return Ok("");
        }

        [Route("api/search/ExportToExcel_beta")]
        [HttpPost, HttpGet]
        public HttpResponseMessage ExportToExcel_beta(SearchDto searchDto)
        {
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);

            try
            {
                SpireTools SP = new SpireTools();
                string NombreArchivo = "Zamba - Grilla de Resultados" + " - ";
                string FechaYHora = ArmDateTime_ToString();
                string Extencion = ".xlsx";

                string Formatocompleto_NombreArchivo = NombreArchivo + FechaYHora + Extencion;

                JsonDto dataGridResults = JsonConvert.DeserializeObject<JsonDto>(DoSearch(searchDto));
                searchDto.Lista_ColumnasFiltradas.ForEach(n => dataGridResults.data.Columns.Remove(n));

                //El siguiente codigo ejecuta un metodo de Spire que en realidad no pertenece al DLL, fue agregado manualmente.
                if (SP.ExportToXLSx(dataGridResults.data, MembershipHelper.AppTempDir("\\temp") + "\\" + Formatocompleto_NombreArchivo))
                {
                    SZOptBusiness zOptBusiness = new SZOptBusiness();
                    var rutaRelativa = MembershipHelper.AppTempDir("\\temp") + "\\" + Formatocompleto_NombreArchivo;
                    result = new HttpResponseMessage(HttpStatusCode.OK);
                    var stream = new FileStream(rutaRelativa, FileMode.Open, FileAccess.Read);
                    result.Content = new StreamContent(stream);
                    //result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Exportacion a Excel de la Grilla de Resultados Exitosa");
                    return result;
                    //http://codeconcerns.com/downloading-a-file-using-web-api-with-jquery-or-knockout/
                }
                else
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "[ERROR]: Fallo la exportacion a Excel de la Grilla de Resultados");
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "[ERROR]:" + ex.Message);

                if (ex is HttpResponseException)
                    throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
            return result;
        }

        /// <summary>
        /// Obtiene un archivo en formato de tipo String.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("api/search/GetDocument")]
        public IHttpActionResult GetDocument(genericRequest request)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "GetDocument");

                string userId = request.Params["userId"].ToString();

                //AccountController AC = new AccountController();
                //AC.LoginById(Int64.Parse(userId));

                string doctypeId = request.Params["doctypeId"].ToString();
                string docId = request.Params["docid"].ToString();
                bool convertToPDf = true;
                bool includeAttachs = true;
                string iframeID = string.Empty;

                if (request.Params.ContainsKey("converttopdf"))
                    convertToPDf = bool.Parse(request.Params["converttopdf"].ToString());

                if (request.Params.ContainsKey("includeAttachs"))
                    includeAttachs = bool.Parse(request.Params["includeAttachs"].ToString());

                if (request.Params.ContainsKey("iframeID"))
                    iframeID = request.Params["iframeID"].ToString();


                SResult sResult = new SResult();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Result Data");
                Result res = (Result)sResult.GetResult(long.Parse(docId), long.Parse(doctypeId), true);

                DocumentData DD = new DocumentData();

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Doc Data");
                string newPDFFile = string.Empty;

                bool MsgViewerAux = bool.Parse(request.Params["viewer"].ToString().ToLower());

                string data = GetDocumentData(userId, doctypeId, docId, ref convertToPDf, res, MsgViewerAux, includeAttachs, ref newPDFFile);

                if (convertToPDf)
                {
                    DD.fileName = newPDFFile;
                }
                else
                {
                    string currentFileName = res.Name + Path.GetExtension(res.Doc_File);

                    foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                    {
                        currentFileName = currentFileName.Replace(c, ' ');
                    }
                    DD.fileName = currentFileName;
                }

                DD.ContentType = convertToPDf ? "application/pdf" : res.MimeType;



                try
                {
                    var thumbPath = newPDFFile.Replace(".pdf", ".bmp");
                    if (File.Exists(thumbPath))
                    {
                        DD.thumbImage = FileEncode.Encode(thumbPath);
                    }
                    else
                    {
                        var thumbimage = Zamba.Thumbs.Base64.ConvertFromPath(newPDFFile, thumbPath);
                        if (thumbimage != string.Empty)
                            DD.thumbImage = System.Convert.FromBase64String(thumbimage);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }

                try
                {

                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                    if (DD.ContentType == "audio/mpeg")
                    {
                        DD.data = System.Convert.FromBase64String(data);
                    }
                    else
                    {
                        DD.dataObject = JsonConvert.DeserializeObject<MsgData>(data);
                    }

                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                    DD.data = System.Convert.FromBase64String(data);
                }

                try
                {
                    DD.iframeID = iframeID;
                }
                catch (Exception)
                {
                }


                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo");
                    var jsonDD = JsonConvert.SerializeObject(DD);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json");
                    return Ok(jsonDD);
                }
                catch (Exception ex)
                {
                    //return Ok(System.Convert.FromBase64String(DD));
                    //throw ex;
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo With EX");
                    var jsonDD = JsonConvert.SerializeObject(DD);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json with EX");
                    return Ok(jsonDD);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el recurso")));
            }
        }

        /// <summary>
        /// Obtiene un archivo en formato de tipo String.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("api/search/getPermitsForIndexPanel")]
        public IHttpActionResult GetPermitsForIndexPanel(genericRequest request)
        {
            try
            {
                UserPreferences UP = new UserPreferences();
                if (Boolean.Parse(UP.getValue("ShowIndexLinkUnderTask", UPSections.UserPreferences, "False")))
                {
                    return Ok(true);
                }
                else
                {

                    return Ok(false);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el recurso")));
            }
        }




        /// <summary>
        /// Obtiene un archivo en formato de tipo String.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("api/search/GetThumb")]
        public IHttpActionResult GetThumb(genericRequest request)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "GetThumb");

                string userId = request.Params["userId"].ToString();

                //AccountController AC = new AccountController();
                //AC.LoginById(Int64.Parse(userId));

                string doctypeId = request.Params["doctypeId"].ToString();
                string docId = request.Params["docid"].ToString();
                bool convertToPDf = true;
                bool includeAttachs = true;

                if (request.Params.ContainsKey("converttopdf"))
                    convertToPDf = bool.Parse(request.Params["converttopdf"].ToString());

                if (request.Params.ContainsKey("includeAttachs"))
                    includeAttachs = bool.Parse(request.Params["includeAttachs"].ToString());

                SResult sResult = new SResult();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Result Data");
                Result res = (Result)sResult.GetResult(long.Parse(docId), long.Parse(doctypeId), true);

                DocumentData DD = new DocumentData();

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Doc Data");
                string newPDFFile = string.Empty;

                string data = GetDocumentData(userId, doctypeId, docId, ref convertToPDf, res, true, includeAttachs, ref newPDFFile);
                DD.fileName = convertToPDf ? newPDFFile : res.Doc_File;

                DD.ContentType = convertToPDf ? "application/pdf" : res.MimeType;

                try
                {
                    var thumbPath = newPDFFile.Replace(".pdf", ".bmp");
                    if (File.Exists(thumbPath))
                    {
                        DD.thumbImage = FileEncode.Encode(thumbPath);
                    }
                    else
                    {
                        var thumbimage = Zamba.Thumbs.Base64.ConvertFromPath(newPDFFile, thumbPath);
                        if (thumbimage != string.Empty)
                            DD.thumbImage = System.Convert.FromBase64String(thumbimage);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }


                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                    DD.dataObject = JsonConvert.DeserializeObject<MsgData>(data);
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToBase64 Archivo");
                    DD.data = System.Convert.FromBase64String(data);
                }

                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo");
                    var jsonDD = JsonConvert.SerializeObject(DD);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json");
                    return Ok(jsonDD);
                }
                catch (Exception ex)
                {
                    //return Ok(System.Convert.FromBase64String(DD));
                    //throw ex;
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ToJson Archivo With EX");
                    var jsonDD = JsonConvert.SerializeObject(DD);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "return Json with EX");
                    return Ok(jsonDD);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el recurso")));
            }
        }




        /// <summary>
        /// Obtiene el archivo asociado a la tarea a traves de un token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("api/search/GetDocFile")]
        public IHttpActionResult GetDocFile(genericRequest request)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo GetDocFile");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, request.Params["externuserid"].ToString());
                //ZTrace.WriteLineIf(ZTrace.IsVerbose, Request.Headers.GetValues("Authorization").First() != null ? Request.Headers.GetValues("Authorization").First() : string.Empty);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, request.Params["id"] != null ? request.Params["id"].ToString() : string.Empty);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            }

            string token;
            try
            {
                //token = Request.Headers.GetValues("Authorization").First();
                //if (string.IsNullOrEmpty(token))
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token Nulo")));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el token")));
            }

            byte[] _file = null;
            try
            {

                string _userId = MembershipHelper.CurrentUser.ID.ToString();  //DecryptString(request.Params["externuserid"].ToString());
                                                                              // if (String.IsNullOrEmpty(_userId))
                                                                              //     return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

                //long userID = long.Parse(_userId);

                //if (!validateToken(userID, token))
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
                UserBusiness UB = new UserBusiness();
                //IUser User = UB.ValidateLogIn(userID, ClientType.WebApi);
                //if (User != null)
                //{

                string _documentId = DecryptString(request.Params["id"].ToString());

                if (string.IsNullOrEmpty(_documentId))
                    throw new Exception("No se reconoce ID del documento");

                string _doctypeId = _documentId.Split('-')[1];
                string _docId = _documentId.Split('-')[0];
                bool convertToPDf = true;
                bool includeAttachs = true;

                if (request.Params.ContainsKey("converttopdf"))
                    convertToPDf = bool.Parse(request.Params["converttopdf"].ToString());

                if (request.Params.ContainsKey("includeAttachs"))
                    includeAttachs = bool.Parse(request.Params["includeAttachs"].ToString());


                SResult sResult = new SResult();
                Result res = (Result)sResult.GetResult(long.Parse(_docId), long.Parse(_doctypeId), false);

                //string documentdata = GetDocumentData(_userId, _doctypeId, _docId, ref convertToPDf, res);
                try
                {
                    //JsonConvert.DeserializeObject(documentdata);
                    string newPDFFile = string.Empty;
                    return Ok(GetDocumentData(_userId, _doctypeId, _docId, ref convertToPDf, res, true, includeAttachs, ref newPDFFile));
                    //return Ok(documentdata);
                }
                catch (Exception ex)
                {
                    string newPDFFile = string.Empty;
                    return Ok(System.Convert.FromBase64String(GetDocumentData(_userId, _doctypeId, _docId, ref convertToPDf, res, false, includeAttachs, ref newPDFFile)));
                    throw ex;
                }
                //}
                //return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
            }
            finally
            {
                if (_file != null)
                    _file = null;
            }
        }

        private string DecryptString(string value)
        {
            try
            {
                Zamba.Tools.Encryption encript = new Zamba.Tools.Encryption();
                return encript.DecryptNewStringNonShared(value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene el archivo asociado a la tarea.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="doctypeId"></param>
        /// <param name="docId"></param>
        /// <param name="convertToPDf"></param>
        /// <returns>Devuelve un JSON o Bytes[] como String</returns>
        private string GetDocumentData(string userId, string doctypeId, string docId, ref bool convertToPDf, Result res, bool MsgPreview, bool includeAttachs, ref string newPDFFile)
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            SResult sResult = new SResult();

            long DocTypeId, DocId, userID;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(doctypeId) && !string.IsNullOrEmpty(docId))
            {
                DocTypeId = long.Parse(doctypeId);
                DocId = long.Parse(docId);
                userID = long.Parse(userId);

                try
                {
                    if (MembershipHelper.CurrentUser == null)
                    {
                        UserBusiness UB = new UserBusiness();
                        UB.ValidateLogIn(userID, ClientType.Web);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                }
            }
            else
            {
                ZClass.raiseerror(new Exception("No se recibieron todos los parametros"));
                throw new Exception("No se recibieron todos los parametros");
            }



            byte[] _file = null;
            if (res != null && res.FullPath != null && res.FullPath.Contains("."))
            {
                bool IsBlob = false;

                string filename = res.FullPath;
                if (newPDFFile == string.Empty)
                {
                    if (!res.FullPath.EndsWith(".pdf"))
                        newPDFFile = res.FullPath + ".pdf";
                    else
                        newPDFFile = res.FullPath;

                }

                if (convertToPDf && File.Exists(newPDFFile) && res.IsMsg == false && res.IsOffice2 == false)
                {
                    _file = FileEncode.Encode(newPDFFile);
                    filename = newPDFFile;
                }

                if (_file == null || _file.Length == 0)
                {
                    if (convertToPDf == false)
                        filename = res.FullPath;

                    if (res.IsMsg)
                    {
                        filename = res.FullPath;
                        if (MsgPreview)
                        {
                            Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo MSG");
                            Zamba.FileTools.MailPreview o = (Zamba.FileTools.MailPreview )ST.ConvertMSGToJSON(res.FullPath, newPDFFile, includeAttachs);
                            o.body =  o.body
                                .Replace("Ã±", "ñ")
                                .Replace("Ã¡", "á")
                                .Replace("Ã©", "é")                                
                                .Replace("Ã³", "ó")
                                .Replace("Ãº", "ú")
                                .Replace("â€œ","\"")
                                .Replace("â€", "\"")
                                .Replace("Ã‘","Ñ")
                                .Replace("Â°","º")
                                .Replace("Ã", "í")
                                 
                                ;
                            var a = JsonConvert.SerializeObject(o, Formatting.Indented,
                              new JsonSerializerSettings
                              {
                                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
                              });
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Retornando Archivo");
                            return a;
                        }
                    }

                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 && (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase || (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))
                    {
                        sResult.LoadFileFromDB(res);
                    }
                    //Verifica si el result contiene el documento guardado
                    if (res.EncodedFile != null)
                    {
                        _file = res.EncodedFile;
                    }
                    else
                    {
                        string sUseWebService = Zopt.GetValue("UseWebService");
                        //Verifica si debe utilizar el webservice para obtener el documento
                        if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                            _file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, userID);
                        else
                            _file = sResult.GetFileFromResultForWeb(res, out IsBlob);
                    }

                    if (_file != null && _file.Length > 0)
                    {
                        if (res.IsWord)
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }

                        if ((res.IsHtml || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertHTMLToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }

                        if ((res.IsImage) && res.IsTif == false || res.FullPath.Contains(".jpeg"))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                    convertToPDf = false;
                                }
                            }
                        }

                        if ((res.IsTif))
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                            String TempDir = ZOptBusiness.GetValueOrDefault("PdfsDirectory", Path.Combine(Zamba.Membership.MembershipHelper.AppTempPath, "Pdfs"));
                            TempDir = Path.Combine(TempDir, res.DocTypeId.ToString());

                            try
                            {
                                if (Directory.Exists(TempDir) == false)
                                    Directory.CreateDirectory(TempDir);
                            }
                            catch (Exception)
                            {
                                TempDir = Path.Combine(MembershipHelper.AppTempPath, "Pdfs", res.DocTypeId.ToString());
                            }

                            if (convertToPDf == false)
                            {
                                if (res.RealFullPath().Contains("__.__"))
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                    FileEncode.Decode(tempPDFFile, _file);
                                    _file = FileEncode.Encode(tempPDFFile);
                                    filename = res.FullPath;
                                }
                            }
                            else
                            {
                                if (res.RealFullPath().Contains("__.__"))
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    newPDFFile = Path.Combine(TempDir, Path.GetFileName(res.RealFullPath())) + ".pdf";
                                    if (File.Exists(newPDFFile) == false)
                                    {
                                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                        FileEncode.Decode(tempPDFFile, _file);
                                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, _file);
                                            _file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
                                            convertToPDf = false;
                                        }

                                    }
                                    else
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                }
                                else
                                {
                                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                    if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else { filename = res.FullPath; convertToPDf = false; }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (convertToPDf)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo");
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; convertToPDf = false; }
                            }
                        }
                    }
                }

                if (_file == null || _file.Length <= 0)
                {
                    throw new Exception("No se pudo obtener el recurso File nulo");
                }

                Zopt = null;
                sResult = null;

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convert To Base64");
                return System.Convert.ToBase64String(_file);
            }
            throw new Exception("No se pudo obtener el recurso");
        }
















        /// <summary>
        /// Prepara una cadena de caracteres con Fecha y Hora con el formato DDMMYYYY_HHMMSS. Destinado al nombramiento de archivos.
        /// </summary>
        /// <returns></returns>
        private string ArmDateTime_ToString()
        {
            return DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_" +
                DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        }

        [Route("api/search/DoSearch")]
        [OverrideAuthorization]
        [HttpPost]
        [HttpGet]
        public string FillWF(long userId)
        {
            try
            {
                var user = GetUser(userId);

                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser))).ToString();


                //Se cambia el tipo a la variable WorkFlows para no tener que realizar el ".ToArray".
                List<EntityView> WorkFlows;

                SWorkflow SWorkflow = new SWorkflow();

                WorkFlows = SWorkflow.GetUserWFIdsAndNamesWithSteps(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                if (WorkFlows != null && WorkFlows.Count > 0)
                {
                    //Se comenta esta linea ya que no es usada en otro lado.
                    //ArrayList StepsOfRestrictedDoctypes = Zamba.Core.WFBusiness.GetStepsByUserRestrictedDoctypes(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                    //Se agrega el count para cambiar el "foreach" a "for".
                    int countWF = WorkFlows.Count;

                    for (int i = 0; i < countWF; i++)
                    {
                        //Se agrega el count para cambiar el "foreach" a "for".
                        int countCE = WorkFlows[i].ChildsEntities.Count;

                        EntityView step;

                        for (int j = 0; j < countCE; j++)
                        {
                            try
                            {
                                //Se hace una copia del step para acceder con mayor velocidad.
                                step = WorkFlows[i].ChildsEntities[j];
                            }
                            catch (Exception exStep)
                            {
                                Zamba.AppBlock.ZException.Log(exStep);
                            }
                        }
                    }
                }
                var newresults = JsonConvert.SerializeObject(WorkFlows, Formatting.Indented,
                                   new JsonSerializerSettings
                                   {
                                       PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                   });

                return newresults;
            }
            catch (Exception ex)
            {
                Zamba.AppBlock.ZException.Log(ex);
                return ex.ToString();
            }
        }

        [OverrideAuthorization]
        [Route("api/search/FillIndex")]
        [HttpPost]
        public string FillIndex(string IndexId)
        {

            try
            {
                string newresults = string.Empty;
                if (Int64.Parse(IndexId) >= 0)
                {
                    IndexsBusiness IndexB = new IndexsBusiness();

                    var Indexs = IndexB.GetIndex(Int64.Parse(IndexId));
                    newresults = JsonConvert.SerializeObject(Indexs, Formatting.Indented,
                   new JsonSerializerSettings
                   {
                       PreserveReferencesHandling = PreserveReferencesHandling.Objects
                   });
                    IndexB = null;
                }
                else
                {
                    IndexsBusiness IndexB = new IndexsBusiness();
                    string indexName = GridColumns.VisibleColumns.FirstOrDefault(x => x.Value == long.Parse(IndexId)).Key;
                    var Index = IndexB.GenerateDummyIndex(long.Parse(IndexId),
                        indexName,
                        GridColumns.ZambaColumnsType[indexName]);
                    newresults = JsonConvert.SerializeObject(Index,
                        Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            PreserveReferencesHandling = PreserveReferencesHandling.Objects
                        });
                    IndexB = null;
                }


                return newresults;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }
            finally
            {
            }
        }


        [OverrideAuthorization]
        [Route("api/search/GetIndexWords")]
        public List<Word> GetIndexWords(Int64 userId, int entity, int index)
        {
            RightsSchema rightsSchema = new RightsSchema();
            try
            {
                return rightsSchema.GetIndexWords(userId, entity, index);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;

            }
            finally
            {
                rightsSchema = null;
            }

        }
        [OverrideAuthorization]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getResultsDoShowTable")]
        public IHttpActionResult getResultsDoShowTable(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                //var user = GetUser(paramRequest.UserId);
                //if (user == null)
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                //        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Results_Business RB = new Results_Business();
                    DataTable result = RB.GetSelectDoShowTable();

                    var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                   new JsonSerializerSettings
                   {
                       PreserveReferencesHandling = PreserveReferencesHandling.Objects
                   });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [OverrideAuthorization]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getInsertDoShowTable")]
        public IHttpActionResult getInsertDoShowTable(genericRequest paramRequest)
        {
            try
            {
                //En servicio $("#zamba_index_139601").val();

                if (paramRequest != null)
                {
                    //160587
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));

                    List<string> ListaGuias = new List<string>();

                    string List = paramRequest.Params["TableList"];
                    ListaGuias = List.Split(',').ToList();

                    //  Obtenes una lista de los NroGuia-------



                    Int64 NroHojaRuta = Int64.Parse(paramRequest.Params["NroHojaRuta"]);
                    Int64 TaskId = Int64.Parse(paramRequest.Params["TaskId"]);
                    Int64 ruleId = Int64.Parse(paramRequest.Params["RuleId"]);
                    //  Int64 ruleId = 175975;

                    WFTaskBusiness WTB = new WFTaskBusiness();

                    ITaskResult Task = WTB.GetTask(TaskId, user.ID);
                    if (Task != null)
                    {
                        if (!VariablesInterReglas.ContainsKey("NroHojaRuta"))
                            VariablesInterReglas.Add("NroHojaRuta", NroHojaRuta);
                        else
                            VariablesInterReglas.set_Item("NroHojaRuta", NroHojaRuta);

                        List<ITaskResult> results = new List<ITaskResult>();
                        results.Add(Task);

                        foreach (string NroGuia in ListaGuias)
                        {
                            if (!VariablesInterReglas.ContainsKey("NroGuia"))
                                VariablesInterReglas.Add("NroGuia", NroGuia);
                            else
                                VariablesInterReglas.set_Item("NroGuia", NroGuia);


                            try
                            {
                                Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
                                Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
                                List<Int64> ExecutedIDs = new List<Int64>();
                                List<Int64> PendingChildRules = new List<Int64>();
                                Hashtable Params = new Hashtable();
                                bool RefreshRule = false;
                                WFExecution wFExecution = new WFExecution(MembershipHelper.CurrentUser);

                                wFExecution._haceralgoEvent += HandleWFExecutionPendingEvents;

                                wFExecution.ExecuteRule(ruleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, new List<Int64>(), true);

                            }
                            catch (Exception ex)
                            {
                                Zamba.AppBlock.ZException.Log(ex);

                            }

                        }

                        return Ok();
                    }
                    else
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                 new HttpError(StringHelper.TaskIdExpected)));

                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
         new HttpError(StringHelper.InvalidParameter)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
           new HttpError(StringHelper.InvalidParameter)));
            }
        }

        public void HandleWFExecutionPendingEvents(long RuleId, ref List<ITaskResult> results,
         ref RulePendingEvents PendigEvent, ref RuleExecutionResult ExecutionResult,
         ref List<Int64> ExecutedIDs, ref Hashtable Params, ref List<Int64> PendingChildRules,
         ref Boolean RefreshRule, List<long> TaskIdsToRefresh, Boolean IsAsync)
        {
            return;
        }

        [OverrideAuthorization]
        [Route("api/search/Tree")]
        public IHttpActionResult GetTree(int currentuserid)
        {
            Zamba.Core.UserPreferences US = new Zamba.Core.UserPreferences();
            try
            {
                var lastselectednodeid = US.getEspecificUserValue("WebSearchLastNodes", UPSections.Viewer, string.Empty, currentuserid);
                var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, string.Format("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON select " + Zamba.Servers.Server.DBUser + ".qfn_XmlToJson((select tree from " + Zamba.Servers.Server.DBUser + ".zfn_search_100_gettree({0},'{1}')))", currentuserid, lastselectednodeid));
                return Ok(JsonTree.ToString());

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
            }

            finally
            {
                US = null;
            }
        }


        const string _URLFORMAT = "/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&userid={2},token={3}";

        //[OverrideAuthorization]
        [Route("api/search/GetFileUrl")]
        [HttpPost]
        public string GetFileUrl(ResultDto resultDto)
        {
            try
            {
                String token = new Zamba.Core.ZssFactory().GetZss(Zamba.Membership.MembershipHelper.CurrentUser).TokenQueryString;
                var url = string.Format(_URLFORMAT, resultDto.DOC_TYPE_ID, resultDto.DOC_ID, resultDto.UserId, token);
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

        [Route("api/search/GetIndexData")]
        //[OverrideAuthorization]
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


        [Route("api/search/GetIndexDataSelect")]
        [HttpPost]
        public IHttpActionResult GetIndexDataSelect(genericRequest paramRequest)
        {

            Zamba.Core.UserPreferences UP;
            try
            {
                if (paramRequest != null)
                {

                    Int64 id = Int64.Parse(paramRequest.Params["id"]);

                    UP = new Zamba.Core.UserPreferences();
                    var OrderByDescripcion = Boolean.Parse(UP.getValue("OrderListsByDescription", UPSections.UserPreferences, "True", MembershipHelper.CurrentUser.ID));
                    DataTable dt = new DataTable();
                    WFTaskBusiness WTB = new WFTaskBusiness();
                    dt = WTB.SelectResult(id);

                    var newresults = JsonConvert.SerializeObject(dt, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(newresults);
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
        }
        [OverrideAuthorization]
        [Route("api/search/GetIndexDataSelectDinamic")]
        [HttpPost]
        public IHttpActionResult GetIndexDataSelectDinamic(genericRequest paramRequest)
        {

            Zamba.Core.UserPreferences UP;
            try
            {
                if (paramRequest != null)
                {
                    Int64 id = Int64.Parse(paramRequest.Params["id"]);
                    string inputValue = paramRequest.Params["inputValue"];

                    UP = new Zamba.Core.UserPreferences();
                    var OrderByDescripcion = Boolean.Parse(UP.getValue("OrderListsByDescription", UPSections.UserPreferences, "True", MembershipHelper.CurrentUser.ID));
                    //DataTable dt = new DataTable();
                    WFTaskBusiness WTB = new WFTaskBusiness();
                    List<WFTaskBusiness.CodigoDescripcion> list;
                    if (paramRequest.Params.ContainsKey("LimitTo"))
                    {
                        Int64 LimitTo = System.Convert.ToInt64(paramRequest.Params["LimitTo"]);
                        list = WTB.SelectResultDinamicWithLimit(id, inputValue, LimitTo);
                    }
                    else
                    {
                        list = WTB.SelectResultDinamic(id, inputValue);
                    }
                    var newresults = JsonConvert.SerializeObject(list, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(newresults);
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
        }



        [Route("api/search/GetFile")]
        //[OverrideAuthorization]
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

        [Route("api/search/SetLastNodes")]
        [isGenericRequest]
        [HttpPost]
        public IHttpActionResult SetLastNodes(genericRequest lastNodeObj)

        {
            Zamba.Core.UserPreferences UP;
            try
            {
                var user = GetUser(lastNodeObj.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));


                string LastNodes = lastNodeObj.Params["LastNodes"];

                UP = new Zamba.Core.UserPreferences();
                UP.setEspecificUserValue("WebSearchLastNodes", LastNodes, UPSections.Viewer, user.ID);
                var newresults = JsonConvert.SerializeObject("OK", Formatting.Indented,
                new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                return Ok(newresults);
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

        [Route("api/search/GetLastNodes")]
        [OverrideAuthorization]
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

        [Route("api/search/GetMyUnreadTasksCount")]
        [OverrideAuthorization]
        [HttpGet]
        public long GetMyUnreadTasksCount(long currentUserId)
        {
            try
            {
                //WFTaskBusiness WFTB = new WFTaskBusiness();
                //return WFTB.GetUnreadTasksCountByUserID(currentUserId);
                return 0;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return 0;
            }
        }

        [Route("api/search/GetGroupsByUserIds")]
        [OverrideAuthorization]
        [HttpGet]
        public List<long> GetGroupsByUserIds(long usrID)
        {
            try
            {
                IUser usr = GetUser(usrID);
                List<long> groups = new List<long>();
                foreach (IUserGroup gr in usr.Groups)
                    groups.Add(gr.ID);

                return groups;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return new List<long>();
            }
        }

        [OverrideAuthorization]
        [HttpPost]
        [Route("api/Search/NotifyDocumentRead")]

        public IHttpActionResult NotifyDocumentRead(long UserId, long DocTypeId, long DocId)
        {
            try
            {
                WFTaskBusiness WFTB = new WFTaskBusiness();
                WFTB.SetDocumentRead(UserId, DocTypeId, DocId);
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        private bool IsNumeric(string value)
        {
            decimal testDe;
            double testDo;
            int testI;

            if (decimal.TryParse(value, out testDe) || double.TryParse(value, out testDo) || int.TryParse(value, out testI))
                return true;

            return false;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getNewId")]
        [OverrideAuthorization]
        public IHttpActionResult getNewId(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 newId = 0;
                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("idType"))
                    {
                        Zamba.Core.IdTypes idType;
                        idType = (Zamba.Core.IdTypes)Enum.Parse(typeof(Zamba.Core.IdTypes), paramRequest.Params["idType"].ToString());
                        newId = CoreData.GetNewID(idType);
                    }

                    return Ok(newId);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getAddComentarios")]
        [OverrideAuthorization]
        [isGenericRequest]
        public IHttpActionResult getResultsComentarios(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 indexId = 0;
                    Int64 parentResultId = 0;
                    Int64 entityId = 0;
                    string Evaluation = string.Empty;
                    string InputObservacion = validateParam("InputObservacion", paramRequest);
                    string TextareaObservacion = validateParam("TextareaObservacion", paramRequest);
                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        indexId = Int64.Parse(paramRequest.Params["indexId"]);
                        parentResultId = Int64.Parse(paramRequest.Params["parentResultId"]);
                        entityId = Int64.Parse(paramRequest.Params["entityId"]);
                        Evaluation = paramRequest.Params["bool"].ToString();
                        if (InputObservacion != "")
                        {
                            if (TextareaObservacion != "")
                            {
                                InputObservacion = TextareaObservacion + "\n" + DateTime.Now.ToString("dd/MM/yyyy") + " - " + user.Name + " - " + InputObservacion;
                            }
                            else
                            {
                                InputObservacion = DateTime.Now.ToString("dd/MM/yyyy") + " - " + user.Name + " - " + InputObservacion;
                            }
                        }
                        if (TextareaObservacion != "" && InputObservacion == "")
                        {
                            Evaluation = false.ToString();
                        }

                    }

                    Results_Business RB = new Results_Business();
                    DataTable result = RB.GetIndexObservaciones(indexId, entityId, parentResultId, InputObservacion, Evaluation);

                    if (Evaluation == "true")
                    {
                        try
                        {
                            Int64 newsID = CoreBusiness.GetNewID(IdTypes.News);
                            NewsBusiness NB = new NewsBusiness();
                            NB.SaveNews(newsID, parentResultId, entityId, "Comento: " + InputObservacion, user.ID, string.Empty);

                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                        }
                    }

                    var newresults = string.Empty;
                    if (result != null && result.Rows.Count > 0)
                    {
                        newresults = result.Rows[0][0].ToString();
                    }
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetResultsByAllReport")]
        [OverrideAuthorization]
        public string GetResultsByAllReport(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    throw new Exception(StringHelper.InvalidUser);
                try
                {

                    Results_Business RB = new Results_Business();
                    DataSet result = RB.AllReport(paramRequest.UserId);

                    var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
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
            }
            return null;

        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]

        [Route("api/search/GetResultsByReportId")]
        [OverrideAuthorization]
        public IHttpActionResult GetResultsByReportId(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int32 ReportID = 0;
                    string FormVariables = null;
                    Int64 TaskId = 0;
                    Hashtable dicFormVariables = new Hashtable();

                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        ReportID = Int32.Parse(paramRequest.Params["ReportID"]);


                        if (paramRequest.Params.ContainsKey("FormVariables"))
                        {
                            FormVariables = paramRequest.Params["FormVariables"];
                            //                             TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                            //tomo los valores asignados desde el form en js
                            /// Se convierte el valor en un diccionario para poder iterarlo

                            List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                            if (listFormVariables == null)
                            {
                                listFormVariables = new List<itemVars>();
                            }

                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                        }
                    }



                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    var newresultss = RB.EvaluationRunWebQueryBuilder(ReportID, true, dicFormVariables, null);
                    DataTable dtAsoc = newresultss.Tables[0];

                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-dd",
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetResultsByReportIdDoShowTable")]
        [OverrideAuthorization]
        public string GetResultsByReportIdDoShowTable(genericRequest paramRequest)
        {
            cleanRuleVariables_ByConvention();

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    throw new Exception(StringHelper.InvalidUser);
                try
                {
                    Int32 ReportID = 0;
                    string FormVariables = null;
                    Int64 TaskId = 0;
                    Hashtable dicFormVariables = new Hashtable();


                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        ReportID = Int32.Parse(paramRequest.Params["ReportID"]);


                        if (paramRequest.Params.ContainsKey("FormVariables"))
                        {
                            FormVariables = paramRequest.Params["FormVariables"];
                            if (!string.IsNullOrEmpty(FormVariables))
                            {
                                // TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                                //tomo los valores asignados desde el form en js
                                /// Se convierte el valor en un diccionario para poder iterarlo

                                List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                                for (int i = 0; i < listFormVariables.Count; i++)
                                {
                                    dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                                }
                            }

                        }
                    }



                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    var newresultss = RB.EvaluationRunWebQueryBuilder(ReportID, true, dicFormVariables, null);
                    DataTable dtAsoc = newresultss.Tables[0];

                    var ObjectResult = new { data = dtAsoc };

                    var newresults = JsonConvert.SerializeObject(ObjectResult, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-dd",
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });


                    return newresults;

                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetResultsByReportIdDoShowTablePro")]
        [OverrideAuthorization]
        public IHttpActionResult GetResultsByReportIdDoShowTablePro(genericRequest paramRequest)
        {
            cleanRuleVariables_ByConvention();

            string DataSourceVariable = string.Empty;

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    throw new Exception(StringHelper.InvalidUser);
                try
                {
                    Int32 ReportID = 0;
                    //string FormVariables = null;
                    //Int64 TaskId = 0;
                    //Hashtable dicFormVariables = new Hashtable();
                    DataTable a = new DataTable();
                    Int64 OriginRuleId = Int64.Parse(paramRequest.Params["originId"]);
                    DataSourceVariable = paramRequest.Params["datasourceVariable"];
                    string FormVariables = paramRequest.Params["FormVariables"];
                    Int64 TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                    //string SelectedresultList = paramRequest.Params["SelectedresultList"];

                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        //Int64 OriginRuleId = Int64.Parse(paramRequest.Params["OriginRuleId"]);
                        //string OriginTableVariable = paramRequest.Params["OriginTableVariable"];

                        //string FormVariables = paramRequest.Params["FormVariables"];
                        //Int64 TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                        //string SelectedresultList = paramRequest.Params["SelectedresultList"];
                        WFTaskBusiness WTB = new WFTaskBusiness();

                        ITaskResult Task = WTB.GetTask(TaskId, user.ID);
                        if (Task != null)
                        {
                            List<ITaskResult> results = new List<ITaskResult>();
                            results.Add(Task);

                            //tomo los valores asignados desde el form en js
                            /// Se convierte el valor en un diccionario para poder iterarlo
                            Dictionary<string, string> dicFormVariables = new Dictionary<string, string>();
                            List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                            //Se itera el diccionario de los valores

                            foreach (var itemlist in dicFormVariables) //VARS
                            {
                                if (!VariablesInterReglas.ContainsKey(itemlist.Key))
                                    VariablesInterReglas.Add(itemlist.Key, itemlist.Value);
                                else
                                    VariablesInterReglas.set_Item(itemlist.Key, itemlist.Value);
                            }


                            //cLEANeXECUTIONvARS
                            //List accion,error,msg

                            //    for
                            //    //Limpiar Variables de Ejecucion por Convencion
                            //if (!VariablesInterReglas.ContainsKey("msg"))
                            //    VariablesInterReglas.Add("msg", "");
                            //else
                            //    VariablesInterReglas.set_Item("msg", "");

                            //if (!VariablesInterReglas.ContainsKey("error"))
                            //    VariablesInterReglas.Add("error", "");
                            //else
                            //    VariablesInterReglas.set_Item("error", "");

                            //if (!VariablesInterReglas.ContainsKey("accion"))
                            //    VariablesInterReglas.Add("accion", "");
                            //else
                            //    VariablesInterReglas.set_Item("accion", "");




                            Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
                            Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
                            List<Int64> ExecutedIDs = new List<Int64>();
                            List<Int64> PendingChildRules = new List<Int64>();
                            Hashtable Params = new Hashtable();
                            bool RefreshRule = false;
                            WFExecution wFExecution = new WFExecution(MembershipHelper.CurrentUser);

                            wFExecution._haceralgoEvent += HandleWFExecutionPendingEvents;

                            wFExecution.ExecuteRule(OriginRuleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, new List<Int64>(), true);

                            DataTable dtAsoc = new DataTable();

                            if (VariablesInterReglas.ContainsKey(DataSourceVariable))
                            {
                                if (VariablesInterReglas.get_Item(DataSourceVariable) is string)
                                {
                                    DataTable DT = new DataTable();
                                    DT.Columns.Add(DataSourceVariable);
                                    DT.Rows.Add(VariablesInterReglas.get_Item(DataSourceVariable).ToString());

                                    a = DT;
                                }
                                else
                                {
                                    a = ((DataSet)VariablesInterReglas.get_Item(DataSourceVariable)).Tables[0];
                                }
                            }

                            var dataJsonResult = JsonConvert.SerializeObject(a,
                                Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    DateFormatString = "yyyy-MM-dd",
                                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                                });

                            //OriginResultDTO OResult = new OriginResultDTO{
                            //    data = dataJsonResult,
                            //    vars = VariablesInterreglesToJson(VariablesInterReglas.Keys())
                            //};

                            OriginResultDTO OResult = new OriginResultDTO
                            {
                                data = dataJsonResult,
                                vars = VariablesInterreglesToJson(VariablesInterReglas.Keys())
                            };

                            try
                            {
                                return Ok(OResult);
                                //return Ok(a);
                            }
                            catch (Exception ex)
                            {
                                ZClass.raiseerror(ex);
                                throw new Exception(StringHelper.InvalidParameter);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;
        }

        /// <summary>
        /// Elimina las variables interreglas definidas por convencion en el enumerador "EVariablesInterReglas_Convencion".
        /// </summary>
        private void cleanRuleVariables_ByConvention()
        {
            foreach (string item in Enum.GetNames(typeof(EVariablesInterReglas_Convencion)))
            {
                if (VariablesInterReglas.ContainsKey(item))
                    VariablesInterReglas.Remove(item);
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetResultsByReportIdDash")]
        [OverrideAuthorization]
        public string GetResultsByReportIdDash(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                //var user = GetUser(paramRequest.UserId);
                //if (user == null)
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                //        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int32 ReportID = 0;
                    string FormVariables = null;
                    Int64 TaskId = 0;
                    string VarX = null;
                    string VarY = null;
                    Hashtable dicFormVariables = new Hashtable();

                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        ReportID = Int32.Parse(paramRequest.Params["ReportID"]);
                        VarX = paramRequest.Params["VarX"].ToString();
                        VarY = paramRequest.Params["VarY"].ToString();

                        if (paramRequest.Params.ContainsKey("FormVariables"))
                        {
                            FormVariables = paramRequest.Params["FormVariables"];
                            //                             TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                            //tomo los valores asignados desde el form en js
                            /// Se convierte el valor en un diccionario para poder iterarlo

                            List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                        }
                    }


                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    var newresultss = RB.EvaluationRunWebQueryBuilder(ReportID, true, dicFormVariables, null);
                    DataTable dtAsoc = newresultss.Tables[0];

                    List<ReportDto> RP = new List<ReportDto>();
                    //DataTable dt = new DataTable();
                    foreach (DataRow r in dtAsoc.Rows)
                    {
                        ReportDto dto = new ReportDto();
                        if (r.Table.Columns.Contains(VarX))
                        {
                            dto.varx = r[VarX].ToString();
                        }
                        if (r.Table.Columns.Contains(VarY))
                        {
                            dto.vary = r[VarY].ToString();
                        }


                        RP.Add(dto);
                    }

                    var newresults = JsonConvert.SerializeObject(RP, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        DateFormatString = "yyyy-MM-dd",
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return newresults;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;
        }




        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/GetExportExcel")]
        [OverrideAuthorization]
        public string GetExportExcel(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {

                try
                {

                    Int32 ReportID = 0;
                    string PrintTo = string.Empty;
                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        ReportID = Int32.Parse(paramRequest.Params["ReportID"]);
                        PrintTo = paramRequest.Params["Print"].ToString();

                    }
                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    var newresultss = RB.EvaluationRunWebQueryBuilder(ReportID, true, null, null);
                    DataTable dtAsoc = newresultss.Tables[0];

                    if (PrintTo == "Excel")
                    {
                        Zamba.FileTools.SpireTools Ex = new Zamba.FileTools.SpireTools();
                        //string RutaArchivo = @"C:\temp";

                        Ex.ExportToXLSTable(dtAsoc);
                    }

                    if (PrintTo == "Pdf")
                    {
                        Zamba.FileTools.SpireTools Ex = new Zamba.FileTools.SpireTools();
                        Ex.ExportToPDFTable(dtAsoc);

                    }


                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
              new JsonSerializerSettings
              {
                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
              });

                    return newresults;
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    throw new Exception(StringHelper.InvalidParameter);
                }
            }
            return null;
        }

        public class itemVars
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        public class itemVarsList
        {
            public Int32 ID { get; set; }
            public string newValue { get; set; }
        }
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetNewInsertDoShowTable")]
        [OverrideAuthorization]
        public IHttpActionResult GetNewInsertDoShowTable(genericRequest paramRequest)
        {

            try
            {
                if (paramRequest != null)
                {

                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
                    }

                    Int64 ruleId = Int64.Parse(paramRequest.Params["RuleId"]);

                    string FormVariables = string.Empty;
                    if (paramRequest.Params.ContainsKey("FormVariables"))
                        FormVariables = paramRequest.Params["FormVariables"];

                    Int64 TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                    string SelectedresultList = paramRequest.Params["SelectedresultList"];
                    string datasourceColumn = paramRequest.Params["datasourceColumn"];

                    List<string> ListSelectedResult = new List<string>();

                    string ListResult = SelectedresultList;
                    ListSelectedResult = ListResult.Split(',').ToList();


                    WFTaskBusiness WTB = new WFTaskBusiness();

                    ITaskResult Task = WTB.GetTask(TaskId, user.ID);
                    if (Task != null)
                    {
                        List<ITaskResult> results = new List<ITaskResult>();
                        results.Add(Task);
                        Dictionary<string, string> dicFormVariables = new Dictionary<string, string>();

                        if (!string.IsNullOrEmpty(FormVariables))
                        {
                            //tomo los valores asignados desde el form en js
                            /// Se convierte el valor en un diccionario para poder iterarlo
                            //Dictionary<string, string> dicFormVariables = new Dictionary<string, string>();
                            List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                            //Se itera el diccionario de los valores

                            foreach (var itemlist in dicFormVariables) //VARS
                            {

                                if (!VariablesInterReglas.ContainsKey(itemlist.Key))
                                    VariablesInterReglas.Add(itemlist.Key, itemlist.Value);
                                else
                                    VariablesInterReglas.set_Item(itemlist.Key, itemlist.Value);
                            }
                        }

                        //tomo los valores de la columna de las filas seleccionadas y por cada una asigno su valor a la variable y ejecuto
                        foreach (var resultSelect in ListSelectedResult)  //results
                        {

                            if (!VariablesInterReglas.ContainsKey(datasourceColumn))
                                VariablesInterReglas.Add(datasourceColumn, resultSelect);
                            else
                                VariablesInterReglas.set_Item(datasourceColumn, resultSelect);
                            try
                            {
                                Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
                                Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
                                List<Int64> ExecutedIDs = new List<Int64>();
                                List<Int64> PendingChildRules = new List<Int64>();
                                Hashtable Params = new Hashtable();
                                bool RefreshRule = false;
                                WFExecution wFExecution = new WFExecution(MembershipHelper.CurrentUser);

                                wFExecution._haceralgoEvent += HandleWFExecutionPendingEvents;

                                wFExecution.ExecuteRule(ruleId, ref results, ref PendigEvent, ref ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, new List<Int64>(), true);

                            }
                            catch (Exception ex)
                            {
                                Zamba.AppBlock.ZException.Log(ex);
                                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,
                           new HttpError(ex.ToString())));

                            }
                        }

                        try
                        {
                            return Ok(VariablesInterreglesToJson(VariablesInterReglas.Keys()));
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(ex);
                            throw new Exception(StringHelper.InvalidParameter);
                        }

                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.TaskIdExpected)));
                    }
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError,
           new HttpError(ex.ToString())));
            }

        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("api/search/getTimeLineNews")]
        [OverrideAuthorization]
        public IHttpActionResult getTimeLineNews(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 resultId = 0;
                    string entityId = string.Empty;
                    string AsocNewsIds = "";
                    if (paramRequest.Params != null)
                    {
                        resultId = Int64.Parse(paramRequest.Params["DocId"].ToString());
                        entityId = paramRequest.Params["EntityId"].ToString();
                        if (paramRequest.Params.ContainsKey("AsocNewsIds"))
                            AsocNewsIds = paramRequest.Params["AsocNewsIds"].ToString();
                    }

                    NewsBusiness NB = new NewsBusiness();
                    DataSet dtAsoc = NB.GetNews(paramRequest.UserId, resultId, entityId);
                    //            strselect.Append("select znu.NewsId, zn.docid,zn.doctypeid,zn.c_value,Zn.crdate, zn.userid,zn.details,usuario from ZNewsUsers as znu ")

                    //ML: falta ver de traer las novedades de los asociados segun ids especificados.

                    List<TimeLineDto> TL = new List<TimeLineDto>();
                    //DataTable dt = new DataTable();
                    foreach (DataRow r in dtAsoc.Tables[0].Rows)
                    {
                        TimeLineDto dto = new TimeLineDto();
                        if (r.Table.Columns.Contains("c_value"))
                        {
                            dto.subtitle = r["c_value"].ToString();
                        }
                        else
                        {
                            dto.subtitle = r["value"].ToString();
                        }

                        if (r.Table.Columns.Contains("Usuario"))
                        {
                            dto.UsrAprobacion = r["Usuario"].ToString();
                        }

                        if (r.Table.Columns.Contains("Details"))
                        {
                            dto.description = r["Details"].ToString();
                        }

                        if (r.Table.Columns.Contains("crdate"))
                        {
                            dto.date = r["crdate"].ToString();
                        }

                        dto.resultId = Int64.Parse(r["DOCID"].ToString());
                        dto.entityId = Int64.Parse(r["DOCTYPEID"].ToString());

                        dto.avatar = GetBase64Photo(dto.UserId);

                        TL.Add(dto);

                    }

                    var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }




        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getTimeLineAprobaciones")]
        [OverrideAuthorization]
        public IHttpActionResult getAsociatedResultsTimeLine(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    Int64 AsociatedIdsList = 0;
                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        resultId = Int64.Parse(paramRequest.Params["parentDocId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["ParentEntityId"].ToString());
                        AsociatedIdsList = Int64.Parse(paramRequest.Params["EntityId"].ToString());
                        AsociatedIds.Add(paramRequest.Params["EntityId"].ToString());
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);

                    DataTable dtAsoc = null;

                    if (result != null)
                    {
                        dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsList(AsociatedIdsList, result, 100, user.ID);
                    }
                    if (dtAsoc.Columns.Contains("Fecha Accion Aprobacion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha Accion Aprobacion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha de Accion Aprobacion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha de Accion Aprobacion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha de Accion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha de Accion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha Accion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha Accion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    List<TimeLineDto> TL = new List<TimeLineDto>();
                    //DataTable dt = new DataTable();
                    foreach (DataRow r in dtAsoc.Rows)
                    {
                        TimeLineDto dto = new TimeLineDto();
                        if (r.Table.Columns.Contains("Observaciones"))
                        {
                            dto.description = r["Observaciones"].ToString();
                        }
                        if (r.Table.Columns.Contains("Estado de la Solicitud"))
                        {
                            dto.description = r["Estado de la Solicitud"].ToString();
                        }
                        try
                        {

                            if (r.Table.Columns.Contains("I1020139"))
                            {
                                dto.UserId = Int64.Parse(r["I1020139"].ToString());
                            }
                            else if (r.Table.Columns.Contains("PLATTER_ID"))
                            {
                                dto.UserId = Int64.Parse(r["PLATTER_ID"].ToString());
                            }
                            else if (r.Table.Columns.Contains("Usuario"))
                            {
                                dto.UserId = Int64.Parse(r["Usuario"].ToString());
                            }
                        }
                        catch (Exception)
                        {
                        }

                        try
                        {
                            if (r.Table.Columns.Contains("Usuario de Accion Aprobacion"))
                            {
                                dto.UsrAprobacion = r["Usuario de Accion Aprobacion"].ToString();
                            }
                            else if (r.Table.Columns.Contains("Usuario"))
                            {
                                dto.UsrAprobacion = r["Usuario"].ToString();
                            }
                        }
                        catch (Exception)
                        {
                        }

                        if (r.Table.Columns.Contains("Accion de Aprobacion"))
                        {
                            dto.subtitle = r["Accion de Aprobacion"].ToString();
                        }

                        if (r.Table.Columns.Contains("Fecha de Accion Aprobacion"))
                        {
                            dto.date = r["Fecha de Accion Aprobacion"].ToString();
                        }else                          if (r.Table.Columns.Contains("Fecha Accion Aprobacion"))
                        {
                            dto.date = r["Fecha Accion Aprobacion"].ToString();
                        }
                        else if (r.Table.Columns.Contains("Fecha y Hora Derivacion"))
                        {
                            dto.date = r["Fecha y Hora Derivacion"].ToString();
                        }
                        // --  Pretensiones --
                        if (r.Table.Columns.Contains("Ejecutivo Autorizante"))
                        {
                            dto.UsrAprobacion = r["Ejecutivo Autorizante"].ToString();
                        }
                        if (r.Table.Columns.Contains("Estado Pretension y Oferta"))
                        {
                            dto.subtitle = r["Estado Pretension y Oferta"].ToString();
                        }
                        if (r.Table.Columns.Contains("Fecha Autorizacion"))
                        {
                            dto.date = r["Fecha Autorizacion"].ToString();
                        }

                        dto.resultId = Int64.Parse(r["DOC_ID"].ToString());
                        dto.entityId = Int64.Parse(r["DOC_TYPE_ID"].ToString());


                        //dto.description = Int64.Parse(r["DOC_TYPE_ID"].ToString());
                        //public long UserId Int64.Parse(r["ixxxx"].tostring()); 

                        dto.avatar = GetBase64Photo(dto.UserId);

                        TL.Add(dto);



                    }
                    //new TimeLineDto

                    var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetResultsByReportIdTimeLine")]
        [OverrideAuthorization]
        public IHttpActionResult GetResultsByReportIdTimeLine(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int32 ReportID = 0;
                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null)
                    {
                        ReportID = Int32.Parse(paramRequest.Params["ReportID"]);


                    }
                    Hashtable dicFormVariables = new Hashtable();

                    if (paramRequest.Params != null)
                    {
                        if (paramRequest.Params.ContainsKey("FormVariables"))
                        {
                            string FormVariables = paramRequest.Params["FormVariables"];
                            //                             TaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                            //tomo los valores asignados desde el form en js
                            /// Se convierte el valor en un diccionario para poder iterarlo

                            List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                        }
                    }

                    ITaskResult parentTask = null;
                    if (paramRequest.Params != null)
                    {
                        if (paramRequest.Params.ContainsKey("ParentTaskId"))
                        {
                            Int64 ParentTaskId = Int64.Parse(paramRequest.Params["ParentTaskId"]);
                            parentTask = new WFTaskBusiness().GetTask(ParentTaskId, user.ID);
                        }
                    }

                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

                    DataTable dtAsoc = RB.EvaluationRunWebQueryBuilder(ReportID, true, dicFormVariables, parentTask).Tables[0];


                    if (dtAsoc.Columns.Contains("Fecha Accion Aprobacion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha Accion Aprobacion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha de Accion Aprobacion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha de Accion Aprobacion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha de Accion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha de Accion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    if (dtAsoc.Columns.Contains("Fecha Accion"))
                    {
                        dtAsoc.DefaultView.Sort = "Fecha Accion desc";
                        dtAsoc = dtAsoc.DefaultView.ToTable();
                    }
                    List<TimeLineDto> TL = new List<TimeLineDto>();
                    //DataTable dt = new DataTable();
                    foreach (DataRow r in dtAsoc.Rows)
                    {
                        TimeLineDto dto = new TimeLineDto();
                        if (r.Table.Columns.Contains("Observaciones"))
                        {
                            dto.description = r["Observaciones"].ToString();
                        }
                        if (r.Table.Columns.Contains("Estado de la Solicitud"))
                        {
                            dto.description = r["Estado de la Solicitud"].ToString();
                        }
                        try
                        {

                            if (r.Table.Columns.Contains("I1020139"))
                            {
                                dto.UserId = Int64.Parse(r["I1020139"].ToString());
                            }
                        }
                        catch (Exception)
                        {
                        }

                        if (r.Table.Columns.Contains("Usuario de Accion Aprobacion"))
                        {
                            dto.UsrAprobacion = r["Usuario de Accion Aprobacion"].ToString();
                        }

                        if (r.Table.Columns.Contains("Accion de Aprobacion"))
                        {
                            dto.subtitle = r["Accion de Aprobacion"].ToString();
                        }
                        if (r.Table.Columns.Contains("Fecha de Accion Aprobacion"))
                        {
                            dto.date = r["Fecha de Accion Aprobacion"].ToString();
                        }
                        if (r.Table.Columns.Contains("Fecha Accion Aprobacion"))
                        {
                            dto.date = r["Fecha Accion Aprobacion"].ToString();
                        }
                        // --  Pretensiones --
                        if (r.Table.Columns.Contains("Ejecutivo Autorizante"))
                        {
                            dto.UsrAprobacion = r["Ejecutivo Autorizante"].ToString();
                        }
                        if (r.Table.Columns.Contains("Estado Pretension y Oferta"))
                        {
                            dto.subtitle = r["Estado Pretension y Oferta"].ToString();
                        }
                        if (r.Table.Columns.Contains("Fecha Autorizacion"))
                        {
                            dto.date = r["Fecha Autorizacion"].ToString();
                        }

                        dto.resultId = Int64.Parse(r["DOC_ID"].ToString());
                        dto.entityId = Int64.Parse(r["DOC_TYPE_ID"].ToString());


                        //dto.description = Int64.Parse(r["DOC_TYPE_ID"].ToString());
                        //public long UserId Int64.Parse(r["ixxxx"].tostring()); 

                        dto.avatar = GetBase64Photo(dto.UserId);

                        TL.Add(dto);



                    }
                    //new TimeLineDto

                    var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpGet]
        //[Route("api/search/GetResultsByReportIdTimeline")]
        //public IHttpActionResult GetResultsByReportIdTimeline(genericRequest paramRequest)
        //{

        //    if (paramRequest != null)
        //    {
        //        var user = GetUser(paramRequest.UserId);
        //        if (user == null)
        //            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
        //                new HttpError(StringHelper.InvalidUser)));
        //        try
        //        {
        //            Int32 ReportID = 0;
        //            List<string> AsociatedIds = new List<string>();
        //            if (paramRequest.Params != null)
        //            {
        //                ReportID = Int32.Parse(paramRequest.Params["ReportID"]);


        //            }

        //            Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();

        //            var newresultss = RB.EvaluationRunWebQueryBuilder(ReportID, true, null);

        //            DataTable dtAsoc = newresultss.Tables[0];


        //            List<TimeLineDto> TL = new List<TimeLineDto>();
        //            //DataTable dt = new DataTable();
        //            foreach (DataRow r in dtAsoc.Rows)
        //            {
        //                TimeLineDto dto = new TimeLineDto();
        //                if (r.Table.Columns.Contains("Usuario Zamba"))
        //                {
        //                    dto.UsuarioZamba = r["Usuario Zamba"].ToString();
        //                }
        //                if (r.Table.Columns.Contains("Fecha Conexion"))
        //                {
        //                    dto.FechaConexion = r["Fecha Conexion"].ToString();
        //                }
        //                if (r.Table.Columns.Contains("Ultima Actividad"))
        //                {
        //                    dto.UltimaActividad = r["Ultima Actividad"].ToString();
        //                }
        //                if (r.Table.Columns.Contains("Usuario de Windows"))
        //                {
        //                    dto.UsuarioWindows = r["Usuario de Windows"].ToString();
        //                }
        //                if (r.Table.Columns.Contains("Nombre PC"))
        //                {
        //                    dto.NombrePC = r["Nombre PC"].ToString();
        //                }

        //                TL.Add(dto);

        //            }
        //            //new TimeLineDto

        //            var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
        //            new JsonSerializerSettings
        //            {
        //                PreserveReferencesHandling = PreserveReferencesHandling.Objects
        //            });

        //            return Ok(newresults);
        //        }
        //        catch (Exception ex)
        //        {
        //            ZClass.raiseerror(ex);
        //            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
        //       new HttpError(StringHelper.InvalidParameter)));
        //        }
        //    }
        //    return null;
        //}

        //public GetAvatarUrl(Int64 UserId)
        //{

        //        return EnvironmentUtil.curProtocol(Request) + @"://" + Request.ServerVariables["HTTP_HOST"] + Request.ApplicationPath + @"/Services/GetDocFile.ashx?DocTypeId={0}&DocId={1}&UserID={2}";
        //    }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getAsociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult getAsociatedResults(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> AsociatedIds = new List<string>();
                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        AsociatedIds.AddRange(paramRequest.Params["AsociatedIds"].ToString().Split(char.Parse(",")));
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);

                    DataTable dtAsoc = null;

                    if (result != null)
                    {
                        dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(result, 100, AsociatedIds, user.ID);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getPermisosInsert")]
        

        public IHttpActionResult getPermisosInsert(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Se inicio validacion de usuario para btn insertar");

                    Results_Business RB = new Results_Business();
                    DataTable result = RB.getPermisosInsert();

                    for (int i = 0; i < user.Groups.Count; i++)
                    {
                        for (int f = 0; f < result.Rows.Count; f++)
                        {
                            if (Convert.ToInt32(((Zamba.Core.ZBaseCore)user.Groups[i]).ID) == Convert.ToInt32(result.Rows[f].ItemArray[0]))
                            {
                                ZTrace.WriteLineIf(ZTrace.IsInfo, "Permisos para boton insertar id de grupo: " + Convert.ToInt32(((Zamba.Core.ZBaseCore)user.Groups[i]).ID));
                                return Ok(true);
                            }

                        }

                    }

                    DataTable dtAsoc = null;


                    return Ok(false);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/setTaskIndex")]
        [OverrideAuthorization]
        public IHttpActionResult setTaskIndex(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 indexId = 0;
                    Int64 parentResultId = 0;
                    Int64 entityId = 0;
                    Int64 taskId = 0;
                    string indexValue = validateParam("indexValue", paramRequest);
                    if (paramRequest.Params != null)
                    {
                        indexId = Int64.Parse(paramRequest.Params["indexId"]);
                        parentResultId = Int64.Parse(paramRequest.Params["parentResultId"]);
                        entityId = Int64.Parse(paramRequest.Params["entityId"]);

                        if (entityId == 0)
                        {
                            taskId = Int64.Parse(paramRequest.Params["taskId"]);
                            STasks sTasks = new STasks();
                            entityId = sTasks.GetDocTypeId(taskId);
                            sTasks = null;
                        }
                    }

                    Results_Business RB = new Results_Business();
                    bool isIndexUpdated = RB.setIndexData(indexId, entityId, parentResultId, indexValue);
                    return Ok(isIndexUpdated);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return Ok(false);
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/setTaskIndexs")]
        [OverrideAuthorization]
        public IHttpActionResult setTaskIndexs(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {

                    Int64 ResultId = 0;
                    Int64 EntityId = 0;
                    Int64 TaskId = 0;
                    IResult result = null;

                    if (paramRequest.Params != null)
                    {
                        ResultId = Int64.Parse(paramRequest.Params["resultId"]);
                        EntityId = Int64.Parse(paramRequest.Params["entityId"]);

                        if (paramRequest.Params.ContainsKey("taskId"))
                        {
                            TaskId = Int64.Parse(paramRequest.Params["taskId"]);
                        }

                        Results_Business RB = new Results_Business();

                        if (TaskId != 0)
                        {
                            WFTaskBusiness WTB = new WFTaskBusiness();
                            result = WTB.GetTask(TaskId, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        }
                        else
                        {
                            result = RB.GetResult(ResultId, EntityId, true);
                        }
                        string Indexs = paramRequest.Params["Indexs"];

                        List<IIndex> IndexsObject = new JavaScriptSerializer().Deserialize<List<IIndex>>(Indexs);

                        foreach (IIndex I in IndexsObject)
                        {
                            Int64 IndexId = I.ID;
                            string indexValue = I.Data;

                            result.get_GetIndexById(I.ID).Data = I.Data;
                            result.get_GetIndexById(I.ID).DataTemp = I.Data;

                            RB.SaveModifiedIndexData(ref result, true, false, null, null);
                        }
                    }

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return Ok(false);
        }




        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetTaskFilterConfig")]
        [isGenericRequest]
        public IHttpActionResult GetTaskFilterConfig(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {

                if (paramRequest.UserId == 0)
                {
                    var UsuarioID = Zamba.Membership.MembershipHelper.CurrentUser.ID;
                    paramRequest.Params = new Dictionary<string, string>();
                    paramRequest.Params.Add("UserId", UsuarioID.ToString());

                    var user = GetUser(Int64.Parse(paramRequest.Params["UserId"].ToString()));

                }
                else
                {
                    var user = GetUser(paramRequest.UserId);
                    if (user == null)
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                            new HttpError(StringHelper.InvalidUser)));
                }
                try
                {
                    Zamba.Core.UserPreferences up = new Zamba.Core.UserPreferences();
                    paramRequest.Params = new Dictionary<string, string>();
                    //Obtengo los parametros que hacen visible a los checks por cada usuario, ver si hay que hacer algun casteo a bool
                    paramRequest.Params.Add("ShowMyTasks", up.getEspecificUserValue("ShowMyTasks", UPSections.UserPreferences, true, paramRequest.UserId).ToString().ToLower() == "true" ? "true" : "false");
                    paramRequest.Params.Add("ShowMyTeamTasks", up.getEspecificUserValue("ShowMyTeamTasks", UPSections.UserPreferences, false, paramRequest.UserId).ToString().ToLower() == "true" ? "true" : "false");
                    paramRequest.Params.Add("ShowMyAllTeamTasks", up.getEspecificUserValue("ShowMyAllTeamTasks", UPSections.UserPreferences, false, paramRequest.UserId).ToString().ToLower() == "true" ? "true" : "false");
                    paramRequest.Params.Add("ShowAllTasks", up.getEspecificUserValue("ShowAllTasks", UPSections.UserPreferences, true, paramRequest.UserId).ToString().ToLower() == "true" ? "true" : "false");

                    paramRequest.Params.Add("IdsAllTasks", up.getEspecificUserValue("IdsAllTasks", UPSections.UserPreferences, "2523", paramRequest.UserId).ToString());

                    //obtengo los textos personalizados para cada usuario
                    paramRequest.Params.Add("MyTasksText", up.getEspecificUserValue("MyTasksText", UPSections.UserPreferences, "Mis Tareas", paramRequest.UserId).ToString());
                    paramRequest.Params.Add("MyTeamTasksText", up.getEspecificUserValue("MyTeamTasksText", UPSections.UserPreferences, "Tareas del Equipo", paramRequest.UserId).ToString());
                    paramRequest.Params.Add("MyAllTeamTasksText", up.getEspecificUserValue("MyAllTeamTasksText", UPSections.UserPreferences, "Todo el Equipo", paramRequest.UserId).ToString());
                    paramRequest.Params.Add("AllTasksText", up.getEspecificUserValue("AllTasksText", UPSections.UserPreferences, "Todas las tareas", paramRequest.UserId).ToString());

                    up = null;
                    var newresults = JsonConvert.SerializeObject(paramRequest, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetBaremosAsociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult GetBaremosAsociatedResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                IndexsBusiness indexsBusiness = new IndexsBusiness();


                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> associatedIds = new List<string>();
                    Char separator = ',';

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);





                    //DataTable baremosIds = RB.GetIdsFromABaremoMuerte(resultId);

                    DataTable parentsIds = RB.GetIdsToAsociatedParents(resultId, Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]));

                    var idReclamo = Int64.Parse(parentsIds.Rows[0].ItemArray[0].ToString());
                    var idParent = Int64.Parse(parentsIds.Rows[0].ItemArray[1].ToString());



                    DataTable dtAsoc = null;
                    List<Int64> idsToFilter = new List<Int64>();
                    idsToFilter.Add(2677);
                    idsToFilter.Add(Int64.Parse(associatedIds[1]));

                    Dictionary<Int64, string> indexsFiltered = getIndexsIdsAndNamesFromIndexsResult(result.Indexs, idsToFilter);

                    if (result != null)
                    {
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo, idParent, result.DocTypeId, Int64.Parse(associatedIds[0]));
                        dtAsoc = RB.GetAsociatedToEditTable(idReclamo,
                                                            idParent,
                                                            result.DocTypeId,
                                                            Int64.Parse(associatedIds[0]),
                                                            Int64.Parse(associatedIds[1]),
                                                            indexsFiltered,
                                                            result.DocTypeId);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetRequestAsociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult GetRequestAsociatedResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                IndexsBusiness indexsBusiness = new IndexsBusiness();


                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> associatedIds = new List<string>();
                    Char separator = ',';

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);


                    Int64 parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), resultId);



                    DataTable dtAsoc = null;
                    List<Int64> idsToFilter = new List<Int64>();
                    idsToFilter.Add(16);

                    List<Int64> indexsFiltered = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

                    if (result != null)
                    {
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo, idParent, result.DocTypeId, Int64.Parse(associatedIds[0]));
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo,
                        //                                    idParent,
                        //                                    result.DocTypeId,
                        //                                    Int64.Parse(associatedIds[0]),
                        //                                    Int64.Parse(associatedIds[1]),
                        //                                    indexsFiltered,
                        //                                    result.DocTypeId);
                        dtAsoc = RB.GetRequestAssociatedResult(parentsId, 106, 16, indexsFiltered);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetRequestAsociatedResults2")]
        [OverrideAuthorization]
        public IHttpActionResult GetRequestAsociatedResults2(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                IndexsBusiness indexsBusiness = new IndexsBusiness();


                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> associatedIds = new List<string>();
                    Char separator = ',';

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);


                    Int64 parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), resultId);



                    DataTable dtAsoc = null;
                    List<Int64> idsToFilter = new List<Int64>();
                    idsToFilter.Add(16);

                    List<Int64> indexsFiltered = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

                    if (result != null)
                    {
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo, idParent, result.DocTypeId, Int64.Parse(associatedIds[0]));
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo,
                        //                                    idParent,
                        //                                    result.DocTypeId,
                        //                                    Int64.Parse(associatedIds[0]),
                        //                                    Int64.Parse(associatedIds[1]),
                        //                                    indexsFiltered,
                        //                                    result.DocTypeId);
                        dtAsoc = RB.GetRequestAssociatedResult(parentsId, 106, 16, indexsFiltered);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetDispatcherAsociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult GetDispatcherAsociatedResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                IndexsBusiness indexsBusiness = new IndexsBusiness();


                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> associatedIds = new List<string>();
                    Char separator = ',';

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);


                    Int64 parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), resultId);



                    DataTable dtAsoc = null;
                    List<Int64> idsToFilter = new List<Int64>();
                    idsToFilter.Add(139600);

                    List<Int64> indexsFiltered = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

                    if (result != null)
                    {
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo, idParent, result.DocTypeId, Int64.Parse(associatedIds[0]));
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo,
                        //                                    idParent,
                        //                                    result.DocTypeId,
                        //                                    Int64.Parse(associatedIds[0]),
                        //                                    Int64.Parse(associatedIds[1]),
                        //                                    indexsFiltered,
                        //                                    result.DocTypeId);
                        dtAsoc = RB.GetRequestAssociatedResult(parentsId, 139084, 139600, indexsFiltered);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetRemitoAsociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult GetRemitoAsociatedResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                IndexsBusiness indexsBusiness = new IndexsBusiness();


                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 resultId = 0;
                    Int64 entityId = 0;
                    List<string> associatedIds = new List<string>();
                    Char separator = ',';

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
                    {
                        resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                        associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                    }

                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(resultId, entityId, true);


                    Int64 parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), resultId);



                    DataTable dtAsoc = null;
                    List<Int64> idsToFilter = new List<Int64>();
                    idsToFilter.Add(86);

                    List<Int64> indexsFiltered = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

                    if (result != null)
                    {
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo, idParent, result.DocTypeId, Int64.Parse(associatedIds[0]));
                        //dtAsoc = RB.GetAsociatedToEditTable(idReclamo,
                        //                                    idParent,
                        //                                    result.DocTypeId,
                        //                                    Int64.Parse(associatedIds[0]),
                        //                                    Int64.Parse(associatedIds[1]),
                        //                                    indexsFiltered,
                        //                                    result.DocTypeId);
                        dtAsoc = RB.GetRequestAssociatedResult(parentsId, 1020018, 86, indexsFiltered);
                    }
                    var newresults = JsonConvert.SerializeObject(dtAsoc, Formatting.Indented,
               new JsonSerializerSettings
               {
                   PreserveReferencesHandling = PreserveReferencesHandling.Objects
               });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }



        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/ModifyAssociatedResult")]
        [OverrideAuthorization]
        public void ModifyAssociatedResult(genericRequest paramRequest)
        {
            SResult sResult = new SResult();

            Results_Business RB = new Results_Business();
            List<IIndex> emptyIndexs = new List<IIndex>();
            List<IIndex> indexs = new List<IIndex>();
            IndexsBusiness indexBusiness = new IndexsBusiness();
            Int64 entityId = Int64.Parse(paramRequest.Params["entityId"]);
            Int64 resultId = Int64.Parse(paramRequest.Params["resultId"]);
            List<string> associatedIds = new List<string>();
            Char separator = ',';
            associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
            Int64 parentResultID = Int64.Parse(paramRequest.Params["parentResultId"].ToString());
            emptyIndexs = indexBusiness.GetIndexsSchema(entityId);
            indexBusiness = null;

            DataTable parentsIds = new DataTable();
            Int64 idReclamo = 0;
            Int64 idParent = 0;
            Int64 parentsId = 0;


            if (paramRequest.Params.ContainsKey("isRequest") || paramRequest.Params.ContainsKey("isDispatcher") ||
                paramRequest.Params.ContainsKey("isRemito"))
            {
                parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), parentResultID);
            }
            else
            {
                parentsIds = RB.GetIdsToAsociatedParents(parentResultID, Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]));
                idReclamo = Int64.Parse(parentsIds.Rows[0].ItemArray[0].ToString());
                idParent = Int64.Parse(parentsIds.Rows[0].ItemArray[1].ToString());
            }


            if (paramRequest.Params.ContainsKey("isPedidoFondo"))
            {
                string favorTo = validateParamRequest(paramRequest.Params["favorTo"]);
                string commitmentNumber = validateParamRequest(paramRequest.Params["commitmentNumber"]);
                string payMethod = validateParamRequest(paramRequest.Params["payMethod"]);
                string cbu = validateParamRequest(paramRequest.Params["cbu"]);
                string cuit = validateParamRequest(paramRequest.Params["cuit"]);
                string concept = validateParamRequest(paramRequest.Params["concept"]);
                string amount = validateParamRequest(paramRequest.Params["amount"]);
                string mail = validateParamRequest(paramRequest.Params["email"]);
                string personNumber = validateParamRequest(paramRequest.Params["personNumber"]);
                string alternativeConcept = validateParamRequest(paramRequest.Params["alternativeConcept"]);

                indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                indexs.Add(SetIndexData(favorTo.ToString(), 10289, emptyIndexs));
                indexs.Add(SetIndexData(concept.ToString(), 2788, emptyIndexs));
                indexs.Add(SetIndexData(amount.ToString(), 109, emptyIndexs));
                indexs.Add(SetIndexData(commitmentNumber.ToString(), 10318, emptyIndexs));
                indexs.Add(SetIndexData(payMethod.ToString(), 10251, emptyIndexs));
                indexs.Add(SetIndexData(cbu.ToString(), 10290, emptyIndexs));
                indexs.Add(SetIndexData(cuit.ToString(), 2, emptyIndexs));
                indexs.Add(SetIndexData(idParent.ToString(), 1020129, emptyIndexs));
                indexs.Add(SetIndexData(mail.ToString(), 2885, emptyIndexs));
                indexs.Add(SetIndexData(personNumber.ToString(), 1020144, emptyIndexs));
                indexs.Add(SetIndexData(alternativeConcept.ToString(), 1220204, emptyIndexs));
            }
            else if (paramRequest.Params.ContainsKey("isRequest"))
            {
                string line = validateParamRequest(paramRequest.Params["line"]);
                string product = validateParamRequest(paramRequest.Params["product"]);
                string unitPrice = validateParamRequest(paramRequest.Params["unitPrice"]);
                string measure = validateParamRequest(paramRequest.Params["measure"]);
                string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                string typeOfCurrency = validateParamRequest(paramRequest.Params["typeOfCurrency"]);
                string price = validateParamRequest(paramRequest.Params["price"]);
                string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                string delegations = validateParamRequest(paramRequest.Params["delegations"]);

                indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                indexs.Add(SetIndexData(unitPrice.ToString(), 198, emptyIndexs));
                indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                indexs.Add(SetIndexData(typeOfCurrency.ToString(), 92, emptyIndexs));
                indexs.Add(SetIndexData(price.ToString(), 152, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 16, emptyIndexs));
                indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
            }
            else if (paramRequest.Params.ContainsKey("isDispatcher"))
            {
                string familyCode = validateParamRequest(paramRequest.Params["familyCode"]);
                string quatity = validateParamRequest(paramRequest.Params["quatity"]);

                indexs.Add(SetIndexData(familyCode.ToString(), 139602, emptyIndexs));
                indexs.Add(SetIndexData(quatity.ToString(), 139609, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 139609, emptyIndexs));
            }
            else if (paramRequest.Params.ContainsKey("isRemito"))
            {
                string line = validateParamRequest(paramRequest.Params["line"]);
                string product = validateParamRequest(paramRequest.Params["product"]);
                string measure = validateParamRequest(paramRequest.Params["measure"]);
                string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                string delegations = validateParamRequest(paramRequest.Params["delegations"]);
                string amountsReceived = validateParamRequest(paramRequest.Params["amountsReceived"]);

                indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 86, emptyIndexs));
                indexs.Add(SetIndexData(amountsReceived.ToString(), 1020181, emptyIndexs));
            }
            else
            {
                string reclaimantName = validateParamRequest(paramRequest.Params["reclaimantName"]);
                string reclaimentType = validateParamRequest(paramRequest.Params["reclaimentType"]);
                //Id Reclamo
                indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                //Id Baremo Muerte
                indexs.Add(SetIndexData(idParent.ToString(), 1020121, emptyIndexs));
                //Tipo de Reclamante
                indexs.Add(SetIndexData(reclaimentType, 10317, emptyIndexs));
                //Nombre
                indexs.Add(SetIndexData(reclaimantName, 2706, emptyIndexs));
            }


            //List<Int64> specificIndexs = new List<Int64>();
            //specificIndexs.Add(10317);
            //specificIndexs.Add(2706);

            List<Int64> idsToFilter = new List<Int64>();
            List<Int64> specificIndexs = new List<Int64>();

            if (paramRequest.Params.ContainsKey("isRequest"))
            {
                idsToFilter.Add(16);
            }
            else if (paramRequest.Params.ContainsKey("isDispatcher"))
            {
                idsToFilter.Add(139600);
            }
            else if (paramRequest.Params.ContainsKey("isRemito"))
            {
                idsToFilter.Add(86);
            }
            else
            {
                idsToFilter.Add(2677);
                idsToFilter.Add(Int64.Parse(associatedIds[1]));
            }




            IResult result = RB.GetResult(resultId, entityId, true);

            specificIndexs = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

            if (paramRequest != null)
            {
                try
                {
                    sResult.SaveModifiedIndexs(GetResultFromParamRequest(paramRequest), specificIndexs, indexs);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("El parametro es nulo.");
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/ModifyAssociatedResult2")]
        [OverrideAuthorization]
        public void ModifyAssociatedResult2(genericRequest paramRequest)
        {
            SResult sResult = new SResult();

            Results_Business RB = new Results_Business();
            List<IIndex> emptyIndexs = new List<IIndex>();
            List<IIndex> indexs = new List<IIndex>();
            IndexsBusiness indexBusiness = new IndexsBusiness();
            Int64 entityId = Int64.Parse(paramRequest.Params["entityId"]);
            Int64 resultId = Int64.Parse(paramRequest.Params["resultId"]);
            List<string> associatedIds = new List<string>();
            Char separator = ',';
            associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
            Int64 parentResultID = Int64.Parse(paramRequest.Params["parentResultId"].ToString());
            emptyIndexs = indexBusiness.GetIndexsSchema(entityId);
            indexBusiness = null;

            DataTable parentsIds = new DataTable();
            Int64 idReclamo = 0;
            Int64 idParent = 0;
            Int64 parentsId = 0;


            if (paramRequest.Params.ContainsKey("isRequest") || paramRequest.Params.ContainsKey("isDispatcher") ||
                paramRequest.Params.ContainsKey("isRemito"))
            {
                parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), parentResultID);
            }
            else
            {
                parentsIds = RB.GetIdsToAsociatedParents(parentResultID, Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]));
                idReclamo = Int64.Parse(parentsIds.Rows[0].ItemArray[0].ToString());
                idParent = Int64.Parse(parentsIds.Rows[0].ItemArray[1].ToString());
            }


            if (paramRequest.Params.ContainsKey("isPedidoFondo"))
            {
                string favorTo = validateParamRequest(paramRequest.Params["favorTo"]);
                string commitmentNumber = validateParamRequest(paramRequest.Params["commitmentNumber"]);
                string payMethod = validateParamRequest(paramRequest.Params["payMethod"]);
                string cbu = validateParamRequest(paramRequest.Params["cbu"]);
                string cuit = validateParamRequest(paramRequest.Params["cuit"]);
                string concept = validateParamRequest(paramRequest.Params["concept"]);
                string amount = validateParamRequest(paramRequest.Params["amount"]);
                string mail = validateParamRequest(paramRequest.Params["email"]);
                string personNumber = validateParamRequest(paramRequest.Params["personNumber"]);
                string alternativeConcept = validateParamRequest(paramRequest.Params["alternativeConcept"]);

                indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                indexs.Add(SetIndexData(favorTo.ToString(), 10289, emptyIndexs));
                indexs.Add(SetIndexData(concept.ToString(), 2788, emptyIndexs));
                indexs.Add(SetIndexData(amount.ToString(), 109, emptyIndexs));
                indexs.Add(SetIndexData(commitmentNumber.ToString(), 10318, emptyIndexs));
                indexs.Add(SetIndexData(payMethod.ToString(), 10251, emptyIndexs));
                indexs.Add(SetIndexData(cbu.ToString(), 10290, emptyIndexs));
                indexs.Add(SetIndexData(cuit.ToString(), 2, emptyIndexs));
                indexs.Add(SetIndexData(idParent.ToString(), 1020129, emptyIndexs));
                indexs.Add(SetIndexData(mail.ToString(), 2885, emptyIndexs));
                indexs.Add(SetIndexData(personNumber.ToString(), 1020144, emptyIndexs));
                indexs.Add(SetIndexData(alternativeConcept.ToString(), 1220204, emptyIndexs));


            }
            else if (paramRequest.Params.ContainsKey("isRequest"))
            {
                string line = validateParamRequest(paramRequest.Params["line"]);
                string product = validateParamRequest(paramRequest.Params["product"]);
                string unitPrice = validateParamRequest(paramRequest.Params["unitPrice"]);
                string measure = validateParamRequest(paramRequest.Params["measure"]);
                string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                string typeOfCurrency = validateParamRequest(paramRequest.Params["typeOfCurrency"]);
                string price = validateParamRequest(paramRequest.Params["price"]);
                string description = (paramRequest.Params.ContainsKey("description")) ? validateParamRequest(paramRequest.Params["description"]) : string.Empty;
                string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                string delegations = validateParamRequest(paramRequest.Params["delegations"]);

                indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                indexs.Add(SetIndexData(unitPrice.ToString(), 198, emptyIndexs));
                indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                indexs.Add(SetIndexData(typeOfCurrency.ToString(), 92, emptyIndexs));
                indexs.Add(SetIndexData(price.ToString(), 152, emptyIndexs));
                indexs.Add(SetIndexData(description.ToString(), 1020147, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 16, emptyIndexs));
                indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
            }
            else if (paramRequest.Params.ContainsKey("isDispatcher"))
            {
                string familyCode = validateParamRequest(paramRequest.Params["familyCode"]);
                string quatity = validateParamRequest(paramRequest.Params["quatity"]);

                indexs.Add(SetIndexData(familyCode.ToString(), 139602, emptyIndexs));
                indexs.Add(SetIndexData(quatity.ToString(), 139609, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 139609, emptyIndexs));
            }
            else if (paramRequest.Params.ContainsKey("isRemito"))
            {
                string line = validateParamRequest(paramRequest.Params["line"]);
                string product = validateParamRequest(paramRequest.Params["product"]);
                string measure = validateParamRequest(paramRequest.Params["measure"]);
                string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                string delegations = validateParamRequest(paramRequest.Params["delegations"]);
                string amountsReceived = validateParamRequest(paramRequest.Params["amountsReceived"]);

                indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                indexs.Add(SetIndexData(parentsId.ToString(), 86, emptyIndexs));
                indexs.Add(SetIndexData(amountsReceived.ToString(), 1020181, emptyIndexs));
            }
            else
            {
                string reclaimantName = validateParamRequest(paramRequest.Params["reclaimantName"]);
                string reclaimentType = validateParamRequest(paramRequest.Params["reclaimentType"]);
                //Id Reclamo
                indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                //Id Baremo Muerte
                indexs.Add(SetIndexData(idParent.ToString(), 1020121, emptyIndexs));
                //Tipo de Reclamante
                indexs.Add(SetIndexData(reclaimentType, 10317, emptyIndexs));
                //Nombre
                indexs.Add(SetIndexData(reclaimantName, 2706, emptyIndexs));
            }


            //List<Int64> specificIndexs = new List<Int64>();
            //specificIndexs.Add(10317);
            //specificIndexs.Add(2706);

            List<Int64> idsToFilter = new List<Int64>();
            List<Int64> specificIndexs = new List<Int64>();

            if (paramRequest.Params.ContainsKey("isRequest"))
            {
                idsToFilter.Add(16);
            }
            else if (paramRequest.Params.ContainsKey("isDispatcher"))
            {
                idsToFilter.Add(139600);
            }
            else if (paramRequest.Params.ContainsKey("isRemito"))
            {
                idsToFilter.Add(86);
            }
            else
            {
                idsToFilter.Add(2677);
                idsToFilter.Add(Int64.Parse(associatedIds[1]));
            }




            IResult result = RB.GetResult(resultId, entityId, true);

            specificIndexs = getIndexsFromIndexsResult(result.Indexs, idsToFilter);

            if (paramRequest != null)
            {
                try
                {
                    sResult.SaveModifiedIndexs(GetResultFromParamRequest(paramRequest), specificIndexs, indexs);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("El parametro es nulo.");
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/InsertAssociatedResult")]
        [OverrideAuthorization]
        public InsertResult InsertAssociatedResult(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                Results_Business RB = new Results_Business();
                Int64 DocTypeId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                Int64 parentResultId = Int64.Parse(paramRequest.Params["parentResultId"].ToString());

                // Int64 idParent = Int64.Parse(paramRequest.Params["parentEntityId"].ToString());


                SResult sResult = new SResult();
                INewResult newresult = new SResult().GetNewNewResult(DocTypeId);
                List<IIndex> emptyIndexs = new List<IIndex>();
                List<IIndex> indexs = new List<IIndex>();
                InsertResult insertResult = new InsertResult();



                IndexsBusiness indexBusiness = new IndexsBusiness();
                emptyIndexs = indexBusiness.GetIndexsSchema(DocTypeId);
                indexBusiness = null;


                List<string> associatedIds = new List<string>();
                Char separator = ',';
                associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                DataTable parentsIds = new DataTable();
                Int64 parentsId = 0;
                Int64 idReclamo = 0;
                Int64 idParent = 0;

                if (paramRequest.Params.ContainsKey("isRequest") || paramRequest.Params.ContainsKey("isDispatcher") ||
                    paramRequest.Params.ContainsKey("isRemito"))
                {
                    parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), parentResultId);
                }
                else
                {
                    parentsIds = RB.GetIdsToAsociatedParents(parentResultId, Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]));
                    idReclamo = Int64.Parse(parentsIds.Rows[0].ItemArray[0].ToString());
                    idParent = Int64.Parse(parentsIds.Rows[0].ItemArray[1].ToString());
                }


                if (paramRequest.Params.ContainsKey("isPedidoFondo"))
                {
                    string favorTo = validateParamRequest(paramRequest.Params["favorTo"]);
                    string commitmentNumber = validateParamRequest(paramRequest.Params["commitmentNumber"]);
                    string payMethod = validateParamRequest(paramRequest.Params["payMethod"]);
                    string cbu = validateParamRequest(paramRequest.Params["cbu"]);
                    string cuit = validateParamRequest(paramRequest.Params["cuit"]);
                    string concept = validateParamRequest(paramRequest.Params["concept"]);
                    string amount = validateParamRequest(paramRequest.Params["amount"]);
                    string mail = validateParamRequest(paramRequest.Params["email"]);
                    string personNumber = validateParamRequest(paramRequest.Params["personNumber"]);
                    string alternativeConcept = validateParamRequest(paramRequest.Params["alternativeConcept"]);
                    string cuitpro = validateParamRequest(paramRequest.Params["cuitpro"]);



                    indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                    indexs.Add(SetIndexData(favorTo.ToString(), 10289, emptyIndexs));
                    indexs.Add(SetIndexData(concept.ToString(), 2788, emptyIndexs));
                    indexs.Add(SetIndexData(amount.ToString(), 109, emptyIndexs));
                    indexs.Add(SetIndexData(commitmentNumber.ToString(), 10318, emptyIndexs));
                    indexs.Add(SetIndexData(payMethod.ToString(), 10251, emptyIndexs));
                    indexs.Add(SetIndexData(cbu.ToString(), 10290, emptyIndexs));
                    indexs.Add(SetIndexData(cuit.ToString(), 2, emptyIndexs));
                    indexs.Add(SetIndexData(idParent.ToString(), 1020129, emptyIndexs));
                    indexs.Add(SetIndexData(mail.ToString(), 2885, emptyIndexs));
                    indexs.Add(SetIndexData(personNumber.ToString(), 1020144, emptyIndexs));
                    indexs.Add(SetIndexData(alternativeConcept.ToString(), 1220204, emptyIndexs));
                    indexs.Add(SetIndexData(cuitpro.ToString(), 11535246, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isRequest"))
                {
                    string line = validateParamRequest(paramRequest.Params["line"]);
                    string product = validateParamRequest(paramRequest.Params["product"]);
                    string unitPrice = validateParamRequest(paramRequest.Params["unitPrice"]);
                    string measure = validateParamRequest(paramRequest.Params["measure"]);
                    string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                    string typeOfCurrency = validateParamRequest(paramRequest.Params["typeOfCurrency"]);
                    string price = validateParamRequest(paramRequest.Params["price"]);
                    string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                    string delegations = validateParamRequest(paramRequest.Params["delegations"]);

                    indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                    indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                    indexs.Add(SetIndexData(unitPrice.ToString(), 198, emptyIndexs));
                    indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                    indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                    indexs.Add(SetIndexData(typeOfCurrency.ToString(), 92, emptyIndexs));
                    indexs.Add(SetIndexData(price.ToString(), 152, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 16, emptyIndexs));
                    indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                    indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isDispatcher"))
                {
                    string familyCode = validateParamRequest(paramRequest.Params["familyCode"]);
                    string quatity = validateParamRequest(paramRequest.Params["quatity"]);

                    indexs.Add(SetIndexData(familyCode.ToString(), 139602, emptyIndexs));
                    indexs.Add(SetIndexData(quatity.ToString(), 139609, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 139609, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isRemito"))
                {
                    string line = validateParamRequest(paramRequest.Params["line"]);
                    string product = validateParamRequest(paramRequest.Params["product"]);
                    string measure = validateParamRequest(paramRequest.Params["measure"]);
                    string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                    string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                    string delegations = validateParamRequest(paramRequest.Params["delegations"]);
                    string amountsReceived = validateParamRequest(paramRequest.Params["amountsReceived"]);

                    indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                    indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                    indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                    indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                    indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                    indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 86, emptyIndexs));
                    indexs.Add(SetIndexData(amountsReceived.ToString(), 1020181, emptyIndexs));
                }
                else
                {
                    string reclaimantName = validateParamRequest(paramRequest.Params["reclaimantName"]);
                    string reclaimentType = validateParamRequest(paramRequest.Params["reclaimentType"]);
                    //Id Reclamo
                    indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                    //Id Baremo Muerte
                    indexs.Add(SetIndexData(idParent.ToString(), 1020121, emptyIndexs));
                    //Tipo de Reclamante
                    indexs.Add(SetIndexData(reclaimentType, 10317, emptyIndexs));
                    //Nombre
                    indexs.Add(SetIndexData(reclaimantName, 2706, emptyIndexs));
                }

                try
                {
                    insertResult = sResult.InsertBaremo(newresult, string.Empty, DocTypeId, indexs, paramRequest.UserId);
                    return insertResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("El parametro es nulo.");
            }
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/InsertAssociatedResult2")]
        [OverrideAuthorization]
        public InsertResult InsertAssociatedResult2(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                Results_Business RB = new Results_Business();
                Int64 DocTypeId = Int64.Parse(paramRequest.Params["entityId"].ToString());
                Int64 parentResultId = Int64.Parse(paramRequest.Params["parentResultId"].ToString());

                // Int64 idParent = Int64.Parse(paramRequest.Params["parentEntityId"].ToString());


                SResult sResult = new SResult();
                INewResult newresult = new SResult().GetNewNewResult(DocTypeId);
                List<IIndex> emptyIndexs = new List<IIndex>();
                List<IIndex> indexs = new List<IIndex>();
                InsertResult insertResult = new InsertResult();



                IndexsBusiness indexBusiness = new IndexsBusiness();
                emptyIndexs = indexBusiness.GetIndexsSchema(DocTypeId);
                indexBusiness = null;


                List<string> associatedIds = new List<string>();
                Char separator = ',';
                associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
                DataTable parentsIds = new DataTable();
                Int64 parentsId = 0;
                Int64 idReclamo = 0;
                Int64 idParent = 0;

                if (paramRequest.Params.ContainsKey("isRequest") || paramRequest.Params.ContainsKey("isDispatcher") ||
                    paramRequest.Params.ContainsKey("isRemito"))
                {
                    parentsId = RB.GetRequestNumber(Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]), parentResultId);
                }
                else
                {
                    parentsIds = RB.GetIdsToAsociatedParents(parentResultId, Int64.Parse(associatedIds[0]), Int64.Parse(associatedIds[1]));
                    idReclamo = Int64.Parse(parentsIds.Rows[0].ItemArray[0].ToString());
                    idParent = Int64.Parse(parentsIds.Rows[0].ItemArray[1].ToString());
                }


                if (paramRequest.Params.ContainsKey("isPedidoFondo"))
                {
                    string favorTo = validateParamRequest(paramRequest.Params["favorTo"]);
                    string commitmentNumber = validateParamRequest(paramRequest.Params["commitmentNumber"]);
                    string payMethod = validateParamRequest(paramRequest.Params["payMethod"]);
                    string cbu = validateParamRequest(paramRequest.Params["cbu"]);
                    string cuit = validateParamRequest(paramRequest.Params["cuit"]);
                    string concept = validateParamRequest(paramRequest.Params["concept"]);
                    string amount = validateParamRequest(paramRequest.Params["amount"]);
                    string mail = validateParamRequest(paramRequest.Params["email"]);
                    string personNumber = validateParamRequest(paramRequest.Params["personNumber"]);
                    string alternativeConcept = validateParamRequest(paramRequest.Params["alternativeConcept"]);
                    string cuitpro = validateParamRequest(paramRequest.Params["cuitpro"]);



                    indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                    indexs.Add(SetIndexData(favorTo.ToString(), 10289, emptyIndexs));
                    indexs.Add(SetIndexData(concept.ToString(), 2788, emptyIndexs));
                    indexs.Add(SetIndexData(amount.ToString(), 109, emptyIndexs));
                    indexs.Add(SetIndexData(commitmentNumber.ToString(), 10318, emptyIndexs));
                    indexs.Add(SetIndexData(payMethod.ToString(), 10251, emptyIndexs));
                    indexs.Add(SetIndexData(cbu.ToString(), 10290, emptyIndexs));
                    indexs.Add(SetIndexData(cuit.ToString(), 2, emptyIndexs));
                    indexs.Add(SetIndexData(idParent.ToString(), 1020129, emptyIndexs));
                    indexs.Add(SetIndexData(mail.ToString(), 2885, emptyIndexs));
                    indexs.Add(SetIndexData(personNumber.ToString(), 1020144, emptyIndexs));
                    indexs.Add(SetIndexData(alternativeConcept.ToString(), 1220204, emptyIndexs));
                    indexs.Add(SetIndexData(cuitpro.ToString(), 11535246, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isRequest"))
                {
                    string line = validateParamRequest(paramRequest.Params["line"]);
                    string product = validateParamRequest(paramRequest.Params["product"]);
                    string unitPrice = validateParamRequest(paramRequest.Params["unitPrice"]);
                    string measure = validateParamRequest(paramRequest.Params["measure"]);
                    string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                    string typeOfCurrency = validateParamRequest(paramRequest.Params["typeOfCurrency"]);
                    string price = validateParamRequest(paramRequest.Params["price"]);
                    string description = (paramRequest.Params.ContainsKey("description")) ? validateParamRequest(paramRequest.Params["description"]) : string.Empty;
                    string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                    string delegations = validateParamRequest(paramRequest.Params["delegations"]);

                    indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                    indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                    indexs.Add(SetIndexData(unitPrice.ToString(), 198, emptyIndexs));
                    indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                    indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                    indexs.Add(SetIndexData(typeOfCurrency.ToString(), 92, emptyIndexs));
                    indexs.Add(SetIndexData(price.ToString(), 152, emptyIndexs));
                    indexs.Add(SetIndexData(description.ToString(), 1020147, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 16, emptyIndexs));
                    indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                    indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isDispatcher"))
                {
                    string familyCode = validateParamRequest(paramRequest.Params["familyCode"]);
                    string quatity = validateParamRequest(paramRequest.Params["quatity"]);

                    indexs.Add(SetIndexData(familyCode.ToString(), 139602, emptyIndexs));
                    indexs.Add(SetIndexData(quatity.ToString(), 139609, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 139609, emptyIndexs));
                }
                else if (paramRequest.Params.ContainsKey("isRemito"))
                {
                    string line = validateParamRequest(paramRequest.Params["line"]);
                    string product = validateParamRequest(paramRequest.Params["product"]);
                    string measure = validateParamRequest(paramRequest.Params["measure"]);
                    string quantity = validateParamRequest(paramRequest.Params["quantity"]);
                    string costCenter = validateParamRequest(paramRequest.Params["costCenter"]);
                    string delegations = validateParamRequest(paramRequest.Params["delegations"]);
                    string amountsReceived = validateParamRequest(paramRequest.Params["amountsReceived"]);

                    indexs.Add(SetIndexData(line.ToString(), 149, emptyIndexs));
                    indexs.Add(SetIndexData(product.ToString(), 197, emptyIndexs));
                    indexs.Add(SetIndexData(measure.ToString(), 150, emptyIndexs));
                    indexs.Add(SetIndexData(quantity.ToString(), 151, emptyIndexs));
                    indexs.Add(SetIndexData(costCenter.ToString(), 148, emptyIndexs));
                    indexs.Add(SetIndexData(delegations.ToString(), 82, emptyIndexs));
                    indexs.Add(SetIndexData(parentsId.ToString(), 86, emptyIndexs));
                    indexs.Add(SetIndexData(amountsReceived.ToString(), 1020181, emptyIndexs));
                }
                else
                {
                    string reclaimantName = validateParamRequest(paramRequest.Params["reclaimantName"]);
                    string reclaimentType = validateParamRequest(paramRequest.Params["reclaimentType"]);
                    //Id Reclamo
                    indexs.Add(SetIndexData(idReclamo.ToString(), 2677, emptyIndexs));
                    //Id Baremo Muerte
                    indexs.Add(SetIndexData(idParent.ToString(), 1020121, emptyIndexs));
                    //Tipo de Reclamante
                    indexs.Add(SetIndexData(reclaimentType, 10317, emptyIndexs));
                    //Nombre
                    indexs.Add(SetIndexData(reclaimantName, 2706, emptyIndexs));
                }

                try
                {
                    insertResult = sResult.InsertBaremo(newresult, string.Empty, DocTypeId, indexs, paramRequest.UserId);
                    return insertResult;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("El parametro es nulo.");
            }
        }


        public IResult GetResultFromParamRequest(genericRequest paramRequest)
        {
            Int64 resultId = 0;
            Int64 entityId = 0;
            if (paramRequest.Params != null && paramRequest.Params.ContainsKey("resultId"))
            {
                resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                entityId = Int64.Parse(paramRequest.Params["entityId"].ToString());
            }
            Results_Business RB = new Results_Business();
            IResult result = RB.GetResult(resultId, entityId, true);
            return result;
        }

        private const string STR_SLSTPLAIN_SEPARATOR = "-";
        public IIndex SetIndexData(string indexValue, IIndex currIndex)
        {
            int toSplitIndex;

            currIndex.Data = indexValue.Trim();
            currIndex.DataTemp = currIndex.Data;

            if (currIndex.DropDown != IndexAdditionalType.AutoSustitución && currIndex.DropDown != IndexAdditionalType.AutoSustituciónJerarquico)
            {
                if (currIndex.Type == IndexDataType.Si_No)
                {
                    if (string.Compare(indexValue, "on") == 0)
                    {
                        currIndex.Data = "1";
                        currIndex.DataTemp = "1";
                    }
                    else
                    {
                        currIndex.Data = "0";
                        currIndex.DataTemp = "0";
                    }
                }
            }
            else
            {
                toSplitIndex = indexValue.IndexOf(STR_SLSTPLAIN_SEPARATOR);
                if (toSplitIndex > -1)
                {
                    currIndex.Data = indexValue.Substring(0, toSplitIndex + 1);
                    currIndex.DataTemp = currIndex.Data;
                    currIndex.dataDescription = indexValue.Replace(indexValue.Substring(0, toSplitIndex) + STR_SLSTPLAIN_SEPARATOR, string.Empty);
                }
                else
                {
                    currIndex.Data = indexValue.Trim();
                    currIndex.DataTemp = indexValue.Trim();
                    currIndex.dataDescription = new AutoSubstitutionBusiness().getDescription(indexValue.Trim(), currIndex.ID);
                }

                currIndex.dataDescriptionTemp = currIndex.dataDescription;
            }
            return currIndex;
        }

        public IIndex SetIndexData(string indexValue, Int64 indexid, List<IIndex> Indexs)
        {
            int toSplitIndex;

            foreach (IIndex currIndex in Indexs)
            {
                if (currIndex.ID == indexid)
                {
                    currIndex.Data = indexValue.Trim();
                    currIndex.DataTemp = currIndex.Data;

                    if (currIndex.DropDown != IndexAdditionalType.AutoSustitución && currIndex.DropDown != IndexAdditionalType.AutoSustituciónJerarquico)
                    {
                        if (currIndex.Type == IndexDataType.Si_No)
                        {
                            if (string.Compare(indexValue, "on") == 0)
                            {
                                currIndex.Data = "1";
                                currIndex.DataTemp = "1";
                            }
                            else
                            {
                                currIndex.Data = "0";
                                currIndex.DataTemp = "0";
                            }
                        }
                    }
                    else
                    {
                        toSplitIndex = indexValue.IndexOf(STR_SLSTPLAIN_SEPARATOR);
                        if (toSplitIndex > -1)
                        {
                            currIndex.Data = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[0];
                            currIndex.DataTemp = currIndex.Data;
                            currIndex.dataDescription = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[1];
                            currIndex.dataDescriptionTemp = indexValue.Split(char.Parse(STR_SLSTPLAIN_SEPARATOR))[1];
                        }
                        else
                        {
                            currIndex.Data = indexValue.Trim();
                            currIndex.DataTemp = indexValue.Trim();
                            currIndex.dataDescription = new AutoSubstitutionBusiness().getDescription(indexValue.Trim(), currIndex.ID);
                            currIndex.dataDescriptionTemp = currIndex.dataDescription;
                        }

                        currIndex.dataDescriptionTemp = currIndex.dataDescription;
                    }
                    return currIndex;
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [Route("api/search/GetUserRightToSearchWeb")]
        [OverrideAuthorization]
        public bool GetUserRightToSearchWeb(long userid)
        {
            if (userid > 0)
            {
                RightsBusiness RiB = new RightsBusiness();
                bool HasRightToSearchWeb = RiB.GetUserRights(userid, ObjectTypes.SearchWeb, RightsType.View, -1);
                return HasRightToSearchWeb;
            }
            else

                return false;
        }

        [System.Web.Http.AcceptVerbs("POST")]
        [Route("api/search/GetAllEntities")]
        


        public IHttpActionResult GetAllEntities(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                try
                {

                    if (paramRequest.Params != null)
                    {
                        Results_Business RB = new Results_Business();
                        DataTable result = RB.GetAllEntities();

                        var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                       new JsonSerializerSettings
                       { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                        return Ok(newresults);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("POST")]
        [Route("api/search/GetIndexForEntities")]
        public IHttpActionResult GetIndexForEntities(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {

                var IndexID = Int64.Parse(paramRequest.Params["IndexID"].ToString());

                try
                {
                    if (paramRequest.Params != null)
                    {
                        Results_Business RB = new Results_Business();
                        DataTable result = RB.GetIndexForEntities(IndexID);

                        var newresults = JsonConvert.SerializeObject(result, Formatting.Indented,
                       new JsonSerializerSettings
                       { PreserveReferencesHandling = PreserveReferencesHandling.Objects });
                        return Ok(newresults);
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetResultIndexs")]
        
        public List<Int64> GetResultIndexs(genericRequest paramRequest)
        {
            IResult result = GetResultFromParamRequest(paramRequest);
            Char separator = ',';
            List<string> associatedIds = new List<string>();
            associatedIds = paramRequest.Params["associatedIds"].ToString().Split(separator).ToList();
            List<Int64> idsToFilter = new List<Int64>();
            idsToFilter.Add(2677);
            idsToFilter.Add(Int64.Parse(associatedIds[0]));
            List<Int64> indexFiltered = new List<Int64>();

            foreach (var index in result.Indexs)
            {
                if (!idsToFilter.Contains(index.ID))
                {
                    indexFiltered.Add(index.ID);
                }
            }

            return indexFiltered;
        }

        public List<Int64> getIndexsFromIndexsResult(List<IIndex> Indexs, List<Int64> idsToFilter)
        {
            List<Int64> indexsWithResultValue = new List<Int64>();
            foreach (var index in Indexs)
            {
                if (!idsToFilter.Contains(index.ID))
                {
                    indexsWithResultValue.Add(index.ID);
                }
            }
            return indexsWithResultValue;
        }

        public Dictionary<Int64, string> getIndexsIdsAndNamesFromIndexsResult(List<IIndex> Indexs, List<Int64> idsToFilter)
        {
            Dictionary<Int64, string> indexsWithResultValue = new Dictionary<Int64, string>();
            foreach (var index in Indexs)
            {
                if (!idsToFilter.Contains(index.ID))
                {
                    indexsWithResultValue.Add(index.ID, index.Name);
                }
            }
            return indexsWithResultValue;
        }

        public string validateParamRequest(string paramData)
        {
            string data = null;
            if (paramData == null)
            {
                data = string.Empty;
            }
            else
            {
                data = paramData;
            }
            return data;
        }

        public string validateParam(string paramData, genericRequest paramRequest)
        {
            string data = null;
            if (paramRequest.Params.ContainsKey(paramData))
            {
                var isnull = paramRequest.Params[paramData] == null;
                data = isnull ? string.Empty : paramRequest.Params[paramData];
            }
            else
            {
                data = string.Empty;
            }
            return data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetCalendarReport")]
        [OverrideAuthorization]
        public IHttpActionResult GetCalendarReport(genericRequest paramRequest)
        {

            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {


                    List<string> AsociatedIds = new List<string>();
                    string entityId = string.Empty;
                    string titleAttribute = string.Empty;
                    string startAttribute = string.Empty;
                    string endAttribute = string.Empty;
                    string filterColumn = string.Empty;
                    string filterValue = string.Empty;
                    if (paramRequest.Params != null)
                    {
                        entityId = paramRequest.Params.ContainsKey("entityId") ? paramRequest.Params["entityId"].ToString() : "0";
                        titleAttribute = paramRequest.Params.ContainsKey("titleAttribute") ? paramRequest.Params["titleAttribute"].ToString() : "";
                        startAttribute = paramRequest.Params.ContainsKey("startAttribute") ? paramRequest.Params["startAttribute"].ToString() : "";
                        endAttribute = paramRequest.Params.ContainsKey("endAttribute") ? paramRequest.Params["endAttribute"].ToString() : "";
                        filterColumn = paramRequest.Params.ContainsKey("filterColumn") ? paramRequest.Params["filterColumn"].ToString() : "";
                        filterValue = paramRequest.Params.ContainsKey("filterValue") ? paramRequest.Params["filterValue"].ToString() : "";
                    }


                    //IResult result = RB.GetResult(resultId, entityId);
                    Results_Business RB = new Results_Business();
                    DataTable result = RB.GetCalendarRslt(entityId, titleAttribute, startAttribute, endAttribute, filterColumn, filterValue);
                    //DataTable dtAsoc = null;

                    //if (result != null)
                    //{
                    //    dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsList(AsociatedIdsList, result, 100, user.ID);
                    //}

                    if (result != null)
                    {
                        List<CalendarEvents> TL = new List<CalendarEvents>();
                        //DataTable dt = new DataTable();
                        foreach (DataRow r in result.Rows)
                        {
                            CalendarEvents dto = new CalendarEvents();


                            dto.title = r["title"].ToString();
                            DateTime date = DateTime.Parse(r["start"].ToString());
                            dto.start = date.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                            //dto.end = r["LUPDATE"].ToString();
                            DateTime fdate = DateTime.Parse(r["end"].ToString());
                            dto.end = fdate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);



                            //dto.description = Int64.Parse(r["DOC_TYPE_ID"].ToString());
                            //public long UserId Int64.Parse(r["ixxxx"].tostring()); 

                            TL.Add(dto);

                        }
                        //new TimeLineDto



                        var newresults = JsonConvert.SerializeObject(TL, Formatting.Indented,
                       new JsonSerializerSettings
                       {
                           PreserveReferencesHandling = PreserveReferencesHandling.Objects
                       });

                        return Ok(newresults);
                    }
                    else
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                new HttpError(StringHelper.InvalidParameter)));
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetGraphResults")]
        [OverrideAuthorization]
        public IHttpActionResult GetGraphResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));
                try
                {
                    Int64 reportId = 0;
                    System.Collections.Hashtable conditions = null;

                    if (paramRequest.Params != null && paramRequest.Params.ContainsKey("reportId"))
                    {
                        // resultId = Int64.Parse(paramRequest.Params["resultId"].ToString());
                        reportId = Int64.Parse(paramRequest.Params["reportId"].ToString());
                    }


                    Zamba.ReportBuilder.Business.ReportBuilderComponent RB = new Zamba.ReportBuilder.Business.ReportBuilderComponent();
                    DataSet DS = RB.EvaluationRunWebQueryBuilder(reportId, true, conditions, null);
                    DataTable DT = DS.Tables[0];

                    var newresults = JsonConvert.SerializeObject(DT, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;

        }

        [HttpPost]
        [Route("api/search/GetZvarTableResult")]
        
        public IHttpActionResult GetZvarTableResult(genericRequest paramRequest)
        {
            try
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return BadRequest("Invalid user");

                var userId = paramRequest.UserId;
                var taskId = Int64.Parse(paramRequest.Params["taskId"].ToString());
                var zvarName = paramRequest.Params["zvarName"].ToString();

                var repo = new ZVarsRulesRepo();
                var data = repo.GetByVarName(userId, taskId, zvarName);

                var result = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Ok(result);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return InternalServerError(new Exception("Ha ocurrido un error al obtener valor de variable"));
            }
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/getAssociatedResults")]
        [OverrideAuthorization]
        public IHttpActionResult getAssociatedResults(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                try
                {
                    STasks stasks = new STasks();
                    Int64 parentTaskId = 0;
                    Int64 parentResultId = 0;
                    Int64 parentEntityId = 0;
                    List<string> AsociatedIds = new List<string>();

                    parentResultId = Int64.Parse(paramRequest.Params["parentResultId"].ToString());
                    parentEntityId = Int64.Parse(paramRequest.Params["parentEntityId"].ToString());
                    parentTaskId = Int64.Parse(paramRequest.Params["parentTaskId"].ToString());
                    if (parentEntityId == 0)
                    {
                        parentEntityId = stasks.GetDocTypeId(parentTaskId);
                    }
                    AsociatedIds.AddRange(paramRequest.Params["AsociatedIds"].ToString().Split(char.Parse(",")));


                    Results_Business RB = new Results_Business();
                    IResult result = RB.GetResult(parentResultId, parentEntityId, true);

                    //DataTable dtAsoc = null;

                    //if (result != null)
                    //{
                    //    dtAsoc = Zamba.Core.DocTypes.DocAsociated.DocAsociatedBusiness.getAsociatedResultsFromResultAsDT(result, 100, AsociatedIds, user.ID);
                    //}


                    SDocAsociated sda = new SDocAsociated();
                    DataTable AsociatedResults = new DataTable();

                    Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();

                    searchResult sr = new searchResult();
                    sr.columnsStringAssociated = UP.getValue("columnStringAssociated-" + parentEntityId + "-" + String.Join("-", AsociatedIds), UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);

                    var customUserAssociatedOrderBy = UP.getValue("customUserAssociatedOrderBy-" + parentEntityId + "-" + String.Join("-", AsociatedIds), UPSections.UserPreferences, "", MembershipHelper.CurrentUser.ID);

                    if (customUserAssociatedOrderBy != string.Empty)
                    {
                        sr.OrderBy = customUserAssociatedOrderBy;
                    }

                    foreach (string DocTypeId in AsociatedIds)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, $"Buscando en Entidad {DocTypeId}");

                        if (AsociatedResults.Rows.Count == 0)
                            AsociatedResults = sda.getAsociatedResultsFromResultAsList(Int64.Parse(DocTypeId), result, Zamba.Membership.MembershipHelper.CurrentUser.ID);
                        else
                            AsociatedResults.Merge(sda.getAsociatedResultsFromResultAsList(Int64.Parse(DocTypeId), result, Zamba.Membership.MembershipHelper.CurrentUser.ID), true, MissingSchemaAction.Ignore);

                        sr.entities.Add(new EntityDto() { id = Int64.Parse(DocTypeId), name = new DocTypesBusiness().GetDocTypeName(Int64.Parse(DocTypeId)), enabled = true });
                    }

                    sda = null;

                    List<string> ColumnsToRemove = new List<string>();
                    foreach (DataColumn c in AsociatedResults.Columns)
                    {
                        if ((c.ColumnName.ToLower().StartsWith("i") && IsNumeric(c.ColumnName.Remove(0, 1))) || (GridColumns.ColumnsVisibility.ContainsKey(c.ColumnName.ToLower()) && GridColumns.ColumnsVisibility[c.ColumnName.ToLower()] == false))
                        {
                            ColumnsToRemove.Add(c.ColumnName);
                        }
                    }
                    //Remuevo las columnas
                    if (ColumnsToRemove.Count > 0)
                    {
                        foreach (string colName in ColumnsToRemove)
                            if (colName != "ICON_ID")
                            {
                                AsociatedResults.Columns.Remove(colName);
                            }
                    }

                    // Cambia el nombre por el alias para mostrar en la grilla
                    foreach (var item in GridColumns.ZambaColumns)
                    {
                        if (AsociatedResults.Columns.Contains(item.Value))
                            AsociatedResults.Columns[item.Value].ColumnName = item.Key;
                    }


                    foreach (DataColumn c in AsociatedResults.Columns)
                    {
                        c.ColumnName = c.ColumnName.Replace(" ", "_").Replace("-", "_").Replace("%", "").Replace("/", "_").Replace("._", "_").Replace("*", "_").Replace("__", "_");
                    }

                    String CustomOrderBy = string.Empty;
                    string tempOrderBy = string.Empty;
                    string sortOp = string.Empty;


                    if (!string.IsNullOrEmpty(customUserAssociatedOrderBy))
                    {
                        sr.OrderBy = customUserAssociatedOrderBy;

                        if (customUserAssociatedOrderBy.ToLower().Contains(" desc"))
                            sortOp = "desc";
                        else
                            sortOp = "asc";

                        var indexof = customUserAssociatedOrderBy.IndexOf(sortOp);
                        if (indexof == -1) indexof = customUserAssociatedOrderBy.Length;
                        tempOrderBy = customUserAssociatedOrderBy.Substring(0, indexof).ToString().Trim();
                        tempOrderBy = GridColumns.GetColumnNameByAliasName(tempOrderBy);

                        if (tempOrderBy.Contains("_"))
                        {
                            tempOrderBy = tempOrderBy.Replace("_", " ");

                            if (tempOrderBy.Split().Length > 1)
                                tempOrderBy = string.Format("{0}", tempOrderBy.Trim());
                        }

                        //Se pregunta si el result tiene filas y si esta filtrando por columna

                        if (Zamba.Servers.Server.isOracle)
                        {
                            CustomOrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                        }
                        else
                        {
                            CustomOrderBy = string.Format("{0} {1}", tempOrderBy.Replace(" ", "_").Replace("\"", ""), sortOp);
                        }
                    }

                    if (AsociatedResults.Rows.Count > 0 && CustomOrderBy.Length > 0)
                    {
                        AsociatedResults.DefaultView.Sort = CustomOrderBy;
                        AsociatedResults.AcceptChanges();

                        sr.data = AsociatedResults.DefaultView.ToTable();
                    }
                    else
                    {
                        sr.data = AsociatedResults;
                    }

                    var newresults = JsonConvert.SerializeObject(sr, Formatting.Indented, new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }

        [AcceptVerbs("GET", "POST")]
        [Route("api/search/GetUsersOrGroupsById")]
        [OverrideAuthorization]
        public IHttpActionResult GetUsersOrGroupsById(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                try
                {
                    var usersGroupsIds = paramRequest.Params["usersGroupsIds"].Split(',');
                    Boolean GetOneMemberIngroup = false;

                    if (paramRequest.Params.ContainsKey("GetOneMemberIngroup"))
                    {
                        GetOneMemberIngroup = Boolean.Parse(paramRequest.Params["GetOneMemberIngroup"]);
                    }

                    UserGroupBusiness UGB = new UserGroupBusiness();
                    string userGroupName;
                    string userImg;
                    List<UserGroupDTO> usersGroups = new List<UserGroupDTO>();

                    foreach (string id in usersGroupsIds)
                    {
                        bool IsGroup = false;
                        userGroupName = UGB.GetUserOrGroupNamebyIdNonShared(long.Parse(id), ref IsGroup);

                        if (IsGroup && GetOneMemberIngroup)
                        {
                            var users = UGB.GetUsersByGroup(long.Parse(id));
                            if (users != null && users.Count == 1)
                            {
                                userImg = GetBase64Photo(users[0].ID);

                            }
                            else
                            {
                                userImg = GetBase64Photo(long.Parse(id));
                            }
                        }
                        else
                        {
                            userImg = GetBase64Photo(long.Parse(id));
                        }

                        usersGroups.Add(new UserGroupDTO { Id = int.Parse(id), Name = userGroupName.Replace("Zamba", "").Replace("_", " "), Image = userImg });
                    }

                    var UsersOrGroupsResult = JsonConvert.SerializeObject(usersGroups, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                    return Ok(UsersOrGroupsResult);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }
            return null;
        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetResultsByUserConfigModule")]
        [OverrideAuthorization]
        public IHttpActionResult GetResultsByUserConfigModule(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                //CommonFuntions cf = new CommonFuntions();

                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                try
                {
                    Zamba.Core.UserPreferences up = new Zamba.Core.UserPreferences();



                    var analize = false;
                    var entities = GetEntitisModule(user.ID);
                    //Dictionary<string,string> ListEntities = new Dictionary<string, string>();
                    List<KeyValuePair<string, string>> ListEntities = new List<KeyValuePair<string, string>>();

                    foreach (var item in entities)
                    {

                        var listEntities = new KeyValuePair<string, string>(item.id.ToString(), item.name);
                        ListEntities.Add(listEntities);
                    }

                    DataTable LoadConfig = up.LoadAllUserAndGroupConfigValuesForModule(user.ID);

                    Dictionary<string, object> DataForWiew = new Dictionary<string, object>();


                    string ListName = "ShowAllTasks,AllTasksText,ShowMyAllTeamTasks,MyAllTeamTasksText,ShowMyTasks,MyTasksText,ShowMyTeamTasks, MyTeamTasksText,MyTasksEntities,IdsAllTasks,MyAllTeamEntities,MyTeamEntities";

                    foreach (DataRow row in LoadConfig.Rows)
                    {
                        if (ListName.Contains(row.ItemArray[1].ToString()))
                        {
                            if (row.ItemArray[1].ToString() == "IdsAllTasks")
                            {
                                List<CheckListObject> IdsAllValue = new List<CheckListObject>();
                                if (row.ItemArray[3].ToString() == "")
                                {
                                    foreach (var ls in ListEntities)
                                    {
                                        IdsAllValue.Add(new CheckListObject { Id = int.Parse(ls.Key), Name = ls.Value, visible = "" });
                                    }
                                    DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);

                                }
                                if (row.ItemArray[3].ToString() != "")
                                {
                                    var list = row.ItemArray[3].ToString();
                                    string[] ArrayList;
                                    ArrayList = list.Split(',');
                                    foreach (var itemEntities in ListEntities)
                                    {
                                        analize = false;
                                        foreach (var itemArrary in ArrayList)
                                        {
                                            if (itemArrary == itemEntities.Key.ToString())
                                            {
                                                IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "true" });
                                                analize = true;
                                            }

                                        }
                                        if (analize == false)
                                        {
                                            IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "" });
                                        }

                                    }
                                    DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);
                                }

                            }
                            else
                            {
                                if (row.ItemArray[1].ToString() == "MyTasksEntities")
                                {
                                    List<CheckListObject> IdsAllValue = new List<CheckListObject>();
                                    if (row.ItemArray[3].ToString() == "")
                                    {
                                        foreach (var ls in ListEntities)
                                        {
                                            IdsAllValue.Add(new CheckListObject { Id = int.Parse(ls.Key), Name = ls.Value, visible = "" });
                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);

                                    }
                                    if (row.ItemArray[3].ToString() != "")
                                    {
                                        var list = row.ItemArray[3].ToString();
                                        string[] ArrayList;
                                        ArrayList = list.Split(',');



                                        foreach (var itemEntities in ListEntities)
                                        {
                                            analize = false;
                                            foreach (var itemArrary in ArrayList)
                                            {
                                                if (itemArrary == itemEntities.Key.ToString())
                                                {
                                                    IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "true" });
                                                    analize = true;
                                                }

                                            }
                                            if (analize == false)
                                            {
                                                IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "" });
                                            }

                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);
                                    }

                                }
                                else if (row.ItemArray[1].ToString() == "MyTeamEntities")
                                {
                                    List<CheckListObject> IdsAllValue = new List<CheckListObject>();
                                    if (row.ItemArray[3].ToString() == "")
                                    {
                                        foreach (var ls in ListEntities)
                                        {
                                            IdsAllValue.Add(new CheckListObject { Id = int.Parse(ls.Key), Name = ls.Value, visible = "" });
                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);

                                    }
                                    if (row.ItemArray[3].ToString() != "")
                                    {
                                        var list = row.ItemArray[3].ToString();
                                        string[] ArrayList;
                                        ArrayList = list.Split(',');



                                        foreach (var itemEntities in ListEntities)
                                        {
                                            analize = false;
                                            foreach (var itemArrary in ArrayList)
                                            {
                                                if (itemArrary == itemEntities.Key.ToString())
                                                {
                                                    IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "true" });
                                                    analize = true;
                                                }

                                            }
                                            if (analize == false)
                                            {
                                                IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "" });
                                            }

                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);
                                    }

                                }
                                else if (row.ItemArray[1].ToString() == "MyAllTeamEntities")
                                {
                                    List<CheckListObject> IdsAllValue = new List<CheckListObject>();
                                    if (row.ItemArray[3].ToString() == "")
                                    {
                                        foreach (var ls in ListEntities)
                                        {
                                            IdsAllValue.Add(new CheckListObject { Id = int.Parse(ls.Key), Name = ls.Value, visible = "" });
                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);

                                    }
                                    if (row.ItemArray[3].ToString() != "")
                                    {
                                        var list = row.ItemArray[3].ToString();
                                        string[] ArrayList;
                                        ArrayList = list.Split(',');



                                        foreach (var itemEntities in ListEntities)
                                        {
                                            analize = false;
                                            foreach (var itemArrary in ArrayList)
                                            {
                                                if (itemArrary == itemEntities.Key.ToString())
                                                {
                                                    IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "true" });
                                                    analize = true;
                                                }

                                            }
                                            if (analize == false)
                                            {
                                                IdsAllValue.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "" });
                                            }

                                        }
                                        DataForWiew.Add(row.ItemArray[1].ToString(), IdsAllValue);
                                    }

                                }
                                else
                                {
                                    DataForWiew.Add(row.ItemArray[1].ToString(), row.ItemArray[3].ToString());
                                }

                            }

                        }
                    }
                    List<CheckListObject> IdsAllVal = new List<CheckListObject>();
                    foreach (var itemEntities in ListEntities)
                    {
                        IdsAllVal.Add(new CheckListObject { Id = int.Parse(itemEntities.Key), Name = itemEntities.Value.ToString(), visible = "" });
                    }
                    DataForWiew.Add("ListEntities", IdsAllVal);


                    var newresults = JsonConvert.SerializeObject(DataForWiew, Formatting.Indented,
              new JsonSerializerSettings
              {
                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
              });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }

            }
            return null;

        }


        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("api/search/GetInsertByUserConfigModule")]
        [OverrideAuthorization]
        public IHttpActionResult GetInsertByUserConfigModule(genericRequest paramRequest)
        {
            if (paramRequest != null)
            {
                //CommonFuntions cf = new CommonFuntions();

                var user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                try
                {

                    string Indexs = paramRequest.Params["arrayList"];
                    List<IndexValueGrid> IndexsObjectGrid = new JavaScriptSerializer().Deserialize<List<IndexValueGrid>>(Indexs);
                    ;
                    ;
                    for (int i = 0; i < IndexsObjectGrid.Count; i++)
                    {
                        if (IndexsObjectGrid[i].value.ToString().Contains("System"))
                        {
                            string ListaDeArrays = string.Empty;

                            for (int j = 0; j < ((object[])IndexsObjectGrid[i].value).Length; j++)
                            {
                                //if (j == ((object[])IndexsObjectGrid[i].value).Length)
                                //{
                                if (ListaDeArrays == "")
                                {
                                    ListaDeArrays = ((object[])IndexsObjectGrid[i].value)[j].ToString();
                                }
                                else
                                {
                                    ListaDeArrays = ListaDeArrays + "," + ((object[])IndexsObjectGrid[i].value)[j];
                                }

                                //}
                                //else
                                //{
                                //    ListaDeArrays = ListaDeArrays + "," + ((object[])IndexsObjectGrid[i].value)[j];
                                //}

                            }

                            Zamba.Core.UserPreferences.setValue(IndexsObjectGrid[i].Key.ToString(), ListaDeArrays, UPSections.UserPreferences, user.ID);
                        }
                        else
                        {
                            Zamba.Core.UserPreferences.setValue(IndexsObjectGrid[i].Key.ToString(), IndexsObjectGrid[i].value.ToString(), UPSections.UserPreferences, user.ID);
                        }

                    }



                    var newresults = JsonConvert.SerializeObject(IndexsObjectGrid, Formatting.Indented,
              new JsonSerializerSettings
              {
                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
              });
                    return Ok(newresults);
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
               new HttpError(StringHelper.InvalidParameter)));
                }

            }
            return null;

        }

        public class IndexValueGrid
        {
            public string Key { get; set; }
            public object value { get; set; }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/search/GetUserOrGroupAvatarById")]
        [OverrideAuthorization]
        public IHttpActionResult GetUserOrGroupAvatarById(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                var userGroupId = paramRequest.Params["userGroupId"];
                string userPhoto = GetBase64Photo(long.Parse(userGroupId));

                var userPhotoResult = JsonConvert.SerializeObject(userPhoto, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Ok(userPhotoResult);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/search/GetUsersOrGroupsAvatarsByIds")]
        [OverrideAuthorization]
        public IHttpActionResult GetUsersOrGroupsAvatarsByIds(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                var usersGroupsIds = paramRequest.Params["usersGroupsIds"].Split(',');
                List<string> usersPhotos = new List<string>();

                foreach (string id in usersGroupsIds)
                    usersPhotos.Add(GetBase64Photo(long.Parse(id)));

                var usersPhotosResult = JsonConvert.SerializeObject(usersPhotos, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Ok(usersPhotosResult);

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        private List<Entity> GetEntitisModule(long user)
        {
            RightsSchema rightsSchema = new RightsSchema();
            try
            {

                var entities = rightsSchema.GetEntities(user, FillTypes.WithIndexs);
                return entities;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;

            }
            finally
            {
                rightsSchema = null;
            }

        }

        private string GetBase64Photo(long userId)
        {

            string B64Photo = string.Empty;
            if (Zamba.Core.Cache.UsersAndGroups.hsUserPhotos.ContainsKey(userId) == false)
            {
                UserBusiness usrB = new UserBusiness();
                string userPhotoPath = usrB.GetUserPhotoPathById(userId);
                Zamba.FileTools.Base64 base64 = new Zamba.FileTools.Base64();
                B64Photo = base64.PathToBase64(userPhotoPath);
                Zamba.Core.Cache.UsersAndGroups.hsUserPhotos.Add(userId, B64Photo);
            }
            else
                B64Photo = Zamba.Core.Cache.UsersAndGroups.hsUserPhotos[userId].ToString();
            return B64Photo;
        }


        //Obtiene las variables interreglas, lo convierte en un objeto JSON y lo devuelve.
        public string VariablesInterreglesToJson(ICollection keyList)
        {
            Dictionary<string, object> Lista_VariablesInterReglas = new Dictionary<string, object>();
            foreach (var item in keyList)
            {
                Lista_VariablesInterReglas.Add(item.ToString(), item != "" ? VariablesInterReglas.get_Item(item).ToString() : "");
            }

            return JsonConvert.SerializeObject(Lista_VariablesInterReglas, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
        }

        public string HelperCommas(string value)
        {
            try
            {
                value = value.Replace(",", "");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                //return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }

            return value;
        }





        [HttpGet]
        [Route("api/search/GetThumbsPath")]
        [OverrideAuthorization]
        public IHttpActionResult GetThumbsPath()
        {
            try
            {
                string fileServerPath = string.Empty;

                Zamba.Core.ZOptBusiness zOptBusiness = new Zamba.Core.ZOptBusiness();
                fileServerPath = zOptBusiness.GetValueOrDefaultNonShared("FileServerUrl", "http://imageapp/ZambaWeb.FS");

                return Ok(fileServerPath);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }


        }


        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        [Route("api/search/GetThumbsPathHome")]
        [isGenericRequest]
        public IHttpActionResult GetThumbsPathHome(genericRequest paramRequest)
        {
            try
            {
                if (paramRequest == null)
                    return null;

                IUser user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                string usersPhotos;
                usersPhotos = GetBase64Photo(user.ID);

                var usersPhotosResult = JsonConvert.SerializeObject(usersPhotos, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });

                return Ok(usersPhotos);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }


        }





    }

    class MsgData
    {
        public string id { get; set; }
        public string body { get; set; }
        public string date { get; set; }
        public string subject { get; set; }
        public string from { get; set; }
        public bool isMsg { get; set; }
        public List<string> to { get; set; }
        public List<object> attachs { get; set; }
    }

    class DocumentData
    {
        public string fileName { get; set; }
        public string ContentType { get; set; }
        public MsgData dataObject { get; set; }
        public byte[] data { get; set; }
        public byte[] thumbImage { get; set; }

        public string iframeID { get; set; }
    }

    /// <summary>
    /// Clase DTO para mapper un Json que contenga "data" a un DataTable. 
    /// </summary>
    class JsonDto
    {
        /// <summary>
        /// Obtiene o establece el atributo "data" de un Json.
        /// </summary>
        public DataTable data { get; set; }
    }

    /// <summary>
    /// Clase utilizada para encapsular valores traidos de una regla ejecutada.
    /// </summary>
    class OriginResultDTO
    {
        /// <summary>
        /// Obtiene o establece el resultado de 
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// Obtiene o establece las variables interreglas en formato JSON.
        /// </summary>
        public string vars { get; set; }
    }

    internal class UserGroupDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    internal class CheckListObject
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string visible { get; set; }
    }


}
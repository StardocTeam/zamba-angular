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

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/SearchWeb")]
    //[Authorize]
    [RestAPIAuthorize]
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
        #endregion

        [Route("GetTree")]

        [RestAPIAuthorize(OverrideAuthorization = true)]
        [HttpGet]
        public IHttpActionResult GetTree()
        {
            try
            {
                var user = GetUser(null);
                if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));
                var result = GetTree((int)user.ID);
                return Ok(result);

            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [Route("GetEntitiesTree")]
        [HttpGet, HttpPost]
        public IHttpActionResult GetEntitiesTree(genericRequest paramRequest)
        {
            IUser user = null;
            if (paramRequest != null)
            {
                user = GetUser(paramRequest.UserId);
                if (user == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));


                string EntitiesString = string.Empty;

                if (DocTypesAndIndexs.hsSearchEntities.ContainsKey(user.ID) == false)
                {
                    try
                    {

                        string LastSelectedNodesIds = GetLastSelectedNodesIds(user.ID);
                        DataTable entities = GetEntitiesByUser(user.ID);
                        EntitiesString = GetTreeNodeJsonString(entities, LastSelectedNodesIds);

                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);

                        throw new Exception("Error al obtener el Arbol de Busqueda: " + ex.ToString());
                    }

                    lock (DocTypesAndIndexs.hsSearchEntities)
                    {
                        if (!DocTypesAndIndexs.hsSearchEntities.ContainsKey(user.ID))
                            DocTypesAndIndexs.hsSearchEntities.Add(user.ID, EntitiesString);
                    }

                    return Ok(EntitiesString);

                }
                else
                {
                    return Ok(DocTypesAndIndexs.hsSearchEntities[user.ID].ToString());
                }

            }
            else
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                 new HttpError(StringHelper.InvalidParameter)));
            }
        }

        private string GetLastSelectedNodesIds(Int64 currentuserid)
        {
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
            string lastSelectedNodeIds = UP.getEspecificUserValue("WebSearchLastNodes", UPSections.Viewer, string.Empty, currentuserid);
            lastSelectedNodeIds = FormatNodesIds(lastSelectedNodeIds);

            return lastSelectedNodeIds;
        }

        private string FormatNodesIds(string lastSelectedNodeIds)
        {
            lastSelectedNodeIds = lastSelectedNodeIds.Replace("=", "").Replace("Section", "").Replace("Entity", "").Replace("-", "");
            if (lastSelectedNodeIds.Length == 0) lastSelectedNodeIds = "0";
            return lastSelectedNodeIds;
        }

        private DataTable GetEntitiesByUser(long userId)
        {
            StringBuilder entitiesQuery = new StringBuilder();

            entitiesQuery.Append("SELECT distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME) text, DocTypeDocGroup.orden orden ");
            entitiesQuery.Append("FROM doc_type e inner join USR_RIGHTS re on re.RTYPE = 7 and re.ADITIONAL = e.DOC_TYPE_ID AND ");
            entitiesQuery.Append($"(groupid = {userId} or groupid in (select inheritedusergroup from group_r_group where usergroup = {userId}) OR ");
            entitiesQuery.Append($"groupid in ( Select groupid from usr_r_group where usrid = {userId}) or groupid IN ");
            entitiesQuery.Append($"(select inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid = {userId} ))) ");
            entitiesQuery.Append("INNER JOIN (select * from(select doc_type_id, min(doc_order) orden from doc_type_r_doc_type_group A group by doc_type_id) q ) ");
            entitiesQuery.Append("DocTypeDocGroup ON DocTypeDocGroup.doc_type_id = e.DOC_TYPE_ID order by text");

            var Entities = Zamba.Servers.Server.get_Con().ExecuteDataset(CommandType.Text, entitiesQuery.ToString());

            Entities.Tables[0].Columns.RemoveAt(2);

            return Entities.Tables[0];
        }

        private string GetTreeNodeJsonString(DataTable entities, string lastselectednodeid)
        {
            StringBuilder EntitiesTree = new StringBuilder();
            EntitiesTree.Append("[{\"id\":-1,\"text\":\"Entidades\",\"PARENT_ID\":0,\"spriteCssClass\":\"glyphicon glyphicon-folder-open\",\"NodeType\":\"Section\",\"checked\":\"false\",\"items\":[");

            foreach (DataRow r in entities.Rows)
            {
                EntitiesTree.Append("{");
                EntitiesTree.Append($"\"id\":{r["id"].ToString()},\"text\":\"{r["text"].ToString()}\",\"EntityOrder\":1,\"ParentId\":1,\"spriteCssClass\":\"glyphicon glyphicon-file\",\"NodeType\":\"Entity\",\"checked\":\"");
                EntitiesTree.Append(lastselectednodeid.IndexOf(r["id"].ToString()) != -1 ? "true" : "false");
                EntitiesTree.Append("\"},");
            }
            if (entities.Rows.Count > 0)
            {
                EntitiesTree.Remove(EntitiesTree.Length - 1, 1);
            }
            else {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no tiene permisos para buscar ninguna entidad");
            }
            EntitiesTree.Append("]}]");

            return EntitiesTree.ToString();
        }
        public string GetTree(int currentuserid, string token = "")
        {
            Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
            try
            {
                if (Zamba.Servers.Server.isSQLServer)
                {
                    var lastselectednodeid = UP.getEspecificUserValue("WebSearchLastNodes", Zamba.UPSections.Viewer, string.Empty, currentuserid);
                    lastselectednodeid = lastselectednodeid.Replace("=", "");
                    lastselectednodeid = lastselectednodeid.Replace("Section", "");
                    lastselectednodeid = lastselectednodeid.Replace("Entity", "");
                    lastselectednodeid = lastselectednodeid.Replace("-", "");
                    if (lastselectednodeid.Length == 0) lastselectednodeid = "0";
                    StringBuilder querybldr = new StringBuilder();
                    querybldr.Append("SET ARITHABORT ON SET QUOTED_IDENTIFIER ON declare @userid numeric(18); declare @lastselectednodeid nvarchar(MAX); declare @q nvarchar(MAX); set @userid = {0};");
                    querybldr.Append(" set @lastselectednodeid = '{1}'; Declare @SQL nvarchar(MAX); DECLARE @xml AS XML;");
                    querybldr.Append(" set @sql = N'set @XML = (");
                    querybldr.Append(" select( select  Secciones1.*, (");
                    querybldr.Append(" Select distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME) text, r.DOC_ORDER EntityOrder, r.DOC_TYPE_GROUP ParentId, ''glyphicon glyphicon-file'' as spriteCssClass, ''Entity'' as NodeType, (CASE WHEN convert(nvarchar, e.DOC_TYPE_ID) in (' + @lastselectednodeid + ')  THEN ''true'' ELSE ''false'' END) as checked from DOC_TYPE e");
                    querybldr.Append(" left join DOC_TYPE_R_DOC_TYPE_GROUP r on e.DOC_TYPE_ID = r.DOC_TYPE_ID");
                    querybldr.Append(" inner join USR_RIGHTS re on re.RTYPE = 1");
                    querybldr.Append(" and re.ADITIONAL = e.DOC_TYPE_ID");
                    querybldr.Append(" and(re.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or re.GROUPID = ' + convert(nvarchar,@userid) + ')");
                    querybldr.Append(" where Secciones1.id = r.DOC_TYPE_GROUP");
                    querybldr.Append(" for XML path (''item'') , type");
                    querybldr.Append(" ) items , (");
                    querybldr.Append(" select  S2.*, (");
                    querybldr.Append(" Select distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME)text, r.DOC_ORDER EntityOrder, r.DOC_TYPE_GROUP ParentId, ''glyphicon glyphicon-file'' as spriteCssClass, ''Entity'' as NodeType, (CASE WHEN convert(nvarchar, e.DOC_TYPE_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked    from DOC_TYPE e");
                    querybldr.Append(" left join DOC_TYPE_R_DOC_TYPE_GROUP r on e.DOC_TYPE_ID = r.DOC_TYPE_ID");
                    querybldr.Append(" inner join USR_RIGHTS re on re.RTYPE = 1");
                    querybldr.Append(" and re.ADITIONAL = e.DOC_TYPE_ID");
                    querybldr.Append(" and(re.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or re.GROUPID = ' + convert(nvarchar,@userid) + ')");
                    querybldr.Append(" where S2.id = r.DOC_TYPE_GROUP for XML path (''item'') , type) items , (");
                    querybldr.Append(" select  S3.*, (");
                    querybldr.Append(" Select distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME)text, r.DOC_ORDER EntityOrder, r.DOC_TYPE_GROUP ParentId, ''glyphicon glyphicon-file'' as spriteCssClass, ''Entity'' as NodeType, (CASE WHEN convert(nvarchar, e.DOC_TYPE_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked   from DOC_TYPE e");
                    querybldr.Append(" left join DOC_TYPE_R_DOC_TYPE_GROUP r on e.DOC_TYPE_ID = r.DOC_TYPE_ID");
                    querybldr.Append(" inner join USR_RIGHTS re on re.RTYPE = 1");
                    querybldr.Append(" and re.ADITIONAL = e.DOC_TYPE_ID and (re.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or re.GROUPID = ' + convert(nvarchar,@userid) + ')");
                    querybldr.Append(" where S3.id = r.DOC_TYPE_GROUP ");
                    querybldr.Append(" for XML path (''item'') , type ) items");
                    querybldr.Append(" , (select  S4.*, (");
                    querybldr.Append(" Select distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME)text, r.DOC_ORDER EntityOrder, r.DOC_TYPE_GROUP ParentId, ''glyphicon glyphicon-file'' as spriteCssClass, ''Entity'' as NodeType, (CASE WHEN convert(nvarchar, e.DOC_TYPE_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked   from DOC_TYPE e");
                    querybldr.Append(" left join DOC_TYPE_R_DOC_TYPE_GROUP r on e.DOC_TYPE_ID = r.DOC_TYPE_ID");
                    querybldr.Append(" inner join USR_RIGHTS re on re.RTYPE = 1 and re.ADITIONAL = e.DOC_TYPE_ID");
                    querybldr.Append(" and (re.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or re.GROUPID = ' + convert(nvarchar,@userid) + ')");
                    querybldr.Append(" where S4.id = r.DOC_TYPE_GROUP");
                    querybldr.Append(" for XML path (''item'') , type ) items");
                    querybldr.Append(" from(select distinct s.DOC_TYPE_GROUP_ID as id, s.DOC_TYPE_GROUP_NAME as text, s.PARENT_ID, ''glyphicon glyphicon-folder-open'' as spriteCssClass, ''Section'' as NodeType, (CASE WHEN convert(nvarchar, s.DOC_TYPE_GROUP_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked  from doc_type_group s");
                    querybldr.Append(" inner join USR_RIGHTS rs on rs.RTYPE = 1 and rs.ADITIONAL = DOC_TYPE_GROUP_ID");
                    querybldr.Append(" and(rs.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or rs.GROUPID = ' + convert(nvarchar,@userid) + ')) S4");
                    querybldr.Append(" where S3.id = PARENT_ID ");
                    querybldr.Append(" for XML path (''item''), type ) items");
                    querybldr.Append(" from(select distinct s.DOC_TYPE_GROUP_ID as id, s.DOC_TYPE_GROUP_NAME as text, s.PARENT_ID, ''glyphicon glyphicon-folder-open'' as spriteCssClass, ''Section'' as NodeType, (CASE WHEN convert(nvarchar, s.DOC_TYPE_GROUP_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked  from doc_type_group s");
                    querybldr.Append(" inner join USR_RIGHTS rs on rs.RTYPE = 1 and rs.ADITIONAL = DOC_TYPE_GROUP_ID");
                    querybldr.Append(" and (rs.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or rs.GROUPID = ' + convert(nvarchar,@userid) + ')) S3");
                    querybldr.Append(" where S2.id = PARENT_ID for XML path (''item''), type) items");
                    querybldr.Append(" from(select distinct s.DOC_TYPE_GROUP_ID as id, s.DOC_TYPE_GROUP_NAME as text, s.PARENT_ID, ''glyphicon glyphicon-folder-open'' as spriteCssClass, ''Section'' as NodeType, (CASE WHEN convert(nvarchar, s.DOC_TYPE_GROUP_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked  from doc_type_group s");
                    querybldr.Append(" inner join USR_RIGHTS rs on rs.RTYPE = 1");
                    querybldr.Append(" and rs.ADITIONAL = DOC_TYPE_GROUP_ID and(rs.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or rs.GROUPID = ' + convert(nvarchar,@userid) + ')) S2");
                    querybldr.Append(" where Secciones1.id = PARENT_ID ");
                    querybldr.Append(" for XML path (''item''), type) items");
                    querybldr.Append(" from(select distinct s.DOC_TYPE_GROUP_ID as id, s.DOC_TYPE_GROUP_NAME as text, s.PARENT_ID, ''glyphicon glyphicon-folder-open'' as spriteCssClass, ''Section'' as NodeType, (CASE WHEN convert(nvarchar, s.DOC_TYPE_GROUP_ID) in (' + @lastselectednodeid + ') THEN ''true'' ELSE ''false'' END) as checked   from doc_type_group s");
                    querybldr.Append(" inner join USR_RIGHTS rs on rs.RTYPE = 1");
                    querybldr.Append(" and rs.ADITIONAL = DOC_TYPE_GROUP_ID ");
                    querybldr.Append(" and (rs.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = ' + convert(nvarchar,@userid) + ') or rs.GROUPID = ' + convert(nvarchar,@userid) + ')) Secciones1");
                    querybldr.Append(" where PARENT_ID = 0 for XML path (''item''), type) as Tree)'");
                    querybldr.Append(" execute sp_executesql @sql, N'@XML XML OUTPUT', @XML OUTPUT");
                    querybldr.Append(" select ");
                    querybldr.Append(Zamba.Servers.Server.dbOwner);
                    querybldr.Append(".qfn_XmlToJson(@XML);");

                    string query = string.Format(querybldr.ToString(), currentuserid, lastselectednodeid);
                    var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, query);
                    return JsonTree.ToString();
                }
                else
                {
                    var lastselectednodeid = UP.getEspecificUserValue("WebSearchLastNodes", Zamba.UPSections.Viewer, string.Empty, currentuserid);
                    lastselectednodeid = lastselectednodeid.Replace("=", "");
                    lastselectednodeid = lastselectednodeid.Replace("Section", "");
                    lastselectednodeid = lastselectednodeid.Replace("Entity", "");
                    lastselectednodeid = lastselectednodeid.Replace("-", "");
                    if (lastselectednodeid.Length == 0) lastselectednodeid = "0";
                    StringBuilder querybldr = new StringBuilder();
                    querybldr.Append(" Select distinct e.DOC_TYPE_ID id, RTRIM(e.DOC_TYPE_NAME) text from doc_type e inner join USR_RIGHTS re on re.RTYPE = 1 and re.ADITIONAL = e.DOC_TYPE_ID and (re.GROUPID in (select ug.GROUPID from USR_R_GROUP ug where ug.USRID = {0}) or re.GROUPID = {0})");

                    string query = string.Format(querybldr.ToString(), currentuserid);
                    var JsonTree = Zamba.Servers.Server.get_Con().ExecuteScalar(CommandType.Text, query);
                    return JsonTree.ToString();
                }
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

        /// <summary>
        /// Obtiene lista de Indices para grilla de busqueda en web
        /// </summary>
        /// <param name="indexs"></param>
        /// <returns></returns>
        /// [System.Web.Http.AcceptVerbs("GET", "POST")]
        [Route("GetIndexs")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
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

        [Route("GetResults")]
        [RestAPIAuthorize(OverrideAuthorization = true)]
        [HttpPost]
        public IHttpActionResult GetResults(SearchDto searchDto)
        {
            DocTypesBusiness DTB = new DocTypesBusiness();
            try
            {
                var user = GetUser(searchDto.UserId);
                if (user == null) return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidUser)));

                TinyMapper.Bind<SearchDto, Search>();
                var search = TinyMapper.Map<Search>(searchDto);

                if (searchDto.DoctypesIds == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(StringHelper.IndexExpected)));

                foreach (Int64 EntityId in searchDto.DoctypesIds)
                {
                    IDocType Entity = DTB.GetDocType(EntityId);
                    search.AddDocType(Entity);
                }

                search.Indexs = new List<IIndex>();
                var searchDtoIndexs = searchDto.Indexs;

                foreach (object searchDtoIndex in searchDto.Indexs)
                {
                    var index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
                    if (index.Data != string.Empty || index.dataDescription != string.Empty)
                        search.AddIndex(index);
                }

                ModDocuments MD = new ModDocuments();
                Int64 TotalCount = 0;
                var results = MD.DoSearch(ref search, user.ID, 0, 100, false, false, true, ref TotalCount);
                var js = JsonConvert.SerializeObject(results);
                return Ok(js);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
            finally { DTB = null; }

        }

    }
}

using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using Zamba.Core;
using Zamba.Core.Search;
using System;
using Newtonsoft.Json;
using System.Linq;
using Zamba.Core.Searchs;
using Zamba;
using System.Net.Http;
using Nelibur.ObjectMapper;
using ZambaWeb.RestApi.ViewModels;
using System.Net;
using Zamba.Framework;
using Zamba.Services;
using Zamba.Membership;
using System.Web;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.Services.Description;
using System.Text.RegularExpressions;

namespace ZambaWeb.RestApi.Controllers
{


    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(HttpRequestMessage request)
        {
            //Web-hosting
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress.Replace("::1", "127.0.0.1");
                }
            }
            //Self-hosting
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }
            //Owin-hosting
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic ctx = request.Properties[OwinContext];
                if (ctx != null)
                {
                    return ctx.Request.RemoteIpAddress;
                }
            }
            if (System.Web.HttpContext.Current != null)
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");
            }
            // Always return all zeroes for any failure
            return "0.0.0.0";
        }
    }


    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ExternalSearch")]
    public class ExternalSearchController : ApiController
    {


        [AcceptVerbs("GET", "POST")]
        [Route("KeepAlive")]
        public IHttpActionResult KeepAlive()
        {
            try
            {
                HttpContext.Current.Session["SessionRefreshToken"] = DateTime.Now;
                return Ok("Hi");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Error en KeepAlive: " + ex.ToString())));
            }
        }



        [AcceptVerbs("GET", "POST")]
        [Route("Login")]
        public IHttpActionResult Login(LoginVM loginVM, HttpRequestMessage Request)
        {
            try
            {
                //string ip = HttpRequestMessageExtensions.GetClientIpAddress(Request);
                //loginVM.ComputerNameOrIp = ip;
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo Login");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.UserName);
                loginVM.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.ComputerNameOrIp);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            }

            if (loginVM.UserName == null || loginVM.UserName == string.Empty)
            {
                ZClass.raiseerror(new Exception("El usuario es nulo"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("El usuario es nulo")));
            }
            if (loginVM.Password == null || loginVM.Password == string.Empty)
            {
                ZClass.raiseerror(new Exception("La clave es nula"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("La clave es nula")));
            }


            try
            {
                IUser user = null;
                try
                {
                    UserBusiness UB = new UserBusiness();
                    user = UB.GetUserByname(loginVM.UserName, false);
                    //HttpRequestMessageExtensions.GetClientIpAddress();
                    if (user == null)
                    {

                        IUser newuser = new User();
                        newuser.Name = loginVM.UserName;
                        newuser.Password = loginVM.Password;

                        try
                        {
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController Creacion de Usuario nuevo");
                            if (loginVM.name != null && loginVM.name != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.name);
                                newuser.Nombres = loginVM.name;
                            }
                            else
                                newuser.Nombres = loginVM.UserName;

                            if (loginVM.lastName != null && loginVM.lastName != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.lastName);
                                newuser.Apellidos = loginVM.lastName;
                            }
                            else
                                newuser.Apellidos = loginVM.UserName;

                            if (loginVM.eMail != null && loginVM.eMail != string.Empty)
                            {
                                ZTrace.WriteLineIf(ZTrace.IsVerbose, loginVM.eMail);

                                short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                                string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                                bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));

                                newuser.eMail = new Correo()
                                {
                                    Mail = loginVM.eMail,
                                    UserName = loginVM.eMail,
                                    EnableSsl = enablessl,
                                    ProveedorSMTP = smtpProvider,
                                    Type = MailTypes.NetMail,
                                    Puerto = mailPort
                                };
                            }



                            newuser.ID = Zamba.Data.CoreData.GetNewID(IdTypes.USERTABLEID);
                            UB.AddUser(newuser);
                            UB.SetNewUser(ref newuser);
                            UserPreferences UP = new UserPreferences();
                            Int32 TraceLevel = Int32.Parse(UP.getValue("TraceLevel", UPSections.UserPreferences, 4, user.ID));
                            user.TraceLevel = TraceLevel;
                            MembershipHelper.SetCurrentUser(newuser);


                            user = UB.ValidateLogIn(loginVM.UserName, loginVM.Password ?? string.Empty, ClientType.Service);
                            UB = null;

                            ZOptBusiness zopt = new ZOptBusiness();

                            string GroupIds = ZOptBusiness.GetValueOrDefault("DefaultGroupForExternals", "11526386");

                            try
                            {
                                long groupId = Int64.Parse(GroupIds);
                                if (groupId != 0)
                                {
                                    UB.AssignGroup(user.ID, groupId);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        catch (Exception e)
                        {
                            ZClass.raiseerror(e);
                            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error al dar de alta el usuario");
                            return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(e.ToString())));
                        }
                    }
                    else
                    {
                        user = UB.ValidateLogIn(loginVM.UserName, loginVM.Password ?? string.Empty, ClientType.Service);
                        UB = null;
                    }
                }
                catch (Exception ex)
                {
                    ZClass.raiseerror(ex);
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
                }


                var tokenString = GetTokenString(loginVM, user);

                var userInfo = new
                {
                    token = tokenString.SelectToken(@"access_token").Value<string>(),
                    tokenExpire = tokenString.SelectToken(@"expiredate").Value<string>(),
                    connectionId = tokenString.SelectToken(@"connectionId").Value<string>(),
                    userID = EncriptString(user.ID.ToString())
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError(ex.ToString())));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("SearchResults")]
        public IHttpActionResult SearchResults(SearchDto searchDto)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo SearchResults");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, searchDto.ExternUserID.ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Request.Headers.GetValues("Authorization").First());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Concat(searchDto.entities.ToString()));
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Concat(searchDto.Indexs.ToString()));

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            }

            string token;
            try
            {
                if (Request.Headers.Authorization == null || string.IsNullOrEmpty(Request.Headers.Authorization.ToString()))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token Nulo")));

                token = Request.Headers.Authorization.ToString();

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new HttpError("No se pudo obtener el token")));
            }


            if (searchDto.Indexs == null || searchDto.Indexs.Count <= 0)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Coleccion de indices vacia")));

            string _userId = DecryptString(searchDto.ExternUserID.ToString());
            if (String.IsNullOrEmpty(_userId))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

            long userID = long.Parse(_userId);


            DocTypesBusiness DTB = new DocTypesBusiness();
            UserBusiness UB = new UserBusiness();
            IndexsBusiness IB = new IndexsBusiness();
            Zamba.FUS.EncryptBusiness EB = new Zamba.FUS.EncryptBusiness();


            try
            {
                if (!validateToken(userID, token))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token invalido")));

                IUser User = UB.ValidateLogIn(userID, ClientType.WebApi);
                if (User != null)
                {
                    TinyMapper.Bind<SearchDto, Search>();
                    var search = TinyMapper.Map<Search>(searchDto);
                    ExternalSearchResult sr = new ExternalSearchResult();

                    Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();
                    var ExternalEntities = zopt.GetValueOrDefaultNonShared("ES", EB.Encrypt("15,17,2524"));
                    ExternalEntities = EB.Decrypt(ExternalEntities);

                    var ExternalAttributes = zopt.GetValueOrDefaultNonShared("ESA", EB.Encrypt("I29|TipoId,Tipos Doc Siniestros|Tipo"));
                    ExternalAttributes = EB.Decrypt(ExternalAttributes);

                    Dictionary<string, string> ExternalAttributesToKeep = new Dictionary<string, string>();
                    foreach (string EA in ExternalAttributes.Split(char.Parse(",")))
                    {
                        ExternalAttributesToKeep.Add(EA.Split(char.Parse("|"))[0].ToLower(), EA.Split(char.Parse("|"))[1]);
                    }


                    var EntitiesWithRights = DTB.GetDocTypesbyUserRights(userID, RightsType.Buscar);

                    foreach (Int64 EntityId in searchDto.DoctypesIds)
                    {
                        //No se permiten buscar entidades virtuales
                        if (ExternalEntities != null && ExternalEntities.Split(',').ToList().Contains(EntityId.ToString()))
                        {
                            foreach (IDocType E in EntitiesWithRights)
                            {
                                if (E.ID == EntityId)
                                {
                                    IDocType Entity = DTB.GetDocType(EntityId);
                                    sr.entities.Add(new EntityDto() { id = Entity.ID, name = Entity.Name });
                                    search.AddDocType(Entity);
                                    break;
                                }
                            }
                        }
                    }

                    if (search.Doctypes == null || search.Doctypes.Count == 0)
                    {
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Las entidad no esta habilitada para este modulo o el modulo no soporta ese tipo de entidad.")));
                    }

                    search.Indexs = new List<IIndex>();
                    IIndex index;
                    foreach (object searchDtoIndex in searchDto.Indexs)
                    {
                        index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
                        index = IB.GetIndexById(index.ID, index.Data);
                        if (index == null)
                        {
                            throw new Exception("El atributo especificado no existe");
                        }
                        index.Operator = "=";
                        search.AddIndex(index);
                    }

                    search.Filters = new List<ikendoFilter>();
                    var searchDtoFilters = searchDto.Filters;

                    if (searchDto.Filters != null)
                    {
                        foreach (object searchDtoFilter in searchDto.Filters)
                        {
                            var filter = JsonConvert.DeserializeObject<kendoFilter>(searchDtoFilter.ToString());
                            if (filter.Field != string.Empty || filter.Value != string.Empty || filter.Operator != string.Empty)
                                search.AddFilter(filter);
                        }
                    }

                    UB = null;
                    DTB = null;
                    IB = null;
                    search.UserAssignedId = -1;

                    long TotalCount = 0;
                    ModDocuments MD = new ModDocuments();
                    DataTable results = MD.DoSearch(ref search, User.ID, search.LastPage, search.PageSize, false, false, true, ref TotalCount, false);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Resultados obtenidos: " + TotalCount);

                    // Agrego la nueva columna
                    results.Columns.Add("ID", typeof(string)).SetOrdinal(0);

                    string rowValue = string.Empty;
                    //Genero el id del doc
                    foreach (DataRow row in results.Rows)
                        row["id"] = EncriptString(string.Concat(row["doc_id"].ToString(), "-", row["doc_type_id"].ToString(), "-", _userId, "-", DateTime.Today.ToString("yyyy|MM|dd|HH|mm|ss|sss")));



                    ////Genero el id del doc
                    if (searchDto.url)
                    {

                        results.Columns.Add("url", typeof(string)).SetOrdinal(0);

                        String dominio = ZOptBusiness.GetValueOrDefault("ThisDomain", "http://imageapd/zamba.web");
                        foreach (DataRow row in results.Rows)
                        {
                            string documentUrl = dominio + "Services/GetDocFile.ashx?DocTypeId=" + row["doc_type_id"] + " &DocId = " + row["doc_id"] + " &UserID = " + _userId + " &ConvertToPDf = true";
                            string newDocumentUrl = Regex.Replace(documentUrl, @"\s", "");
                            row["url"] = newDocumentUrl;
                        }
                    }

                    List<string> columnsToRemove = new List<string>();
                    //Columnas que no deben verse
                    foreach (DataColumn column in results.Columns)
                    {
                        if (ColumnsToShare.Contains(column.ColumnName.ToLower()))
                        { }
                        else if (ExternalAttributesToKeep.Keys.Contains(column.ColumnName.ToLower()))
                        {
                            column.ColumnName = ExternalAttributesToKeep[column.ColumnName.ToLower()];
                        }
                        else
                            if (column.ColumnName != "url")
                            columnsToRemove.Add(column.ColumnName);
                    }

                    //Remuevo las columnas
                    foreach (string colName in columnsToRemove)
                        results.Columns.Remove(colName);

                    results.AcceptChanges();
                    sr.data = results;

                    return Ok(sr.data);
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo realizar la busqueda" + ex.ToString())));
            }
        }





        [AcceptVerbs("GET", "POST")]
        [Route("SearchResultsForDashboard")]
        public IHttpActionResult SearchResultsForDashboard(SearchDto searchDto)
        {
            try
            {
                
                //string token = "";

                if (searchDto.Indexs == null || searchDto.Indexs.Count <= 0)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Coleccion de indices vacia")));

                long userID = long.Parse(searchDto.ExternUserID);

                UserBusiness UB = new UserBusiness();

                //if (!validateToken(userID, token))
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token invalido")));

                IUser User = UB.ValidateLogIn(userID, ClientType.WebApi);

                if (User != null)
                {
                    DocTypesBusiness DTB = new DocTypesBusiness();
                    IndexsBusiness IB = new IndexsBusiness();
                    ExternalSearchResult sr = new ExternalSearchResult();

                    Zamba.FUS.EncryptBusiness EB = new Zamba.FUS.EncryptBusiness();
                    Zamba.Core.ZOptBusiness zopt = new Zamba.Core.ZOptBusiness();

                    TinyMapper.Bind<SearchDto, Search>();
                    var search = TinyMapper.Map<Search>(searchDto);
                    long TotalCount = 0;

                    var ExternalEntities = zopt.GetValueOrDefaultNonShared("ES", EB.Encrypt("15,17,2524"));
                    ExternalEntities = EB.Decrypt(ExternalEntities);

                    var ExternalAttributes = zopt.GetValueOrDefaultNonShared("ESA", EB.Encrypt("I29|TipoId,Tipos Doc Siniestros|Tipo"));
                    ExternalAttributes = EB.Decrypt(ExternalAttributes);

                    Dictionary<string, string> ExternalAttributesToKeep = new Dictionary<string, string>();
                    foreach (string EA in ExternalAttributes.Split(char.Parse(",")))                    
                        ExternalAttributesToKeep.Add(EA.Split(char.Parse("|"))[0].ToLower(), EA.Split(char.Parse("|"))[1]);                    

                    var EntitiesWithRights = DTB.GetDocTypesbyUserRights(userID, RightsType.Buscar);

                    sr.entities = GetEntitiesWithRights(searchDto, DTB, search, ExternalEntities, EntitiesWithRights);

                    if (search.Doctypes == null || search.Doctypes.Count == 0)                    
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Las entidad no esta habilitada para este modulo o el modulo no soporta ese tipo de entidad.")));

                    SetIndexs(searchDto, IB, search);
                    Setfilters(searchDto, search);

                    UB = null;
                    DTB = null;
                    IB = null;

                    search.UserAssignedId = -1;

                    DataTable results = new ModDocuments().DoSearch(ref search, User.ID, search.LastPage, search.PageSize, false, false, true, ref TotalCount, false);
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Resultados obtenidos: " + TotalCount);

                    AddAndRemoveColumns(searchDto, searchDto.ExternUserID, ExternalAttributesToKeep, results);

                    sr.data = results;
                    return Ok(sr.data);
                }

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo realizar la busqueda" + ex.ToString())));
            }
        }

        private static void SetIndexs(SearchDto searchDto, IndexsBusiness IB, Search search)
        {
            search.Indexs = new List<IIndex>();

            foreach (Zamba.Core.Index searchDtoIndex in searchDto.Indexs)
            {
                IIndex index;
                index = IB.GetIndexById(searchDtoIndex.ID, searchDtoIndex.Data);

                if (index == null)                
                    throw new Exception("El atributo especificado no existe");                

                index.Operator = "=";
                search.AddIndex(index);
            }
        }

        private void Setfilters(SearchDto searchDto, Search search)
        {
            search.Filters = new List<ikendoFilter>();

            if (searchDto.Filters != null)
            {
                foreach (object searchDtoFilter in searchDto.Filters)
                {
                    var filter = JsonConvert.DeserializeObject<kendoFilter>(searchDtoFilter.ToString());
                    if (filter.Field != string.Empty || filter.Value != string.Empty || filter.Operator != string.Empty)
                        search.AddFilter(filter);
                }
            }
        }

        private void AddAndRemoveColumns(SearchDto searchDto, string _userId, Dictionary<string, string> ExternalAttributesToKeep, DataTable results)
        {
            // Agrego la nueva columna
            results.Columns.Add("ID", typeof(string)).SetOrdinal(0);

            //Genero el id del doc
            foreach (DataRow row in results.Rows)
                row["id"] = EncriptString(string.Concat(row["doc_id"].ToString(), "-", row["doc_type_id"].ToString(), "-", _userId, "-", DateTime.Today.ToString("yyyy|MM|dd|HH|mm|ss|sss")));

            // nuevo archivo
            results.Columns.Add("url", typeof(string)).SetOrdinal(0);

            ////Genero el id del doc
            if (searchDto.url)
            {
                String dominio = ZOptBusiness.GetValueOrDefault("ThisDomain", "http://imageapd/zamba.web");
                foreach (DataRow row in results.Rows)
                {
                    string documentUrl = dominio + "Services/GetDocFile.ashx?DocTypeId=" + row["doc_type_id"] + " &DocId = " + row["doc_id"] + " &UserID = " + _userId + " &ConvertToPDf = true";
                    string newDocumentUrl = Regex.Replace(documentUrl, @"\s", "");
                    row["url"] = newDocumentUrl;
                }
            }

            List<string> columnsToRemove = new List<string>();
            //Columnas que no deben verse
            foreach (DataColumn column in results.Columns)
            {
                if (ColumnsToShare.Contains(column.ColumnName.ToLower()))
                { }
                else if (ExternalAttributesToKeep.Keys.Contains(column.ColumnName.ToLower()))
                {
                    column.ColumnName = ExternalAttributesToKeep[column.ColumnName.ToLower()];
                }
                else
                    if (column.ColumnName != "url")
                    columnsToRemove.Add(column.ColumnName);
            }

            //Remuevo las columnas
            foreach (string colName in columnsToRemove)
                results.Columns.Remove(colName);

            results.AcceptChanges();
        }

        private static List<EntityDto> GetEntitiesWithRights(SearchDto searchDto, DocTypesBusiness DTB, Search search, string ExternalEntities, List<IDocType> EntitiesWithRights)
        {
            List<EntityDto> result = new List<EntityDto>();

            IDocType Entity = DTB.GetDocType(searchDto.DoctypesIds.First());
            result.Add(new EntityDto() { id = Entity.ID, name = Entity.Name });
            search.AddDocType(Entity);

            return result;
        }

        /// <summary>
        /// Obtiene el archivo asociado a la tarea a traves de un token.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("GetDocFile")]
        public IHttpActionResult GetDocFile(genericRequest request)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo GetDocFile");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, request.Params["externuserid"].ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Request.Headers.GetValues("Authorization").First() != null ? Request.Headers.GetValues("Authorization").First() : string.Empty);
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
                token = Request.Headers.GetValues("Authorization").First();
                if (string.IsNullOrEmpty(token))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token Nulo. Err. 7004")));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el token Err. 7005")));
            }

            byte[] _file = null;
            try
            {
                string _userId = DecryptString(request.Params["externuserid"].ToString());
                if (String.IsNullOrEmpty(_userId))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

                long userID = long.Parse(_userId);
                UserBusiness UB = new UserBusiness();

                if (!validateToken(userID, token))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));


                IUser User = UB.ValidateLogIn(userID, ClientType.WebApi);
                if (User != null)
                {

                    string _documentId = request.Params["id"].ToString();

                    long DocTypeId = 0;
                    long DocId = 0;

                    DecryptDocId(_documentId, userID, out DocId, out DocTypeId);

                    if (userID > 0 && DocTypeId > 0 && DocId > 0)
                    {
                        try
                        {
                            if (MembershipHelper.CurrentUser == null)
                            {

                                UB.ValidateLogIn(userID, ClientType.Web);
                            }
                        }
                        catch (Exception ex)
                        {
                            ZClass.raiseerror(new Exception("Error validando usuario. Err. 7006", ex));
                        }

                    }
                    else
                    {
                        ZClass.raiseerror(new Exception($"No se recibieron todos los parametros. userID {userID} DocTypeId {DocTypeId} DocId {DocId} Err. 7007"));
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se recibieron todos los parametros. Err. 7007")));
                    }


                    bool convertToPDf = true;

                    if (request.Params.ContainsKey("converttopdf"))
                        convertToPDf = bool.Parse(request.Params["converttopdf"].ToString());

                    SResult sResult = new SResult();
                    Result res = (Result)sResult.GetResult(DocId, DocTypeId, false);

                    try
                    {
                        return Ok(GetDocumentData(userID, DocTypeId, DocId, ref convertToPDf, res, false));
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(new Exception("Error al obtener el archivo. Se reintenta decode. Err. 7008", ex));
                        return Ok(System.Convert.FromBase64String(GetDocumentData(userID, DocTypeId, DocId, ref convertToPDf, res, false)));
                        throw;
                    }

                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
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

        private void DecryptDocId(string documentId, long userID, out long docId, out long doctypeId)
        {
            try
            {
                string _documentId = DecryptString(documentId);
                docId = long.Parse(_documentId.Split('-')[0]);
                doctypeId = long.Parse(_documentId.Split('-')[1]);

                long _userId = long.Parse(_documentId.Split('-')[2]);
                string _DateTime = _documentId.Split('-')[3];

                if (_userId != userID) throw new Exception($"No se reconoce ID del documento. Err 7001: {documentId}");

                DateTime _IdDateTime = new DateTime(int.Parse(_DateTime.Split(char.Parse("|"))[0]), int.Parse(_DateTime.Split(char.Parse("|"))[1]), int.Parse(_DateTime.Split(char.Parse("|"))[2]), int.Parse(_DateTime.Split(char.Parse("|"))[3]), int.Parse(
                _DateTime.Split(char.Parse("|"))[4]), int.Parse(_DateTime.Split(char.Parse("|"))[5]), int.Parse(_DateTime.Split(char.Parse("|"))[6]));
                if ((DateTime.Today - _IdDateTime).TotalHours > 24) throw new Exception($"No se reconoce ID del documento: {documentId}. Err 7002");
            }
            catch (Exception ex)
            {
                throw new Exception($"No se reconoce ID del documento. Err 7000: {documentId}", ex);
            }

        }

        /// <summary>
        /// Obtiene un archivo en formato de tipo String.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Devuelve un ActionResult</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("GetDocument")]
        public IHttpActionResult GetDocument(genericRequest request)
        {
            try
            {
                long userId = long.Parse(request.Params["userId"].ToString());
                long doctypeId = long.Parse(request.Params["doctypeId"].ToString());
                long docId = long.Parse(request.Params["docid"].ToString());

                bool convertToPDf = true;

                if (request.Params.ContainsKey("converttopdf"))
                    convertToPDf = bool.Parse(request.Params["converttopdf"].ToString());

                //if (!validateToken(userID, token))
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
                UserBusiness UB = new UserBusiness();
                IUser User = UB.ValidateLogIn(userId, ClientType.WebApi);
                if (User != null)
                {


                    SResult sResult = new SResult();
                    Result res = (Result)sResult.GetResult(docId, doctypeId, true);

                    DocumentData DD = new DocumentData();

                    string data = GetDocumentData(userId, doctypeId, docId, ref convertToPDf, res, true);
                    DD.fileName = res.Doc_File;

                    DD.ContentType = convertToPDf ? "application/pdf" : res.MimeType;

                    try
                    {
                        DD.dataObject = JsonConvert.DeserializeObject(data);
                    }
                    catch (Exception ex)
                    {
                        DD.data = System.Convert.FromBase64String(data);
                    }

                    try
                    {
                        var jsonDD = JsonConvert.SerializeObject(DD);
                        return Ok(jsonDD);
                    }
                    catch (Exception ex)
                    {
                        //return Ok(System.Convert.FromBase64String(DD));
                        //throw ;
                        var jsonDD = JsonConvert.SerializeObject(DD);
                        return Ok(jsonDD);
                    }
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el recurso" + ex.ToString())));
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
        private string GetDocumentData(long userId, long doctypeId, long docId, ref bool convertToPDf, IResult res, bool MsgPreview)
        {
            SZOptBusiness Zopt = new SZOptBusiness();
            SResult sResult = new SResult();

            try
            {
                if (MembershipHelper.CurrentUser == null)
                {
                    UserBusiness UB = new UserBusiness();
                    UB.ValidateLogIn(userId, ClientType.Web);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }




            byte[] _file = null;

            ZTrace.WriteLineIf(ZTrace.IsInfo, res.FullPath);

            if (res != null && res.FullPath != null && res.FullPath.Contains("."))
            {
                bool IsBlob = false;

                string filename = res.FullPath;
                string newPDFFile = res.FullPath + ".pdf";

                if (convertToPDf && File.Exists(newPDFFile) && res.IsMsg == false && res.IsOffice2 == false)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "convertToPDf" + convertToPDf);
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

                            var a = JsonConvert.SerializeObject(ST.ConvertMSGToJSON(res.FullPath, newPDFFile, true), Formatting.Indented,
                              new JsonSerializerSettings
                              {
                                  PreserveReferencesHandling = PreserveReferencesHandling.Objects
                              });
                            return a;
                        }
                    }

                    //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                    if (res.Disk_Group_Id > 0 && (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase || (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob")))))
                    {
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "ForceBlob" + Zopt.GetValue("ForceBlob"));
                        sResult.LoadFileFromDB(ref res);
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
                            _file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, userId);
                        else
                            _file = sResult.GetFileFromResultForWeb(res, out IsBlob);
                    }

                    if (_file != null && _file.Length > 0)
                    {
                        if (res.IsWord)
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsHTML || res.IsRTF || res.IsText || res.IsXoml))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertWordToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }

                        if ((res.IsExcel))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertExcelToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                }
                            }
                        }

                        if ((res.IsImage) && res.IsTif == false || res.FullPath.Contains(".jpeg"))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertImageToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else
                                {
                                    filename = res.FullPath;
                                }
                            }
                        }

                        if ((res.IsTif))
                        {
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
                                    newPDFFile = Path.Combine(TempDir, Path.GetFileName(res.RealFullPath())) + ".pdf";
                                    if (File.Exists(newPDFFile) == false)
                                    {
                                        string tempPDFFile = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                        FileEncode.Decode(tempPDFFile, _file);
                                        Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                        if (ST.ConvertTIFFToPDF(tempPDFFile, newPDFFile))
                                        {
                                            _file = FileEncode.Encode(newPDFFile);
                                            filename = newPDFFile;
                                        }
                                        else
                                        {
                                            string tempPDFFile1 = Path.Combine(TempDir, Path.GetFileName(res.FullPath));
                                            FileEncode.Decode(tempPDFFile1, _file);
                                            _file = FileEncode.Encode(tempPDFFile1);
                                            filename = res.FullPath;
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
                                    Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                    if (ST.ConvertTIFFToPDF(res.FullPath, newPDFFile))
                                    {
                                        _file = FileEncode.Encode(newPDFFile);
                                        filename = newPDFFile;
                                    }
                                    else { filename = res.FullPath; }
                                }
                            }
                        }

                        if ((res.IsPowerpoint))
                        {
                            if (convertToPDf)
                            {
                                Zamba.FileTools.SpireTools ST = new Zamba.FileTools.SpireTools();
                                if (ST.ConvertPowerPointToPDF(res.FullPath, newPDFFile))
                                {
                                    _file = FileEncode.Encode(newPDFFile);
                                    filename = newPDFFile;
                                }
                                else { filename = res.FullPath; }
                            }
                        }
                    }
                }

                if (_file == null || _file.Length <= 0)
                {
                    throw new Exception("No se pudo obtener el recurso (404)");
                }

                Zopt = null;
                sResult = null;

                return System.Convert.ToBase64String(_file);
            }
            throw new Exception("No se pudo obtener el recurso 404");
        }

        [AcceptVerbs("GET", "POST")]
        [Route("DeleteDocFile")]
        public IHttpActionResult DeleteDocFile(genericRequest request)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo DeleteDocFile");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, request.Params["externuserid"].ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Request.Headers.GetValues("Authorization").First() != null ? Request.Headers.GetValues("Authorization").First() : string.Empty);
                ZTrace.WriteLineIf(ZTrace.IsInfo, request.Params["id"] != null ? request.Params["id"].ToString() : string.Empty);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsError, "Error al dar de baja el resultado");
            }

            string token;
            try
            {
                token = Request.Headers.GetValues("Authorization").First();
                if (string.IsNullOrEmpty(token))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token Nulo")));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el token")));
            }


            try
            {

                string _userId = DecryptString(request.Params["externuserid"].ToString());
                if (String.IsNullOrEmpty(_userId))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

                long userID = long.Parse(_userId);

                if (!validateToken(userID, token))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));

                string _documentId = request.Params["id"].ToString();

                long DocTypeId = 0;
                long DocId = 0;

                DecryptDocId(_documentId, userID, out DocId, out DocTypeId);

                if (userID > 0 && DocTypeId > 0 && DocId > 0)
                {
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
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se recibieron todos los parametros")));
                }

                SResult sResult = new SResult();
                Result res = (Result)sResult.GetResult(DocId, DocTypeId, false);

                if (res != null && res.FullPath != null && res.FullPath.Contains("."))
                {
                    if (res.Platter_Id == userID)
                    {
                        sResult.delete(res);

                        sResult = null;

                        // Aca devolver el documento
                        return Ok("File Deleted");
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception("El usuario no es el creador del documento"));
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("El usuario no es el creador del documento")));
                    }
                }
                ZClass.raiseerror(new Exception("No se pudo obtener el recurso"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("EditDoc")]
        public IHttpActionResult EditDoc(EditDto editDto)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo SearchResults");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, editDto.ExternUserID.ToString());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Request.Headers.GetValues("Authorization").First());
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Concat(editDto.Indexs.ToString()));
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            }

            string token;
            try
            {
                if (Request.Headers.Authorization == null || string.IsNullOrEmpty(Request.Headers.Authorization.ToString()))
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Token Nulo")));

                token = Request.Headers.Authorization.ToString();

            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Unauthorized, new HttpError("No se pudo obtener el token")));
            }


            if (editDto.Indexs == null || editDto.Indexs.Count <= 0)
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Coleccion de indices vacia")));

            string _userId = DecryptString(editDto.ExternUserID.ToString());
            if (String.IsNullOrEmpty(_userId))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

            long userID = long.Parse(_userId);

            if (!validateToken(userID, token))
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError(StringHelper.InvalidUser)));

            try
            {

                string _documentId = editDto.Id.ToString();

                long DocTypeId = 0;
                long DocId = 0;

                DecryptDocId(_documentId, userID, out DocId, out DocTypeId);

                if (userID > 0 && DocTypeId > 0 && DocId > 0)
                {
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
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se recibieron todos los parametros")));
                }

                SResult sResult = new SResult();
                IResult res = sResult.GetResult(DocId, DocTypeId, true);
                IndexsBusiness IB = new IndexsBusiness();

                List<IIndex> Indexs = new List<IIndex>();
                IIndex index;
                foreach (object searchDtoIndex in editDto.Indexs)
                {
                    index = JsonConvert.DeserializeObject<Zamba.Core.Index>(searchDtoIndex.ToString());
                    index = IB.GetIndexById(index.ID, index.Data);
                    if (index == null)
                    {
                        throw new Exception("El atributo especificado no existe");
                    }
                    index.Operator = "=";
                    Indexs.Add(index);
                }

                if (res != null)
                {
                    if (res.Platter_Id == userID || Boolean.Parse(ZOptBusiness.GetValueOrDefault("ExternalSearchAllowEditNotCreator", "false")) == true)
                    {
                        Results_Business RB = new Results_Business();
                        List<Int64> onlyIndexsIds = new List<long>();
                        foreach (IIndex I in Indexs)
                        {

                            IIndex In = res.get_GetIndexById(I.ID);
                            if (In == null)
                            {
                                throw new Exception("El atributo especificado no existe");
                            }
                            In.DataTemp = I.Data;
                            if (I.Data != string.Empty)
                            {
                                onlyIndexsIds.Add(I.ID);
                            }
                        }
                        RB.SaveModifiedIndexData(ref res, true, false, onlyIndexsIds, null);

                        UserBusiness UB = new UserBusiness();
                        UB.SaveAction(res.ID, Zamba.ObjectTypes.Documents, Zamba.Core.RightsType.ReIndex, res.Name, 0);


                        sResult = null;

                        return Ok("Document Updated");
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception("El usuario no es el creador del documento"));
                        return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("El usuario no es el creador del documento")));
                    }
                }
                ZClass.raiseerror(new Exception("No se pudo obtener el recurso"));
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new HttpError("No se pudo obtener el recurso")));
            }

        }


        [AcceptVerbs("GET", "POST")]
        [Route("EndSession")]
        public IHttpActionResult EndSession(genericRequest request)
        {
            //try
            //{
            //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "ExternalSearchController metodo EndSession");
            //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Datos recibidos:");
            //    ZTrace.WriteLineIf(ZTrace.IsVerbose, request.Params["externuserid"].ToString());
            //}
            //catch (Exception e)
            //{
            //    ZClass.raiseerror(e);
            //    ZTrace.WriteLineIf(ZTrace.IsVerbose, "Fallo al escribir trace para datos recibidos");
            //}

            //try
            //{
            //    if (!request.Params.ContainsKey("externuserid"))
            //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("Debe indicar el ID de usuario.")));

            //    string _userId = DecryptString(request.Params["externuserid"].ToString());
            //    if (String.IsNullOrEmpty(_userId))
            //        return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new HttpError("No se pudo obtener el usuario")));

            //    long userID = long.Parse(_userId);

            //    long connId = 0; //Obtengo el connId de algun lado.

            //    SRights sr = new SRights();
            //    sr.RemoveConnectionFromWeb(connId);
            //    MembershipHelper.SetCurrentUser(null);

            //    var zssFactory = new ZssFactory();
            //    zssFactory.RemoveZss(userID);

            //    return Ok("Session closed");
            //}
            //catch (Exception ex)
            //{
            //    ZClass.raiseerror(ex);
            //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            //}
            return Ok("Session closed - Not Implemented");
        }

        #region "Private Methods"
        private void SaveZss(Zss zss)
        {
            var zssFactory = new ZssFactory();
            zssFactory.SetZssValues(zss);
        }
        private JObject GetTokenString(LoginVM l, IUser user)
        {
            //Obtengo token si todavia no caduco
            UserToken uToken = new UserToken();
            JObject tokenInfo = uToken.GetTokenIfIsValid(user);
            if (tokenInfo != null)
            {
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Token existente Valido");
                return (tokenInfo);
            }
            else
            {
                ZTrace.WriteLineIf(ZTrace.IsError, "Generando Token");
                tokenInfo = uToken.GenerateToken(user);
                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                l.ComputerNameOrIp = HttpContext.Current.Request.UserHostAddress.Replace("::1", "127.0.0.1");

                Ucm ucm = new Ucm();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "New Connection");

                Int32 timeOut = Int32.Parse(UP.getValue("TimeOut", Zamba.UPSections.UserPreferences, 30, user.ID));
                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("TimeOut: {0}", timeOut.ToString()));
                user.ConnectionId = Int32.Parse(ucm.NewConnection(user.ID, user.Name, l.ComputerNameOrIp, timeOut, 0, false).ToString());

                ZTrace.WriteLineIf(ZTrace.IsVerbose, string.Format("Connection: {0}", user.ConnectionId.ToString()));
                Zss zss = new Zss()
                {
                    UserId = user.ID,
                    ConnectionId = user.ConnectionId,
                    CreateDate = DateTime.Parse(tokenInfo.SelectToken(@"issued").Value<string>()),
                    TokenExpireDate = DateTime.Parse(tokenInfo.SelectToken(@"expiredate").Value<string>()),
                    Token = tokenInfo.SelectToken(@"access_token").Value<string>(),
                };
                SaveZss(zss);
                HttpContext.Current.Session["TokenExpires"] = zss.TokenExpireDate;
                HttpContext.Current.Session["BearerToken"] = zss.Token;
                UserBusiness UB = new UserBusiness();
                UB.ValidateLogIn(user.ID, ClientType.Web);
                return (tokenInfo);
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
        private string EncriptString(string value)
        {
            try
            {
                Zamba.Tools.Encryption encript = new Zamba.Tools.Encryption();
                return encript.EncryptNewStringNonShared(value);
            }
            catch (Exception)
            {
                throw;
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
        private bool validateToken(long userId, string token)
        {
            return true;
            //IUser userForToken = GetUser(userId);
            //UserToken uToken = new UserToken();
            //JObject tokenInfo = uToken.GetTokenIfIsValid(userForToken);

            //if (tokenInfo != null && tokenInfo["access_token"].ToString() == token)
            //    return true;

            //return false;
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
        #endregion

        #region "Helper Classes"
        private class ExternalSearchResult
        {
            public DataTable data { get; set; }
            public long total { get; set; } = 0;
            public string OrderBy { get; set; }
            public List<EntityDto> entities { get; set; } = new List<EntityDto>();
        }

        class DocumentData
        {
            public string fileName { get; set; }
            public string ContentType { get; set; }
            public object dataObject { get; set; }
            public byte[] data { get; set; }
        }

        #endregion

        #region "Columns to share"
        public List<string> ColumnsToShare
        {
            get
            {
                // Poner las columnas en minuscula
                return new[] { "id", "name", "original", "entidad", "creado", "modificado" }.ToList();
            }
        }
        #endregion


    }
}
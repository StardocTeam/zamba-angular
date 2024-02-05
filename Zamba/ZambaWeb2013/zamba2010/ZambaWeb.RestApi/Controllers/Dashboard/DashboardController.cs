using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using Zamba.Core;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Zamba.Framework;
using ZambaWeb.RestApi.Controllers.Dashboard.DB;
using static ZambaWeb.RestApi.Controllers.Dashboard.DB.DashboardDatabase;
using ZambaWeb.RestApi.Controllers.Class;
using static Zamba.Data.UserFactory;
using System.IO;
using static ZambaWeb.RestApi.Controllers.TasksController;
using Zamba.Core.Access;
using ZambaWeb.RestApi.Controllers.Dashboard;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using Zamba.Core.Cache;
using static ZambaWeb.RestApi.Controllers.SearchController;
using Zamba.Core.WF.WF;
using ZambaWeb.RestApi.ViewModels;
using System.Web;
using Newtonsoft.Json.Linq;
using static ZambaWeb.RestApi.Controllers.Dashboard.DB.ZambaTokenDatabase;
using Zamba.Services;


namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {

        [AcceptVerbs("GET", "POST")]
        [Route("Register")]
        public IHttpActionResult Register(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string phoneNumber = request.Params["mobilePrefix"] + request.Params["mobile"];

                Int32.TryParse(request.Params["department"], out int department);

                DashboarUserDTO newUser = new DashboarUserDTO(0, request.Params["companyName"],
                    request.Params["name"],
                    request.Params["lastname"],
                    phoneNumber,
                    request.Params["mail"],
                    request.Params["password"],
                    department, false);

                Validator validator = dashboardDatabase.EmailAlreadyTaken(newUser.Email);

                if (validator.emailIsTaken)
                {
                    return Ok(JsonConvert.SerializeObject(validator, Formatting.Indented));
                }

                dashboardDatabase.RegisterUser(newUser);
                string body = getWelcomeHtml(newUser);

                try
                {
                    sendRegister(request.Params["mail"], body);
                }
                catch (Exception ex)
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "No se pudo enviar el correo de verificacion.");
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                }


                return Ok(JsonConvert.SerializeObject(validator, Formatting.Indented));
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("executeRuleDashboard")]
        public IHttpActionResult executeRuleDashboard(genericRequest paramRequest)
        {
            try
            {

                //Int32 RuleId = Convert.ToInt16(paramRequest.Params["password"]);

                int UserId = Convert.ToInt16(paramRequest.UserId);

                List<Zamba.Core.ITaskResult> Results = new List<Zamba.Core.ITaskResult>();
                List<itemVarsResults> listResultIds = new List<itemVarsResults>();

                UserBusiness ub = new UserBusiness();
                ub.ValidateLogIn(UserId, ClientType.Web);

                // se crea uan tarea nueva
                //long stepid = 0;
                //DocType dt = new DocType(0);
                //ITaskResult task = new TaskResult(ref stepid, 0, 0, dt, "Home", 0, 0, TaskStates.Asignada, null, null, UserId, UserId.ToString());
                //Results.Add(task);


                string[] ZvarParams = new string[] { };
                ITaskResult NewTaskResult = null;
                string oldFullPath = string.Empty;

                if (UserId == null)
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                        new HttpError(StringHelper.InvalidUser)));

                if (paramRequest.Params.ContainsKey("ruleId") && !string.IsNullOrEmpty(paramRequest.Params["ruleId"]))
                {
                    Int64 ruleId = 0;
                    List<string> docIds = new List<string>();
                    STasks sTasks = new STasks();
                    //List<Zamba.Core.ITaskResult> Results = new List<Zamba.Core.ITaskResult>();
                    string resultIds;
                    //List<itemVarsResults> listResultIds = new List<itemVarsResults>();
                    ruleId = Int64.Parse(paramRequest.Params["ruleId"].ToString());
                    if (paramRequest.Params.ContainsKey("resultIds") && !string.IsNullOrEmpty(paramRequest.Params["resultIds"]))
                    {
                        docIds.AddRange(paramRequest.Params["resultIds"].ToString().Split(char.Parse(",")));
                        /// Se convierte el valor en un diccionario para poder iterarlo
                        resultIds = paramRequest.Params["resultIds"];
                        listResultIds = JsonConvert.DeserializeObject<List<itemVarsResults>>(resultIds);
                    }

                    string FormVariables = string.Empty;

                    if (paramRequest.Params.ContainsKey("FormVariables") && !string.IsNullOrEmpty(paramRequest.Params["FormVariables"]))
                    {
                        FormVariables = paramRequest.Params["FormVariables"];

                    }
                    if (paramRequest.Params.ContainsKey("zvars") && !string.IsNullOrEmpty(paramRequest.Params["zvars"]))
                    {
                        string zvars = paramRequest.Params["zvars"].ToString();
                        char delimitador = ';';
                        ZvarParams = zvars.Split(delimitador);
                    }

                    if (listResultIds.Count > 0)
                    {
                        for (int i = 0; i < listResultIds.Count; i++)
                        {
                            if (int.Parse(listResultIds[i].Docid) > 0)
                            {
                                var TaskByDocId = sTasks.GetTaskByDocId(Int64.Parse(listResultIds[i].Docid));
                                if (TaskByDocId == null)
                                {
                                    WFStep WT = new WFStep();
                                    IResult res = new Results_Business().GetResult(Int64.Parse(listResultIds[i].Docid), Int64.Parse(listResultIds[i].DocTypeid), true);
                                    oldFullPath = res.FullPath;
                                    NewTaskResult = new TaskResult(ref WT
                                    , 0
                                    , Int64.Parse(listResultIds[i].Docid)
                                    , (Zamba.Core.DocType)res.DocType
                                    , res.Name
                                    , res.IconId
                                    , 0
                                    , TaskStates.Asignada
                                    , res.Indexs
                                    , res.DISK_VOL_PATH
                                    , "0"
                                    , res.OffSet.ToString()
                                    , res.Doc_File
                                    , res.Disk_Group_Id
                                    , WT.InitialState, 0, ""
                                    );

                                    Results.Add(NewTaskResult);
                                }
                                else
                                {
                                    Results.Add(TaskByDocId);
                                }


                            }
                            else
                            {
                                ITaskResult ExecutionTask = new TaskResult();
                                ExecutionTask.AsignedToId = UserId;
                                ExecutionTask.UserId = (int)UserId;
                                ExecutionTask.TaskId = 0;
                                ExecutionTask.Name = "Ejecucion de regla IMAP"; //
                                                                                //ExecutionTask.StartRule = ruleId;       
                                Results.Add(ExecutionTask);
                            }
                        }
                    }
                    else
                    {
                        ITaskResult ExecutionTask = new TaskResult();
                        ExecutionTask.AsignedToId = UserId;
                        ExecutionTask.UserId = (int)UserId;
                        ExecutionTask.TaskId = 0;
                        ExecutionTask.Name = "Ejecucion de regla sin tarea"; //
                                                                             // ExecutionTask.StartRule = ruleId;              
                        Results.Add(ExecutionTask);
                    }


                    if (ZvarParams.Length > 0)
                    {
                        foreach (var item in ZvarParams)
                        {
                            switch (item)
                            {
                                case "rutaDocumento":
                                    if (!VariablesInterReglas.ContainsKey(item))
                                        VariablesInterReglas.Add(item, oldFullPath);
                                    else
                                        VariablesInterReglas.set_Item(item, oldFullPath);
                                    break;
                            }
                        }
                    }

                    if (FormVariables != string.Empty)
                    {
                        Dictionary<string, string> dicFormVariables = new Dictionary<string, string>();
                        List<itemVars> listFormVariables = JsonConvert.DeserializeObject<List<itemVars>>(FormVariables);

                        if (!string.IsNullOrEmpty(FormVariables))
                        {
                            for (int i = 0; i < listFormVariables.Count; i++)
                            {
                                dicFormVariables.Add(listFormVariables[i].name, listFormVariables[i].value);
                            }
                        }

                        //Se itera el diccionario de los valores
                        foreach (var itemlist in dicFormVariables)
                        {
                            if (!VariablesInterReglas.ContainsKey(itemlist.Key))
                                VariablesInterReglas.Add(itemlist.Key, itemlist.Value);
                            else
                                VariablesInterReglas.set_Item(itemlist.Key, itemlist.Value);
                        }
                    }

                    WFTaskBusiness WFTB = new WFTaskBusiness();
                    GenericExecutionResponse genericExecutionResult = null;
                    foreach (ITaskResult result in Results)
                    {
                        List<ITaskResult> currentResults = new List<ITaskResult>() { result };
                        TasksController TC = new TasksController();
                        genericExecutionResult = TC.ExecuteRule(ruleId, currentResults, true);

                    }
                    WFTB = null;

                    string js = null;
                    try
                    {
                        js = JsonConvert.SerializeObject(genericExecutionResult);
                        return Ok(js);
                    }
                    catch (Exception ex)
                    {
                        ZClass.raiseerror(ex);
                    }

                    return Ok();

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotAcceptable, new HttpError(StringHelper.InvalidParameter)));
                }
            }

            //string rule = "";
            //if (RuleId > 0)
            //    rule = ExecuteRuleDasboard(RuleId, results);

            //return Ok();

            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("Login")]
        public IHttpActionResult Login(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();
                UserInfo userInfo = new UserInfo();


                string email = request.Params["email"];
                string password = request.Params["password"];

                LoginResponseData userData = dashboardDatabase.Login(email, password);

                if (userData.msg == "ok")
                {

                    userInfo = new ZambaTokenDatabase().GetZambaToken(email, password);

                    userData.user.token = userInfo.token;
                    userData.user.name = email;

                    userData.user.groups = new PermissionsDatabase().getUserPermissions(userData.user.userid);
                }


                return Ok(JsonConvert.SerializeObject(userData, Formatting.Indented));
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }





        [AcceptVerbs("GET", "POST")]
        [Route("ActivateUser")]
        public IHttpActionResult ActivateUser(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string username = request.Params["email"];
                string password = request.Params["password"];
                string names = request.Params["name"];
                string lastname = request.Params["lastname"];
                string email = request.Params["email"];

                string[] parameters = new[] { username, password, names, lastname, email };

                bool allParametersHaveValues = parameters.All(param => !string.IsNullOrEmpty(param));

                if (allParametersHaveValues)
                {
                    if (dashboardDatabase.UserNeedValidation(email))
                    {
                        long newUserId = CreateNewUserForZamba(username, password, names, lastname, email, 171);

                        dashboardDatabase.ActivateUser(password, email, newUserId);
                    }
                }
                else
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Algunos parametros no estan completos");
                    return StatusCode(HttpStatusCode.BadRequest);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("ResendVerificationEmail")]
        public IHttpActionResult ResendVerificationEmail(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();
                string email = request.Params["mail"];
                if (!String.IsNullOrEmpty(email))
                {

                    var emailExist = dashboardDatabase.EmailAlreadyTaken(email).emailIsTaken;
                    if (emailExist)
                    {
                        if (dashboardDatabase.UserNeedValidation(email))
                        {
                            DashboarUserDTO user = dashboardDatabase.GetUserDashboard(email.Trim());
                            string body = getWelcomeHtml(user);
                            sendRegister(request.Params["mail"], body);
                        }
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }




        private long CreateNewUserForZamba(string username, string password, string names, string lastname, string email, long groupid)
        {

            try
            {
                long newUserID = Zamba.Data.CoreData.GetNewID(IdTypes.USERTABLEID);
                IUser newuser = new Zamba.Core.User();
                newuser.ID = newUserID;
                newuser.Name = username;
                newuser.Password = password;
                newuser.Nombres = names;
                newuser.Apellidos = lastname;

                //TODO - cuales son las configuraciones por defecto para el email de los usuarios para la base de datos de RRHH?
                short mailPort = short.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailPort", "25"));
                string smtpProvider = ZOptBusiness.GetValueOrDefault("DefaultMailSMTPProvider", "mx04.main.pseguros.com");
                bool enablessl = bool.Parse(ZOptBusiness.GetValueOrDefault("DefaultMailEnableSsl", "True"));

                newuser.eMail = new Correo()
                {
                    Mail = email,
                    UserName = email,
                    EnableSsl = enablessl,
                    ProveedorSMTP = smtpProvider,
                    Type = MailTypes.NetMail,
                    Puerto = mailPort
                };


                UserBusiness UB = new UserBusiness();
                UB.AddUserFromDashboard(newuser);
                UB.SetNewUser(ref newuser);
                UB.AssignGroupFromDashboard(newUserID, 171);

                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, "Dashboard - Nuevo usuario en Zamba - OK");
                return newUserID;
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "Dashboard - Nuevo usuario en Zamba - Error al crear nuevo usuario: " + ex.Message);
                throw ex;
            }

        }



        [AcceptVerbs("GET", "POST")]
        [Route("getDepartment")]
        public IHttpActionResult getDepartment(genericRequest request)
        {
            try
            {
                string JsonResult = JsonConvert.SerializeObject(new DashboardDatabase().GetDepartment());
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }


        [AcceptVerbs("GET", "POST")]
        [Route("getRol")]
        public IHttpActionResult getRol(genericRequest request)
        {
            try
            {
                string JsonResult = JsonConvert.SerializeObject(new DashboardDatabase().GetRol());
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }



        public string getWelcomeHtml(DashboarUserDTO newUser)
        {
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "Obteniendo HTML para mensaje de bienvenida.");

            try
            {
                var Scheme = Request.RequestUri.Scheme;
                var Authority = Request.RequestUri.Authority;
                string EndPoint = Request.RequestUri.Segments[1] + Request.RequestUri.Segments[2];


                // var pathEndPoint = Scheme + "://" + Authority + "/"+ EndPoint + "Dashboard/ActivateUser?" +
                var validateDashboardRoute = DashboardRoutesHelper.validate + "?" +
                     "password=" + newUser.Password + "&" +
                     "name=" + newUser.FirstName + "&" +
                     "lastname=" + newUser.LastName + "&" +
                     "email=" + newUser.Email;

                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Views\\RegistrationWelcomeBody.html";

                string htmlContent = File.ReadAllText(filePath);
                htmlContent = htmlContent.Replace("#login", validateDashboardRoute);
                return htmlContent;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);

                throw ex;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("getinfoSideBar")]
        public IHttpActionResult getinfoSideBar(genericRequest request)
        {
            try
            {
                var username = request.UserId;
                var usergroup = request.Params["groups"];
                DataTable menuOPtions = new DashboardDatabase().optionsUserSidbar(usergroup);

                foreach (DataRow item in menuOPtions.Rows)
                {
                    MenuOptionsParameters MPParams = JsonConvert.DeserializeObject<MenuOptionsParameters>(item["parameters"].ToString());

                    item["action"] += "?" + "ruleId=" + MPParams.ruleId + "&" + "typeRule=" + MPParams.typeRule;
                }

                rootObject data = new rootObject
                {
                    app = new app { name = "stardoc", description = "Stardoc Sa" },
                    user = new User { name = "", avatar = "", email = "" },
                    menu = new menu
                    {

                        items = new List<MenuItem>{

                        new MenuItem{
                            text = "Principal",
                            i18n = "menu.main",
                            group = true,
                            hideInBreadcrumb = true,
                            children = ListMenuItem(menuOPtions)
                            }
                        }
                    }
                };

                return Ok(JsonConvert.SerializeObject(data, Formatting.Indented));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("configUserSidbar")]
        public IHttpActionResult configUserSidbar(genericRequest request)
        {
            try
            {
                var usergroup = request.Params["groups"];

                string JsonResult = JsonConvert.SerializeObject(new DashboardDatabase().configUserSidbar(usergroup));
                return Ok(JsonResult);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }

        [AcceptVerbs("GET", "POST")]
        [Route("getCarouselContent")]
        public IHttpActionResult getCarouselContent(genericRequest request)
        {
            try
            {
                List<string> Listcontent = new List<string>();

                DataTable resultsDT = new DashboardDatabase().CarouselContent(request.UserId.ToString());

                if (resultsDT.Rows.Count > 0)
                {
                    Listcontent = GetListBase64Strings(resultsDT);
                }
                else
                {
                    ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, "No hay contenido de caruosel para el usuario: " + request.UserId.ToString());
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return Ok(JsonConvert.SerializeObject(Listcontent));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }


        [AcceptVerbs("GET", "POST")]
        [Route("getCarouselConfig")]
        public IHttpActionResult getCarouselConfig(genericRequest request)
        {
            try
            {
                DataTable resultsDT = new DashboardDatabase().CarouselConfig(request.UserId.ToString());
                CarouselConfigDTO CConfig = new CarouselConfigDTO();

                CConfig.DotPosition = resultsDT.Rows[0]["DotPosition"].ToString();
                CConfig.EnableSwipe = int.Parse(resultsDT.Rows[0]["EnableSwipe"].ToString());
                CConfig.AutoPlaySpeed = int.Parse(resultsDT.Rows[0]["AutoPlaySpeed"].ToString());
                CConfig.AutoPlay = int.Parse(resultsDT.Rows[0]["AutoPlay"].ToString());
                CConfig.Loop = int.Parse(resultsDT.Rows[0]["Loop"].ToString());

                return Ok(JsonConvert.SerializeObject(CConfig));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }


        [AcceptVerbs("GET", "POST")]
        [Route("setWidgetsContainer")]
        public IHttpActionResult setWidgetsContainer(genericRequest request)
        {
            try
            {
                new DashboardDatabase().InsertOrUpdateWidgetsContainer(request);
                return Ok("Insercion Exitosa");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }





        [AcceptVerbs("GET", "POST")]
        [Route("getWidgetsContainer")]
        public IHttpActionResult getWidgetsContainer(genericRequest request)
        {
            try
            {
                DataTable resultsDT = new DashboardDatabase().getWidgetsContainer(request.UserId.ToString());
                return Ok(JsonConvert.SerializeObject(resultsDT));
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }


        [AcceptVerbs("GET", "POST")]
        [Route("getEvents")]
        public IHttpActionResult GetEvents(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();
                string userid = request.Params["userid"];
                string groupids = request.Params["groupids"];
                DataSet eventsForUser = dashboardDatabase.GetEventsForUser(userid);
                DataSet eventsForUserGroups = dashboardDatabase.GetEventsForGroups(groupids);
                eventsForUserGroups.Merge(eventsForUser);

                List<string> eventDataList = new List<string>();
                foreach (DataRow row in eventsForUserGroups.Tables[0].Rows)
                {
                    eventDataList.Add(row["eventdata"].ToString());
                }
                string jsonCalendarEvents = "[" + string.Join(",", eventDataList) + "]";

                return Ok(jsonCalendarEvents);
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("insertNewEvent")]
        public IHttpActionResult InsertNewEvent(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string groupid = request.Params["groupid"];
                string eventdata = request.Params["eventdata"];
                string userid = request.Params["userid"];
                dashboardDatabase.InsertNewEvent(eventdata,groupid,userid);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("updateEvent")]
        public IHttpActionResult UpdateEvent(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string groupid = request.Params["groupid"];
                string eventdata = request.Params["eventdata"];
                string userid = request.Params["userid"];
                string calendareventid = request.Params["calendareventid"];
                dashboardDatabase.UpdateEvent(eventdata, groupid, userid, calendareventid);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }

        [AcceptVerbs("GET", "POST")]
        [Route("deleteEvent")]
        public IHttpActionResult DeleteEvent(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string eventid = request.Params["calendareventid"];
                dashboardDatabase.DeleteEvent(eventid);
                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }




        private List<string> GetListBase64Strings(DataTable resultsDT)
        {
            List<string> list = new List<string>();

            foreach (DataRow item in resultsDT.Rows)
            {
                string SourceContent = item["SourceContent"].ToString();
                string ContentPath = item["ContentPath"].ToString();

                if (SourceContent == "RestApiApp")
                {
                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Controllers\\Dashboard\\CarouselContent\\" + ContentPath;

                    if (File.Exists(filePath))
                    {
                        byte[] BytesArray = FileEncode.Encode(filePath);
                        var Base64String = System.Convert.ToBase64String(BytesArray);

                        string contentType = System.IO.Path.GetExtension(filePath).TrimStart('.');
                        list.Add("data:image/" + contentType + ";base64," + Base64String);
                    }
                }
                else
                {
                    if (File.Exists(ContentPath))
                    {
                        byte[] BytesArray = FileEncode.Encode(ContentPath);
                        var Base64String = System.Convert.ToBase64String(BytesArray);

                        string contentType = System.IO.Path.GetExtension(ContentPath).TrimStart('.');
                        list.Add("data:image/" + contentType + ";base64," + Base64String);
                    }
                }                
            }

            return list;
        }

        public bool sendRegister(string mailTo, string body)
        {
            try
            {
                SendMailConfig mail = new SendMailConfig
                {
                    UserName = SMTPDashboard.user,
                    Password = SMTPDashboard.password,
                    From = SMTPDashboard.from,
                    SMTPServer = SMTPDashboard.smtpServer,
                    Port = SMTPDashboard.port,
                    EnableSsl = SMTPDashboard.enableSsl,
                    IsBodyHtml = true,
                    MailTo = mailTo,
                    Subject = "Te damos la bienvenida a Zamba RRHH 🥳🥳",
                    Body = body
                };

                new SMail().SendMail(mail);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                throw ex;
            }
        }
        public List<MenuItem> ListMenuItem(DataTable result)
        {
            List<MenuItem> listItem = new List<MenuItem>();
            if (result == null) {

                return listItem;
            }

            foreach (DataRow item in result.Rows)
            {
                MenuItem menuitem = new MenuItem();
                menuitem.text = item["name"].ToString();
                menuitem.icon = item["icon"].ToString();
                menuitem.link = item["action"].ToString();

                listItem.Add(menuitem);
            }

            return listItem;

        }


        [AcceptVerbs("GET", "POST")]
        [Route("getVideoplayerURL")]
        public IHttpActionResult GetVideoplayerURL(genericRequest request)
        {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                long userid = request.UserId;
                var data = dashboardDatabase.GetVideoplayerURL(userid);

                return Ok(JsonConvert.SerializeObject(data));
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }
        
        public class app
        {
            public string name { get; set; }
            public string description { get; set; }
        }

        public class User
        {
            public string name { get; set; }
            public string avatar { get; set; }
            public string email { get; set; }
        }

        public class MenuItem
        {
            public string text { get; set; }
            public string i18n { get; set; }
            public bool group { get; set; }
            public bool hideInBreadcrumb { get; set; }
            public List<MenuItem> children { get; set; }
            public string icon { get; set; }
            public string link { get; set; }
        }

        public class menu
        {
            public List<MenuItem> items { get; set; }
        }

        public class rootObject
        {
            public app app { get; set; }
            public User user { get; set; }
            public menu menu { get; set; }
        }

        public class CarouselConfigDTO
        {
            public string DotPosition { get; set; }
            public int EnableSwipe { get; set; }
            public int AutoPlaySpeed { get; set; }
            public int AutoPlay { get; set; }
            public int Loop { get; set; }
        }
    }
}
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
using Zamba.Services;
using ZambaWeb.RestApi.Controllers.Class;
using static Zamba.Data.UserFactory;
using System.IO;
using static ZambaWeb.RestApi.Controllers.TasksController;
using Zamba.Core.Access;

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
                    department,false);

                Validator validator = dashboardDatabase.EmailAlreadyTaken(newUser.Email);

                if (validator.emailIsTaken)
                {
                    return Ok(JsonConvert.SerializeObject(validator, Formatting.Indented));
                }

                dashboardDatabase.RegisterUser(newUser);
                string body = getWelcomeHtml(newUser);

                sendRegister(request.Params["mail"], body);

                return Ok(JsonConvert.SerializeObject(validator, Formatting.Indented));
            }
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

                string email = request.Params["email"];
                string password = request.Params["password"];

                LoginResponseData userData = dashboardDatabase.Login(email, password);

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

                string username = request.Params["username"];
                string password = request.Params["password"];
                string names = request.Params["name"];
                string lastname = request.Params["lastname"];
                string email = request.Params["email"];

                CreateNewUserForZamba(username, password, names, lastname, email);


                dashboardDatabase.ActivateUser(username, email);

                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }

        }


        private void CreateNewUserForZamba(string username, string password, string names, string lastname, string email)
        {

            try
            {
                IUser newuser = new User();
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
                long newUserID = Zamba.Data.CoreData.GetNewID(IdTypes.USERTABLEID);

                UserBusiness UB = new UserBusiness();
                UB.AddUser(newuser);
                UB.SetNewUser(ref newuser);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Info, "Dashboard - Nuevo usuario en Zamba - OK");
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

                string UrlValidateHtml = WebUrl.Url + "?" +
                    "username=" + newUser.Username + "&" +
                    "password=" + newUser.Password + "&" +
                    "name=" + newUser.FirstName + "&" +
                    "lastname=" + newUser.LastName + "&" +
                    "email=" + newUser.Email;

                //var pathEndPoint = Scheme + "://" + Authority + "/"+ EndPoint + "Dashboard/ActivateUser?" +
                //    "username=" + newUser.Username + "&" +
                //    "password=" + newUser.Password + "&" +
                //    "name=" + newUser.FirstName + "&" +
                //    "lastname=" + newUser.LastName + "&" +
                //    "email=" + newUser.Email;

                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Views\\RegistrationWelcomeBody.html";

                string htmlContent = File.ReadAllText(filePath);
                htmlContent = htmlContent.Replace("#login", UrlValidateHtml);
                return htmlContent;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);

                throw ex;
            }
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
    }
}
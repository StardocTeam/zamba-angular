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
                Int32.TryParse(request.Params["rol"], out int rol);

                DashboarUserDTO newUser = new DashboarUserDTO(0, request.Params["companyName"],
                    request.Params["name"],
                    request.Params["lastname"],
                    phoneNumber, request.Params["username"],
                    request.Params["mail"],
                    request.Params["password"],
                    department, rol, false);

                Validator validator = dashboardDatabase.UsernameOrEmailAlreadyTaken(newUser.Username, newUser.Email);

                if (validator.emailIsTaken || validator.usernameIsTaken)
                {
                    return Ok(JsonConvert.SerializeObject(validator, Formatting.Indented));
                }

                dashboardDatabase.RegisterUser(newUser);

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

                string userName = request.Params["userName"];
                string password = request.Params["password"];

                LoginResponseData userData = dashboardDatabase.Login(userName, password);

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


        private void CreateNewUserForZamba(string username, string password, string names, string lastname,string email) {

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

        [AcceptVerbs("GET", "POST")]
        [Route("getWelcomeHtml")]
        public IHttpActionResult getWelcomeHtml(genericRequest request)
        {
            ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Verbose, "Obteniendo HTML para mensaje de bienvenida.");

            try
            {
                //System.AppDomain.CurrentDomain.BaseDirectory;

                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Views\\RegistrationWelcomeBody.html";

                string htmlContent = File.ReadAllText(filePath);







                return Ok(htmlContent);
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);

                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }



        [AcceptVerbs("GET", "POST")]
        [Route("sendRegister")]
        public IHttpActionResult sendRegister(genericRequest request)
        {
            try
            {
                SendMailConfig mail = new SendMailConfig
                {
                    UserName = request.Params["user"],
                    Password = request.Params["pass"],
                    From = request.Params["from"],
                    SMTPServer = request.Params["smtp"],
                    Port = request.Params["port"],
                    EnableSsl = Boolean.Parse(request.Params["enableSsl"]),
                    MailTo = /*request.Params["mailTo"]*/ "emiliano.alvarez@stardoc.com.ar", //debugger;
                    Subject = request.Params["subject"],
                    Body = request.Params["body"]
                };

                new SMail().SendMail(mail);
                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError));
            }
        }


        /// <summary>
        /// Gestiona el envio de un registro a la applicacion de 'Dashboard', al usuario en cuestion.
        /// </summary>
        /// <param name="emailData">Datos del correo</param>
        /// <returns>Verdadero si fue enviado el correo, caso contrario, falso,</returns>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [AllowAnonymous]
        [Route("SendRegisterFromDashBoard")]
        [OverrideAuthorization]
        public IHttpActionResult SendRegisterFromDashBoard(genericRequest FormData)
        {
            SendMailConfig mail = null;

            try
            {


                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(ZTrace.IsError, "[ERROR]:" + ex.Message);

                //return ERROR();

                throw ex;
            }
            finally
            {
                //if (mail != null)
                //    mail.Dispose();
            }
        }

    }
}
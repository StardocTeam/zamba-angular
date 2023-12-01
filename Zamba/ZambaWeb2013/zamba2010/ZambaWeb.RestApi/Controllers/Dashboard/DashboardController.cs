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

                DashboarUserDTO newUser = new DashboarUserDTO(0, request.Params["companyName"], request.Params["name"], request.Params["lastname"], phoneNumber, request.Params["mail"], request.Params["password"], department, rol, false);
                dashboardDatabase.RegisterUser(newUser);

                return Ok();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
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
        [Route("sendRegister")]
        public IHttpActionResult sendRegister(genericRequest request)
        {
            try
            {
                SendMailConfig mail = null; // = request

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
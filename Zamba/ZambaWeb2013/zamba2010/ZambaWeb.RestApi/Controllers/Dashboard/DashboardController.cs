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
using ZambaWeb.RestApi.Controllers.Dashboard.DB;
using static ZambaWeb.RestApi.Controllers.Dashboard.DB.DashboardDatabase;

namespace ZambaWeb.RestApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {

        [AcceptVerbs("GET", "POST")]
        [Route("Register")]
        public IHttpActionResult Register(genericRequest request) {
            try
            {
                DashboardDatabase dashboardDatabase = new DashboardDatabase();

                string phoneNumber = request.Params["mobilePrefix"] + request.Params["mobile"];
                
                Int32.TryParse(request.Params["department"], out int department);
                Int32.TryParse(request.Params["rol"], out int rol);

                DashboarUserDTO newUser = new DashboarUserDTO(0,request.Params["companyName"],
                    request.Params["name"],
                    request.Params["lastname"],
                    phoneNumber,request.Params["username"],
                    request.Params["mail"],
                    request.Params["password"],
                    department,rol,false);

                dashboardDatabase.RegisterUser(newUser);

                return Ok();
            }
            catch (Exception ex)
            {
                ZTrace.WriteLineIf(System.Diagnostics.TraceLevel.Error, ex.Message);
                return StatusCode(HttpStatusCode.BadRequest);
            }
           
        }
        

    }
      
}
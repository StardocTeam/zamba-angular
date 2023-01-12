using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Collections;
using ZambaWeb.RestApi.Models;
using Zamba.Core;
using Zamba.Filters;
using Zamba.Core.Search;
using System;
using System.Text;
using Zamba.Services;
using Zamba;
using System.Web;
using Zamba.Core.WF.WF;
using System.Web.Http.Results;
using Newtonsoft.Json;
using ZambaWeb.RestApi.ViewModels;
using System.Net.Http;
using System.Net;
using System.Linq;
using Zamba.Core.Enumerators;
using Zamba.Framework;
using Zamba.Data;
using ZambaWeb.RestApi.Controllers.Web;
using ZambaWeb.RestApi.Controllers.Class;
using Zamba.FileTools;
using System.IO;
using Zamba.Membership;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Web.Security;
using System.Net.Http.Headers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;

namespace ZambaWeb.RestApi.Controllers.Class
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UcmController : ApiController
    {
        [Route("api/UcmServices/UCM")]
        [AcceptVerbs("GET", "POST")]
        [HttpPost, HttpGet]
        public IHttpActionResult UCM()
        {
            try
            {
                (new Ucm()).CheckInactiveSessions();
                return Ok("OK");
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return Ok(ex.Message);
            }
            
        }

    }
}



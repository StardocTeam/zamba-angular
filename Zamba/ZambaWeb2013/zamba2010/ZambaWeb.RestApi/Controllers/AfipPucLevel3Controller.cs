using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZambaWeb.RestApi.AuthorizationRequest;


namespace ZambaWeb.RestApi.Controllers
{
    [RestAPIAuthorize]
    [globalControlRequestFilter]
    public class AfipPucLevel3Controller : ApiController
    {
        // GET: api/AfipPucLevel3/5

        [OverrideAuthorization]
        public string Get(string cuit)
        {
            GetPUCLevel3 Puc3 = new GetPUCLevel3();
            var response = Puc3.Get(cuit);
            return response;
        }
    }
}

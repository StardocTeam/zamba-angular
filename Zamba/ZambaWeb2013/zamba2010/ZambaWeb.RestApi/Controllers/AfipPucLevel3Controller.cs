using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ZambaWeb.RestApi.Controllers
{
    public class AfipPucLevel3Controller : ApiController
    {
        // GET: api/AfipPucLevel3/5
        public string Get(string cuit)
        {
            GetPUCLevel3 Puc3 = new GetPUCLevel3();
            var response = Puc3.Get(cuit);
            return response;
        }
    }
}

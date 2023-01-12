using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using Zamba;
using Zamba.Core;
using Zamba.Framework;
using Zamba.Services;
using ZambaWeb.RestApi.Models;

namespace ZambaWeb.RestApi.Controllers.ExternalPublish
{
    public class APIController : ApiController
    {
        // GET: api/API
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/API/5
        public string Get(int id)
        {
            return "value";
        }

      

        // PUT: api/API/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/API/5
        public void Delete(int id)
        {
        }
    }
}

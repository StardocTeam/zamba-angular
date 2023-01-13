using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Zamba.Servers;

namespace Zamba.Web.Controllers
{
    public class WebTemplatesController : ApiController
    {
        public WebTemplatesController()
        {
            if (Zamba.Servers.Server.ConInitialized == false)
            {
                Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
                ZC.InitializeSystem("Zamba.Web");
            }
        }

        [HttpGet]
        [Route("api/WebTemplates/GetTemplates")]
        public DataSet OpenNewFile()
        {
            DataSet ds = new DataSet();
            string query = "select * from ZTempl";
            ds = Server.get_Con().ExecuteDataset(CommandType.Text, query);
            return (ds);
        }

        [HttpGet]
        [Route("api/WebTemplates/DownloadTemplate")]
        public HttpResponseMessage DownloadTemplate(string path)
        {
            if (!File.Exists(path)) return null;

            HttpResponseMessage result = null;
            // Serve the file to the client
            result = Request.CreateResponse(HttpStatusCode.OK);
            result.Content = new StreamContent(new FileStream(path, FileMode.Open, FileAccess.Read));
            result.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            result.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);

            return result;
        }
    }
}

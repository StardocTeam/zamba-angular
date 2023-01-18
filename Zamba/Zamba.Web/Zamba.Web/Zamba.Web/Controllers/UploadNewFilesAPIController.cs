using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;


namespace Zamba.Web.Controllers
{
    public class UploadNewFilesAPIController : ApiController
    {
        [HttpGet]
        [Route("api/UploadNewFiles/OpenNewFile")]
        public System.Web.Mvc.FileResult OpenNewFile(string type)
        {

            var url = System.Web.HttpContext.Current.Server.MapPath("~/Content/Templates");
            var fileNameWithPath = url + "/word.docx";

            return null;// System.Web.Mvc.Controller.File(fileNameWithPath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(fileNameWithPath));

        }

        public HttpResponseMessage DownloadFile(string file)
        {
            var stream = new MemoryStream();
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "CertificationCard.pdf"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}

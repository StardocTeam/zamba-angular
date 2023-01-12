using System;
using System.Web;
using System.Web.Mvc;
using IOFile = System.IO.File;

namespace Kendo.Mvc.Examples.Controllers
{
    public class SourceController : Controller
    {
        public ActionResult Index(string path)
        {
            if (String.IsNullOrEmpty(path) || (
                    !path.StartsWith("~/Views") &&
                    !path.StartsWith("~/Models") &&
                    !path.StartsWith("~/content") &&
                    !path.StartsWith("~/Controllers")))
            {
                return HttpNotFound();
            }

            var mappedPath = Server.MapPath(path);

            if (!IOFile.Exists(mappedPath))
            {
                return HttpNotFound();
            }

            if (path.StartsWith("~/content") && path.EndsWith("html"))
            {
                return Content(IOFile.ReadAllText(mappedPath), "text/html");
            }

            var source = IOFile.ReadAllText(mappedPath);

            return Content("<pre class='prettyprint'>" + HttpUtility.HtmlEncode(source) + "</pre>", "text/plain");
        }
    }
}

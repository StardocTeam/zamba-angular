
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Export;
using System.Net;
using System.IO;


namespace Kendo.Mvc.Examples.Controllers
{
    public class EditorExportController : Controller
    {
        [HttpPost]
        public ActionResult Export(EditorExportData data)
        {
            var settings = new EditorDocumentsSettings();
            settings.HtmlImportSettings.LoadFromUri += HtmlImportSettings_LoadFromUri;
            try
            {
                return EditorExport.Export(data, settings);
            }
            catch
            {
                return RedirectToAction("exportas", "editor");
            }
        }

        private void HtmlImportSettings_LoadFromUri(object sender, Telerik.Windows.Documents.Flow.FormatProviders.Html.LoadFromUriEventArgs e)
        {
            var uri = e.Uri;
            var absoluteUrl = uri.StartsWith("http://") || uri.StartsWith("www.");
            if (!absoluteUrl)
            {

                var filePath = Server.MapPath(uri);
                using (var fileStream = System.IO.File.OpenRead(filePath))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        e.SetData(memoryStream.ToArray());
                    }
                }
            }
        }    }
}

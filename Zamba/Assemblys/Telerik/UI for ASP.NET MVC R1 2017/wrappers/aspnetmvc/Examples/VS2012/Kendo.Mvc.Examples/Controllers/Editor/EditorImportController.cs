using Kendo.Mvc.Export;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kendo.Mvc.Examples.Controllers.Editor
{
    public class EditorImportController : Controller
    {
        public ActionResult Import(HttpPostedFileBase file)
        {
            var settings = new EditorImportSettings();
            string htmlResult;
            switch (Path.GetExtension(file.FileName))
            {
                case ".docx":
                    htmlResult = EditorImport.ToDocxImportResult(file, settings);
                    break;
                case ".rtf":
                    htmlResult = EditorImport.ToRtfImportResult(file, settings);
                    break;
                case ".pdf":
                    htmlResult = EditorImport.ToPdfImportResult(file, settings);
                    break;
                default:
                    htmlResult = EditorImport.GetTextContent(file);
                    break;
            }

            return Json(new { html = htmlResult });
        }
    }
}

using System.IO;
using System.Web.Mvc;
using Kendo.Models;
using Telerik.Web.Spreadsheet;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf.Export;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class SpreadsheetController : Controller
    {
        [Demo]
        public ActionResult Server_Side_Pdf_Export()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveAsPDF(string data, string activeSheet, PrintOptions options)
        {
            var workbook = Workbook.FromJson(data);
            var document = workbook.ToDocument();

            var provider = new PdfFormatProvider();
            provider.ExportSettings = new PdfExportSettings(options.Source, false);

            //document.ActiveSheet = document.Worksheets.First(sheet => sheet.Name == activeSheet);

            foreach (var sheet in document.Worksheets)
            {
                var pageSetup = sheet.WorksheetPageSetup;

                pageSetup.PaperType = options.PaperSize;
                pageSetup.PageOrientation = options.Orientation;

                pageSetup.CenterHorizontally = options.CenterHorizontally;
                pageSetup.CenterVertically = options.CenterVertically;

                pageSetup.PrintOptions.PrintGridlines = options.PrintGridlines;

                pageSetup.Margins = options.Margins;
            }

            using (var stream = new MemoryStream())
            {
                provider.Export(document, stream);

                var mimeType = Telerik.Web.Spreadsheet.MimeTypes.PDF;
                return File(stream.ToArray(), mimeType, "Print.pdf");
            }
        }
    }
}
using Kendo.Mvc.Export;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Telerik.Documents.SpreadsheetStreaming;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class GridController : Controller
    {
        [Demo]
        public ActionResult Server_Export()
        {
            return View(productService.Read());
        }

        [HttpPost]
        public FileStreamResult ExportServer(string model, string data)
        {
            var columnsData = JsonConvert.DeserializeObject<IList<ExportColumnSettings>>(HttpUtility.UrlDecode(model));
            dynamic options = JsonConvert.DeserializeObject(HttpUtility.UrlDecode(data));
            SpreadDocumentFormat exportFormat = options.format.ToString() == "csv" ? exportFormat = SpreadDocumentFormat.Csv : exportFormat = SpreadDocumentFormat.Xlsx;
            Action<ExportCellStyle> cellStyle = new Action<ExportCellStyle>(ChangeCellStyle);
            Action<ExportRowStyle> rowStyle = new Action<ExportRowStyle>(ChangeRowStyle);
            Action<ExportColumnStyle> columnStyle = new Action<ExportColumnStyle>(ChangeColumnStyle);

            string fileName = string.Format("{0}.{1}", options.title, options.format);
            string mimeType = Helpers.GetMimeType(exportFormat);

            Stream exportStream = exportFormat == SpreadDocumentFormat.Xlsx ?
                productService.Read().ToXlsxStream(columnsData, (string)options.title.ToString(), cellStyleAction: cellStyle, rowStyleAction: rowStyle, columnStyleAction: columnStyle) :
                productService.Read().ToCsvStream(columnsData);

            var fileStreamResult = new FileStreamResult(exportStream, mimeType);
            fileStreamResult.FileDownloadName = fileName;
            fileStreamResult.FileStream.Seek(0, SeekOrigin.Begin);

            return fileStreamResult;
        }

        private void ChangeCellStyle(ExportCellStyle e)
        {
            bool isHeader = e.Row == 0;
            SpreadCellFormat format = new SpreadCellFormat
            {
                ForeColor = isHeader ? SpreadThemableColor.FromRgb(50, 54, 58) : SpreadThemableColor.FromRgb(214, 214, 217),
                IsItalic = true,
                VerticalAlignment = SpreadVerticalAlignment.Center,
                WrapText = true,
                Fill = SpreadPatternFill.CreateSolidFill(isHeader ? new SpreadColor(93, 227, 0) : new SpreadColor(50, 54, 58))
            };
            e.Cell.SetFormat(format);
        }

        private void ChangeRowStyle(ExportRowStyle e)
        {
            e.Row.SetHeightInPixels(e.Index == 0 ? 80 : 30);
        }

        private void ChangeColumnStyle(ExportColumnStyle e)
        {
            double width = e.Name == "Product name" || e.Name == "Category Name" ? 250 : 100;
            e.Column.SetWidthInPixels(width);
        }
    }
}

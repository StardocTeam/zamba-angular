using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Documents.SpreadsheetStreaming;
using Kendo.Mvc.Examples.Models.SpreadStreamProcessing;
using System.IO;

namespace Kendo.Mvc.Examples.Controllers.SpreadStreamProcessing
{
    public class SpreadStreamProcessingController : Controller
    {
        [Demo]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateDocument(int rowsCount, string fileType)
        {
            int headerRowHeight = 22;
            string[] columnHeaders = { "ID", "DATE", "TIME", "CLIENT", "COMPANY", "SHIPPING", "DISCOUNT", "STATUS" };
            double[] columnWidths = { 9.43, 12.29, 10.71, 15.43, 21.71, 14.29, 13.57, 11.29 };

            int rowHeight = 18;

            DocumentSettings docSettings = new DocumentSettings();

            docSettings.ExportedCellsCount = 0;
            //docSettings.exportStarted = DateTime.Now;

            MemoryStream documentStream = new MemoryStream();
            byte[] renderedBytes = null;

            string mimeType = string.Empty;
            string fileExtension = string.Empty;

            //set file format 
            switch (fileType)
            {
                case "xlsx":
                    docSettings.SelectedDocumentFormat = SpreadDocumentFormat.Xlsx;
                    mimeType = "application / vnd.openxmlformats - officedocument.spreadsheetml.sheet";
                    fileExtension = "xlsx";
                    break;
                case "csv":
                    docSettings.SelectedDocumentFormat = SpreadDocumentFormat.Csv;
                    mimeType = "text/csv";
                    fileExtension = "csv";
                    break;
                default:
                    break;
            }


            using (IWorkbookExporter workbookExporter = SpreadExporter.CreateWorkbookExporter(docSettings.SelectedDocumentFormat, documentStream))
            {
                using (IWorksheetExporter worksheetExporter = workbookExporter.CreateWorksheetExporter("Orders Log"))
                {
                    for (int i = 0; i < columnWidths.Length; i++)
                    {
                        using (IColumnExporter columnExporter = worksheetExporter.CreateColumnExporter())
                        {
                            columnExporter.SetWidthInCharacters(columnWidths[i]);
                        }
                    }

                    DocumentExportHelper exportHelper = new DocumentExportHelper(headerRowHeight, columnHeaders, columnWidths);

                    exportHelper.ExportHeaderRows(worksheetExporter);

                    for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
                    {
                        using (IRowExporter rowExporter = worksheetExporter.CreateRowExporter())
                        {
                            rowExporter.SetHeightInPoints(rowHeight);

                            DocumentRow row = DocumentHelper.GenerateDocumentRow(rowsCount, rowIndex);

                            SpreadCellFormat normalFormat = new SpreadCellFormat();
                            normalFormat.FontSize = 10;
                            normalFormat.VerticalAlignment = SpreadVerticalAlignment.Center;
                            normalFormat.HorizontalAlignment = SpreadHorizontalAlignment.Center;

                            exportHelper.ExportIdColumn(rowExporter, row, normalFormat);
                            exportHelper.ExportDateColumn(rowExporter, row);
                            exportHelper.ExportTimeColumn(rowExporter, row);
                            exportHelper.ExportClientColumn(rowExporter, row, normalFormat);
                            exportHelper.ExportCompanyColumn(rowExporter, row, normalFormat);
                            exportHelper.ExportShippingColumn(rowExporter, row);
                            exportHelper.ExportDiscountColumn(rowExporter, row);
                            exportHelper.ExportStatusColumn(rowExporter, row);
                        }
                    }

                    worksheetExporter.MergeCells(0, 0, 0, 7);
                    worksheetExporter.MergeCells(1, 0, 1, 5);
                    worksheetExporter.MergeCells(1, 6, 1, 7);
                }


            }

            renderedBytes = documentStream.ToArray();

            return File(renderedBytes, mimeType, "GeneratedFile." + fileExtension);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.Data;
using System.Drawing;
using Spire.Doc.Interface;
using System.Collections;
using Spire.Xls;
using Spire.DataExport.TXT;
using Spire.Pdf.Graphics;
using Spire.Pdf;
using System.Drawing.Imaging;
using System.IO;
using Zamba.AppBlock;
using Zamba.Core;
using Spire.Barcode;
using System.Windows.Forms;
using Spire.Pdf.HtmlConverter;
using Microsoft.Office.Interop.Word;
using System.Drawing.Printing;
using Zamba.Tools;
using System.Threading;

namespace Zamba.FileTools
{
    public class SpireTools
    {
        private string barcodeInBase64;

        /// <summary>
        /// Exporta los datos de un excel como DataTable
        /// puede recibir el nombre de la hoja a exportar, sino toma la primera 
        /// </summary>
        /// <param name="file">Archivo excel a exportar</param>
        /// <param name="sheetName">Opcional, nombre de la hoja a exportar, si no se pasa toma la primera</param>
        /// <returns>Un DataTable con los datos</returns>
        public DataTable GetExcelAsDataSet(string file, string sheetName = "")
        {
            Workbook workbook = new Workbook();
            workbook.LoadFromFile(file);
            Worksheet sheet;

            sheet = workbook.Worksheets[sheetName];

            if (sheet == null)
                sheet = workbook.Worksheets[0];

            // Quito las filas en blanco
            for (int i = sheet.Rows.Count() - 1; i >= 0; i--)
                if (sheet.Rows[i].IsBlank)
                    sheet.DeleteRow(i + 1);

            return sheet.ExportDataTable();
        }

        /// <summary>
        /// Exporta un datatable a Excel 
        /// </summary>
        /// <param name="dt">Datos a exportar</param>
        /// <param name="path">Ruta donde se va a guardar el archivo</param>
        public void ExportToXLS(DataTable dt, String path)
        {
            Workbook ef = null;
            Worksheet ws = null;
            try
            {
                ef = new Workbook();
                ws = ef.Worksheets.Add("Exportacion");

                while (ef.Worksheets.Count != 1)
                    ef.Worksheets.Remove(0);

                //Inserta los datos en excel
                ws.InsertDataTable(dt, true, 1, 1);

                //Ajusta ancho de columnas
                foreach (var column in ws.Columns)
                    column.AutoFitColumns();

                //Aplica bordesa los datos
                CellRange range = ws.Range[ws.FirstRow, ws.FirstColumn, ws.LastRow, ws.LastColumn];
                range.BorderInside(LineStyleType.Thin, Color.Black);
                range.BorderAround(LineStyleType.Medium, Color.Black);

                //Guarda el archivo
                ef.SaveToFile(path, ExcelVersion.Version2007);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (ws != null)
                {
                    ws.Dispose();
                    ws = null;
                }
                if (ef != null)
                {
                    ef.Dispose();
                    ef = null;
                }
            }
        }

        public Image GenerateBarcodeImage(int barcodeId, short barCodeType)
        {
            try
            {
                BarcodeSettings.ApplyKey("G769Y-1ZXJP-ZVDB4-D851K-4WNPB");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Spire.License.LicenseProvider.GetLicenseFileName());

                BarcodeSettings bcSettings = new BarcodeSettings();
                bcSettings.Unit = GraphicsUnit.Millimeter;
                bcSettings.TextAlignment = StringAlignment.Far;
                bcSettings.BarHeight = 8;
                bcSettings.TextFont = new Font("Arial", 10);
                bcSettings.TopMargin = 0;
                bcSettings.RightMargin = 0;
                bcSettings.BottomMargin = 0;
                bcSettings.LeftMargin = 0;
                bcSettings.HasBorder = false;
                bcSettings.Data = barcodeId.ToString();
                bcSettings.Data2D = barcodeId.ToString();

                switch (barCodeType)
                {
                    case 1:
                        bcSettings.Type = BarCodeType.Codabar;
                        break;
                    case 2:
                        bcSettings.Type = BarCodeType.Code39;
                        break;
                    case 3:
                        bcSettings.Type = BarCodeType.Code128;
                        break;
                    case 4:
                        bcSettings.Type = BarCodeType.Code93;
                        break;
                    default:
                        bcSettings.Type = BarCodeType.Codabar;
                        break;
                }

                bcSettings.TopText = "Zamba";

                BarCodeGenerator bargenerator = new BarCodeGenerator(bcSettings);
                return bargenerator.GenerateImage();
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                return null;
            }
        }

        public string GenerateBarcodeImage(string templatePath, long barcodeId)
        {
            try
            {
                //Spire.License.LicenseProvider.SetLicenseKey("f8EtGD4252xT64cBAPDOf9M+Cd1SPbPLWcuUZaCfbE28yxH9vjXouk1R6RxmIIDOkYjefGNH+GvHblXKlf5s/8w/tus4H/n1f3D50w7lUl92XvbW6Cv5XnXgdsv2yr8+JZ4KaekZi5DH5ZfVicftemM/MqGSFl3GmzdpFHGhMOrD0OLteC+bUKiYKDtGx5953q4f/fzDE5JhUK6W9TQfkrdIjKgyliBzvLRQwae6PpVyu2iIXSr8Iezdyf1r7DKycQdg6zxxS7BR8UojG3JLDI4y/Iw7nmTSjAz5u12LhJ0Rwn+6AjvEDAS/Dqj2tAWpirGCGQqGFTl/LrPSGJ7kUkMcRGwDB14OJ8ezMJb6qwgcE8RsvakynQEMunzMMpPK/86UDo1XUCF9okhqGzF1sGRr7+j3vYWGra1gGm3hPupPDVvSq/XBoZz/0jVsGctYQ955woQStIawztZ4LSamLupgzT486h/rxAvdB0REvBwdtI/vX3bI3Q+h4HgoR4Uutm00XI3AGeM4N9ExBOkMlSqkg/jA4OS50Xi5zhUZVuS7MTSoZDwNIe4DCtFAJKQRFZQJvC4SgR6TJuynYD1o8XM6ApeI/hVjcMPFCQnMfLW5EHvY/lH+YqWKmBx1uMhjZMhgRrW3gZ6Mnvjn9+6RuDWgIHTlWAJZBAWZVk9L3Y1aRQ773T1MXlMMwaGu0QTI7tWSrOTQrd8YPVgfHnMk3oWKluz1sSSb+Bo0Lvd9upGsmJCJXf/uHP5ahNMnBCIwrJCxoUmDBUpmFzvK9Bg1g4o/nWNQgunpjEgTj2XoFP4Dukk5Gk7USnCpvSLpFqGngKF4aBq1vatZThH/00nHIdfVLsdxH0mX+Vm1obTc49JdpmzRZ/reYQcCXiPku6u6ErsdZyk8m6SBHePTP7G9tBUKYFyubN0JAnQboUYHm8CbbjU9zY6qCqLinvhhVES9JY1spb3pD4TLJ1tZt/WmNWHJO9IlEEts/NTUZKFUMYBXJSamUuv6adb3HB1NnX6FKdwnvcWEnXS2GBxqYnmJx6Lp9gu+62gRP8nYA4PY8QgJtdvsUwkzdUg5Ag5UsSs3am2U6WvbQ0DbJuMT7DSoK8Hbv1RDY8GiFqkgZMjG2ltiCUJKIW4cDEaPuRUz3yJaqkTQdHLNP/7sOEmrK3nUpChgYGxy4Ob1tQ59XIZmwdwEjmT/Sqw8DXERhTWMA4FUm6SsJThHnpLjHNxq6RL48H0HH1H4N7OYwLTao6Vq0BeKCHCQEQa7AkUhD/9zrQc9wcHF331Uwa+73lDN+xxPwG3viNU+kY637SLuWS7DecTAm5Xxent0tF6oLxvaztpufkMs3nNclN7DMiIE/jEe5IP4KZwAN/yK182z4kae3LEVJ4AUTnGaft2uxatxPYEIYijdEQhBFACjgut3RqdWsdBjb7K7N9e282pjCqcsfY9grQPiLovuvwj5XAxM8XYHMzbtWP5DUEdQSOlmzOWxGw==");

                Spire.Barcode.BarcodeSettings.ApplyKey("G769Y-1ZXJP-ZVDB4-D851K-4WNPB");
                ZTrace.WriteLineIf(ZTrace.IsVerbose, Spire.License.LicenseProvider.GetLicenseFileName());

                BarcodeSettings bcSettings = new BarcodeSettings();
                bcSettings.Unit = GraphicsUnit.Millimeter;
                bcSettings.TextAlignment = StringAlignment.Far;
                //bcSettings.UseAntiAlias = false;
                //bcSettings.AutoResize = true;
                bcSettings.BarHeight = 8;
                bcSettings.TextFont = new Font("Arial", 10);
                bcSettings.TopMargin = 0;
                bcSettings.RightMargin = 0;
                bcSettings.BottomMargin = 0;
                bcSettings.LeftMargin = 0;
                bcSettings.HasBorder = false;
                bcSettings.Data = barcodeId.ToString();
                bcSettings.Data2D = barcodeId.ToString();

                int bcType = Int32.Parse(UserPreferences.getValueForMachine("BarCodeType", UPSections.Barcode, 1, true));

                switch (bcType)
                {
                    case 1:
                        bcSettings.Type = BarCodeType.Codabar;
                        break;
                    case 2:
                        bcSettings.Type = BarCodeType.Code39;
                        break;
                    case 3:
                        bcSettings.Type = BarCodeType.Code128;
                        break;
                    case 4:
                        bcSettings.Type = BarCodeType.Code93;
                        break;
                    default:
                        bcSettings.Type = BarCodeType.Codabar;
                        break;
                }

                bcSettings.TopText = "Zamba";
                //bcSettings.ShowTopText = false;
                //bcSettings.ShowTextOnBottom = false;

                BarCodeGenerator bargenerator = new BarCodeGenerator(bcSettings);
                Image barcodeImage = bargenerator.GenerateImage();

                //using (MemoryStream ms = new MemoryStream())
                //{
                //    barcodeImage.Save(ms, ImageFormat.Png);
                //    byte[] imageBytes = ms.ToArray();
                //    barcodeInBase64 = Convert.ToBase64String(imageBytes);
                //}

                return GetBase64StringFromImage(barcodeImage, ImageFormat.Png);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetBase64StringFromImage(Image img, ImageFormat imgFormat)
        {
            if (img != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, imgFormat);
                    byte[] imageBytes = ms.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            return string.Empty;
        }

        public static void PrintHtmlDocByHtmlString(string htmlString, short copies)
        {
            PdfDocument doc = new PdfDocument();
            string tempFile = FileBusiness.GetUniqueFileName(Path.Combine(Membership.MembershipHelper.AppTempPath, "OfficeTemp", "BarcodePdfTemp.pdf"));

            try
            {
                //Tomo tamaño de hoja de la impresora por defecto y los paso a Point.
                PrinterSettings DefaultPrinterSettings = new PrinterSettings();
                PdfUnitConvertor unitCvtr = new PdfUnitConvertor();
                float _Width = unitCvtr.ConvertUnits((float)DefaultPrinterSettings.DefaultPageSettings.PaperSize.Width / 100, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);
                float _Height = unitCvtr.ConvertUnits((float)DefaultPrinterSettings.DefaultPageSettings.PaperSize.Height / 100, PdfGraphicsUnit.Inch, PdfGraphicsUnit.Point);

                //Configuro la pagina PDF
                PdfPageSettings pdfPageSettings = new PdfPageSettings();
                pdfPageSettings.SetMargins(0, 0, 0, 0);
                pdfPageSettings.Orientation = PdfPageOrientation.Landscape;
                pdfPageSettings.Size = new SizeF(_Width, _Height);

                //Lo necesito para pasar al metodo
                PdfHtmlLayoutFormat pdfHtmlLayoutFormat = new PdfHtmlLayoutFormat();

                //Creo nuevo documento PDF, y lo cargo a traves del html string
                doc = new PdfDocument();
                doc.LoadFromHTML(htmlString, false, pdfPageSettings, pdfHtmlLayoutFormat);

                //Genero un pdf fisico temporal para poder imprimirlo
                doc.SaveToFile(tempFile);
                doc.PrintDocument.PrinterSettings.Copies = copies;
                doc.PrintDocument.Print();
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            finally
            {
                if (doc != null)
                    doc.Close();

                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }

        }

        /// <summary>
        /// Exporta un datatable a un CSV
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public void ExportToCSV(DataTable dt, String path)
        {
            TXTExport CSVExport = null;
            try
            {
                CSVExport = new TXTExport();
                CSVExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
                CSVExport.DataTable = dt;
                CSVExport.ExportType = TextExportType.CSV;
                CSVExport.ActionAfterExport = Spire.DataExport.Common.ActionType.None;
                CSVExport.SaveToFile(path);
            }
            finally
            {
                if (CSVExport != null)
                {
                    CSVExport.Dispose();
                    CSVExport = null;
                }
            }
        }

        public void ConvertHtmlToPDF(string html, string File)
        {
            try
            {


                //Create a pdf document.

                PdfDocument doc = new PdfDocument();
                PdfPageSettings pdfps = new PdfPageSettings();
                PdfHtmlLayoutFormat pdfLF = new PdfHtmlLayoutFormat();
                pdfLF.FitToHtml = Clip.None;
                pdfLF.FitToPage = Clip.None;


                String url = html;

                Thread thread = new Thread(() =>

                { doc.LoadFromHTML(url, true, true, true, pdfps, pdfLF, false); });

                thread.SetApartmentState(ApartmentState.STA);

                thread.Start();

                thread.Join();

                //Save pdf file.

                doc.SaveToFile(File);

                doc.Close();
            }
            catch (System.ArgumentException ex)
            {
                throw new Exception(ex.Message, ex);
            }


        }


        /// <summary>
        /// Convierte el word ubicado en la ruta a un PDF
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="PDFPath"></param>
        public void ConvertWordToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Document doc = null;

            try
            {
                doc = new Document();
                //Load word 2007 file from disk.
                doc.LoadFromFile(wordPath);
                //Save doc file to pdf.
                doc.SaveToFile(PDFPath, Spire.Doc.FileFormat.PDF);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }
            }
        }

        public void ConvertPowerPointToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Spire.Presentation.Presentation presentation = null;
            try
            {
                //create PPT document
                presentation = new Spire.Presentation.Presentation();
                //load PPT file from disk
                presentation.LoadFromFile(wordPath);
                //save the PPT do PDF file format
                presentation.SaveToFile(PDFPath, Spire.Presentation.FileFormat.PDF);
            }
            finally
            {
                if (presentation != null)
                {
                    presentation = null;
                }
            }
        }

        public void ConvertImageToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            PdfDocument doc = new PdfDocument();
            PdfImage image = null;
            try
            {
                PdfSection section = doc.Sections.Add();
                PdfPageBase page = doc.Pages.Add();

                //Load a tiff image from system
                image = PdfImage.FromFile(wordPath);
                //Set image display location and size in PDF
                float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
                float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
                float fitRate = Math.Max(widthFitRate, heightFitRate);
                float fitWidth = image.PhysicalDimension.Width / fitRate;
                float fitHeight = image.PhysicalDimension.Height / fitRate;
                page.Canvas.DrawImage(image, 30, 30, fitWidth, fitHeight);

                //save and launch the file
                doc.SaveToFile(PDFPath);
                doc.Close();
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                    doc.Dispose();
                    doc = null;
                }

                if (image != null)
                {
                    image = null;
                }
            }
        }


        public void ConvertTIFFToPDF(String ImageFilename, String PDFPath)
        {
            using (PdfDocument pdfDoc = new PdfDocument())
            {
                Image image = Image.FromFile(ImageFilename);
                Image[] img = SplitImages(image, ImageFormat.Png);

                for (int i = 0; i < img.Length; i++)
                {
                    PdfImage pdfImg = PdfImage.FromImage(img[i]);
                    PdfPageBase page = pdfDoc.Pages.Add();
                    float width = pdfImg.Width * 0.3f;
                    float height = pdfImg.Height * 0.3f;
                    float x = (page.Canvas.ClientSize.Width - width) / 2;

                    page.Canvas.DrawImage(pdfImg, x, 0, width, height);
                }

                pdfDoc.SaveToFile(PDFPath);
                image = null;
                img = null;

            }
        }

        public static Image[] SplitImages(Image image, ImageFormat format)
        {
            Guid guid = image.FrameDimensionsList[0];
            FrameDimension dimension = new FrameDimension(guid);
            int pageCount = image.GetFrameCount(dimension);
            Image[] frames = new Image[pageCount];

            for (int i = 0; i < pageCount; i++)
            {
                using (MemoryStream buffer = new MemoryStream())
                {
                    image.SelectActiveFrame(dimension, i);
                    image.Save(buffer, format);
                    frames[i] = Image.FromStream(buffer);
                }
            }
            return frames;
        }

        /// <summary>
        /// Convierte el word ubicado en la ruta a un PDF
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="PDFPath"></param>
        public void ConvertExcelToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Workbook workbook = null;

            try
            {
                workbook = new Workbook();
                try
                {
                    workbook.LoadFromFile(wordPath, ExcelVersion.Version2010);
                }
                catch (Exception)
                {
                    try
                    {
                        workbook.LoadFromFile(wordPath, ExcelVersion.Version2013);
                    }
                    catch (Exception)
                    {
                        try
                        {
                            workbook.LoadFromFile(wordPath, ExcelVersion.Version2007);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                workbook.LoadFromFile(wordPath, ExcelVersion.Version97to2003);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    workbook.LoadFromFile(wordPath, ExcelVersion.ODS);
                                }
                                catch (Exception)
                                {
                                    try
                                    {
                                        workbook.LoadFromFile(wordPath, ExcelVersion.Xlsb2007);
                                    }
                                    catch (Exception)
                                    {
                                        try
                                        {
                                            workbook.LoadFromFile(wordPath, ExcelVersion.Xlsb2010);
                                        }
                                        catch (Exception)
                                        {
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //Save doc file to pdf.
                workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
            }
            finally
            {
                if (workbook != null)
                {
                    workbook = null;
                }
            }
        }

        /// <summary>
        /// Reemplaza texto en un documento de word
        /// </summary>
        /// <param name="wordPath">Ruta del documento word</param>
        /// <param name="matchString">Palabra a reemplazar</param>
        /// <param name="newValue">Valor por el cual reemplazar</param>
        /// <param name="caseSensitive">Sensible a mayusculas</param>
        /// <param name="wholeWord">Reemplazar todas las palabras</param>
        public void ReplaceInWord(String wordPath, string matchString, string newValue, bool caseSensitive, bool wholeWord)
        {
            //Create word document
            Document document = null;

            try
            {
                document = new Document();

                //load a document
                document.LoadFromFile(wordPath);

                //Replace text
                document.Replace(matchString, newValue, caseSensitive, wholeWord);

                //Save doc file.
                document.SaveToFile(wordPath, Spire.Doc.FileFormat.Doc);
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
            }
        }

        /// <summary>
        /// Reemplaza el texto del documento word por los valores obtenidos de dos hashtables.
        /// </summary>
        /// <param name="wordPath">Ruta del documento</param>
        /// <param name="hash1">Key es el valor a buscar y Value el valor a reemplazar</param>
        /// <param name="hash2">Key es el valor a buscar y Value el valor a reemplazar</param>
        /// <remarks>Se utiliza para reemplazar el contenido del word con variables y texto inteligente.
        ///          Un hashtable es para las variables y el otro para texto inteligente, 
        ///          donde Key es el valor a buscar y el Value es el valor a reemplazar.
        /// </remarks>
        public void ReplaceInWord(string wordPath, Hashtable hash1, Hashtable hash2)
        {
            if ((hash1 != null && hash1.Count > 0) || (hash2 != null && hash2.Count > 0))
            {
                Document document = null;
                try
                {
                    //Create word document
                    document = new Document();
                    //load a document
                    document.LoadFromFile(wordPath);
                    //Replace the text
                    if (hash1 != null)
                        foreach (string key in hash1.Keys)
                            if (hash1[key] != null)
                                document.Replace(key, hash1[key].ToString(), true, true);
                    if (hash2 != null)
                        foreach (string key in hash2.Keys)
                            if (hash2[key] != null)
                                document.Replace(key, hash2[key].ToString(), true, true);
                    //Save doc file
                    document.SaveToFile(wordPath, wordPath.EndsWith(".docx") ? Spire.Doc.FileFormat.Docx : Spire.Doc.FileFormat.Doc);
                }
                finally
                {
                    if (document != null)
                    {
                        document.Close();
                        document = null;
                    }
                }
            }
        }

        /// <summary>
        /// Inserta una tabla en un word
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="data"></param>
        public void InsertTableInWord(String wordPath, DataTable data, Int32 section, bool fontConfig = false, Font font = null, Color color = new Color(), Color backColor = new Color())
        {
            //Create word document
            Document document = null;

            try
            {
                document = new Document();

                //load a document
                document.LoadFromFile(wordPath);
                if (section == -1)
                {
                    section = document.Sections.Count - 1;
                }

                //Replace text
                addTable(document.Sections[section], data, fontConfig, font,
                    color, backColor);
                //Save doc file.
                document.SaveToFile(wordPath, Spire.Doc.FileFormat.Doc);
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
            }
        }

        /// <summary>
        /// Agrega una tabla a la seccion
        /// </summary>
        /// <param name="section"></param>
        /// <param name="data"></param>
        private Spire.Doc.Table addTable(Spire.Doc.Section section, DataTable data, bool fontConfig = false, Font font = null, Color color = new Color(), Color backColor = new Color())
        {
            int offset = 0;
            TextRange text;
            Spire.Doc.Table table = section.AddTable();
            table.ResetCells(data.Rows.Count + 1, data.Columns.Count);

            // ***************** First Row *************************
            TableRow row = table.Rows[0];
            row.IsHeader = true;
            if (fontConfig)
            {
                table.TableFormat.Borders.Color = color;
                row.RowFormat.BackColor = color;
            }
            else
            {
                row.Height = 20;    //unit: point, 1point = 0.3528 mm
                row.HeightType = TableRowHeightType.Exactly;
                row.RowFormat.BackColor = Color.Gray;
            }
            offset = 1;
            for (int i = 0; i < data.Columns.Count; i++)
            {
                row.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph p = row.Cells[i].AddParagraph();
                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                TextRange txtRange = p.AppendText(data.Columns[i].ColumnName);
                if (fontConfig)
                {
                    /*Si esta habilitada la configuración de fuente invierte el color de fondo con el del texto 
                     *para la cabecera*/
                    txtRange.CharacterFormat.Font = font;
                    txtRange.CharacterFormat.TextColor = backColor;
                }
                txtRange.CharacterFormat.Bold = true;

            }
            for (int r = 0; r < data.Rows.Count; r++)
            {
                TableRow dataRow = table.Rows[r + offset];
                dataRow.Height = 20;
                dataRow.HeightType = TableRowHeightType.Exactly;
                dataRow.RowFormat.BackColor = Color.Empty;
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    dataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    text = dataRow.Cells[c].AddParagraph().AppendText(data.Rows[r][c].ToString());
                    if (fontConfig)
                    {
                        dataRow.Cells[c].CellFormat.BackColor = backColor;
                        text.CharacterFormat.Font = font;
                        text.CharacterFormat.TextColor = color;
                    }
                }
            }

            return table;
        }

        /// <summary>
        /// Inserta una cadena de caracteres en un word
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="data"></param>
        public void InsertTextInWord(String wordPath, String data, Int32 section, bool textAsTable = false, bool fontConfig = false, Font font = null, Color color = new Color(), Color BackColor = new Color())
        {
            //Create word document
            Document document = null;

            try
            {
                document = new Document();

                //load a document
                document.LoadFromFile(wordPath);
                if (section == -1)
                {
                    section = document.Sections.Count - 1;
                }

                Paragraph p;
                TextRange text;
                if (textAsTable)
                {
                    Table table = document.Sections[section].AddTable();
                    table.TableFormat.Borders.Color = color;
                    table.ResetCells(1, 1);
                    TableRow row = table.Rows[0];
                    row.IsHeader = true;
                    row.RowFormat.BackColor = BackColor;
                    p = row.Cells[0].AddParagraph();
                    text = p.AppendText(data);
                }
                else
                {
                    //Replace text
                    p = document.Sections[section].AddParagraph();
                    text = p.AppendText(data);
                }

                if (fontConfig)
                {
                    text.CharacterFormat.Font = font;
                    text.CharacterFormat.TextColor = color;
                    text.CharacterFormat.TextBackgroundColor = BackColor;
                }

                //Save doc file.

                document.SaveToFile(wordPath, Spire.Doc.FileFormat.Doc);
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
            }
        }

        public void CompleteTableInWord(string wordpath, Int32 pageindex, Int32 tableindex, bool withheader, DataTable dt, bool intable, Int32 rownindex, bool fontConfig = false, Font font = null, Color color = new Color(), Color backColor = new Color())
        {
            Document doc = null;

            try
            {
                doc = new Document(wordpath);
                if (pageindex == -1)
                {
                    pageindex = doc.Sections.Count - 1;
                }
                Spire.Doc.Section section = doc.Sections[pageindex];

                Spire.Doc.Interface.ITable table;
                if (intable)
                    table = section.Tables[tableindex].Rows[rownindex].Cells[0].Tables[0];
                else
                    table = section.Tables[tableindex];
                //#region replace text
                //TableCell cell1 = table.Rows[1].Cells[0];
                //Paragraph p1 = cell1.Paragraphs[0];
                //p1.Text = "abc";

                //TableCell cell2 = table.Rows[1].Cells[1];
                //Paragraph p2 = cell2.Paragraphs[0];
                //p2.Items.Clear();
                //p2.AppendText("def");

                //TableCell cell3 = table.Rows[1].Cells[2];
                //Paragraph p3 = cell3.Paragraphs[0];
                //(p3.Items[0] as TextRange).Text = "hij";
                //#endregion
                #region add rows

                foreach (DataRow dr in dt.Rows)
                {

                    TableRow newRow = table.AddRow(true, true);
                    TextRange newText;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        newText = newRow.Cells[i].AddParagraph().AppendText(dr[i].ToString());

                        if (fontConfig)
                        {
                            newRow.Cells[i].CellFormat.BackColor = backColor;
                            newText.CharacterFormat.Font = font;
                            newText.CharacterFormat.TextColor = color;
                        }

                    }

                    table.Rows.Insert(table.Rows.Count - 1, newRow);
                }

                #endregion

                doc.SaveToFile(wordpath);
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close();
                    doc = null;
                }
            }
        }

        /// <summary>
        /// Obtiene el texto de un documento
        /// </summary>
        /// <param name="wordPath">Ruta del documento</param>
        /// <returns>Texto del documento</returns>
        public string GetText(string wordPath)
        {
            Document document = null;

            try
            {
                document = new Document();
                document.LoadFromFile(wordPath);
                return document.GetText();
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
            }
        }

        public string GetTextFromDoc(string wordPath)

        {
            {
                Document document = null;

                try
                {


                    //Create word document
                    if (System.IO.Path.GetExtension(wordPath).Equals(".doc"))
                    {

                        document = new Document(wordPath);
                    }
                    else
                    {
                        document = new Document();

                        //load a document
                        document.LoadFromFile(wordPath);
                    }

                    return document.GetText();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Zip exception.Can't locate end of central directory record"))
                    {

                        //                        ZException.Log(new Exception("Error al obtener el documento : " + wordPath + " " + ex.ToString()));

                    }
                    ZException.Log(new Exception("Error al obtener el documento : " + wordPath + " " + ex.ToString()));

                    throw ex;

                }
                finally
                {
                    if (document != null)
                    {
                        document.Close();
                        document = null;
                    }
                }

            }
        }

        public string GetTextFromPDF(String pdfPath)
        {
            PdfDocument document = null;
            StringBuilder PDFtext = null;
            Spire.Pdf.Widget.PdfPageCollection PageArray = null;
            try
            {
                document = new PdfDocument();

                document.LoadFromFile(pdfPath);

                PDFtext = new StringBuilder();

                PageArray = document.Pages;

                //Get text from each page
                foreach (PdfPageBase page in document.Pages)
                {
                    try
                    {
                        PDFtext.Append(page.ExtractText());
                    }
                    catch (Spire.Pdf.Exceptions.PdfDocumentException)
                    { // Spire error getting text from PDF
                    }
                }

                return PDFtext.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (document != null)
                {
                    document.Close();
                    document = null;
                }
                if (PDFtext != null) PDFtext = null;
                if (PageArray != null) PageArray = null;
            }
        }

        public string GetTextFromExcel(String excelPath)
        {
            Workbook workbook = null;
            StringBuilder excelText = null;

            //CultureInfo cc = Thread.CurrentThread.CurrentCulture;
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            try
            {
                workbook = new Workbook();
                workbook.LoadFromFile(excelPath);

                excelText = new StringBuilder();

                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    //Initailize worksheet
                    Worksheet sheet = workbook.Worksheets[i];

                    //Read value from worksheet
                    foreach (CellRange range in sheet.AllocatedRange)//SerializeData
                    {
                        if (!String.IsNullOrEmpty(range.DisplayedText))
                            //excelText.Append(range.Text + " ");
                            excelText.Append(range.DisplayedText + " ");

                    }
                }
                return excelText.ToString();
            }
            catch (Exception e)
            {
                Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError, e.Message);
                return String.Empty;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Dispose();
                    workbook = null;
                }
                if (excelText != null)
                {
                    excelText = null;
                }
                //Thread.CurrentThread.CurrentCulture = cc;
            }
        }


        public string GetTextFromMsg(string filePath)
        {
            {
                Document document = null;

                try
                {
                    //Create MSG document
                    if (Path.GetExtension(filePath).Equals(".msg"))
                    {
                        document = new Document(filePath, Spire.Doc.FileFormat.Auto);
                    }
                    else
                    {
                        document = new Document();
                        //load a document
                        document.LoadFromFile(filePath);
                    }

                    return document.GetText();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Msg exception.Can't locate end of central directory record"))
                    {
                        //ZException.Log(new Exception("Error al obtener el documento : " + wordPath + " " + ex.ToString()));
                    }
                    ZException.Log(new Exception("Error al obtener el documento : " + filePath + " " + ex.ToString()));
                    throw ex;
                }
                finally
                {
                    if (document != null)
                    {
                        document.Close();
                        document = null;
                    }
                }

            }
        }

        public string GetTextFromPowerPoint(string filePath)
        {


            Workbook workbook = null;
            StringBuilder PowerPointText = null;

            //CultureInfo cc = Thread.CurrentThread.CurrentCulture;
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            try
            {
                workbook = new Workbook();
                workbook.LoadFromFile(filePath);

                PowerPointText = new StringBuilder();

                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    //Initailize worksheet
                    Worksheet sheet = workbook.Worksheets[i];

                    //Read value from worksheet
                    foreach (CellRange range in sheet.AllocatedRange)//SerializeData
                    {
                        if (!String.IsNullOrEmpty(range.DisplayedText))
                            //excelText.Append(range.Text + " ");
                            PowerPointText.Append(range.DisplayedText + " ");

                    }
                }
                return PowerPointText.ToString();
            }
            catch (Exception e)
            {
                Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError, e.Message);
                return String.Empty;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Dispose();
                    workbook = null;
                }
                if (PowerPointText != null)
                {
                    PowerPointText = null;
                }
                //Thread.CurrentThread.CurrentCulture = cc;
            }
        }
    }

    // [JUDN 11-11-2014]:Guarda un Excel dentro de un Dataset. Guardando cada hoja en un datatable del dataset.


}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
using Spire.Pdf.HtmlConverter;
using System.Drawing.Printing;
using System.IO;
using Zamba.AppBlock;
using Spire.Barcode;
using Zamba.Core;
using Zamba.Tools;
using System.Threading;
using Spire.Email;
using System.Reflection;
using Spire.Email.IMap;

namespace Zamba.FileTools
{
    public class SpireTools : ISpireTools
    {
        private string barcodeInBase64;
        public byte[] BytesFile { get; set; }
        public Stream StreamFile { get; private set; }

        public SpireTools()
        {

        }

        public SpireTools(byte[] file)
        {
            this.BytesFile = file;
            StreamFile = new MemoryStream(BytesFile);
        }

        /// <summary>
        /// Exporta un datatable a Excel
        /// </summary>
        /// <param name = "dt" ></ param >
        /// < param name="path"></param>
        /// 
        public DataTable GetExcelAsDataSet(string file, string sheetName = "")
        {
            Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
            System.Type t = tt.GetType("Zamba.SpireTools.SpireTools", true, true);

            ISpireTools Rule = (ISpireTools)Activator.CreateInstance(t);
            return Rule.GetExcelAsDataSet(file, sheetName);

        }



        public Boolean ExportToXLS(DataTable dt, String path)
        {
            Workbook ef = null;
            Worksheet ws = null;
            try
            {
                ef = new Workbook();
                ws = ef.Worksheets.Add("Exportacion");

                while (ef.Worksheets.Count != 1)
                {
                    ef.Worksheets.Remove(0);
                }

                ws.InsertDataTable(dt, true, 1, 1);

                ef.SaveToFile(path);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo {ex}"));
                return false;
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


        public Boolean ExportToXLSx(DataTable dt, String path)
        {
            Workbook ef = null;
            Worksheet ws = null;
            try
            {
                ef = new Workbook();
                ef.Version = ExcelVersion.Version2013;

                ws = ef.Worksheets.Add("Exportacion");

                while (ef.Worksheets.Count != 1)
                {
                    ef.Worksheets.Remove(0);
                }

                ws.InsertDataTable(dt, true, 1, 1);

                ef.SaveToFile(path, ExcelVersion.Version2013);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo {ex}"));
                return false;
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

        public void ExportToXLSTable(DataTable dt)
        {
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets[0];
            sheet.InsertDataTable(dt, true, 1, 1);
            book.SaveToFile("DocumentoEXCEL.xls", ExcelVersion.Version97to2003);
            System.Diagnostics.Process.Start("DocumentoEXCEL.xls");


        }

        public void ExportToPDFTable(DataTable dt)
        {
            Spire.DataExport.PDF.PDFExport PDFExport = new Spire.DataExport.PDF.PDFExport();
            PDFExport.DataSource = Spire.DataExport.Common.ExportSource.DataTable;
            PDFExport.DataTable = dt;
            PDFExport.PDFOptions.PageOptions.Height = 11.67;
            PDFExport.PDFOptions.PageOptions.MarginBottom = 0.78;
            PDFExport.PDFOptions.PageOptions.MarginLeft = 0.2;
            PDFExport.PDFOptions.PageOptions.MarginRight = 0.2;
            PDFExport.PDFOptions.PageOptions.MarginTop = 0.78;
            PDFExport.PDFOptions.PageOptions.Width = 30;
            PDFExport.PDFOptions.PageOptions.Orientation = 0;
            PDFExport.PDFOptions.ColSpacing = 0;
            PDFExport.PDFOptions.RowSpacing = 2;
            PDFExport.PDFOptions.GridLineWidth = 2;

            PDFExport.ActionAfterExport = Spire.DataExport.Common.ActionType.OpenView;
            PDFExport.SaveToFile("DocumentoPDF.pdf");
        }

        /// <summary>
        /// Exporta un datatable a un CSV
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public Boolean ExportToCSV(DataTable dt, String path)
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
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo {ex}"));
                return false;
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

        /// <summary>
        /// Convierte el word ubicado en la ruta a un PDF
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="PDFPath"></param>

        public Boolean ConvertWordToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Document doc = null;

            try
            {
                doc = new Document();

                //Load word 2007 file from disk.
                if (File.Exists(wordPath))
                    doc.LoadFromFile(wordPath);
                else if (StreamFile != null)
                    doc.LoadFromStream(StreamFile, Spire.Doc.FileFormat.PDF);

                //Save doc file to pdf.
                doc.SaveToFile(PDFPath, Spire.Doc.FileFormat.PDF);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
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
        public Boolean ConvertXPSToPFD(String wordPath, String PDFPath)
        {
            //Document doc = null;
            PdfDocument doc = null;
            try
            {
                doc = new PdfDocument();

                if (File.Exists(wordPath))
                    doc.LoadFromXPS(wordPath);
                else if (StreamFile != null)
                    doc.LoadFromXPS(StreamFile);

                doc.SaveToFile(PDFPath);
                doc.Close();
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
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

        public Boolean ConvertHTMLToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Document doc = null;

            try
            {
                doc = new Document();
                //Load word 2007 file from disk.

                if (File.Exists(wordPath))
                    doc.LoadFromFile(wordPath, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);
                else if (StreamFile != null)
                    doc.LoadFromStream(StreamFile, Spire.Doc.FileFormat.Html, XHTMLValidationType.None);

                //Save doc file to pdf.
                doc.SaveToFile(PDFPath, Spire.Doc.FileFormat.PDF);
                return true;
            }
            catch (Exception ex)
            {
                PdfDocument pdf = new PdfDocument();
                try
                {

                    PdfHtmlLayoutFormat htmlLayoutFormat = new PdfHtmlLayoutFormat();
                    htmlLayoutFormat.IsWaiting = false;
                    PdfPageSettings setting = new PdfPageSettings();
                    setting.Size = PdfPageSize.A3;
                    string htmlCode = File.ReadAllText(wordPath);


                    Thread thread = new Thread(() =>
                   {
                       pdf.LoadFromHTML(htmlCode, true, setting, htmlLayoutFormat);
                   });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    pdf.SaveToFile(PDFPath);
                    return true;
                }
                catch (Exception ex1)
                {
                    ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex1}"));
                    return false;
                }
                finally
                {
                    pdf.Close();
                    pdf.Dispose();
                    pdf = null;
                }
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

        public Boolean ConvertPowerPointToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Spire.Presentation.Presentation presentation = null;
            try
            {
                //create PPT document
                presentation = new Spire.Presentation.Presentation();

                //load PPT file from disk or db
                if (File.Exists(wordPath))
                    presentation.LoadFromFile(wordPath);
                else if (StreamFile != null)
                    presentation.LoadFromStream(StreamFile, Spire.Presentation.FileFormat.PDF);

                //save the PPT do PDF file format
                presentation.SaveToFile(PDFPath, Spire.Presentation.FileFormat.PDF);
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
            }

            finally
            {
                if (presentation != null)
                {
                    presentation = null;
                }
            }
        }

        public Boolean ConvertImageToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            PdfDocument doc = new PdfDocument();

            try
            {
                PdfSection section = doc.Sections.Add();
                PdfPageBase page = doc.Pages.Add();

                //Load a tiff image from system
                PdfImage image = null;

                if (File.Exists(wordPath))
                    image = PdfImage.FromFile(wordPath);
                else if (StreamFile != null)
                    image = PdfImage.FromStream(StreamFile);

                if (image != null)
                {
                    //Set image display location and size in PDF
                    float widthFitRate = image.PhysicalDimension.Width / page.Canvas.ClientSize.Width;
                    float heightFitRate = image.PhysicalDimension.Height / page.Canvas.ClientSize.Height;
                    float fitRate = Math.Max(widthFitRate, heightFitRate);
                    float fitWidth = image.PhysicalDimension.Width / fitRate;
                    float fitHeight = image.PhysicalDimension.Height / fitRate;
                    page.Canvas.DrawImage(image, 10, 10, fitWidth, fitHeight);

                    //save and launch the file
                    doc.SaveToFile(PDFPath);
                    doc.Close();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
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

        public Boolean ConvertTIFFToPDF(String ImageFilename, String PDFPath)
        {
            try
            {

                using (PdfDocument pdfDoc = new PdfDocument())
                {
                    Image image = null;

                    if (File.Exists(ImageFilename))
                        image = Image.FromFile(ImageFilename);
                    else if (StreamFile != null)
                        image = Image.FromStream(StreamFile);

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
                }
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo {ex}"));
                return false;
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
        public Boolean ConvertExcelToPDF(String wordPath, String PDFPath)
        {
            //Create a new document 
            Workbook workbook = null;

            try
            {
                workbook = new Workbook();
                try
                {
                    if (File.Exists(wordPath))
                        workbook.LoadFromFile(wordPath, ExcelVersion.Version2013);
                    else if (StreamFile != null)
                        workbook.LoadFromStream(StreamFile, ExcelVersion.Version2013);

                    //Save doc file to pdf.
                    workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                }
                catch (Exception e)
                {
                    if (wordPath.EndsWith("xlsx") == false)
                    {
                        try
                        {
                            if (File.Exists(wordPath))
                                workbook.LoadFromFile(wordPath, ExcelVersion.Version2010);
                            else if (StreamFile != null)
                                workbook.LoadFromStream(StreamFile, ExcelVersion.Version2010);
                            //Save doc file to pdf.
                            workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                        }
                        catch (Exception e1)
                        {
                            try
                            {
                                if (File.Exists(wordPath))
                                    workbook.LoadFromFile(wordPath, ExcelVersion.Version2007);
                                else if (StreamFile != null)
                                    workbook.LoadFromStream(StreamFile, ExcelVersion.Version2007);
                                //Save doc file to pdf.
                                workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                            }
                            catch (Exception e2)
                            {
                                try
                                {
                                    if (File.Exists(wordPath))
                                        workbook.LoadFromFile(wordPath, ExcelVersion.Version97to2003);
                                    else if (StreamFile != null)
                                        workbook.LoadFromStream(StreamFile, ExcelVersion.Version97to2003);

                                    //Save doc file to pdf.
                                    workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                                }
                                catch (Exception e3)
                                {
                                    try
                                    {
                                        if (File.Exists(wordPath))
                                            workbook.LoadFromFile(wordPath, ExcelVersion.ODS);
                                        else if (StreamFile != null)
                                            workbook.LoadFromStream(StreamFile, ExcelVersion.ODS);

                                        //Save doc file to pdf.
                                        workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                                    }
                                    catch (Exception e4)
                                    {
                                        try
                                        {
                                            if (File.Exists(wordPath))
                                                workbook.LoadFromFile(wordPath, ExcelVersion.Xlsb2010);
                                            else if (StreamFile != null)
                                                workbook.LoadFromStream(StreamFile, ExcelVersion.Xlsb2010);

                                            //Save doc file to pdf.
                                            workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                                        }
                                        catch (Exception e5)
                                        {
                                            try
                                            {
                                                if (File.Exists(wordPath))
                                                    workbook.LoadFromFile(wordPath, ExcelVersion.Xlsb2007);
                                                else if (StreamFile != null)
                                                    workbook.LoadFromStream(StreamFile, ExcelVersion.Xlsb2007);

                                                //Save doc file to pdf.
                                                workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                                            }
                                            catch (Exception e7)
                                            {
                                                try
                                                {
                                                    if (File.Exists(wordPath))
                                                        workbook.LoadFromFile(wordPath);
                                                    else if (StreamFile != null)
                                                        workbook.LoadFromStream(StreamFile);

                                                    //Save doc file to pdf.
                                                    workbook.SaveToFile(PDFPath, Spire.Xls.FileFormat.PDF);
                                                }
                                                catch (Exception e6)
                                                {
                                                    try
                                                    {
                                                        Spire.Doc.Document document = new Spire.Doc.Document();

                                                        if (File.Exists(wordPath))
                                                            document.LoadFromFile(wordPath, Spire.Doc.FileFormat.Auto);
                                                        else if (StreamFile != null)
                                                            document.LoadFromStream(StreamFile, Spire.Doc.FileFormat.Auto);

                                                        document.SaveToFile(PDFPath, Spire.Doc.FileFormat.PDF);
                                                    }
                                                    catch (Exception e8)
                                                    {

                                                        ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {e8.Message}"));
                                                        return false;
                                                    }

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {e}"));
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
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
        /// Obtiene de los volumenes o de la base de datos y estandariza el archivo CSV pasado por parametro y lo guarda en una ruta temporal.
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="PDFPath"></param>
        /// <returns></returns>
        public Boolean ConvertCSVToPDF(String wordPath, String PDFPath)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obteniendo Archivo CSV almacenado.");

                StreamReader sr = null;
                Workbook wb = new Workbook();
                string fullCSV = "";
                string CSVPath = "";

                if (File.Exists(wordPath))
                {
                    sr = File.OpenText(wordPath);
                    fullCSV = sr.ReadToEnd();

                    if (fullCSV.Split(';').Length > fullCSV.Split(',').Length)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Empieza convercion de CSV.");
                        CSVPath = Convert_CSVSemicolonToCSVComma(sr);
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convercion de CSV OK.");
                        wb.LoadFromFile(CSVPath, ",", 1, 1);
                    }
                    else
                        wb.LoadFromFile(wordPath, ",", 1, 1);

                    wb.ConverterSetting.SheetFitToPage = true;
                }
                else if (StreamFile != null)
                {
                    sr = new StreamReader(StreamFile);
                    fullCSV = sr.ReadToEnd();

                    if (fullCSV.Split(';').Length > fullCSV.Split(',').Length)
                    {
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Empieza convercion de CSV.");
                        CSVPath = Convert_CSVSemicolonToCSVComma(sr);
                        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convercion de CSV OK.");
                        wb.LoadFromFile(CSVPath, ",", 1, 1);
                    }
                    else
                        wb.LoadFromStream(StreamFile, ",", 1, 1);

                    wb.ConverterSetting.SheetFitToPage = true;
                }

                Worksheet sheet = wb.Worksheets[0];
                for (int i = 1; i < sheet.Columns.Length; i++)
                {
                    sheet.AutoFitColumn(i);
                }

                sr.Close();
                sr.Dispose();

                wb.SaveToPdf(PDFPath);
                wb.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
            }
        }

        //private string Convert_CSVSemicolonToCSVComma(string path)
        private string Convert_CSVSemicolonToCSVComma(StreamReader SR)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Inicio la funcion [Convert_CSVSemicolonToCSVComma].");
                string TemporaryPath = Zamba.Membership.MembershipHelper.AppTempDir(@"\OfficeTemp\").FullName + DateTime.Now.Ticks.ToString() + ".csv";
                //StreamReader SR = new StreamReader(path);
                StreamWriter SW = new StreamWriter(TemporaryPath, false, Encoding.UTF8);

                string CSVInput = SR.ReadToEnd();
                string CSVOutput = "";

                string[] Array_Lines = CSVInput.Split('\n');

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Empieza convercion de CSV...");
                foreach (String itemRow in Array_Lines)
                {
                    CSVReadStates VarState = CSVReadStates.StartRow;
                    string Cell = "";
                    string Row = "";

                    for (int i = 0; i < itemRow.Length; i++)
                    {
                        char Character = itemRow.ToCharArray()[i];
                        char? NextCharacter = null;

                        if (i < itemRow.Length - 1)
                            NextCharacter = itemRow.ToCharArray()[i + 1];
                        else
                            VarState = CSVReadStates.EndRow;

                        switch (VarState)
                        {
                            case CSVReadStates.StartRow:
                                if (Character != '\"')
                                {
                                    Cell += Character.ToString();
                                    VarState = CSVReadStates.ReadingWithoutQuotes;
                                }
                                else
                                    VarState = CSVReadStates.ReadingBetweenQuotes;

                                break;
                            case CSVReadStates.StartCell:
                                Row += ",";

                                if (Character == '\"')
                                    VarState = CSVReadStates.ReadingBetweenQuotes;
                                else
                                {
                                    if (Character != ';')
                                    {
                                        Cell += Character.ToString();
                                        VarState = CSVReadStates.ReadingWithoutQuotes;
                                    }
                                }

                                break;
                            case CSVReadStates.ReadingBetweenQuotes:
                                if (Character == '\"')
                                {
                                    if (VarState != CSVReadStates.EndRow)
                                    {
                                        if (NextCharacter == '\"')
                                        {
                                            Cell += "\"\"";
                                            i++;
                                        }
                                        else
                                        {
                                            VarState = CSVReadStates.EndCell;
                                        }
                                    }
                                }
                                else
                                    Cell += Character.ToString();

                                break;
                            case CSVReadStates.ReadingWithoutQuotes:
                                if (Character == ';')
                                {
                                    Row += '"' + Cell + '"';
                                    Cell = "";
                                    VarState = CSVReadStates.StartCell;
                                }
                                else
                                    Cell += Character.ToString();

                                break;
                            case CSVReadStates.EndRow:
                                Row += '"' + Cell + '"';
                                VarState = CSVReadStates.StartCell;

                                if (Character == ';')
                                    Row += ",\"\"";

                                Cell = "";

                                CSVOutput += Row + "\n";

                                break;
                            case CSVReadStates.EndCell:
                                Row += '"' + Cell + '"';
                                VarState = CSVReadStates.StartCell;

                                Cell = "";

                                break;
                            default:
                                break;
                        }

                    }

                }

                // Creo el Archivo CSV.
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Terminado convercion de CSV.");

                SW.Write(CSVOutput);
                SW.Close();
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Se creo el archivo CSV correspondiente en la ruta" + TemporaryPath);

                return TemporaryPath;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir CSV: {ex}"));
                throw;
            }
        }

        private enum CSVReadStates
        {
            StartRow,
            StartCell,
            ReadingBetweenQuotes,
            ReadingWithoutQuotes,
            EndRow,
            EndCell
        }

        public static void WriteToFile()
        {
            using (StreamWriter sw = File.CreateText(@"E:\Programming Practice\CSharp\Console\table.tbl"))
            {
                sw.WriteLine("Please find the below generated table of 1 to 10");

                sw.WriteLine("");
                for (int i = 1; i <= 10; i++)
                {
                    sw.WriteLine("==============");
                }
            }
        }
        public string ConvertMsgToEml(string MsgPath)
        {
            Aspose.Email.MailMessage asposeObj = Aspose.Email.MailMessage.Load(MsgPath, new Aspose.Email.MsgLoadOptions());
            var EmlPath = "";

            try
            {
                EmlPath = Path.ChangeExtension(MsgPath, ".eml");
                asposeObj.Save(EmlPath, Aspose.Email.SaveOptions.DefaultEml);
                return EmlPath;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                throw ex;
            }
            finally
            {
                asposeObj.Dispose();
            }
        }

        /// <summary>
        /// Convierte un .msg a un HTML mediante la particion de los datos del archivo .msg
        /// </summary>
        /// <param name="msgPath"></param>
        /// <param name="PDFPath"></param>
        /// <returns></returns>
        public object ConvertMSGToHTML(String msgPath, String PDFPath, bool includeAttachs)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF OK");
                MailMessage miMailMessage = MailMessage.Load(msgPath);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF OK");
                MailPreview miMailPreview = new MailPreview(miMailMessage, includeAttachs);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF PREVIEW OK");
                object obj_toJson = miMailPreview;
                return obj_toJson;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Convierte un .msg a un HTML mediante la particion de los datos del archivo .msg
        /// </summary>
        /// <param name="msgPath"></param>
        /// <param name="PDFPath"></param>
        /// <returns></returns>
        public object ConvertMSGToJSON(String msgPath, String PDFPath, bool includeAttachs)
        {
            try
            {
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convirtiendo correo a objeto");
                MailMessage miMailMessage = MailMessage.Load(msgPath);
                MailPreview miMailPreview = new MailPreview(miMailMessage, includeAttachs);

                miMailPreview.body = UpdateBodyMessage(msgPath, miMailPreview);

                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Convercion Completada con exito.");
                object obj_toJson = miMailPreview;
                return obj_toJson;
            }
            catch (FileNotFoundException ex)
            {
                ZClass.raiseerror(ex);
                Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Obtiene la posicion del string especificado por parametro dentro del body de un mail y devuelve una lista de indices
        /// </summary>
        /// <param name="FullMessage"></param>
        /// <param name="str"></param>
        /// <returns>devuelve una lista de indices</returns>
        private static List<int> GetPositionsSrcFromBody(string body, string str)
        {
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Buscando ubicacion de elementos 'IMG'...");
            List<int> IndicesList = new List<int>();
            int index = 0;
            while (index != -1)
            {
                for (; ; index += str.Length)
                {
                    index = body.IndexOf(str, index);
                    if (index == -1)
                        break;
                    else
                    {
                        IndicesList.Add(index);
                    }
                }
            }
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Se encontraron " + IndicesList.Count + " ubicaciones efectivas.");
            return IndicesList;
        }

        private static string UpdateBodyMessage(string msgPath, MailPreview miMailPreview)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Realizando mapeo de imagenes embebidas");

            List<ObjUUIDsDTO> keyValuePairs = new List<ObjUUIDsDTO>();
            List<int> UUIDIndices = GetPositionsSrcFromBody(miMailPreview.body, "cid:");

            Aspose.Email.MailMessage messagePro = Aspose.Email.MailMessage.Load(msgPath);

            try
            {
                foreach (int item in UUIDIndices)
                {
                    string targetUUID = miMailPreview.body.Substring(item + 4, miMailPreview.body.Substring(item + 4).IndexOf("\""));
                    var targetUUIDsplitted = targetUUID.Split('@');

                    //Validacion solo para permitir UUIDs como valor.
                    if (!targetUUIDsplitted.Contains(".com"))
                    {
                        if (!(targetUUIDsplitted.First().Contains("~")))
                        {
                            foreach (var itemLinkedResource in messagePro.LinkedResources)
                            {
                                if (targetUUID == itemLinkedResource.ContentId)
                                {
                                    keyValuePairs.Add(GetValuePairs(targetUUID, itemLinkedResource));
                                }
                            }
                        }
                    }
                }

                foreach (var item in keyValuePairs)
                {
                    miMailPreview.body = miMailPreview.body.Replace("cid:" + item.UUID, "data:image/" + item.FileName + ";base64," + item.Base64String);
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
                Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError, ex.Message);
                throw ex;
            }
            finally
            {
                messagePro.Dispose();
            }

            return miMailPreview.body;
        }

        public static ObjUUIDsDTO GetValuePairs(string targetUUID, Aspose.Email.LinkedResource itemLinkedResource)
        {
            Stream targetPartStream = itemLinkedResource.ContentStream;
            Byte[] bytes;

            using (var memoryStream = new MemoryStream())
            {
                targetPartStream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string Base64String = System.Convert.ToBase64String(bytes);

            return new ObjUUIDsDTO
            {
                UUID = targetUUID,
                Base64String = Base64String,
                FileName = itemLinkedResource.ContentDisposition.FileName
            };
        }

        public class ObjUUIDsDTO
        {
            public string UUID { get; set; }
            public string Base64String { get; set; }
            public string FileName { get; set; }
        }

        /// <summary>
        /// Convierte un .msg a un HTML mediante la particion de los datos del archivo .msg
        /// </summary>
        /// <param name="msgPath"></param>
        /// <param name="PDFPath"></param>
        /// <returns></returns>
        public object ConvertMSGToJSON(Stream msgStream, String PDFPath, bool includeAttachs)
        {
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF OK");
            MailMessage miMailMessage = MailMessage.Load(msgStream, MailMessageFormat.Msg);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF OK");
            MailPreview miMailPreview = new MailPreview(miMailMessage, includeAttachs);
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "MSG a PDF PREVIEW OK");

            object obj_toJson = miMailPreview;
            return obj_toJson;

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
        public Boolean InsertTableInWord(String wordPath, DataTable data, Int32 section, bool fontConfig = false, Font font = null, Color color = new Color(), Color backColor = new Color())
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
                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo: {wordPath} {ex}"));
                return false;
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

                    throw;

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

        // Metodos para la generacion de Barcode
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

                Zamba.Core.UserPreferences UP = new Zamba.Core.UserPreferences();
                int bcType = Int32.Parse(UP.getValueForMachine("BarCodeType", UPSections.Barcode, 1));

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

        public string GetBase64StringFromImage(Image img, ImageFormat imgFormat)
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

        public Boolean CreatePdfFromAnotherPdf(string[] list, String PDFPath)
        {
            try
            {
                PdfDocumentBase doc = PdfDocument.MergeFiles(list);
                doc.Save(PDFPath, Spire.Pdf.FileFormat.PDF);

                return true;
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(new Exception($"Error al convertir a PDF el archivo {ex}"));
                return false;
            }


        }


        public void PrintHtmlDocByHtmlString(string htmlString, short copies, string tempFile, float Width, float Height)
        {
            PdfDocument doc = new PdfDocument();
            //PrinterSettings DefaultPrinterSettings = new PrinterSettings(); 
            try
            {
                float _Width = 0;
                float _Height = 0;
                PdfUnitConvertor unitCvtr;

                if (Width <= 0 || Height <= 0)
                {
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se encuentran configuradas las medidas de la hoja a imprimir, se establecen en 210 mm x 297 mm (A4)");
                    Width = 210;
                    Height = 297;
                }

                unitCvtr = new PdfUnitConvertor();
                _Width = unitCvtr.ConvertUnits(Width, PdfGraphicsUnit.Millimeter, PdfGraphicsUnit.Point);
                _Height = unitCvtr.ConvertUnits(Height, PdfGraphicsUnit.Millimeter, PdfGraphicsUnit.Point);

                //Configuro la pagina PDF
                PdfPageSettings pdfPageSettings = new PdfPageSettings();
                pdfPageSettings.SetMargins(0, 0, 0, 0);
                pdfPageSettings.Orientation = PdfPageOrientation.Landscape;
                pdfPageSettings.Size = new SizeF(_Width, _Height);

                //Lo necesito para pasar al metodo
                PdfHtmlLayoutFormat pdfHtmlLayoutFormat = new PdfHtmlLayoutFormat();
                pdfHtmlLayoutFormat.IsWaiting = false;

                Thread thread = new Thread(() => { doc.LoadFromHTML(htmlString, false, pdfPageSettings, pdfHtmlLayoutFormat); });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
                doc.SaveToFile(tempFile);
            }
            catch (Exception e)
            {
                ZClass.raiseerror(e);
            }
            finally
            {
                if (doc != null)
                    doc.Close();
            }

        }

        /// <summary>
        /// Metodo para extraer los archivos adjuntos de un mail con la libreria Spire
        /// retorna diccionario con nombre y contenido de cada adjunto
        /// </summary>
        /// <param name="msgFile">Mail del que extraer los adjuntos</param>
        /// <returns>Retorna un diccionario conteniendo el nombre y contenido de cada archivo adjunto</returns>
        public Dictionary<string, Stream> GetEmailAttachs(string msgFile)
        {
            Dictionary<string, Stream> attachs = new Dictionary<string, Stream>();
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);

                foreach (Attachment attach in mail.Attachments)
                    attachs.Add(attach.ContentType.Name, attach.Data);

            }
            catch (Exception)
            {
                throw;
            }

            return attachs;
        }

        /// <summary>
        /// Metodo que retorna la cantidad de adjuntos de un mail
        /// </summary>
        /// <param name="msgFile">El mail que contiene los adjuntos</param>
        /// <returns>El numero de adjuntos</returns>
        public long GetEmailAttachsCount(string msgFile)
        {
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);
                return mail.Attachments.Count;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Metodo que devuelve una lista con los nombres de los archivos adjuntos
        /// </summary>
        /// <param name="msgFile">El mail que contiene los adjuntos</param>
        /// <returns>List de nombres</returns>
        public List<string> GetEmailAttachsNames(string msgFile)
        {
            try
            {
                MailMessage mail = MailMessage.Load(msgFile);
                return mail.Attachments.Select(a => a.ContentType.Name).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable GetExcelAsDataSet(string file)
        {
            Assembly tt = Assembly.LoadFrom(Zamba.Membership.MembershipHelper.StartUpPath + "\\Spire\\Zamba.SpireTools.dll");
            System.Type t = tt.GetType("Zamba.SpireTools.SpireTools", true, true);

            ISpireTools Rule = (ISpireTools)Activator.CreateInstance(t);
            return Rule.GetExcelAsDataSet(file);
        }

        public MailPreview ConvertEmlToMsg(Stream file)
        {
            
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Comienza la conversion de EML a MSG iniciada...");
                MailMessage miMailMessage = MailMessage.Load(file, MailMessageFormat.Eml);
                MailPreview miMailPreview = new MailPreview(miMailMessage, true);
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "Conversion de EML a MSG exitosa...");

                return miMailPreview;
            
        }
    }

    /// <summary>
    /// Clase que renderiza un msg a variables.
    /// </summary>
    public class MailPreview
    {
        public string body { get; set; }
        public string date { get; set; }
        public string subject { get; set; }
        public string from { get; set; }
        public bool isMsg { get; set; } = true;
        public List<string> to { get; set; } = new List<string>();
        public List<string> cc { get; set; } = new List<string>();
        public List<string> cco { get; set; } = new List<string>();
        public List<AttachmentDTO> attachs { get; set; } = new List<AttachmentDTO>();

        public MailPreview() { }

        public MailPreview(MailMessage msg, bool includeAttachs) : this()
        {
            //Hacer split por ; para obtener el array o hacer una validacion para ver si tiene ;s.

            if (msg.To.Count == 1)
            {
                to.AddRange(msg.To[0].DisplayName.Split(';'));
            }
            else if (msg.To.Count > 1)
            {
                foreach (MailAddress item in msg.To)
                {
                    to.Add(item.Address);
                }
            }

            if (msg.Cc.Count == 1)
            {
                cc.AddRange(msg.Cc[0].DisplayName.Split(';'));
            }
            else if (msg.Cc.Count > 1)
            {
                foreach (MailAddress item in msg.Cc)
                {
                    cc.Add(item.Address);
                }
            }


            if (msg.Bcc.Count == 1)
            {
                cco.AddRange(msg.Bcc[0].DisplayName.Split(';'));
            }
            else if (msg.Bcc.Count > 1)
            {
                foreach (MailAddress item in msg.Bcc)
                {
                    cco.Add(item.Address);
                }
            }


            if (includeAttachs)
            {
                AttachmentDTO MiDTO;
                foreach (Attachment item in msg.Attachments)
                {
                    MiDTO = new AttachmentDTO(item);

                    attachs.Add(MiDTO);
                }
            }


            from = msg.From.Address;
            subject = msg.Subject;
            date = msg.Date.ToString();
            body = msg.BodyHtml;
        }
    }
}

public class AttachmentDTO
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public string Size { get; set; }
    public string MediaType { get; set; }

    public string Data { get; set; }

    public AttachmentDTO()
    {

    }

    public AttachmentDTO(Attachment att) : this()
    {
        this.Id = att.ContentId;
        this.FileName = att.ContentType.Name;
        this.Size = att.Data.Length.ToString();
        this.MediaType = att.ContentType.MediaType;
        this.Data = System.Convert.ToBase64String(((MemoryStream)att.Data).ToArray());
    }
}

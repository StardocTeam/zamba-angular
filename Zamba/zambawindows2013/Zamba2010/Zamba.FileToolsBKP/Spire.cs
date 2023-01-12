using Spire.DataExport.TXT;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Pdf;
using Spire.Xls;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using Zamba.AppBlock;

namespace Zamba.FileTools
{
    public class SpireTools
    {
        /// <summary>
        /// Exporta un datatable a Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="path"></param>
        public void ExportToXLS(DataTable dt, String path)
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
        public void InsertTableInWord(String wordPath, DataTable data, Int32 section)
        {
            //Create word document
            Document document = null;

            try
            {
                document = new Document();

                //load a document
                document.LoadFromFile(wordPath);

                //Replace text
                addTable(document.Sections[section], data);

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
        private void addTable(Section section, DataTable data)
        {
            Spire.Doc.Table table = section.AddTable();
            table.ResetCells(data.Rows.Count + 1, data.Columns.Count);

            // ***************** First Row *************************
            TableRow row = table.Rows[0];
            row.IsHeader = true;
            row.Height = 20;    //unit: point, 1point = 0.3528 mm
            row.HeightType = TableRowHeightType.Exactly;
            row.RowFormat.BackColor = Color.Gray;
            for (int i = 0; i < data.Columns.Count; i++)
            {
                row.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                Paragraph p = row.Cells[i].AddParagraph();
                p.Format.HorizontalAlignment = Spire.Doc.Documents.HorizontalAlignment.Center;
                TextRange txtRange = p.AppendText(data.Columns[i].ColumnName);
                txtRange.CharacterFormat.Bold = true;
            }

            for (int r = 0; r < data.Rows.Count; r++)
            {
                TableRow dataRow = table.Rows[r + 1];
                dataRow.Height = 20;
                dataRow.HeightType = TableRowHeightType.Exactly;
                dataRow.RowFormat.BackColor = Color.Empty;
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    dataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    dataRow.Cells[c].AddParagraph().AppendText(data.Rows[r][c].ToString());
                }
            }
        }

        /// <summary>
        /// Inserta una cadena de caracteres en un word
        /// </summary>
        /// <param name="wordPath"></param>
        /// <param name="data"></param>
        public void InsertTextInWord(String wordPath, String data, Int32 section)
        {
            //Create word document
            Document document = null;

            try
            {
                document = new Document();

                //load a document
                document.LoadFromFile(wordPath);

                //Replace text
                Paragraph p = document.Sections[section].AddParagraph();
                p.AppendText(data);

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

        public void CompleteTableInWord(string wordpath, Int32 pageindex, Int32 tableindex, bool withheader, DataTable dt, bool intable, Int32 rownindex)
        {
            Document doc = null;

            try
            {
                doc = new Document(wordpath);
                Section section = doc.Sections[pageindex];

                ITable table;
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

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        newRow.Cells[i].AddParagraph().AppendText(dr[i].ToString());
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
                    if (ex.Message.Contains("Zip exception.Can't locate end of central directory record")) {

                        ZException.Log(new Exception("Error al obtener el documento : " + wordPath + " " + ex.ToString()));
                        
                    }
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
                Zamba.Core.ZTrace.WriteLineIf(Zamba.Core.ZTrace.IsError,e.Message);
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

        // [JUDN 11-11-2014]:Guarda un Excel dentro de un Dataset. Guardando cada hoja en un datatable del dataset.
        public DataSet GetExcelAsDataSet(String xlsPath)
        {
            Workbook workbook = null;
            DataSet excelDs = null;
            try
            {
                workbook = new Workbook();

                if (xlsPath.Contains(".xlsx")) { workbook.LoadFromFile(xlsPath,ExcelVersion.Version2010); }
                else { workbook.LoadFromFile(xlsPath, ExcelVersion.Version97to2003); }

                excelDs = new DataSet();

                //Recorre el Excel y va agregando un datatable al dataset por cada hoja de la hoja de cálculo.
                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    DataTable newDt = new DataTable();

                    //Valido que la hoja no esté vacia.
                    if (workbook.Worksheets[i].IsEmpty != true)
                    {
                        newDt = workbook.Worksheets[i].ExportDataTable(1, 1, workbook.Worksheets[i].Rows.Length, workbook.Worksheets[i].Columns.Length, false);
                        if (newDt != null) { excelDs.Tables.Add(newDt); }
                    }
                }
                return excelDs;
            }
            finally
            {
                if (workbook != null)
                {
                    workbook.Dispose();
                    workbook = null;
                }
                if (excelDs != null)
                {
                    excelDs.Dispose();
                    excelDs = null;
                }
            }
        }
    }
}

using System.Collections;
using System.Data;
using System.Drawing;

namespace Zamba.FileTools
{
    public interface ISpireTools
    {
        void CompleteTableInWord(string wordpath, int pageindex, int tableindex, bool withheader, DataTable dt, bool intable, int rownindex, bool fontConfig = false, Font font = null, Color color = default(Color), Color backColor = default(Color));
        void ConvertExcelToPDF(string wordPath, string PDFPath);
        void ConvertImageToPDF(string wordPath, string PDFPath);
        void ConvertPowerPointToPDF(string wordPath, string PDFPath);
        void ConvertTIFFToPDF(string ImageFilename, string PDFPath);
        void ConvertWordToPDF(string wordPath, string PDFPath);
        void ExportToCSV(DataTable dt, string path);
        void ExportToXLS(DataTable dt, string path);
        Image GenerateBarcodeImage(int barcodeId, short barCodeType);
        string GenerateBarcodeImage(string templatePath, long barcodeId);
        DataTable GetExcelAsDataSet(string file);
        string GetText(string wordPath);
        string GetTextFromDoc(string wordPath);
        string GetTextFromExcel(string excelPath);
        string GetTextFromPDF(string pdfPath);
        void InsertTableInWord(string wordPath, DataTable data, int section, bool fontConfig = false, Font font = null, Color color = default(Color), Color backColor = default(Color));
        void InsertTextInWord(string wordPath, string data, int section, bool textAsTable = false, bool fontConfig = false, Font font = null, Color color = default(Color), Color BackColor = default(Color));
        void ReplaceInWord(string wordPath, Hashtable hash1, Hashtable hash2);
        void ReplaceInWord(string wordPath, string matchString, string newValue, bool caseSensitive, bool wholeWord);
    }
}
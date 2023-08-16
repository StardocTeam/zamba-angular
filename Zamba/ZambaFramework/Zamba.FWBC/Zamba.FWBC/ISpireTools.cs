
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using Zamba.Core;

namespace Zamba.FileTools
{
    public interface ISpireTools
    {
        void CompleteTableInWord(string wordpath, int pageindex, int tableindex, bool withheader, DataTable dt, bool intable, int rownindex, bool fontConfig = false, Font font = null, Color color = default(Color), Color backColor = default(Color));
        bool ConvertExcelToPDF(string wordPath, string PDFPath);
        bool ConvertImageToPDF(string wordPath, string PDFPath);
        bool ConvertPowerPointToPDF(string wordPath, string PDFPath);
        bool ConvertTIFFToPDF(string ImageFilename, string PDFPath);
        bool ConvertWordToPDF(string wordPath, string PDFPath);
        bool ExportToCSV(DataTable dt, string path);
        bool ExportToXLS(DataTable dt, string path);
        bool ExportToXLSx(DataTable dt, string path);
        Image GenerateBarcodeImage(int barcodeId, short barCodeType);
        string GenerateBarcodeImage(string templatePath, long barcodeId);
        DataTable GetExcelAsDataSet(string file);
        DataTable GetExcelAsDataSet(string file, string sheetName);
        string GetText(string wordPath);
        string GetTextFromDoc(string wordPath);
        string GetTextFromExcel(string excelPath);
        string GetTextFromPDF(string pdfPath);
        bool InsertTableInWord(string wordPath, DataTable data, int section, bool fontConfig = false, Font font = null, Color color = default(Color), Color backColor = default(Color));
        void InsertTextInWord(string wordPath, string data, int section, bool textAsTable = false, bool fontConfig = false, Font font = null, Color color = default(Color), Color BackColor = default(Color));
        void ReplaceInWord(string wordPath, Hashtable hash1, Hashtable hash2);
        void ReplaceInWord(string wordPath, string matchString, string newValue, bool caseSensitive, bool wholeWord);

        void PrintHtmlDocByHtmlString(string htmlString, short copies, string tempFile, float Width, float Height);

        // Funcionalidad mails
        Dictionary<string, Stream> GetEmailAttachs(string msgFile);
        long GetEmailAttachsCount(string msgFile);
        List<string> GetEmailAttachsNames(string msgFile);



    }


    public interface ISpireEmailTools
    {
      
         string ReadInBox();
        void InsertEmailsInZamba(List<IDTOObjectImap> imapProcessList, object ResultBusiness);

        void ConnectToExchange(Dictionary<string, string> Params);

        List<IListEmail> GetEMailsFromServer(Dictionary<string, string> Dic_paramRequest);




    }


public interface IDTOObjectImap
    {
        void Dispose();

        Int64 Id_proceso { get; set; }
        Int64 Is_Active { get; set; }
        string Nombre_usuario { get; set; }
        string Nombre_proceso { get; set; }
        string Correo_electronico { get; set; }
        Int64 Id_usuario { get; set; }
        string Password { get; set; }
        string Direccion_servidor { get; set; }
        int Puerto { get; set; }
        string Protocolo { get; set; }
        Int64 Filtrado { get; set; }
        string Filtro_campo { get; set; }
        string Filtro_valor { get; set; }
        Int64 Filtro_recientes { get; set; }
        Int64 Filtro_noleidos { get; set; }
        Int64 Exportar_adjunto_por_separado { get; set; }
        string Carpeta { get; set; }
        string CarpetaDest { get; set; }
        Int64 Entidad { get; set; }
        Int64 Enviado_por { get; set; }
        Int64 Para { get; set; }
        Int64 Cc { get; set; }
        Int64 Cco { get; set; }
        Int64 Asunto { get; set; }
        Int64 Body { get; set; }
        Int64 Fecha { get; set; }
        Int64 Usuario_zamba { get; set; }
        Int64 Codigo_mail { get; set; }
        Int64 Tipo_exportacion { get; set; }
        Int64 Autoincremento { get; set; }
        Int64 GenericInbox { get; set; }
    }


}
<%@ WebHandler Language="C#" Class="GetDocFile" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using Zamba.Core;
using Zamba.Services;

public class GetDocFile : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        SZOptBusiness Zopt;
        SResult sResult;
        
        try
        {
            Zopt = new SZOptBusiness();
            long DocTypeId = 0;
            long DocId = 0;
            Int64 UserID = 0;

            if (context.Request.QueryString["DocTypeId"].ToString() != null && context.Request.QueryString["DocId"].ToString() != null && context.Request.QueryString["UserID"].ToString() != null)
            {
                DocTypeId = long.Parse(context.Request.QueryString["DocTypeId"].ToString());
                DocId = long.Parse(context.Request.QueryString["DocId"].ToString());
                UserID = Int64.Parse(context.Request.QueryString["UserID"].ToString());
            }
            else
                throw new ArgumentException("No parameter specified");

            sResult = new SResult();
            Result res = (Result)sResult.GetResult(DocId, DocTypeId);
            bool fileNotFound = false;

            if (res != null)
            {
                byte[] file;

                //Verifica que el volumen sea de tipo blob o que se encuentre forzada la opción de volumenes del mismo tipo
                if (IsDigitalVolume(res, Zopt))
                    sResult.LoadFileFromDB(res);

                //Verifica si el result contiene el documento guardado
                if (res.EncodedFile != null)
                {
                    file = res.EncodedFile;
                }
                else
                {
                    string sUseWebService = Zopt.GetValue("UseWebService");
                    //Verifica si debe utilizar el webservice para obtener el documento
                    if (!String.IsNullOrEmpty(sUseWebService) && bool.Parse(sUseWebService))
                        file = sResult.GetWebDocFileWS(res.DocTypeId, res.ID, UserID);
                    else
                        file = sResult.GetResultFile(res);
                }

                //Verifica la existencia del documento buscado 
                if (!(file != null && file.Length > 0))
                {
                    FileStream fs = File.Open(context.Server.MapPath("FileNotFound.htm"), FileMode.Open, FileAccess.Read);
                    file = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(file, 0, file.Length);
                    fileNotFound = true;
                    fs.Close();
                    fs = null;
                }

                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.BufferOutput = true;

                string filePath;
                FileInfo FI;
                string contectDisposition;
                string contectType;

                if (fileNotFound)
                {
                    filePath = context.Server.MapPath("FileNotFound.htm");
                    contectDisposition = "inline; filename=FileNotFound.htm";
                    contectType = "text/html";
                }
                else
                {
                    filePath = res.OriginalName.Trim();
                    if (string.IsNullOrEmpty(filePath))
                        filePath = "file" + Path.GetExtension(res.FullPath);
                    
                    FI = new FileInfo(filePath);
                    contectDisposition = (showFileInline(FI.Extension) ? "inline" : "attachment") + "; filename=" + DocId.ToString() + FI.Extension;
                    contectType = res.MimeType;
                    FI = null;
                }
                
                context.Response.AppendHeader("Content-Length", file.Length.ToString());
                context.Response.AppendHeader("Content-Disposition", contectDisposition);
                context.Response.ContentType = contectType;
                context.Response.HeaderEncoding = System.Text.Encoding.UTF8;
                context.Response.BinaryWrite(file);
                context.Response.Flush();             
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            LoadFileNotFound(context);
        }
        finally
        {
            Zopt = null;
            sResult = null;
        }
    }

    private bool IsDigitalVolume(Result res, SZOptBusiness Zopt)
    {
        return res.Disk_Group_Id > 0 &&
                (VolumesBusiness.GetVolumeType(res.Disk_Group_Id) == (int)VolumeType.DataBase ||
                (!String.IsNullOrEmpty(Zopt.GetValue("ForceBlob")) && bool.Parse(Zopt.GetValue("ForceBlob"))));
    }

    private bool showFileInline(string fileExtension)
    {
        //Recupera la lista de formatos de archivo que deben mostrarse en el explorador, cualquier formato que no figure en esa lista se descarga en lugar de mostrarse
        ZOptBusiness zoptb = new ZOptBusiness();
        string fileTypes = zoptb.GetValue("ShowInlineFileTypes");
        zoptb = null;
        List<string> fileTypesList = null;
        
        if (fileTypes == null)
        {
            fileTypesList = new List<string>(new string[] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".htm", ".html", ".rtf", ".txt", ".msg", ".png", ".bmp", ".jpg", ".jpeg", ".gif", ".xps"  }); 
        }
        else
        {
            fileTypesList = new List<string>(fileTypes.Split(new char[]{','}));   
        }

        return fileTypesList.Contains(fileExtension.ToLower());
    }
    
    //Esta función es usada para mostrar un mensaje de error en el iframe sin posibilidad de error(es la última instancia).
    //Si es necesario cargar un mensaje más descriptivo.
    private void LoadFileNotFound(HttpContext context)
    {
        try
        {
            context.Response.Write("Ha ocurrido un error al intentar obtener el documento.");
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
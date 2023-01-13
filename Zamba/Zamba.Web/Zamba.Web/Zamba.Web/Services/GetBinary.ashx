<%@ WebHandler Language="C#" Class="GetBinary" %>

using System;
using System.Web;
using System.IO;
using Zamba.Core;

public class GetBinary : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        try
        {
            if (context.Request.QueryString["id"] != null)
            {
                bool fileNotFound = false;
                string mimeType;
                byte[] file;
                string id;

                //Se obtiene el mime y el binario
                id = context.Request.QueryString["id"];
                mimeType = context.Session["BinaryMime" + id].ToString();
                file = (byte[])context.Session["BinaryFile" + id];

                //Verifica la existencia del documento buscado 
                if (!(file != null && file.Length > 0))
                {
                    FileStream fs = File.Open(context.Server.MapPath("FileNotFound.htm"), FileMode.Open, FileAccess.Read);
                    Stream strm = fs;

                    file = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(file, 0, file.Length);
                    fileNotFound = true;
                    fs.Close();
                }

                //Muestra el binario
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.BufferOutput = true;
                FileInfo FI = new FileInfo((fileNotFound) ? context.Server.MapPath("FileNotFound.htm") : id);
                context.Response.AppendHeader("content-disposition", "inline; filename=" + FI.Name);
                context.Response.ContentType = (fileNotFound) ? "text/html" : mimeType;
                context.Response.OutputStream.Write(file, 0, file.Length);
                context.Response.OutputStream.Flush();
            }
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
            LoadFileNotFound(context);
        }
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
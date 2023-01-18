<%@ WebHandler Language="C#" Class="GetAttachBlobForum" %>

using System;
using System.Web;
using Zamba.Core;
using System.IO;
using Zamba.Services;
using System.Data;

public class GetAttachBlobForum : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        try
        {
            int MessageID;
            string FileName;
            long UserID;
            System.Collections.Specialized.NameValueCollection currentQueryString = context.Request.QueryString;

            if (int.TryParse(currentQueryString["MessageID"], out MessageID) && long.TryParse(currentQueryString["UserID"], out UserID) 
                && !string.IsNullOrEmpty(currentQueryString["FileName"]))
            {
                FileName = currentQueryString["FileName"];

                bool fileNotFound = false;

                byte[] file = null;
                SForum sForum = new SForum();
                DataTable tempTable = sForum.GetAttachFileByFileName(MessageID, FileName);

                if (tempTable.Rows.Count > 0)
                {
                    file = (byte[])tempTable.Rows[0][0];
                }

                //Si el archivo no se obtuvo, solo si se puede usar web service lo busca por fuera
                if (file == null || file.Length == 0)
                {
                    SZOptBusiness zoptb =  new SZOptBusiness();
                    string sValue = zoptb.GetValue("UseWebService");
                    if (!string.IsNullOrEmpty(sValue) && bool.Parse(sValue))
                        file = sForum.GetAttachFileByMessageIdAndNameWS(MessageID, FileName, UserID);
                    else
                    {
                        string serverPathForAttachs = Path.Combine(zoptb.GetValue("ServAdjuntosRuta"), MessageID.ToString());
                        DirectoryInfo dInfo = new DirectoryInfo(serverPathForAttachs);
                        if (!string.IsNullOrEmpty(serverPathForAttachs) && dInfo.Exists)
                        {
                            file = FileEncode.Encode(Path.Combine(serverPathForAttachs, FileName));
                        }
                    }
                }

                if (file == null || file.Length == 0)
                {
                    FileStream fs = File.Open(context.Server.MapPath("FileNotFound.htm"), FileMode.Open, FileAccess.Read);
                    Stream strm = fs;

                    file = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(file, 0, file.Length);
                    fileNotFound = true;
                    fs.Close();
                }

                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.BufferOutput = true;
                FileInfo FI = new FileInfo((fileNotFound) ? context.Server.MapPath("FileNotFound.htm") : context.Server.MapPath("~/temp/") + FileName);
                context.Response.AppendHeader("content-disposition", "inline; filename=" + FI.Name);
                context.Response.ContentType = (fileNotFound) ? "application/html" : "application/unknown";
                context.Response.OutputStream.Write(file, 0, file.Length);
                context.Response.OutputStream.Flush();
            }
            else
                throw new ArgumentException("No parameter specified");
        }
        catch (Exception ex)
        {
            Zamba.AppBlock.ZException.Log(ex);
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
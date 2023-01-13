<%@ WebHandler Language="C#" Class="GetMessageFile" %>

using System;
using System.Web;
using Zamba.Core;
using System.IO;
using Zamba.Services;
using System.Data;
using System.Text;

public class GetMessageFile : IHttpHandler {

    public void ProcessRequest (HttpContext context) {
        try
        {
            string url, ext;
            Int64 UserID;
            System.Collections.Specialized.NameValueCollection currentQueryString = context.Request.QueryString;

            if (!string.IsNullOrEmpty(currentQueryString["url"]) && Int64.TryParse(currentQueryString["UserID"], out UserID))
            {
                url = currentQueryString["url"];
                bool fileNotFound = false;
                byte[] file = null;

                SMail sMail = new SMail();

                //file = sMail.GetMailWS(url, Int64.Parse(currentQueryString["UserID"]));

                ext = Encoding.UTF8.GetString(Convert.FromBase64String(url));

                if (!(file != null && file.Length > 0))
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
                FileInfo FI = new FileInfo((fileNotFound) ? context.Server.MapPath("FileNotFound.htm") : context.Server.MapPath("~/temp/") + Path.GetFileName(ext));
                context.Response.AppendHeader("content-disposition", "inline; filename=" + Path.GetFileName(ext));
                context.Response.ContentType = GetMIME(Path.GetExtension(ext));
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

    public string GetMIME(string ext)
    {
        if(ext == ".msg")
            return "application/vnd.ms-outlook";
        else if(ext == ".htm" || ext == ".html")
            return "application/html";
        else
            return "application/unknown";
    }

}
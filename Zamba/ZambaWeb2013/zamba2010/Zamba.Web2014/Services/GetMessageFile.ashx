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
            string ext;
            Int64 userId, mailId;
            System.Collections.Specialized.NameValueCollection currentQueryString = context.Request.QueryString;

            if (Int64.TryParse(currentQueryString["id"], out mailId) && 
                Int64.TryParse(currentQueryString["user"], out userId))
            {
                bool fileNotFound = false;
                byte[] file = null;

                SMail sMail = new SMail();
                file = sMail.GetMailWS(mailId, userId);
                sMail = null;
                ext = EmailBusiness.GetMailExtension(mailId);

                if (file == null || file.Length == 0)
                {
                    FileStream fs = File.Open(context.Server.MapPath("FileNotFound.htm"), FileMode.Open, FileAccess.Read);
                    file = new byte[int.Parse(fs.Length.ToString())];
                    fs.Read(file, 0, file.Length);
                    fileNotFound = true;
                    fs.Close();
                    fs = null;
                }

                string fileName = mailId.ToString() + ext;
                context.Response.ClearContent();
                context.Response.ClearHeaders();
                context.Response.BufferOutput = true;
                context.Response.AppendHeader("content-disposition", "inline; filename=" + fileName);
                context.Response.AppendHeader("content-length", file.Length.ToString());
                context.Response.ContentType = (fileNotFound) ? "text/html" : GetMIME(ext);
                context.Response.BinaryWrite(file);
                context.Response.Flush();
                context.Response.Close();
                context.Response.End();
                file = null;
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
<%@ WebHandler Language="C#" Class="SessionAliveHandler" %>

using System;
using System.Web;
using System.Web.SessionState;

public class SessionAliveHandler : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Session["SessionRefreshToken"] = DateTime.Now;
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
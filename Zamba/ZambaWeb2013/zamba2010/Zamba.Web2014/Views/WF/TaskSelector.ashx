<%@ WebHandler Language="C#" Class="TaskSelector" %>

using System;
using System.Web;
using Zamba.Core;
using Zamba.Services;
using System.Web.Security;

public class TaskSelector : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Session == null || context.Session["User"] == null)
        {
            FormsAuthentication.RedirectToLoginPage();
            return;
        }
        
        String path = string.Empty;
        try
        {
            String docid = context.Request.QueryString["DocId"];
            String doctypeid = context.Request.QueryString["DocTypeId"];
            if (String.IsNullOrEmpty(doctypeid))
            {
                doctypeid = context.Request.QueryString["DocType"];    
            }
            
            if (!String.IsNullOrEmpty(docid))
            {
                STasks sTasks = new STasks();

                String news = context.Request.QueryString["news"];
               
                if (!String.IsNullOrEmpty(news))
                    NewsBusiness.SetRead(Int64.Parse(doctypeid), Int64.Parse(docid));

                String taskid = context.Request.QueryString["TaskId"];
                SRights sRights = new SRights();
                
                if (!string.IsNullOrEmpty(taskid))
                {
                    String stepid = context.Request.QueryString["WFStepID"];
                    
                    if(string.IsNullOrEmpty(stepid))
                        stepid = sTasks.GetStepId(Int64.Parse(taskid)).ToString();

                    if (sRights.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, Int64.Parse(stepid)))
                        path = "~/Views/WF/TaskViewer.aspx?taskid=" + taskid + "&docid=" + docid;
                    else
                        path = "~/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid;
                }
                else
                {
                    ITaskResult task = string.IsNullOrEmpty(doctypeid) ? sTasks.GetTaskByDocId(Int64.Parse(docid)) : sTasks.GetTaskByDocIdAndDocTypeId(Int64.Parse(docid), Int64.Parse(doctypeid));

                    if (task != null)
                    {
                        if (sRights.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, task.StepId))
                            path = "~/Views/WF/TaskViewer.aspx?taskid=" + task.TaskId + "&docid=" + docid;
                        else
                            path = "~/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid;
                    }
                    else
                    {
                        NewsBusiness.SetRead(Int64.Parse(doctypeid), Int64.Parse(docid));
                        path = "~/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid;
                    }
                }
            }
        }
        catch (Exception ex)
        {
          ZCore.raiseerror(ex);
        }
        if (string.IsNullOrEmpty(path)) return;
        
        context.Response.ContentType = "text/plain";
        context.Response.Redirect(path,false);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}

<%@ WebHandler Language="C#" Class="TaskSelector" %>

using System;
using System.Web;
using Zamba.Core;
using Zamba.Services;
using System.Web.Security;
using System.Web.UI;
using Zamba;

public class TaskSelector : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        if (context.Session == null || context.Session["User"] == null)
        {
            IUser User = null;
            String userId = string.Empty;

            if (context.Request.QueryString["userId"] != null)
            {
                UserBusiness UB = new UserBusiness();
                userId = context.Request.QueryString["userId"];
                if (userId.EndsWith(";")) userId = userId.Replace(";", "");
                if (userId.EndsWith("'")) userId = userId.Replace("'", "");

                User = UB.ValidateLogIn(Int64.Parse(userId), ClientType.Web);
                UB = null;
            }

            if (User == null && String.IsNullOrEmpty(userId))
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
        }

        String path = string.Empty;
        try
        {
            String doctypeid = context.Request.QueryString["DocTypeId"];
            if (String.IsNullOrEmpty(doctypeid))
            {
                doctypeid = context.Request.QueryString["DocType"];
            }
            if (String.IsNullOrEmpty(doctypeid))
            {
                doctypeid = context.Request.QueryString["DT"];
            }
            if (String.IsNullOrEmpty(doctypeid))
            {
                doctypeid = context.Request.QueryString["E"];
            }

            String docid = context.Request.QueryString["DocId"];
            if (String.IsNullOrEmpty(docid))
            {
                docid = context.Request.QueryString["ID"];
            }
            if (String.IsNullOrEmpty(docid))
            {
                docid = context.Request.QueryString["doc_id"];
            }

            if (!String.IsNullOrEmpty(docid))
            {
                STasks sTasks = new STasks();
                NewsBusiness NB = new NewsBusiness();
                String news = context.Request.QueryString["news"];

                if (!String.IsNullOrEmpty(news))
                    NB.SetRead(Int64.Parse(doctypeid), Int64.Parse(docid));

                String taskid = context.Request.QueryString["TaskId"];
                SRights sRights = new SRights();

                String mode = context.Request.QueryString["mode"];

                RightsBusiness RB = new RightsBusiness();
                String token = new Zamba.Core.ZssFactory().GetZss(Zamba.Membership.MembershipHelper.CurrentUser).TokenQueryString;
                if (!string.IsNullOrEmpty(taskid) && taskid != "0" && taskid != "undefined")
                {
                    String stepid = context.Request.QueryString["WFStepID"];

                    if (string.IsNullOrEmpty(stepid) || stepid == "0" || stepid == "undefined")
                        stepid = sTasks.GetStepId(Int64.Parse(taskid)).ToString();

                    if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, Int64.Parse(stepid)))
                        //path = "";
                        path = "/Views/WF/TaskViewer.aspx?taskid=" + taskid + "&docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&token=" + token;
                    else
                        path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&token=" + token;

                }
                else
                {
                    ITaskResult task = string.IsNullOrEmpty(doctypeid) ? sTasks.GetTaskByDocId(Int64.Parse(docid)) : sTasks.GetTaskByDocIdAndDocTypeId(Int64.Parse(docid), Int64.Parse(doctypeid));

                    if (task != null)
                    {
                        if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, task.StepId))
                            path = "/Views/WF/TaskViewer.aspx?taskid=" + task.TaskId + "&docid=" + docid  + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&token=" + token;
                        else
                            path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&token=" + token;
                    }
                    else
                    {

                        NB.SetRead(Int64.Parse(doctypeid), Int64.Parse(docid));
                        path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&token=" + token;
                        //path = "";
                    }
                }
                RB = null;
            }
        }
        catch (Exception ex)
        {
            ZCore.raiseerror(ex);
        }
        if (string.IsNullOrEmpty(path)) return;

        path = GetProtocol(context.Request) + context.Request.Headers.GetValues("HOST")[0] + context.Request.ApplicationPath + path;
        context.Response.ContentType = "text/plain";
        context.Response.Redirect(path, false);
    }


    public string GetProtocol(HttpRequest request)
    {
        string protocol = String.Empty;
        switch (request.Url.Scheme)
        {
            case "http":
                protocol = "http://";
                break;
            case "https":
                protocol = "https://";
                break;
            default:
                break;
        }
        return protocol;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}

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
            String token = string.Empty;

            if (context.Request.QueryString["t"] != null)
            {
                UserBusiness UB = new UserBusiness();
                token = context.Request.QueryString["t"];
                var tokenBytes = System.Convert.FromBase64String(token);
                token = System.Text.Encoding.UTF8.GetString(tokenBytes);
                //if (token.EndsWith(";")) token = token.Replace(";", "");
                //if (token.EndsWith("'")) token = token.Replace("'", "");

                User = UB.ValidateLogIn(token, ClientType.Web);

            } else  if (context.Request.QueryString["userId"] != null)
            {
                UserBusiness UB = new UserBusiness();
                userId = context.Request.QueryString["userId"];
                if (userId.EndsWith(";")) userId = userId.Replace(";", "");
                if (userId.EndsWith("'")) userId = userId.Replace("'", "");

                User = UB.ValidateLogIn(Int64.Parse(userId), ClientType.Web);
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
            try
            {
                String newsid = context.Request.QueryString["NewsId"];
                if (!String.IsNullOrEmpty(newsid))
                {
                    NewsBusiness NB = new NewsBusiness();
                    NB.SetRead(Int64.Parse(newsid));
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
            }


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

                String taskid = context.Request.QueryString["TaskId"];
                SRights sRights = new SRights();

                String mode = context.Request.QueryString["mode"];
                String modalmode = context.Request.QueryString["modalmode"];


                String userToken = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.QueryString["t"]))
                    userToken = context.Request.QueryString["t"].ToString().Trim();

                String formId = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.QueryString["f"]))
                    formId = context.Request.QueryString["f"].ToString().Trim();

                String modalFormId = string.Empty;
                if (!string.IsNullOrEmpty(context.Request.QueryString["mf"]))
                    modalFormId = context.Request.QueryString["mf"].ToString().Trim();

                String stepid = context.Request.QueryString["WFStepID"];
                String breakmodal = context.Request.QueryString["breakmodal"];

                RightsBusiness RB = new RightsBusiness();
                if (!string.IsNullOrEmpty(taskid) && taskid != "0" && taskid != "undefined")
                {
                    if (string.IsNullOrEmpty(stepid) || stepid == "0" || stepid == "undefined")
                        stepid = sTasks.GetStepId(Int64.Parse(taskid)).ToString();

                    if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, Int64.Parse(stepid)))
                        path = "/Views/WF/TaskViewer.aspx?taskid=" + taskid + "&docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken + (breakmodal == string.Empty ? string.Empty : "&breakmodal=1");
                    else
                        path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken;

                }
                else
                {
                    ITaskResult task = string.IsNullOrEmpty(doctypeid) ? sTasks.GetTaskByDocId(Int64.Parse(docid)) : sTasks.GetTaskByDocIdAndDocTypeId(Int64.Parse(docid), Int64.Parse(doctypeid));

                    if (task != null)
                    {
                        if (RB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.WFSteps, RightsType.Use, task.StepId))
                            path = "/Views/WF/TaskViewer.aspx?doctypeid=" + task.DocTypeId.ToString() + "&taskid=" + task.TaskId + "&docid=" + docid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&UserID=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken;
                        else
                            path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken;
                    }
                    else
                    {

                        String Edit = context.Request.QueryString["Ed"];

                        if (Edit != null)
                        {
                            path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&Ed=" + Edit + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken;
                        }
                        else
                        {

                            path = "/Views/Search/DocViewer.aspx?docid=" + docid + "&doctype=" + doctypeid + (mode == string.Empty ? string.Empty : "&mode=" + mode) + (modalmode == string.Empty ? string.Empty : "&modalmode=" + modalmode) + "&U=" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "&m=" + formId + "&mf=" + modalFormId + "&t=" + userToken;

                        }



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

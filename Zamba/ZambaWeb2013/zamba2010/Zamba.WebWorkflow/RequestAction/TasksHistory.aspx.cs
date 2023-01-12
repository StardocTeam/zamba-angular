using System;
using Zamba.Core;
using System.Collections.Generic;
using System.Text;


public partial class RequestAction_TasksHistory : System.Web.UI.Page
{
    private const String QUERY_STRING_USER_ID = "UserId";
    private const String QUERY_STRING_REQUEST_ACTION_ID = "RequestActionId";

    private Int64? RequestActionId
    {
        get
        {
            Int64? Id = null;
            Int64 TryValue;

            if (Int64.TryParse(Request.QueryString[QUERY_STRING_REQUEST_ACTION_ID], out TryValue))
                Id = (Int64?)TryValue;

            return Id;
        }
    }

    private Int64? UserId
    {
        get
        {
            Int64? Id = null;
            Int64 TryValue;

            if (Int64.TryParse(Request.QueryString[QUERY_STRING_USER_ID], out TryValue))
                Id = (Int64?)TryValue;

            return Id;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (RequestActionId.HasValue)
            {
                RequestAction Request = RequestActionBusiness.GetRequestAction(RequestActionId.Value);
                UcTasks.Tasks = Request.ExecutedTasks;
                if (Request.ExecutedTasks.Count > 0)
                    UcTasks.Select(Request.ExecutedTasks[0].TaskId);
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void lnkViewRequest_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder LinkBuilder = new StringBuilder();
            LinkBuilder.Append("RequestAction.aspx?");
            LinkBuilder.Append(QUERY_STRING_REQUEST_ACTION_ID);
            LinkBuilder.Append("=");
            LinkBuilder.Append(RequestActionId.Value.ToString());
            LinkBuilder.Append("&");
            LinkBuilder.Append(QUERY_STRING_USER_ID);
            LinkBuilder.Append("=");
            LinkBuilder.Append(UserId.Value);

            Response.Redirect(LinkBuilder.ToString());
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void UcTasks_SelectedTaskChanged(Int64? taskId)
    {
        try
        {
            UcTaskInformation.TaskId = taskId;
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
}

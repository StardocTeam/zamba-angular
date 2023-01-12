using System;
using System.Web.UI;
using Zamba.Core;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Text;

public partial class RequestActionList
    : Page
{
    private const String QUERY_STRING_USER_ID = "UserId";
    private const String QUERY_STRING_REQUEST_ACTION_ID = "RequestActionId";
    private const String RESPONSE_PAGE = "RequestAction.aspx";
    private const Int32 ROW_INDEX_REQUESTED_BY = 1;
    private const Int32 ROW_INDEX_REQUEST_DATE = 2;
    private const Int32 ROW_INDEX_REQUEST_NAME = 3;
    private const String NO_REQUESTS_MESSAGE = "No hay pedidos asignados a usted.";

    private static StringBuilder _linkBuilder = null;

    /// <summary>
    /// Devuelve el Id de usuario logeado
    /// </summary>
    private Int64? UserId
    {
        get
        {
            Int64? Id = null;

            if (null != Session["UserId"])
            {
                Int64 TryValue;
                if (Int64.TryParse(Session["UserId"].ToString(), out TryValue))
                    Id = TryValue;
            }

            return Id;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!UserId.HasValue)
                FormsAuthentication.RedirectToLoginPage();

            LoadRequests();
            FullName();
        }
    }

    private void LoadRequests()
    {

        if (UserId.HasValue)
        {
            List<RequestAction> requests = RequestActionBusiness.GetRequestActionByUser(UserId.Value);

            if (requests != null && requests.Count > 0)
                LoadRequests(requests);
        }


        ValidateStatus();
    }

    private void ValidateStatus()
    {
        if (gvRequests.Rows.Count > 0)
        {
            fsTaskList.Visible = true;
            gvRequests.Visible = true;
            lbNoTasks.Text = String.Empty;
            lbNoTasks.Visible = false;
        }
        else
        {
            fsTaskList.Visible = false;
            gvRequests.Visible = false;
            lbNoTasks.Text = NO_REQUESTS_MESSAGE;
            lbNoTasks.Visible = true;
        }
    }

    private void LoadRequests(List<RequestAction> requests)
    {
        gvRequests.DataSource = null;

        gvRequests.DataSource = requests;
        gvRequests.DataBind();

        Int32 BaseIndex = gvRequests.PageSize * gvRequests.PageIndex;
        Int32 CurrentIndex;
        RequestAction CurrentTask = null;
        GridViewRow CurrentRow = null;
        LinkButton CurrentLink = null;

        for (int i = 0; i < gvRequests.PageSize; i++)
        {
            CurrentIndex = BaseIndex + i;
            if (CurrentIndex <= requests.Count && i < gvRequests.Rows.Count)
            {
                CurrentTask = requests[CurrentIndex];

                CurrentRow = gvRequests.Rows[i];
                CurrentLink = (LinkButton)CurrentRow.FindControl("lnkRequest");
                CurrentLink.PostBackUrl = BuildResponseLink(UserId.Value, CurrentTask.RequestActionId.Value);
                CurrentLink.Text = "Ver";

                CurrentRow.Cells[ROW_INDEX_REQUESTED_BY].Text = UserBusiness.GetUserNamebyId((Int32)CurrentTask.RequestUserId);
                CurrentRow.Cells[ROW_INDEX_REQUEST_DATE].Text = CurrentTask.RequestDate.ToString();
                CurrentRow.Cells[ROW_INDEX_REQUEST_NAME].Text = CurrentTask.Name.ToString();
            }
        }

        lblPendingTasks.Text = "Tareas pendientes: " + requests.Count.ToString ();
    }

    private static String BuildResponseLink(Int64 userId, Int64 requestActionId)
    {
        if (null == _linkBuilder)
            _linkBuilder = new StringBuilder();

        _linkBuilder.Remove(0, _linkBuilder.Length);

        _linkBuilder.Append(RESPONSE_PAGE);
        _linkBuilder.Append("?");
        _linkBuilder.Append(QUERY_STRING_REQUEST_ACTION_ID);
        _linkBuilder.Append("=");
        _linkBuilder.Append(requestActionId.ToString());
        _linkBuilder.Append("&");
        _linkBuilder.Append(QUERY_STRING_USER_ID);
        _linkBuilder.Append("=");
        _linkBuilder.Append(userId.ToString());

        return _linkBuilder.ToString();
    }
    protected void gvRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvRequests.PageIndex = e.NewPageIndex;
            LoadRequests();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            e.Cancel = true;
        }
    }

    protected void FullName()
    {
       IUser FullName = UserBusiness.GetUserById(Int64.Parse(Session["userid"].ToString()));
       lblFullName.Text  = "Bienvenido " + FullName.Nombres + " " + FullName.Apellidos;
    }
}

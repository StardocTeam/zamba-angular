using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using System.Web.Configuration;
using Zamba.AppBlock;
using Zamba.Membership;
using Zamba;
using System.Web.UI;
using ExtExtenders;
using System.Web.UI.WebControls;
using System.Web.Security;
using Zamba.Web;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Http;
using Zamba.Core.WF.WF;
using System.Configuration;

partial class Default : System.Web.UI.Page
{

    private ArrayList _hideColumns;

    private Int16 m_resultsPagingId;
    private Int16 m_pageSize;



    public Int16 ResultsPagingId
    {
        get
        {
            if (Session["ResultsPagingId"] == null)
            {
                m_resultsPagingId = 0;
            }
            else
            {
                m_resultsPagingId = Int16.Parse(Session["ResultsPagingId"].ToString());
            }
            return m_resultsPagingId;
        }
        set { m_resultsPagingId = value; }
    }

    public object Information { get; private set; }
    public object UpdatePanel2 { get; private set; }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
            Page.Theme = ZC.InitWebPage();
        }
        catch (Exception ex)
        {

            ZClass.raiseerror(ex);
        }


    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {

            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                OpenRecentTasks();
                LoadFeeds();
                LoadDynamicBtns();
                AddArbolHandler();
            }
            else
            {
                //Marcos-Al iniciar la Aplicacion te redirecciona al Login-No quitar//
                Response.Redirect("~/Views/Security/Login.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        //finally
        //{
        //    string Script = "$(document).ready(function(){ hideLoading();});";
        //    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseLoadingDialog", Script, true);
        //}
    }

    private void AddArbolHandler()
    {
        try
        {
            if (Zamba.Membership.MembershipHelper.CurrentUser != null)
            {
                Arbol.SelectedNodeChanged += SelectedNodeChanged;
                Arbol.WFTreeRefreshed += RefreshTaskGrid;
                Arbol.WFTreeIsEmpty -= WfTreeIsEmpty;
                Arbol.WFTreeIsEmpty += WfTreeIsEmpty;
                Arbol.FillWF();
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

    protected void OpenRecentTasks()
    {
        UserPreferences UP = new UserPreferences();
        WFTaskBusiness WFTB = new WFTaskBusiness();
        try
        {
            if (Convert.ToBoolean(UP.getValue("OpenRecentTask", UPSections.WorkFlow, false)))
            {
                String thisDomain = Zamba.Membership.MembershipHelper.AppUrl;
                if (!string.IsNullOrEmpty(thisDomain))
                {
                    DataTable lTasks = WFTB.GetUserOpenedTasks(Zamba.Membership.MembershipHelper.CurrentUser.ID);

                    if (lTasks.Rows.Count > 0)
                    {
                        string Script = "$(document).ready(function(){";
                        foreach (DataRow task in lTasks.Rows)
                        {

                            string url = thisDomain + "/views/WF/TaskSelector.ashx?taskid=" + task["Task_ID"] + "&docid=" + task["Doc_ID"] + "&DocTypeId=" + task["doc_type_id"].ToString() + "&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                            Script += "AddDocTaskToOpenList(" + task["Task_ID"] + ", " + task["Doc_ID"] + ", " + task["doc_type_id"].ToString() + ", false, '" + task["Name"] + "', '" + url + "', '" + Zamba.Membership.MembershipHelper.CurrentUser.ID + "');";
                        }
                        Script += "OpenPendingTabs(true);});";
                        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenTask", Script, true);
                    }

                    lTasks.Dispose();
                    lTasks = null;
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally { UP = null;
            WFTB = null;
        }
    }

    protected void LoadFeeds()
    {
        RightsBusiness RiB = new RightsBusiness();
        try
        {
            SZOptBusiness ZOptBusines = new SZOptBusiness();
            bool sFeedView = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Feeds, RightsType.View);
            viewsNews.Value = sFeedView.ToString();
            viewInsert.Value = Convert.ToString(RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.InsertWeb, RightsType.View));

            string PageTitle = ZOptBusines.GetValue("WebViewTitle");
            if (string.IsNullOrEmpty(PageTitle))
            {
                this.Title = "Zamba";
            }
            else
            {
                this.Title = PageTitle + " - Zamba";
            }
            //Solapa Novedades
            if (sFeedView)
            {
                //Obtiene permiso para ver los feeds
                sFeedView = RiB.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.Feeds, RightsType.View, -1);
                hdnFView.Value = sFeedView.ToString();

                if (sFeedView)
                {
                    //Obtiene variables de configuracion
                    string sFeedRefreshInterval = ZOptBusines.GetValue("FeedRefreshInterval");

                    if (string.IsNullOrEmpty(sFeedRefreshInterval))
                    {
                        hdnFRefresh.Value = "5000";
                    }
                    else
                    {
                        hdnFRefresh.Value = sFeedRefreshInterval;
                    }

                    string sFeedLinesCount = ZOptBusines.GetValue("FeedLinesCount");

                    if (string.IsNullOrEmpty(sFeedLinesCount))
                    {
                        hdnFLinesCount.Value = "6";
                    }
                    else
                    {
                        hdnFLinesCount.Value = sFeedLinesCount;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
        finally {
            RiB = null;
        }
    }

    protected void LoadDynamicBtns()
    {
        try
        {


            DynamicButtonController dynamicBtnController = new DynamicButtonController();
            DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHomeButtons(Zamba.Membership.MembershipHelper.CurrentUser);

            if (dynBtnView != null)
            {//se comento la linea de codigo ya que el buttons pertenecia a la vista modal del menu de inicio
                //pnlHomeButtons.Controls.Add(dynBtnView);

                this.lblUsuarioActual2.InnerText = Zamba.Membership.MembershipHelper.CurrentUser.Name;
                DynamicButtonPartialViewBase dynBtnHeaderView = dynamicBtnController.GetViewHeaderButtons(Zamba.Membership.MembershipHelper.CurrentUser);
                if (dynBtnHeaderView.RenderButtons.Count > 0)
                {
                    pnlHeaderButtons.Controls.Add(dynBtnHeaderView);
                    string Script = "$(document).ready(function(){ $('#dropdown-header').show();});";
                    Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseHeaderActions", Script, true);
                }
            }
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    protected void Page_PreRender(object sender, System.EventArgs e)
    {
        try
        {


            string weblink = MembershipHelper.AppUrl;

            if (!string.IsNullOrEmpty(weblink))
            {

                if (Page.Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Page.Request.QueryString["docid"]))
                {
                    string docid = Page.Request.QueryString["docid"];
                    STasks _STask = new STasks();
                    ITaskResult _task = _STask.GetTaskByDocId(Int64.Parse(docid));
                    _STask = null;


                    if (_task != null)
                    {
                        String thisDomain = Zamba.Membership.MembershipHelper.AppUrl;
                        string Script = "$(document).ready(function(){";
                        string url = thisDomain + "/views/WF/TaskSelector.ashx?taskid=" + _task.TaskId + "&docid=" + _task.ID + "&DocTypeId=" + _task.DocTypeId + "&userId=" + Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
                        Script += "AddDocTaskToOpenList(" + _task.TaskId + ", " + _task.ID + ", " + _task.DocTypeId + ", false, '" + _task.Name + "', '" + url + "', " + Zamba.Membership.MembershipHelper.CurrentUser.ID + ");";
                        Script += "OpenPendingTabs(false);});";
                        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenTask", Script, true);

                        _task = null;
                    }
                    else
                    {
                        string script = "$(document).ready(function(){toastr.error('El documento es inexistente o no tiene permiso para acceder al mismo');});";
                        Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ErrorMessage", script, true);

                    }

                }

            }

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }



    private void SelectedNodeChanged(Int32 WFId, Int32 StepId, Int32 DocTypeId)
    {
        TaskGrid.LoadTasks(WFId, StepId, DocTypeId, Arbol.WFTreeView.SelectedNode);
    }

    private void RefreshTaskGrid(Int32 StepId)
    {
        TaskGrid.RebindGrid();
    }

    private void WfTreeIsEmpty()
    {
        UpdTaskGrid.Visible = false;
        lblNoWFVisible.Visible = true;
    }

    protected Boolean IsNumeric(object DATA)
    {
        try
        {
            int a = int.Parse(DATA.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected void CleanCache(object sender, EventArgs e)
    {
        Zamba.Core.CacheBusiness.ClearCaches();
    }



    public Default()
    {
        PreRender += Page_PreRender;
        Load += Page_Load;
        PreInit += Page_Init;
    }

    private static string TokenExpires()
    {
        var date = HttpContext.Current.Session["TokenExpires"];
        return date == null ? "" : date.ToString();
    }


    [AllowAnonymous]
    [System.Web.Services.WebMethod]
    public static string GetBearerToken()
    {
        try
        {
            //var token = HttpContext.Current.Session["BearerToken"];
            //var tokenExpires = HttpContext.Current.Session["TokenExpires"];
            //if (token == null || DateTime.Parse(tokenExpires.ToString()) >= DateTime.Now)
            //{
                if (Zamba.Membership.MembershipHelper.CurrentUser != null)
                {
                    var _token = Zamba.Web.Helpers.UserToken.GetBearerToken(Zamba.Membership.MembershipHelper.CurrentUser.Name, Zamba.Membership.MembershipHelper.CurrentUser.Password, HttpContext.Current.Request.UserHostAddress.Replace("::1","127.0.0.1") , HttpContext.Current.Request.Url.Host + ConfigurationManager.AppSettings.GetValues("RestApiUrl")[0]);
                    if (_token == null || _token == string.Empty) return string.Empty;
                    var tI = new TokenInfo
                    {
                        token = _token,
                        tokenExpire = TokenExpires(),
                        userName = Zamba.Membership.MembershipHelper.CurrentUser.Name,
                    };
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    return serializer.Serialize(tI);
                }
                else
                    return string.Empty;
            //}
            //else
            //{
            //    return token.ToString();
            //}
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            return string.Empty;
        }
    }


}

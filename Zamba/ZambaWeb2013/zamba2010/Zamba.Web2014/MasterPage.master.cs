
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using Zamba.Core;
using Zamba.Services;


partial class MasterPage : System.Web.UI.MasterPage
{   
    public bool loadChatZmb { get; set; }
    public bool loadGlobalSearch { get; set; }
    protected void Page_Init(object sender, System.EventArgs e)
	{
		if ((Zamba.Servers.Server.ConInitialized == false)) {
			Zamba.Core.ZCore ZC = new Zamba.Core.ZCore();
			ZC.InitializeSystem("Zamba.Web");
		}

		Zamba.Core.UserPreferences.LoadAllMachineConfigValues();
		Zamba.Membership.MembershipHelper.OptionalAppTempPath = Zamba.Core.UserPreferences.getValue("AppTempPath", Zamba.Core.Sections.UserPreferences, string.Empty);

		ZOptBusiness zoptb = new ZOptBusiness();
		string CurrentTheme = zoptb.GetValue("CurrentTheme");
		zoptb = null;


		if (Session["UserId"] == null) {
			FormsAuthentication.RedirectToLoginPage();
			Response.End();
		}


		//icono del titulo
		lnkWebIcon.Attributes.Add("href", "~/App_Themes/" + CurrentTheme + "/Images/WebIcon.ico");
	}

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            Page.Header.DataBind();

            if (Session["User"] != null)
            {
                IUser user = default(IUser);
                user = (IUser)Session["User"];
                this.lblUsuarioActual2.InnerText = user.Name;

                if (!Page.IsPostBack)
                {
                    hdnUserId.Value = user.ID.ToString();
                    ZOptBusiness zopt = new ZOptBusiness();
                    string link = zopt.GetValue("WebHomeLink");
                    string target = zopt.GetValue("WebHomeTarget");
                    zopt = null;
                    SUserPreferences sUserPref = new SUserPreferences();
                    hdnRefreshWF.Value = sUserPref.getValue("WebRefreshWFTab", Sections.WorkFlow, false);

                    if (string.IsNullOrEmpty(link))
                    {
                        hdnLink.Value = HttpContext.Current.Request.Url.ToString();
                    }
                    else {
                        hdnLink.Value = link;
                    }

                    if (string.IsNullOrEmpty(target))
                    {
                        hdnTarget.Value = "_self";
                    }
                    else {
                        hdnTarget.Value = target;
                    }
                    //Me.UC_WFExecution.TaskID = Task_ID

                    Session["ListOfTask"] = null;

                    //Actualiza el timemout
                    SRights rights = new SRights();
                    Int32 type = 0;
                    if (user.WFLic)
                        type = 1;
                    if ((user.ConnectionId > 0))
                    {
                        SUserPreferences SUserPreferences = new SUserPreferences();
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
                        SUserPreferences = null;
                    }
                    else {
                        Response.Redirect("~/Views/Security/LogIn.aspx");
                    }
                    rights = null;


                }
            }

            DynamicButtonController dynamicBtnController = new DynamicButtonController();
            DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHeaderButtons(Zamba.Membership.MembershipHelper.CurrentUser);

            if (dynBtnView.RenderButtons.Count > 0)
            {
                pnlHeaderButtons.Controls.Add(dynBtnView);
                string Script = "$(document).ready(function(){ $('#dropdown-header').show();});";
                Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseHeaderActions", Script, true);
            }
           
            hdnUserId.Value = Session["UserId"].ToString();
            hdnConnectionId.Value = Session["ConnectionId"].ToString();
            hdnComputer.Value = Session["ComputerNameOrIP"].ToString();
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }

	protected void MasterLogout(object sender, System.EventArgs e)
	{
		Response.Redirect("~/Views/Security/Logout.aspx");
	}

	public static string GetAppRootUrl(bool endSlash)
	{
		string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
		string appRootUrl = HttpContext.Current.Request.ApplicationPath;
		if (!appRootUrl.EndsWith("/")) {
			//a virtual
			appRootUrl += "/";
		}
		if (!endSlash) {
			appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1);
		}
		return host + appRootUrl;
	}

	public static string GetShowSessionRefreshLog()
	{
		SZOptBusiness zopt = new SZOptBusiness();
		string val = zopt.GetValue("ShowSessionRefreshLog");
		if (string.IsNullOrEmpty(val)) {
			return "false";
		} else {
			return val.ToLower();
		}
	}

	public static string GetSessionTimeOut()
	{
		return HttpContext.Current.Session.Timeout.ToString();
	}

	public static IHtmlString RegisterThemeBundles()
	{

		return Tools.RegisterThemeBundles(HttpContext.Current.Request);
	}

	public static IHtmlString GetJqueryCoreScript()
	{
		return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
	}

	public static string GetIsOldBrowser()
	{
		return Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower();
	}
	public MasterPage()
	{
		Load += Page_Load;
		Init += Page_Init;
	}
}

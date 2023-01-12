
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using System.IO;
using System.Web;
using System.Web.Security;

partial class MasterBlankPage : System.Web.UI.MasterPage
{


	protected void Page_Init(object sender, System.EventArgs e)
	{
		if (Session["UserId"] == null) {
			if (Request.QueryString["UserId"]== null && Request.QueryString["Token"] == null) {
				FormsAuthentication.RedirectToLoginPage();
				Response.End();
			} else {
				int userid = int.Parse(Request.QueryString["UserId"]);
				IUser user = Zamba.Core.UserBusiness.Rights.ValidateLogIn(userid, Zamba.Core.ClientType.Web);
				Session["UserId"] = userid;
			}
		}
	}


	protected void Page_Load(object sender, System.EventArgs e)
	{
		hdnUserId.Value = Session["UserId"].ToString();
		if (Request.Url.ToString().ToLower().Contains("wf.aspx") || Request.Url.ToString().ToLower().Contains("search.aspx") || Request.Url.ToString().ToLower().Contains("results.aspx")) {
			this.EnableViewState = true;
		} else {
			this.EnableViewState = false;
		}

		System.Web.UI.ServiceReference wsReference = new System.Web.UI.ServiceReference("~/Services/IndexsService.asmx");
		ScriptManager1.Services.Add(wsReference);
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

	public static IHtmlString GetJqueryCoreScript()
	{
		return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
	}

	public static string GetIsOldBrowser()
	{
		return Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower();
	}
	public MasterBlankPage()
	{
		Load += Page_Load;
	}
}

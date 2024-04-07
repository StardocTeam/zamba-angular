
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using Zamba.Core;


partial class MasterPopUpPage : System.Web.UI.MasterPage
{

	protected void Page_Init(object sender, System.EventArgs e)
	{
		if (Session["UserId"] == null) {
			FormsAuthentication.RedirectToLoginPage();
			Response.End();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		try {
			if (string.IsNullOrEmpty(Page.Title)) {
				SubTitleLabel.Text = "Zamba - Web";
			} else {
				SubTitleLabel.Text = Page.Title;
			}


			if (Page.IsPostBack == false) {
				if ((Session["User"] != null)) {
					IUser user = default(IUser);
					user =(IUser) Session["User"];
					this.lblUsuarioActual.Text = user.Name;

					//Me.UC_WFExecution.TaskID = Task_ID
				}
			}
		} catch (Exception ex) {
			ZClass.raiseerror(ex);
		}
	}



	protected void MasterLogout(object sender, System.EventArgs e)
	{
		Response.Redirect("~/Views/Security/Logout.aspx");
	}


	public static IHtmlString GetJqueryCoreScript()
	{
		return Tools.GetJqueryCoreScript(HttpContext.Current.Request);
	}

	public static string GetIsOldBrowser()
	{
		return Tools.GetIsOldBrowser(HttpContext.Current.Request).ToString().ToLower();
	}
	public MasterPopUpPage()
	{
		Load += Page_Load;
		Init += Page_Init;
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
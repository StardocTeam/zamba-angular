
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

partial class Logout : System.Web.UI.Page
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		try {
			IUser User = (IUser)Session["User"];
			Ucm UcmObj = new Ucm();

			//Se remueven las conexiones y licencias
			if ((Session["ComputerNameOrIP"] != null) & (Session["ConnectionId"] != null)) {
				Zamba.Services.SRights SRights = new Zamba.Services.SRights();
				Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByConId(int.Parse(Session["ConnectionId"].ToString()));
				SRights.RemoveConnectionFromWeb(Int64.Parse(Session["ConnectionId"].ToString()));
				SRights = null;
			}

			Session.RemoveAll();
			Session.Abandon();

			FormsAuthentication.SignOut();
			Response.Redirect("~/Views/Security/LogIn.aspx", false);
			//FormsAuthentication.RedirectToLoginPage()

		} catch (Exception ex) {
			ZTrace.WriteLine(ex.ToString());
			Session.RemoveAll();
			Session.Abandon();
		} finally {
			ZTrace.RemoveCurrentInstance();
		}
	}
	public Logout()
	{
		Load += Page_Load;
	}
}

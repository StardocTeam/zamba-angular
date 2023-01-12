
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
using Zamba.Core.WF.WF;

partial class Logout : System.Web.UI.Page
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		try {
			IUser User = Zamba.Membership.MembershipHelper.CurrentUser;
			Ucm UcmObj = new Ucm();

			//Se remueven las conexiones y licencias
			if ((Session["ComputerNameOrIP"] != null) & (Session["ConnectionId"] != null)) {
                WFTaskBusiness WFTB = new WFTaskBusiness();

                Zamba.Services.SRights SRights = new Zamba.Services.SRights();
				WFTB.CloseOpenTasksByConId(int.Parse(Session["ConnectionId"].ToString()));
				SRights.RemoveConnectionFromWeb(Int64.Parse(Session["ConnectionId"].ToString()));
                //CacheBusiness.ClearCaches();
                SRights = null;
                WFTB = null;
			}

			Session.RemoveAll();
			Session.Abandon();

			FormsAuthentication.SignOut();
			Response.Redirect("~/Views/Security/LogIn.aspx", false);
			//FormsAuthentication.RedirectToLoginPage()

		} catch (Exception ex) {
			ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString());
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

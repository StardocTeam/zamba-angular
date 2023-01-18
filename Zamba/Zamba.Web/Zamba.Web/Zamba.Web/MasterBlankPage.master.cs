
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
using Zamba.Membership;

namespace Zamba.Web
{
	public partial class MasterBlankPage : System.Web.UI.MasterPage
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{

			//if (MembershipHelper.CurrentUser == null || !Response.IsClientConnected || (MembershipHelper.CurrentUser != null && Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined"  && MembershipHelper.CurrentUser.ID != long.Parse(Request.QueryString["userid"])))
			//{
			//	FormsAuthentication.RedirectToLoginPage();
			//	return;
			//}

			//if (Membership.MembershipHelper.CurrentUser == null && Request.QueryString.HasKeys() && Request.QueryString["userid"] != null && Request.QueryString["userid"] != "undefined")
			//{


			//	UserBusiness UB = new UserBusiness();
			//	UB.ValidateLogIn(long.Parse(Request.QueryString["userid"]), ClientType.Web);

			//	//FormsAuthentication.RedirectToLoginPage();
			//}

			//if (MembershipHelper.CurrentUser == null || !Response.IsClientConnected)
			//{
			//	FormsAuthentication.RedirectToLoginPage();
			//	return;
			//}


		}

		public static string GetAppRootUrl(bool endSlash)
		{
			string host = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
			string appRootUrl = HttpContext.Current.Request.ApplicationPath;
			if (!appRootUrl.EndsWith("/"))
			{
				//a virtual
				appRootUrl += "/";
			}
			if (!endSlash)
			{
				appRootUrl = appRootUrl.Substring(0, appRootUrl.Length - 1);
			}
			return host + appRootUrl;
		}

		
	}
}

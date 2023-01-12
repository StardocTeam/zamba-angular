
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba;
using Zamba.Core;
using Zamba.Membership;
using Zamba.Web;

partial class Views_UC_Home_UCHome : System.Web.UI.UserControl
{
    RightsBusiness RiB = new RightsBusiness();
    protected void Page_Load(object sender, System.EventArgs e)
	{

		if (MembershipHelper.CurrentUser  != null) {
			//valido permisos de creacion para el entidad industrias
			if (RiB.GetUserRights(MembershipHelper.CurrentUser.ID,ObjectTypes.DocTypes, RightsType.Create, 1027)) {
				btnAltaEntidad.Visible = true;
			} else {
				btnAltaEntidad.Visible = false;
			}

			//valido permisos de creacion para el entidad Reintegro
			if (RiB.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Create, 26028)) {
				btnAltaEntidad1.Visible = true;
			} else {
				btnAltaEntidad1.Visible = false;
			}

			//valido permisos de creacion para el entidad afiliado
			if (RiB.GetUserRights(MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.Create, 26042)) {
				btnAltaEntidad6.Visible = true;
			} else {
				btnAltaEntidad6.Visible = false;
			}

			//Instancio un controller 
			DynamicButtonController dynamicBtnController = new DynamicButtonController();
			//Pido la vista
			DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHomeButtons(MembershipHelper.CurrentUser);
			//La agrego
			pnl.Controls.Add(dynBtnView);
		}

	}
	public Views_UC_Home_UCHome()
	{
		Load += Page_Load;
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================

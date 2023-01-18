
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;

partial class Views_UC_Home_UCHome : System.Web.UI.UserControl
{

	protected void Page_Load(object sender, System.EventArgs e)
	{

		if (Session["UserId"] != null) {
			//valido permisos de creacion para el entidad industrias
			if (RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 1027)) {
				btnAltaEntidad.Visible = true;
			} else {
				btnAltaEntidad.Visible = false;
			}

			//valido permisos de creacion para el entidad Reintegro
			if (RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 26028)) {
				btnAltaEntidad1.Visible = true;
			} else {
				btnAltaEntidad1.Visible = false;
			}

			//valido permisos de creacion para el entidad afiliado
			if (RightsBusiness.GetUserRights(ObjectTypes.DocTypes, RightsType.Create, 26042)) {
				btnAltaEntidad6.Visible = true;
			} else {
				btnAltaEntidad6.Visible = false;
			}

			//Instancio un controller 
			DynamicButtonController dynamicBtnController = new DynamicButtonController();
			//Pido la vista
			DynamicButtonPartialViewBase dynBtnView = dynamicBtnController.GetViewHomeButtons((IUser)Session["User"]);
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

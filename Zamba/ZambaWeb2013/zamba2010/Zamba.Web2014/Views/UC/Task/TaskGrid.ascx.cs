
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Zamba.Core;
using Zamba.Services;


public partial class TaskGrid : System.Web.UI.UserControl
{

	public void LoadTasks(Int32 WFId, Int32 StepId, Int32 DocTypeId, TreeNode Node)
	{
		SRights sRights = new SRights();

		if ((Node != null) && sRights.GetUserRights(ObjectTypes.WFSteps, RightsType.Use, StepId)) {
			if (WFId == null) {
				if (!string.IsNullOrEmpty(ViewState["WFId"].ToString())) {
					WFId = Convert.ToInt32(ViewState["WFId"]);
				}
			}

			if (StepId == 0) {
				if (!string.IsNullOrEmpty(ViewState["StepId"].ToString())) {
					StepId = Convert.ToInt32(ViewState["StepId"]);
				}
			}

			if (DocTypeId == 0) {
				if (ViewState["DocTypeId"] != null && !String.IsNullOrEmpty(ViewState["DocTypeId"].ToString())) {
					DocTypeId = Convert.ToInt32(ViewState["DocTypeId"]);
				}
			}

			//Variables de estado solo usadas en la grilla de tareas
			ViewState["WFId"] = WFId;
			ViewState["StepId"]= StepId;
			ViewState["DocTypeId"] = DocTypeId;

			//Variables de sesion usadas en el taskviewer (si se abre una tarea)
			Session["WFId"] = WFId;
			Session["StepId"] = StepId;
			Session["DocTypeId"] = DocTypeId;


			UserPreferences.setValue("UltimoWFStepUtilizado", StepId.ToString(), Zamba.Core.Sections.WorkFlow);
			ucTaskGrid.Visible = true;
			lblMsg.Visible = false;

			//ucTaskGrid.loadTasks(StepId, Node, ucTaskGridFilter.FC)
			ZOptBusiness zopt = new ZOptBusiness();
			string showAysaFilter = zopt.GetValue("ShowAysaGridFilter");
			string showMarshFilter = zopt.GetValue("ShowMarshGridFilter");
			zopt = null;
			if (!string.IsNullOrEmpty(showAysaFilter) && bool.Parse(showAysaFilter) == true) {
				if ((ucTaskGridFilter != null))
					ucTaskGridFilter.Visible = true;
			} else if (!string.IsNullOrEmpty(showAysaFilter) && bool.Parse(showAysaFilter) == true) {
				if ((ucTaskGridFilter != null))
					ucTaskGridFilter.Visible = true;
			} else {
				if ((ucTaskGridFilter != null))
					ucTaskGridFilter.Visible = false;
			}
		} else {
			ucTaskGrid.Visible = false;
			if ((ucTaskGridFilter != null))
				ucTaskGridFilter.Visible = false;
			lblMsg.Visible = true;
		}
	}

	//Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	//    'AddHandler Grid.DocTypeSelected, AddressOf DocTypeSelected
	//    'Me.Grid.GridWidth = "650"
	//End Sub

	//Private Sub DocTypeSelected(ByVal DocTypeId As Long)
	//    'ViewState("DocTypeId") = String.Empty
	//    'LoadTasks(Session["WFId"], Session["StepId"], DocTypeId)
	//    'cuando se llame a esta funcion (DocTypeSelected) se debe usar el request en lugar de la llamada a "session"
	//    'LoadTasks(Request("WFid")...
	//End Sub

	protected void ucTaskGridFilter_OnFilterCall(object sender, EventArgs e)
	{
		ucTaskGrid.ApplyFilter();
	}

	protected void ucTaskGridFilterMarsh_OnFilterCall(object sender, EventArgs e)
	{
		ucTaskGrid.ApplyFilter();
	}

	public void ClearCurrentFilters(long StepId)
	{
		if ((ucTaskGridFilter != null))
			ucTaskGridFilter.ClearCurrentFilters(StepId);
		if ((ucTaskGridFilterMarsh != null))
			ucTaskGridFilterMarsh.ClearCurrentFilters(StepId);
	}

	public void SetFilters(long StepId)
	{
		if ((ucTaskGridFilter != null))
			ucTaskGridFilter.SetFilters(StepId);
		if ((ucTaskGridFilterMarsh != null))
			ucTaskGridFilterMarsh.SetFilters(StepId);
	}

	/// <summary>
	/// Realiza el rebindeo de la grilla de tareas
	/// </summary>
	/// <remarks></remarks>
	public void RebindGrid()
	{
		ucTaskGrid.BindZGrid(true);
	}
}

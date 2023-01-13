
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Services;
using Zamba.Core;
using System.Web.UI;

public partial class _Main : System.Web.UI.Page
{


	protected void Page_Load(object sender, System.EventArgs e)
	{

		if (Page.Session["UserId"] == null == false) {
			Arbol.SelectedNodeChanged += SelectedNodeChanged;
			Arbol.WFTreeRefreshed += RefreshTaskGrid;
			Arbol.WFTreeIsEmpty -= WfTreeIsEmpty;
			Arbol.WFTreeIsEmpty += WfTreeIsEmpty;

			if (!Page.IsPostBack) {
				Arbol.FillWF();
			} else {
				Arbol.Refresh();
			}
		}
		this.Title = "Zamba - Tareas";
	}

	private void SelectedNodeChanged(Int32 WFId, Int32 StepId, Int32 DocTypeId)
	{
		if (Page.IsPostBack) {
			ScriptManager.RegisterClientScriptBlock(this, GetType(), "TaskCount", "$(document).ready(function () { LoadStepsCounts(); });", true);
		}

		ZOptBusiness zoptb = new ZOptBusiness();
		string CurrentTheme = zoptb.GetValue("CurrentTheme");
		zoptb = null;

		if (CurrentTheme == "AysaDiseno") {
			TaskGrid.ClearCurrentFilters(StepId);
			TaskGrid.SetFilters(StepId);
		}

		TaskGrid.LoadTasks(WFId, StepId, DocTypeId, Arbol.WFTreeView.SelectedNode);
	}

	/// <summary>
	/// Handler para una vez finalizado el refresco de wf, refrescar la grilla
	/// </summary>
	/// <param name="StepId"></param>
	/// <remarks></remarks>
	private void RefreshTaskGrid(Int32 StepId)
	{
		ZOptBusiness zoptb = new ZOptBusiness();
		string CurrentTheme = zoptb.GetValue("CurrentTheme");
		zoptb = null;

		if (CurrentTheme == "AysaDiseno") {
			TaskGrid.ClearCurrentFilters(StepId);
			TaskGrid.SetFilters(StepId);
		}

		TaskGrid.RebindGrid();
	}


	protected void Page_PreRender(object sender, System.EventArgs e)
	{
		if (Page.Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Page.Request.QueryString["docid"])) {
			string docid = Page.Request.QueryString["docid"];

			STasks _STask = new STasks();
			ITaskResult _task = _STask.GetTaskByDocId(Int64.Parse(docid));
			ZOptBusiness zopt = new ZOptBusiness();
			string weblink = zopt.GetValue("WebViewPath");
			zopt = null;
			if (_task != null && !string.IsNullOrEmpty(weblink)) {
				string script = "parent.CreateTaskIframe('" + weblink + "/views/WF/TaskSelector.ashx?doctype=" + _task.DocTypeId + "&docid=" + _task.ID + "&taskid=" + _task.TaskId + "&wfstepid=" + _task.StepId + "'," + _task.TaskId + ",'" + _task.Name + "');";
				Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "OpenTaskLink", script, true);
				_task = null;
			}
		}
	}

	/// <summary>
	/// Metodo que se utiliza para atrapar el evento que el arbol esta vacio
	/// </summary>
	/// <remarks></remarks>
	private void WfTreeIsEmpty()
	{
		UpdTaskGrid.Visible = false;
		lblNoWFVisible.Visible = true;
	}
	public _Main()
	{
		PreRender += Page_PreRender;
		Load += Page_Load;
	}
}


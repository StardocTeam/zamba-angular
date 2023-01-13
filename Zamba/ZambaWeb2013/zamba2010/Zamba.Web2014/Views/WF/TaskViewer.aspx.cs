
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Zamba.Core;
using Zamba.Services;
using Zamba.Core.Enumerators;
using System.Linq;
using System.Web.Services;
using System.Web.Script.Services;
using Zamba.Core.WF.WF;
using System.Web.Security;
using System.Web.UI;
using System.Text;

   [assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Zamba.WFActivity.Regular")]
public partial class TaskViewer : System.Web.UI.Page, ITaskViewer{
 
	private long _userid;
	private IUser _user;
	private int _TaskId;

	private TaskResult _ITaskResult;
	#region "Properties"
	public long Task_ID {
		get { return _TaskId; }
		set { _TaskId = Convert.ToInt32(value); }
	}

	public ITaskResult TaskResult {
		get { return _ITaskResult; }
		set { _ITaskResult = (TaskResult)value; }
	}
	#endregion

	protected void Page_PreInit(object sender, EventArgs e)
	{
		ZOptBusiness zoptb = new ZOptBusiness();
		Page.Theme = zoptb.GetValue("CurrentTheme");
		zoptb = null;

	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		try {
			hdnPostBack.Value = false.ToString();

			//Actualiza el timeout
			if (Page.IsPostBack == false & (Session["User"] != null)) {
				IUser user = default(IUser);
				SRights rights = default(SRights);
				SUserPreferences SUserPreferences = default(SUserPreferences);
				Int32 type = 0;

				try {
					user = (IUser)Session["User"];
					rights = new SRights();
					SUserPreferences = new SUserPreferences();

					if (user.WFLic) {
						type = 1;
					} else {
						Ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), 1);
						Zamba.Membership.MembershipHelper.CurrentUser.WFLic = true;
						((IUser)Session["User"]).WFLic = true;
						type = 1;
					}

					if ((user.ConnectionId > 0)) {
						rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type);
					} else {
						Response.Redirect("~/Views/Security/LogIn.aspx");
					}
					rights = null;
				} catch (Exception ex) {
					Zamba.AppBlock.ZException.Log(ex);
				} finally {
					user = null;
					rights = null;
					SUserPreferences = null;
				}
			}
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	protected void Page_PreLoad(object sender, System.EventArgs e)
	{
		try {
			if (Page.Session["UserId"] != null) {
				_user = (IUser)Session["User"];


				if (!string.IsNullOrEmpty(Request["TaskId"])) {
					Task_ID = Convert.ToInt32(Request["TaskId"]);

					STasks STasks = new STasks();
					TaskResult = STasks.GetTask(Task_ID);
					STasks = null;

					if ((TaskResult != null)) {
						//Se comprueba si se pulsó el botón cerrar, si lo hizo se cierra desde acá
						//y se corta la cade de page load con los controles asignandolos a null
						if (!string.IsNullOrEmpty(this.Request["__EVENTTARGET"]) && this.Request["__EVENTTARGET"].Contains("BtnClose")) {
							CloseAndFinishTask();

						} else {
							//Obtener las reglas que se podrán ejecutar por el usuario
							TaskResult.UserRules = GetDoEnableRules();
							ucTaskHeader.TaskResult = TaskResult;
							ucTaskDetail.TaskResult = TaskResult;

							ucTaskHeader.ExecuteRule -= ExecuteRule;
							ucTaskHeader.ExecuteRule += ExecuteRule;
							ucTaskDetail.ExecuteRule -= ExecuteRule;
							ucTaskDetail.ExecuteRule += ExecuteRule;

							//Ezequiel - 01/02/10 - Creo variable de ejecucion de workflow y se la paso al taskheader.
							WFExecution WFExec = new WFExecution(_user);
							WFExec._haceralgoEvent += UC_WFExecution.HandleWFExecutionPendingEvents;
							UC_WFExecution.RefreshTask += RefreshTask;
							UC_WFExecution.RefreshWFTree -= GenerateWfRefreshJs;
							UC_WFExecution.RefreshWFTree += GenerateWfRefreshJs;
							this.UC_WFExecution.WFExec = WFExec;
							this.UC_WFExecution.TaskID = Task_ID;

							hdnTaskID.Value = Task_ID.ToString();
							hdnUserId.Value = Zamba.Membership.MembershipHelper.CurrentUser.ID.ToString();
						}
					}
				}
			} else {
				FormsAuthentication.RedirectToLoginPage();
				this.Controls.Remove(ucTaskDetail);
				this.Controls.Remove(ucTaskHeader);
				ucTaskDetail = null;
				ucTaskHeader = null;
			}
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	private void CloseAndFinishTask(ITaskResult task = null)
	{
		try {
			this.Controls.Remove(ucTaskDetail);
			this.Controls.Remove(ucTaskHeader);
			ucTaskDetail = null;
			ucTaskHeader = null;

			ITaskResult taskToClose = default(ITaskResult);
			if (task == null) {
				taskToClose = this.TaskResult;
			} else {
				taskToClose = task;
			}

			if (TaskResult != null) {
				WFTaskBusiness.UnLockTask(TaskResult.TaskId);
			}

			//Marca a la tarea como cerrada para el usuario
			Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByTaskId(taskToClose.TaskId);

			//Se cierra la tarea abierta
			string script = "$(window).load(function(){hideLoading();isClosingTask=true;parent.CloseTask(" + taskToClose.TaskId + ",true);});";
			ScriptManager.RegisterClientScriptBlock(this, Page.GetType(), "closingScript", script, true);

			//Finalización de la tarea en caso de que corresponda
			if (taskToClose.AsignedToId == _user.ID) {
				SUserPreferences SUserPreferences = new SUserPreferences();
				SRights SRights = new SRights();

				//Si tiene el permiso de TERMINAR o el tilde de FINALIZAR_AL_CERRAR, entonces desasigna la tarea.
				if (SRights.GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, Convert.ToInt32(taskToClose.StepId)) && Convert.ToBoolean(SUserPreferences.getValue("CheckFinishTaskAfterClose", Sections.WorkFlow, "True"))) {
					taskToClose.TaskState = Zamba.Core.TaskStates.Desasignada;
					taskToClose.AsignedToId = 0;
				} else {
					taskToClose.TaskState = Zamba.Core.TaskStates.Asignada;
					taskToClose.AsignedToId = _user.ID;
				}
				SRights = null;
				SUserPreferences = null;

				STasks STasks = new STasks();
				STasks.Finalizar(taskToClose);
				STasks = null;

				System.Collections.Generic.List<Zamba.Core.ITaskResult> Results = new System.Collections.Generic.List<Zamba.Core.ITaskResult>();
				Results.Add(taskToClose);

				foreach (Zamba.Core.WFRuleParent Rule in taskToClose.WfStep.Rules) {
					if (Rule.RuleType == TypesofRules.Terminar) {
						SRules SRules = new SRules();
						SRules.ExecuteRule(Rule, Results);
					}
				}
			}

		} catch (Exception ex) {
			Zamba.Core.ZClass.raiseerror(ex);
		} finally {
			TaskResult = null;
            string Script = "$(document).ready(function(){ hideLoading();});";
            Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "CloseLoadingDialog", Script, true);
        }
	}

	private void RefreshTask(ITaskResult task, bool DoPostBack, ref List<long> TaskIDsToRefresh)
	{

		try {
			if (TaskIDsToRefresh == null) {
				TaskIDsToRefresh = new List<long>();
			}

			if (!TaskIDsToRefresh.Contains(task.TaskId)) {
				TaskIDsToRefresh.Add(task.TaskId);
			}

			StringBuilder sbScript = new StringBuilder();
			long taskIdToRefresh = 0;
			List<long> idsRegistereds = new List<long>();
			ITaskResult tempTask = default(ITaskResult);

			long max = TaskIDsToRefresh.Count;

			sbScript.Append("$(document).ready(function(){");
            Zamba.Core.WF.WF.WFTaskBusiness WFTB = new Zamba.Core.WF.WF.WFTaskBusiness();
			for (int i = 0; i <= max - 1; i++) {
				taskIdToRefresh =TaskIDsToRefresh[i];

				if (!idsRegistereds.Contains(taskIdToRefresh) && taskIdToRefresh > 0) {
					tempTask = WFTB.GetTask(taskIdToRefresh);
					sbScript.Append("parent.RefreshTask(");
					sbScript.Append(taskIdToRefresh);
					sbScript.Append(",");
					sbScript.Append(tempTask.ID);
					sbScript.Append(");");

					idsRegistereds.Add(taskIdToRefresh);
				}
			}
			WFTB = null;

			sbScript.Append("});");
			TaskIDsToRefresh.Clear();

			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RefreshTaskScript", sbScript.ToString(), true);
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	private void GenerateCloseTaskJs(Int64 taskID)
	{
		try {
			Response.Write("<script language='javascript'> { parent.CloseTask(" + taskID + "),false;}</script>");
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	/// <summary>
	/// Actualizo el arbol de tareas
	/// </summary>
	/// <remarks></remarks>
	private void GenerateWfRefreshJs()
	{
		try {
			Response.Write("<script language='javascript'> { parent.RefreshGrid('tareas');}</script>");
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	/// <summary>
	/// Metodo el cual verifica si existe una doshowform entre las reglas.
	/// </summary>
	/// <param name="_rule"></param>
	/// <returns></returns>
	/// <remarks></remarks>
	private IWFRuleParent CheckChildRules(IWFRuleParent _rule)
	{
        IWFRuleParent raux = null;
		try {
			foreach (IRule _ruleaux in _rule.ChildRules) {
				raux = CheckChildRules((IWFRuleParent)_ruleaux);
			}
			if (_rule.GetType().FullName == "Zamba.WFActivity.Regular.DoShowForm") {
				raux = _rule;
				((IDoShowForm)_rule).RuleParentType = TypesofRules.AbrirDocumento;
			}
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
		return raux;
	}

	/// <summary>
	/// Verifica que reglas se habilitan por la DoEnableRule
	/// </summary>
	/// <returns>Devuelve una hashtable donde la key es el ID de la regla(long) y el valor es una lista de boolans.</returns>
	/// <remarks></remarks>
	private Hashtable GetDoEnableRules()
	{
		try {
			//Se comenta esto, dado que no es necesario obtener la etapa para ver sus reglas
			//Obtenemos las reglas de esa etapa
			//Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

			dynamic returnEnableRules = new Hashtable();
			Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
			Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
			List<Int64> ExecutedIDs = new List<Int64>();
			Hashtable Params = new Hashtable();
			List<Int64> PendingChildRules = new List<Int64>();
            //Dim tempRuleBooleanList As List(Of Boolean) = New List(Of Boolean)()
            System.Collections.Generic.List<ITaskResult> List = new System.Collections.Generic.List<ITaskResult>();
			List.Add(TaskResult);

			//Se mueven estas variables del foreach, ya que se utilizara linq y for, 
			//para luego ver si se puede hacer en paralelo
			WFRulesBusiness WFRB = new WFRulesBusiness();
			bool RefreshRule = false;
			SRules sRules = new SRules();

			List<IWFRuleParent> rules = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId);

			//For Each Rule As WFRuleParent In wfstep.Rules
			//    If (Rule.RuleType = TypesofRules.AbrirDocumento) Then
			//        If (Rule.GetType().FullName <> "Zamba.WFActivity.Regular.DoShowForm") Then
			//            RefreshRule = Rule.RefreshRule
			//            WFRB.ExecuteWebRule(Rule.ID, List, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, RefreshRule)
			//            'Else
			//            'To-do: cargar con regla DoShowForm 
			//        End If
			//    End If
			//Next
			if ((rules != null)) {
				dynamic rulesOpenDoc = from rule in rules where rule.RuleType == TypesofRules.AbrirDocumento && string.Compare(rule.GetType().FullName, "Zamba.WFActivity.Regular.DoShowForm") != 0 select rule;

				if (rulesOpenDoc.Count > 0) {
					if (TaskResult.WfStep.Rules.Count == 0) {
						dynamic userActionRules = from rule in rules where rule.ParentType == TypesofRules.AccionUsuario select rule;

						TaskResult.WfStep.Rules.AddRange(userActionRules);
					}

					for (int i = 0; i <= rulesOpenDoc.Count - 1; i++) {
						RefreshRule = rulesOpenDoc(i).RefreshRule;
						WFRB.ExecuteWebRule(rulesOpenDoc(i).ID, List, ref PendigEvent, ref  ExecutionResult, ref ExecutedIDs, ref Params, ref PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh);
					}
				}
			}

			return TaskResult.UserRules;
		} catch (Exception ex) {
			ZClass.raiseerror(ex);
			return null;
		}
	}

	/// <summary>
	/// Ejecuta las reglas desde la master
	/// </summary>
	/// <param name="ruleId">ID de la regla que se quiere ejecutar</param>
	/// <param name="results">Tareas a ejecutar</param>
	/// <remarks></remarks>
	private void ExecuteRule(Int64 ruleId, List<Zamba.Core.ITaskResult> results)
	{
		try {
			Zamba.Core.RuleExecutionResult ExecutionResult = new Zamba.Core.RuleExecutionResult();
			Zamba.Core.RulePendingEvents PendigEvent = new Zamba.Core.RulePendingEvents();
			List<Int64> ExecutedIDs = new List<Int64>();
			List<Int64> PendingChildRules = new List<Int64>();
			Hashtable Params = new Hashtable();
			bool RefreshRule = false;

			if (UC_WFExecution.TaskIdsToRefresh == null) {
				UC_WFExecution.TaskIdsToRefresh = new List<long>();
			}

			this.UC_WFExecution.WFExec.ExecuteRule(ruleId, ref results, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, ref RefreshRule, UC_WFExecution.TaskIdsToRefresh);
		} catch (Exception ex) {
			Zamba.AppBlock.ZException.Log(ex);
		}
	}

	public string TaskName()
	{
		return TaskResult.Name;
	}

	/// <summary>
	/// Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
	/// </summary>
	/// <remarks></remarks>
	[WebMethod()]
	public static FieldOptions GetZVarOptions(string controlId, string dataSourceName, string displayMember, string valueMember, string filterColumn, string filterValue)
	{
		FormControlsController formControlsController = new FormControlsController();

		return formControlsController.GetZVarOptions(controlId, dataSourceName, displayMember, valueMember, filterColumn, filterValue);
	}

	/// <summary>
	/// Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
	/// </summary>
	/// <remarks></remarks>
	[WebMethod()]
	public static FieldOptions GetZDynamicTable(string controlId, string dataSource, string showColumns, string filterFieldId, string editableColumns, string editableColumnsAttributes, string filterValues, string additionalValidationButton, string postAjaxFuncion)
	{
		FormControlsController formControlsController = new FormControlsController();

		return formControlsController.GetZDynamicTable(controlId, dataSource, showColumns, filterFieldId, editableColumns, editableColumnsAttributes, filterValues, additionalValidationButton, postAjaxFuncion);
	}

	/// <summary>
	/// Devuelve una lista de key value, para renderizar a las opciones de un autocomplete. ZVar aun no soportado.
	/// </summary>
	/// <remarks></remarks>
	[WebMethod()]
	public static List<KeyValuePair<string, string>> GetAutoCompleteOptions(string query, string dataSource, string displayMember, string valueMember, string additionalFilters)
	{
		FormControlsController formControlsController = new FormControlsController();

		return formControlsController.GetAutoCompleteOptions(query, dataSource, displayMember, valueMember, additionalFilters);
	}
	public TaskViewer()
	{
		PreLoad += Page_PreLoad;
		Load += Page_Load;
		PreInit += Page_PreInit;
	}
}

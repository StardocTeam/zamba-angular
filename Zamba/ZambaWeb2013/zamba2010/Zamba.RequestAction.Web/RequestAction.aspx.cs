using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using Zamba.Core;
using ASP;
using RequestActionWebService;

public partial class ExecuteRequestAction : Page
{
    #region Constants
    private const Int32 VALIDATION_ITEMS = 2;
    private const Int32 COLUMN_SPAN_INFORMATION_TABLE = 2;

    private const String ERRORS_FOUND = "Se han encontrado errores en la página";
    private const String ERROR_NO_USER_ID = "No se especifico usuario";
    private const String ERROR_REQUEST_ACTION_ID = "No se especifico tarea";

    private const String QUERY_STRING_USER_ID = "UserId";
    private const String QUERY_STRING_RULE_ID = "RuleId";
    private const String QUERY_STRING_REQUEST_ACTION_ID = "RequestActionId";

    private const String MESSAGE_ALREADY_EXECUTED = "Esta Tarea ya ha sido realizada.";
    private const String MESSAGE_EXECUTION_SUCCESSFUL = "Tarea finalizada";

    private const String CSS_LABEL = "Label";
    private const String CSS_BUTTON = "Button";
    #endregion

    private RequestAction _requestAction = null;

    #region Properties
    public Int64? RequestActionId
    {
        get
        {
            String RequestActionId = Request.QueryString[QUERY_STRING_REQUEST_ACTION_ID];

            Int64? ReturnValue = null;
            Int64 PosibleValue;

            if (Int64.TryParse(RequestActionId, out PosibleValue))
                ReturnValue = (Int64?)PosibleValue;

            return ReturnValue;
        }
    }
    public Int64? UserId
    {
        get
        {
            String UserId = Request.QueryString[QUERY_STRING_USER_ID];

            Int64? ReturnValue = null;
            Int64 PosibleValue;

            if (Int64.TryParse(UserId, out PosibleValue))
                ReturnValue = (Int64?)PosibleValue;

            return ReturnValue;
        }
    }
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {

        List<String> ErrorMessages = null;
        try
        {
            HideControls();

            if (CanExecuteRule(ref ErrorMessages))
            {
                _requestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value);
                ShowInformation(_requestAction);
            }
            else
                HandleErrors(ErrorMessages);

        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
        finally
        {
            #region Dispose
            if (null != ErrorMessages)
            {
                ErrorMessages.Clear();
                ErrorMessages = null;
            }
            #endregion
        }
    }

    protected void btExecuteRule_Click(object sender, EventArgs e)
    {
        if (sender is Button)
        {
            Button CurrentButton = (Button)sender;
            List<Int64> SelectedTaskIds = null;
            try
            {
                Int64 RuleId;

                if (Int64.TryParse(CurrentButton.CommandName, out RuleId)) //El unico lugar donde pude poder el ID via DataSource fue en el CommandName
                {
                    SelectedTaskIds = UcTasksList.SelectedTaskIds;

                    if (null == SelectedTaskIds || SelectedTaskIds.Count == 0)
                        HandleError("Seleccione al menos una tarea de la grilla.");
                    else
                    {
                        using (RequestAction CurrentRequestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value))
                        {
                            using (RequestActionService CurrentWebService = new RequestActionService())
                            {

                                if (CurrentRequestAction.TasksAndStepIds.ContainsKey(SelectedTaskIds[0]))//Obtengo el stepId de cualquier Task seleccionado
                                {
                                    Int64 StepId = CurrentRequestAction.TasksAndStepIds[SelectedTaskIds[0]];
                                    CurrentWebService.ExecuteRules(RuleId, SelectedTaskIds.ToArray(), StepId);
                                }
                            }

                            CurrentRequestAction.IsFinished = true;
                            RequestActionBusiness.Update(CurrentRequestAction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                #region Dispose
                if (null != CurrentButton)
                {
                    CurrentButton.Dispose();
                    CurrentButton = null;
                }
                if (null != SelectedTaskIds)
                {
                    SelectedTaskIds.Clear();
                    SelectedTaskIds = null;
                }
                #endregion
            }
        }
    }
    protected void UcTasksList_SelectedTaskChanged(Int64? taskId)
    {
        try
        {
            if (taskId.HasValue)
            {
                UcTasksInformation.TaskId = taskId.Value;
                UcTasksInformation.Visible = true;
            }
            else
            {
                UcTasksInformation.Clear();
                UcTasksInformation.Visible = false;
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    protected void UcFinishedTasks_SelectedTaskChanged(Int64? taskId)
    {
        try
        {
            if (taskId.HasValue)
            {
                UcFinishedTasksInformation.TaskId = taskId.Value;
                UcFinishedTasksInformation.Visible = true;
            }
            else
            {
                UcFinishedTasksInformation.Clear();
                UcFinishedTasksInformation.Visible = false;
            }
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }
    #endregion

    /// <summary>
    /// Valida si se puede ejecutar el RequestAction
    /// </summary>
    /// <param name="errorMessages"></param>
    /// <returns></returns>
    private Boolean CanExecuteRule(ref List<String> errorMessages)
    {
        Boolean CanExecute = true;

        if (null == errorMessages)
            errorMessages = new List<String>(VALIDATION_ITEMS);

        if (!RequestActionId.HasValue)
        {
            errorMessages.Add(ERROR_REQUEST_ACTION_ID);
            CanExecute = false;
        }
        if (!UserId.HasValue)
        {
            errorMessages.Add(ERROR_NO_USER_ID);
            CanExecute = false;
        }

        return CanExecute;
    }

    /// <summary>
    /// Esconde todos los controles de esta pagina. Se usa para que en cada postback se "limpie" el estado 
    /// y no quede algo visible que no deba. 
    /// 
    /// Cada Metodo habilita los controles que necesita solamente.
    /// </summary>
    private void HideControls()
    {
        lbSuccess.Visible = false;
        lbError.Visible = false;
        blErrors.Visible = false;

        fsButtons.Visible = false;
        rpRules.Visible = false;

        tblNeededValues.Visible = false;
        tblRequestInformation.Visible = false;

        UcTasksInformation.Visible = false;
        UcTasksInformation.Visible = false;
    }

    #region Extra Values
    /// <summary>
    /// Valida si se necesitan valores extras para ejecutar una regla
    /// </summary>
    /// <param name="requestTaskId"></param>
    /// <returns></returns>
    private Boolean NeedsExtraValues(Int64 ruleId)
    {

        // TODO: Implementar si una tarea necesita o no valores extras.
        return false;
    }
    //private void GetExtraValues(ref List<Int64> indexsId, ref List<Int64> indexsValue)
    //{
    //   if (null == indexsId)
    //      indexsId = new List<Int64>();
    //   if (null == indexsValue)
    //      indexsValue = new List<Int64>();

    //}
    #endregion

    #region Information
    /// <summary>
    /// Muestra la información de la RequestAction , incluyendo información de las
    /// las tareas , regla a ejecutar 
    /// </summary>
    /// <param name="requestAction"></param>
    private void ShowInformation(RequestAction request)
    {
        List<Int64> UnfinishedTasks = new List<Int64>(request.Tasks.Count);
        List<Int64> FinishedTasks = new List<Int64>(request.Tasks.Count);

        foreach (RequestActionTask CurrentTask in request.Tasks)
        {
            if (CurrentTask.IsExecuted)
                FinishedTasks.Add(CurrentTask.TaskId);
            else
                UnfinishedTasks.Add(CurrentTask.TaskId);
        }

        UcTasksList.Tasks = WFTaskBussines.GetTasksByTaskIds(UnfinishedTasks);
        if (UnfinishedTasks.Count > 0)
            UcTasksList.Select(UnfinishedTasks[0]);

        UcFinishedTasks.Tasks = WFTaskBussines.GetTasksByTaskIds(FinishedTasks);
        if (FinishedTasks.Count > 0)
            UcFinishedTasks.Select(FinishedTasks[0]);

        UnfinishedTasks.Clear();
        UnfinishedTasks = null;

        FinishedTasks.Clear();
        FinishedTasks = null;

        LoadRules(request);
        ShowHistory(request);
    }
    /// <summary>
    /// Muestra el historial de un Request Action
    /// </summary>
    /// <param name="requestAction"></param>
    private void ShowHistory(RequestAction request)
    {
        lbSuccess.Visible = true;

        if (request.IsFinished)
            lbSuccess.Text = MESSAGE_ALREADY_EXECUTED;

        tblRequestInformation.Visible = true;
        tblRequestInformation.Rows.Add(BuildInformationRow("Fecha de Pedido:", request.RequestDate.ToString()));

        if (request.IsFinished)
            tblRequestInformation.Rows.Add(BuildInformationRow("Fecha de Finalización:", request.FinishDate.Value.ToString()));

        IUser RequestUser = UserBusiness.GetUserById(request.RequestUserId);
        tblRequestInformation.Rows.Add(BuildInformationRow("Requerido por:", RequestUser.Apellidos + " , " + RequestUser.Name));
    }
    /// <summary>
    /// Creates the buttons for the Rules 
    /// </summary>
    /// <param name="request"></param>
    private void LoadRules(RequestAction request)
    {
        Dictionary<Int64, Int64> RulesAndStepIds = new Dictionary<Int64, Int64>();

        foreach (Int64 CurrentRuleId in request.RulesIds)
            RulesAndStepIds.Add(CurrentRuleId, 0); // TODO: Ponerle un StepId coherente , averiguar de donde sacarlo

        List<IWFRuleParent> Rules = WFRulesBussines.GetRulesById(RulesAndStepIds, Server.MapPath("~/bin"));

        if (null == Rules || Rules.Count == 0)
        {
            fsButtons.Visible = false;
            rpRules.Visible = false;
        }
        else
        {
            fsButtons.Visible = true;
            rpRules.Visible = true;

            rpRules.DataSource = Rules;
            rpRules.DataBind();
        }
      

        if (null != Rules)
        {
            Rules.Clear();
            Rules = null;
        }

    }
    /// <summary>
    /// Crea una HtmlTableRow con informacion generica. Solo arma la estructura.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private HtmlTableRow BuildInformationRow(String key, String value)
    {
        Label LbKey = new Label();
        LbKey.Text = key;
        LbKey.CssClass = CSS_LABEL;

        HtmlTableCell TcKey = new HtmlTableCell();
        TcKey.Controls.Add(LbKey);

        Label LbValue = new Label();
        LbValue.Text = value;
        LbValue.CssClass = CSS_LABEL;

        HtmlTableCell TcValue = new HtmlTableCell();
        TcValue.Controls.Add(LbValue);

        HtmlTableRow CurrentRow = new HtmlTableRow();
        CurrentRow.Cells.Add(TcKey);
        CurrentRow.Cells.Add(TcValue);

        return CurrentRow;
    }
    #endregion

    #region Exceptions & Errors
    /// <summary>
    /// Se encarga del manejo de la logica de errores.
    /// </summary>
    /// <param name="errors"></param>
    private void HandleErrors(List<String> errors)
    {
        blErrors.Visible = true;
        blErrors.Items.Clear();

        lbError.Visible = true;
        lbError.Text = ERRORS_FOUND;

        foreach (String CurrentError in errors)
            blErrors.Items.Add(new ListItem(CurrentError));
    }
    /// <summary>
    /// Se encarga del manejo de la logica de errores.
    /// </summary>
    /// <param name="error"></param>
    private void HandleError(String error)
    {
        lbError.Visible = true;
        lbError.Text = ERRORS_FOUND;

        blErrors.Visible = true;
        blErrors.Items.Clear();
        blErrors.Items.Add(new ListItem(error));
    }
    /// <summary>
    /// Se encarga del manejo de la logica de Exceptions.
    /// </summary>
    /// <param name="exception"></param>
    private void HandleException(Exception exception)
    {
        ZClass.raiseerror(exception);
        HandleError(exception.Message);
    }
    #endregion
}
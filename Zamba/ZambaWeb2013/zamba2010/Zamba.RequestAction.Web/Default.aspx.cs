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

public partial class _Default
    : System.Web.UI.Page
{
    #region Constants
    private const Int32 VALIDATION_ITEMS = 4;
    private const Int32 COLUMN_SPAN_INFORMATION_TABLE = 2;
    private const String ERROR_NO_USER_ID = "No se especifico usuario";
    private const String ERROR_NO_RULE_ID = "No se especifico regla a ejecutar";
    private const String ERROR_REQUEST_ACTION_ID = "No se especifico tarea";
    private const String ERROR_ALREADY_EXECUTED = "Tarea ya Realizada";
    private const String QUERY_STRING_USER_ID = "UserId";
    private const String QUERY_STRING_RULE_ID = "RuleId";
    private const String QUERY_STRING_REQUEST_ACTION_ID = "RequestActionId";
    private const String MESSAGE_ALREADY_EXECUTED = "Esta Tarea ya ha sido realizada.";
    private const String MESSAGE_EXECUTION_SUCCESSFUL = "Tarea finalizada";
    #endregion

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
    public Int64? RuleId
    {
        get
        {
            String RuleId = Request.QueryString[QUERY_STRING_RULE_ID];

            Int64? ReturnValue = null;
            Int64 PosibleValue;

            if (Int64.TryParse(RuleId, out PosibleValue))
                ReturnValue = (Int64?)PosibleValue;

            return ReturnValue;
        }
    }
    public List<Int64> SelectedTasksIds
    {
        get
        {
            return UcTasksList.SelectedTaskIds;
        }
    }
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        HideControls();

        try
        {    
            List<String> ErrorMessages = null;

            if (CanExecuteRule(ref ErrorMessages))
            {
                RequestAction CurrentRequestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value);

                if (CurrentRequestAction.IsFinished)
                    ShowRequestActionHistory(CurrentRequestAction);
                else
                    btExectuteRule.Visible = true;
            }
            else
                HandleErrors(ErrorMessages);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    protected void btExectuteRule_Click(object sender, EventArgs e)
    {
        try
        {
            RequestAction CurrentRequestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value);
        }
        catch (Exception ex)
        {
            HandleException(ex);
        }
    }

    protected void UcTasksList_SelectedTaskChanged(Int64? taskId)
    {
        try
        {
            if (taskId.HasValue)
                UcTaskInformation.TaskId = taskId.Value;
            else
                UcTaskInformation.Clear();
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
            errorMessages = new List<string>(VALIDATION_ITEMS);

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
        if (!RuleId.HasValue)
        {
            errorMessages.Add(ERROR_NO_RULE_ID);
            CanExecute = false;
        }

        return CanExecute;
    }
    /// <summary>
    /// Valida si se necesitan valores extras para ejecutar una regla
    /// </summary>
    /// <param name="requestTaskId"></param>
    /// <returns></returns>
    private Boolean NeedsExtraValues(Int64 ruleId)
    {
        return false;
    }

    /// <summary>
    /// Muestra el historial de un Request Action
    /// </summary>
    /// <param name="requestAction"></param>
    private void ShowRequestActionHistory(RequestAction requestAction)
    {
        lbSuccess.Visible = true;
        lbSuccess.Text = MESSAGE_ALREADY_EXECUTED;

        tblInformation.Visible = true;

        #region Tasks
        List<Int64> TaskIds = new List<Int64>(requestAction.TasksAndStepIds.Keys.Count);
        TaskIds.AddRange(requestAction.TasksAndStepIds.Keys);
        
        UcTaskInformation.Visible = true;

        HtmlTableCell TcTasks = new HtmlTableCell();
        TcTasks.Controls.Add(UcTaskInformation);
        TcTasks.ColSpan = COLUMN_SPAN_INFORMATION_TABLE;

        HtmlTableRow TrTasks = new HtmlTableRow();
        TrTasks.Controls.Add(TcTasks);

        tblInformation.Rows.Add(TrTasks);
        #endregion

        #region Information
        tblInformation.Rows.Add(BuildInformationRow("Fecha de Pedido:", requestAction.RequestDate.ToString()));
        tblInformation.Rows.Add(BuildInformationRow("Fecha de finalización:", requestAction.FinishDate.Value.ToString()));

        IUser RequestUser = UserBusiness.GetUserById(requestAction.RequestUserId);
        tblInformation.Rows.Add(BuildInformationRow("Requerido por:", RequestUser.Apellidos + " , " + RequestUser.Name));

        #endregion
    }

    /// <summary>
    /// Crea una HtmlTableRow con informacion generica. Solo arma la estructura.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    private HtmlTableRow BuildInformationRow(String key, String value)
    {
        HtmlTableCell TcKey = new HtmlTableCell();
        TcKey.InnerText = key;

        HtmlTableCell TcValue = new HtmlTableCell();
        TcValue.InnerText  = value;

        HtmlTableRow CurrentRow = new HtmlTableRow();
        CurrentRow.Cells.Add(TcKey);
        CurrentRow.Cells.Add(TcValue);

        return CurrentRow;
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

        tblInformation.Visible = false;
        tblNeededValues.Visible = false; 
        
        tbSuccess.Visible = false;

        UcTaskInformation.Visible = false;
        btExectuteRule.Visible = false;
    }

    #region Exceptions & Errors
    /// <summary>
    /// Se encarga del manejo de la logica de errores.
    /// </summary>
    /// <param name="errors"></param>
    private void HandleErrors(List<String> errors)
    {
        StringBuilder ErrorBuilder = new StringBuilder();

        foreach (String CurrentError in errors)
            ErrorBuilder.AppendLine(CurrentError);

        lbError.Text = ErrorBuilder.ToString();
        lbError.Visible = true;

        ErrorBuilder.Remove(0, ErrorBuilder.Length);
    }
    /// <summary>
    /// Se encarga del manejo de la logica de errores.
    /// </summary>
    /// <param name="error"></param>
    private void HandleError(String error)
    {
        lbError.Text = error;
        lbError.Visible = true;
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

    [Obsolete("Metodo de testeo")]
    private void TestObtenerRequestAction()
    {
        lbError.Visible = false;
        RequestAction CurrentRequestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value);

        StringBuilder InfoBuilder = new StringBuilder();

        InfoBuilder.AppendLine("ID = " + CurrentRequestAction.RequestActionId .Value.ToString());
        InfoBuilder.AppendLine("RequestDate = " + CurrentRequestAction.RequestDate.ToString());
        InfoBuilder.AppendLine("Finalizado = " + CurrentRequestAction.IsFinished.ToString());

        if (null != CurrentRequestAction.FinishDate)
            InfoBuilder.AppendLine("FinishDate = " + CurrentRequestAction.FinishDate);
        else
            InfoBuilder.AppendLine("FinishDate = NULL");

        InfoBuilder.AppendLine("RequestUserId = " + CurrentRequestAction.RequestUserId.ToString());

        InfoBuilder.AppendLine();

        InfoBuilder.AppendLine("RuleIds =");

        foreach (Int64 RuleId in CurrentRequestAction.RulesIds)
            InfoBuilder.AppendLine("    " + RuleId.ToString());

        InfoBuilder.AppendLine();

        InfoBuilder.AppendLine("UserIds =");

        foreach (Int64 RuleId in CurrentRequestAction.UsersIds)
            InfoBuilder.AppendLine("    " + RuleId.ToString());

        InfoBuilder.AppendLine();

        InfoBuilder.AppendLine("TaskId+StepId=");

        foreach (KeyValuePair<Int64, Int64> CurrentItem in CurrentRequestAction.TasksAndStepIds)
            InfoBuilder.AppendLine("    " + CurrentItem.Key.ToString() + " - " + CurrentItem.Value.ToString());


        tbSuccess.Visible = true;
        tbSuccess.Text = InfoBuilder.ToString();
    }
    [Obsolete("Metodo de testeo")]
    private void TestEnvioMail()
    {
        RequestAction CurrentRequestAction = new RequestAction();
        CurrentRequestAction.IsFinished = false;
        CurrentRequestAction.RulesIds.Add(1228);
        CurrentRequestAction.RulesIds.Add(1230);
        CurrentRequestAction.RulesIds.Add(1232);

        CurrentRequestAction.TasksAndStepIds.Add(12547, 3265);
        CurrentRequestAction.UsersIds.Add(1870);
        CurrentRequestAction.RequestDate = DateTime.Now;
        CurrentRequestAction.FinishDate = null;

        RequestActionBusiness.Notify(CurrentRequestAction, "Body del Mensaje", "Test de Envio de Mail Request Action");
    }
}
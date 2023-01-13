using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zamba.Core;
using System.Data;

public partial class ExecuteRequestAction
    : Page
{
    #region Constants
    private const Int32 VALIDATION_ITEMS = 2;
    private const Int32 COLUMN_SPAN_INFORMATION_TABLE = 2;

    private const String ERRORS_FOUND = "Se han encontrado errores en la página";

    private const String QUERY_STRING_USER_ID = "UserId";
    private const String QUERY_STRING_REQUEST_ACTION_ID = "RequestActionId";

    private const String MESSAGE_ALREADY_EXECUTED = "Esta Tarea ya ha sido realizada.";
    private const String MESSAGE_EXECUTION_SUCCESSFUL = "Tarea finalizada";

    private const String CSS_LABEL = "Label";
    private const String CSS_BUTTON = "Button";
    #endregion

    #region Atributos
    private RequestAction _requestAction = null;
    #endregion

    #region Properties
    /// <summary>
    /// Devuelve el Id del RequestAction cargado en la pagina
    /// </summary>
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
    /// <summary>
    /// Devuelve el Id de usuario cargado
    /// </summary>
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
    /// <summary>
    /// Devuelve el listado de tareas seleccionados
    /// </summary>
    private List<Int64> SelectedTaskIds
    {
        get
        {
            return UcTasksList.SelectedTaskIds;
        }
    }
    #endregion

    #region Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            HideControls();

            if (RequestActionId.HasValue && UserId.HasValue)
            {
                _requestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value);
                ShowInformation(_requestAction);
            }
            else //Cargo la pagina de listado de pedidos
                Response.Redirect("RequestActionList.aspx");

        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
            HandleException(ex);
        }
    }
    protected void btExecuteRule_Click(object sender, EventArgs e)
    {
        //Este delegado se pone dinamicamente a los botones de las reglas, 
        //valido por las dudas que sea tirado por 1 boton

        if (sender is Button)
        {
            Button CurrentButton = (Button)sender;
            try
            {
                Int64 RuleId;

                if (Int64.TryParse(CurrentButton.CommandName, out RuleId)) //El unico lugar donde pude poder el ID via DataSource fue en el CommandName
                {
                    if (null == SelectedTaskIds || SelectedTaskIds.Count == 0)
                        HandleError("Seleccione al menos una tarea de la grilla.");
                    else
                    {
                        using (RequestAction CurrentRequestAction = RequestActionBusiness.GetRequestAction(RequestActionId.Value))
                        {
                            if (CurrentRequestAction.TasksAndStepIds.ContainsKey(SelectedTaskIds[0]))//Obtengo el stepId de cualquier Task seleccionado
                            {
                                WFRulesBussines oWFRB = new WFRulesBussines();
                                Int64 StepId = CurrentRequestAction.TasksAndStepIds[SelectedTaskIds[0]];
                                oWFRB.Execute(RuleId, StepId, SelectedTaskIds, Server.MapPath("~/bin/"));
                                SaveHistory(RuleId, StepId, SelectedTaskIds);
                                Page_Init(null, null);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ZClass.raiseerror(ex);
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
            //else
            //{
            //    UcTasksInformation.Clear();
            //    UcTasksInformation.Visible = false;
            //}
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
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
            ZClass.raiseerror(ex);
            HandleException(ex);
        }
    }
    protected void lnkViewHistory_Click(object sender, EventArgs e)
    {
        try
        {
            StringBuilder LinkBuilder = new StringBuilder();
            LinkBuilder.Append("TasksHistory.aspx?");
            LinkBuilder.Append(QUERY_STRING_REQUEST_ACTION_ID);
            LinkBuilder.Append("=");
            LinkBuilder.Append(RequestActionId.Value.ToString());
            LinkBuilder.Append("&");
            LinkBuilder.Append(QUERY_STRING_USER_ID);
            LinkBuilder.Append("=");
            LinkBuilder.Append(UserId.Value);

            Response.Redirect(LinkBuilder.ToString());
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }
    }
    #endregion

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

        //fsButtons.Visible = false;
        rpRules.Visible = false;

        tblNeededValues.Visible = false;
        tblRequestInformation.Visible = false;

        UcTasksInformation.Visible = false;
        UcTasksInformation.Visible = false;

        //lnkViewHistory.Visible = false;
    }

    /// <summary>
    /// Guarda el historial de las ejecucion de la regla en las tareas
    /// </summary>
    /// <param name="RuleId"></param>
    /// <param name="StepId"></param>
    private void SaveHistory(Int64 RuleId, Int64 StepId, List<Int64> taskIds)
    {
        RequestActionTask CurrentTaskAction = null;
        foreach (Int64 TaskId in taskIds)
        {
            CurrentTaskAction = new RequestActionTask(TaskId, StepId, UserId.Value, RuleId, DateTime.Now);
            RequestActionBusiness.Insert(RequestActionId.Value, CurrentTaskAction);
        }
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
    #endregion

    #region Information
    /// <summary>
    /// Muestra la información de la RequestAction , incluyendo información de las
    /// las tareas , regla a ejecutar 
    /// </summary>
    /// <param name="requestAction"></param>
    private void ShowInformation(RequestAction request)
    {
        List<Int64> FinishedTaskIds = new List<Int64>(request.Tasks.Count);
        List<RequestActionTask> FinishedTasksList = new List<RequestActionTask>(request.Tasks.Count);
        foreach (RequestActionTask CurrentTask in request.ExecutedTasks)
        {
            //Solo cargo las que estan finalizadas  y que realizo este usuario
            if (CurrentTask.UserID == UserId.Value)
            {
                FinishedTaskIds.Add(CurrentTask.TaskId);
                FinishedTasksList.Add(CurrentTask);
            }
        }

        List<Int64> UnfinishedTaskIds = new List<Int64>(request.Tasks.Count);
        foreach (RequestActionTask CurrentTask in request.Tasks)
        {
            //Solo cargo las que no estan finalizadas 
            if (!FinishedTaskIds.Contains(CurrentTask.TaskId))
                UnfinishedTaskIds.Add(CurrentTask.TaskId);
        }

        List<ITaskResult> UnfinishedTaskResults = WFTaskBussines.GetTasksByTaskIds(UnfinishedTaskIds);
        UcTasksList.Tasks = UnfinishedTaskResults;

        //Si la cantidad devuelta de tareas es menor a los tasksIds
        //intuyo que algunas tareas estan borradas del sistema.
        if (UnfinishedTaskIds.Count != UnfinishedTaskResults.Count)
        {
            List<Int64> DeletedTaskIds = new List<Int64>();
            Boolean exists;

            foreach (Int64 TaskId in UnfinishedTaskIds)
            {
                exists = false;
                foreach (ITaskResult task in UnfinishedTaskResults)
                {
                    if (task.ID == TaskId)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    DeletedTaskIds.Add(TaskId);
                    fsButtons.Visible = false;
                }

            }

            UcTasksList.DeletedTasks = DeletedTaskIds;
        }

        if (UnfinishedTaskIds.Count > 0)
            UcTasksList.Select(UnfinishedTaskIds[0]);

        UcFinishedTasks.Tasks = FinishedTasksList;
        if (FinishedTasksList.Count > 0)
            UcFinishedTasks.Select(FinishedTasksList[0].TaskId);


        UnfinishedTaskIds.Clear();
        UnfinishedTaskIds = null;

        FinishedTaskIds.Clear();
        FinishedTaskIds = null;

        LoadRules(request);
        ShowHistory(request);
    }
    /// <summary>
    /// Muestra el historial de un Request Action
    /// </summary>
    /// <param name="requestAction"></param>
    private void ShowHistory(RequestAction request)
    {
        try
        {

            lbSuccess.Visible = true;

            if (request.IsFinished)
                lbSuccess.Text = MESSAGE_ALREADY_EXECUTED;

            tblRequestInformation.Visible = true;
            tblRequestInformation.Rows.Add(BuildInformationRow("Fecha de Pedido:", request.RequestDate.ToString()));

            if (request.IsFinished && request.FinishDate != null)
                tblRequestInformation.Rows.Add(BuildInformationRow("Fecha de Finalización:", request.FinishDate.Value.ToString()));

            IUser RequestUser = UserBusiness.GetUserById(request.RequestUserId);
            tblRequestInformation.Rows.Add(BuildInformationRow("Requerido por:", RequestUser.Apellidos + " , " + RequestUser.Name));
        }
        catch (Exception ex)
        {
            ZClass.raiseerror(ex);
        }

    }
    /// <summary>
    /// Creates the buttons for the Rules 
    /// Dejo comentado la parte de la DoEnable para hacer a futuro
    /// </summary>
    /// <param name="request"></param>
    private void LoadRules(RequestAction request)
    {
        List<IWFRuleParent> Rules = WFRulesBussines.GetRulesById(request.RulesIds, Server.MapPath("~/bin"));
        List<IWFRuleParent> EnabledRules = new List<IWFRuleParent>(Rules.Count);
        List<IWFRuleParent> DisabledRules = new List<IWFRuleParent>(Rules.Count);

        DataSet permissions = null;
        Boolean IsRuleEnabled;
        Boolean useTabEnable = true;
        //Variable de tipo Preferencia de regla
        RulePreferences selectionvalue;
        Int64 stateId = WFTaskBussines.GetTaskStateByTaskId(request.Tasks[0].TaskId);
        
        foreach (IWFRuleParent rule in Rules)
        {
            IsRuleEnabled = true;
            //if (TaskResult.UserRules.ContainsKey(Rule.ID) == true)
            //{
                //List<Boolean> lstRulesEnabled = TaskResult.UserRules(Rule.ID);
                //RuleEnabled = lstRulesEnabled[0];
                //Rule.Enable = true;
            //}
            //else
            //{
                //Obtiene el estado
                rule.Enable = WFRulesBussines.GetRuleEstado(rule.ID);
                //Trace.WriteLine("DoEnable en base de datos" + rule.Enable.ToString());
            //}

            //Si la regla no fue procesada antes por la DoEnable
            //if (TaskResult.UserRules.Contains(idRule) == true)
            //{
                //Trace.WriteLine("Regla ya procesada por doenable " & idRule);

                /*Lista que en la posicion 0 guarda si esta habilitada la regla o no
                'y en la 1 si se acumula a la habilitacion de las solapas o no*/
                //List<Boolean> lstRulesEnabled = TaskResult.UserRules(idRule);
                //Si la regla esta deshabilitada, no uso los estados de los tabs
                //if (lstRulesEnabled(0) == true)
                //{
                    //Si no esta marcada la opcion de ejecucion conjunta con los tabs, no uso los estados
                    //if (lstRulesEnabled(1) == False)
                        //useTabEnable = false;
                //}
                //else
                //{
                    //useTabEnable = False;
                //}
            //}
            //else
            //{
                //Trace.WriteLine("Regla no procesada por doenable" + rule.ID);
            //}

            //Si utilizo los tabs (porq no uso la doenable o porq la ejecucion es conjunta)
            if (useTabEnable == true)
            {
                //Obtiene el valor 
                selectionvalue = WFBusiness.recoverItemSelectedThatCanBe_StateOrUserOrGroup(rule.ID,false);
                //Se Evalua el valor de la variable seleccion 
                switch (selectionvalue)
                {
                    //Caso de trabajo con Estados
                    case RulePreferences.HabilitationSelectionState:
                        //Se Obtienen los ids de estados DESHABILITADOS
                        DataTable dt = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeState);
                        //Por cada Id de estado se compara con el id de estado seleccionado, en cada de encontrar
                        //Coincidencia, se deshabilita la regla
                        foreach (DataRow r in dt.Rows)
                        {
                            if (Int64.Parse(r.ItemArray[0].ToString()) == stateId)
                            {
                                IsRuleEnabled = false;
                                break;
                            }
                        }
                        break;
                    //Caso de trabajo con Usuarios o Grupos
                    case RulePreferences.HabilitationSelectionUser:
                        //Se Obtienen los ids de USUARIOS DESHABILITADOS
                        DataTable dt2 = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser);
                        //Por cada Id de usuario se compara con el id de usuario logeado, en cada de encontrar
                        //Coincidencia, se deshabilita la regla
                        foreach (DataRow r in dt2.Rows)
                        {
                            if (Int64.Parse(r.ItemArray[0].ToString()) == UserBusiness.CurrentUser().ID)
                            {
                                IsRuleEnabled = false;
                                break;
                            }
                        }

                        //si no se deshabilito la regla por usuario se intenta deshabilitar por grupo
                        if (!IsRuleEnabled == false)
                        {
                            //Se Obtienen los ids de GRUPOS DESHABILITADOS
                            DataTable dt3 = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeGroup);
                            //Por cada Id de Grupo se recorren sus usuario y se comparan con el id de usuario logeado, en cada de encontrar
                            //Coincidencia, se deshabilita la regla
                            foreach (DataRow r in dt3.Rows)
                            {
                                foreach (User u in UserGroupBusiness.GetUsersByGroup(Int64.Parse(r.ItemArray[0].ToString())))
                                {
                                    if (u.ID == UserBusiness.CurrentUser().ID)
                                    {
                                        IsRuleEnabled = false;
                                        break;
                                    }
                                }
                                if (IsRuleEnabled == false) break;
                            }
                        }
                        break;
                    //Caso de trabajo con Usuarios/Grupos y estados
                    case RulePreferences.HabilitationSelectionBoth:
                        //Se Obtienen los ids de USUARIOS Y ESTADOS DESHABILITADOS
                        DataTable DataTable = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndUser);
                        //Por cada Id de usuario se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                        //, en cada de encontrar coincidencia, se deshabilita la regla
                        foreach (DataRow r in DataTable.Rows)
                        {
                            if (Int64.Parse(r.ItemArray[0].ToString()) == UserBusiness.CurrentUser().ID && Int64.Parse(r.ItemArray[1].ToString()) == stateId)
                            {
                                IsRuleEnabled = false;
                                break;
                            }
                        }

                        //si no se deshabilito la regla por usuario y estado se intenta deshabilitar por grupo y estado
                        if (IsRuleEnabled == true)
                        {
                            //Se Obtienen los ids de GRUPOS Y ESTADOS DESHABILITADOS
                            DataTable dt4 = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeStateAndGroup);
                            //Por cada Id de grupo se recorren sus usuarios, se comparan con el id de usuario logeado y los ids de etapa con el seleccionado en el combobox
                            //, en cada de encontrar coincidencia, se deshabilita la regla
                            foreach (DataRow r in dt4.Rows)
                            {
                                if (Int64.Parse(r.ItemArray[1].ToString()) == stateId)
                                {
                                    foreach (User u in UserGroupBusiness.GetUsersByGroup(Int64.Parse(r.ItemArray[0].ToString())))
                                    {
                                        if (u.ID == UserBusiness.CurrentUser().ID)
                                        {
                                            IsRuleEnabled = false;
                                            break;
                                        }
                                    }
                                }

                                if (IsRuleEnabled == false)
                                    break;
                            }
                        }
                        break;
                }
            }

            //permissions = WFBusiness.GetRulesPreferences(rule.ID, RuleSectionOptions.Habilitacion, RulePreferences.HabilitationTypeUser);

            //if (null != permissions && permissions.Tables.Count > 0)
            //{
            //    foreach (DataRow row in permissions.Tables[0].Rows)
            //    {
            //        if (Int64.Parse(row[0].ToString()) == UserId.Value)
            //        {

            //            IsRuleEnabled = false;
            //            break;
            //        }
            //    }
            //}



            if (IsRuleEnabled)
                EnabledRules.Add(rule);
            else
                DisabledRules.Add(rule);
        }

        if (null != permissions)
        {
            permissions.Dispose();
            permissions = null;
        }


        if (null == EnabledRules || EnabledRules.Count == 0)
            rpRules.Visible = false;
        else
        {
            rpRules.Visible = true;

            rpRules.DataSource = EnabledRules;
            rpRules.DataBind();
        }

        //if (null == DisabledRules || DisabledRules.Count == 0)
        //    rpDisabledRules.Visible = false;
        //else
        //{
        //    rpDisabledRules.Visible = true;

        //    rpDisabledRules.DataSource = DisabledRules;
        //    rpDisabledRules.DataBind();
        //}

        //if (rpRules.Controls.Count > 0 || rpDisabledRules.Controls.Count > 0)
        //    fsButtons.Visible = true;
        //else
        //    fsButtons.Visible = false;

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
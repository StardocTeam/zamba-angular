Imports Zamba.Core
Imports System.Data
Imports Zamba.Services
Imports System.Collections.Generic
Imports Zamba.Core.Enumerators
Imports System.Linq
Imports System.Web.Services
Imports System.Web.Script.Services
Imports Zamba.Core.WF.WF

Partial Public Class TaskViewer
    Inherits System.Web.UI.Page
    Implements ITaskViewer

    Private _userid As Long
    Private _user As IUser
    Private _TaskId As Integer
    Private _ITaskResult As TaskResult

#Region "Properties"
    Public Property Task_ID() As Long Implements ITaskViewer.Task_ID
        Get
            Return _TaskId
        End Get
        Set(ByVal value As Long)
            _TaskId = value
        End Set
    End Property

    Public Property TaskResult() As ITaskResult Implements ITaskViewer.TaskResult
        Get
            Return _ITaskResult
        End Get
        Set(ByVal value As ITaskResult)
            _ITaskResult = value
        End Set
    End Property
#End Region

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
        Dim zoptb As New ZOptBusiness()
        Page.Theme = zoptb.GetValue("CurrentTheme")
        zoptb = Nothing

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            hdnPostBack.Value = False

            'Actualiza el timeout
            If Page.IsPostBack = False And Not IsNothing(Session("User")) Then
                Dim user As IUser
                Dim rights As SRights
                Dim SUserPreferences As SUserPreferences
                Dim type As Int32 = 0

                Try
                    user = DirectCast(Session("User"), IUser)
                    rights = New SRights()
                    SUserPreferences = New SUserPreferences()

                    If user.WFLic Then
                        type = 1
                    Else
                        Ucm.changeLicDocToLicWF(user.ConnectionId, user.ID, user.Name, user.puesto, Int16.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), 1)
                        Zamba.Membership.MembershipHelper.CurrentUser.WFLic = True
                        DirectCast(Session("User"), IUser).WFLic = True
                        type = 1
                    End If

                    If (user.ConnectionId > 0) Then
                        rights.UpdateOrInsertActionTime(user.ID, user.Name, user.puesto, user.ConnectionId, Int32.Parse(SUserPreferences.getValue("TimeOut", Sections.UserPreferences, "30")), type)
                    Else
                        Response.Redirect("~/Views/Security/LogIn.aspx")
                    End If
                    rights = Nothing
                Catch ex As Exception
                    Zamba.AppBlock.ZException.Log(ex)
                Finally
                    user = Nothing
                    rights = Nothing
                    SUserPreferences = Nothing
                End Try
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Protected Sub Page_PreLoad(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreLoad
        Try
            If Page.Session("UserId") IsNot Nothing Then
                _user = Session("User")

                If Not String.IsNullOrEmpty(Request("TaskId")) Then

                    Task_ID = CInt(Request("TaskId"))

                    Dim STasks As New STasks()
                    TaskResult = STasks.GetTask(Task_ID)
                    STasks = Nothing

                    If Not TaskResult Is Nothing Then
                        'Se comprueba si se pulsó el botón cerrar, si lo hizo se cierra desde acá
                        'y se corta la cade de page load con los controles asignandolos a null
                        If Not String.IsNullOrEmpty(Me.Request("__EVENTTARGET")) AndAlso Me.Request("__EVENTTARGET").Contains("BtnClose") Then
                            CloseAndFinishTask()

                        Else
                            'Obtener las reglas que se podrán ejecutar por el usuario
                            TaskResult.UserRules = GetDoEnableRules()
                            ucTaskHeader.TaskResult = TaskResult
                            ucTaskDetail.TaskResult = TaskResult

                            RemoveHandler ucTaskHeader.ExecuteRule, AddressOf ExecuteRule
                            AddHandler ucTaskHeader.ExecuteRule, AddressOf ExecuteRule
                            RemoveHandler ucTaskDetail.ExecuteRule, AddressOf ExecuteRule
                            AddHandler ucTaskDetail.ExecuteRule, AddressOf ExecuteRule

                            'Ezequiel - 01/02/10 - Creo variable de ejecucion de workflow y se la paso al taskheader.
                            Dim WFExec As New WFExecution(_user)
                            AddHandler WFExec._haceralgoEvent, AddressOf UC_WFExecution.HandleWFExecutionPendingEvents
                            AddHandler UC_WFExecution.RefreshTask, AddressOf RefreshTask
                            RemoveHandler UC_WFExecution.RefreshWFTree, AddressOf GenerateWfRefreshJs
                            AddHandler UC_WFExecution.RefreshWFTree, AddressOf GenerateWfRefreshJs
                            Me.UC_WFExecution.WFExec = WFExec
                            Me.UC_WFExecution.TaskID = Task_ID

                            hdnTaskID.Value = Task_ID
                            hdnUserId.Value = Zamba.Membership.MembershipHelper.CurrentUser.ID
                        End If
                    End If
                End If
            Else
                FormsAuthentication.RedirectToLoginPage()
                Me.Controls.Remove(ucTaskDetail)
                Me.Controls.Remove(ucTaskHeader)
                ucTaskDetail = Nothing
                ucTaskHeader = Nothing
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Sub CloseAndFinishTask(Optional ByVal task As ITaskResult = Nothing)
        Try
            Me.Controls.Remove(ucTaskDetail)
            Me.Controls.Remove(ucTaskHeader)
            ucTaskDetail = Nothing
            ucTaskHeader = Nothing

            Dim taskToClose As ITaskResult
            If task Is Nothing Then
                taskToClose = Me.TaskResult
            Else
                taskToClose = task
            End If

            If TaskResult IsNot Nothing Then
                WFTaskBusiness.UnLockTask(TaskResult.TaskId)
            End If

            'Marca a la tarea como cerrada para el usuario
            Zamba.Core.WF.WF.WFTaskBusiness.CloseOpenTasksByTaskId(taskToClose.TaskId)

            'Se cierra la tarea abierta
            Dim script As String = "$(window).load(function(){hideLoading();isClosingTask=true;parent.CloseTask(" & taskToClose.TaskId & ",true);});"
            ScriptManager.RegisterClientScriptBlock(Me, Page.GetType(), "closingScript", script, True)

            'Finalización de la tarea en caso de que corresponda
            If taskToClose.AsignedToId = _user.ID Then
                Dim SUserPreferences As New SUserPreferences
                Dim SRights As New SRights

                'Si tiene el permiso de TERMINAR o el tilde de FINALIZAR_AL_CERRAR, entonces desasigna la tarea.
                If SRights.GetUserRights(Zamba.Core.ObjectTypes.WFSteps, Zamba.Core.RightsType.Terminar, CInt(taskToClose.StepId)) AndAlso _
                    CBool(SUserPreferences.getValue("CheckFinishTaskAfterClose", Sections.WorkFlow, "True")) Then
                    taskToClose.TaskState = Zamba.Core.TaskStates.Desasignada
                    taskToClose.AsignedToId = 0
                Else
                    taskToClose.TaskState = Zamba.Core.TaskStates.Asignada
                    taskToClose.AsignedToId = _user.ID
                End If
                SRights = Nothing
                SUserPreferences = Nothing

                Dim STasks As New STasks()
                STasks.Finalizar(taskToClose)
                STasks = Nothing

                Dim Results As New System.Collections.Generic.List(Of Zamba.Core.ITaskResult)
                Results.Add(taskToClose)

                For Each Rule As Zamba.Core.WFRuleParent In taskToClose.WfStep.Rules
                    If Rule.RuleType = TypesofRules.Terminar Then
                        Dim SRules As New SRules()
                        SRules.ExecuteRule(Rule, Results)
                    End If
                Next
            End If

        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        Finally
            TaskResult = Nothing
        End Try
    End Sub

    Private Sub RefreshTask(ByVal task As ITaskResult, ByVal DoPostBack As Boolean, ByRef TaskIDsToRefresh As List(Of Long))
        Try

            If TaskIDsToRefresh Is Nothing Then
                TaskIDsToRefresh = New List(Of Long)()
            End If

            If Not TaskIDsToRefresh.Contains(task.TaskId) Then
                TaskIDsToRefresh.Add(task.TaskId)
            End If

            Dim sbScript As New StringBuilder
            Dim taskIdToRefresh As Long
            Dim idsRegistereds As New List(Of Long)
            Dim tempTask As ITaskResult

            Dim max As Long = TaskIDsToRefresh.Count

            sbScript.Append("$(document).ready(function(){")
            Dim WFTB As New WF.WF.WFTaskBusiness
            For i As Long = 0 To max - 1
                taskIdToRefresh = TaskIDsToRefresh(i)

                If Not idsRegistereds.Contains(taskIdToRefresh) AndAlso taskIdToRefresh > 0 Then
                    tempTask = WFTB.GetTask(taskIdToRefresh)
                    sbScript.Append("parent.RefreshTask(")
                    sbScript.Append(taskIdToRefresh)
                    sbScript.Append(",")
                    sbScript.Append(tempTask.ID)
                    sbScript.Append(");")

                    idsRegistereds.Add(taskIdToRefresh)
                End If
            Next
            WFTB = Nothing

            sbScript.Append("});")
            TaskIDsToRefresh.Clear()

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "RefreshTaskScript", sbScript.ToString(), True)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Private Sub GenerateCloseTaskJs(ByVal taskID As Int64)
        Try
            Response.Write("<script language='javascript'> { parent.CloseTask(" & taskID & "),false;}</script>")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Actualizo el arbol de tareas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateWfRefreshJs()
        Try
            Response.Write("<script language='javascript'> { parent.RefreshGrid('tareas');}</script>")
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo el cual verifica si existe una doshowform entre las reglas.
    ''' </summary>
    ''' <param name="_rule"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckChildRules(ByVal _rule As IWFRuleParent) As IWFRuleParent
        Dim raux As WFRuleParent = Nothing
        Try
            For Each _ruleaux As Zamba.Core.IRule In _rule.ChildRules
                raux = Me.CheckChildRules(_ruleaux)
            Next
            If _rule.GetType().FullName = "Zamba.WFActivity.Regular.DoShowForm" Then
                raux = _rule
                DirectCast(_rule, IDoShowForm).RuleParentType = TypesofRules.AbrirDocumento
            End If
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
        Return raux
    End Function

    ''' <summary>
    ''' Verifica que reglas se habilitan por la DoEnableRule
    ''' </summary>
    ''' <returns>Devuelve una hashtable donde la key es el ID de la regla(long) y el valor es una lista de boolans.</returns>
    ''' <remarks></remarks>
    Private Function GetDoEnableRules() As Hashtable
        Try
            'Se comenta esto, dado que no es necesario obtener la etapa para ver sus reglas
            'Obtenemos las reglas de esa etapa
            'Dim wfstep As IWFStep = WFStepBusiness.GetStepById(TaskResult.StepId, False)

            Dim returnEnableRules = New Hashtable()
            Dim PendigEvent As New Zamba.Core.RulePendingEvents
            Dim ExecutionResult As New Zamba.Core.RuleExecutionResult
            Dim ExecutedIDs As New List(Of Int64)()
            Dim Params As New Hashtable()
            Dim PendingChildRules As New List(Of Int64)()
            'Dim tempRuleBooleanList As List(Of Boolean) = New List(Of Boolean)()
            Dim List As New Generic.List(Of ITaskResult)
            List.Add(TaskResult)

            'Se mueven estas variables del foreach, ya que se utilizara linq y for, 
            'para luego ver si se puede hacer en paralelo
            Dim WFRB As New WFRulesBusiness
            Dim RefreshRule As Boolean
            Dim sRules As New SRules

            Dim rules As List(Of IWFRuleParent) = sRules.GetCompleteHashTableRulesByStep(TaskResult.StepId)

            'For Each Rule As WFRuleParent In wfstep.Rules
            '    If (Rule.RuleType = TypesofRules.AbrirDocumento) Then
            '        If (Rule.GetType().FullName <> "Zamba.WFActivity.Regular.DoShowForm") Then
            '            RefreshRule = Rule.RefreshRule
            '            WFRB.ExecuteWebRule(Rule.ID, List, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, RefreshRule)
            '            'Else
            '            'To-do: cargar con regla DoShowForm 
            '        End If
            '    End If
            'Next
            If Not rules Is Nothing Then
                Dim rulesOpenDoc = From rule In rules _
                    Where rule.RuleType = TypesofRules.AbrirDocumento AndAlso _
                    String.Compare(rule.GetType().FullName, "Zamba.WFActivity.Regular.DoShowForm") <> 0 _
                    Select rule

                If rulesOpenDoc.Count > 0 Then
                    If TaskResult.WfStep.Rules.Count = 0 Then
                        Dim userActionRules = From rule In rules _
                            Where rule.ParentType = TypesofRules.AccionUsuario _
                            Select rule

                        TaskResult.WfStep.Rules.AddRange(userActionRules)
                    End If

                    For i As Integer = 0 To rulesOpenDoc.Count - 1
                        RefreshRule = rulesOpenDoc(i).RefreshRule
                        WFRB.ExecuteWebRule(rulesOpenDoc(i).ID, List, PendigEvent, ExecutionResult, ExecutedIDs, Params, PendingChildRules, RefreshRule, Me.UC_WFExecution.TaskIdsToRefresh)
                    Next
                End If
            End If

            Return TaskResult.UserRules
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Ejecuta las reglas desde la master
    ''' </summary>
    ''' <param name="ruleId">ID de la regla que se quiere ejecutar</param>
    ''' <param name="results">Tareas a ejecutar</param>
    ''' <remarks></remarks>
    Private Sub ExecuteRule(ByVal ruleId As Int64, ByVal results As List(Of Zamba.Core.ITaskResult))
        Try
            Dim ExecutionResult As New Zamba.Core.RuleExecutionResult
            Dim PendigEvent As New Zamba.Core.RulePendingEvents
            Dim ExecutedIDs As New List(Of Int64)()
            Dim PendingChildRules As New List(Of Int64)()
            Dim Params As New Hashtable()
            Dim RefreshRule As Boolean = False

            If UC_WFExecution.TaskIdsToRefresh Is Nothing Then
                UC_WFExecution.TaskIdsToRefresh = New List(Of Long)
            End If

            Me.UC_WFExecution.WFExec.ExecuteRule(ruleId, results, PendigEvent, ExecutionResult, ExecutedIDs, _
                    Params, PendingChildRules, RefreshRule, UC_WFExecution.TaskIdsToRefresh)
        Catch ex As Exception
            Zamba.AppBlock.ZException.Log(ex)
        End Try
    End Sub

    Public Function TaskName() As String
        Return TaskResult.Name
    End Function

    ''' <summary>
    ''' Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetZVarOptions(ByVal controlId As String, ByVal dataSourceName As String, ByVal displayMember As String, _
                                   ByVal valueMember As String, ByVal filterColumn As String, ByVal filterValue As String) As FieldOptions
        Dim formControlsController As New FormControlsController()

        Return formControlsController.GetZVarOptions(controlId, dataSourceName, displayMember, valueMember, filterColumn, filterValue)
    End Function

    ''' <summary>
    ''' Metodo para recibir la llamada de la vista y pasarla al controler de la obtencion de las opciones de un select
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetZDynamicTable(ByVal controlId As String, ByVal dataSource As String, ByVal showColumns As String, _
                                   ByVal filterFieldId As String, ByVal editableColumns As String, ByVal editableColumnsAttributes As String, _
                                   ByVal filterValues As String, ByVal additionalValidationButton As String, ByVal postAjaxFuncion As String) As FieldOptions
        Dim formControlsController As New FormControlsController()

        Return formControlsController.GetZDynamicTable(controlId, dataSource, showColumns, filterFieldId, _
                                                       editableColumns, editableColumnsAttributes, filterValues, _
                                                       additionalValidationButton, postAjaxFuncion)
    End Function

    ''' <summary>
    ''' Devuelve una lista de key value, para renderizar a las opciones de un autocomplete. ZVar aun no soportado.
    ''' </summary>
    ''' <remarks></remarks>
    <WebMethod()> _
    Public Shared Function GetAutoCompleteOptions(ByVal query As String, ByVal dataSource As String, ByVal displayMember As String, _
                                   ByVal valueMember As String, ByVal additionalFilters As String) As List(Of KeyValuePair(Of String, String))
        Dim formControlsController As New FormControlsController()

        Return formControlsController.GetAutoCompleteOptions(query, dataSource, displayMember, valueMember, additionalFilters)
    End Function
End Class

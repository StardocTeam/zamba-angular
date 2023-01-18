Imports System.Collections.Generic
Imports System.Text
Imports Zamba.Data
Imports Zamba.Outlook


Public Class RequestActionBusiness

#Region "Constantes"
    'Nombres de las columnas de los datasets que usa esta clase para construir Requests
    Private Const ROW_REQUEST_ID As String = "Id"
    Private Const ROW_REQUEST_DATE As String = "RequestDate"
    Private Const ROW_FINISH_DATE As String = "FinishDate"
    Private Const ROW_ISFINISHED As String = "IsFinished"
    Private Const ROW_REQUEST_USER_ID As String = "RequestUserId"
    Private Const ROW_RULE_ID As String = "RuleId"
    Private Const ROW_TASK_ID As String = "TaskId"
    Private Const ROW_STEP_ID As String = "StepId"
    Private Const ROW_USER_ID As String = "UserId"
    Private Const ROW_EXECUTED_RULE_ID As String = "RuleId"
    Private Const ROW_EXECUTED_USER_ID As String = "UserId"
    Private Const ROW_EXECUTION_DATE As String = "ExecutionDate"

    'Nombre de los querystrings usados para los links enviados por mail
    Private Const QUERY_STRING_REQUEST_ACTION_ID As String = "RequestActionId"
    Private Const QUERY_STRING_RULE_ID As String = "RuleId"
    Private Const QUERY_STRING_USER_ID As String = "UserId"
    Private Const KEY_SERVER_LOCATION As String = "RequestActionServerLocation"

#End Region

    ''' <summary>
    ''' Crea una instancia de un pedido a partir de un Datatable
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function BuildRequestAction(ByVal id As Int64, ByVal dr As DataRow) As RequestAction
        Dim CurrentRequestAction As RequestAction = Nothing

        CurrentRequestAction = New RequestAction()

        CurrentRequestAction.RequestActionId = id
        CurrentRequestAction.RequestDate = DateTime.Parse(dr(ROW_REQUEST_DATE).ToString())
        CurrentRequestAction.Name = "Autorizacion" 'dr(ROW_REQUEST_NAME).ToString()

        If (Not dr(ROW_FINISH_DATE) Is DBNull.Value) Then
            CurrentRequestAction.FinishDate = DateTime.Parse(dr(ROW_FINISH_DATE))
        Else
            CurrentRequestAction.FinishDate = Nothing
        End If


        CurrentRequestAction.IsFinished = IsFinished(id)
        CurrentRequestAction.RequestUserId = Int64.Parse(dr(ROW_REQUEST_USER_ID).ToString())

        CurrentRequestAction.RulesIds.AddRange(GetRulesIds(CurrentRequestAction.RequestActionId.Value))
        CurrentRequestAction.UsersIds.AddRange(GetUsersIds(CurrentRequestAction.RequestActionId.Value))
        CurrentRequestAction.Tasks.AddRange(GetRequestActionTasks(CurrentRequestAction.RequestActionId.Value))
        CurrentRequestAction.ExecutedTasks.AddRange(GetExecutedTasks(CurrentRequestAction.RequestActionId.Value))

        Return CurrentRequestAction
    End Function

    Private Shared Function BuildRequestAction(ByVal dr As DataRow) As RequestAction
        Dim RequestId As Int64 = Int64.Parse(dr(ROW_REQUEST_ID.ToString()))
        Return BuildRequestAction(requestid, dr)
    End Function

    ''' <summary>
    ''' Devuelve los ids de reglas de 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetRulesIds(ByVal requestActionId As Int64) As List(Of Int64)
        Dim Dt As DataTable = RequestActionFactory.GetRules(requestActionId)
        Dim RulesIds As New List(Of Int64)

        If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
            Dim Id As Int64

            For Each CurrentRow As DataRow In Dt.Rows
                Id = Int64.Parse(CurrentRow(ROW_RULE_ID).ToString())
                RulesIds.Add(Id)
            Next
        End If

        Dt.Dispose()

        Return RulesIds
    End Function
    ''' <summary>
    ''' Devuelve los ids de usuarios de 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetUsersIds(ByVal requestActionId As Int64) As List(Of Int64)
        Dim Dt As DataTable = RequestActionFactory.GetUsers(requestActionId)
        Dim UsersIds As New List(Of Int64)

        If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
            Dim Id As Int64

            For Each CurrentRow As DataRow In Dt.Rows
                Id = Int64.Parse(CurrentRow(ROW_USER_ID).ToString())
                UsersIds.Add(Id)
            Next
        End If

        Dt.Dispose()

        Return UsersIds
    End Function
    ''' <summary>
    ''' Devuelve las tareas ejecutadas de 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetExecutedTasks(ByVal requestActionId As Int64) As List(Of RequestActionTask)
        Dim TaskList As New List(Of RequestActionTask)

        Using Dt As DataTable = RequestActionFactory.GetExecutedTasks(requestActionId)
            If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
                Dim TaskId As Int64
                Dim StepId As Int64
                Dim RuleID As Int64
                Dim UserId As Int64
                Dim ExecutionDate As DateTime

                For Each CurrentRow As DataRow In Dt.Rows

                    TaskId = Int64.Parse(CurrentRow(ROW_TASK_ID).ToString())
                    StepId = Int64.Parse(CurrentRow(ROW_STEP_ID).ToString())
                    RuleID = Int64.Parse(CurrentRow(ROW_EXECUTED_RULE_ID).ToString())
                    UserId = Int64.Parse(CurrentRow(ROW_EXECUTED_USER_ID).ToString())
                    ExecutionDate = DateTime.Parse(CurrentRow(ROW_EXECUTION_DATE).ToString())

                    TaskList.Add(New RequestActionTask(TaskId, StepId, UserId, RuleID, ExecutionDate))

                Next
            End If
        End Using

        Return TaskList
    End Function
    ''' <summary>
    ''' Devuelve todas las tareas de 1 pedido , esten o no ejecutadas
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetRequestActionTasks(ByVal requestActionId As Int64) As List(Of RequestActionTask)
        Dim TaskList As New List(Of RequestActionTask)

        Using Dt As DataTable = RequestActionFactory.GetTasks(requestActionId)
            If Not IsNothing(Dt) AndAlso Dt.Rows.Count > 0 Then
                Dim TaskId As Int64
                Dim StepId As Int64

                For Each CurrentRow As DataRow In Dt.Rows
                    TaskId = Int64.Parse(CurrentRow(ROW_TASK_ID).ToString())
                    StepId = Int64.Parse(CurrentRow(ROW_STEP_ID).ToString())

                    TaskList.Add(New RequestActionTask(TaskId, StepId))
                Next
            End If
        End Using

        Return TaskList
    End Function


    'Public Shared Function GetRequestActions() As List(Of RequestAction)

    '    Dim DtRequestActions As DataTable = RequestActionFactory.GetRequestActions()
    '    Return Nothing
    'End Function

    ''' <summary>
    ''' Devuelve 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRequestAction(ByVal id As Int64) As RequestAction
        Dim DtRequestAction As DataTable = RequestActionFactory.GetRequestAction(id)

        Return BuildRequestAction(id, DtRequestAction.Rows(0))
    End Function

    ''' <summary>
    ''' Devuelve los pedidos asignados a 1 usuario
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRequestActionByUser(ByVal userId As Int64) As List(Of RequestAction)
        Dim dtRequests As DataTable = RequestActionFactory.GetRequestActionsByUser(userId)

        Dim Requests As New List(Of RequestAction)(dtRequests.Rows.Count)

        For Each CurrentRow As DataRow In dtRequests.Rows
            Requests.Add(BuildRequestAction(CurrentRow))
        Next

        Return Requests
    End Function


    ''' <summary>
    ''' Borra los pedidos finalizados
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ClearFinished()
        RequestActionFactory.ClearFinished()
    End Sub

    ''' <summary>
    ''' Inserta 1 pedido en la base de datos
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal request As RequestAction) As Int64
        Return RequestActionFactory.Insert(request.RequestDate, request.FinishDate, request.IsFinished, request.RequestUserId, request.RulesIds, request.UsersIds, request.TasksAndStepIds, request.Name)
    End Function

    ''' <summary>
    ''' Inserta la informacion de la ejecucion de 1 regla en 1 tarea de 1 pedido en la base de datos
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <param name="request"></param>
    ''' <remarks></remarks>
    Public Shared Sub Insert(ByVal requestActionId As Int64, ByVal request As RequestActionTask)
        RequestActionFactory.Insert(requestActionId, request.UserID, request.StepId, request.TaskId, request.RuleId, request.ExecutionDate)
    End Sub

    ''' <summary>
    ''' Actualiza 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="request"></param>
    ''' <remarks></remarks>
    Public Shared Sub Update(ByVal request As RequestAction)
        RequestActionFactory.Update(request.RequestActionId, request.RequestDate, request.FinishDate, request.IsFinished, request.RequestUserId)
    End Sub

    ' Se modifico la tabla de requestActionTasks y no tiene sentido modificar los valores insertados.
    'Public Shared Sub UpdateTask(ByVal id As Int64, ByVal task As RequestActionTask)
    '   RequestActionFactory.UpdateTask(id, task.TaskId, task.StepId, task.ExecutedRuleId, task.ExecutedUserId)
    'End Sub

    ''' <summary>
    ''' Borra 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="request"></param>
    ''' <remarks></remarks>
    Public Shared Sub Delete(ByVal request As RequestAction)
        RequestActionFactory.Delete(request.RequestActionId)
    End Sub

    ''' <summary>
    ''' Valida si un pedido se ha completado
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks>Se obtiene el total de tareas y usuarios. Si el total de ejecuciones es igual al multiplo de las tareas y los usuarios , entonces todos los usuarios eejcutaron 1 regla sobre todas las tareas del pedido.</remarks>
    Public Shared Function IsFinished(ByVal id As Int64) As Boolean
        Dim TasksCount As Int64 = RequestActionFactory.GetTasksCount(id)
        Dim UsersCount As Int64 = RequestActionFactory.GetUsersCount(id)
        Dim ExecutionsCount As Int64 = RequestActionFactory.GetExecutionsCount(id)

        Return ExecutionsCount = (TasksCount * UsersCount)

    End Function

#Region "Notify"
    ''' <summary>
    ''' Notifica a 1 usuario de 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="requestUserId"></param>
    ''' <param name="ruleIds"></param>
    ''' <param name="taskAndStepIds"></param>
    ''' <param name="message"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Public Shared Sub Notify(ByVal userId As Int64, ByVal requestUserId As Int64, ByVal ruleIds As List(Of Int64), ByVal taskAndStepIds As Dictionary(Of Int64, Int64), ByVal message As String, ByVal title As String, ByVal linkMail As String)
        Dim CurrentRequestAction As New RequestAction()
        Trace.WriteLineIf(ZTrace.IsInfo, "Notificando a UserId: " & userId)

        CurrentRequestAction.UsersIds.Add(userId)
        CurrentRequestAction.RequestUserId = requestUserId
        Trace.WriteLineIf(ZTrace.IsInfo, "RequestUserId: " & requestUserId)

        CurrentRequestAction.RulesIds.AddRange(ruleIds)

        Dim RulesIdsAndName As New Dictionary(Of Int64, String)(ruleIds.Count)
        Dim RuleName As String

        For Each CurrentRuleId As Int64 In CurrentRequestAction.RulesIds
            Trace.WriteLineIf(ZTrace.IsInfo, "Pidiendo nombre de regla para RuleId: " & CurrentRuleId)
            RuleName = WFRulesBusiness.GetRuleName(CurrentRuleId)
            Trace.WriteLineIf(ZTrace.IsInfo, "Nombre de regla para RuleId: " & CurrentRuleId & " es " & RuleName)
            RulesIdsAndName.Add(CurrentRuleId, RuleName)
        Next

        For Each CurrentItem As KeyValuePair(Of Int64, Int64) In taskAndStepIds
            CurrentRequestAction.Tasks.Add(New RequestActionTask(CurrentItem.Key, CurrentItem.Value))
        Next

        Notify(CurrentRequestAction, RulesIdsAndName, message, title, linkMail)

    End Sub

    ''' <summary>
    ''' Notifica a n usuarios de 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="usersIds"></param>
    ''' <param name="requestUserId"></param>
    ''' <param name="RuleIds"></param>
    ''' <param name="taskAndStepIds"></param>
    ''' <param name="message"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Public Shared Sub Notify(ByVal usersIds As List(Of Int64), ByVal requestUserId As Int64, ByVal RuleIds As List(Of Int64), ByVal taskAndStepIds As Dictionary(Of Int64, Int64), ByVal message As String, ByVal title As String, ByVal linkMail As String)
        Dim CurrentRequestAction As New RequestAction()
        CurrentRequestAction.UsersIds.AddRange(usersIds)
        CurrentRequestAction.RequestUserId = requestUserId
        CurrentRequestAction.RulesIds.AddRange(RuleIds)

        Dim RulesIdsAndName As New Dictionary(Of Int64, String)(RuleIds.Count)
        Dim RuleName As String

        For Each CurrentRuleId As Int64 In CurrentRequestAction.RulesIds
            RuleName = WFRulesBusiness.GetRuleName(CurrentRuleId)
            RulesIdsAndName.Add(CurrentRuleId, RuleName)
        Next

        For Each CurrentItem As KeyValuePair(Of Int64, Int64) In taskAndStepIds
            CurrentRequestAction.Tasks.Add(New RequestActionTask(CurrentItem.Key, CurrentItem.Value))
        Next

        Notify(CurrentRequestAction, RulesIdsAndName, message, title, linkMail)
    End Sub

    ''' <summary>
    ''' Notifica 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="request"></param>
    ''' <param name="message"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Public Shared Sub Notify(ByVal request As RequestAction, ByVal message As String, ByVal title As String, ByVal linkMail As String)
        Dim RulesIdsAndName As New Dictionary(Of Int64, String)(request.RulesIds.Count)
        Dim RuleName As String

        Trace.WriteLineIf(ZTrace.IsInfo, "cantidad de reglas a ejecutar que recibe la regla: " & request.RulesIds.Count)
        For Each CurrentRuleId As Int64 In request.RulesIds
            Trace.WriteLineIf(ZTrace.IsInfo, "RuleId: " & CurrentRuleId)
            RuleName = WFRulesBusiness.GetRuleName(CurrentRuleId)
            Trace.WriteLineIf(ZTrace.IsInfo, "RuleName: " & RuleName)
            RulesIdsAndName.Add(CurrentRuleId, RuleName)
        Next

        Notify(request, RulesIdsAndName, message, title, linkMail)
    End Sub

    ''' <summary>
    ''' Notifica 1 pedido 
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="request"></param>
    ''' <param name="rulesIdsAndName"></param>
    ''' <param name="message"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Private Shared Sub Notify(ByVal request As RequestAction, ByVal rulesIdsAndName As Dictionary(Of Int64, String), ByVal message As String, ByVal title As String, ByVal linkMail As String)
        Trace.WriteLineIf(ZTrace.IsInfo, "Notificando")
        request.RequestActionId = Insert(request)

        Trace.WriteLineIf(ZTrace.IsInfo, "UserBusiness.GetUsersWithMailsNames(request.UsersIds)")
        Dim Users As List(Of User) = UserBusiness.GetUsersWithMailsNames(request.UsersIds)
        Trace.WriteLineIf(ZTrace.IsInfo, "Cantidad de Usuarios con Mails " & Users.Count)

        For Each CurrentUser As User In Users
            'Se comento esta linea de codigo porque no compila
            'request.ServerLocation no existe en ZAmba.Core
            Trace.WriteLineIf(ZTrace.IsInfo, "Enviando Mail a " & CurrentUser.Name)
            Trace.WriteLineIf(ZTrace.IsInfo, "Mail del from " & CurrentUser.eMail.Mail)
            Trace.WriteLineIf(ZTrace.IsInfo, "URL de requestAction " & request.ServerLocation)
            SendMail(request.ServerLocation, request.RequestActionId, rulesIdsAndName, CurrentUser.eMail.Mail, CurrentUser.ID, message, title, linkMail)
        Next

    End Sub

    ''' <summary>
    ''' Envia 1 mail a 1 usuario notoficando 1 pedido
    '''[Update] Andres 2/5/08
    ''' </summary>
    ''' <param name="pagePath"></param>
    ''' <param name="requestActionId"></param>
    ''' <param name="ruleIdsAndName"></param>
    ''' <param name="mailAddress"></param>
    ''' <param name="userId"></param>
    ''' <param name="message"></param>
    ''' <param name="title"></param>
    ''' <remarks></remarks>
    Private Shared Sub SendMail(ByVal pagePath As String, ByVal requestActionId As Int64, ByVal ruleIdsAndName As Dictionary(Of Int64, String), ByVal mailAddress As String, ByVal userId As Int64, ByVal message As String, ByVal title As String, ByVal linkMail As String)
        'Dim RuleLinks As New List(Of String)(ruleIdsAndName.Count)

        Dim RuleLinkBuilder As New StringBuilder()
        If String.IsNullOrEmpty(linkMail) Then
            linkMail = "Ir a la Tarea..."
        End If

        '<a href=http://localhost:1431/Zamba.RequestAction.Web/Default.aspx?RequestActionId=13&RuleId=1232&UserId=1870> Búsqueda avanzada</a>
        'For Each CurrentRule As KeyValuePair(Of Int64, String) In ruleIdsAndName
        RuleLinkBuilder.Append("<a href=")
        RuleLinkBuilder.Append(Chr(34))
        RuleLinkBuilder.Append(pagePath)
        RuleLinkBuilder.Append("?")
        RuleLinkBuilder.Append(QUERY_STRING_REQUEST_ACTION_ID)
        RuleLinkBuilder.Append("=")
        RuleLinkBuilder.Append(requestActionId.ToString())
        'RuleLinkBuilder.Append("&")
        'RuleLinkBuilder.Append(QUERY_STRING_RULE_ID)
        'RuleLinkBuilder.Append("=")
        'RuleLinkBuilder.Append(CurrentRule.Key.ToString())
        RuleLinkBuilder.Append("&")
        RuleLinkBuilder.Append(QUERY_STRING_USER_ID)
        RuleLinkBuilder.Append("=")
        RuleLinkBuilder.Append(userId.ToString())
        RuleLinkBuilder.Append(Chr(34))
        RuleLinkBuilder.Append(">")

        RuleLinkBuilder.Append(linkMail)
        RuleLinkBuilder.Append("</a>")

        'RuleLinks.Add(RuleLinkBuilder.ToString())

        'RuleLinkBuilder.Remove(0, RuleLinkBuilder.Length)
        'Next

        Dim BodyBuilder As New StringBuilder()
        BodyBuilder.AppendLine(message)
        BodyBuilder.AppendLine(RuleLinkBuilder.ToString())
        'BodyBuilder.AppendLine("<table><tr>")

        'For Each CurrentRuleLink As String In RuleLinks
        '    BodyBuilder.AppendLine("<td>")
        '    BodyBuilder.AppendLine(CurrentRuleLink)
        '    BodyBuilder.AppendLine("</td>")
        'Next

        'BodyBuilder.AppendLine("</tr></table>")

        'Dim ToList As New List(Of String)(0)
        'ToList.Add(mailAddress)

        Trace.WriteLineIf(ZTrace.IsInfo, "Body: " & BodyBuilder.ToString())
        MessagesBusiness.SendMail(mailAddress, String.Empty, String.Empty, title, BodyBuilder.ToString(), True)

        'Dim Mail As New Outlook.Mail
        'Mail.SendMail(BodyBuilder.ToString(), True, title, ToList)
        BodyBuilder.Remove(0, BodyBuilder.Length)
        RuleLinkBuilder.Remove(0, RuleLinkBuilder.Length)
        'ToList.Clear()
        'RuleLinks.Clear()
    End Sub
#End Region

End Class

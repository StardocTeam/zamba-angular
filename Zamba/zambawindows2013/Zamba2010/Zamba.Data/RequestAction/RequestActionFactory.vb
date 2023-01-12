Imports System.Collections.Generic
Imports System.Data.SqlClient
Imports System.Text


Public Class RequestActionFactory

#Region "Constantes"

    Private Const SP_SELECT_REQUEST_ACTION As String = "sp_SelectRequestAction"
    Private Const SP_SELECT_REQUEST_ACTIONS As String = "sp_SelectRequestActions"
    Private Const SP_SELECT_REQUEST_ACTIONS_BY_USER As String = "sp_GetRequestActionsByUser"
    Private Const SP_SELECT_RULES As String = "sp_SelectRequestActionRules"
    Private Const SP_SELECT_USERS As String = "sp_SelectRequestActionUsers"
    Private Const SP_SELECT_TASKS As String = "sp_SelectRequestActionTasks"
    Private Const SP_SELECT_EXECUTED_TASKS As String = "sp_SelectRequestActionExecutedTasks"
    Private Const SP_SELECT_EXECUTION_COUNT As String = "sp_SelectRequestActionExecutionsCount"
    Private Const SP_SELECT_TASKS_COUNT As String = "sp_SelectRequestActionTasksCount"
    Private Const SP_SELECT_USERS_COUNT As String = "sp_GetRequestActionUsersCount"

    Private Const SP_CLEAR_FINISHED As String = "sp_ClearFinishedRequestActions"

    Private Const SP_UPDATE_REQUEST_ACTION As String = "sp_UpdateRequestAction"
    Private Const SP_UPDATE_REQUEST_ACTION_TASK As String = "sp_UpdateRequestActionTask"

    Private Const SP_DELETE_REQUEST_ACTION As String = "sp_DeleteRequestAction"
    Private Const SP_DELETE_RULES As String = "sp_DeleteRequestActionRules"
    Private Const SP_DELETE_TASKS As String = "sp_DeleteRequestActionTasks"
    Private Const SP_DELETE_USERS As String = "sp_DeleteRequestActionUsers"

    Private Const SP_INSERT_REQUEST_ACTION As String = "sp_InsertRequestAction"
    Private Const SP_INSERT_RULE As String = "sp_InsertRequestActionRule"
    Private Const SP_INSERT_TASK As String = "sp_InsertRequestActionTask"
    Private Const SP_INSERT_USER As String = "sp_InsertRequestActionUser"
    Private Const SP_INSERT_EXECUTION As String = "sp_InsertRequestActionExecution"

#End Region

    ''' <summary>
    ''' Devuelve 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetRequestAction(ByVal id As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = id
            IdParameter.ParameterName = "@id"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_REQUEST_ACTION, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT RequestDate, FinishDate, IsFinished, RequestUserId) ")
            sqlBuilder.Append("FROM RequestAction ")
            sqlBuilder.Append("Where Id = " & id)
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        'Modifique esto xq tiraba error
        If IsNothing(ds) AndAlso ds.Tables.Count = 0 Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If
    End Function


    ''' <summary>
    ''' Devuelve los pedidos asignados a 1 usuario
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetRequestActionsByUser(ByVal userId As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = userId
            IdParameter.ParameterName = "@userId"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_REQUEST_ACTIONS_BY_USER, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT Id, RequestDate, FinishDate, IsFinished, RequestUserId, RequestName ")
            sqlBuilder.Append("FROM RequestAction INNER JOIN RequestActionUsers ON RequestAction.id = ")
            sqlBuilder.Append("RequestActionUsers.RequestActionId WHERE RequestActionUsers.UserId = ")
            sqlBuilder.Append(userId.ToString())
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        If (IsNothing(ds) OrElse (ds.Tables.Count = 0)) Then
            Return Nothing
        Else
            Return (ds.Tables(0))
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Devuelve las reglas de 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetRules(ByVal requestActionId As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = requestActionId
            IdParameter.ParameterName = "@RequestActionId"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_RULES, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Select RuleId ")
            sqlBuilder.Append("FROM RequestActionRules ")
            sqlBuilder.Append("Where RequestActionId = " & requestActionId)
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        If ((IsNothing(ds)) OrElse (ds.Tables.Count = 0)) Then
            Return Nothing
        Else
            Return (ds.Tables(0))
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Devuelve los usuarios de 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetUsers(ByVal requestActionId As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = requestActionId
            IdParameter.ParameterName = "@RequestActionId"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_USERS, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT UserId ")
            sqlBuilder.Append("FROM RequestActionUsers ")
            sqlBuilder.Append("Where RequestActionId = " & requestActionId)
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        If ((IsNothing(ds)) OrElse (ds.Tables.Count = 0)) Then
            Return Nothing
        Else
            Return (ds.Tables(0))
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Devuelve las tareas de 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetTasks(ByVal requestActionId As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = requestActionId
            IdParameter.ParameterName = "@RequestActionId"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_TASKS, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT TaskId, StepId ")
            sqlBuilder.Append("FROM RequestActionTasks ")
            sqlBuilder.Append("Where RequestActionId = " & requestActionId)
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        If ((IsNothing(ds)) OrElse (ds.Tables.Count = 0)) Then
            Return Nothing
        Else
            Return (ds.Tables(0))
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Devuelve las tareas ejecutadas de 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetExecutedTasks(ByVal requestActionId As Int64) As DataTable

        Dim ds As New DataSet

        If (Server.isSQLServer) Then

            Dim IdParameter As IDbDataParameter = New SqlParameter()
            IdParameter.DbType = DbType.Int32
            IdParameter.Value = requestActionId
            IdParameter.ParameterName = "@RequestActionId"

            Dim Parameters As IDbDataParameter() = {IdParameter}
            ds = Server.Con.ExecuteDataset(CommandType.StoredProcedure, SP_SELECT_EXECUTED_TASKS, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT UserId, StepId, TaskId, RuleId, ExecutionDate ")
            sqlBuilder.Append("FROM RequestActionExecution ")
            sqlBuilder.Append("Where RequestActionId = " & requestActionId)
            ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

        If (IsNothing(ds) OrElse (ds.Tables.Count = 0)) Then
            Return Nothing
        Else
            Return ds.Tables(0)
        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' Borra los pedidos finalizados
    ''' </summary>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Sub ClearFinished()

        If (Server.isSQLServer) Then

            Server.Con.ExecuteNonQuery(CommandType.StoredProcedure, SP_CLEAR_FINISHED)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Delete FROM RequestAction ")
            sqlBuilder.Append("Where isfinished = 1")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Sub

#Region "Insert"

    ''' <summary>
    ''' Inserta 1 pedido
    ''' </summary>
    ''' <param name="requestDate"></param>
    ''' <param name="finishDate"></param>
    ''' <param name="isFinished"></param>
    ''' <param name="requestUserId"></param>
    ''' <param name="ruleIds"></param>
    ''' <param name="userIds"></param>
    ''' <param name="tasksAndStepIds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Insert(ByVal requestDate As Date, ByVal finishDate As Nullable(Of Date), ByVal isFinished As Boolean, ByVal requestUserId As Int64, ByVal ruleIds As List(Of Int64), ByVal userIds As List(Of Int64), ByVal tasksAndStepIds As Dictionary(Of Int64, Int64), ByVal requestName As String) As Int64

        Dim RequestActionId As Int64 = InsertRequestAction(requestDate, finishDate, isFinished, requestUserId, requestName)

        InsertRules(RequestActionId, ruleIds)
        InsertTasks(RequestActionId, tasksAndStepIds)
        InsertUsers(RequestActionId, userIds)

        Return RequestActionId

    End Function

    ''' <summary>
    ''' Inserta 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <param name="userId"></param>
    ''' <param name="stepId"></param>
    ''' <param name="taskId"></param>
    ''' <param name="ruleId"></param>
    ''' <param name="executionDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function Insert(ByVal requestActionId As Int64, ByVal userId As Int64, ByVal stepId As Int64, ByVal taskId As Int64, ByVal ruleId As Int64, ByVal executionDate As DateTime)

        If (Server.isSQLServer) Then

            Dim paramenters As Object() = {requestActionId, userId, stepId, taskId, ruleId, executionDate}
            Server.Con.ExecuteNonQuery(SP_INSERT_EXECUTION, paramenters)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("INSERT INTO RequestActionExecution(requestActionId, userId, stepId, taskId, ruleId, executionDate) VALUES (")
            sqlBuilder.Append(requestActionId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append(userId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append(stepId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append(taskId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append(ruleId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append(executionDate)
            sqlBuilder.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Function

    ''' <summary>
    ''' Inserta 1 pedido
    ''' </summary>
    ''' <param name="requestDate"></param>
    ''' <param name="finishDate"></param>
    ''' <param name="isFinished"></param>
    ''' <param name="requestUserId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    '''                 25/09/2008  Modified    Corrección consulta Oracle
    ''' </history>
    Private Shared Function InsertRequestAction(ByVal requestDate As Date, ByVal finishDate As Nullable(Of Date), ByVal isFinished As Boolean, ByVal requestUserId As Int64, ByVal requestName As String) As Int64

        ' Si el servidor es SQL
        If (Server.isSQLServer) Then

            Dim Parameters(4) As Object

            Parameters.SetValue(requestDate, 0)

            If finishDate.HasValue() Then
                Parameters.SetValue(finishDate, 1)
            Else
                Parameters.SetValue(DBNull.Value, 1)
            End If

            Parameters.SetValue(isFinished, 2)
            Parameters.SetValue(requestUserId, 3)
            Parameters.SetValue(requestName, 4)

            Dim InsertedId As Object = Server.Con.ExecuteScalar(SP_INSERT_REQUEST_ACTION, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

            Return Int64.Parse(InsertedId.ToString())

            ' Sino, si es Oracle
        Else

            Dim id As Object = Server.Con.ExecuteScalar(CommandType.Text, "SELECT MAX(Id) FROM RequestAction")

            If Not (IsDBNull(id)) Then
                id = id + 1
            Else
                id = 1
            End If

            Dim sqlBuilder As New StringBuilder

            sqlBuilder.Append("INSERT INTO RequestAction (Id,requestDate, finishDate, isFinished, requestUserId, requestName) VALUES (")
            sqlBuilder.Append(CType(id, Integer))
            sqlBuilder.Append(", " & Server.Con.ConvertDateTime(requestDate))
            sqlBuilder.Append(", ")

            If finishDate.HasValue() Then
                sqlBuilder.Append(Server.Con.ConvertDateTime(finishDate.Value))
            Else
                sqlBuilder.Append("''")
            End If

            sqlBuilder.Append(", ")

            If (isFinished) Then
                sqlBuilder.Append("1")
            Else
                sqlBuilder.Append("0")
            End If

            sqlBuilder.Append(", ")
            sqlBuilder.Append(requestUserId)
            sqlBuilder.Append(", ")
            sqlBuilder.Append("'" & requestName & "'")
            sqlBuilder.Append(")")
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

            Return Int64.Parse(id.ToString())

        End If

    End Function

    ''' <summary>
    ''' Inserta reglas en 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <param name="ruleIds"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub InsertRules(ByVal requestActionId As Int64, ByVal ruleIds As List(Of Int64))

        For Each CurrentRuleId As Int64 In ruleIds

            If (Server.isSQLServer) Then

                Dim paramenters As Object() = {requestActionId, CurrentRuleId}
                Server.Con.ExecuteNonQuery(SP_INSERT_RULE, paramenters)

            Else

                Dim sqlBuilder As New StringBuilder
                sqlBuilder.Append("INSERT INTO RequestActionRules(requestActionId, RuleId) VALUES (")
                sqlBuilder.Append(requestActionId)
                sqlBuilder.Append(", ")
                sqlBuilder.Append(CurrentRuleId)
                sqlBuilder.Append(")")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                sqlBuilder = Nothing

            End If

        Next

    End Sub

    ''' <summary>
    ''' Inserta tareas en 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <param name="tasksAndStepIds"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub InsertTasks(ByVal requestActionId As Int64, ByVal tasksAndStepIds As Dictionary(Of Int64, Int64))

        For Each CurrentItem As KeyValuePair(Of Int64, Int64) In tasksAndStepIds

            If (Server.isSQLServer) Then

                Dim parameters As Object() = {requestActionId, CurrentItem.Key, CurrentItem.Value}
                Server.Con.ExecuteNonQuery(SP_INSERT_TASK, parameters)
                Array.Clear(parameters, 0, parameters.Length)

            Else

                Dim sqlBuilder As New StringBuilder
                sqlBuilder.Append("INSERT INTO RequestActionTasks(requestActionId, TaskId, StepId) VALUES (")
                sqlBuilder.Append(requestActionId)
                sqlBuilder.Append(", ")
                sqlBuilder.Append(CurrentItem.Key)
                sqlBuilder.Append(", ")
                sqlBuilder.Append(CurrentItem.Value)
                sqlBuilder.Append(")")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                sqlBuilder = Nothing

            End If

        Next

    End Sub

    ''' <summary>
    ''' Inserta usuarios en 1 pedido
    ''' </summary>
    ''' <param name="requestActionId"></param>
    ''' <param name="userIds"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub InsertUsers(ByVal requestActionId As Int64, ByVal userIds As List(Of Int64))

        For Each CurrentUserId As Int64 In userIds

            If (Server.isSQLServer) Then

                Dim paramenters As Object() = {requestActionId, CurrentUserId}
                Server.Con.ExecuteNonQuery(SP_INSERT_USER, paramenters)

            Else

                Dim sqlBuilder As New StringBuilder
                sqlBuilder.Append("INSERT INTO RequestActionUsers(requestActionId, UserId) VALUES (")
                sqlBuilder.Append(requestActionId)
                sqlBuilder.Append(", ")
                sqlBuilder.Append(CurrentUserId)
                sqlBuilder.Append(")")
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
                sqlBuilder = Nothing

            End If

        Next

    End Sub

#End Region

#Region "Delete"
    ''' <summary>
    ''' Borra 1 pedido 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Sub Delete(ByVal id As Int64)

        DeleteRules(id)
        DeleteTasks(id)
        DeleteUsers(id)

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Server.Con.ExecuteNonQuery(SP_DELETE_REQUEST_ACTION, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Delete FROM RequestAction ")
            sqlBuilder.Append("Where Id = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Borra las reglas de 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub DeleteRules(ByVal id As Int64)

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Server.Con.ExecuteNonQuery(SP_DELETE_RULES, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Delete FROM RequestActionRules ")
            sqlBuilder.Append("Where RequestActionId = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Borra las tareas de 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub DeleteTasks(ByVal id As Int64)

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Server.Con.ExecuteNonQuery(SP_DELETE_TASKS, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Delete FROM RequestActionTasks ")
            sqlBuilder.Append("Where RequestActionId = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Sub

    ''' <summary>
    ''' Borra los usuarios de 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Private Shared Sub DeleteUsers(ByVal id As Int64)

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Server.Con.ExecuteNonQuery(SP_DELETE_USERS, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("Delete FROM RequestActionUsers ")
            sqlBuilder.Append("Where RequestActionId = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Sub

#End Region

    ''' <summary>
    ''' Modifica 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="requestDate"></param>
    ''' <param name="finishDate"></param>
    ''' <param name="isFinished"></param>
    ''' <param name="requestUserId"></param>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    '''                 26/09/2008  Modified    Correción consulta Oracle
    ''' </history>
    Public Shared Sub Update(ByVal id As Int64, ByVal requestDate As Date, ByVal finishDate As Date, ByVal isFinished As Boolean, ByVal requestUserId As Int64)

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id, requestDate, finishDate, isFinished, requestUserId}
            Server.Con.ExecuteNonQuery(SP_UPDATE_REQUEST_ACTION, Parameters)
            Array.Clear(Parameters, 0, Parameters.Length)

        Else

            Dim sqlBuilder As New StringBuilder

            sqlBuilder.Append("Update RequestAction SET ")
            sqlBuilder.Append("RequestDate = " & Server.Con.ConvertDateTime(requestDate))
            sqlBuilder.Append("FinishDate = " & Server.Con.ConvertDateTime(finishDate))

            If (isFinished) Then
                sqlBuilder.Append("IsFinished = 1, ")
            Else
                sqlBuilder.Append("IsFinished = 0, ")
            End If

            sqlBuilder.Append("RequestUserId = " & requestUserId & " ")
            sqlBuilder.Append("Where Id = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            sqlBuilder = Nothing

        End If

    End Sub

    'Se modifico la tabla de requestActionTasks y no tiene sentido modificar los valores insertados.
    'Public Shared Sub UpdateTask(ByVal id As Int64, ByVal taskId As Int64, ByVal stepId As Int64, ByVal executedRuleId As Nullable(Of Int64), ByVal executedUserId As Nullable(Of Int64))
    '    Dim parameters(4) As Object

    '    parameters.SetValue(id, 0)
    '    parameters.SetValue(taskId, 1)
    '    parameters.SetValue(stepId, 2)

    '    If (executedRuleId.HasValue) Then
    '        parameters.SetValue(executedRuleId.Value, 3)
    '    Else
    '        parameters.SetValue(DBNull.Value, 3)
    '    End If

    '    If (executedUserId.HasValue) Then
    '        parameters.SetValue(executedUserId.Value, 4)
    '    Else
    '        parameters.SetValue(DBNull.Value, 4)
    '    End If

    '    Server.Con.ExecuteNonQuery(SP_UPDATE_REQUEST_ACTION_TASK, parameters)

    '    Array.Clear(parameters, 0, parameters.Length)
    'End Sub

    ''' <summary>
    ''' Devuelve la cantidad de ejecuciones de reglas sobre tareas en 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetExecutionsCount(ByVal id As Int64) As Int64

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Return Int64.Parse(Server.Con.ExecuteScalar(SP_SELECT_EXECUTION_COUNT, Parameters).ToString())

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT COUNT(RequestActionId) as Executions ")
            sqlBuilder.Append("FROM RequestActionExecution ")
            sqlBuilder.Append("WHERE Requestactionid = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Function

    ''' <summary>
    ''' Devuelve la cantidad de usuarios de 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetUsersCount(ByVal id As Int64) As Int64

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Return Int64.Parse(Server.Con.ExecuteScalar(SP_SELECT_USERS_COUNT, Parameters).ToString())

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT COUNT(Userid) as UsersCount ")
            sqlBuilder.Append("FROM RequestActionUsers ")
            sqlBuilder.Append("WHERE RequestActionId = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Function

    ''' <summary>
    ''' Devuelve la cantidad de tareas de 1 pedido
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' 	[Gaston]	31/07/2008	Modified    Funcionalidad para Oracle
    ''' </history>
    Public Shared Function GetTasksCount(ByVal id As Int64) As Int64

        If (Server.isSQLServer) Then

            Dim Parameters As Object() = {id}
            Return Int64.Parse(Server.Con.ExecuteScalar(SP_SELECT_TASKS_COUNT, Parameters).ToString())

        Else

            Dim sqlBuilder As New StringBuilder
            sqlBuilder.Append("SELECT COUNT(TaskId) as TaskCount ")
            sqlBuilder.Append("FROM RequestActionTasks ")
            sqlBuilder.Append("WHERE RequestActionId = " & id)
            Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())
            sqlBuilder = Nothing

        End If

    End Function

End Class
Imports System.Collections.Generic
Imports Zamba.Data

Public Class NewsBusiness


    Dim NF As New Newsfactory
    ''' <summary>
    ''' Guarda la notificacion
    ''' </summary>
    ''' <param name="NewsID"></param>
    ''' <param name="DocID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="comment"></param>
    ''' <remarks></remarks>
    Public Sub SaveNews(ByVal NewsID As Int64, ByVal DocID As Int64, ByVal DocTypeID As Int64, ByVal comment As String, UserId As Int64, details As String)
        Try
            NF.SaveNews(NewsID, DocID, DocTypeID, comment, UserId, details)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Marca la novedad como leida
    ''' </summary>
    ''' <param name="currentUser">Usuario</param>
    ''' <param name="DocTypeID">ID del entidad</param>
    ''' <param name="resultID">Doc ID</param>
    ''' <remarks></remarks>
    Public Sub SetRead(ByVal NewsID As Int64)
        Dim users As String = Membership.MembershipHelper.CurrentUser.ID & ","

        For Each group As UserGroup In Membership.MembershipHelper.CurrentUser.Groups
            users &= group.ID & ","
        Next

        users = users.Remove(users.Length - 1)
        NF.SetRead(users, NewsID)
    End Sub

    Public Function ShowNews(ByVal userid As Long) As DataSet
        Return NF.GetAllNewsSummary(userid)
    End Function

    Public Function GetNewsSummary(userid As Long, searchType As NewsSearchType) As List(Of News)

        Dim news As New List(Of News)

        Dim newsDataset As DataSet = New Newsfactory().GetAllNewsSummary(userid, searchType)

        If newsDataset IsNot Nothing AndAlso newsDataset.Tables(0) IsNot Nothing Then

            For Each row As DataRow In newsDataset.Tables(0).Rows

                news.Add(New News(row("newsid"), row("doctypeid"), row("docid"), row("value").ToString(), row("crdate").ToString(), row("isread")))

            Next

        End If

        Return news

    End Function

    Public Function GetNews(userId As Long, resultId As Long, AsociatedEntityIds As String) As DataSet
        Return NF.GetNews(userId, resultId, AsociatedEntityIds)
    End Function


#Region "WebParts"

    Public Function GetTasksToExpireGroupByStep(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Return NF.GetTasksToExpireGroupByStep(workflowid, FromHours, ToHours)
    End Function

    Public Function GetTasksToExpireGroupByUser(ByVal workflowid As Int64, ByVal FromHours As Int32, ByVal ToHours As Int32) As DataSet
        Return NF.GetTasksToExpireGroupByUser(workflowid, FromHours, ToHours)
    End Function

    Public Function GetExpiredTasksGroupByUser(ByVal workflowid As Int64) As DataSet
        Return NF.GetExpiredTasksGroupByUser(workflowid)
    End Function

    Public Function GetExpiredTasksGroupByStep(ByVal workflowid As Int64) As DataSet
        Return NF.GetExpiredTasksGroupByStep(workflowid)
    End Function


    Public Function GetTasksBalanceGroupByWorkflow(ByVal workflowid As Int64) As DataSet
        Return NF.GetTasksBalanceGroupByWorkflow(workflowid)
    End Function

    Public Function GetTasksBalanceGroupByStep(ByVal stepid As Int64) As DataSet
        Return NF.GetTasksBalanceGroupByStep(stepid)
    End Function


    Public Function GetMyTasks(ByVal userId As Int64) As DataSet
        Return NF.GetMyTasks(userId)
    End Function
    Public Function GetMyTasksCount(userId As Long) As Long
        Return NF.GetMyTasksCount(userId)
    End Function

    Public Function GetRecentTasks(ByVal userId As Int64) As List(Of TaskDTO)
        Dim newsDataset As DataSet = NF.GetRecentTasks(userId)

        Dim Tasks As New List(Of TaskDTO)
        If newsDataset IsNot Nothing AndAlso newsDataset.Tables(0) IsNot Nothing Then
            Dim cultureInfo As New Globalization.CultureInfo("es-AR")
            For Each row As DataRow In newsDataset.Tables(0).Rows
                Try

                    Dim Task_id As Long
                    Dim doctypeid As Long
                    Dim tarea As String = String.Empty
                    Dim Etapa As String = String.Empty
                    Dim Ingreso As New System.DateTime()
                    Dim Vencimiento As New System.DateTime()
                    If IsDBNull(row("Task_id")) = False Then
                        Task_id = row("Task_id")
                    End If
                    If IsDBNull(row("DOC_TYPE_ID")) = False Then
                        doctypeid = row("DOC_TYPE_ID")
                    End If
                    If IsDBNull(row("tarea")) = False Then
                        tarea = row("tarea")
                    End If
                    If IsDBNull(row("Etapa")) = False Then
                        Etapa = row("Etapa")
                    End If
                    If IsDBNull(row("Ingreso")) = False Then
                        Try
                            Ingreso = DateTime.Parse(row("Ingreso"), cultureInfo)
                        Catch ex As Exception

                        End Try
                    End If
                    If IsDBNull(row("Vencimiento")) = False Then
                        Try
                            Vencimiento = DateTime.Parse(row("Vencimiento"), cultureInfo)
                        Catch ex As Exception

                        End Try
                    End If
                    Tasks.Add(New TaskDTO(tarea, Task_id, row("doc_id"), doctypeid, DateTime.Parse(row("Fecha"), cultureInfo), Etapa, row("Asignado"), Ingreso, Vencimiento))
                Catch ex As Exception
                    ZTrace.WriteLineIf(ZTrace.IsError, ex.ToString())
                End Try
            Next
        End If
        Tasks = Tasks.OrderByDescending(Function(t) t.Fecha).ToList()
        Return Tasks
    End Function
    Public Function GetRecentTasksCount(userId As Long) As Long
        Return NF.GetRecentTasksCount(userId)
    End Function
    Public Function GetAsignedTasksCountsGroupByUser(ByVal workflowid As Int64) As DataSet
        Return NF.GetAsignedTasksCountsGroupByUser(workflowid)
    End Function

    Public Function GetAsignedTasksCountsGroupByStep(ByVal workflowid As Int64) As DataSet
        Return NF.GetAsignedTasksCountsGroupByStep(workflowid)
    End Function

    Public Function GetTaskConsumedMinutesByWorkflowGroupByUsers(ByVal workflowid As Int64) As DataSet
        Return NF.GetTaskConsumedMinutesByWorkflowGroupByUsers(workflowid)
    End Function

    Public Function GetTaskConsumedMinutesByStepGroupByUsers(ByVal stepid As Int64) As DataSet
        Return NF.GetTaskConsumedMinutesByStepGroupByUsers(stepid)
    End Function

    Public Function GetTasksAverageTimeInSteps(ByVal workflowid As Int64) As Hashtable
        Return NF.GetTasksAverageTimeInSteps(workflowid)
    End Function

    Public Function GetTasksAverageTimeByStep(ByVal stepid As Int64) As Hashtable
        Return NF.GetTasksAverageTimeByStep(stepid)
    End Function

#End Region
End Class

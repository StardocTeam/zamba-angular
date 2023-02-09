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
    Public Sub SetRead(ByVal DocTypeID As Int64, ByVal docID As Int64)
        Dim users As String = Membership.MembershipHelper.CurrentUser.ID & ","

        For Each group As UserGroup In Membership.MembershipHelper.CurrentUser.Groups
            users &= group.ID & ","
        Next

        users = users.Remove(users.Length - 1)
        NF.SetRead(users, DocTypeID, docID)
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
End Class

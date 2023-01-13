Imports System.Collections.Generic
Imports Zamba.Data

Public Class ZFeedBusiness
    ''' <summary>
    ''' Obtiene todas las novedades del usuario
    ''' La cantidadse configura por la variable de Zopt FeedCount
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFeeds(ByVal userId As Long) As List(Of ZFeed)
        Dim feedFactory As New ZFeedFactory()
        Dim zoptB As New ZOptBusiness()
        Dim zoptFeedCount As String = zoptB.GetValue("FeedCount")
        Dim feedCount As Long

        If String.IsNullOrEmpty(zoptFeedCount) Then
            feedCount = 10
        Else
            feedCount = Integer.Parse(zoptFeedCount)
        End If

        Dim dt As DataTable = feedFactory.GetFeeds(userId, feedCount)

        'Se inicia el hilo para el borrado de novedades viejas
        Dim cleaningThreadStart As New Threading.ParameterizedThreadStart(AddressOf CleanOldFeeds)
        Dim claningThread As New Threading.Thread(cleaningThreadStart)
        claningThread.Start(userId)

        If dt Is Nothing Then
            Return Nothing
        Else
            Return Me.GetFeedsFromTable(dt)
        End If

        feedFactory = Nothing
        zoptB = Nothing
    End Function

    ''' <summary>
    ''' Borra las novedades viejas
    ''' Se consideran como novedades viejas aquellas que tengan Status 1 y haya pasado mas dias que el intervalo seteado en zopt(Z
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <remarks></remarks>
    Public Sub CleanOldFeeds(ByVal userId As Long)
        Dim feedFactory As New ZFeedFactory()
        feedFactory.CleanOldFeeds(userId)
        feedFactory = Nothing
    End Sub

    ''' <summary>
    ''' Convierte un DataTable con los feeds en una lista de IZFeed
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFeedsFromTable(ByVal dt As DataTable) As List(Of ZFeed)
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            ZClass.raiseerror(New Exception("No se obtuvo source para convertir Feeds"))
            Return Nothing
        End If

        Dim feedList As New List(Of ZFeed)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            feedList.Add(New ZFeed(dt(i)("NewsId"), dt(i)("Value"), dt(i)("crdate"), FeedTypes.News, dt(i)("Status"), dt(i)("Value")))
        Next

        Return feedList
    End Function
End Class
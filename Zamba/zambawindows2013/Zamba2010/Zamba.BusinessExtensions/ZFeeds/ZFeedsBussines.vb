Imports Zamba.Data

Public Class ZFeedsBussines

    ''' <summary>
    ''' Se guarda la configuración de las novedades.
    ''' </summary>
    ''' <param name="refreshInterval"></param>
    ''' <param name="linesCount"></param>
    ''' <param name="feedCount"></param>
    ''' <param name="cleanInterval"></param>
    ''' <remarks></remarks>
    Public Sub SaveConfiguration(ByVal refreshInterval As Integer, ByVal linesCount As Integer, ByVal feedCount As Integer, ByVal cleanInterval As Integer)
        Try
            Dim feedsFactory As New ZFeedsFactory()
            feedsFactory.SaveConfiguration(refreshInterval, linesCount, feedCount, cleanInterval)
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

    End Sub

    Public Function LoadConfiguration() As Hashtable
        Try
            Dim feedsFactory As New ZFeedsFactory()
            Dim config As DataTable = feedsFactory.LoadConfiguration()
            Dim list As New Hashtable()

            FillParameters(list, config)

            FillTheRemainsWithDefaultParameters(list)

            Return list
            list = Nothing
            config.Dispose()
            config = Nothing
            feedsFactory = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Se llena la lista con los valores que se encuentran en la base
    ''' </summary>
    ''' <param name="list"></param>
    ''' <param name="config"></param>
    ''' <remarks></remarks>
    Private Sub FillParameters(ByRef list As Hashtable, ByVal config As DataTable)
        For Each opt As DataRow In config.Rows
            If opt(1) IsNot Nothing AndAlso Not IsDBNull(opt(1)) Then
                Select Case opt(0)
                    Case "FeedCount"
                        list.Add("FeedCount", opt(1))
                    Case "FeedLinesCount"
                        list.Add("FeedLinesCount", opt(1))
                    Case "FeedRefreshInterval"
                        list.Add("FeedRefreshInterval", opt(1))
                    Case "ZFeedCleanInterval"
                        list.Add("ZFeedCleanInterval", opt(1))
                End Select
            End If
        Next
    End Sub

    ''' <summary>
    ''' Se llena el listado con los valores restantes
    ''' </summary>
    ''' <param name="list"></param>
    ''' <remarks></remarks>
    Private Sub FillTheRemainsWithDefaultParameters(ByRef list As Hashtable)
        If Not list.ContainsKey("FeedCount") Then list.Add("FeedCount", 10)
        If Not list.ContainsKey("FeedLinesCount") Then list.Add("FeedLinesCount", 6)
        If Not list.ContainsKey("FeedRefreshInterval") Then list.Add("FeedRefreshInterval", 5000)
        If Not list.ContainsKey("ZFeedCleanInterval") Then list.Add("ZFeedCleanInterval", 1)
    End Sub
End Class

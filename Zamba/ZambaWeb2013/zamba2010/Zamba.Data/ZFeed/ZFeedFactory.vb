Public Class ZFeedFactory
    ''' <summary>
    ''' Obtiene la cantidad especificada de feeds para el usuario especificado
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="feedCount"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFeeds(ByVal userId As Long, ByVal feedCount As Integer) As DataTable
        Dim sQuery As String = "zsp_feed_100_getNewsFeeds"
        'Parametros: @feedCount,@userid
        Dim ds As DataSet = Server.Con.ExecuteDataset(sQuery, New Object() {feedCount, userId})

        If ds IsNot Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0)
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Ejecuta el stored para la limpieza de feeds viejos
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <remarks></remarks>
    Public Sub CleanOldFeeds(ByVal userId As Long)
        Dim sQuery As String = "zsp_feed_100_cleanOldFeeds"
        'Parametros: @userid
        Server.Con.ExecuteNonQuery(sQuery, New Object() {userId})
    End Sub
End Class

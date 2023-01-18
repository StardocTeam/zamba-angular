Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Servers.Server
Imports Zamba.Core.ZClass


Public Class ZFeedsFactory
    Public Sub SaveConfiguration(ByVal refreshInterval As Integer, ByVal linesCount As Integer, ByVal feedCount As Integer, ByVal cleanInterval As Integer)
        Try
            Dim param As Object() = {refreshInterval, linesCount, feedCount, cleanInterval}
            Server.Con.ExecuteDataset("zsp_100_zfeeds_SaveConfiguration", param)
            param = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function LoadConfiguration() As DataTable
        Return Server.Con.ExecuteDataset("zsp_100_zfeeds_LoadConfiguration").Tables(0)
    End Function
End Class

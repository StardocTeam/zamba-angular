
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub scrpManager_AsyncPostBackError(ByVal sender As Object, ByVal e As AsyncPostBackErrorEventArgs)
        Try
        Catch
        End Try
    End Sub
End Class
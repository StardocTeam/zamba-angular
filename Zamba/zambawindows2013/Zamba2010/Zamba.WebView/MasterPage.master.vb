
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Title = "Zamba - Servicios de Informacion de " & ClientName()
        Me.LblTitle.Text = "CONSULTA DE DOCUMENTOS DE " & ClientName().ToUpper()
    End Sub

    'Protected Sub scrpManager_AsyncPostBackError(ByVal sender As Object, ByVal e As AsyncPostBackErrorEventArgs)
    '    Try
    '    Catch
    '    End Try
    'End Sub

    Private Function ClientName() As String
        Return System.Web.Configuration.WebConfigurationManager.AppSettings("ClientName")
    End Function
End Class
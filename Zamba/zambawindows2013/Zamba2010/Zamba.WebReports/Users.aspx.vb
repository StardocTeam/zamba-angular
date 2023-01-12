Imports Zamba.Core
Imports CrystalDecisions.CrystalReports.Engine
Imports Zamba.Data.UserFactory
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Usuarios"
        Try
            Dim DatasetUsuarios As DsUsers = GetUsers()
            Dim myReportDocument As New ReportDocument
            myReportDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\Users.rpt")
            myReportDocument.Database.Tables(0).SetDataSource(DatasetUsuarios)
            CrystalReportViewer1.ReportSource = myReportDocument
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

Imports Zamba.Core
Imports CrystalDecisions.CrystalReports.Engine
Imports Zamba.WFBusiness.WFBusiness



Partial Class DocsByUser
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Documentos por Usuario"
        Try
            Dim ds As DsDocsByUser = GetDocsByUser()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\DocsByUser.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

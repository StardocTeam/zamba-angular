Imports CrystalDecisions.CrystalReports.Engine
Imports Zamba.WFBusiness.WFBusiness
Imports Zamba.Core

Partial Class DocumentosDemorados
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Documentos Demorados"
        Try
            Dim ds As DsDelayedDocs = Zamba.WFBusiness.WFBusiness.GetDelayedDocs()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\DelayedDocs.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

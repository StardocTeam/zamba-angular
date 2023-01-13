Imports Zamba.WFBusiness.WFBusiness
Imports CrystalDecisions.CrystalReports.Engine
Imports Zamba.Core

Partial Class DocumentosPorEtapas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Documentos por Etapa"
        Try
            Dim ds As DsDocsByStep = Zamba.WFBusiness.GetDocsByStep()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\DocsByStep.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

Imports Zamba.WFBusiness.WFBusiness
Imports Zamba.Core
Imports CrystalDecisions.CrystalReports.Engine
Partial Class WorkflowsPorDocType
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Wokflows por Documentos"
        Try
            Dim ds As DsWFsByDocType = GetWFsByDocType()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\WFsByDocType.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

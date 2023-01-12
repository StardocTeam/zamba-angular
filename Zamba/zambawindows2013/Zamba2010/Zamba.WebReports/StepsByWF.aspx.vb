Imports Zamba.Core
Imports Zamba.WFBusiness.WFBusiness
Imports CrystalDecisions.CrystalReports.Engine

Partial Class StepsByWF
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Etapas por Workflow"
        Try
            Dim ds As DStepsByWorkflow = GetStepsByWF()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\StepsByWF.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

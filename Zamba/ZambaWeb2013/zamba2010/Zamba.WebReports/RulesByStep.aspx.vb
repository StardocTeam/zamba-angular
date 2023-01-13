Imports CrystalDecisions.CrystalReports.Engine
Imports Zamba.WFBusiness.WFBusiness
Imports Zamba.Core

Partial Class RulesByStep
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Title = "Reglas por Etapa"
        Try
            Dim ds As DsRulesByStep = GetRulesByStep()
            Dim reporte As New ReportDocument
            reporte.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\RulesByStep.rpt")
            reporte.Database.Tables(0).SetDataSource(ds)
            CrystalReportViewer1.ReportSource = reporte
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

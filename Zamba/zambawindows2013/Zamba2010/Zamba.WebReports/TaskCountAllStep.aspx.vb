Imports Zamba.Data.WFStepsFactory
Imports Zamba.Core
Imports CrystalDecisions.CrystalReports.Engine
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Ds As DsTasksUsers = GetTasksCountAllSteps()
            Dim myReportDocument As New ReportDocument
            myReportDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\TaskCountAllSteps.rpt")
            myReportDocument.Database.Tables(0).SetDataSource(Ds)
            CrystalReportViewer1.ReportSource = myReportDocument
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

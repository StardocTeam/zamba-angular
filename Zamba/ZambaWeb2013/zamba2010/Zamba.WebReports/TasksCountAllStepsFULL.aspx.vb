Imports System.Data
Partial Class _Default
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Dataset As DataSet = Zamba.Data.WFStepsFactory.GetTasksCountAllStepsFULL
            Dim myReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            myReportDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory & "\Reportes\TasksCountAllStepsFull.rpt")
            myReportDocument.Database.Tables(0).SetDataSource(Dataset)
            CrystalReportViewer1.ReportSource = myReportDocument
            CrystalReportViewer1.DataBind()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class

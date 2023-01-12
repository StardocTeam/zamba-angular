Imports CrystalDecisions.CrystalReports.Engine
Imports System.Windows.Forms.Application

Partial Class Informes_Informe_1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim Dataset As System.Data.DataSet = Zamba.Data.WFStepsFactory.GetTasksCountAllStepsFULL
            Dim myReportDocument As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            myReportDocument.Load(StartupPath & "\Informes\CrystalReport5.rpt")
            myReportDocument.Database.Tables("Table").SetDataSource(Dataset)
            CrystalReportViewer1.ReportSource = myReportDocument
            CrystalReportViewer1.DataBind()
        Catch ex As Exception

        End Try
    End Sub
End Class

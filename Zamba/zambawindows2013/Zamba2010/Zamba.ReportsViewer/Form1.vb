Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
Imports System
Imports System.ComponentModel

Public Class Form1


    Public Sub ShowReport(ByVal ReportPath As String)
        Dim rptA As ReportDocument = New ReportDocument()
        rptA.Load(ReportPath)
        Me.CrystalReportViewer1.ReportSource = rptA
        For Each C As CrystalDecisions.CrystalReports.Engine.InternalConnectionInfo In rptA.DataSourceConnections
            Dim ac As New Zamba.Tools.ApplicationConfig(Tools.ApplicationConfig.ConfigType.Database)
            Dim Pw As String = ac.PASSWORD

            C.SetConnection(ac.SERVER, ac.DB, ac.USER, Pw)
            C.SetLogon(ac.USER, Pw)
        Next
    End Sub

End Class

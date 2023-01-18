Imports Zamba.Filters
Imports Zamba.Core
Imports System.Windows.Forms
Imports System.Drawing

Public Class PlayDoExecuteReport

    Private myRule As IDoReportBuilder
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            'EL REPORTE SE MUESTRA UNA UNICA VES
            Trace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la información del reporte...")
            Dim ds As DataSet = ReportBuilder.Business.ReportBuilderComponent.RunQueryBuilder(myrule.ReportId, True, True)
            Dim grilla As New Zamba.Grid.Grid.TelerikGrid(ds.Tables(0), True)
            grilla.Dock = Windows.Forms.DockStyle.Fill
            Dim form As New Windows.Forms.Form
            form.Text = "Reporte"
            form.StartPosition = FormStartPosition.CenterScreen
            form.Size = New Size(700, 600)
            form.Controls.Add(grilla)

            Trace.WriteLineIf(ZTrace.IsInfo, "Mostrando grilla.")

            'AGRUPACION POR COLUMNA
            Dim groupby As String = ReportBuilder.Business.ReportBuilderComponent.GetGroupExpression(myrule.ReportId, False)
            If Not String.IsNullOrEmpty(groupby) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "El reporte se agrupará por columna.")
                grilla.Group(groupby.Split(Chr(34))(3))
            End If

            form.Show()
        Finally
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoReportBuilder)
        Me.myRule = rule
    End Sub
End Class
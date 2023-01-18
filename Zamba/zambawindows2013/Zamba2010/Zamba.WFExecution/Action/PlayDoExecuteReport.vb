Imports System.Windows.Forms
Imports System.Drawing

Public Class PlayDoExecuteReport

    Private myRule As IDoReportBuilder
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim RB As New ReportBuilder.Business.ReportBuilderComponent
        'EL REPORTE SE MUESTRA UNA UNICA VES
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la información del reporte...")
        Dim ds As DataSet = RB.RunQueryBuilder(myRule.ReportId, True, True)
        Dim grilla As New Zamba.Grid.Grid.TelerikGrid(ds.Tables(0), True)
        grilla.Dock = DockStyle.Fill
        Dim form As New Form
        form.Text = "Reporte"
        form.StartPosition = FormStartPosition.CenterScreen
        form.Size = New Size(700, 600)
        form.Controls.Add(grilla)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Mostrando grilla.")

        'AGRUPACION POR COLUMNA
        Dim groupby As String = RB.GetGroupExpression(myRule.ReportId, False)
        If Not String.IsNullOrEmpty(groupby) Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El reporte se agrupará por columna.")
            grilla.GroupByColumnName(groupby.Split(Chr(34))(3))
        End If

        form.Show()

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoReportBuilder)
        myRule = rule
    End Sub
End Class
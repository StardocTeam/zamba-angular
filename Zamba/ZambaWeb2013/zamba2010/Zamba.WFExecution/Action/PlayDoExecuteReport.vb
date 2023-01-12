Imports Zamba.Filters
Imports Zamba.Core

Public Class PlayDoExecuteReport
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDoReportBuilder) As System.Collections.Generic.List(Of Core.ITaskResult)

        Try
            'EL REPORTE SE MUESTRA UNA UNICA VES
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obteniendo la información del reporte...")
            Dim RB As New ReportBuilder.Business.ReportBuilderComponent
            Dim ds As DataSet = RB.RunQueryBuilder(myrule.ReportId, True)


            'AGRUPACION POR COLUMNA
            Dim groupby As String = RB.GetGroupExpression(myrule.ReportId)
            If Not String.IsNullOrEmpty(groupby) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El reporte se agrupará por columna.")

            End If

        Finally

        End Try
        Return results
    End Function

End Class

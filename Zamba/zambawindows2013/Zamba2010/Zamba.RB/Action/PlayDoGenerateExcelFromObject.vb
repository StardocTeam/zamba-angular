Imports System.IO
Imports System.Drawing
Public Class PlayDoGenerateExcelFromObject

    Private _myRule As IDoGenerateExcelFromObject
    Private ExcelName As String
    Private dsName As String

    Sub New(ByVal rule As IDoGenerateExcelFromObject)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            Trace.WriteLineIf(ZTrace.IsInfo, "Generando excel a partir de un objeto...")

            Dim ExcelName As String = String.Empty
            Dim dsName As String = String.Empty
            Trace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results

                    ExcelName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.ExcelNAme, r)
                    ExcelName = WFRuleParent.ReconocerVariables(ExcelName)
                    dsName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.DataSetName, r)
                Next
            Else
                ExcelName = WFRuleParent.ReconocerVariables(Me._myRule.ExcelNAme)
                dsName = Me._myRule.DataSetName
            End If

            Dim dstoexcel As New DataSet
            dstoexcel = WFRuleParent.ReconocerVariablesAsObject(dsName)

            Trace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & ExcelName & "." & Me._myRule.ExportType.ToString)
            Trace.WriteLineIf(ZTrace.IsInfo, "Carpeta donde se generará el archivo: " & Me._myRule.Path)
            'Dim extntype As Int16
            'If myRule.ExportType = ExcelExportTypes.Otro Then
            '    extntype = myRule.OtherFormattype
            'Else
            'extntype = myRule.ExportType
            'End If
            If Not dstoexcel Is Nothing AndAlso Zamba.Office.ExcelInterop.DataTableDirectlyToExcel(Me._myRule.AddColName, dstoexcel.Tables(0), Me._myRule.Path & "\" & ExcelName, Office.ExcelInterop.eEstilosHoja.Reporte2, False, Me._myRule.ExportType) = False Then
                Trace.WriteLineIf(ZTrace.IsInfo, "No se pudo generar el archivo.")
            Else
                Trace.WriteLineIf(ZTrace.IsInfo, "El archivo se ha generado con éxito.")
            End If
        Finally

            Me.ExcelName = Nothing
            Me.dsName = Nothing
        End Try

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

Imports System.IO
Imports System.Drawing
Imports Zamba.FileTools

Public Class PlayDoGenerateExcelFromObject

    Private _myRule As IDoGenerateExcelFromObject
    Private ExcelName As String
    Private dsName As String

    Sub New(ByVal rule As IDoGenerateExcelFromObject)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando excel a partir de un objeto...")

            Dim ExcelName As String = String.Empty
            Dim dsName As String = String.Empty
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                    ExcelName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.ExcelNAme, r)
                    ExcelName = VarInterReglas.ReconocerVariables(ExcelName)
                    dsName = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.DataSetName, r)
                Next
            Else
                ExcelName = VarInterReglas.ReconocerVariables(Me._myRule.ExcelNAme)
                dsName = Me._myRule.DataSetName
            End If
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Variables reemplazadas" & vbCrLf & "Obteniendo dataset")
            Dim dstoexcel As New DataSet
            dstoexcel = VarInterReglas.ReconocerVariablesAsObject(dsName)
            VarInterReglas = Nothing
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Dataset obtenido")
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & ExcelName & "." & Me._myRule.ExportType.ToString)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Carpeta donde se generará el archivo: " & Me._myRule.Path)
            'Dim extntype As Int16
            'If myRule.ExportType = ExcelExportTypes.Otro Then
            '    extntype = myRule.OtherFormattype
            'Else
            'extntype = myRule.ExportType
            'End If
            Dim spireTools As New SpireTools()
            spireTools.ExportToXLS(dstoexcel.Tables(0), Me._myRule.Path & "\" & ExcelName & ".xls")

            ZTrace.WriteLineIf(ZTrace.IsVerbose, "El archivo se ha generado con éxito.")


            'ExcelName = Trim(ExcelName)

            'If Not dstoexcel Is Nothing AndAlso Zamba.Office.ExcelInterop.DataTableDirectlyToExcel(Me._myRule.AddColName, dstoexcel.Tables(0), Me._myRule.Path & "\" & ExcelName & "." & Me._myRule.ExportType.ToString(), Office.ExcelInterop.eEstilosHoja.Reporte2, False) = False Then

            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se pudo generar el archivo.")
            'Else
            'ZTrace.WriteLineIf(ZTrace.IsVerbose, "El archivo se ha generado con éxito.")
            'End If
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "No se pudo generar el archivo.")
            ZTrace.WriteLineIf(ZTrace.IsVerbose, "Error: " & ex.Message)
        Finally
            VarInterReglas = Nothing
            Me.ExcelName = Nothing
            Me.dsName = Nothing
        End Try

        Return results
    End Function
End Class

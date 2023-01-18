Imports System.IO
Public Class PlayDoGenerateExcelFromObject

    Private _myRule As IDoGenerateExcelFromObject
    Private ExcelName As String
    Private dsName As String

    Sub New(ByVal rule As IDoGenerateExcelFromObject)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando excel a partir de un objeto...")

            Dim ExcelName As String = String.Empty
            Dim dsName As String = String.Empty
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Obtener las variables")
            If results.Count > 0 Then
                For Each r As ITaskResult In results

                    ExcelName = TextoInteligente.ReconocerCodigo(_myRule.ExcelNAme, r)
                    ExcelName = WFRuleParent.ReconocerVariables(ExcelName)
                    dsName = TextoInteligente.ReconocerCodigo(_myRule.DataSetName, r)
                Next
            Else
                ExcelName = WFRuleParent.ReconocerVariables(_myRule.ExcelNAme)
                dsName = _myRule.DataSetName
            End If

            Dim dstoexcel As New DataSet
            dstoexcel = WFRuleParent.ReconocerVariablesAsObject(dsName)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Nombre del archivo: " & ExcelName & "." & _myRule.ExportType.ToString)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Carpeta donde se generará el archivo: " & _myRule.Path)
            'Dim extntype As Int16
            'If myRule.ExportType = ExcelExportTypes.Otro Then
            '    extntype = myRule.OtherFormattype
            'Else
            'extntype = myRule.ExportType
            'End If

            If Not IsNothing(dstoexcel) Then

                If Not _myRule.Path.EndsWith("\") Then _myRule.Path = _myRule.Path & "\"

                If _myRule.ExportType = ExcelExportTypes.csv Then
                    CreateFileCSV(dstoexcel.Tables(0), _myRule.Path & ExcelName & ".txt")

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo se ha generado con éxito.")

                    'ElseIf Zamba.Office.ExcelInterop.DataTableDirectlyToExcel(_myRule.AddColName, dstoexcel.Tables(0), _myRule.Path & ExcelName & "." & _myRule.ExportType.ToString(), Office.ExcelInterop.eEstilosHoja.Reporte2, False) Then
                ElseIf _myRule.ExportType = ExcelExportTypes.xlsx Then

                    Dim sp As New FileTools.SpireTools
                    Try
                        sp.ExportToXLS(dstoexcel.Tables(0), _myRule.Path & ExcelName & "." & _myRule.ExportType.ToString())
                    Catch ex As Exception
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo generar el archivo.")
                        ZClass.raiseerror(ex)
                    End Try

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "El archivo se ha generado con éxito.")

                Else

                    ZTrace.WriteLineIf(ZTrace.IsInfo, "No se pudo generar el archivo.")

                End If

            End If

        Finally

            ExcelName = Nothing
            dsName = Nothing
        End Try

        Return results
    End Function

    Public Shared Sub CreateFileCSV(dt As DataTable, nameFile As String)
        Try

            Dim writingFile As StreamWriter = File.CreateText(nameFile)
            Dim countCol As Integer = dt.Columns.Count
            Dim index As Int32

            For index = 0 To countCol - 1

                writingFile.Write(dt.Columns(index))

                writingFile.Write(";")

                index += 1
            Next

            writingFile.Write(writingFile.NewLine)
            index = 0

            For Each lala As DataRow In dt.Rows

                For index = 0 To countCol - 1

                    If Not IsDBNull(lala(index)) Then
                        writingFile.Write(lala(index))
                    End If

                    writingFile.Write(";")

                    index += 1
                Next
                writingFile.Write(writingFile.NewLine)
            Next

            writingFile.Close()
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Sub

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

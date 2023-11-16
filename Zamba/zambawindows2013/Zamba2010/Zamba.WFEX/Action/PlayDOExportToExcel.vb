Imports Zamba.Core

Public Class PlayDoExportToExcel

    Private myRule As IDoExportToExcel
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim bCabecera As Boolean
        bCabecera = False
        Dim arr_Excel As New List(Of String)
        Dim sBuf As New System.Text.StringBuilder
        Dim sSep As String
        Try
            For Each r As Core.TaskResult In results
                Try
                    If r.Indexs.Count > 0 Then
                        'La cabecera la extra la primera vez
                        If Not bCabecera Then
                            'Extrae la cabecera
                            sSep = ""
                            sBuf.Append("Nombre")
                            sBuf.Append(vbTab)
                            sBuf.Append("Estado")
                            sBuf.Append(vbTab)
                            sBuf.Append("Etapa")
                            sBuf.Append(vbTab)
                            For Each unIndex As Core.Index In r.Indexs
                                sBuf.Append(sSep)
                                'Si esta vacio agrega un espacio
                                If String.IsNullOrEmpty(unIndex.Name) Then
                                    sBuf.Append(" ")
                                Else
                                    sBuf.Append(unIndex.Name)
                                End If
                                sSep = vbTab
                            Next
                            'Escribe Cabecera
                            arr_Excel.Add(sBuf.ToString)
                            'Agrega un salto de linea
                            arr_Excel.Add(vbNewLine)
                            sBuf.Remove(0, sBuf.Length)
                            bCabecera = True
                        End If
                        'Extrae Fila
                        sSep = ""
                        sBuf.Append(r.Name)
                        sBuf.Append(vbTab)
                        sBuf.Append(r.State.Name)
                        sBuf.Append(vbTab)
                        sBuf.Append(WFStepBusiness.GetStepNameById(r.StepId))
                        sBuf.Append(vbTab)
                        For Each unIndex As Core.Index In r.Indexs
                            sBuf.Append(sSep)
                            'Si Esta vacio agrega un espacio
                            If String.IsNullOrEmpty(unIndex.Data) Then
                                sBuf.Append(" ")
                            Else
                                sBuf.Append(unIndex.Data)
                            End If
                            sSep = vbTab
                        Next
                        'Escribe Fila
                        arr_Excel.Add(sBuf.ToString)
                        'Agrega un salto de linea
                        arr_Excel.Add(vbNewLine)
                        sBuf.Remove(0, sBuf.Length)
                    End If
                Catch
                End Try
            Next
            'Guarda el contenido un archivo
            Trace.WriteLineIf(ZTrace.IsInfo, "Exportando excel...")
            ExportarAExcel(arr_Excel, myRule.Ruta)
            Trace.WriteLineIf(ZTrace.IsInfo, "Exportaci�n realizada con �xito!")
        Finally

            arr_Excel.Clear()
            arr_Excel = Nothing
            sBuf.Remove(0, sBuf.Length)
            sBuf = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
    ''' <summary>
    ''' Crea un archivo temporal con el contenido de la lista
    ''' y llama a metodo TextToExcel para covertir el contenido a Excel
    ''' </summary>
    ''' <param name="p_Lista">lista</param>
    ''' <remarks></remarks>
    ''' <history>
    '''     Oscar [Created]  12-10-2006 
    ''' </history>
    Private Sub ExportarAExcel(ByVal p_Lista As List(Of String), ByVal ruta As String)
        Dim sArchivo As String
        Dim sPath As String

        sArchivo = System.DateTime.Now().ToString("yyyyMMddhhmmss")
        sPath = System.Windows.Forms.Application.StartupPath & "\" & sArchivo

        If p_Lista.Count > 0 Then
            Dim ArchivoTemp As System.IO.StreamWriter = New System.IO.StreamWriter(sPath, False)
            Using ArchivoTemp
                For Each row As String In p_Lista
                    ArchivoTemp.Write(row)
                Next
                ArchivoTemp.Close()
            End Using
            'Zamba.Excel.Business.Excel.TextToExcel(sPath, params(0).tostring() & "\" & sArchivo & ".xls", Zamba.Excel.Business.Excel.eEstilosHoja.Estilo2)
            Zamba.Office.ExcelInterop.TextToExcel(sPath, ruta & "\" & sArchivo & ".xls", Zamba.Office.ExcelInterop.eEstilosHoja.Estilo2)
            IO.File.Delete(sPath)
        End If
    End Sub

    Public Sub New(ByVal rule As IDoExportToExcel)
        Me.myRule = rule
    End Sub
End Class
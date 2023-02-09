Imports Zamba.Core

Public Class PlayDOGenerateExcel
    Private MyRule As IDoGenerateExcel
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IDoGenerateExcel) As System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            MyRule = rule
            If results.Count > 0 Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Generando Excel...")
                GenerarFactura(results)
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Excel generado con éxito!")
            Else
                ZTrace.WriteLineIf(ZTrace.IsInfo, "No hay datos para generar el excel.")
            End If
        Finally
        End Try

        Return results
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Arma los datos para pasarlo a excel
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	01/03/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Private Sub GenerarFactura(ByVal Results As System.Collections.Generic.List(Of Core.ITaskResult))
        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Armo el cuerpo")

        'Contadores
        Dim i As Int32 = 0
        Dim j As Int32 = 0

        Dim strIndex As String = MyRule.Index

        'variables que van a contener los datos
        Dim values As New ArrayList()
        Dim Encabezado As New ArrayList()
        Dim showIndex As New ArrayList()
        Dim sumIndex As New ArrayList()
        Dim countIndex As New ArrayList()

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Obtengo los indices y sus valores")
        While strIndex <> ""
            'Obtengo el indice y sus respectivos valores
            Dim strItem As String = strIndex.Split("//")(0)
            'If the index is set to show save the index and the name for the header
            If Boolean.Parse(strItem.Split("|")(2)) = True Then
                showIndex.Add(strItem.Split("|")(0))
                Encabezado.Add(strItem.Split("|")(1))
                i += 1
                'Si quiero ver la suma guardo la posicion
                If Boolean.Parse(strItem.Split("|")(3)) = True Then
                    sumIndex.Add(i)
                End If
                'Si quiero ver la cuenta guardo la posicion
                If Boolean.Parse(strItem.Split("|")(4)) = True Then
                    countIndex.Add(i)
                End If
            End If

            strIndex = strIndex.Remove(0, strIndex.Split("//")(0).Length)
            If strIndex.Length > 0 Then
                strIndex = strIndex.Remove(0, 2)
            End If
        End While

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Guardo los datos de los indices")
        'Salvo los valores de los indices para el cuerpo del documento
        For Each v As Core.TaskResult In Results
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & v.Name)
            If MyRule.DocTypeId = v.DocType.ID Then
                Dim line As New ArrayList()
                i = 0
                For Each index As Core.Index In v.Indexs
                    For Each id As Int32 In showIndex
                        If id = index.ID Then
                            line.Add(index.Data)
                            i += 1
                            Exit For
                        End If
                    Next
                Next
                values.Add(line)
                j += 1
            End If
        Next

        ZTrace.WriteLineIf(ZTrace.IsVerbose, "Creo el documento de Excel")
        Office.ExcelInterop.GenerateExcelDoc(MyRule.Title, Encabezado, values, MyRule.Footer, sumIndex, countIndex)
    End Sub
End Class

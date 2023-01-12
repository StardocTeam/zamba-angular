Public Class PlayDOExecuteQuery
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOExecuteQuery) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLineIf(ZTrace.IsVerbose, "*** Ejecutando regla " & myRule.Name)
        Try
            Dim QueryType As ReturnType = myRule.QueryType
            Dim folder As String = myRule.Folder
            Dim q As Zamba.Data.cExecuteSql = New Zamba.Data.cExecuteSql()

            Dim strselect As String
            strselect = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.Sql, results)
            strselect = WFRuleParent.ReconocerVariablesValuesSoloTexto(strselect)

            Trace.WriteLineIf(ZTrace.IsVerbose, "Ejecutando Consulta SQL (" & strselect & ")")
            If QueryType = ReturnType.Table Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo Tablas de Datos")
                Ds = q.ExecuteSQL(QueryType, strselect)
            End If
            If QueryType = ReturnType.Scalar OrElse QueryType = ReturnType.NoValue Then
                Trace.WriteLineIf(ZTrace.IsVerbose, "Tipo Unico Dato")
                i = q.ExecuteSQL(QueryType, strselect)
            End If
            SaveToFile(folder)
        Catch ex As Exception
            Debug.WriteLine(ex.Message & ex.StackTrace)
            Trace.WriteLineIf(ZTrace.IsVerbose, "Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            Trace.WriteLineIf(ZTrace.IsVerbose, "*** Finalizado ejecucion de regla " & myRule.Name)
        End Try
        Return results
    End Function

    Dim Ds As DataSet
    Dim i As Int64 = 0 'representa el valor Scalar devuelto o la cantidad de filas actualizadas/Eliminadas

    Private Sub SaveToFile(ByVal folder As String)
        If folder.Trim <> String.Empty Then
            If IsNothing(Ds) = False Then
                Ds.WriteXml(folder & "\resultado.xml")
            Else
                If i > 0 Then
                    Dim filepath As String = folder & "\resultado.txt"
                    Trace.WriteLineIf(ZTrace.IsVerbose, "Guardando resultado de la consulta en : " & filepath)
                    Dim sw As New IO.StreamWriter(filepath)
                    sw.WriteLine(i)
                    sw.Close()
                    sw.Dispose()
                    sw = Nothing
                End If
            End If
        End If
    End Sub
End Class

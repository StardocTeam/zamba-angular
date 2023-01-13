Public Class PlayDOExecuteQuery
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOExecuteQuery) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "*** Ejecutando regla " & myRule.Name)
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            Dim QueryType As ReturnType = myRule.QueryType
            Dim folder As String = myRule.Folder
            Dim q As Zamba.Data.cExecuteSql = New Zamba.Data.cExecuteSql()

            Dim strselect As String
            strselect = Zamba.Core.TextoInteligente.ReconocerCodigo(myRule.Sql, results)
            strselect = VarInterReglas.ReconocerVariablesValuesSoloTexto(strselect)

            ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando Consulta SQL (" & strselect & ")")
            If QueryType = ReturnType.Table Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo Tablas de Datos")
                Ds = q.ExecuteSQL(QueryType, strselect)
            End If
            If QueryType = ReturnType.Scalar OrElse QueryType = ReturnType.NoValue Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Tipo Unico Dato")
                i = q.ExecuteSQL(QueryType, strselect)
            End If
            SaveToFile(folder)
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsError, ex.Message & ex.StackTrace)
            ZTrace.WriteLineIf(ZTrace.IsError, "Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            VarInterReglas = Nothing
            ZTrace.WriteLineIf(ZTrace.IsInfo, "*** Finalizado ejecucion de regla " & myRule.Name)
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
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Guardando resultado de la consulta en : " & filepath)
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

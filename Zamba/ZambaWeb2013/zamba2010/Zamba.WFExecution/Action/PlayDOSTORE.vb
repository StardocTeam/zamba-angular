Public Class PlayDOSTORE
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOSOTORE) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "*** Ejecutando regla " & myRule.Name)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                NewList.Add(r)
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.Message & ex.StackTrace)
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            ZTrace.WriteLineIf(ZTrace.IsInfo, "*** Finalizado ejecucion de regla " & myRule.Name)
        End Try

        Return NewList
    End Function
End Class

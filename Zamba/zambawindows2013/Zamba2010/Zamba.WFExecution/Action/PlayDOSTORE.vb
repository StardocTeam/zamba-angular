Public Class PlayDOSTORE
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOSOTORE) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLineIf(ZTrace.IsVerbose, "*** Ejecutando regla " & myRule.Name)
        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As Core.TaskResult In results
                NewList.Add(r)
            Next
        Catch ex As Exception
            Debug.WriteLine(ex.Message & ex.StackTrace)
            Trace.WriteLineIf(ZTrace.IsVerbose, "Error en la ejecucion de la regla " & myRule.Name)
            ZClass.raiseerror(ex)
        Finally
            Trace.WriteLineIf(ZTrace.IsVerbose, "*** Finalizado ejecucion de regla " & myRule.Name)
        End Try

        Return NewList
    End Function
End Class

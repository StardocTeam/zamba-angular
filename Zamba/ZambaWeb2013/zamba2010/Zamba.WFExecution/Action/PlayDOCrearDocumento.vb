Public Class PlayDOCrearDocumento
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOCrearDocumento) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name & ", Id " & r.TaskId)
                NewList.Add(r)
            Next
        Finally

        End Try
        Return NewList
    End Function
End Class

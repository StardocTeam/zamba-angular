Public Class PlayDOUSERTASK
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myRule As IDOUSERTASK) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                NewList.Add(r)
            Next
        Finally

        End Try

        Return NewList
    End Function

    Public Sub New()

    End Sub
End Class

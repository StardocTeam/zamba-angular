Public Class PlayDoImport
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IDoImport) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Importando...")
            For Each r As Core.TaskResult In results
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Ejecutando la regla para la tarea " & r.Name)
                NewList.Add(r)
            Next
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Importación realizada con éxito!")
        Finally

        End Try

        Return NewList
    End Function
End Class

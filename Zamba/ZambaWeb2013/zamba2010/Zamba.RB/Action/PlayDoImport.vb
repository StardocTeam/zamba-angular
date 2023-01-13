Public Class PlayDoImport


    Private myRule As IDoImport

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            Trace.WriteLineIf(ZTrace.IsInfo, "Importando...")
            For Each r As Core.TaskResult In results
                NewList.Add(r)
            Next
            Trace.WriteLineIf(ZTrace.IsInfo, "Importación realizada con éxito!")
        Finally

        End Try

        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoImport)
        Me.myRule = rule
    End Sub
End Class

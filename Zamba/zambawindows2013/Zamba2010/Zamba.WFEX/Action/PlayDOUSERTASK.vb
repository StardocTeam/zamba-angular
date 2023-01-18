Imports Zamba.Core

Public Class PlayDOUSERTASK

    Private myRule As IDOUSERTASK

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        Try
            For Each r As Core.TaskResult In results
                NewList.Add(r)
            Next
        Finally

        End Try

        Return NewList
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDOUSERTASK)
        Me.myRule = rule
    End Sub
End Class

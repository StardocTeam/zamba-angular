Public Class PlayIfDoAction
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDoAction) As System.Collections.Generic.List(Of Core.ITaskResult)

        If rule.ChildRules.Count = 2 Then
            For Each child As Object In rule.ChildRules
                If child.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        For Each r As Core.TaskResult In results
            NewList.Add(r)
        Next

        Return NewList
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDoAction, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

Public Class PlayIfStepDateTime

    Private myRule As IIfStepDateTime

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfStepDateTime)
        Me.myRule = rule
    End Sub
End Class
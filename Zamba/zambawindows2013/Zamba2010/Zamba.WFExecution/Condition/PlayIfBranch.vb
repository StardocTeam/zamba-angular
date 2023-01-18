Public Class PlayIfBranch

    Private _myRule As IIfBranch

    Sub New(ByVal rule As IIfBranch)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return DirectCast(_myRule.ParentRule, IRuleIFPlay).PlayIf(results, _myRule.ifType)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

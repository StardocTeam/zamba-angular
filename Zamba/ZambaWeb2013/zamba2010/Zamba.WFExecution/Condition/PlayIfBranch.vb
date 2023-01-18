Imports Zamba.Core.ZClass
Public Class PlayIfBranch

    Private _myRule As IIfBranch

    Sub New(ByVal rule As IIfBranch)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return DirectCast(Me._myRule.ParentRule, IRuleIFPlay).PlayIf(results, Me._myRule.ifType)
    End Function
End Class

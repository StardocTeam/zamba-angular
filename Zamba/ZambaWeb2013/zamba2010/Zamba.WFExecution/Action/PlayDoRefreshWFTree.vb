Public Class PlayDoRefreshWfTree
    Private _myRule As IDoRefreshWfTree

    Sub New(ByVal rule As IDoRefreshWfTree)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
End Class
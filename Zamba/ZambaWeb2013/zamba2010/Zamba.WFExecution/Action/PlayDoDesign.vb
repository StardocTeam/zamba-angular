Imports Zamba.Core

Public Class PlayDoDesign
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoDesign) As System.Collections.Generic.List(Of ITaskResult)
        Dim id As Integer = DirectCast(myRule, WFRuleParent).ID
        Return results
    End Function
End Class

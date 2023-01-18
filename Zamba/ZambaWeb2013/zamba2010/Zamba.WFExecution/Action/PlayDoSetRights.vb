Imports Zamba.Core

Public Class PlayDoSetRights

    Private myRule As IDoSetRights

    Sub New(ByVal rule As IDoSetRights)
        Me.myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoSetRights) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function
End Class
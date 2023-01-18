Imports Zamba.Core

Public Class PlayIfDesign
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IIfDesign) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IIfDesign, ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function
End Class

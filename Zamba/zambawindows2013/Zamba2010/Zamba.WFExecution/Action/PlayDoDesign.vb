Public Class PlayDoDesign
    Private _myRule As IDoDesign
    Sub New(ByVal rule As IDoDesign)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoDesign) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

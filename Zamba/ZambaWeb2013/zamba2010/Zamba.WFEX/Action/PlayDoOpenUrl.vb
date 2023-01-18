Imports Zamba.Core

Public Class PlayDoOpenUrl
    Private _myRule As IDoOpenUrl
    Private url As String

    Sub New(ByVal rule As IDoOpenUrl)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        Return (results)
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

Public Class PlayDoRefreshWfTree
    Private _myRule As IDoRefreshWfTree

    Sub New(ByVal rule As IDoRefreshWfTree)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        ZambaCore.HandleGenericAction(GenericActions.RefreshWfTree)
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
Public Class PlayDoGoHome

    Private myRule As IDoGoHome

    Sub New(ByVal rule As IDoGoHome)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        ZambaCore.HandleRuleModule(ResultActions.IrAHome, results, New Hashtable)
        Return results
    End Function

    Function DiscoverParams() As List(Of String)

    End Function

End Class

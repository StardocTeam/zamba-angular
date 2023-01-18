Public Class PlayDoGoHome

    Private myRule As IDoGoHome

    Sub New(ByVal rule As IDoGoHome)
        myRule = rule
    End Sub

    Public Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        'Ir al Home de la web
        Return results
    End Function

    Public Function PlayWeb(ByVal results As List(Of ITaskResult), ByVal Params As Hashtable) As List(Of ITaskResult)
        Dim VarInterReglas As New VariablesInterReglas()
        Try

        Finally
        End Try

        Return results

    End Function

    Function DiscoverParams() As List(Of String)

    End Function

End Class

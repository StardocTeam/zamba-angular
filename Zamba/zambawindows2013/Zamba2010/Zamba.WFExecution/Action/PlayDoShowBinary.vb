Public Class PlayDoShowBinary

    Private myRule As IDoShowBinary

    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Throw New NotImplementedException("La regla se encuentra implementada unicamente en el cliente Web de Zamba. Comuníquese con Sistemas para solicitar la implementación en el cliente de Windows.")
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoShowBinary)
        myRule = rule
    End Sub
End Class

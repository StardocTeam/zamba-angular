Imports Zamba.Core

Public Class PlayDoGenerateHTMLReport
    Private _myRule As IDoGenerateHTMLReport

    Sub New(ByVal rule As IDoGenerateHTMLReport)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Trace.WriteLineIf(ZTrace.IsInfo, "Regla no disponible en cliente Windows")
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

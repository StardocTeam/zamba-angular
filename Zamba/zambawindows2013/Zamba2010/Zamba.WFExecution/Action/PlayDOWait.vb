Public Class PlayDOWait
    Private _myRule As IDoWait

    Sub New(ByVal rule As IDoWait)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Cantidad de results a ejecutar: " & results.Count)

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Tiempo a esperar: " & _myRule.WaitTime)
        Dim waitTime As String = WFRuleParent.ReconocerVariables(_myRule.WaitTime)
        If results.Count > 0 Then
            waitTime = TextoInteligente.ReconocerCodigo(waitTime, results(0))
        End If
        Threading.Thread.CurrentThread.Sleep(_myRule.WaitTime * 1000)
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Espera finalizada")

        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

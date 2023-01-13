Public Class PlayDoExecuteScript
    Private myRule As IDoExecuteScript
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim script As String = myRule.Script
        Try
            If Not IsNothing(results) AndAlso results.Count > 0 Then
                script = TextoInteligente.ReconocerCodigo(script, results(0))
            End If


            Dim VarInterReglas As New VariablesInterReglas()
            script = VarInterReglas.ReconocerVariables(script)
            VarInterReglas = Nothing


            ZTrace.WriteLineIf(ZTrace.IsInfo, "Script: " & script)
            Dim params As New Hashtable
            params.Add("Script", script)
            ZambaCore.HandleRuleModule(ResultActions.ExecuteScript, results, params)
            Return results
        Finally

        End Try
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IDoExecuteScript)
        myRule = rule
    End Sub
End Class

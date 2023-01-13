Public Class PlayDoExecuteScript

    Private _myRule As IDoExecuteScript

    Sub New(ByVal rule As IDoExecuteScript)
        Me._myRule = rule
    End Sub
    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoExecuteScript) As System.Collections.Generic.List(Of ITaskResult)
        Return PlayWeb(results, New Hashtable, _myRule)

    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoExecuteScript) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim script As String = myRule.Script

        If Not IsNothing(results) AndAlso results.Count > 0 Then
                script = TextoInteligente.ReconocerCodigo(script, results(0))
            End If

            Dim VarInterReglas As New VariablesInterReglas()
            script = VarInterReglas.ReconocerVariables(script)
            VarInterReglas = Nothing
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Script: " & script)

        Params.Add("ScriptToExecute", script)
        Params.Add("DoExecuteScript", True)
        Return results
    End Function

    Public Function PlayWebSecondExecution(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoExecuteScript) As System.Collections.Generic.List(Of Core.ITaskResult)
        Params.Clear()
        Return results
    End Function


End Class

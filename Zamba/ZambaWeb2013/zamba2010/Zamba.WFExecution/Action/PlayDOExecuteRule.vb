Public Class PlayDOExecuteRule

    Private _myRule As IDOExecuteRule
    Private WFStepId As Int64
    Private lista As List(Of ITaskResult)
    Private WFRS As WFRulesBusiness

    Sub New(ByVal rule As IDOExecuteRule)
        Me._myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me.lista = New List(Of ITaskResult)
        Dim IDRule As String
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            If _myRule.Mode Then
                IDRule = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.IDRule, results(0)).Trim
                If IDRule.Contains("zvar") = True Then
                    IDRule = VarInterReglas.ReconocerVariablesValuesSoloTexto(IDRule)
                End If
            Else
                IDRule = Me._myRule.RuleID
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ID Regla = " + IDRule)

            Me.WFRS = New WFRulesBusiness
            Dim TB As New WF.WF.WFTaskBusiness

            Try
                Dim R As WFRuleParent = WFRS.GetInstanceRuleById(Int64.Parse(IDRule))
                R.ParentRule = Me._myRule

                '//                lista = R.ExecuteWebRule(results, TB, WFRS, RulePendingEvents.ExecuteErrorRule, RuleExecutionResult.PendingEventExecution, Nothing, _myRule.IsAsync)
                lista = R.ExecuteRule(results, TB, WFRS, _myRule.IsAsync)

            Catch e As Exception
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Error ejecutando regla : " + IDRule + " " + e.Message)
                ZClass.raiseerror(e)
            End Try


        Finally
            VarInterReglas = Nothing
            Me.WFStepId = Nothing
            Me.WFRS = Nothing
        End Try
        Return lista
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me.lista = New List(Of ITaskResult)
        Dim IDRule As String
        Dim VarInterReglas As New VariablesInterReglas()
        Try
            If _myRule.Mode Then
                IDRule = Zamba.Core.TextoInteligente.ReconocerCodigo(Me._myRule.IDRule, results(0)).Trim
                If IDRule.Contains("zvar") = True Then
                    IDRule = VarInterReglas.ReconocerVariablesValuesSoloTexto(IDRule)
                End If
            Else
                IDRule = Me._myRule.RuleID
            End If
            ZTrace.WriteLineIf(ZTrace.IsInfo, "ID Regla = " + IDRule)
            Params.Add("RuleID", IDRule)


            Params.Add("ExecuteRuleID", Me._myRule.ChildRulesIds)


        Finally
            VarInterReglas = Nothing
            Me.WFStepId = Nothing
            Me.WFRS = Nothing
        End Try
        Return results
    End Function
End Class
Public Class PlayDOExecuteRule

    Private _myRule As IDOExecuteRule
    Private WFStepId As Int64
    Private lista As List(Of ITaskResult)
    Private WFRS As WFRulesBusiness

    Sub New(ByVal rule As IDOExecuteRule)
        _myRule = rule
    End Sub

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal refreshTasks As List(Of Int64)) As System.Collections.Generic.List(Of Core.ITaskResult)
        lista = New List(Of ITaskResult)
        Dim IDRule As String = _myRule.IDRule.ToString

        Try
            If _myRule.Mode Then

                IDRule = TextoInteligente.ReconocerCodigo(_myRule.IDRule, results(0)).Trim

                If IDRule.Contains("zvar") = True Then
                    IDRule = WFRuleParent.ReconocerVariablesValuesSoloTexto(IDRule)
                End If
                ZTrace.WriteLineIf(ZTrace.IsInfo, "ID Regla = " + IDRule)

                WFStepId = WFRulesBusiness.GetWFStepIdbyRuleID(IDRule)
            Else
                WFStepId = WFRulesBusiness.GetWFStepIdbyRuleID(_myRule.RuleID)

            End If


            WFRS = New WFRulesBusiness

                If _myRule.Mode Then
                    lista = WFRS.ExecuteRule(IDRule, WFStepId, results, False, refreshTasks)
                Else
                    lista = WFRS.ExecuteRule(_myRule.RuleID, WFStepId, results, False, refreshTasks)
                End If


        Catch ex As Exception
            If _myRule.ContinueWithError Then
                lista = results
            Else
                Throw ex
            End If

        Finally
            WFStepId = Nothing
            WFRS = Nothing
        End Try

        Return lista
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class
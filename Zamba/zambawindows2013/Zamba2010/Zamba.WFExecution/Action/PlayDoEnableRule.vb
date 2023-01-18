Public Class PlayDoEnableRule

    Private _myRule As IDoEnableRule
    Private lstSelectedRuleIDs As Generic.List(Of Int64)
    Private sAux() As String

    Sub New(ByVal rule As IDoEnableRule)
        _myRule = rule
    End Sub


    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        lstSelectedRuleIDs = New Generic.List(Of Int64)
        lstSelectedRuleIDs.Clear()
        Try
            sAux = New String() {}
            If Not String.IsNullOrEmpty(_myRule.SelectedRulesIDs) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Los Id de las reglas a modificar son: " & _myRule.SelectedRulesIDs)
                sAux = _myRule.SelectedRulesIDs.Split(Char.Parse(","))
                For Each sRuleID As String In sAux
                    lstSelectedRuleIDs.Add(Convert.ToInt64(sRuleID))
                Next
            End If

            If _myRule.OnlyForTask = False Then

                For Each selectedRuleID As Int64 In lstSelectedRuleIDs

                    WFRulesBusiness.SetRuleEstado(selectedRuleID, _myRule.RuleEstado)

                Next

            Else


                For Each r As Core.TaskResult In results
                    For Each myRuleID As Int64 In lstSelectedRuleIDs

                        'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        'y en la 1 si se acumula a la habilitacion de las solapas o no
                        Dim lstRulesEnabled As New List(Of Boolean)
                        lstRulesEnabled.Add(_myRule.RuleEstado)
                        lstRulesEnabled.Add(_myRule.RuleEjecucion)

                        If r.UserRules.ContainsKey(myRuleID) Then
                            r.UserRules(myRuleID) = lstRulesEnabled.ToList()
                        Else
                            r.UserRules.Add(myRuleID, lstRulesEnabled)
                        End If
                    Next
                Next
            End If
        Finally
            lstSelectedRuleIDs = Nothing
            sAux = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

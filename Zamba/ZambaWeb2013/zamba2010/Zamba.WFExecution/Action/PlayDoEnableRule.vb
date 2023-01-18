Public Class PlayDoEnableRule

    Private _myRule As IDoEnableRule
    Private lstSelectedRuleIDs As Generic.List(Of Int64)
    Private sAux() As String
    Private lstRulesEnabled As List(Of Boolean)

    Sub New(ByVal rule As IDoEnableRule)
        Me._myRule = rule
    End Sub




    Public Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal myRule As IDoEnableRule) As System.Collections.Generic.List(Of ITaskResult)
        Return PlayWeb(results, New Hashtable, _myRule)
    End Function

    Public Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal Params As Hashtable, ByVal myRule As IDoEnableRule) As System.Collections.Generic.List(Of Core.ITaskResult)

        Me.lstSelectedRuleIDs = New Generic.List(Of Int64)
        Me.lstRulesEnabled = New List(Of Boolean)
        Me.lstSelectedRuleIDs.Clear()
        Try
            Me.sAux = New String() {}
            If Not String.IsNullOrEmpty(Me._myRule.SelectedRulesIDs) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "Los Id de las reglas a modificar son: " & Me._myRule.SelectedRulesIDs)
                Me.sAux = Me._myRule.SelectedRulesIDs.Split(Char.Parse(","))
                For Each sRuleID As String In Me.sAux
                    Me.lstSelectedRuleIDs.Add(Convert.ToInt64(sRuleID))
                Next
            End If

            'If Me._myRule.OnlyForTask = False Then
            '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando el estado de la regla en la base de datos")
            '    Dim WFRulesBusiness As New WFRulesBusiness

            '    For Each selectedRuleID As Int64 In Me.lstSelectedRuleIDs
            '        ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando el estado de la regla")
            '        ZTrace.WriteLineIf(ZTrace.IsVerbose, " cuyo Id es " & selectedRuleID & " a " & Me._myRule.RuleEstado.ToString())
            '        WFRulesBusiness.SetRuleEstado(selectedRuleID, Me._myRule.RuleEstado)
            '        ZTrace.WriteLineIf(ZTrace.IsInfo, " OK")
            '    Next
            '    WFRulesBusiness = Nothing
            '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Los estados se han modificado con éxito!")
            'Else
            For Each r As Core.TaskResult In results
                For Each myRuleID As Int64 In Me.lstSelectedRuleIDs
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Modificando el estado de la regla Id " & myRuleID & " a " & Me._myRule.RuleEstado.ToString())
                    'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                    'y en la 1 si se acumula a la habilitacion de las solapas o no
                    Me.lstRulesEnabled.Clear()
                    Me.lstRulesEnabled.Add(Me._myRule.RuleEstado)
                    Me.lstRulesEnabled.Add(Me._myRule.RuleEjecucion)

                    If r.UserRules.ContainsKey(myRuleID) Then
                        r.UserRules(myRuleID) = Me.lstRulesEnabled
                    Else
                        r.UserRules.Add(myRuleID, Me.lstRulesEnabled)
                    End If
                Next
                Params.Add("UserRules", r.UserRules)
            Next
            ' End If
        Finally

            Me.lstSelectedRuleIDs = Nothing
            sAux = Nothing
            Me.lstRulesEnabled = Nothing
        End Try
        Return results

    End Function


End Class

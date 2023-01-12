Public Class PlayDoEnableRule

    Private _myRule As IDoEnableRule
    Private lstSelectedRuleIDs As Generic.List(Of Int64)
    Private sAux() As String

    Sub New(ByVal rule As IDoEnableRule)
        Me._myRule = rule
    End Sub


    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Me.lstSelectedRuleIDs = New Generic.List(Of Int64)
        Me.lstSelectedRuleIDs.Clear()
        Try
            Me.sAux = New String() {}
            If Not String.IsNullOrEmpty(Me._myRule.SelectedRulesIDs) Then
                Trace.WriteLineIf(ZTrace.IsInfo, "Los Id de las reglas a modificar son: " & Me._myRule.SelectedRulesIDs)
                Me.sAux = Me._myRule.SelectedRulesIDs.Split(Char.Parse(","))
                For Each sRuleID As String In Me.sAux
                    Me.lstSelectedRuleIDs.Add(Convert.ToInt64(sRuleID))
                Next
            End If

            If Me._myRule.OnlyForTask = False Then

                For Each selectedRuleID As Int64 In Me.lstSelectedRuleIDs
                
                    WFRulesBusiness.SetRuleEstado(selectedRuleID, Me._myRule.RuleEstado)

                Next

            Else


                For Each r As Core.TaskResult In results
                    For Each myRuleID As Int64 In Me.lstSelectedRuleIDs
                   
                        'Lista que en la posicion 0 guarda si esta habilitada la regla o no
                        'y en la 1 si se acumula a la habilitacion de las solapas o no
                        Dim lstRulesEnabled As New List(Of Boolean)
                        lstRulesEnabled.Add(Me._myRule.RuleEstado)
                        lstRulesEnabled.Add(Me._myRule.RuleEjecucion)

                        If r.UserRules.ContainsKey(myRuleID) Then
                            r.UserRules(myRuleID) = lstRulesEnabled.ToList()
                        Else
                            r.UserRules.Add(myRuleID, lstRulesEnabled)
                        End If
                    Next
                Next
            End If
        Finally
            Me.lstSelectedRuleIDs = Nothing
            sAux = Nothing
        End Try
        Return results
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
End Class

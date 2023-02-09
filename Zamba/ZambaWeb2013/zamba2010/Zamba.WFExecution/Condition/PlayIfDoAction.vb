Public Class PlayIfDoAction
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDoAction) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (rule.ChildRulesIds Is Nothing OrElse rule.ChildRulesIds.Count = 0) Then
            rule.ChildRulesIds = WFRB.GetChildRulesIds(rule.ID)
        End If

        If rule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In rule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = rule
                R.IsAsync = rule.IsAsync
                If R.GetType().ToString().Contains("IfBranch") Then
                    Return results
                End If
            Next
        End If

        Dim NewList As New System.Collections.Generic.List(Of Core.ITaskResult)
        For Each r As Core.TaskResult In results
            NewList.Add(r)
        Next

        Return NewList
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal rule As IIfDoAction, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
End Class

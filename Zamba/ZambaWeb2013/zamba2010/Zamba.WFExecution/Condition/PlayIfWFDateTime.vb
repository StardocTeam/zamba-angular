Public Class PlayIfWFDateTime
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfWFDateTime) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID, myrule.RuleClass, results)
        End If

        If myrule.ChildRulesIds.Count = 2 Then
            For Each childruleId As Int64 In myrule.ChildRulesIds
                Dim R As WFRuleParent = WFRB.GetInstanceRuleById(childruleId)
                R.ParentRule = myrule
                R.IsAsync = myrule.IsAsync
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
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfWFDateTime, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
End Class
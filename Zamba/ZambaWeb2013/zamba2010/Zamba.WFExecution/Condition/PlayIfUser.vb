Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayIfUser
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfUser) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim WFRB As New WFRulesBusiness
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Verificando la existencia de Reglas hijas")
        If (myrule.ChildRulesIds Is Nothing OrElse myrule.ChildRulesIds.Count = 0) Then
            myrule.ChildRulesIds = WFRB.GetChildRulesIds(myrule.ID)
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

        Dim validResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim comparator As UserComparators = myrule.Comparator
        Select Case comparator
            Case UserComparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If taskResult.AsignedToId = myrule.UserId Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If taskResult.AsignedToId <> myrule.UserId Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.CurrentUser
                For Each taskResult As TaskResult In results
                    If Zamba.Membership.MembershipHelper.CurrentUser.ID = myrule.UserId Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotCurrentUser
                For Each taskResult As TaskResult In results
                    If Zamba.Membership.MembershipHelper.CurrentUser.ID <> myrule.UserId Then
                        validResults.Add(taskResult)
                    End If
                Next
        End Select
        Return validResults
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfUser, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim validResults As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim comparator As UserComparators = myrule.Comparator
        Select Case comparator
            Case UserComparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If (taskResult.AsignedToId = myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If (taskResult.AsignedToId <> myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.CurrentUser
                For Each taskResult As TaskResult In results
                    If (Zamba.Membership.MembershipHelper.CurrentUser.ID = myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotCurrentUser
                For Each taskResult As TaskResult In results
                    If (Zamba.Membership.MembershipHelper.CurrentUser.ID <> myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
        End Select
        Return validResults
    End Function
End Class

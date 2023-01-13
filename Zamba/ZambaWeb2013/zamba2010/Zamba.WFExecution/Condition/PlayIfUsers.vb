Imports Zamba.Core
Imports Zamba.Membership

Public Class PlayIfUsers
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfUsers) As System.Collections.Generic.List(Of Core.ITaskResult)
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

        Dim validUser As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UserList As Hashtable = myrule.UserList
        Dim comparator As Comparators = myrule.Comparator

        Select Case comparator
            Case Comparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If UserList.ContainsKey(taskResult.AsignedToId) Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If UserList.ContainsKey(taskResult.AsignedToId) = False Then 'Si encontro al usuario asignado en la lista de usuarios invalidos NO lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.CurrentUser
                If UserList.ContainsKey(Zamba.Membership.MembershipHelper.CurrentUser.ID) Then 'Si encontro al CurrentUser en la lista de usuarios validos lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
            Case Comparators.NotCurrentUser
                If UserList.ContainsKey(Zamba.Membership.MembershipHelper.CurrentUser.ID) = False Then 'Si encontro al CurrentUser en la lista de usuarios invalidos NO lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
        End Select
        Return validUser
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfUsers, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim validUser As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UserList As Hashtable = myrule.UserList
        Dim comparator As Comparators = myrule.Comparator

        Select Case comparator
            Case Comparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If (UserList.ContainsKey(taskResult.AsignedToId)) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If (UserList.ContainsKey(taskResult.AsignedToId) = False) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios invalidos NO lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.CurrentUser
                If (UserList.ContainsKey(Zamba.Membership.MembershipHelper.CurrentUser.ID)) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios validos lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
            Case Comparators.NotCurrentUser
                If (UserList.ContainsKey(Zamba.Membership.MembershipHelper.CurrentUser.ID) = False) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios invalidos NO lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
        End Select
        Return validUser
    End Function
    Public Enum Comparators
        AssignedTo = 0
        NotAsignedTo = 1
        CurrentUser = 2
        NotCurrentUser = 3
    End Enum
End Class

Imports Zamba.Core
Public Class PlayIfUsers

    Private myRule As IIfUsers

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim validUser As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim UserIdsList As List(Of Int64) = myrule.UserIdsList
        Dim comparator As Comparators = myrule.Comparator

        Select Case comparator
            Case Comparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If (UserIdsList.Contains(taskResult.AsignedToId)) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If (UserIdsList.Contains(taskResult.AsignedToId) = False) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios invalidos NO lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.CurrentUser
                If (UserIdsList.Contains(Membership.MembershipHelper.CurrentUser.ID)) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios validos lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
            Case Comparators.NotCurrentUser
                If (UserIdsList.Contains(Membership.MembershipHelper.CurrentUser.ID) = False) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios invalidos NO lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
        End Select
        Return validUser
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal myrule As IIfUsers, ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim validUser As New System.Collections.Generic.List(Of Core.ITaskResult)
        Dim comparator As Comparators = myrule.Comparator

        Select Case comparator
            Case Comparators.AssignedTo
                For Each taskResult As TaskResult In results
                    If (myrule.UserIdsList.Contains(taskResult.AsignedToId)) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios validos lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.NotAsignedTo
                For Each taskResult As TaskResult In results
                    If (myrule.UserIdsList.Contains(taskResult.AsignedToId) = False) = ifType Then 'Si encontro al usuario asignado en la lista de usuarios invalidos NO lo agrego
                        validUser.Add(taskResult)
                    End If
                Next
            Case Comparators.CurrentUser
                If (myrule.UserIdsList.Contains(Membership.MembershipHelper.CurrentUser.ID)) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios validos lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
            Case Comparators.NotCurrentUser
                If (myrule.UserIdsList.Contains(Membership.MembershipHelper.CurrentUser.ID) = False) = ifType Then 'Si encontro al CurrentUser en la lista de usuarios invalidos NO lo agrego
                    For Each taskresult As TaskResult In results
                        validUser.Add(taskresult)
                    Next
                End If
        End Select
        Return validUser
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function
    Public Enum Comparators
        AssignedTo = 0
        NotAsignedTo = 1
        CurrentUser = 2
        NotCurrentUser = 3
    End Enum

    Public Sub New(ByVal rule As IIfUsers)
        Me.myRule = rule
    End Sub
End Class

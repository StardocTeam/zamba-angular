Imports Zamba.Core
Public Class PlayIfUser

    Private myRule As IIfUser

    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return results
    End Function
    Public Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult)
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
                    If (Membership.MembershipHelper.CurrentUser.ID = myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotCurrentUser
                For Each taskResult As TaskResult In results
                    If (Membership.MembershipHelper.CurrentUser.ID <> myrule.UserId) = ifType Then
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
                    If (Membership.MembershipHelper.CurrentUser.ID = myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
            Case UserComparators.NotCurrentUser
                For Each taskResult As TaskResult In results
                    If (Membership.MembershipHelper.CurrentUser.ID <> myrule.UserId) = ifType Then
                        validResults.Add(taskResult)
                    End If
                Next
        End Select
        Return validResults
    End Function

    Public Function PlayTest() As Boolean

    End Function


    Function DiscoverParams() As List(Of String)

    End Function

    Public Sub New(ByVal rule As IIfUser)
        Me.myRule = rule
    End Sub
End Class

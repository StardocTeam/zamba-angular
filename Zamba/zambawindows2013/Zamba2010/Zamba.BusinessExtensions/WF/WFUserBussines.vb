Imports Zamba.Data

Public Class WFUserBusiness
    Public Shared Function GetAsignedUsersCountByStep(ByVal stepid As Int64) As Int32
        Dim UsersCounts As New Int32
        UsersCounts = WFUsersFactory.GetAsignedUsersCountByStep(stepid)
        Return UsersCounts
    End Function
    Public Shared Function GetAsignedUsersCountByWorkflow(ByVal workflowid As Int64) As Int32
        Dim UsersCount As New Int32
        UsersCount = WFUsersFactory.GetAsignedUsersCountByWorkflow(workflowid)
        Return UsersCount
    End Function
    Public Shared Function GetUsersByStepID(ByVal stepId As Int64) As SortedList
        Dim dsFull As DataSet = WFUsersFactory.GetUsersByStepID(stepId)
        Dim sort As SortedList = New SortedList()

        If Not IsNothing(dsFull) Then
            If dsFull.Tables.Count > 0 Then
                For Each row As DataRow In dsFull.Tables(0).Rows
                    If sort.ContainsKey(Int64.Parse(row("ID").ToString())) = False Then
                        Dim usr As IUser = DirectCast(UserBusiness.GetUserById(row.Item("ID")), User)
                        sort.Add(Int64.Parse(row("ID").ToString()), usr)
                    End If
                Next
            End If
        End If

        Return sort
    End Function

    Public Shared Function GetUsersDsByStepID(ByVal stepId As Int64) As DataSet
        Return WFUsersFactory.GetDistinctUsersByStepID(stepId)
    End Function

    Public Shared Function GetGroupsDsByStepID(ByVal stepId As Int64) As DataSet
        Return WFUsersFactory.GetGroupsByStepID(stepId)
    End Function

    'Public Shared Function GetDistinctUsersByStepID(ByVal stepId As Int64) As DataSet
    '    Return WFUsersFactory.GetDistinctUsersByStepID(stepId)
    'End Function
End Class
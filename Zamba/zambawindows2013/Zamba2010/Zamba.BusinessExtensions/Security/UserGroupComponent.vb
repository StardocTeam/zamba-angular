Imports Zamba.Data
'Imports Zamba.Users.Factory
Public Class UserGroupComponent

    Public Shared Function GetAllGroups() As Hashtable
        Return UserGroupBusiness.GetAllGroups()
    End Function
    Public Shared Function GetAllGroupsArrayList() As ArrayList
        Return UserGroupBusiness.GetAllGroupsArrayList()
    End Function
    Public Shared Function GetFilteredAllGroupsArrayList(ByVal SelectedUserGroupsIds As ArrayList) As ArrayList
        Return UserGroupBusiness.GetFilteredAllGroupsArrayList(SelectedUserGroupsIds)
    End Function
    Public Shared Function GetNewGroup(ByVal Name As String) As iusergroup
        Return UserGroupBusiness.GetNewGroup(Name)
    End Function

    Public Shared Function getUserGroups(ByVal UserId As Int32) As ArrayList
        Return UserGroupBusiness.GetUserGroups(UserId)
    End Function

    Public Shared Sub DeleteGroup(ByVal ug As iusergroup)
        UserGroupBusiness.DeleteGroup(ug)
    End Sub
    Public Shared Sub DeleteGroupRights(ByVal groupid As Int32)
        UserGroupComponentFactory.DeleteGroupRights(groupid)
    End Sub

    Public Shared Function RemoveUser(ByVal ug As iusergroup, ByVal u As iuser) As Boolean
        Return UserGroupComponentFactory.RemoveUser(ug, u)
    End Function

    Public Shared Function AssignUser(ByVal u As iuser, ByVal ug As iusergroup) As Boolean
        Return UserGroupComponentFactory.AssignUser(u, ug)
    End Function

End Class
Imports Zamba.Membership

Namespace Cache
    Public Class UsersAndGroups
        Public Shared hsGroupTable As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsGroupsByUserId As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUserTable As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared hsUserGroupsInStep As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUsersInStep As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared hsOptions As New SynchronizedHashtable

        Public Shared hsAllMachineConfigValues As New SynchronizedHashtable

        Public Shared hsUserorGroupIds As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUserorGroupInheritedIds As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUsersNames As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUsersInGroup As SynchronizedHashtable = New SynchronizedHashtable
        Public Shared hsUsersIdsInGroup As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared hsUserRuleViewRight As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared hsUserGroupInheritance As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared hUserPreferencesByUserIdAndName As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared Property hsUserPhotos As SynchronizedHashtable = New SynchronizedHashtable

        Public Shared Property hsEMails As New SynchronizedHashtable

        Friend Shared Sub RemoveCurrentInstance()
            hsGroupTable.Clear()
            hsGroupsByUserId.Clear()
            hsUserTable.Clear()
            hsOptions.Clear()
            hsUserorGroupIds.Clear()
            hsUserorGroupInheritedIds.Clear()
            hsUsersNames.Clear()
            hsUsersInGroup.Clear()
            hsUsersIdsInGroup.Clear()
            hsUserRuleViewRight.Clear()
            hsUserGroupInheritance.Clear()
            hUserPreferencesByUserIdAndName.Clear()
            hsUserGroupsInStep.Clear()
            hsUsersInStep.Clear()
            hsUserPhotos.Clear()
            hsEMails.Clear()
        End Sub



    End Class
End Namespace
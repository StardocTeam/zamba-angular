Namespace Cache
    Public Class UsersAndGroups
        Public Shared Sub ClearAll()
            hsGroupTable.Clear()
            hsUserTable.Clear()
            IndexRightValues.Clear()
            hsModules.Clear()
            hsOptions.Clear()
            hsUserorGroupIds.Clear()
            hsUsersNames.Clear()
            hsUsersInGroup.Clear()
            hsUsersIdsInGroup.Clear()
            hsInheritanceGroups.Clear()
            hsOffspringGroups.Clear()
            hsUserPreferences.Clear()
            hsUserRights.Clear()
            hsInheritanceOfGroup.Clear()
            hsUserGroupsIds.Clear()
            hsUserOrGroupsNames.Clear()
            Permisos.Clear()

            If lstUsers IsNot Nothing Then
                lstUsers.Clear()
            End If
        End Sub

        Public Shared hsGroupTable As Hashtable = New Hashtable
        Public Shared hsUserTable As Hashtable = New Hashtable
        Public Shared IndexRightValues As Hashtable = New Hashtable
        Public Shared hsModules As Hashtable = New Hashtable
        Public Shared hsOptions As New Hashtable
        Public Shared hsUsersNames As Hashtable = New Hashtable
        Public Shared hsUsersInGroup As Hashtable = New Hashtable
        Public Shared hsUsersIdsInGroup As Hashtable = New Hashtable
        Public Shared hsInheritanceGroups As Hashtable = New Hashtable
        Public Shared hsOffspringGroups As Hashtable = New Hashtable
        Public Shared hsUserPreferences As Hashtable = New Hashtable

        Public Shared hsUserRights As Hashtable = New Hashtable
        Public Shared Permisos As New Hashtable

        Public Shared hsInheritanceOfGroup As Hashtable = New Hashtable

        Public Shared hsUserorGroupIds As Hashtable = New Hashtable
        Public Shared hsUserGroupsIds As Hashtable = New Hashtable
        Public Shared hsUserOrGroupsNames As Hashtable = New Hashtable


        Public Shared lstUsers As New ArrayList
    End Class
End Namespace
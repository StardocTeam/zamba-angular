Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections.Generic

'Imports zamba.Users.Factory
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.UserGroupFactory
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Fabrica para trabajar con Grupos de Usuarios de Zamba
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class UserGroupBusiness
    Inherits ZClass

    Private UserGroups As New UserGroups
    Public ReadOnly Property GroupTable() As SynchronizedHashtable
        Get
            'Para que refresque cuando se borra un grupo
            If IsNothing(Cache.UsersAndGroups.hsGroupTable) OrElse Cache.UsersAndGroups.hsGroupTable.Count = 0 Then
                Cache.UsersAndGroups.hsGroupTable = GetGroups()
            End If
            Return Cache.UsersAndGroups.hsGroupTable
        End Get
    End Property

    Private Function GetGroups(Optional ByVal filter As String = "", Optional ByVal Order As String = "NAME") As SynchronizedHashtable
        Dim hsGroupTable As SynchronizedHashtable = New SynchronizedHashtable
        'ACA VA EL CACHE
        Dim ds As DataSet = UserGroupsComponent.GetGroups()
        For Each r As DataRow In ds.Tables(0).Rows
            Dim ug As New UserGroup
            ug.ID = r("ID")
            ug.Name = r("NAME")
            If (IsDBNull(r("DESCRIPCION")) = False) Then
                ug.Description = r("DESCRIPCION")
            Else
                ug.Description = ""
            End If

            If hsGroupTable.ContainsKey(ug.ID) Then
                hsGroupTable(ug.ID) = ug
            Else
                hsGroupTable.Add(ug.ID, ug)
            End If
        Next
        Return hsGroupTable
    End Function




#Region "Funciones Privadas"
    Private Shared Sub Addgroup(ByVal usr As IUserGroup)

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, UserName) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.Group), usr.Name))

        Dim str As String = "insert into usrgroup(id,name) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "')"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, str)
        'hernan: porque comentastes esto ?? donde se carga ??
        'GroupTable.Add(usr.id, usr) ya se carga


    End Sub

    Public Shared Function GetNewGroup(ByVal Name As String) As IUserGroup


        Dim group As New UserGroup
        group.Name = Name
        Try
            group.ID = CoreData.GetNewID(Zamba.Core.IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error al obtener el Id del grupo.", ex)
        End Try
        Try
            Addgroup(group)
            '[Sebastian] 02-07-09 Se agrego validación porque estaba lanzando exception.
            If Cache.UsersAndGroups.hsGroupTable.ContainsKey(group.ID) = False Then
                Cache.UsersAndGroups.hsGroupTable.Add(group.ID, group)
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrió un error registrar el grupo.", ex)
        End Try
        Return group
    End Function
#End Region

#Region "Funciones Publicas"

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna un ARRAYLIST con todos los grupos existentes en Zamba
    ''' </summary>
    ''' <returns>Arraylist de objetos UserGrouops</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetAllGroupsArrayList() As ArrayList
        Dim Groups As New ArrayList
        Groups.AddRange(GroupTable.Values)
        Return Groups

    End Function
    Public Function GetAllGroups() As List(Of IUserGroup)
        Dim Groups As New List(Of IUserGroup)
        Groups.AddRange(GroupTable.Values)
        Return Groups
    End Function

    Public Function GetFilteredAllGroupsArrayList(ByVal SelectedUserGroupsIds As ArrayList) As ArrayList
        Dim UserGroups As New ArrayList
        For Each usergroup As IUserGroup In GroupTable.Values

            If SelectedUserGroupsIds.Contains(CDec(usergroup.ID)) Then
                UserGroups.Add(usergroup)
            End If
        Next
        Return UserGroups
    End Function

    Public Function GetUserGroupsIds(ByVal usrid As Integer) As ArrayList
        Dim arr As New ArrayList

        Dim ds As DataSet
        If Servers.Server.isOracle Then
            Dim strselect As String
            strselect = "Select * from usr_r_group where usrid=" & usrid
            ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Else
            ds = Servers.Server.Con.ExecuteDataset("Zsp_usr_r_group_100_GetUsrRGroupByUsrId ", New Object() {usrid})
        End If
        For Each r As DataRow In ds.Tables(0).Rows
            'TODO: Ver si no existe el grupo  If GroupTable.ContainsKey(CInt(r("GROUPID"))) Then
            arr.Add(r("GROUPID"))
            ' End If
        Next
        Return arr
    End Function
    Public Function GetUserGroup(ByVal userGroupID As Int64) As IUserGroup
        For Each G As IUserGroup In GroupTable.Values
            If G.ID = userGroupID Then
                Return G
            End If
        Next
    End Function
    Public Function GetUserorGroupNamebyId(ByVal userGroupID As Int64, ByRef IsGroup As Boolean) As String
        Return UserGroups.GetUserorGroupNamebyId(userGroupID, IsGroup)
    End Function

    Public Function GetUserOrGroupNamebyIdNonShared(ByVal userGroupID As Int64, ByRef IsGroup As Boolean) As String

        Return UserGroups.GetUserorGroupNamebyId(userGroupID, IsGroup)
    End Function

    ''' <summary>
    ''' Devuelve el id del grupo o usuario en base al nombre
    ''' </summary>
    ''' <param name="userGroupName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <History>
    '''         [Ezequiel] 03/12/09 - Modified, se aplico cache al metodo.
    ''' </History>
    Public Function GetUserorGroupIdbyName(ByVal userGroupName As String) As Int64
        userGroupName = userGroupName.ToLower
        If String.IsNullOrEmpty(userGroupName) Then
            Return 0
        End If
        If Cache.UsersAndGroups.hsUserorGroupIds.ContainsKey(userGroupName) = False Then
            Dim UserOrGroup As Int64 = UserGroups.GetUserorGroupIdbyName(userGroupName)
            SyncLock (Cache.UsersAndGroups.hsUserorGroupIds)
                If Cache.UsersAndGroups.hsUserorGroupIds.ContainsKey(userGroupName) = False Then
                    Cache.UsersAndGroups.hsUserorGroupIds.Add(userGroupName, UserOrGroup)
                Else
                    Cache.UsersAndGroups.hsUserorGroupIds(userGroupName) = UserOrGroup
                End If
            End SyncLock
        End If
        Return Cache.UsersAndGroups.hsUserorGroupIds.Item(userGroupName)
    End Function
    Public Function GetUsersIds(ByVal userGroupID As Int64) As Generic.List(Of Int64)
        Dim ds As New DataSet
        Dim tmpUserIDs As New Generic.List(Of Int64)


        If Cache.UsersAndGroups.hsUsersIdsInGroup.Contains(userGroupID) = False Then
            SyncLock (Cache.UsersAndGroups.hsUsersIdsInGroup)
                Cache.UsersAndGroups.hsUsersIdsInGroup.Add(userGroupID, UserGroups.GetUsr_R_Groups(userGroupID))
            End SyncLock
        End If
        ds = Cache.UsersAndGroups.hsUsersIdsInGroup(userGroupID)
        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                tmpUserIDs.Add(Convert.ToInt64(r("USRID")))
            Next
        End If
        Return tmpUserIDs
    End Function


    ''' <summary>
    ''' Obtiene el id de un grupo de usuario segun el nombre
    ''' </summary>
    ''' <param name="GroupName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetGroupIdByName(ByVal GroupName As String) As Int32
        Return UserGroups.GetGroupIdByName(GroupName)
    End Function



    Public Sub DeleteGroupRights(ByVal groupid As Int32)
        Try
            Dim sql As String = "Delete from Usr_Rights where GROUPID=" & groupid
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
    End Sub

    Public Function GetUsersByGroup(ByVal groupId As Int64) As List(Of IUser)
        If Cache.UsersAndGroups.hsUsersInGroup.Contains(groupId) = False Then
            Dim dt As DataTable = UserGroups.GetUserIds(groupId)
            Dim Users As New List(Of IUser)
            Dim UserBusiness As New UserBusiness
            If Not IsNothing(dt) Then
                Dim UserId As Int64
                For Each CurrentRow As DataRow In dt.Rows
                    UserId = Int64.Parse(CurrentRow.Item("USRID").ToString())
                    Users.Add(UserBusiness.GetUserById(UserId))
                Next
                Cache.UsersAndGroups.hsUsersInGroup.Clear()
                Cache.UsersAndGroups.hsUsersInGroup.Add(groupId, Users)
            Else
                Return New List(Of IUser)
            End If
        End If

        Return Cache.UsersAndGroups.hsUsersInGroup(groupId)
    End Function
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Quita un Usuario de un Grupo asignado
    ''' </summary>
    ''' <param name="ug">Objeto UserGroup</param>
    ''' <param name="u">Objeto User que se desea desasignar</param>
    ''' <returns>boolean, True si se desasigno
    ''' False, si fallo o el usuario NO pertenecia al grupo</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    '''     ''' -----------------------------------------------------------------------------
    Public Function RemoveUser(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
        If GetUsersByGroup(ug.ID).Contains(u) Then 'ug.Users.Contains(u)
            UserFactory.RemoveUser(u, ug)
            ug.Users.Remove(u)
            Return True
        Else
            Return False
        End If
    End Function

    'Public Sub Fill(ByRef instance As UserGroup)
    '    If IsNothing(instance.Users) Then
    '        instance.Users = New ArrayList()
    '        instance.Users.AddRange(UserGroupBusiness.GetUsersByGroup(instance.ID))
    '    End If
    'End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que asigna un usuario a un grupo
    ''' </summary>
    ''' <param name="u">Objeto User que se desea forme parte del grupo</param>
    ''' <param name="ug">Objeto UserGroup</param>
    ''' <returns>Boolean, True si se asigno correctamente
    ''' False si fallo o el mismo YA pertenecia al grupo</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function AssignUser(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        For Each usr As IUser In ug.Users
            If u.ID = usr.ID Then
                Return False
            End If
        Next

        'If Not ug.Users.Contains(u) Then      
        Dim UB As New UserBusiness

        If UB.AssignGroup(u, ug) = True Then
            ug.Users.Add(u)
            Return True
        Else
            Return False
        End If
        'Else
        'Return False
        'End If
    End Function
    Public Overrides Sub Dispose()

    End Sub

    Public Shared Sub ClearHashTables()
        SyncLock (Cache.UsersAndGroups.hsGroupTable)
            Cache.UsersAndGroups.hsGroupTable = New SynchronizedHashtable
        End SyncLock
        '   Zamba.Data.UserGroupFactory.ClearHashTables()
        Zamba.Data.UserGroups.ClearHashTables()
    End Sub



    Public Function getUserGroups(ByVal UserId As Int64) As ArrayList
        Dim Groups As New ArrayList
        If Cache.UsersAndGroups.hsGroupsByUserId.ContainsKey(UserId) = False Then
            Dim GroupsIds As New ArrayList
            Dim UserGroupFactory As New UserGroupFactory
            GroupsIds = UserGroupFactory.GetUserGroupsIds(UserId)
            Dim i As Int32
            For i = 0 To GroupsIds.Count - 1
                If GroupTable.Contains(Long.Parse(GroupsIds(i))) Then
                    Dim UG As IUserGroup = GroupTable(Long.Parse(GroupsIds(i)))
                    Groups.Add(UG)
                End If
            Next

            SyncLock Cache.UsersAndGroups.hsGroupsByUserId.SyncRoot
                If Cache.UsersAndGroups.hsGroupsByUserId.ContainsKey(UserId) = False Then
                    Cache.UsersAndGroups.hsGroupsByUserId.Add(UserId, Groups)
                Else
                    Cache.UsersAndGroups.hsGroupsByUserId(UserId) = Groups
                End If
            End SyncLock

        Else
            Groups = Cache.UsersAndGroups.hsGroupsByUserId(UserId)
        End If
        Return Groups
    End Function

    Public Function GetGroupsAndInheritanceOfGroupsIds(userID As Long, withCache As Boolean) As List(Of Long)
        Try
            Dim GroupsUsersIds As New List(Of Long)
            ' Cargo los grupos a los cuales pertenece el usuario
            GroupsUsersIds = UserFactory.GetUserGroupsIdsByUserid(userID, Not withCache)
            ' Se agrega id de usuario
            GroupsUsersIds.Add(userID)
            Return GroupsUsersIds
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
End Class

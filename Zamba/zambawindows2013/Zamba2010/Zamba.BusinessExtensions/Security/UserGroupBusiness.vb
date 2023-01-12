Imports Zamba.Data

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
    Private Shared ReadOnly Property GroupTable(Optional ByVal isRefresh As Boolean = False) As Hashtable
        Get
            'Para que refresque cuando se borra un grupo
            If IsNothing(Cache.UsersAndGroups.hsGroupTable) OrElse Cache.UsersAndGroups.hsGroupTable.Count = 0 OrElse isRefresh Then
                Cache.UsersAndGroups.hsGroupTable = UserGroupComponentFactory.GetGroups()
            End If
            Return Cache.UsersAndGroups.hsGroupTable
        End Get
    End Property


#Region "Funciones Privadas"
    Private Shared Sub Addgroup(ByVal usr As IUserGroup)

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, Name) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.Group), usr.Name))

        Dim str As String = "insert into usrgroup(id,name) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "')"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, str)
        GroupTable.Add(usr.ID, usr)

    End Sub

    Public Shared Function GetNewGroup(ByVal Name As String) As IUserGroup
        For Each g As IUserGroup In GroupTable.Values
            If g.Name.ToUpper = Name.ToUpper Then
                Throw New ZambaEx("El nombre del grupo ya existe", ZambaEx.MessageIcon.Exclamation)
            End If
        Next

        Dim group As New UserGroup
        group.Name = Name
        Try
            group.ID = CoreData.GetNewID(Zamba.Core.IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error al obtener el Id del grupo.", ex)
        End Try
        Try
            Addgroup(group)
            UserBusiness.Rights.SaveAction(group.ID, ObjectTypes.UserGroups, RightsType.Create, "Se ha creado el grupo " & group.Name & "(" & group.ID & ")")
            '[Sebastian] 02-07-09 Se agrego validación porque estaba lanzando exception.
            If Cache.UsersAndGroups.hsGroupTable.ContainsKey(group.ID) = False Then
                Cache.UsersAndGroups.hsGroupTable.Add(group.ID, group)
            End If
        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error registrar el grupo.", ex)
        End Try
        Return group
    End Function
#End Region

#Region "Funciones Publicas"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Retorna un HASHTABLE con todos los grupos existentes en Zamba
    ''' </summary>
    ''' <returns>HASHTABLE</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetAllGroups() As Hashtable
        Return GroupTable
    End Function
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
    Public Shared Function GetAllGroupsArrayList() As ArrayList
        Return UserGroupComponentFactory.GetGroupsArray
    End Function
    Public Shared Function GetGroupAdminReport() As DataTable
        Return UserGroupComponentFactory.GetGroupAdminReport
    End Function
    Public Shared Function GetFilteredAllGroupsArrayList(ByVal SelectedUserGroupsIds As ArrayList) As ArrayList
        Dim UserGroups As New ArrayList
        For Each usergroup As IUserGroup In GroupTable.Values
            If SelectedUserGroupsIds.Contains(CDec(usergroup.ID)) Then
                UserGroups.Add(usergroup)
            End If
        Next
        Return UserGroups
    End Function

    Public Shared Function GetUserGroupsIds(ByVal usrid As Integer, Optional ByVal forceReloadCache As Boolean = False) As ArrayList
        If Not Cache.UsersAndGroups.hsUserGroupsIds.ContainsKey(usrid) OrElse forceReloadCache Then
            Cache.UsersAndGroups.hsUserGroupsIds.Clear()
            Cache.UsersAndGroups.hsUserGroupsIds.Add(usrid, UserGroupFactory.GetUserGroupsIds(usrid))
        End If
        Return Cache.UsersAndGroups.hsUserGroupsIds.Item(usrid)
    End Function

    Public Shared Function GetAllUsersorGroupsNamesForPreLoad() As Hashtable
        Dim UsersorGroupsNames As Hashtable
        UsersorGroupsNames = UserGroups.GetAllUsersorGroupsNamesForPreLoad()
        For Each id As Int64 In UsersorGroupsNames.Keys
            If Not Cache.UsersAndGroups.hsUserOrGroupsNames.ContainsKey(id) Then
                Cache.UsersAndGroups.hsUserOrGroupsNames.Add(id, UsersorGroupsNames(id))
            End If
        Next
    End Function

    Public Shared Function GetUserorGroupNamebyId(ByVal userGroupID As Int64) As String
        If Not Cache.UsersAndGroups.hsUserOrGroupsNames.ContainsKey(userGroupID) Then
            Cache.UsersAndGroups.hsUserOrGroupsNames.Add(userGroupID, UserGroups.GetUserorGroupNamebyId(userGroupID))
        End If
        Return Cache.UsersAndGroups.hsUserOrGroupsNames.Item(userGroupID)
    End Function

    Public Shared Function GetUserGroup(ByVal userGroupID As Int64) As DataSet
        Return UserGroups.GetUserGroup(userGroupID)
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
    Public Shared Function GetUserorGroupIdbyName(ByVal userGroupName As String, Optional ByVal useCache As Boolean = True) As Int64

        If String.IsNullOrEmpty(userGroupName) Then
            Return 0
        End If

        If Not useCache Then
            Return UserGroups.GetUserorGroupIdbyName(userGroupName)
        End If

        If Not Cache.UsersAndGroups.hsUserorGroupIds.ContainsKey(userGroupName) Then
            Cache.UsersAndGroups.hsUserorGroupIds.Add(userGroupName, UserGroups.GetUserorGroupIdbyName(userGroupName))
        End If

        Return Cache.UsersAndGroups.hsUserorGroupIds.Item(userGroupName)

    End Function
    Public Shared Function GetUsersIds(ByVal userGroupID As Int64, ByVal withcache As Boolean) As Generic.List(Of Int64)
        Dim ds As New DataSet
        Dim tmpUserIDs As New Generic.List(Of Int64)
        If withcache = False Then ds = UserGroups.GetUsr_R_Groups(userGroupID)
        If Cache.UsersAndGroups.hsUsersIdsInGroup.Contains(userGroupID) = False Then
            Cache.UsersAndGroups.hsUsersIdsInGroup.Add(userGroupID, UserGroups.GetUsr_R_Groups(userGroupID))
        End If
        ds = Cache.UsersAndGroups.hsUsersIdsInGroup(userGroupID)

        If Not IsNothing(ds) AndAlso ds.Tables.Count > 0 AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                tmpUserIDs.Add(Convert.ToInt64(r("USRID")))
            Next
        End If
        Return tmpUserIDs
    End Function

    Public Shared Function GetUserGroupAsUserGroup(ByVal userGroupID As Int64) As IUserGroup
        Dim ds As New DataSet()
        Dim tmpUserGroup As New UserGroup()
        ds = UserGroups.GetUserGroup(userGroupID)
        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            For Each r As DataRow In ds.Tables(0).Rows
                tmpUserGroup = New UserGroup(r("ID"), r("NAME"), r("STATE"))
            Next
        End If
        Return tmpUserGroup
    End Function

    ''' <summary>
    ''' Obtiene el id de un grupo de usuario segun el nombre
    ''' </summary>
    ''' <param name="GroupName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGroupIdByName(ByVal GroupName As String) As Int32
        Return UserGroups.GetGroupIdByName(GroupName)
    End Function

    Public Shared Function GetUserGroupsNamesByUserId(ByVal UserId As Int64) As List(Of String)

        Dim userGroups As ArrayList = getUserGroups(UserId)
        Dim groupName As String
        Dim tmpListUserGroupsName As New List(Of String)

        Try
            If Not IsNothing(userGroups) AndAlso userGroups.Count > 0 Then
                For Each group As UserGroup In userGroups
                    tmpListUserGroupsName.Add(group.Name)
                Next
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return tmpListUserGroupsName

    End Function

    Public Shared Function GetUserGroups() As Generic.List(Of UserGroup)

        Dim ds As New DataSet()
        ds = UserGroups.GetUserGoupsAsDataSet()
        Dim tmpUserGroup As IUserGroup
        Dim tmpListUserGroup As New Generic.List(Of UserGroup)

        Try
            If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
                For Each r As DataRow In ds.Tables(0).Rows
                    tmpUserGroup = New UserGroup(r("ID"), r("NAME"), r("STATE"))
                    tmpListUserGroup.Add(tmpUserGroup)
                Next
            End If
        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return tmpListUserGroup

    End Function
    Public Shared Function getUserGroups(ByVal UserId As Int32, Optional ByVal reloadCache As Boolean = False) As ArrayList
        Dim Groups As New ArrayList
        Dim GroupsIds As New ArrayList
        GroupsIds = GetUserGroupsIds(UserId, reloadCache)
        Dim i As Int32
        For i = 0 To GroupsIds.Count - 1
            If GetAllGroups.Contains(Long.Parse(GroupsIds(i))) Then
                'Cargo los permisos en cache para el grupo, para mejorar la performance luego al querer usarlos.
                Dim UG As IUserGroup = GetAllGroups(Long.Parse(GroupsIds(i)))
                Dim Rights = RightFactory.GroupRights(UG.ID)
                Groups.Add(UG)
            End If
        Next
        Return Groups
    End Function


    Public Shared Sub DeleteGroup(ByVal ug As IUserGroup)
        UserGroupComponentFactory.DeleteGroupRights(ug.ID)
        UserGroupComponentFactory.DeleteGroup(ug.ID)
        UserBusiness.Rights.SaveAction(ug.ID, ObjectTypes.UserGroups, RightsType.Delete, "Se ha eliminado el grupo " & ug.Name & "(" & ug.ID & ")")
        GroupTable.Remove(ug.ID)
    End Sub

    Public Shared Function GetUsersByGroup(ByVal groupId As Int64) As List(Of IUser)
        If Cache.UsersAndGroups.hsUsersInGroup.Contains(groupId) = False Then
            Dim dt As DataTable = UserGroups.GetUserIds(groupId)
            Dim Users As New List(Of IUser)

            If Not IsNothing(dt) Then
                Dim UserId As Int64
                For Each CurrentRow As DataRow In dt.Rows
                    UserId = Int64.Parse(CurrentRow.Item("USRID").ToString())
                    Users.Add(UserBusiness.GetUserById(UserId))
                Next
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
    Public Shared Function RemoveUser(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
        UserFactory.RemoveUser(u, ug)
        UserBusiness.Rights.SaveAction(u.ID, ObjectTypes.Users, RightsType.Edit, "Se ha quitado el usuario " & u.Name & "(" & u.ID & ") del grupo " & ug.Name & "(" & ug.ID & ")")
        If ug.Users.Contains(u) Then
            ug.Users.Remove(u)
        End If
        Return True
    End Function

    Public Shared Sub Fill(ByRef instance As UserGroup)
        If IsNothing(instance.Users) Then
            instance.Users = New ArrayList()
            instance.Users.AddRange(UserGroupBusiness.GetUsersByGroup(instance.ID))
        End If
    End Sub

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
    Public Shared Function AssignUser(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        For Each usr As IUser In ug.Users
            If u.ID = usr.ID Then
                Return False
            End If
        Next

        'If Not ug.Users.Contains(u) Then
        If UserBusiness.AssignGroup(u, ug) = True Then
            UserBusiness.Rights.SaveAction(u.ID, ObjectTypes.Users, RightsType.Edit, "Se ha agregado el usuario " & u.Name & "(" & u.ID & ") al grupo " & ug.Name & "(" & ug.ID & ")")
            UserBusiness.Rights.SaveAction(u.ID, ObjectTypes.UserGroups, RightsType.Edit, "Se ha agregado el usuario " & u.Name & "(" & u.ID & ") al grupo " & ug.Name & "(" & ug.ID & ")")
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



    ''' <summary>
    ''' Obtiene todos los grupos padres de los cual hereda. De quienes hereda.
    ''' </summary>
    ''' <param name="Groupid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Shared Function getInheritance(ByVal Groupid As Int64, ByVal isReload As Boolean) As List(Of ICore)
        SyncLock (Cache.UsersAndGroups.hsInheritanceGroups)
            If isReload Then
                If Cache.UsersAndGroups.hsInheritanceGroups.ContainsKey(Groupid) Then
                    Cache.UsersAndGroups.hsInheritanceGroups.Remove(Groupid)
                End If
            End If
            If Cache.UsersAndGroups.hsInheritanceGroups.ContainsKey(Groupid) = False Then

                Cache.UsersAndGroups.hsInheritanceGroups.Add(Groupid, getInheritanceOfGroup(Groupid))

            End If
            Return Cache.UsersAndGroups.hsInheritanceGroups(Groupid)
        End SyncLock
    End Function

    Public Shared Function getInheritanceOfGroup(ByVal GroupID As Int64) As List(Of ICore)
        SyncLock (Cache.UsersAndGroups.hsInheritanceOfGroup)
            'Si no estan cargados los permisos, los cargo
            If Cache.UsersAndGroups.hsInheritanceOfGroup.Contains(GroupID) = False Then

                Dim usrGroupFactory As UserGroupFactory = New UserGroupFactory()
                Dim lstGroups As List(Of ICore) = New List(Of ICore)
                Dim dt As DataTable = usrGroupFactory.getInheritance(GroupID)
                If dt.Rows.Count <> 0 Then
                    For Each row As DataRow In dt.Rows
                        Dim group As UserGroup = New UserGroup
                        group.ID = row("ID")
                        group.Name = row("Name")
                        lstGroups.Add(group)
                        group.Dispose()
                        Dim lst As List(Of ICore) = getInheritanceOfGroup(group.ID)
                        For Each child As ICore In lst
                            If Not lstGroups.Contains(DirectCast(child, Zamba.Core.UserGroup), New CoreComparer) Then
                                lstGroups.Add(child)
                            End If
                        Next
                        lst = Nothing
                    Next

                End If
                dt.Dispose()

                usrGroupFactory.Dispose()
                usrGroupFactory = Nothing

                Cache.UsersAndGroups.hsInheritanceOfGroup.Add(GroupID, lstGroups)
                Return lstGroups


            Else
                Return Cache.UsersAndGroups.hsInheritanceOfGroup(GroupID)
            End If
        End SyncLock

    End Function

End Class
''''''Imports Zamba.Data
''''''Imports Zamba.Core
''''''''' -----------------------------------------------------------------------------
''''''''' Project	 : Zamba.Data
''''''''' Class	 : Data.UserGroupFactory
''''''''' 
''''''''' -----------------------------------------------------------------------------
''''''''' <summary>
''''''''' Fabrica para trabajar con Grupos de Usuarios de Zamba
''''''''' </summary>
''''''''' <remarks>
''''''''' </remarks>
''''''''' <history>
''''''''' 	[Hernan]	29/05/2006	Created
''''''''' </history>
''''''''' -----------------------------------------------------------------------------
''''''Public Class UserGroupFactory
''''''    Inherits ZClass
''''''    Public Overrides Sub Dispose()

''''''    End Sub
''''''#Region "Funciones Privadas"
''''''    Private Shared _GroupTable As Hashtable
''''''    ' Private Shared TableLoaded As Boolean = False
''''''    Private Shared ReadOnly Property GroupTable() As Hashtable
''''''        Get
''''''            'Para que refresque cuando se borra un grupo
''''''            If IsNothing(_GroupTable) Then
''''''                _GroupTable = New Hashtable
''''''                _GetGroups()
''''''            End If
''''''            Return _GroupTable
''''''        End Get
''''''    End Property
''''''    Private Shared Sub Addgroup(ByVal usr as iusergroup)
''''''        Dim str As String = "insert into usrgroup(id,name) values(" & usr.Id & ",'" & usr.Name.Replace("'", "") & "')"
''''''        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, str)
''''''        'hernan: porque comentastes esto ?? donde se carga ??
''''''        'GroupTable.Add(usr.id, usr) ya se carga

''''''        GroupTable.Add(usr.Id, usr)
''''''    End Sub
''''''    Private Shared Sub _GetGroups(Optional ByVal filter As String = "", Optional ByVal Order As String = "NAME")
''''''        Dim strselect As String = "select * from usrgroup " & filter & " order by " & Order
''''''        Dim ds As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
''''''        For Each r As DataRow In ds.Tables(0).Rows
''''''            Dim ug As New UserGroup
''''''            ug.Id = r("ID")
''''''            ug.Name = r("NAME")
''''''            Try
''''''                ug.Description = r("DESCRIPCION")
''''''            Catch
''''''                ug.Description = ""
''''''            End Try

''''''            If _GroupTable.ContainsKey(ug.Id) Then
''''''                _GroupTable(ug.Id) = ug
''''''            Else
''''''                _GroupTable.Add(ug.Id, ug)
''''''            End If
''''''        Next
''''''        ' TableLoaded = True
''''''    End Sub
''''''#End Region

''''''#Region "Funciones Publicas"
''''''    ''' -----------------------------------------------------------------------------
''''''    ''' <summary>
''''''    ''' Retorna un HASHTABLE con todos los grupos existentes en Zamba
''''''    ''' </summary>
''''''    ''' <returns>HASHTABLE</returns>
''''''    ''' <remarks>
''''''    ''' </remarks>
''''''    ''' <history>
''''''    ''' 	[Hernan]	29/05/2006	Created
''''''    ''' </history>
''''''    ''' -----------------------------------------------------------------------------
''''''    Public Shared Function GetAllGroups() As Hashtable
''''''        Return GroupTable
''''''    End Function
''''''    ''' -----------------------------------------------------------------------------
''''''    ''' <summary>
''''''    ''' Retorna un ARRAYLIST con todos los grupos existentes en Zamba
''''''    ''' </summary>
''''''    ''' <returns>Arraylist de objetos UserGrouops</returns>
''''''    ''' <remarks>
''''''    ''' </remarks>
''''''    ''' <history>
''''''    ''' 	[Hernan]	29/05/2006	Created
''''''    ''' </history>
''''''    ''' -----------------------------------------------------------------------------
''''''    Public Shared Function GetAllGroupsArrayList() As ArrayList
''''''        Dim Groups As New ArrayList
''''''        Groups.AddRange(GroupTable.Values)
''''''        Return Groups
''''''    End Function
''''''    Public Shared Function GetFilteredAllGroupsArrayList(ByVal SelectedUserGroupsIds As ArrayList) As ArrayList
''''''        Dim UserGroups As New ArrayList
''''''        For Each usergroup as iusergroup In GroupTable.Values
''''''            If SelectedUserGroupsIds.Contains(CDec(usergroup.Id)) Then
''''''                UserGroups.Add(usergroup)
''''''            End If
''''''        Next
''''''        Return UserGroups
''''''    End Function
''''''    Public Shared Function GetNewGroup(ByVal Name As String) as iusergroup
''''''        For Each g as iusergroup In GroupTable.Values
''''''            If String.Compare(g.Name, Name) = 0 Then
''''''                Throw New Exception("El nombre del grupo ya existe")
''''''            End If
''''''        Next

''''''        Dim group As New UserGroup
''''''        group.Name = Name
''''''        Try
''''''            group.ID = CoreBusiness.GetNewID(IdTypes.USERTABLEID)
''''''        Catch ex As Exception
''''''            Throw New Exception("Ocurrió un error al obtener el Id del grupo.", ex)
''''''        End Try
''''''        Try
''''''            Addgroup(group)
''''''        Catch ex As Exception
''''''            Throw New Exception("Ocurrió un error registrar el grupo.", ex)
''''''        End Try
''''''        Return group
''''''    End Function
''''''    Public Shared Function GetUserGroupsIds(ByVal usrid As Integer) As ArrayList
''''''        Dim arr As New ArrayList
''''''        Dim strselect As String = "Select * from usr_r_group where usrid=" & usrid
''''''        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
''''''        For Each r As DataRow In ds.Tables(0).Rows
''''''            'TODO: Ver si no existe el grupo  If GroupTable.ContainsKey(CInt(r("GROUPID"))) Then
''''''            arr.Add(r("GROUPID"))
''''''            ' End If
''''''        Next
''''''        Return arr
''''''    End Function
''''''    Public Shared Function getUserGroups(ByVal UserId As Int32) As ArrayList
''''''        Dim Groups As New ArrayList
''''''        Dim GroupsIds As New ArrayList
''''''        GroupsIds = GetUserGroupsIds(UserId)
''''''        Dim i As Int32
''''''        For i = 0 To GroupsIds.Count - 1
''''''            If GetAllGroups.Contains(Long.Parse(GroupsIds(i))) Then
''''''                Groups.Add(GetAllGroups(Long.Parse(GroupsIds(i))))
''''''            End If
''''''        Next
''''''        Return Groups
''''''    End Function
''''''    Public Shared Function Update(ByVal Group as iusergroup) As Boolean
''''''        Dim strupdate As New System.Text.StringBuilder
''''''        strupdate.Append("update usrgroup set ")
''''''        If Group.Description.Length > 100 Then Group.Description = Group.Description.Substring(0, 99)
''''''        If Group.Name.Length > 20 Then Group.Name = Group.Name = Group.Name.Substring(0, 19)

''''''        strupdate.Append("NAME='")
''''''        strupdate.Append(Group.Name.Trim.Replace("'", "''"))
''''''        strupdate.Append("'")
''''''        strupdate.Append(",Descripcion='")
''''''        strupdate.Append(Group.Description.Trim.Replace("'", "''"))
''''''        strupdate.Append("'")
''''''        strupdate.Append(",STATE='")
''''''        strupdate.Append(Group.State)
''''''        strupdate.Append("'")

''''''        strupdate.Append(" where ID=")
''''''        strupdate.Append(Group.Id)

''''''        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
''''''    End Function
''''''    Public Shared Sub DeleteGroup(ByVal ug as iusergroup)
''''''        Try
''''''            DeleteGroupRights(ug.Id)
''''''            Dim strDel As String = "delete usr_r_group where groupid = " & ug.Id
''''''            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
''''''            strDel = "Delete from UsrGroup where ID=" & ug.Id
''''''            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
''''''            GroupTable.Remove(ug.Id)
''''''        Catch ex As Exception
''''''            zamba.core.zclass.raiseerror(ex)
''''''        End Try
''''''    End Sub
''''''    Public Shared Sub DeleteGroupRights(ByVal groupid As Int32)
''''''        Try
''''''            Dim sql As String = "Delete from Usr_Rights where GROUPID=" & groupid
''''''            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
''''''        Catch
''''''        End Try
''''''    End Sub

''''''#End Region

''''''    ''' -----------------------------------------------------------------------------
''''''    ''' <summary>
''''''    ''' Quita un Usuario de un Grupo asignado
''''''    ''' </summary>
''''''    ''' <param name="ug">Objeto UserGroup</param>
''''''    ''' <param name="u">Objeto User que se desea desasignar</param>
''''''    ''' <returns>boolean, True si se desasigno
''''''    ''' False, si fallo o el usuario NO pertenecia al grupo</returns>
''''''    ''' <remarks>
''''''    ''' </remarks>
''''''    ''' <history>
''''''    ''' 	[Hernan]	29/05/2006	Created
''''''    ''' </history>
''''''    '''     ''' -----------------------------------------------------------------------------
''''''    Public Shared Function RemoveUser(ByVal ug as iusergroup, ByVal u as iuser) As Boolean
''''''        If ug.Users.Contains(u) Then
''''''            UserBusiness.RemoveUser(u, ug)
''''''            ug.Users.Remove(u)
''''''            Return True
''''''        Else
''''''            Return False
''''''        End If
''''''    End Function

''''''    ''' -----------------------------------------------------------------------------
''''''    ''' <summary>
''''''    ''' Funcion que asigna un usuario a un grupo
''''''    ''' </summary>
''''''    ''' <param name="u">Objeto User que se desea forme parte del grupo</param>
''''''    ''' <param name="ug">Objeto UserGroup</param>
''''''    ''' <returns>Boolean, True si se asigno correctamente
''''''    ''' False si fallo o el mismo YA pertenecia al grupo</returns>
''''''    ''' <remarks>
''''''    ''' </remarks>
''''''    ''' <history>
''''''    ''' 	[Hernan]	29/05/2006	Created
''''''    ''' </history>
''''''    ''' -----------------------------------------------------------------------------
''''''    Public Shared Function AssignUser(ByVal u as iuser, ByVal ug as iusergroup) As Boolean
''''''        If Not ug.Users.Contains(u) Then
''''''            UserBusiness.AssignGroup(u, ug)
''''''            ug.Users.Add(u)
''''''            Return True
''''''        Else
''''''            Return False
''''''        End If
''''''    End Function
''''''End Class
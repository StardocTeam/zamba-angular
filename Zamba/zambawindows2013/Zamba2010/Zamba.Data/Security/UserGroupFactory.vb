Imports Zamba.Core
Imports System.Text

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
Public Class UserGroupFactory
    Inherits ZClass

#Region "Funciones Privadas"
    Private Shared _GroupTable As Hashtable
    ' Private Shared TableLoaded As Boolean = False
    Private Shared ReadOnly Property GroupTable() As Hashtable
        Get
            'Para que refresque cuando se borra un grupo
            If IsNothing(_GroupTable) Then
                _GroupTable = New Hashtable
                _GetGroups()
            End If
            Return _GroupTable
        End Get
    End Property
    Private Shared Sub Addgroup(ByVal usr As IUserGroup)

        Server.Con.ExecuteNonQuery(CommandType.Text, String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, UserName) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.Group), usr.Name))

        Dim str As String = "insert into usrgroup(id,name) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, str)
        'hernan: porque comentastes esto ?? donde se carga ??
        'GroupTable.Add(usr.id, usr) ya se carga

        GroupTable.Add(usr.ID, usr)
    End Sub
    Private Shared Sub _GetGroups(Optional ByVal filter As String = "", Optional ByVal Order As String = "NAME")
        Dim strselect As String = "select * from usrgroup " & filter & " order by " & Order
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        For Each r As DataRow In ds.Tables(0).Rows
            Dim ug As New UserGroup
            ug.ID = r("ID")
            ug.Name = r("NAME")
            Try
                ug.Description = r("DESCRIPCION")
            Catch
                ug.Description = ""
            End Try

            If _GroupTable.ContainsKey(ug.ID) Then
                _GroupTable(ug.ID) = ug
            Else
                _GroupTable.Add(ug.ID, ug)
            End If
        Next
        ' TableLoaded = True
    End Sub
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
        Dim Groups As New ArrayList
        Groups.AddRange(GroupTable.Values)
        Return Groups
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
    Public Shared Function GetNewGroup(ByVal Name As String) As IUserGroup
        For Each g As IUserGroup In GroupTable.Values
            If String.Compare(g.Name, Name) = 0 Then
                Throw New Exception("El nombre del grupo ya existe")
            End If
        Next

        Dim group As New UserGroup
        group.Name = Name
        Try
            group.ID = CoreData.GetNewID(IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error al obtener el Id del grupo.", ex)
        End Try
        Try
            Addgroup(group)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error registrar el grupo.", ex)
        End Try
        Return group
    End Function
    Public Shared Function GetUserGroupsIds(ByVal usrid As Integer) As ArrayList
        Dim arr As New ArrayList
        Dim strselect As String = "Select * from usr_r_group where usrid=" & usrid
        Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
        For Each r As DataRow In ds.Tables(0).Rows
            'TODO: Ver si no existe el grupo  If GroupTable.ContainsKey(CInt(r("GROUPID"))) Then
            arr.Add(r("GROUPID"))
            ' End If
        Next
        Return arr
    End Function

    Public Shared Function getUserGroups(ByVal UserId As Int32) As ArrayList
        Dim Groups As New ArrayList
        Dim GroupsIds As New ArrayList
        GroupsIds = GetUserGroupsIds(UserId)
        Dim i As Int32
        For i = 0 To GroupsIds.Count - 1
            If GetAllGroups.Contains(Long.Parse(GroupsIds(i))) Then
                Groups.Add(GetAllGroups(Long.Parse(GroupsIds(i))))
            End If
        Next
        Return Groups
    End Function


    ''' <summary>
    ''' Actualiza datos de un grupo
    ''' </summary>
    ''' <param name="Group">Grupo al que se le actualizaran datos</param>
    ''' <returns></returns>
    Public Shared Function UpdateGroup(ByVal Group As IUserGroup) As Boolean
        Dim strupdate As New Text.StringBuilder
        Try
            ZClass.raiseerror(New Exception("Se actualiza grupo: " & Group.Name))
            strupdate.Append("update usrgroup set ")

            strupdate.Append("NAME='")
            strupdate.Append(Group.Name.Trim.Replace("'", "''"))
            strupdate.Append("'")
            strupdate.Append(",Descripcion='")
            strupdate.Append(Group.Description.Trim.Replace("'", "''"))
            strupdate.Append("'")
            strupdate.Append(",STATE='")
            strupdate.Append(Group.State)
            strupdate.Append("'")

            strupdate.Append(" where ID=")
            strupdate.Append(Group.ID)

            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        Finally
            strupdate = Nothing
        End Try
    End Function
    Public Shared Sub DeleteGroup(ByVal ug As IUserGroup)
        Try
            DeleteGroupRights(ug.ID)
            Dim strDel As String = "delete usr_r_group where groupid = " & ug.ID
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
            strDel = "Delete from UsrGroup where ID=" & ug.ID
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
            GroupTable.Remove(ug.ID)
        Catch ex As Exception
            raiseerror(ex)
        End Try
    End Sub
    Public Shared Sub DeleteGroupRights(ByVal groupid As Int32)
        Try
            Dim sql As String = "Delete from Usr_Rights where GROUPID=" & groupid
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
    End Sub

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
        If ug.Users.Contains(u) Then
            UserFactory.RemoveUser(u, ug)
            ug.Users.Remove(u)
            Return True
        Else
            Return False
        End If
    End Function

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
        If Not ug.Users.Contains(u) Then
            UserFactory.AssignGroup(u, ug)
            ug.Users.Add(u)
            Return True
        Else
            Return False
        End If
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Function getInheritance(ByVal groupid As Int64) As DataTable
        If Server.isOracle = True Then
            Dim strbuilder As New StringBuilder()
            strbuilder.Append("select distinct group_r_group.inheritedUserGroup as ID,usrgroup.name  from group_r_group ")
            strbuilder.Append("inner join usrgroup on group_r_group.inheritedUserGroup = usrgroup.ID ")
            strbuilder.Append("where usergroup = ")
            strbuilder.Append(groupid)

            Return Server.Con.ExecuteDataset(CommandType.Text, strbuilder.ToString()).Tables(0)
        Else
            Dim parameters() As Object = {groupid}
            Try
                Return Server.Con.ExecuteDataset("zsp_100_getInheritance", parameters).Tables(0)
            Catch
                Dim dt As New DataTable

                dt.Columns.Add("ID")
                dt.Columns.Add("name")

                Return dt
            End Try
        End If
    End Function
End Class
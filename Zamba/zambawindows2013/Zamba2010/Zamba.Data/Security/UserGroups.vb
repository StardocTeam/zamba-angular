Imports System.Text
Imports Zamba.Core

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Data
''' Class	 : Data.UserGroups
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear grupos de usuarios
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class UserGroups
    Inherits ZClass
    '    Implements IUser
    Public Overrides Sub Dispose()
        Try
            Server.Con.dispose()
        Catch
        End Try
    End Sub
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    '    Private _type As IUser.Usertypes = IUser.Usertypes.Group

    'Public Property Type() As IUser.Usertypes Implements IUser.Type
    '    Get
    '        Return _type
    '    End Get
    '    Set(ByVal Value As IUser.Usertypes)
    '        _type = Value
    '    End Set
    'End Property

    'Private _Id As Int32
    'Public Property Id() As Int32 Implements IUser.Id
    '    Get
    '        Return _Id
    '    End Get
    '    Set(ByVal Value As Int32)
    '        _Id = Value
    '    End Set
    'End Property

    'Private _pictureId As Int32
    'Public Property PictureId() As Int32 Implements IUser.PictureId
    '    Get
    '        Return _pictureId
    '    End Get
    '    Set(ByVal Value As Int32)
    '        _pictureId = Value
    '    End Set
    'End Property

    'Private _Name As String

    'Public Property Name() As String Implements IUser.Name
    '    Get
    '        Return _Name
    '    End Get
    '    Set(ByVal Value As String)
    '        _Name = Value
    '    End Set
    'End Property

    'Public Rights As Integer
    'Public CreateDate As String
    'Public State As Boolean

    Public Shared ReadOnly Property GroupName(ByVal Group_Id As Integer) As String
        Get
            Dim strSelect As String = ("SELECT User_Group_Name FROM User_Group where ( User_Group_Id = " & Group_Id & ")")
            Try
                Return Server.Con.ExecuteScalar(CommandType.Text, strSelect).ToString
            Catch ex As Exception
                Return "0"
            End Try
        End Get
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Persiste un grupo de usuarios en la base de datos
    ''' </summary>
    ''' <param name="NewGroup">Objeto UserGroups</param>
    ''' <returns>Integer, devuelve el Id del nuevo grupo guardado</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function AddUserGroup(ByVal NewGroup As IUserGroup) As Integer
        Dim ColState As Integer
        If NewGroup.State = True Then
            ColState = 1
        Else
            ColState = 0
        End If

        Dim Columns As String = "User_GROUP_Id, User_GROUP_Name, User_GROUP_State, User_GROUP_CrDate"
        Dim Values As String = CoreData.GetNewID(IdTypes.USERGROUPID) & ",'" & NewGroup.Name & "', " & ColState & "," & "CONVERT(DATETIME, '" & NewGroup.CreateDate & "', 102)"
        Dim strInsert As String

        If Server.IsOracle Then
            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
            strInsert = utilities.Convert_Datetime(strInsert)
        Else
            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
        End If
        Try
            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
        Catch ex As Exception
            'MessageBox.Show("Ocurrio un error al agregar el Grupo de usuarios" & ex.ToString, "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            raiseerror(ex)
        End Try
        Return NewGroup.ID
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza los cambios de un grupo en la base de datos
    ''' </summary>
    ''' <param name="Usergroup">Objeto que se ha modificado y se desea persistir sus modificaciones</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateUserGroup(ByVal Usergroup As IUserGroup)
        Dim ColState As Integer
        If Usergroup.State = True Then
            ColState = 1
        Else
            ColState = 0
        End If

        Dim Updatestring As String = "User_group_Name = '" & Usergroup.Name & "', User_group_State = " & ColState
        Dim strUpdate As String = "UPDATE User_group SET " & Updatestring & " WHERE USER_group_ID = " & Usergroup.ID & String.Empty
        If Server.IsOracle Then
            strUpdate = utilities.Convert_Datetime(strUpdate)
        End If

        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Elimina un Grupo de usuarios
    ''' </summary>
    ''' <param name="UsergroupID">Id del grupo que se desea eliminar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DelUserGroup(ByVal UsergroupID As Integer)
        Dim strDelete As String = "DELETE from User_group where (user_group_id = " & UsergroupID & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un dataset tipeado con los Grupos existentes
    ''' </summary>
    ''' <returns>DSUserGroup</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserGroups() As DSUserGroup
        Dim DsUserGroups As New DSUserGroup
        Dim DSTEMP As DataSet
        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "User_Group"
        DsUserGroups.Merge(DSTEMP)
        Return DsUserGroups
    End Function
    Public Shared Function GetUsr_R_Groups(ByVal groupID As Int64) As DataSet
        Dim ds As New DataSet
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("SELECT * FROM USR_R_GROUP WHERE GROUPID = '")
        sqlBuilder.Append(groupID.ToString())
        sqlBuilder.Append("'")
        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
        Return ds
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que devuelve un Arraylist de objetos USERGroup
    ''' </summary>
    ''' <returns>Arraylist de objetos UserGroup</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserGroupsArrayList() As ArrayList
        Dim DsUserGroups As New DSUserGroup
        Dim DSTEMP As DataSet
        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
        DSTEMP.Tables(0).TableName = "User_Group"
        DsUserGroups.Merge(DSTEMP)
        Dim UserGroups As New ArrayList
        Dim i As Int32
        For i = 0 To DsUserGroups.User_Group.Count - 1
            UserGroups.Add(New UserGroup(DsUserGroups.User_Group(i).User_Group_ID, DsUserGroups.User_Group(i).User_Group_Name, DsUserGroups.User_Group(i).User_Group_State))
        Next
        Return UserGroups
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si existe un Grupo con el nombre pasado como parametro
    ''' </summary>
    ''' <param name="UserGroupName">Nombre del grupo que se desea verificar su existencia</param>
    ''' <returns>Boolean, TRUE existe</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsUserGroupDuplicated(ByVal UserGroupName As String) As Boolean
        Try
            Dim strSelect As String = "SELECT COUNT(User_group_id) from USER_group where (User_group_Name = '" & UserGroupName.Trim & "')"
            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
            If Qrows > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Ocurrio un error al consultar la duplicidad del Grupo de Usuario " & ex.ToString)
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve un DataSet con todos los datos del grupID pasado por parámetro
    ''' </summary>
    ''' <param name="userGroupID">Id del grupo a buscar</param>
    ''' <history> [Alejandro] </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserGroup(ByVal userGroupID As Int64) As DataSet
        Dim ds As New DataSet()
        Dim sqlBuilder As New StringBuilder()

        sqlBuilder.Append("SELECT * FROM USRGROUP WHERE ID ='")
        sqlBuilder.Append(userGroupID.ToString())
        sqlBuilder.Append("'")
        ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())

        Return ds
    End Function

    Public Shared Function GetAllUsersorGroupsNamesForPreLoad() As Hashtable
        Dim UsersAndGroupsNames As New Hashtable
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, "SELECT ID,NAME FROM USRTABLE UNION SELECT ID,NAME FROM USRGROUP")
        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                If Not UsersAndGroupsNames.ContainsKey(Int64.Parse(row(0).ToString())) Then UsersAndGroupsNames.Add(Int64.Parse(row(0).ToString()), row(1).ToString())
            Next
        End If
        Return UsersAndGroupsNames
    End Function
    Public Shared Function GetUserorGroupNamebyId(ByVal UserorGroupID As Int64) As String
        Dim sqlBuilder As New StringBuilder()

        If UserorGroupID <> 0 Then
            sqlBuilder.Append("SELECT NAME FROM USRTABLE WHERE ID =")
            sqlBuilder.Append(UserorGroupID)
            Dim name As String = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            If Not IsNothing(name) AndAlso String.Compare(name, String.Empty) <> 0 Then
                Return name
            Else
                sqlBuilder.Remove(0, sqlBuilder.Length)
                sqlBuilder.Append("SELECT NAME FROM USRGROUP WHERE ID =")
                sqlBuilder.Append(UserorGroupID)
                name = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
                If Not IsNothing(name) AndAlso String.Compare(name, String.Empty) <> 0 Then
                    Return name
                Else
                    Return String.Empty
                End If
            End If
        Else
            Return String.Empty
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Devuelve el nombre del grupo
    ''' </summary>
    ''' <param name="userGroupID">Id del grupo a buscar</param>
    ''' <history> [Marcelo] Created
    '''           [Marcelo] Modified    23/06/09    Devolvia string.empty en lugar de 0 
    '''</history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserorGroupIdbyName(ByVal UserorGroupName As String) As Int64
        'Ojo puede haber Usuarios y grupos con el mismo nombre, con el cual usaria el usuario

        Dim sqlBuilder As New StringBuilder()

        If Not Server.isOracle Then

            sqlBuilder.Append("SELECT ID FROM USRTABLE WHERE Name ='")
            sqlBuilder.Append(UserorGroupName)
            sqlBuilder.Append("'")
            Dim id As Int64 = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            If id <> 0 Then
                Return id
            Else
                sqlBuilder.Remove(0, sqlBuilder.Length)
                sqlBuilder.Append("SELECT ID FROM USRGROUP WHERE NAME ='")
                sqlBuilder.Append(UserorGroupName)
                sqlBuilder.Append("'")
                id = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
                If id <> 0 Then
                    Return id
                Else
                    Return 0
                End If
            End If
        Else
            sqlBuilder.Append("SELECT ID FROM USRTABLE WHERE rtrim(ltrim(Name)) ='")
            sqlBuilder.Append(UserorGroupName)
            sqlBuilder.Append("'")
            Dim id As Int64 = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
            If id <> 0 Then
                Return id
            Else
                sqlBuilder.Remove(0, sqlBuilder.Length)
                sqlBuilder.Append("SELECT ID FROM USRGROUP WHERE rtrim(ltrim(Name))  ='")
                sqlBuilder.Append(UserorGroupName)
                sqlBuilder.Append("'")
                id = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
                If id <> 0 Then
                    Return id
                Else
                    Return 0
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Obtiene el id de un grupo de usuario segun el nombre
    ''' </summary>
    ''' <param name="GroupName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetGroupIdByName(ByVal GroupName As String) As Int32
        Dim sqlBuilder As New StringBuilder()
        Dim id As Int32
        sqlBuilder.Append("SELECT ID FROM USRGROUP WHERE NAME ='")
        sqlBuilder.Append(GroupName)
        sqlBuilder.Append("'")
        id = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
        Return id
    End Function

    Public Shared Function GetUserGoupsAsDataSet() As DataSet
        Dim sqlBuilder As New StringBuilder()
        sqlBuilder.Append("SELECT * FROM USRGROUP ORDER BY NAME")
        Return Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
    End Function
#Region "Members"
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Agrega un usuario a un grupo de usuarios
    ''' </summary>
    ''' <param name="UserId">Id del usuario que se desea agregar</param>
    ''' <param name="UserGroupId">Id del grupo</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub AddMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
        Dim StrInsert As String = "INSERT INTO USER_R_USER_GROUP (USER_ID,USER_GROUP_ID) VALUES (" & UserId & "," & UserGroupId & ")"
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Quita a un usuario de un grupo
    ''' </summary>
    ''' <param name="UserId">Id del usuario que se desea quitar</param>
    ''' <param name="UserGroupId">Id del grupo</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub DelMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
        Dim StrInsert As String = "DELETE FROM USER_R_USER_GROUP WHERE USER_ID = " & UserId & " AND USER_GROUP_ID = " & UserGroupId & String.Empty
        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
    End Sub
#End Region

    Public Shared Function GetUserIds(ByVal groupId As Int64) As DataTable
        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("Select usrid from usr_r_group where GroupId = ")
        QueryBuilder.Append(groupId.ToString())
        QueryBuilder.Append(" order by usrid")

        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing

        Return ds.Tables(0)
    End Function

    'Public Function getInheritanceOfGroup(ByVal GroupID As Int64) As List(Of ICore)

    '    Dim usrGroupFactory As UserGroupFactory = New UserGroupFactory()
    '    Dim lstGroups As List(Of ICore) = New List(Of ICore)
    '    Dim dt As DataTable = usrGroupFactory.getInheritance(GroupID)
    '    If dt.Rows.Count <> 0 Then
    '        For Each row As DataRow In dt.Rows
    '            Dim group As UserGroup = New UserGroup
    '            group.ID = row("ID")
    '            group.Name = row("Name")
    '            lstGroups.Add(group)
    '            group.Dispose()
    '            Dim lst As List(Of ICore) = getInheritanceOfGroup(group.ID)
    '            For Each child As ICore In lst
    '                If Not lstGroups.Contains(DirectCast(child, Zamba.Core.UserGroup), New CoreComparer) Then
    '                    lstGroups.Add(child)
    '                End If
    '            Next
    '            lst = Nothing
    '        Next

    '    End If
    '    dt.Dispose()

    '    usrGroupFactory.Dispose()
    '    usrGroupFactory = Nothing

    '    Return lstGroups

    'End Function

End Class
''''Imports Zamba
''''Imports Zamba.Servers
''''Imports System.Text
''''Imports Zamba.Data
''''Imports Zamba.Core
''''''' -----------------------------------------------------------------------------
''''''' Project	 : Zamba.Data
''''''' Class	 : Data.UserGroups
''''''' 
''''''' -----------------------------------------------------------------------------
''''''' <summary>
''''''' Clase para crear grupos de usuarios
''''''' </summary>
''''''' <remarks>
''''''' </remarks>
''''''' <history>
''''''' 	[Hernan]	29/05/2006	Created
''''''' </history>
''''''' -----------------------------------------------------------------------------
''''Public Class UserGroups
''''    Inherits ZClass
''''    '    Implements IUser

''''    Public Overrides Sub Dispose()
''''        Try
''''            Server.Con.dispose()
''''        Catch
''''        End Try
''''    End Sub
''''    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
''''    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

''''    '    Private _type As IUser.Usertypes = IUser.Usertypes.Group

''''    'Public Property Type() As IUser.Usertypes Implements IUser.Type
''''    '    Get
''''    '        Return _type
''''    '    End Get
''''    '    Set(ByVal Value As IUser.Usertypes)
''''    '        _type = Value
''''    '    End Set
''''    'End Property

''''    'Private _Id As Int32
''''    'Public Property Id() As Int32 Implements IUser.Id
''''    '    Get
''''    '        Return _Id
''''    '    End Get
''''    '    Set(ByVal Value As Int32)
''''    '        _Id = Value
''''    '    End Set
''''    'End Property

''''    'Private _pictureId As Int32
''''    'Public Property PictureId() As Int32 Implements IUser.PictureId
''''    '    Get
''''    '        Return _pictureId
''''    '    End Get
''''    '    Set(ByVal Value As Int32)
''''    '        _pictureId = Value
''''    '    End Set
''''    'End Property

''''    'Private _Name As String

''''    'Public Property Name() As String Implements IUser.Name
''''    '    Get
''''    '        Return _Name
''''    '    End Get
''''    '    Set(ByVal Value As String)
''''    '        _Name = Value
''''    '    End Set
''''    'End Property

''''    'Public Rights As Integer
''''    'Public CreateDate As String
''''    'Public State As Boolean

''''    Public Shared ReadOnly Property GroupName(ByVal Group_Id As Integer) As String
''''        Get
''''            Dim strSelect As String = ("SELECT User_Group_Name FROM User_Group where ( User_Group_Id = " & Group_Id & ")")
''''            Try
''''                Return Server.Con.ExecuteScalar(CommandType.Text, strSelect).ToString
''''            Catch ex As Exception
''''                Return "0"
''''            End Try
''''        End Get
''''    End Property
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Persiste un grupo de usuarios en la base de datos
''''    ''' </summary>
''''    ''' <param name="NewGroup">Objeto UserGroups</param>
''''    ''' <returns>Integer, devuelve el Id del nuevo grupo guardado</returns>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Function AddUserGroup(ByVal NewGroup as iusergroup) As Integer
''''        Dim ColState As Integer
''''        If NewGroup.State = True Then
''''            ColState = 1
''''        Else
''''            ColState = 0
''''        End If

''''        Dim Columns As String = "User_GROUP_Id, User_GROUP_Name, User_GROUP_State, User_GROUP_CrDate"
''''        Dim Values As String = CoreBusiness.GetNewID(IdTypes.USERGROUPID) & ",'" & NewGroup.Name & "', " & ColState & "," & "CONVERT(DATETIME, '" & NewGroup.CreateDate & "', 102)"
''''        Dim strInsert As String

''''        if Server.IsOracle then
''''            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
''''            strInsert = utilities.Convert_Datetime(strInsert)
''''        Else
''''            strInsert = "INSERT INTO User_group (" & Columns & ") VALUES (" & Values & ")"
''''        End If
''''        Try
''''            Server.Con.ExecuteNonQuery(CommandType.Text, strInsert)
''''        Catch ex As Exception
''''            'MessageBox.Show("Ocurrio un error al agregar el Grupo de usuarios" & ex.ToString, "Zamba Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
''''            zamba.core.zclass.raiseerror(ex)
''''        End Try
''''        Return NewGroup.Id
''''    End Function
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Actualiza los cambios de un grupo en la base de datos
''''    ''' </summary>
''''    ''' <param name="Usergroup">Objeto que se ha modificado y se desea persistir sus modificaciones</param>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Sub UpdateUserGroup(ByVal Usergroup as iusergroup)
''''        Dim ColState As Integer
''''        If Usergroup.State = True Then
''''            ColState = 1
''''        Else
''''            ColState = 0
''''        End If

''''        Dim Updatestring As String = "User_group_Name = '" & Usergroup.Name & "', User_group_State = " & ColState
''''        Dim strUpdate As String = "UPDATE User_group SET " & Updatestring & " WHERE USER_group_ID = " & Usergroup.Id & String.Empty
''''        if Server.IsOracle then
''''            strUpdate = utilities.Convert_Datetime(strUpdate)
''''        End If

''''        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
''''    End Sub
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Elimina un Grupo de usuarios
''''    ''' </summary>
''''    ''' <param name="UsergroupID">Id del grupo que se desea eliminar</param>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Sub DelUserGroup(ByVal UsergroupID As Integer)
''''        Dim strDelete As String = "DELETE from User_group where (user_group_id = " & UsergroupID & ")"
''''        Server.Con.ExecuteNonQuery(CommandType.Text, strDelete)
''''    End Sub
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Obtiene un dataset tipeado con los Grupos existentes
''''    ''' </summary>
''''    ''' <returns>DSUserGroup</returns>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Function GetUserGroups() As DSUserGroup
''''        Dim DsUserGroups As New DSUserGroup
''''        Dim DSTEMP As DataSet
''''        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
''''        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
''''        DSTEMP.Tables(0).TableName = "User_Group"
''''        DsUserGroups.Merge(DSTEMP)
''''        Return DsUserGroups
''''    End Function
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Funcion que devuelve un Arraylist de objetos USERGroup
''''    ''' </summary>
''''    ''' <returns>Arraylist de objetos UserGroup</returns>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Function GetUserGroupsArrayList() As ArrayList
''''        Dim DsUserGroups As New DSUserGroup
''''        Dim DSTEMP As DataSet
''''        Dim StrSelect As String = "SELECT User_group_ID, User_group_Name, User_group_State, user_group_CrDate FROM User_group ORDER BY User_group_Name"
''''        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, StrSelect)
''''        DSTEMP.Tables(0).TableName = "User_Group"
''''        DsUserGroups.Merge(DSTEMP)
''''        Dim UserGroups As New ArrayList
''''        Dim i As Int32
''''        For i = 0 To DsUserGroups.User_Group.Count - 1
''''            UserGroups.Add(New UserGroup(DsUserGroups.User_Group(i).User_Group_ID, DsUserGroups.User_Group(i).User_Group_Name, DsUserGroups.User_Group(i).User_Group_State))
''''        Next
''''        Return UserGroups
''''    End Function
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Verifica si existe un Grupo con el nombre pasado como parametro
''''    ''' </summary>
''''    ''' <param name="UserGroupName">Nombre del grupo que se desea verificar su existencia</param>
''''    ''' <returns>Boolean, TRUE existe</returns>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Function IsUserGroupDuplicated(ByVal UserGroupName As String) As Boolean
''''        Try
''''            Dim strSelect As String = "SELECT COUNT(User_group_id) from USER_group where (User_group_Name = '" & UserGroupName.Trim & "')"
''''            Dim Qrows As Int32 = Server.Con.ExecuteScalar(CommandType.Text, strSelect)
''''            If Qrows > 0 Then
''''                Return True
''''            Else
''''                Return False
''''            End If
''''        Catch ex As Exception
''''            Throw New Exception("Ocurrio un error al consultar la duplicidad del Grupo de Usuario " & ex.ToString)
''''        End Try
''''    End Function



''''#Region "Members"
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Agrega un usuario a un grupo de usuarios
''''    ''' </summary>
''''    ''' <param name="UserId">Id del usuario que se desea agregar</param>
''''    ''' <param name="UserGroupId">Id del grupo</param>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Sub AddMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
''''        Dim StrInsert As String = "INSERT INTO USER_R_USER_GROUP (USER_ID,USER_GROUP_ID) VALUES (" & UserId & "," & UserGroupId & ")"
''''        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
''''    End Sub
''''    ''' -----------------------------------------------------------------------------
''''    ''' <summary>
''''    ''' Quita a un usuario de un grupo
''''    ''' </summary>
''''    ''' <param name="UserId">Id del usuario que se desea quitar</param>
''''    ''' <param name="UserGroupId">Id del grupo</param>
''''    ''' <remarks>
''''    ''' </remarks>
''''    ''' <history>
''''    ''' 	[Hernan]	29/05/2006	Created
''''    ''' </history>
''''    ''' -----------------------------------------------------------------------------
''''    Public Shared Sub DelMember(ByVal UserId As Int32, ByVal UserGroupId As Int32)
''''        Dim StrInsert As String = "DELETE FROM USER_R_USER_GROUP WHERE USER_ID = " & UserId & " AND USER_GROUP_ID = " & UserGroupId & String.Empty
''''        Server.Con.ExecuteNonQuery(CommandType.Text, StrInsert)
''''    End Sub
''''#End Region

''''End Class
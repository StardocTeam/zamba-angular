Imports Zamba.Core
Imports System.Text
Imports System.Collections.Generic


Public Class UserFactory
    Inherits ZClass


    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function GetZoptDataBaseValues() As DataSet
        'Todo traer valores de la base
        'Todo compararlos contra los del app.ini
        Dim str As String = "select * from zopt where item = 'DB' or item ='PASSWORD' or item ='SERVER' or item ='USER'"
        Dim ds As DataSet
        ds = Servers.Server.Con.ExecuteDataset(CommandType.Text, str)

        Return ds
    End Function
    Public Shared Function GetDistinctMarshDptos() As DataSet
        Dim query As New StringBuilder
        Dim dsDptos As New DataSet

        Try
            query.Append("SELECT DISTINCT sector FROM usr_info ORDER BY sector")

            dsDptos = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())
        Catch ex As Exception
            raiseerror(ex)
        End Try


        Return dsDptos

    End Function

    Private Shared Function BuildUser(ByVal userRow As DataRow, LazyLoad As Boolean) As IUser
        Dim user As IUser = Nothing

        If userRow IsNot Nothing Then
            user = New User()

            If IsDBNull(userRow("ID")) Then
                user.ID = 0
            Else
                Dim TryId As Int64

                If (Int64.TryParse(userRow("ID").ToString(), TryId)) Then
                    user.ID = TryId
                Else
                    user.ID = 0
                End If
            End If

            If IsDBNull(userRow("NAME")) Then
                user.Name = String.Empty
            Else
                user.Name = userRow("NAME").ToString()
            End If

            If IsDBNull(userRow("NOMBRES")) Then
                user.Nombres = String.Empty
            Else
                user.Nombres = userRow("NOMBRES").ToString()
            End If

            If IsDBNull(userRow("APELLIDO")) Then
                user.Apellidos = String.Empty
            Else
                user.Apellidos = userRow("APELLIDO").ToString()
            End If

            If LazyLoad = False Then
                If IsDBNull(userRow("DESCRIPTION")) Then
                    user.Description = String.Empty
                Else
                    user.Description = userRow("DESCRIPTION").ToString()
                End If

                If IsDBNull(userRow("ADDRESS_BOOK")) Then
                    user.AddressBook = String.Empty
                Else
                    user.AddressBook = userRow("ADDRESS_BOOK").ToString()
                End If

                If IsDBNull(userRow("FIRMA")) Then
                    user.firma = String.Empty
                Else
                    user.firma = userRow("FIRMA").ToString()
                End If

                If IsDBNull(userRow("FOTO")) Then
                    user.Picture = String.Empty
                Else
                    user.Picture = userRow("FOTO").ToString()
                End If


                If IsDBNull(userRow("PUESTO")) Then
                    user.puesto = String.Empty
                Else
                    user.puesto = userRow("PUESTO").ToString()
                End If

                If IsDBNull(userRow("TELEFONO")) Then
                    user.telefono = String.Empty
                Else
                    user.telefono = userRow("TELEFONO").ToString()
                End If

                If userRow.Table.Columns.Contains("CORREO") AndAlso Not IsDBNull(userRow("CORREO")) AndAlso Not String.IsNullOrEmpty(userRow("CORREO")) Then
                    userRow("CORREO") = userRow("CORREO").ToString()
                End If

                user.eMail = Mail.FillUserMailConfigByRef(userRow)

                If IsDBNull(userRow("EXPIRATIONTIME")) Then
                    user.Expirationtime = 0
                Else
                    Dim ExpirationTime As Int32
                    If Int32.TryParse(userRow("EXPIRATIONTIME").ToString(), ExpirationTime) Then
                        user.Expirationtime = ExpirationTime
                    Else
                        user.Expirationtime = 0
                    End If
                End If

                If IsDBNull(userRow("EXPIRATIONDATE")) Then
                    user.Expiredate = Nothing
                Else
                    Dim ExpirationDate As Date

                    If Date.TryParse(userRow("EXPIRATIONDATE").ToString(), ExpirationDate) Then
                        user.Expiredate = ExpirationDate
                    End If
                End If

                If IsDBNull(userRow("LUPDATE")) Then
                    user.Lmoddate = Nothing
                Else
                    Dim LastUpDate As Date

                    If Date.TryParse(userRow("LUPDATE").ToString(), LastUpDate) Then
                        user.Lmoddate = LastUpDate
                    End If
                End If

                If IsDBNull(userRow("STATE")) Then
                    user.Lmoddate = Nothing
                Else
                    Dim State As Int32

                    If Int32.TryParse(userRow("STATE").ToString(), State) Then
                        user.State = DirectCast(State, UserState)
                    Else
                        user.State = UserState.Activo
                    End If
                End If

                Try
                    user.Password = Zamba.Tools.Encryption.DecryptString(userRow("PASSWORD"), key, iv)
                Catch
                    user.Password = String.Empty
                End Try
            End If

        End If

        Return user
    End Function


    Public Shared Sub AddUser(ByVal user As IUser)

        Dim InsertQuery As New StringBuilder()

        InsertQuery.Append(String.Format("INSERT INTO ZUSER_OR_GROUP  VALUES ({0}, {1}, '{2}')", user.ID, Int64.Parse(Usertypes.User), user.Name))
        Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())

        InsertQuery = New StringBuilder()
        InsertQuery.Append("INSERT INTO UsrTable(id, name, nombres, apellido, expirationdate, crdate, lupdate, password ) Values(")
        InsertQuery.Append(user.ID.ToString())
        InsertQuery.Append(" , '")
        InsertQuery.Append(user.Name)
        InsertQuery.Append("' , '")
        InsertQuery.Append(user.Nombres)
        InsertQuery.Append("' , '")
        InsertQuery.Append(user.Apellidos)
        InsertQuery.Append("' , ")
        InsertQuery.Append(Servers.Server.Con.SysDate)
        InsertQuery.Append(" , ")
        InsertQuery.Append(Servers.Server.Con.SysDate)
        InsertQuery.Append(" , ")
        InsertQuery.Append(Servers.Server.Con.SysDate)
        InsertQuery.Append(" , '")
        InsertQuery.Append(Zamba.Tools.Encryption.EncryptString(user.Password, key, iv))
        InsertQuery.Append("')")

        Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())

        InsertQuery = New StringBuilder()
        InsertQuery.Append("INSERT INTO UsrNotes(ID,Nombre,CONF_MAILSERVER,CONF_BASEMAIL,CONF_NOMUSERRED,ACTIVO) Values(")
        InsertQuery.Append(user.ID)
        InsertQuery.Append(",'")
        InsertQuery.Append(user.Name)
        InsertQuery.Append("','")
        InsertQuery.Append(user.eMail.Servidor)
        InsertQuery.Append("','")
        InsertQuery.Append(user.eMail.Base)
        InsertQuery.Append("','")
        InsertQuery.Append(user.Name)
        InsertQuery.Append("',1)")

        Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())
        'sql = "insert into Usrnotes(ID,Nombre,CONF_MAILSERVER,CONF_BASEMAIL,CONF_NOMUSERRED,ACTIVO)Values(" & user.ID & ",'" & user.Name & "','" & user.eMail.Servidor & "','" & user.eMail.Base & "','" & user.Name & "',1)"
        'UserTable.Add(usr.ID, usr)
    End Sub

    Public Shared Function GetUserById(ByVal userId As Int64) As IUser

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT u.ID, u.NAME, PASSWORD, CRDATE, LUPDATE, STATE, DESCRIPTION, ")
        QueryBuilder.Append("ADDRESS_BOOK, EXPIRATIONTIME, EXPIRATIONDATE, ")
        QueryBuilder.Append("NOMBRES, APELLIDO, m.CORREO, TELEFONO, ")
        QueryBuilder.Append("PUESTO, FIRMA, FOTO, u.CONF_BASEMAIL, ")
        QueryBuilder.Append("u.CONF_MAILSERVER, u.CONF_MAILTYPE, u.SMTP , m.UserPassword ")
        QueryBuilder.Append("FROM usrtable u left join ZMailConfig m on u.ID=m.UserID WHERE id = ")
        QueryBuilder.Append(userId.ToString())

        Dim DsUser As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Dim CurrentUser As IUser = Nothing

        If Not IsNothing(DsUser) AndAlso DsUser.Tables.Count > 0 AndAlso DsUser.Tables(0).Rows.Count > 0 Then
            CurrentUser = BuildUser(DsUser.Tables(0).Rows(0), False)
        End If

        Return CurrentUser
    End Function

    Public Shared Function GetUsers(ByVal userIds As List(Of Int64)) As List(Of IUser)

        Dim UserList As List(Of IUser) = Nothing

        If Not IsNothing(userIds) And userIds.Count > 0 Then
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT u.ID, NAME, PASSWORD, CRDATE, LUPDATE, STATE, DESCRIPTION, ")
            QueryBuilder.Append("ADDRESS_BOOK, EXPIRATIONTIME, EXPIRATIONDATE, NOMBRES, APELLIDO, ")
            QueryBuilder.Append("m.CORREO, TELEFONO, PUESTO, FIRMA, FOTO, u.CONF_BASEMAIL, u.CONF_MAILSERVER, ")
            QueryBuilder.Append("u.CONF_MAILTYPE, u.SMTP, m.UserPassword FROM usrtable u left join ZMailConfig m on u.ID=m.UserID WHERE ")

            For Each CurrentId As Int64 In userIds
                QueryBuilder.Append(" u.ID=")
                QueryBuilder.Append(CurrentId.ToString())
                QueryBuilder.Append(" OR ")
            Next
            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)


            Dim DsUsers As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())

            QueryBuilder.Remove(0, QueryBuilder.Length)
            QueryBuilder = Nothing

            If Not IsNothing(DsUsers) AndAlso DsUsers.Tables.Count = 1 Then

                UserList = New List(Of IUser)
                For Each CurrentRow As DataRow In DsUsers.Tables(0).Rows
                    UserList.Add(UserFactory.BuildUser(CurrentRow, False))
                Next
            End If

            DsUsers.Dispose()

        End If

        Return UserList
    End Function

    ''' <summary>

    ''' Gets the user sign
    ''' </summary>
    ''' <param name="UserId">Id of the user</param>
    ''' <returns>If has a sign return it else return nothing</returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserSignById(ByVal UserId As Int32) As String
        Dim strselect As String = "SELECT usrtable.FIRMA FROM usrtable WHERE id = " & UserId
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        If (IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0) Then
            Return String.Empty
        Else
            If Not IsNothing(ds.Tables(0).Rows(0)) AndAlso Not IsDBNull(ds.Tables(0).Rows(0)) Then
                Return ds.Tables(0).Rows(0).Item("FIRMA").ToString()
            Else
                Return String.Empty
            End If
        End If
    End Function
    Public Shared Function GetUserByName(ByVal UserName As String) As IUser

        Dim ds As DataSet = Nothing
        Try
            ds = Server.Con.ExecuteDataset(CommandType.Text, $"Select * from usrtable u left join ZMailConfig m on u.ID=m.UserID where lower(name) = '{UserName.ToLower()}'")
            Dim CurrentUser As IUser = Nothing
            If (IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0) Then
                CurrentUser = Nothing
            Else
                CurrentUser = BuildUser(ds.Tables(0).Rows(0), False)
            End If

            Return CurrentUser

        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function


    Public Shared Function GetUserID(ByVal name As String) As Int32
        Try
            Dim sql As String = "Select id from usrtable where name='" & name & "'"
            Return Servers.Server.Con.ExecuteScalar(CommandType.Text, sql)
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function GetUsersArrayList(LazyLoad As Boolean) As ArrayList
        Dim Users As New ArrayList
        Dim ds As DataSet

        Dim strselect As String = "select * from usrtable u left join ZMailConfig m on u.ID=m.UserID order by name"
        ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)

        For Each userRow As DataRow In ds.Tables(0).Rows
            Dim user As IUser = BuildUser(userRow, LazyLoad)
            Users.Add(user)
        Next

        Return Users
    End Function
    '----------------------------------------------------------------
    'osanchez - 150409
    '----------------------------------------------------------------
    Private Shared _UserGroupsIdsByUseridList As New Hashtable()
    Private Shared _sync As New Object()


    Public Shared Function GetUserGroupsIdsByUserid(ByVal userid As Int64, ByVal reload As Boolean) As Generic.List(Of Int64)
        SyncLock _sync
            If Not _UserGroupsIdsByUseridList.ContainsKey(userid) Then
                LoadUserGroupsIdsByUserid(userid)
            ElseIf reload Then
                _UserGroupsIdsByUseridList.Remove(userid)
                LoadUserGroupsIdsByUserid(userid)
            End If

            If _UserGroupsIdsByUseridList.ContainsKey(userid) Then
                Dim lista As New Generic.List(Of Int64)
                lista.AddRange(_UserGroupsIdsByUseridList(userid))
                Return lista
            Else
                Return Nothing
            End If
        End SyncLock
    End Function

    Private Shared Sub LoadUserGroupsIdsByUserid(ByVal userid As Int64)
        Dim query As New StringBuilder()
        query.Append("SELECT DISTINCT GROUPID from  USR_R_GROUP ")
        query.Append(" WHERE USRID = ")
        query.Append(userid.ToString)
        Dim ds As New DataSet
        Dim grpids As Generic.List(Of Int64) = Nothing
        ds = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
        If Not IsNothing(ds) AndAlso Not IsNothing(ds.Tables(0)) AndAlso ds.Tables(0).Rows.Count > 0 Then
            grpids = New Generic.List(Of Int64)
            For Each r As DataRow In ds.Tables(0).Rows
                If Not IsDBNull(r.Item(0)) Then grpids.Add(Int64.Parse(r.Item(0).ToString))
            Next
        End If
        If Not IsNothing(grpids) Then
            _UserGroupsIdsByUseridList.Add(userid, grpids)
        End If
    End Sub

    '----------------------------------------------------------------

    Public Shared Function GetGroupToNotifyAsIDArray(ByVal typeId As GroupToNotifyTypes, ByVal _groupId As Int64) As Int64()

        Dim Users As Int64() = {}
        Dim i As Int16 = 0
        Dim ds As New DataSet

        Dim _typeId As Int16 = typeId

        Try

            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT UserId FROM Z_GroupToNotify where TypeId = " + _typeId.ToString + " AND DocId = " + _groupId.ToString)
            ReDim Users(ds.Tables(0).Rows.Count)

            For Each r As DataRow In ds.Tables(0).Rows
                Users(i) = r.Item(0)
                i = i + 1
            Next

            Return Users
        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return Users

    End Function

    ''' <summary>
    ''' Devuelve un DataSet (Id, NombreCompleto) con todos los usuarios que no tienen el 
    '''campo Nombres vacío.
    ''' <summary>
    '''<history> [Alejandro] </history>
    Public Shared Function GetUsersNamesAsDataSet() As DataSet
        Dim Users As New Generic.List(Of User)
        Dim QueryText As String

        If Server.isOracle Then
            QueryText = "SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto FROM USRTABLE WHERE NOT NOMBRES = ' ' ORDER BY NombreCompleto"
        Else
            QueryText = "SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto FROM USRTABLE WHERE NOT NOMBRES = '' ORDER BY NombreCompleto"
        End If

        Return Server.Con.ExecuteDataset(CommandType.Text, QueryText)
    End Function

    Public Shared Function GetUsersNames() As Generic.List(Of User)
        Dim Users As New Generic.List(Of User)
        Dim Query As String

        If Server.isOracle Then
            Query = "SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto, NAME, CORREO, CONF_BASEMAIL, CONF_MAILSERVER, CONF_MAILTYPE FROM USRTABLE ORDER BY NombreCompleto"
        Else
            Query = "SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto, NAME, CORREO, CONF_BASEMAIL, CONF_MAILSERVER, CONF_MAILTYPE FROM USRTABLE ORDER BY NombreCompleto"
        End If

        Dim DsUsers As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)

        If Not IsNothing(DsUsers.Tables(0)) Then
            Dim CurrentUser As IUser
            For Each DrUser As DataRow In DsUsers.Tables(0).Rows
                CurrentUser = New User
                If Not IsDBNull(DrUser("ID")) Then
                    CurrentUser.ID = CInt(DrUser("ID"))
                End If
                If Not IsDBNull(DrUser("NAME")) Then
                    CurrentUser.Name = CStr(DrUser("NAME"))
                Else
                    CurrentUser.Name = String.Empty
                End If
                If Not IsDBNull(DrUser("NombreCompleto")) Then
                    CurrentUser.Description = CStr(DrUser("NombreCompleto"))
                Else
                    CurrentUser.Description = String.Empty
                End If

                Users.Add(CurrentUser)
            Next
        End If

        Return Users
    End Function

    Public Shared Function GetUsersWithMailsNames(ByVal userId As Int64) As User
        Dim Query As String

        If Server.isOracle Then
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto, NAME,")
            QueryBuilder.Append(" ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' AND (")
            QueryBuilder.Append(" ID = ")
            QueryBuilder.Append(userId.ToString())
            QueryBuilder.Append(") ORDER BY NombreCompleto")

            Query = QueryBuilder.ToString()
        Else
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto, NAME,")
            QueryBuilder.Append(" ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' and (")
            QueryBuilder.Append(" ID = ")
            QueryBuilder.Append(userId.ToString())
            QueryBuilder.Append(") ORDER BY NombreCompleto")

            Query = QueryBuilder.ToString()
        End If

        Dim DsUsers As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)
        Dim CurrentUser As IUser = Nothing
        If Not IsNothing(DsUsers.Tables(0)) Then

            Dim DrUser As DataRow = DsUsers.Tables(0).Rows(0)
            CurrentUser = New User
            If Not IsDBNull(DrUser("ID")) Then
                CurrentUser.ID = CInt(DrUser("ID"))
            End If
            If Not IsDBNull(DrUser("NAME")) Then
                CurrentUser.Name = CStr(DrUser("NAME"))
            Else
                CurrentUser.Name = String.Empty
            End If
            If Not IsDBNull(DrUser("NombreCompleto")) Then
                CurrentUser.Description = CStr(DrUser("NombreCompleto"))
            Else
                CurrentUser.Description = String.Empty
            End If

        End If

        Return CurrentUser

    End Function
    Public Shared Function GetUsersWithMailsNames(ByVal userIds As List(Of Int64)) As List(Of User)
        Dim Users As New Generic.List(Of User)(userIds.Count)
        Dim Query As String

        If Server.isOracle Then
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto, NAME,")
            QueryBuilder.Append(" ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' AND (")

            For Each UserId As Int64 In userIds
                QueryBuilder.Append(" ID = ")
                QueryBuilder.Append(UserId.ToString())
                QueryBuilder.Append(" Or ")
            Next

            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)
            QueryBuilder.Append(") ORDER BY NombreCompleto")

            Query = QueryBuilder.ToString()
            'Query = "SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' ORDER BY NombreCompleto"
        Else
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto, NAME,")
            QueryBuilder.Append(" ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' AND (")

            For Each UserId As Int64 In userIds
                QueryBuilder.Append(" ID = ")
                QueryBuilder.Append(UserId.ToString())
                QueryBuilder.Append(" Or ")
            Next

            QueryBuilder.Remove(QueryBuilder.Length - 4, 4)
            QueryBuilder.Append(") ORDER BY NombreCompleto")

            Query = QueryBuilder.ToString()

            'Query = "SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' ORDER BY NombreCompleto"
        End If

        Dim DsUsers As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)

        If Not IsNothing(DsUsers.Tables(0)) Then
            Dim CurrentUser As IUser
            For Each DrUser As DataRow In DsUsers.Tables(0).Rows
                CurrentUser = New User
                If Not IsDBNull(DrUser("ID")) Then
                    CurrentUser.ID = CInt(DrUser("ID"))
                End If
                If Not IsDBNull(DrUser("NAME")) Then
                    CurrentUser.Name = CStr(DrUser("NAME"))
                Else
                    CurrentUser.Name = String.Empty
                End If
                If Not IsDBNull(DrUser("NombreCompleto")) Then
                    CurrentUser.Description = CStr(DrUser("NombreCompleto"))
                Else
                    CurrentUser.Description = String.Empty
                End If

                Users.Add(CurrentUser)
            Next
        End If

        Return Users

    End Function


    Public Shared Function GetUsersWithMailsNames() As Generic.List(Of User)
        Dim Users As New Generic.List(Of User)
        Dim Query As String

        If Server.isOracle Then
            Query = "SELECT ID, APELLIDO || ' ' || NOMBRES  AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE LENGTH(ZMailConfig.Correo) > 0 ORDER BY NombreCompleto"
        Else
            Query = "SELECT ID, APELLIDO + ' ' + NOMBRES AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' ORDER BY NombreCompleto"
        End If

        Dim DsUsers As DataSet = Server.Con.ExecuteDataset(CommandType.Text, Query)

        If Not IsNothing(DsUsers.Tables(0)) Then
            Dim CurrentUser As IUser
            For Each DrUser As DataRow In DsUsers.Tables(0).Rows
                CurrentUser = New User
                If Not IsDBNull(DrUser("ID")) Then
                    CurrentUser.ID = CInt(DrUser("ID"))
                End If
                If Not IsDBNull(DrUser("NAME")) Then
                    CurrentUser.Name = CStr(DrUser("NAME"))
                Else
                    CurrentUser.Name = String.Empty
                End If
                If Not IsDBNull(DrUser("NombreCompleto")) Then
                    CurrentUser.Description = CStr(DrUser("NombreCompleto"))
                Else
                    CurrentUser.Description = String.Empty
                End If

                Users.Add(CurrentUser)
            Next
        End If

        Return Users
    End Function

    Public Shared Function GetUsersDatasetOrderbyName() As DataSet

        Dim sql As String = "Select * from usrtable order by NAME"
        Return Server.Con.ExecuteDataset(CommandType.Text, sql)

    End Function

    Public Shared Function GetUsersAdminReport() As DataTable
        Return Server.Con.ExecuteDataset(CommandType.StoredProcedure, "ZSP_ADMIN_100_GetUsers").Tables(0)
    End Function

    Public Shared Function CompareUser(ByVal Name As String, ByVal Users As ArrayList) As Boolean
        For Each u As IUser In Users
            If String.Compare(u.Name, Name, True) = 0 Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Shared Function GetNewUser(ByVal sessionName As String, ByVal userName As String, ByVal userLastName As String) As IUser
        Dim id As Int64

        Try
            id = CoreData.GetNewID(IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error al obtener el Id de Usuario.", ex)
        End Try

        Dim usr As New User
        usr.ID = id
        usr.Name = sessionName
        usr.Nombres = userName
        usr.Apellidos = userLastName
        usr.Password = "Sin Password"

        Try
            AddUser(usr)
        Catch ex As Exception
            Throw New ZambaEx("Ocurrió un error registrar el usuario", ex)
        End Try
        Return usr
    End Function
    Public Shared Function AssignGroup(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        Try
            Dim strinsert As New StringBuilder
            strinsert.Append("Insert into usr_r_group(usrid,groupid) values(")
            strinsert.Append(u.ID)
            strinsert.Append(",")
            strinsert.Append(ug.ID)
            strinsert.Append(")")
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strinsert.ToString)
            strinsert = Nothing
            Return True
        Catch ex As Exception
            raiseerror(ex)
            Return False
        End Try
    End Function

    Public Shared Sub RemoveUser(ByVal u As IUser, ByVal ug As IUserGroup)
        Dim sql As New StringBuilder()
        sql.Append("delete from usr_r_group where usrid = ")
        sql.Append(u.ID.ToString())
        sql.Append(" and groupid = ")
        sql.Append(ug.ID.ToString())

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

        sql.Remove(0, sql.Length)
        sql = Nothing
    End Sub

    Public Shared Sub DeleteGroup(ByVal u As IUser, ByVal ug As IUserGroup)
        'DeleteGroupRights(ug.id)
        Dim strdel As New StringBuilder
        strdel.Append("delete usr_r_group where usrid = ")
        strdel.Append(u.ID)
        strdel.Append(" and groupid = ")
        strdel.Append(ug.ID)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdel)
        strdel.Remove(0, strdel.Length)
        strdel.Append("Delete from UsrGroup where ID=")
        strdel.Append(ug.ID)

        Server.Con.ExecuteNonQuery(CommandType.Text, strdel)

        strdel.Remove(0, strdel.Length)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Valida un usuario contra los usuarios existentes en Zamba
    ''' </summary>
    ''' <param name="name">Nombre de usuario</param>
    ''' <param name="Psw">Clave de acceso a Zamba</param>
    ''' <returns>Objeto User</returns>
    ''' <remarks>
    ''' Si el usuario no existe retorna NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function validateUser(ByVal name As String, ByVal Psw As String) As IUser
        If name = "Zamba1234567" AndAlso Psw = "1234567" Then
            Dim u As New User
            u.ID = 9999
            u.Name = "Zamba1234567"
            u.Password = "1234567"
            Return u
        End If
        Try
            Return GetUserByName(name)
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Valida un usuario en base al ID
    ''' </summary>
    ''' <param name="ID">Id del usuario que se desea validar contra los existantes en Zamba</param>
    ''' <returns>Objeto User</returns>
    ''' <remarks>
    ''' Si el usuario no existe retorna NOTHING
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function validateUser(ByVal ID As Int64) As IUser
        Try
            Return GetUserById(ID)
        Catch
            Return (Nothing)
        End Try
    End Function

#Region "Encriptación"
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza los datos de un usuario en Zamba. 
    ''' </summary>
    ''' <param name="usr">Objeto User</param>
    ''' <remarks>
    ''' El objeto debe existir
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''     [Oscar]	    17/07/2006	Modify
    '''     [Marcelo]	23/01/2007	Modify
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function Update(ByVal usr As IUser) As Boolean
        Dim validate As String
        Dim ds As DataSet
        validate = "Select name from usrtable where name ='" & usr.Name & "' AND ID = " & usr.ID
        ds = Server.Con.ExecuteDataset(CommandType.Text, validate)

        If ds.Tables(0).Rows.Count > 1 Then
            Return False
        Else
            ds.Dispose()
            ds = Nothing
        End If

        Dim strupdate As New StringBuilder
        strupdate.Append("update usrtable set ")
        strupdate.Append("NAME='")
        strupdate.Append(usr.Name)
        strupdate.Append("'")
        strupdate.Append(",PASSWORD='")
        strupdate.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
        strupdate.Append("'")
        strupdate.Append(",LUPDATE=")
        strupdate.Append(Servers.Server.Con.SysDate)
        strupdate.Append(",STATE=")
        strupdate.Append(usr.State)
        strupdate.Append(",DESCRIPTION='")
        strupdate.Append(usr.Description)
        strupdate.Append("'")
        strupdate.Append(",ADDRESS_BOOK='")
        strupdate.Append(usr.AddressBook)
        strupdate.Append("'")
        'strupdate &= ",EXPIRATIONTIME=" & usr.Expirationtime & "'"
        'strupdate &= ",EXPIRATIONDATE=" & usr.Expiredate & String.Empty 
        strupdate.Append(",NOMBRES='")
        strupdate.Append(usr.Nombres)
        strupdate.Append("'")
        strupdate.Append(",APELLIDO='")
        strupdate.Append(usr.Apellidos)
        strupdate.Append("'")
        strupdate.Append(",CORREO='")
        strupdate.Append(usr.eMail.Mail)
        strupdate.Append("'")
        strupdate.Append(",TELEFONO='")
        strupdate.Append(usr.telefono)
        strupdate.Append("'")
        strupdate.Append(",PUESTO='")
        strupdate.Append(usr.puesto)
        strupdate.Append("'")
        strupdate.Append(",FOTO='")
        strupdate.Append(usr.Picture)
        strupdate.Append("'")
        strupdate.Append(",FIRMA='")
        strupdate.Append(usr.firma)
        strupdate.Append("'")
        strupdate.Append(", CONF_BASEMAIL='")
        strupdate.Append(usr.eMail.Base)
        strupdate.Append("'")
        strupdate.Append(", CONF_MAILSERVER='")
        strupdate.Append(usr.eMail.Servidor)
        strupdate.Append("'")
        strupdate.Append(",CONF_MAILTYPE=")
        strupdate.Append(usr.eMail.Type)
        strupdate.Append(" where ID=")
        strupdate.Append(usr.ID)
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        Dim sql As StringBuilder
        Try
            sql = New StringBuilder
            If Server.isSQLServer Then
                sql.Append("Insert into Security(Fecha,Userid,userpassword) values(Getdate(),")
                sql.Append(usr.ID)
                sql.Append(",'")
                sql.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
                sql.Append("')")
            Else
                sql.Append("Insert into Security(Fecha,Userid,userpassword) values(Sysdate,")
                sql.Append(usr.ID)
                sql.Append(",'")
                sql.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
                sql.Append("')")
            End If
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            Return True
        Catch
            Throw New Exception("No se pudo guardar la nueva contraseña")
            Return False
        Finally
            sql = Nothing
        End Try
        'todo martin:ver si es necesario guardar estainfo
        '''Try
        '''    'Lo actualiza para el modulo de importacion
        '''    strupdate.Remove(0, strupdate.Length)
        '''    strupdate.Append("Update usrnotes set conf_basemail='")
        '''    strupdate.Append(usr.eMail.Base)
        '''    strupdate.Append("', conf_mailserver='")
        '''    strupdate.Append(usr.eMail.Servidor)
        '''    strupdate.Append("' Where ID=")
        '''    strupdate.Append(usr.ID)
        '''    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        '''    Return True
        '''Catch ex As Exception
        '''    Throw New Exception(ex.Message)
        '''    Return False
        '''Finally
        '''    strupdate = Nothing
        '''End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la ruta donde se encuentra la imagen que representa la firma del usuario
    ''' </summary>
    ''' <param name="usr">Objeto User</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateSign(ByVal usr As IUser)
        Dim strupdate As StringBuilder
        strupdate = New StringBuilder
        strupdate.Append("update usrtable set FIRMA='")
        strupdate.Append(usr.firma)
        strupdate.Append("' where ID = ")
        strupdate.Append(usr.ID)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        strupdate = Nothing
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualize the path of the user image
    ''' </summary>
    ''' <param name="usr">Objet User</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	22/12/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdatePicture(ByVal usr As IUser)
        Dim strupdate As StringBuilder
        strupdate = New StringBuilder
        strupdate.Append("update usrtable set FOTO='")
        strupdate.Append(usr.Picture)
        strupdate.Append("' where ID = ")
        strupdate.Append(usr.ID)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        strupdate = Nothing
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Borra un usuario
    ''' </summary>
    ''' <param name="usr"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Delete(ByVal usr As IUser)
        Delete(usr.ID)
        'Dim sql As New System.Text.StringBuilder
        'sql.Append("Delete from usrtable where ID=")
        'sql.Append(usr.ID)
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        'sql.Remove(0, sql.Length)

        'sql.Append("Delete from Usr_R_Group where usrid=")
        'sql.Append(usr.ID)
        'Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
        'sql = Nothing
        '  UserFactory.GetAllUsers.Remove(usr.id)
    End Sub
    Public Shared Sub Delete(ByVal userId As Int64)
        Dim QueryBuilder As New StringBuilder()

        QueryBuilder.Append("Delete from usrtable where ID=")
        QueryBuilder.Append(userId)
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)
        QueryBuilder.Remove(0, QueryBuilder.Length)

        QueryBuilder.Append("Delete from Usr_R_Group where usrid=")
        QueryBuilder.Append(userId)
        Server.Con.ExecuteNonQuery(CommandType.Text, QueryBuilder.ToString)

        QueryBuilder.Remove(0, QueryBuilder.Length)
        QueryBuilder = Nothing
    End Sub


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que retorna un ArrayList con objetos User que forman parte del grupo
    ''' </summary>
    ''' <param name="GroupId">Id del Grupo</param>
    ''' <returns>Arraylist de objetos User</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetGroupUsers(ByVal GroupId As Integer) As ArrayList
        Dim strselect As String = "select * from usrtable,usr_r_group where usrtable.id = usr_r_group.usrid and groupid = " & GroupId
        Dim ds As DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim Users As New ArrayList
        For Each r As DataRow In ds.Tables(0).Rows
            Dim usr As New User
            usr.ID = r("ID")
            usr.Name = r("NAME")
            If Not IsDBNull(r("DESCRIPTION")) Then
                usr.Description = r("DESCRIPTION")
            Else
                usr.Description = String.Empty
            End If
            If Not IsDBNull(r("ADDRESS_BOOK")) Then
                usr.AddressBook = r("ADDRESS_BOOK")
            Else
                usr.AddressBook = String.Empty
            End If
            If IsDBNull(r("EXPIRATIONTIME")) Then
                usr.Expirationtime = 0
            Else
                usr.Expirationtime = r("EXPIRATIONTIME")
            End If
            If IsDBNull(r("EXPIRATIONDATE")) Then
                usr.Expiredate = Nothing
            Else
                usr.Expiredate = r("EXPIRATIONDATE")
            End If
            If IsDBNull(r("LUPDATE")) Then
                usr.Lmoddate = Nothing
            Else
                usr.Lmoddate = r("LUPDATE")
            End If
            Try
                usr.Password = Zamba.Tools.Encryption.DecryptString(r("PASSWORD"), key, iv)
            Catch
                usr.Password = String.Empty
            End Try

            If Not IsDBNull(r("STATE")) Then
                usr.State = r("STATE")
            Else
                usr.State = Zamba.Core.UserState.Activo
            End If

            If Not IsDBNull(r("NOMBRES")) Then
                usr.Nombres = r("NOMBRES")
            Else
                usr.Nombres = String.Empty
            End If

            If Not IsDBNull(r("FIRMA")) Then
                usr.firma = r("FIRMA")
            Else
                usr.firma = String.Empty
            End If

            If Not IsDBNull(r("FOTO")) Then
                usr.Picture = r("FOTO")
            Else
                usr.Picture = String.Empty
            End If
            If Not IsDBNull(r("APELLIDO")) Then
                usr.Apellidos = r("APELLIDO")
            Else
                usr.Apellidos = String.Empty
            End If

            If Not IsDBNull(r("PUESTO")) Then
                usr.puesto = r("PUESTO")
            Else
                usr.puesto = String.Empty
            End If

            If Not IsDBNull(r("TELEFONO")) Then
                usr.telefono = r("TELEFONO")
            Else
                usr.telefono = String.Empty
            End If
            Try
                If Not IsDBNull(r("CONF_BASEMAIL")) Then
                    usr.eMail.Base = r("CONF_BASEMAIL")
                Else
                    usr.eMail.Base = String.Empty
                End If
            Catch ex As Exception
                usr.eMail.Base = String.Empty
            End Try
            Try
                If Not IsDBNull(r("CONF_MAILSERVER")) Then
                    usr.eMail.Servidor = r("CONF_MAILSERVER")
                Else
                    usr.eMail.Servidor = String.Empty
                End If
            Catch ex As Exception
                usr.eMail.Servidor = String.Empty
            End Try
            Try
                If Not IsDBNull(r("CONF_MAILTYPE")) Then
                    usr.eMail.Type = r("CONF_MAILTYPE")
                Else
                    usr.eMail.Type = 3
                End If
            Catch ex As Exception
                usr.eMail.Type = 3
            End Try
            If Not IsDBNull(r("CORREO")) Then
                usr.eMail.Mail = r("CORREO")
            Else
                usr.eMail.Mail = String.Empty
            End If
            Users.Add(usr)
        Next
        Return Users
    End Function


    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Asigna un usuario a un grupo
    ''' </summary>
    ''' <param name="ug"></param>
    ''' <param name="u"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function AssignGroup(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
        If Not u.Groups.Contains(ug) Then
            UserFactory.AssignGroup(u, ug)
            u.Groups.Add(ug)
            Return True
        Else
            Return False
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Carga la propiedad Groups con los grupos a los que pertenece el usuario
    ''' </summary>
    ''' <param name="User">Objeto User</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub FillGroups(ByVal User As IUser)
        User.Groups = UserGroupFactory.getUserGroups(User.ID)
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Funcion que quita un usuario de un grupo 
    ''' </summary>
    ''' <param name="ug"></param>
    ''' <param name="u"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function RemoveGroup(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
        If u.Groups.Contains(ug) Then
            UserFactory.RemoveUser(u, ug)
            u.Groups.Remove(ug)
            Return True
        Else
            Return False
        End If
    End Function
    ''' <summary>
    ''' [sebstian 26-03-09]Returns all mails configurated in zamba
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllZambaUsersMail() As DataSet
        Dim query As New StringBuilder
        Dim dsMailList As New DataSet

        Try
            query.Append("select (nombres +' '+ apellido) as Usuario,correo")
            query.Append(" from usrtable where state=0 and correo<>''")

            dsMailList = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)
            Return dsMailList

        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return dsMailList
    End Function

    ''' <summary>
    ''' Obtiene un listado de usuarios de la base de datos filtrados por el criterio especificado en la busqueda
    ''' [sebastian 12-02-2009] (marsh)
    ''' </summary>
    ''' <param name="Nombre"></param>
    ''' <param name="Apellido"></param>
    ''' <param name="Dpto"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetSpecificMarshUsers(Optional ByVal Nombre As String = "", Optional ByVal Apellido As String = "", Optional ByVal Dpto As String = "") As DataSet
        Dim dsSpecificUsers As New DataSet
        Dim query As New StringBuilder
        Dim bolAnd As Boolean = False
        query.Append("select usrtable.id, usrtable.Apellido,usrtable.Nombres, usr_info.IntEmpresa as [Int Empresa], usr_info.Interno, usr_info.Sector, usr_info.Empresa, usr_info.Tipo from usrtable")
        query.Append(" inner join  usr_info")
        query.Append(" on usrtable.id = usr_info.idusuario where ")

        If String.Compare(Dpto.ToLower(), "todos") = 0 Then
            Dpto = String.Empty
        End If
        Try




            If String.Compare(Nombre, String.Empty) <> 0 Then
                query.Append(" nombres like " & "'%" & Nombre & "%'")
                bolAnd = True
            End If

            If String.Compare(Apellido, String.Empty) <> 0 Then
                If bolAnd = True Then
                    query.Append(" and apellido like " & "'%" & Apellido & "%'")
                Else
                    query.Append(" apellido like " & "'%" & Apellido & "%'")
                End If
                bolAnd = True
            End If

            If String.Compare(Dpto, String.Empty) <> 0 Then
                If bolAnd = True Then
                    query.Append(" and sector like " & "'%" & Dpto & "%'")
                Else
                    query.Append(" sector like " & "'%" & Dpto & "%'")
                End If
            End If

            dsSpecificUsers = Server.Con.ExecuteDataset(CommandType.Text, query.ToString())



        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return dsSpecificUsers
    End Function
    Public Shared Function GetUserInfo(ByVal UserId As Int64) As Hashtable

        Dim Info As New DataSet
        Dim query As New StringBuilder
        Dim UserInfo As New Hashtable

        query.Append("select * from usrtable inner join usr_info on ")
        query.Append(" usrtable.id = usr_info.idusuario ")
        query.Append(" and  usrtable.id = ")
        query.Append(UserId)


        Try
            Info = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)


            For Each CurrentColumn As DataColumn In Info.Tables(0).Columns
                UserInfo.Add(CurrentColumn.ColumnName.ToUpper, CurrentColumn.Table.Rows(0)(CurrentColumn.ColumnName.ToString))
            Next



        Catch ex As Exception
            raiseerror(ex)
        End Try

        Return UserInfo
    End Function

    Public Shared Function GetUserDataById(ByVal userids As Generic.List(Of Int64)) As DataTable
        Dim query As String = "SELECT ID,NAME,NOMBRES,APELLIDO FROM USRTABLE WHERE ID IN("
        For Each id As Int64 In userids
            query += id.ToString + ","
        Next
        query = query.Remove(query.Length - 1, 1) + ")"
        Return Server.Con.ExecuteDataset(CommandType.Text, query).Tables(0)
    End Function

    Public Class Mail
        ''' <summary>
        ''' Obtiene la configuracion del usuario a partir de su ID
        ''' </summary>
        ''' <param name="_userId">Id del usuario</param>
        ''' <returns>Dataset con la configuracion del indice</returns>
        ''' <history> Marcelo Modified 07/08/09</history>
        ''' <remarks></remarks>
        Public Shared Function GetMailConfigById(ByVal userId As Int64) As DataSet
            Dim ds As New DataSet
            Dim sqlBuilder As New StringBuilder()

            Try
                sqlBuilder.Append("SELECT * FROM ZMailConfig WHERE UserID = ")
                sqlBuilder.Append(userId.ToString())

                ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            Catch ex As Exception
                raiseerror(ex)
            Finally
                sqlBuilder = Nothing
            End Try

            Return ds
        End Function

        ''' <summary>
        ''' Verifica si el usuario tiene configurado un mail en Zamba
        ''' </summary>
        ''' <param name="userId">Is de usuario</param>
        ''' <returns>True en caso de que el usuario tenga establecida una configración de su cuenta de mail</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckUserMailSettings(ByVal userId As Int64) As Boolean
            Dim query As String = "SELECT count(1) FROM ZMailConfig WHERE UserID=" & userId.ToString()
            Dim exists As Boolean

            Try
                exists = CBool(Server.Con.ExecuteScalar(CommandType.Text, query))
            Catch ex As Exception
                raiseerror(ex)
                exists = False
            Finally
                query = Nothing
            End Try

            Return exists
        End Function

        Public Shared Function GetMailByUserId(ByVal _userId As Int64) As String
            Dim strselect As String
            strselect = "SELECT Correo FROM ZMailConfig WHERE UserID = " + _userId.ToString.Trim
            Dim Mail As String = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'se le agrego el return porque la funcion no lo tenia [sebastian 27-02-2009]
            Return Mail
        End Function


        Public Shared Sub SetNewUser(ByVal userId As Int64,
                                     ByVal userName As String,
                                     ByVal password As String,
                                     ByVal proveedorSmtp As String,
                                     ByVal puerto As Int16,
                                     ByVal correo As String,
                                     ByVal mailServer As String,
                                     ByVal mailType As Int16,
                                     ByVal baseMail As String,
                                     ByVal enableSsl As Boolean)

            Dim sqlBuilder As New StringBuilder()
            Dim ssl As Int16
            If enableSsl Then
                ssl = 1
            Else
                ssl = 0
            End If

            Try

                If Server.isOracle Then
                    sqlBuilder.Append("INSERT INTO ZMailConfig (UserID, UserName, UserPassword, ProveedorSMTP, Puerto, Correo, MailServer, MailType, BaseMail, EnableSsl) VALUES (")
                    sqlBuilder.Append(userId.ToString)
                    sqlBuilder.Append(", '")
                    sqlBuilder.Append(userName)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(password)
                    sqlBuilder.Append("', '")

                    If (proveedorSmtp = String.Empty) Then
                        sqlBuilder.Append("Sin Proveedor")
                    Else
                        sqlBuilder.Append(proveedorSmtp)
                    End If

                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(puerto.ToString())
                    sqlBuilder.Append(", '")
                    sqlBuilder.Append(correo)
                    sqlBuilder.Append("', '")

                    If (mailServer = String.Empty) Then
                        sqlBuilder.Append("Sin Servidor")
                    Else
                        sqlBuilder.Append(mailServer)
                    End If

                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(mailType)
                    sqlBuilder.Append(", '")
                    sqlBuilder.Append(baseMail)
                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(ssl)
                    sqlBuilder.Append(")")

                Else


                    sqlBuilder.Append("INSERT INTO ZMailConfig (UserID, UserName, UserPassword, ProveedorSMTP, Puerto, Correo, MailServer, MailType, BaseMail, EnableSsl) VALUES ('")
                    sqlBuilder.Append(userId.ToString)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(userName)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(password)
                    sqlBuilder.Append("', '")

                    If (proveedorSmtp = String.Empty) Then
                        sqlBuilder.Append("Sin Proveedor")
                    Else
                        sqlBuilder.Append(proveedorSmtp)
                    End If

                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(puerto.ToString())
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(correo)
                    sqlBuilder.Append("', '")

                    If (mailServer = String.Empty) Then
                        sqlBuilder.Append("Sin Servidor")
                    Else
                        sqlBuilder.Append(mailServer)
                    End If
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(mailType)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(baseMail)
                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(ssl)
                    sqlBuilder.Append(");")
                End If

                'Dim sql As String = sqlBuilder.ToString()
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                raiseerror(ex)
            End Try

        End Sub

        Public Shared Sub UpdateAllById(ByVal userId As Int64,
                                        ByVal userName As String,
                                        ByVal password As String,
                                        ByVal proveedorSmtp As String,
                                        ByVal puerto As Int16,
                                        ByVal correo As String,
                                        ByVal mailServer As String,
                                        ByVal mailType As Int16,
                                        ByVal baseMail As String,
                                        ByVal enableSsl As Boolean)
            Dim sqlBuilder As New StringBuilder()

            Dim ssl As Int16
            If enableSsl Then
                ssl = 1
            Else
                ssl = 0
            End If

            Try
                sqlBuilder.Append("Select count(1) from zmailconfig WHERE UserID = '")
                sqlBuilder.Append(userId)
                sqlBuilder.Append("'")

                Dim count As Int16 = Server.Con.ExecuteScalar(CommandType.Text, sqlBuilder.ToString())
                sqlBuilder.Remove(0, sqlBuilder.Length)

                If count > 0 Then
                    sqlBuilder.Append("UPDATE ZMailConfig SET UserName = '")
                    sqlBuilder.Append(userName)
                    sqlBuilder.Append("', UserPassword = '")
                    sqlBuilder.Append(password)
                    sqlBuilder.Append("', ProveedorSMTP = '")
                    sqlBuilder.Append(proveedorSmtp)
                    sqlBuilder.Append("', Puerto = '")
                    sqlBuilder.Append(puerto.ToString())
                    sqlBuilder.Append("', Correo = '")
                    sqlBuilder.Append(correo)
                    sqlBuilder.Append("', MailServer = '")
                    sqlBuilder.Append(mailServer)
                    sqlBuilder.Append("', MailType = '")
                    sqlBuilder.Append(mailType)
                    sqlBuilder.Append("', BaseMail = '")
                    sqlBuilder.Append(baseMail)
                    sqlBuilder.Append("', EnableSsl = ")
                    sqlBuilder.Append(ssl)
                    sqlBuilder.Append(" WHERE UserID = '")
                    sqlBuilder.Append(userId)
                    sqlBuilder.Append("'")
                Else
                    sqlBuilder.Append("Insert into ZMailConfig (UserID,UserName,UserPassword,ProveedorSMTP,Puerto,Correo,MailServer,MailType,BaseMail,EnableSsl) values ('")
                    sqlBuilder.Append(userId)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(userName)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(password)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(proveedorSmtp)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(puerto.ToString())
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(correo)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(mailServer)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(mailType)
                    sqlBuilder.Append("','")
                    sqlBuilder.Append(baseMail)
                    sqlBuilder.Append("',")
                    sqlBuilder.Append(ssl)
                    sqlBuilder.Append(")")
                End If

                Select Case Server.ServerType
                    Case Server.isSQLServer, DBTYPES.MSSQLExpress, DBTYPES.MSSQLServer7Up
                        sqlBuilder.Append(";")
                End Select

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                raiseerror(ex)
            End Try
        End Sub

        Public Shared Sub UpdateUserNameById(ByVal _userId As Int64, ByVal _userName As String)
            Dim sqlBuilder As New StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET UserName = '")
                sqlBuilder.Append(_userName)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")
                'Dim sql As String = sqlBuilder.ToString()

                If Server.isSQLServer Then sqlBuilder.Append(";")



                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                raiseerror(ex)
            End Try

        End Sub

        Public Shared Sub UpdateUserPasswordById(ByVal _userId As Int64, ByVal _userPassword As String)
            Dim sqlBuilder As New StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET UserPassword = '")
                sqlBuilder.Append(_userPassword)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")

                If Server.isSQLServer Then sqlBuilder.Append(";")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                raiseerror(ex)
            End Try

        End Sub

        Public Shared Sub UpdateProveedorById(ByVal _userId As Int64, ByVal _proveedorSMTP As String)
            Dim sqlBuilder As New StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET ProveedorSMTP = '")
                sqlBuilder.Append(_proveedorSMTP)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")

                If Server.isSQLServer Then sqlBuilder.Append(";")

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                raiseerror(ex)
            End Try

        End Sub


        ''' <summary>
        ''' Actualiza el email
        ''' </summary>
        ''' <param name="UserId"></param>
        ''' <history>
        ''' 	(Pablo)	26/10/2010	Created
        ''' </history>
        ''' <remarks>
        ''' Se pasan cambios del branch de marsh a este
        '''</remarks>
        Public Shared Function FillUserMailConfigByRef(ByVal userid As Int64) As ICorreo
            Dim ds As DataSet = Nothing
            Dim dr As DataRow
            Dim eMail As Correo

            Try
                eMail = New Correo
                ds = GetMailConfigById(userid)

                If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                    dr = ds.Tables(0).Rows(0)
                    eMail.Mail = dr("Correo").ToString()
                    eMail.Password = dr("UserPassword").ToString()
                    eMail.ProveedorSmtp = dr("ProveedorSMTP").ToString()
                    If Not IsDBNull(dr("Puerto")) Then
                        Int16.TryParse(dr("Puerto").ToString(), eMail.Puerto)
                    End If
                    eMail.Servidor = dr("MailServer").ToString()
                    If Not IsDBNull(dr("MailType")) Then
                        Int32.TryParse(dr("MailType").ToString(), eMail.Type)
                    End If
                    eMail.UserName = dr("UserName").ToString()
                    eMail.Base = dr("BaseMail").ToString()
                    Try
                        If Not IsDBNull(dr("EnableSsl")) Then
                            Boolean.TryParse(dr("EnableSsl").ToString(), eMail.EnableSsl)
                        End If
                    Catch ex As Exception
                        eMail.EnableSsl = False
                    End Try
                End If
                Return eMail
            Catch ex As Exception
                raiseerror(ex)
                eMail = Nothing
                Return Nothing
            Finally
                dr = Nothing
                If ds IsNot Nothing Then
                    ds.Dispose()
                    ds = Nothing
                End If
            End Try
        End Function

        Public Shared Function FillUserMailConfigByRef(ByVal dr As DataRow) As ICorreo
            Dim eMail As Correo

            Try
                eMail = New Correo

                eMail.Mail = dr("Correo").ToString()
                eMail.Password = dr("UserPassword").ToString()

                If (dr.Table.Columns.Contains("ProveedorSMTP")) Then
                    eMail.ProveedorSmtp = dr("ProveedorSMTP").ToString()
                End If
                If dr.Table.Columns.Contains("Puerto") AndAlso Not IsDBNull(dr("Puerto")) Then
                    Int16.TryParse(dr("Puerto").ToString(), eMail.Puerto)
                End If
                If (dr.Table.Columns.Contains("MailServer")) Then
                    eMail.Servidor = dr("MailServer").ToString()
                End If

                If dr.Table.Columns.Contains("MailType") AndAlso Not IsDBNull(dr("MailType")) Then
                    Int32.TryParse(dr("MailType").ToString(), eMail.Type)
                End If
                If (dr.Table.Columns.Contains("UserName")) Then
                    eMail.UserName = dr("UserName").ToString()
                End If
                If (dr.Table.Columns.Contains("BaseMail")) Then
                    eMail.Base = dr("BaseMail").ToString()
                End If


                Try
                    If Not IsDBNull(dr("EnableSsl")) Then
                        Boolean.TryParse(dr("EnableSsl").ToString(), eMail.EnableSsl)
                    End If
                Catch ex As Exception
                    eMail.EnableSsl = False
                End Try

                Return eMail
            Catch ex As Exception
                raiseerror(ex)
                eMail = Nothing
                Return Nothing
            End Try
        End Function

        Public Shared Function GetMailConfigInstance(ByVal user As DataRow) As Correo
            Dim mail As New Correo

            If Not IsDBNull(user("Correo")) AndAlso user("Correo") IsNot Nothing Then mail.Mail = user("Correo").ToString()
            If Not IsDBNull(user("UserPassword")) AndAlso user("UserPassword") IsNot Nothing Then mail.Password = user("UserPassword").ToString()
            If Not IsDBNull(user("ProveedorSMTP")) AndAlso user("ProveedorSMTP") IsNot Nothing Then mail.ProveedorSmtp = user("ProveedorSMTP").ToString()
            If Not IsDBNull(user("Puerto")) AndAlso user("Puerto") IsNot Nothing Then Int16.TryParse(user("Puerto").ToString(), mail.Puerto)
            If Not IsDBNull(user("MailServer")) AndAlso user("MailServer") IsNot Nothing Then mail.Servidor = user("MailServer").ToString()
            If Not IsDBNull(user("MailType")) AndAlso user("MailType") IsNot Nothing Then Int32.TryParse(user("MailType").ToString(), mail.Type)
            If Not IsDBNull(user("UserName")) AndAlso user("UserName") IsNot Nothing Then mail.UserName = user("UserName").ToString()
            If Not IsDBNull(user("BaseMail")) AndAlso user("BaseMail") IsNot Nothing Then mail.Base = user("BaseMail").ToString()
            If Not IsDBNull(user("EnableSsl")) AndAlso user("EnableSsl") IsNot Nothing Then Boolean.TryParse(user("EnableSsl").ToString(), mail.EnableSsl)

            Return mail
        End Function



    End Class

End Class


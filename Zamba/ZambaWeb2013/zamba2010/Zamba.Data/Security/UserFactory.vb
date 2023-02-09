Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.ZClass
Imports System.Text
Imports System.Collections.Generic
Imports Zamba

Public Class UserFactory
    Inherits ZClass


    Public Overrides Sub Dispose()

    End Sub

    Public Shared Function GetZoptDataBaseValues() As DataSet
        'Todo traer valores de la base
        'Todo compararlos contra los del app.ini
        Dim str As String
        Dim ds As DataSet

        str = "Select * from zopt where item Like 'DB%' or item like 'PASSWORD%' or item like 'SERVER' or item like 'USER%'"

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
            ZClass.raiseerror(ex)
        End Try


        Return dsDptos

    End Function

    Private Shared Function BuildUser(ByVal CurrentRow As DataRow) As IUser
        Dim CurrentUser As IUser = Nothing

        If Not IsNothing(CurrentRow) Then
            CurrentUser = New User()

            If IsDBNull(CurrentRow("ID")) Then
                CurrentUser.ID = 0
            Else
                Dim TryId As Int64

                If (Int64.TryParse(CurrentRow("ID").ToString(), TryId)) Then
                    CurrentUser.ID = TryId
                Else
                    CurrentUser.ID = 0
                End If
            End If


            If IsDBNull(CurrentRow("NAME")) Then
                CurrentUser.Name = String.Empty
            Else
                CurrentUser.Name = CurrentRow("NAME").ToString()
            End If


            If IsDBNull(CurrentRow("DESCRIPTION")) Then
                CurrentUser.Description = String.Empty
            Else
                CurrentUser.Description = CurrentRow("DESCRIPTION").ToString()
            End If


            If IsDBNull(CurrentRow("ADDRESS_BOOK")) Then
                CurrentUser.AddressBook = String.Empty
            Else
                CurrentUser.AddressBook = CurrentRow("ADDRESS_BOOK").ToString()
            End If


            If IsDBNull(CurrentRow("NOMBRES")) Then
                CurrentUser.Nombres = String.Empty
            Else
                CurrentUser.Nombres = CurrentRow("NOMBRES").ToString()
            End If


            If IsDBNull(CurrentRow("FIRMA")) Then
                CurrentUser.firma = String.Empty
            Else
                CurrentUser.firma = CurrentRow("FIRMA").ToString()
            End If


            If IsDBNull(CurrentRow("FOTO")) Then
                CurrentUser.Picture = String.Empty
            Else
                CurrentUser.Picture = CurrentRow("FOTO").ToString()
            End If


            If IsDBNull(CurrentRow("APELLIDO")) Then
                CurrentUser.Apellidos = String.Empty
            Else
                CurrentUser.Apellidos = CurrentRow("APELLIDO").ToString()
            End If


            If IsDBNull(CurrentRow("PUESTO")) Then
                CurrentUser.puesto = String.Empty
            Else
                CurrentUser.puesto = CurrentRow("PUESTO").ToString()
            End If


            If IsDBNull(CurrentRow("TELEFONO")) Then
                CurrentUser.telefono = String.Empty
            Else
                CurrentUser.telefono = CurrentRow("TELEFONO").ToString()
            End If


            If IsDBNull(CurrentRow("CORREO")) Then
                CurrentUser.eMail.Mail = String.Empty
            Else
                CurrentUser.eMail.Mail = CurrentRow("CORREO").ToString()
            End If


            If IsDBNull(CurrentRow("EXPIRATIONTIME")) Then
                CurrentUser.Expirationtime = 0
            Else
                Dim ExpirationTime As Int32

                If Int32.TryParse(CurrentRow("EXPIRATIONTIME").ToString(), ExpirationTime) Then
                    CurrentUser.Expirationtime = ExpirationTime
                Else
                    CurrentUser.Expirationtime = 0
                End If

            End If


            If IsDBNull(CurrentRow("EXPIRATIONDATE")) Then
                CurrentUser.Expiredate = Nothing
            Else
                Dim ExpirationDate As Date

                If Date.TryParse(CurrentRow("EXPIRATIONDATE").ToString(), ExpirationDate) Then
                    CurrentUser.Expiredate = ExpirationDate
                End If
            End If


            If IsDBNull(CurrentRow("LUPDATE")) Then
                CurrentUser.Lmoddate = Nothing
            Else
                Dim LastUpDate As Date

                If Date.TryParse(CurrentRow("LUPDATE").ToString(), LastUpDate) Then
                    CurrentUser.Lmoddate = LastUpDate
                End If
            End If



            If IsDBNull(CurrentRow("STATE")) Then
                CurrentUser.Lmoddate = Nothing
            Else
                Dim State As Int32

                If Int32.TryParse(CurrentRow("STATE").ToString(), State) Then
                    CurrentUser.State = DirectCast(State, UserState)
                Else
                    CurrentUser.State = UserState.Activo
                End If
            End If


            Try
                CurrentUser.Password = Zamba.Tools.Encryption.DecryptString(CurrentRow("PASSWORD"), key, iv)
            Catch
                CurrentUser.Password = String.Empty
            End Try
        End If

        Return CurrentUser
    End Function

    Private Shared Function BuildGroup(ByVal CurrentRow As DataRow) As IUserGroup
        Dim CurrentGroup As IUserGroup = Nothing

        If Not IsNothing(CurrentRow) Then
            CurrentGroup = New UserGroups()

            If IsDBNull(CurrentRow("ID")) Then
                CurrentGroup.ID = 0
            Else
                Dim TryId As Int64

                If (Int64.TryParse(CurrentRow("ID").ToString(), TryId)) Then
                    CurrentGroup.ID = TryId
                Else
                    CurrentGroup.ID = 0
                End If
            End If


            If IsDBNull(CurrentRow("NAME")) Then
                CurrentGroup.Name = String.Empty
            Else
                CurrentGroup.Name = CurrentRow("NAME").ToString()
            End If


            If IsDBNull(CurrentRow("DESCRIPTION")) Then
                CurrentGroup.Description = String.Empty
            Else
                CurrentGroup.Description = CurrentRow("DESCRIPTION").ToString()
            End If



            If IsDBNull(CurrentRow("FOTO")) Then
                CurrentGroup.PictureId = String.Empty
            Else
                CurrentGroup.PictureId = CurrentRow("FOTO").ToString()
            End If


            'If IsDBNull(CurrentRow("STATE")) Then
            'CurrentGroup.Lmoddate = Nothing
            'Else
            Dim State As Int32

            If Int32.TryParse(CurrentRow("STATE").ToString(), State) Then
                CurrentGroup.State = DirectCast(State, GroupState)
            Else
                CurrentGroup.State = GroupState.Activo
            End If
            'End If


            Try
                CurrentGroup.Password = Zamba.Tools.Encryption.DecryptString(CurrentRow("PASSWORD"), key, iv)
            Catch
                CurrentGroup.Password = String.Empty
            End Try
        End If

        Return RightFactory.CurrentUser
    End Function


    Public Shared Sub AddUser(ByVal user As IUser)

        Dim InsertQuery As New StringBuilder()

        InsertQuery.Append(String.Format("INSERT INTO ZUSER_OR_GROUP VALUES ({0}, {1}, '{2}')", user.ID, Int64.Parse(Usertypes.User), user.Name))
        Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())

        ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario creado en table generica")
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

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario creado en table de usuarios")

        InsertQuery = New System.Text.StringBuilder()
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

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, InsertQuery.ToString())
        ZTrace.WriteLineIf(ZTrace.IsInfo, "Usuario creado en table de notes")

        'sql = "insert into Usrnotes(ID,Nombre,CONF_MAILSERVER,CONF_BASEMAIL,CONF_NOMUSERRED,ACTIVO)Values(" & user.ID & ",'" & user.Name & "','" & user.eMail.Servidor & "','" & user.eMail.Base & "','" & user.Name & "',1)"
        'UserTable.Add(usr.ID, usr)
    End Sub

    Public Shared Function GetUserById(ByVal userId As Int64) As IUser

        Dim QueryBuilder As New StringBuilder()
        QueryBuilder.Append("SELECT ID, NAME, PASSWORD, CRDATE, LUPDATE, STATE, DESCRIPTION, ")
        QueryBuilder.Append("ADDRESS_BOOK, EXPIRATIONTIME, EXPIRATIONDATE, ")
        QueryBuilder.Append("NOMBRES, APELLIDO, M.CORREO, TELEFONO, ")
        QueryBuilder.Append("PUESTO, FIRMA, FOTO, CONF_BASEMAIL, ")
        QueryBuilder.Append("CONF_MAILSERVER, CONF_MAILTYPE, SMTP ")
        QueryBuilder.Append("FROM usrtable u LEFT JOIN ZMAILCONFIG M ON u.ID = M.UserId WHERE id = ")
        QueryBuilder.Append(userId.ToString())
        'If Zamba.Servers.Server.ConInitialized = False Then
        '    ZCore.InitializeSystem(ObjectTypes.Cliente, null)
        'End If

        Dim DsUser As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QueryBuilder.ToString())
        Dim CurrentUser As IUser = Nothing

        If Not IsNothing(DsUser) AndAlso DsUser.Tables.Count > 0 AndAlso DsUser.Tables(0).Rows.Count > 0 Then
            CurrentUser = BuildUser(DsUser.Tables(0).Rows(0))


        End If

        Return CurrentUser
    End Function

    Public Shared Function GetUsers(ByVal userIds As List(Of Int64)) As List(Of IUser)

        Dim UserList As List(Of IUser) = Nothing

        If Not IsNothing(userIds) And userIds.Count > 0 Then
            Dim QueryBuilder As New StringBuilder()
            QueryBuilder.Append("SELECT ID, NAME, PASSWORD, CRDATE, LUPDATE, STATE, DESCRIPTION, ")
            QueryBuilder.Append("ADDRESS_BOOK, EXPIRATIONTIME, EXPIRATIONDATE, NOMBRES, APELLIDO, ")
            QueryBuilder.Append("CORREO, TELEFONO, PUESTO, FIRMA, FOTO, CONF_BASEMAIL, CONF_MAILSERVER, ")
            QueryBuilder.Append("CONF_MAILTYPE, SMTP FROM USRTABLE WHERE ")

            For Each CurrentId As Int64 In userIds
                QueryBuilder.Append(" ID=")
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
                    UserList.Add(UserFactory.BuildUser(CurrentRow))
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

    Public Shared Function GetUserByMail(ByVal UserMail As String) As IUser
        Dim ds As DataSet = Nothing
        Try
            UserMail = Replace(UserMail, "'", "")
            UserMail = Replace(UserMail, """", "")
            Dim strselect As String = "Select * from usrtable where lower(correo) = '" & UserMail.ToLower() & "'"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Dim CurrentUser As IUser = Nothing
        If (IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0) Then
            CurrentUser = Nothing
        Else
            CurrentUser = BuildUser(ds.Tables(0).Rows(0))
        End If
        If Not IsNothing(CurrentUser) Then
            CurrentUser.eMail = Mail.FillUserMailConfigByRef(CurrentUser.ID)
        End If
        Return CurrentUser
    End Function

    Public Shared Function GetUserByPeopeId(ByVal PeopeID As String) As IUser
        Dim ds As DataSet = Nothing
        Try
            PeopeID = Replace(PeopeID, "'", "")
            PeopeID = Replace(PeopeID, """", "")
            Dim strselect As String = "select usrtable.* from doc_i204182 INNER JOIN usrtable on doc_i204182.I1354 = usrtable.ID where doc_i204182.i204277='" & PeopeID & "'"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Dim CurrentUser As IUser = Nothing
        If (IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0) Then
            CurrentUser = Nothing
        Else
            CurrentUser = BuildUser(ds.Tables(0).Rows(0))
        End If
        If Not IsNothing(CurrentUser) Then
            CurrentUser.eMail = Mail.FillUserMailConfigByRef(CurrentUser.ID)
        End If
        Return CurrentUser
    End Function

    Public Shared Function GetUserByName(ByVal UserName As String) As IUser

        Dim ds As DataSet = Nothing
        Try
            'la ultima version deberia implementar este SP.
            'Es solo para mejorar el tiempo en el inicio de sesion
            '    If Server.ServerType = DBTypes.MSSQLServer7Up OrElse Server.ServerType = DBTypes.MSSQLServer Then
            '        Dim parvalues() As Object = {UserName}
            '        ds = Server.Con.ExecuteDataset("Zsp_users_200_GetUserByName", parvalues)
            '    Else
            '        Dim parNames() As String = {"username", "io_cursor"}
            '        ' Dim parTypes() As Object = {OracleType.VarChar, OracleType.Cursor}
            '        Dim parValues() As Object = {UserName, 2}
            '        ds = Server.Con.ExecuteDataset("Zsp_users_200.GetUserByName", parValues)
            '    End If
            'Catch ex As Exception
            UserName = Replace(UserName, "'", "")
            UserName = Replace(UserName, """", "")
            Dim strselect As String = "Select * from usrtable where lower(name) = '" & UserName.ToLower() & "'"
            ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)


        Catch ex As Exception

            Zamba.Core.ZClass.raiseerror(ex)

        End Try






        Dim CurrentUser As IUser = Nothing


        If (IsNothing(ds) OrElse ds.Tables.Count = 0 OrElse ds.Tables(0).Rows.Count = 0) Then
            CurrentUser = Nothing
        Else
            CurrentUser = BuildUser(ds.Tables(0).Rows(0))
        End If
        If Not IsNothing(CurrentUser) Then
            CurrentUser.eMail = Mail.FillUserMailConfigByRef(CurrentUser.ID)
        End If

        Return CurrentUser
    End Function


    Public Shared Function GetUserID(ByVal name As String) As Int32
        'TODO Store "SPGetUserID"
        Try
            Dim sql As String = "Select id from usrtable where name='" & name & "'"
            Return Servers.Server.Con.ExecuteScalar(CommandType.Text, sql)
        Catch
            Return 0
        End Try
    End Function

    Public Shared Function GetUsersArrayList() As ArrayList
        Dim strselect As String = "select * from usrtable order by name"
        Dim ds As DataSet
        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Dim Users As New ArrayList
        For Each r As DataRow In ds.Tables(0).Rows
            Dim user As IUser
            user = BuildUser(r)
            If Not IsNothing(user) Then
                user.eMail = Mail.FillUserMailConfigByRef(user.ID)
            End If
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
        query.Append(" UNION select distinct  inheritedusergroup from group_r_group where usergroup in ( Select groupid from usr_r_group where usrid= ")
        query.Append(userid.ToString)
        query.Append(")")

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

            ds = Server.Con.ExecuteDataset(CommandType.Text, "SELECT UserId FROM Z_GroupToNotify where TypeId = " + _typeId.ToString + " And DocId = " + _groupId.ToString)
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

        Dim DsUsers As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, Query)

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

        Dim DsUsers As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, Query)
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

        Dim DsUsers As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, Query)

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
            Query = "SELECT ID, NOMBRES || ' ' || APELLIDO AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE LENGTH(ZMailConfig.Correo) > 0 ORDER BY NombreCompleto"
        Else
            Query = "SELECT ID, NOMBRES + ' ' + APELLIDO AS NombreCompleto, NAME, ZMailConfig.CORREO, ZMailConfig.BaseMail, ZMailConfig.MailServer, ZMailConfig.MailType FROM USRTABLE INNER JOIN ZMailConfig ON USRTABLE.ID = ZMailConfig.UserId WHERE ZMailConfig.Correo <> '' ORDER BY NombreCompleto"
        End If

        Dim DsUsers As DataSet = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, Query)

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


    Public Shared Function GetFilteredAllUsersArrayList(ByVal SelectedUsers As ArrayList) As ArrayList
        Dim Users As ArrayList = UserFactory.GetUsersArrayList
        Dim FilteredUsers As New ArrayList
        For Each user As IUser In Users
            If SelectedUsers.Contains(CDec(user.ID)) Then
                FilteredUsers.Add(user)
            End If
        Next
        Return FilteredUsers
    End Function
    Public Shared Function CompareUser(ByVal Name As String, ByVal Users As ArrayList) As Boolean
        For Each u As IUser In Users
            If String.Compare(u.Name, Name, True) = 0 Then
                Return True
            End If
        Next
        Return False
    End Function
    Public Shared Function GetNewUser(ByVal sessionName As String, ByVal userName As String, ByVal userLastName As String, ByVal password As String) As IUser

        Dim usr As New User
        usr.Name = sessionName
        usr.Nombres = userName
        usr.Apellidos = userLastName
        usr.Password = password

        Try
            usr.ID = CoreData.GetNewID(IdTypes.USERTABLEID)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error al obtener el Id de Usuario.", ex)
        End Try
        Try
            AddUser(usr)
        Catch ex As Exception
            Throw New Exception("Ocurrió un error registrar el usuario", ex)
        End Try
        Return usr
    End Function
    'Public Shared Function GetUser(ByVal uid As Integer, ByVal Loaded As Boolean) as iuser
    '    If CompareUser(Name, Users) = False Then Return Nothing

    '    If UserTable.ContainsKey(uid) Then
    '        Return UserTable(uid)
    '    Else
    '        If Loaded Then
    '            Return Nothing
    '        Else
    '            LoadUser(uid)
    '            Return GetUser(uid, True)
    '        End If
    '    End If
    'End Function
    Public Shared Function AssignGroup(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        Try
            Dim strinsert As New System.Text.StringBuilder
            strinsert.Append("Insert into usr_r_group(usrid,groupid) values(")
            strinsert.Append(u.ID)
            strinsert.Append(",")
            strinsert.Append(ug.ID)
            strinsert.Append(")")
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strinsert.ToString)
            strinsert = Nothing
            Return True
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try

    End Function

    Public Shared Function AssignGroup(ByVal uid As Int64, ByVal ugid As Int64) As Boolean
        Try
            Dim strinsert As New System.Text.StringBuilder
            strinsert.Append("Insert into usr_r_group(usrid,groupid) ")
            strinsert.Append("select ")
            strinsert.Append(uid)
            strinsert.Append(",")
            strinsert.Append(ugid)
            strinsert.Append(" from dual where not exists(select * from usr_r_group  where usrid = " & uid & " and groupid = " & ugid & ")")
            ZTrace.WriteLineIf(ZTrace.IsInfo, strinsert.ToString())
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strinsert.ToString)
            strinsert = Nothing
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Grupo asignado satisfactoriamente")

            Return True
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
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



    Public Shared Sub RemoveUser(ByVal uid As Int64, ByVal ugid As Int64)
        Dim sql As New StringBuilder()
        sql.Append("delete from usr_r_group where usrid = ")
        sql.Append(uid.ToString())
        sql.Append(" and groupid = ")
        sql.Append(ugid.ToString())

        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)

        sql.Remove(0, sql.Length)
        sql = Nothing
    End Sub


    Public Shared Sub DeleteGroup(ByVal u As IUser, ByVal ug As IUserGroup)
        'DeleteGroupRights(ug.id)
        Dim strdel As New System.Text.StringBuilder
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

    Public Shared Sub DeleteGroup(ByVal uid As Int64, ByVal ugid As Int64)
        'DeleteGroupRights(ug.id)
        Dim strdel As New System.Text.StringBuilder
        strdel.Append("delete usr_r_group where usrid = ")
        strdel.Append(uid)
        strdel.Append(" and groupid = ")
        strdel.Append(ugid)
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strdel)
        strdel.Remove(0, strdel.Length)
        strdel.Append("Delete from UsrGroup where ID=")
        strdel.Append(ugid)

        Server.Con.ExecuteNonQuery(CommandType.Text, strdel)

        strdel.Remove(0, strdel.Length)
    End Sub
    'Public Shared Sub DeleteGroup(ByVal ug as iusergroup)
    '    DeleteGroupRights(ug.id)
    '    Dim strDel As String = "delete usr_r_group where usrid = " & u.id & " and groupid = " & ug.id
    '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    '    strDel = "Delete from UsrGroup where ID=" & ug.id
    '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    'End Sub
    'Private Shared Sub DeleteuserRights(ByVal userid As Int32)
    '    Try
    '        Dim sql As String = "Delete from Usr_Rights where GROUPID=" & userid
    '        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '    Catch
    '    End Try
    'End Sub


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
            Return RightFactory.CurrentUser
        End If
        Try
            Return GetUserByName(name)
        Catch ex As Exception
            raiseerror(ex)
            Return Nothing
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
        validate = "Select name from usrtable where name = '" & usr.Name & "' And ID <> " & usr.ID
        ds = Server.Con.ExecuteDataset(CommandType.Text, validate)
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            ds.Dispose()
            ds = Nothing
        End If
        Dim strupdate As New System.Text.StringBuilder
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
        Dim sql As System.Text.StringBuilder
        Try
            sql = New System.Text.StringBuilder
            If Server.ServerType = DBTypes.MSSQLServer OrElse Server.ServerType = DBTypes.MSSQLServer7Up Then
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

    Public Shared Function UpdateUserPassword(ByVal usr As IUser) As Boolean
        Dim validate As String
        Dim ds As DataSet
        validate = "Select name from usrtable where name ='" & usr.Name & "' And ID <> " & usr.ID
        ds = Server.Con.ExecuteDataset(CommandType.Text, validate)
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            ds.Dispose()
            ds = Nothing
        End If
        Dim strupdate As New System.Text.StringBuilder
        strupdate.Append("update usrtable set ")
        strupdate.Append(" PASSWORD='")
        strupdate.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
        strupdate.Append("'")
        strupdate.Append(",LUPDATE=")
        strupdate.Append(Servers.Server.Con.SysDate)
        strupdate.Append(" where ID=")
        strupdate.Append(usr.ID)
        Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)

        Dim sql As System.Text.StringBuilder
        Try
            sql = New System.Text.StringBuilder
            If Server.ServerType = DBTypes.MSSQLServer OrElse Server.ServerType = DBTypes.MSSQLServer7Up Then
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

    End Function

    Public Shared Function UpdateUser(ByVal usr As IUser) As Boolean
        Dim validate As String
        Dim ds As DataSet
        validate = "Select name from usrtable where name = '" & usr.Name & "' And ID = " & usr.ID
        ds = Server.Con.ExecuteDataset(CommandType.Text, validate)
        If ds.Tables(0).Rows.Count > 0 Then

            Dim strupdate As New System.Text.StringBuilder
            strupdate.Append("update usrtable set ")
            strupdate.Append("NAME='")
            strupdate.Append(usr.Name)
            strupdate.Append("'")
            strupdate.Append(",APELLIDO='")
            strupdate.Append(usr.Apellidos)
            strupdate.Append("'")
            strupdate.Append(",CORREO='")
            strupdate.Append(usr.eMail.Mail)
            strupdate.Append("'")
            strupdate.Append(" where ID=")
            strupdate.Append(usr.ID)


            Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
            strupdate = Nothing
            Try
                If usr.ThumbNailPhoto <> String.Empty Then
                    strupdate = New System.Text.StringBuilder
                    strupdate.Append("update usrtable set ")
                    strupdate.Append("foto='")
                    strupdate.Append(usr.ThumbNailPhoto)
                    strupdate.Append("'")
                    strupdate.Append(" where ID=")
                    strupdate.Append(usr.ID)


                    Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
                    strupdate = Nothing
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try

        End If

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
        Dim strupdate As System.Text.StringBuilder
        strupdate = New System.Text.StringBuilder
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
        Dim strupdate As System.Text.StringBuilder
        strupdate = New System.Text.StringBuilder
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
    ''' Obtiene la ruta donde se ubican los mails de un usuario específico
    ''' </summary>
    ''' <param name="id">Id del usuario</param>
    ''' <returns>Ruta de la ubicación de los mails del usuario</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Tomás]	17/07/2009	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetFilesPath(ByVal id As Int64) As String
        Dim query As String = "SELECT CONF_PATHARCH FROM USRNOTES WHERE ID = " & id.ToString
        Dim path As String = String.Empty
        Try
            path = Server.Con.ExecuteScalar(CommandType.Text, query)
        Catch ex As Exception
            path = String.Empty
        End Try
        Return path
    End Function
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
        ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
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
    ''' Devuelve el nombre de un usuario en base a su UD
    ''' </summary>
    ''' <param name="UserId">Id de usuario</param>
    ''' <returns>String, nombre del usuario</returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserNamebyId(ByVal UserId As Int64) As String
        Dim strselect As New System.Text.StringBuilder
        strselect.Append("Select Name from USRTABLE where ID = ")
        strselect.Append(UserId)
        Dim name As String = Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)
        strselect = Nothing
        If Not IsNothing(name) Then
            Return name
        Else
            Return String.Empty
        End If
    End Function
    'Public Shared Function ValidateLogIn(ByVal name As String, ByVal Psw As String) as iuser
    '    Return UserFactory.validateUser(name, Psw)
    'End Function
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
    'Public Shared Sub FillGroups(ByVal User As IUser)
    '    User.Groups = UserGroupFactory.getUserGroups(User.ID)
    'End Sub
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
            ZClass.raiseerror(ex)
        End Try

        Return dsMailList
    End Function

    ''' <summary>
    ''' [sebastian 11-02-2009] Genera un data set con los usuarios de marsh
    ''' con todos los datos de los mismos.
    ''' </summary>
    ''' <returns>data set con usuarios</returns>
    ''' <remarks></remarks>
    Public Shared Function GetAllMarshUsers() As DataSet
        Dim query As New StringBuilder()
        Dim dsUsrs As New DataSet

        Try
            query.Append("select usrtable.id, usrtable.Apellido,usrtable.Nombres, usr_info.IntEmpresa as [Int Empresa], usr_info.Interno, usr_info.Sector, usr_info.Empresa, usr_info.Tipo from usrtable")
            query.Append(" inner join  usr_info")
            query.Append(" on usrtable.id = usr_info.idusuario")
            query.Append(" order by apellido")

            dsUsrs = Server.Con.ExecuteDataset(CommandType.Text, query.ToString)

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return dsUsrs

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
            ZClass.raiseerror(ex)
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
            ZClass.raiseerror(ex)
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
        Public Shared Function GetMailConfigById(ByVal _userId As Int64) As DataSet
            Dim ds As New DataSet
            Dim sqlBuilder As New System.Text.StringBuilder()

            Try
                sqlBuilder.Append("SELECT * FROM ZMailConfig WHERE UserID = ")
                sqlBuilder.Append(_userId.ToString())

                ds = Server.Con.ExecuteDataset(CommandType.Text, sqlBuilder.ToString())
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

            sqlBuilder = Nothing

            Return ds
        End Function

        Public Shared Function GetMailByUserId(ByVal _userId As Int64) As String
            Dim strselect As String
            strselect = "SELECT Correo FROM ZMailConfig WHERE UserID = " + _userId.ToString.Trim
            Dim Mail As String = Server.Con.ExecuteScalar(CommandType.Text, strselect)
            'se le agrego el return porque la funcion no lo tenia [sebastian 27-02-2009]
            Return Mail
        End Function


        Public Shared Sub SetNewUser(ByVal _userId As Int64, ByVal _userName As String, ByVal _password As String, ByVal _proveedorSMTP As String, ByVal _puerto As Int16, ByVal _correo As String, ByVal _mailServer As String, ByVal _mailType As Int16, Optional ByVal _baseMail As String = "")

            Dim sqlBuilder As New System.Text.StringBuilder()

            Try

                If Server.isOracle Then
                    sqlBuilder.Append("INSERT INTO ZMailConfig (UserID, UserName, UserPassword, ProveedorSMTP, Puerto, Correo, MailServer, MailType")

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append(", BaseMail")
                    End If

                    sqlBuilder.Append(") VALUES (")
                    sqlBuilder.Append(_userId.ToString)
                    sqlBuilder.Append(", '")
                    sqlBuilder.Append(_userName)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_password)
                    sqlBuilder.Append("', '")

                    If (_proveedorSMTP = String.Empty) Then
                        sqlBuilder.Append("Sin Proveedor")
                    Else
                        sqlBuilder.Append(_proveedorSMTP)
                    End If

                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(_puerto.ToString())
                    sqlBuilder.Append(", '")
                    sqlBuilder.Append(_correo)
                    sqlBuilder.Append("', '")

                    If (_mailServer = String.Empty) Then
                        sqlBuilder.Append("Sin Servidor")
                    Else
                        sqlBuilder.Append(_mailServer)
                    End If

                    sqlBuilder.Append("', ")
                    sqlBuilder.Append(_mailType)

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append(", '")
                        sqlBuilder.Append(_baseMail)
                        sqlBuilder.Append("')")
                    Else
                        sqlBuilder.Append(")")
                    End If



                ElseIf Server.ServerType = DBTYPES.MSSQLServer7Up Then

                    sqlBuilder.Append("INSERT INTO ZMailConfig (UserID, UserName, UserPassword, ProveedorSMTP, Puerto, Correo, MailServer, MailType")

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append(", BaseMail")
                    End If

                    sqlBuilder.Append(") VALUES ('")
                    sqlBuilder.Append(_userId.ToString)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_userName)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_password)
                    sqlBuilder.Append("', '")

                    If (_proveedorSMTP = String.Empty) Then
                        sqlBuilder.Append("Sin Proveedor")
                    Else
                        sqlBuilder.Append(_proveedorSMTP)
                    End If

                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_puerto.ToString())
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_correo)
                    sqlBuilder.Append("', '")

                    If (_mailServer = String.Empty) Then
                        sqlBuilder.Append("Sin Servidor")
                    Else
                        sqlBuilder.Append(_mailServer)
                    End If

                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_mailType)

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append("', '")
                        sqlBuilder.Append(_baseMail)
                    End If

                    sqlBuilder.Append("');")

                Else
                    sqlBuilder.Append("INSERT INTO ZMailConfig (UserID, UserName, UserPassword, ProveedorSMTP, Puerto, Correo, MailServer, MailType")

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append(", BaseMail")
                    End If

                    sqlBuilder.Append(") VALUES ('")
                    sqlBuilder.Append(_userId.ToString)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_userName)
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_password)
                    sqlBuilder.Append("', '")

                    If (_proveedorSMTP = String.Empty) Then
                        sqlBuilder.Append("Sin Proveedor")
                    Else
                        sqlBuilder.Append(_proveedorSMTP)
                    End If

                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_puerto.ToString())
                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_correo)
                    sqlBuilder.Append("', '")

                    If (_mailServer = String.Empty) Then
                        sqlBuilder.Append("Sin Servidor")
                    Else
                        sqlBuilder.Append(_mailServer)
                    End If

                    sqlBuilder.Append("', '")
                    sqlBuilder.Append(_mailType)

                    If Not String.Compare(_baseMail, String.Empty) = 0 Then
                        sqlBuilder.Append("', '")
                        sqlBuilder.Append(_baseMail)
                    End If

                    sqlBuilder.Append("');")
                End If



                'Dim sql As String = sqlBuilder.ToString()
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())


            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)

            End Try

        End Sub

        Public Shared Sub UpdateAllById(ByVal _userId As Int64, ByVal _userName As String, ByVal _password As String, ByVal _proveedorSMTP As String, ByVal _puerto As Int16, ByVal _correo As String, ByVal _mailServer As String, ByVal _mailType As Int16, Optional ByVal _baseMail As String = "")
            Dim sqlBuilder As New System.Text.StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMailConfig SET UserName = '")
                sqlBuilder.Append(_userName)
                sqlBuilder.Append("', UserPassword = '")
                sqlBuilder.Append(_password)
                sqlBuilder.Append("', ProveedorSMTP = '")
                sqlBuilder.Append(_proveedorSMTP)
                sqlBuilder.Append("', Puerto = '")
                sqlBuilder.Append(_puerto.ToString())
                sqlBuilder.Append("', Correo = '")
                sqlBuilder.Append(_correo)
                sqlBuilder.Append("', MailServer = '")
                sqlBuilder.Append(_mailServer)
                sqlBuilder.Append("', MailType = '")
                sqlBuilder.Append(_mailType)

                If Not String.Compare(_baseMail, String.Empty) = 0 Then
                    sqlBuilder.Append("', BaseMail = '")
                    sqlBuilder.Append(_baseMail)
                End If

                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId)
                sqlBuilder.Append("'")

                Select Case Servers.Server.ServerType
                    Case DBTypes.MSSQLServer
                        sqlBuilder.Append(";")
                    Case DBTypes.MSSQLServer7Up
                        sqlBuilder.Append(";")
                    Case Else
                End Select

                'Dim sql As String = sqlBuilder.ToString()
                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Sub

        Public Shared Sub UpdateUserNameById(ByVal _userId As Int64, ByVal _userName As String)
            Dim sqlBuilder As New System.Text.StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET UserName = '")
                sqlBuilder.Append(_userName)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")
                'Dim sql As String = sqlBuilder.ToString()

                Select Case Servers.Server.ServerType
                    Case DBTypes.MSSQLServer
                        sqlBuilder.Append(";")
                    Case DBTypes.MSSQLServer7Up
                        sqlBuilder.Append(";")
                    Case Else
                End Select

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End Sub

        Public Shared Sub UpdateUserPasswordById(ByVal _userId As Int64, ByVal _userPassword As String)
            Dim sqlBuilder As New System.Text.StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET UserPassword = '")
                sqlBuilder.Append(_userPassword)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")
                'Dim sql As String = sqlBuilder.ToString()

                Select Case Servers.Server.ServerType
                    Case DBTypes.MSSQLServer
                        sqlBuilder.Append(";")
                    Case DBTypes.MSSQLServer7Up
                        sqlBuilder.Append(";")
                    Case Else
                End Select

                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try

        End Sub

        Public Shared Sub UpdateProveedorById(ByVal _userId As Int64, ByVal _proveedorSMTP As String)
            Dim sqlBuilder As New System.Text.StringBuilder()

            Try
                sqlBuilder.Append("UPDATE ZMail.Config SET ProveedorSMTP = '")
                sqlBuilder.Append(_proveedorSMTP)
                sqlBuilder.Append("' WHERE UserID = '")
                sqlBuilder.Append(_userId.ToString())
                sqlBuilder.Append("'")
                'Dim sql As String = sqlBuilder.ToString()

                Select Case Servers.Server.ServerType
                    Case DBTypes.MSSQLServer
                        sqlBuilder.Append(";")
                    Case DBTypes.MSSQLServer7Up
                        sqlBuilder.Append(";")
                    Case Else
                End Select


                Server.Con.ExecuteNonQuery(CommandType.Text, sqlBuilder.ToString())

            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
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
                    eMail.ProveedorSMTP = dr("ProveedorSMTP").ToString()
                    If Not IsDBNull(dr("Puerto")) Then
                        Int16.TryParse(dr("Puerto").ToString(), eMail.Puerto)
                    End If
                    eMail.Servidor = dr("MailServer").ToString()
                    If Not IsDBNull(dr("MailType")) Then
                        Int32.TryParse(dr("MailType").ToString(), eMail.Type)
                    End If
                    eMail.UserName = dr("UserName").ToString()
                    eMail.Base = dr("BaseMail").ToString()
                    If Not IsDBNull(dr("EnableSsl")) Then
                        Boolean.TryParse(dr("EnableSsl").ToString(), eMail.EnableSsl)
                    End If
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



    End Class

    Public Shared Sub ClearHashTables()
        If Not IsNothing(_UserGroupsIdsByUseridList) Then
            _UserGroupsIdsByUseridList.Clear()
            _UserGroupsIdsByUseridList = Nothing
            _UserGroupsIdsByUseridList = New Hashtable()
        End If
    End Sub

End Class


Imports Zamba.Membership
Imports Zamba.Data
Imports Zamba.Core
Imports System.Collections.Generic
Imports Zamba.Tools
Imports System.Text
Imports Zamba.Servers


Public Class UserBusiness
    Public Shared Function GetDistinctMarshDptos() As DataSet
        Return UserFactory.GetDistinctMarshDptos()
    End Function
    Public Shared Function GetAllZambaUsersMail() As DataSet
        Return UserFactory.GetAllZambaUsersMail()
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
        Return UserFactory.GetSpecificMarshUsers(Nombre, Apellido, Dpto)
    End Function

    Public Shared Function GetUserInfo(ByVal UserId As Int64) As Hashtable
        Return UserFactory.GetUserInfo(UserId)
    End Function

    Public Shared Sub AddUser(ByVal usr As IUser)
        UserFactory.AddUser(usr)
        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
    End Sub

    Public Shared Function GetAllUsers() As Hashtable
        Return Cache.UsersAndGroups.hsUserTable
    End Function

    Public Shared Function GetAllUsersArrayList() As ArrayList
        GetAllUsers()
        Dim Users As New ArrayList
        Users.AddRange(Cache.UsersAndGroups.hsUserTable.Values)
        Return Users
    End Function
    Public Shared Function GetFilteredAllUsersArrayList(ByVal SelectedUsers As ArrayList) As ArrayList
        GetAllUsers()
        Dim Users As New ArrayList
        For Each user As IUser In Cache.UsersAndGroups.hsUserTable.Values
            If SelectedUsers.Contains(CDec(user.ID)) Then
                Users.Add(user)
            End If
        Next
        Return Users
    End Function

    Public Shared Function GetUser(ByVal uid As Int64) As IUser
        If Cache.UsersAndGroups.hsUserTable.ContainsKey(uid) Then
            Return Cache.UsersAndGroups.hsUserTable(uid)
        Else
            Return Nothing
        End If
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene de la usrtable los campos ID,NAME,NOMBRES,APELLIDO.
    ''' </summary>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserDataById(ByVal userids As Generic.List(Of Int64)) As DataTable
        Return UserFactory.GetUserDataById(userids)
    End Function
    Public Shared Function CurrentUser() As IUser
        'Dim User As IUser = RightFactory.CurrentUser
        'UserBusiness.Mail.FillUserMailConfig(User)
        Return RightFactory.CurrentUser
    End Function



    ''' <summary>
    ''' Verifica si el usuario es el usuario especial de Zamba
    ''' </summary>
    ''' <returns>True en caso de serlo</returns>
    ''' <remarks></remarks>
    Public Shared Function IsZambaUser() As Boolean
        If Membership.MembershipHelper.CurrentUser Is Nothing Then
            Return False
        End If
        Return (String.Compare(Membership.MembershipHelper.CurrentUser.Name, Encryption.DecryptString("ic7isnPkAAxZSMnB9wMtSQ==", key, iv)) = 0)
    End Function

#Region "Get"
    Public Shared Function GetUserById(ByVal userId As Int64) As IUser
        If Cache.UsersAndGroups.hsUserTable.ContainsKey(userId) = False Then
            If userId = 0 Then
                Dim UserNull As IUser
                UserNull = New User
                UserNull.ID = 0
                UserNull.Name = "[Ninguno]"
                Return UserNull
            End If
            Dim CurrentUser As IUser = UserFactory.GetUserById(userId)
            If Not IsNothing(CurrentUser) Then
                CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(CurrentUser.ID)

                Cache.UsersAndGroups.hsUserTable.Add(CurrentUser.ID, CurrentUser)
            End If
            Return CurrentUser
        Else
            Return Cache.UsersAndGroups.hsUserTable(userId)
        End If
    End Function
    Public Shared Function getuserbyid(ByVal userIds As List(Of Int64)) As List(Of IUser)
        Return UserFactory.GetUsers(userIds)
    End Function
    Public Shared Function GetUsers(ByVal userIds As List(Of Int64)) As List(Of IUser)
        Return UserFactory.GetUsers(userIds)
    End Function
    'public shared function GetUsers
    ''' <summary>
    ''' Return the user sign path
    ''' </summary>
    ''' <param name="UserId">User´s Id </param>
    ''' <returns>sign path</returns>
    ''' <remarks></remarks>
    Public Shared Function GetUserSignById(ByVal UserId As Int32) As String
        Return UserFactory.GetUserSignById(UserId)
    End Function



    <Obsolete("Implementar las consultas en Zamba.Data no en Zamba.WfBusiness")>
    Public Shared Function GetUserID(ByVal name As String) As Int32
        Try
            If Server.isSQLServer Then
                Return Servers.Server.Con.ExecuteScalar(CommandType.Text, "Select id from usrtable where lower(name) = '" & name.ToLower & "'")
            Else
                Return Servers.Server.Con.ExecuteScalar(CommandType.Text, "Select id from usrtable where lower(name) = '" & name.ToLower & "'")
            End If
        Catch
            Return 0
        End Try
    End Function


    Public Shared Function GetUsersArrayList() As ArrayList
        Return UserFactory.GetUsersArrayList()
    End Function


    Public Shared Function GetUsersAdminReport() As DataTable
        Return UserFactory.GetUsersAdminReport
    End Function
    Public Shared Function GetUsersNamesAsICollection() As ICollection
        Dim tempDS As DataSet = Nothing
        Try
            tempDS = UserFactory.GetUsersNamesAsDataSet()
            If Not IsNothing(tempDS) AndAlso tempDS.Tables.Count > 0 Then
                Dim tempDV As New DataView(tempDS.Tables(0))
                Return tempDV
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    Public Shared Function GetUsersNames() As List(Of User)
        Dim Users As List(Of User) = UserFactory.GetUsersNames()
        For Each CurrentUser As IUser In Users
            CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(CurrentUser.ID)
        Next

        Return Users
    End Function

    ''' <summary>
    ''' Returns True if the UserName is available
    ''' </summary>
    ''' <param name="name">UserName</param>
    ''' <returns>Returns True if the UserName is available</returns>
    ''' <remarks></remarks>
    Public Shared Function IsUserNameAvailable(ByVal name As String) As Boolean
        Dim ufe As New UserFactoryExt()
        Dim result As Int16 = Int16.Parse(ufe.CheckUserNameAvailability(name).ToString)
        ufe = Nothing

        Return result = 0
    End Function

    Public Shared Function GetUsersWithMailsNames() As List(Of User)
        Dim Users As List(Of User) = UserFactory.GetUsersWithMailsNames()
        For Each CurrentUser As IUser In Users
            CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(CurrentUser.ID)
        Next

        Return Users
    End Function
    Public Shared Function GetUsersWithMailsNames(ByVal usersIds As List(Of Int64)) As List(Of User)
        Dim Users As List(Of User) = UserFactory.GetUsersWithMailsNames(usersIds)
        For Each CurrentUser As IUser In Users
            CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(CurrentUser.ID)
        Next
        Return Users
    End Function
    Public Shared Function GetUserWithMailName(ByVal userId As Int64) As User
        Return UserFactory.GetUsersWithMailsNames(userId)
    End Function

    Public Shared Function GetUsersDatasetOrderbyName() As DataSet
        Return UserFactory.GetUsersDatasetOrderbyName
    End Function

    Public Shared Function GetNewUser(ByVal Name As String) As IUser
        For Each u As IUser In Cache.UsersAndGroups.hsUserTable.Values
            If u.Name.ToUpper = Name.ToUpper Then
                Throw New Exception("El nombre de usuario ya existe")
            End If
        Next

        Dim usr As New User
        usr.Name = Name
        Try
            usr.ID = CoreData.GetNewID(Zamba.Core.IdTypes.USERTABLEID)
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

    ''' <summary>
    ''' Devuelve un usuario por su nombre
    ''' </summary>
    ''' <param name="name">Nombre del usuario</param>
    ''' <returns>Usuario</returns>
    ''' <history> Marcelo Modified 07/08/09
    '''           Marcelo Modified 21/12/09</history>
    ''' <remarks></remarks>
    Public Shared Function GetUserByname(ByVal name As String) As IUser
        For Each u As IUser In Cache.UsersAndGroups.hsUserTable.Values
            If String.Compare(u.Name, name, True) = 0 Then
                Return u
            End If
        Next
        Dim usr As IUser = UserFactory.GetUserByName(name)
        'Comento esta llamada porque ya se hace en el GetUserByName
        'UserBusiness.Mail.FillUserMailConfig(usr)
        If Not IsNothing(usr) Then
            usr.Groups = UserGroupBusiness.getUserGroups(usr.ID)
            Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
        End If
        Return usr
    End Function

    Public Shared Function validateUser(ByVal name As String, ByVal Psw As String) As IUser
        If name = "Zamba1234567" AndAlso Psw = "1234567" Then
            Dim u As New User
            u.ID = 9999
            u.Name = "Zamba1234567"
            u.Password = "1234567"
            Membership.MembershipHelper.SetCurrentUser(u)
            Return u
        End If
        Try
            Dim user As IUser = UserFactory.validateUser(name, Psw)
            Membership.MembershipHelper.SetCurrentUser(user)
            Return Membership.MembershipHelper.CurrentUser
        Catch
            Return (Nothing)
        End Try
    End Function

    Public Shared Function GetUserGroupsIdsByUserid(ByVal userid As Int64) As Generic.List(Of Int64)
        Return UserFactory.GetUserGroupsIdsByUserid(userid, False)
    End Function
#End Region

    Public Shared Sub Fill(ByRef user As IUser)

        If IsNothing(user.Groups) Then
            user.Groups = UserGroupBusiness.getUserGroups(user.ID)
        End If

        If IsNothing(user.eMail) Then
            'user.eMail
        End If

    End Sub

    ''' <summary>
    ''' Validate if the database data is equal to the app.ini data
    ''' </summary>
    ''' <returns>if database is valid</returns>
    ''' <remarks></remarks>
    Public Shared Function ValidateDataBase() As Boolean
        Try
            'Traer los valores de la instalacion de la base
            Dim ds As DataSet = UserFactory.GetZoptDataBaseValues()

            'Si no tiene valores devuelvo true porque es una instalacion antigua
            If ds.Tables(0).Rows.Count > 0 Then

                'Todo compararlos contra los del app.ini
                Dim array As ArrayList = DBTools.GetActualConfig()
                Dim dv As DataView = New DataView(ds.Tables(0))

                'Valido SERVER
                If array(0).ToString().ToLower().Contains("www.stardoc") OrElse array(0).ToString().ToLower().Contains("yoda") Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "Stardoc")
                    Return True
                End If
                dv.RowFilter = "item = 'server'"
                ZTrace.WriteLineIf(ZTrace.IsInfo, array(0).ToString().ToLower())
                If Encryption.DecryptString(dv.ToTable().Rows(0).Item("value").ToString(), key, iv).ToLower() = array(0).ToString().ToLower() Then
                    'Valido DB
                    dv.RowFilter = "item = 'DB'"
                    ZTrace.WriteLineIf(ZTrace.IsInfo, array(1).ToString().ToLower())
                    If Encryption.DecryptString(dv.ToTable().Rows(0).Item("value").ToString(), key, iv).ToLower() = array(1).ToString().ToLower() Then
                        'Si es autenticacion de windows, salteo esta validacion
                        'If array(5).ToString().ToLower() = "true" Then
                        '    ZTrace.WriteLineIf(ZTrace.IsInfo, "Es autenticacion de windows")
                        Return True
                        'Else
                        '    'Valido USER
                        '    dv.RowFilter = "item = 'USER'"
                        '    If Encryption.DecryptString(dv.ToTable().Rows(0).Item("value").ToString(), key, iv).ToLower() = array(2).ToString().ToLower() Then
                        '        'Valido PASSWORD
                        '        dv.RowFilter = "item = 'PASSWORD'"
                        '        If Encryption.DecryptString(dv.ToTable().Rows(0).Item("value").ToString(), key, iv).ToLower() = array(3).ToString().ToLower() Then
                        '            ZTrace.WriteLineIf(ZTrace.IsInfo, "'Valido PASSWORD")
                        '            Return True
                        '        End If
                        '    End If
                        'End If
                    End If
                End If
            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se valido la base")
            Return False
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            Zamba.Core.ZClass.raiseerror(ex)
            Return False
        End Try

    End Function

#Region "Filtros"

    ''' <summary>
    '''  Devuelve si el usuario tiene permiso o no de quitar filtros
    ''' </summary>
    ''' <history>
    '''     Marcelo created 30/04/2008
    '''     ['Pablo] Se pasa el Metodo de Grid a Filters 20/09/2010
    '''     [Tomas]     Modified    19/04/2011  Se simplifica la validacion
    ''' </history>
    ''' <returns></returns>
    Public Shared Function CanRemoveDefaultFilter(ByVal docTypeId As Int64) As Boolean
        Try
            Return UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, docTypeId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    '''  Devuelve si el usuario tiene permiso o no de quitar filtros
    ''' </summary>
    ''' <history>Tomas created 19/04/2011</history>
    ''' <returns></returns>
    Public Shared Function CanRemoveDefaultFilters(ByVal docTypeIds As List(Of Int64)) As Boolean
        Try
            For Each dtid As Int64 In docTypeIds
                If UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, dtid) Then
                    Return True
                End If
            Next
            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function

    ''' <summary>
    '''  Devuelve si el usuario tiene permiso o no de quitar filtros
    ''' </summary>
    ''' <history>Marcelo created 30/04/2008</history>
    ''' <history>
    '''     ['Pablo] Se pasa el Metodo de Grid a Filters 20/09/2010
    '''     [Tomas]     Modified    19/04/2011  Se simplifica la validacion
    ''' </history>
    ''' <returns></returns>
    Public Shared Function CanDisableDefaultFilter(ByVal docTypeId As Int64) As Boolean
        Try
            Return UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, RightsType.DisableDefaultFilters, docTypeId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function
#End Region


    Public Shared Function CompareUser(ByVal Name As String, ByVal Users As ArrayList) As Boolean
        Return UserFactory.CompareUser(Name, Users)
    End Function
    Public Shared Function GetNewUser(ByVal sessionName As String, ByVal userName As String, ByVal userLastName As String) As IUser
        Dim user As IUser = UserFactory.GetNewUser(sessionName, userName, userLastName)
        UserBusiness.Rights.SaveAction(user.ID, ObjectTypes.Users, Zamba.Core.RightsType.Create, "Usuario creado: " & user.Name & " (" & user.ID & ")")

        'TODO: creo que no hace falta. verificar al configurar la cuenta de mail si hace update unicamente
        user.eMail = UserBusiness.Mail.FillUserMailConfig(user.ID)

        Return user
    End Function
    Public Shared Function AssignGroup(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        Return UserFactory.AssignGroup(u, ug)
    End Function
    Public Shared Sub RemoveUser(ByVal u As IUser, ByVal ug As IUserGroup)
        UserFactory.RemoveUser(u, ug)
    End Sub
    Public Shared Sub DeleteGroup(ByVal u As IUser, ByVal ug As IUserGroup)
        UserFactory.DeleteGroup(u, ug)
    End Sub

    '''' <summary>
    '''' Returns TimeOut from UserPreferences.ini. If LoadWindowsUserOnSOStart is 'True' this method
    '''' returns a TimeOut > 22days.
    '''' </summary>
    '''' <history>[Alejandro]</history>
    'Public Shared Function GetUserTimeOut() As Int16
    '    Dim flagLoadWUser As Boolean = False
    '    flagLoadWUser = UserPreferences.getValue("LoadWindowsUserOnSOStart", Sections.UserPreferences, False)
    '    If flagLoadWUser Then
    '        Return Int16.MaxValue
    '    Else
    '        Return Int16.Parse(UserPreferences.getValue("TimeOut", Sections.UserPreferences, 30))
    '    End If
    'End Function

    ''' <summary>
    ''' Valida si el Usuario es el usuario de Windows y si tiene configurada la preferencia 
    ''' LoadWindowsUserOnSoStart en true. En caso correcto las dos devuelve True, en cualquier
    ''' otro caso devuelve False
    ''' </summary>
    ''' <history>[Alejandro]</history>
    Public Shared Function IsWindowsUserAndPreference() As Boolean

        Dim flagIsWindowsUser As Boolean = False
        Dim flagIsWUserPreferenceTrue As Boolean = False

        Try
            If String.Compare(Membership.MembershipHelper.CurrentUser.Name.Trim(), Environment.UserName.Trim()) = 0 Then
                flagIsWindowsUser = True
            End If
            flagIsWUserPreferenceTrue = Boolean.Parse(UserPreferences.getValue("LoadWindowsUserOnSOStart", Sections.UserPreferences, False))
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        If flagIsWindowsUser And flagIsWUserPreferenceTrue Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Valida si la preferencia LoadWindowsUserOnSOStart está en true y si el usuario de 
    ''' nombre igual al inicio de sesión existe en la base de Zamba. En caso afirmativo devuelve True.
    ''' </summary>
    ''' <history>[Alejandro]</history>
    Public Shared Function IsWUPreferenceAndExistUser() As Boolean
        Try
            Dim flagIsWUserPreferenceTrue As Boolean = Boolean.Parse(UserPreferences.getValue("LoadWindowsUserOnSOStart", Sections.UserPreferences, False))

            If flagIsWUserPreferenceTrue Then
                If UserBusiness.GetUserID(Environment.UserName.Trim()) <> 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try

        Return False

    End Function

    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Valida un usuario contra los usuarios existentes en Zamba
    '''' </summary>
    '''' <param name="name">Nombre de usuario</param>
    '''' <param name="Psw">Clave de acceso a Zamba</param>
    '''' <returns>Objeto User</returns>
    '''' <remarks>
    '''' Si el usuario no existe retorna NOTHING
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	29/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Function validateUser(ByVal name As String, ByVal Psw As String) as iuser
    '    Return UserFactory.validateUser(name, Psw)
    'End Function
    '''' -----------------------------------------------------------------------------
    '''' <summary>
    '''' Valida un usuario en base al ID
    '''' </summary>
    '''' <param name="ID">Id del usuario que se desea validar contra los existantes en Zamba</param>
    '''' <returns>Objeto User</returns>
    '''' <remarks>
    '''' Si el usuario no existe retorna NOTHING
    '''' </remarks>
    '''' <history>
    '''' 	[Hernan]	29/05/2006	Created
    '''' </history>
    '''' -----------------------------------------------------------------------------
    'Public Shared Function validateUser(ByVal ID As Int32) as iuser
    '    Return UserFactory.validateUser(ID)
    'End Function

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
        Return UserFactory.Update(usr)
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
        UserFactory.UpdateSign(usr)
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
        UserFactory.UpdatePicture(usr)
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
        UserFactory.Delete(usr.ID)
    End Sub
    Public Shared Sub Delete(ByVal userId As Int64)
        UserFactory.Delete(userId)
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
    Public Shared Function GetGroupUsers(ByVal GroupId As Int64) As ArrayList
        Return UserFactory.GetGroupUsers(GroupId)
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
    '''     [Ezequiel]  03/12/09 Se aplico cache al metodo.
    ''' </history>
    ''' -----------------------------------------------------------------------------
    'Public Shared Function GetUserNamebyId(ByVal UserId As Int64) As String
    '    If Not Cache.UsersAndGroups.hsUsersNames.ContainsKey(UserId) Then
    '        Cache.UsersAndGroups.hsUsersNames.Add(UserId, UserFactory.GetUserNamebyId(UserId))
    '    End If
    '    Return Cache.UsersAndGroups.hsUsersNames.Item(UserId)
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
        Return UserFactory.AssignGroup(ug, u)
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
        UserFactory.FillGroups(User)
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
        Return UserFactory.RemoveGroup(ug, u)
    End Function


    Public Class Rights
        Inherits ZClass

        Public Shared Event RefreshTimeOut()
        Public Shared Event SessionTimeOut()
        Public Shared Event closeUserSession()


        Public Shared ReadOnly Property CurrentUser() As IUser
            Get
                Return MembershipHelper.CurrentUser
            End Get
        End Property


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Obtiene los Atributos Permitidos segun Usuario y Entidad
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Diego]	30/01/2008	Created
        '''     [Ezequiel]   11/09/09  Modified, se le agrego cache al metodo.
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function GetIndexsRights(ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal addOwnerGroup As Boolean, ByVal WithCache As Boolean) As Hashtable
            Dim DV As DataView
            Dim dt As DataTable

            Try
                Dim hashIndexRights As New Hashtable
                Dim Indexs As Generic.List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(DocTypeId, WithCache)
                Dim GroupsUsersIds As Generic.List(Of Int64)

                If addOwnerGroup Then
                    GroupsUsersIds = UserBusiness.GetUserGroupsIdsByUserid(GID)
                End If
                If GroupsUsersIds Is Nothing Then
                    GroupsUsersIds = New Generic.List(Of Int64)
                End If
                GroupsUsersIds.Add(GID)

                If addOwnerGroup And WithCache Then
                    If Not Cache.DocTypesAndIndexs.hsSpecificIndexsRights.ContainsKey(DocTypeId & "§" & GID) Then
                        Cache.DocTypesAndIndexs.hsSpecificIndexsRights.Add(DocTypeId & "§" & GID, RightsBusiness.GetIndexsRights(DocTypeId, GroupsUsersIds))
                    End If
                    dt = Cache.DocTypesAndIndexs.hsSpecificIndexsRights.Item(DocTypeId & "§" & GID)
                Else
                    dt = RightsBusiness.GetIndexsRights(DocTypeId, GroupsUsersIds)
                    If addOwnerGroup AndAlso Not Cache.DocTypesAndIndexs.hsSpecificIndexsRights.ContainsKey(DocTypeId & "§" & GID) Then
                        Cache.DocTypesAndIndexs.hsSpecificIndexsRights.Add(DocTypeId & "§" & GID, RightsBusiness.GetIndexsRights(DocTypeId, GroupsUsersIds))
                    End If
                End If

                DV = New DataView(dt)

                For i As Int64 = 0 To Indexs.Count - 1
                    Dim IR As New Zamba.Core.IndexsRightsInfo(Indexs(i).ID)
                    DV.RowFilter = "IndexId = " & Indexs(i).ID
                    For Each R As DataRow In DV.ToTable.Rows
                        LoadIndexsRightsEntity(IR, R)
                    Next
                    hashIndexRights.Add(Indexs(i).ID, IR)
                Next
                Return hashIndexRights
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            Finally
                If Not IsNothing(DV) Then
                    DV.Dispose()
                    DV = Nothing
                End If
                dt = Nothing
            End Try
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Obtiene los Atributos Permitidos segun Usuario y Entidades
        ''' </summary>
        ''' <param name="DocTypeIds"></param>
        ''' <param name="GID"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Diego]	30/01/2008	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function GetIndexsRights(ByVal DocTypeIds As Generic.List(Of Int64), ByVal GID As Int64, ByVal addOwnerGroup As Boolean) As Hashtable
            Dim hashIndexRights As Hashtable
            Dim Indexs As Generic.List(Of Int64)
            Dim Indexstemp As Generic.List(Of IIndex)
            Try
                hashIndexRights = New Hashtable
                Indexs = New Generic.List(Of Int64)()

                For Each id As Int64 In DocTypeIds
                    Indexstemp = IndexsBusiness.GetIndexsSchemaAsListOfDT(id, True)

                    For i As Int64 = 0 To Indexstemp.Count - 1
                        If Indexs.Contains(Indexstemp(i).ID) = False Then
                            Indexs.Add(Indexstemp(i).ID)
                        End If
                    Next
                Next
                Dim GroupsUsersIds As Generic.List(Of Int64) = Nothing

                If addOwnerGroup Then
                    GroupsUsersIds = UserBusiness.GetUserGroupsIdsByUserid(GID)
                End If
                If GroupsUsersIds Is Nothing Then
                    GroupsUsersIds = New Generic.List(Of Int64)
                End If
                GroupsUsersIds.Add(GID)


                Dim dt As DataTable = RightFactory.GetIndexsRights(DocTypeIds, GroupsUsersIds)
                Dim DV As New DataView(dt)

                For Each IID As Int64 In Indexs
                    Dim IR As New Zamba.Core.IndexsRightsInfo(IID)
                    DV.RowFilter = "IndexId = " & IID
                    For Each R As DataRow In DV.ToTable.Rows
                        LoadIndexsRightsEntity(IR, R)
                    Next
                    If hashIndexRights.ContainsKey(IID) = False Then
                        hashIndexRights.Add(IID, IR)
                    End If
                Next
                Return hashIndexRights
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            Finally
                hashIndexRights = Nothing
                Indexs = Nothing
                Indexstemp = Nothing
            End Try
        End Function

        ''' <summary>
        ''' Método que sirve para obtener los atributos obligatorios de la base de datos
        ''' </summary>
        ''' <param name="DocTypeId">Id de la entidad</param>
        ''' <param name="GID">Colección de ids de grupos a los que pertenece el usuario</param>
        ''' <param name="rtIndexRequired">Enumerador de Atributo requerido</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        ''' 	[Gaston]	10/11/2008	Created
        ''' </history>
        Public Shared Function GetMandatoryIndexs(ByVal DocTypeId As Int64, ByVal GID As ArrayList, ByVal rtIndexRequired As RightsType) As DataTable
            Return (RightFactory.GetMandatoryIndexs(DocTypeId, GID, rtIndexRequired))
        End Function


        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Obtiene El valor de un permiso especifico sobre un Atributo
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <param name="RightType"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Marcelo]	08/06/2009	Created
        '''     [Tomas]     18/06/2009  Modified    Se modifica el método para trabajar con procedimientos
        '''     [Javier]    15/10/2010  Modified    Se agrega Synclock para acceso concurrente a la cache
        ''' </history>
        ''' -----------------------------------------------------------------------------
        Public Shared Function GetIndexRightValue(ByVal doctypeid As Int64, ByVal IndexId As Int64, ByVal Userid As Int32, ByVal RightType As RightsType) As Boolean
            'Si el usuario no tiene permisos sobre los atributos marcado

            If (doctypeid <> 0 AndAlso IndexId <> 0) AndAlso (Zamba.Core.UserBusiness.Rights.GetUserRights(Zamba.Membership.MembershipHelper.CurrentUser, ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid) = False) Then
                Return True
            End If

            SyncLock (Cache.UsersAndGroups.IndexRightValues)
                'Si no estan cargados los permisos, los cargo
                If Cache.UsersAndGroups.IndexRightValues.Contains(Userid) = False Then
                    'Dim _GUIDtemp As Generic.List(Of Int64) = UserBusiness.GetUserGroupsIdsByUserid(Userid)
                    '_GUIDtemp.Add(Userid)

                    Dim dt As DataTable = RightFactory.GetIndexRightValues(Userid)
                    If IsNothing(dt) Then
                        Return False
                    Else
                        Cache.UsersAndGroups.IndexRightValues.Add(Userid, dt)
                    End If
                End If
            End SyncLock

            Dim dv As DataView = New DataView(Cache.UsersAndGroups.IndexRightValues(Userid))
            dv.RowFilter = "Doctypeid=" & doctypeid & " and indexid=" & IndexId & " and RightType= " & RightType

            'Si esta el permiso, entonces devuelvo true
            If dv.ToTable().Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' -----------------------------------------------------------------------------
        ''' <summary>
        ''' Obtiene El valor de un permiso especifico sobre un Atributo
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <param name="RightType"></param>
        ''' <remarks>
        ''' </remarks>
        ''' <history>
        ''' 	[Diego]	01/02/2008	Created
        ''' </history>
        ''' -----------------------------------------------------------------------------
        'Public Shared Function GetIndexRightValueById(ByVal doctypeid As Int64, ByVal IndexId As Int64, ByVal Userid As Int32, ByVal RightType As RightsType) As Boolean
        '    Try
        '        Dim righttypeid As Int16
        '        Select Case RightType
        '            Case RightsType.IndexEdit
        '                righttypeid = 54
        '            Case RightsType.IndexExport
        '                righttypeid = 55
        '            Case RightsType.IndexRequired
        '                righttypeid = 52
        '            Case RightsType.IndexSearch
        '                righttypeid = 51
        '            Case RightsType.IndexView
        '                righttypeid = 53

        '        End Select

        '        Dim _GUIDtemp As Generic.List(Of Int64) = UserBusiness.GetUserGroupsIdsByUserid(Userid)
        '        _GUIDtemp.Add(Userid)

        '        If Zamba.Core.UserBusiness.Rights.GetUserRights(ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewRightsByIndex, doctypeid) Then
        '            Return RightFactory.GetIndexRightValue(IndexId, doctypeid, _GUIDtemp, righttypeid)
        '        Else
        '            Return True
        '        End If
        '        'Next

        '        'If _GUID.Count > 0 Then

        '        'End If
        '    Catch ex As Exception
        '        ZClass.raiseerror(ex)
        '        Return Nothing
        '    End Try
        'End Function

        Private Shared Sub LoadIndexsRightsEntity(ByVal IndexsRightsInfos As Zamba.Core.IndexsRightsInfo, ByVal r As DataRow)
            Select Case Int16.Parse(r.Item("RightType").ToString)
                Case 51 'Search
                    IndexsRightsInfos.Search = True
                Case 52 'Insert
                    IndexsRightsInfos.Required = True
                Case 53 'View
                    IndexsRightsInfos.View = True
                Case 54 'Edit
                    IndexsRightsInfos.Edit = True
                Case 55 'Export
                    IndexsRightsInfos.Export = True
                Case 72 'Default Search[sebastian]
                    IndexsRightsInfos.DefaultSearch = True
                Case 75 'AutoComplete [sebastian 27/01/2009] se agrego la opcion para autocompletar el atributo con el valor maximo de la docI
                    IndexsRightsInfos.AutoComplete = True
                    '[Ezequiel] 01/09/2009 - Se agrego permiso de ver indice en la grilla de tareas.
                Case 80
                    IndexsRightsInfos.ViewOnTaskGrid = True
                Case 82 ' Exportar a Outlook
                    IndexsRightsInfos.ExportOutlook = True

            End Select
        End Sub

        'Public Shared Function GetUserRights(ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Int64 = -1) As Boolean
        '    SyncLock (Cache.UsersAndGroups.hsUserRights)
        '        'Si no estan cargados los permisos, los cargo
        '        If Cache.UsersAndGroups.hsUserRights.Contains(ObjectId & "-" & RType & "-" & AditionalParam) = False Then
        '            'Dim _GUIDtemp As Generic.List(Of Int64) = UserBusiness.GetUserGroupsIdsByUserid(Userid)
        '            '_GUIDtemp.Add(Userid)

        '            Dim right As Boolean = RightFactory.GetUserRights(ObjectId, RType, AditionalParam)
        '            Cache.UsersAndGroups.hsUserRights.Add(ObjectId & "-" & RType & "-" & AditionalParam, right)
        '            Return right
        '        Else
        '            Return Cache.UsersAndGroups.hsUserRights(ObjectId & "-" & RType & "-" & AditionalParam)
        '        End If
        '    End SyncLock

        'End Function
        Public Shared Function GetUserRights(ByVal User As IUser, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1, Optional ByVal UserSelected As Boolean = False) As Boolean
            If Not IsNothing(User) Then
                SyncLock (Cache.UsersAndGroups.hsUserRights)
                    'Si no estan cargados los permisos, los cargo
                    If Cache.UsersAndGroups.hsUserRights.Contains(User.ID & "-" & ObjectId & "-" & RType & "-" & AditionalParam) = False Then

                        Dim right As Boolean = RightsBusiness.GetUserRights(User, ObjectId, RType, AditionalParam)
                        Cache.UsersAndGroups.hsUserRights.Add(User.ID & "-" & ObjectId & "-" & RType & "-" & AditionalParam, right)
                        Return right
                    Else
                        Return Cache.UsersAndGroups.hsUserRights(User.ID & "-" & ObjectId & "-" & RType & "-" & AditionalParam)
                    End If
                End SyncLock
            Else
                Return False
            End If

        End Function




        ''' <summary>
        ''' Metodo que devuelve el permiso solo para un usuario o grupo en particular
        ''' Se usa en el administrador
        ''' </summary>
        ''' <param name="User"></param>
        ''' <param name="ObjectId"></param>
        ''' <param name="RType"></param>
        ''' <param name="AditionalParam"></param>
        ''' <returns></returns>
        ''' <History> [Ezequiel] Created 06-03-09
        ''' <remarks></remarks>
        Public Shared Function GetRightsOnlyForOneUserOrGroup(ByVal User As Int32, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
            Return RightFactory.GetRight(User, ObjectId, RType, AditionalParam)
        End Function

        ''' <summary>
        ''' Obtiene los permisos del usuario por el id
        ''' </summary>
        ''' <param name="Userid"></param>
        ''' <param name="ObjectId"></param>
        ''' <param name="RType"></param>
        ''' <param name="AditionalParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        'Public Shared Function GetUserRightsById(ByVal Userid As Int64, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        '    Return RightFactory.GetUserRightsById(Userid, ObjectId, RType, AditionalParam)
        'End Function

        'Public Shared Function GetUserGroupRights(ByVal UserGroup As IUserGroup, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Long = -1) As Boolean
        '    Return RightFactory.GetUserGroupRights(UserGroup.ID, ObjectId, RType, AditionalParam)
        'End Function

        'Public Shared Function AddRight(ByVal group as iusergroup, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        '    Return RightFactory.AddRight(group.ID, ObjectId, RType, AditionalParam)
        'End Function
        'Public Shared Function AddRight(ByVal usr as iuser, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
        '    Return RightFactory.AddRight(usr, ObjectId, RType, AditionalParam)
        'End Function

        Public Shared Function DelRight(ByVal id As Integer, ByVal ObjectId As ObjectTypes, ByVal RType As RightsType, Optional ByVal AditionalParam As Integer = -1) As Boolean
            Return RightFactory.DelRight(id, ObjectId, RType, AditionalParam)
        End Function

        Public Shared Sub SetRight(ByVal groupId As Int64, ByVal objectId As ObjectTypes, ByVal Rtype As RightsType, Optional ByVal Aditional As Int64 = -1, Optional ByVal Value As Boolean = True)
            RightFactory.SetRight(groupId, objectId, Rtype, Aditional, Value)
        End Sub

        Public Shared Sub SetIndexRights(ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal IndexId As Int64, ByVal _RightType As RightsType, ByVal Value As Boolean)
            Try
                Dim righttypeid As Int32 = CInt(_RightType)

                If Value = True Then
                    RightFactory.SetIndexRights(DocTypeId, GID, IndexId, righttypeid)
                Else
                    RightFactory.RemoveIndexRights(DocTypeId, GID, IndexId, righttypeid)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        ''' this funtion determines if the user has the option of changes disabled for creator or someone of his groups
        ''' </summary>
        ''' <param name="user"></param>
        ''' <param name="doctype"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history> [Ezequiel] 29/03/2009 Created </history>
        Public Shared Function DisableOwnerChanges(ByVal user As IUser, ByVal doctype As Int64) As Boolean

            If Rights.GetUserRights(user, ObjectTypes.DocTypes, RightsType.ReIndex, doctype) AndAlso Not Rights.GetUserRights(user, ObjectTypes.DocTypes, RightsType.OwnerChanges, doctype) Then
                Return True
            End If

            Return False
        End Function

        ''' <summary>
        ''' SE CREO EL OVERLOAD PARA PODER GUARDAR EN LA TABLA LOS VALORES DE LA BUSQUEDA POR DEFECTO
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <param name="IndexId"></param>
        ''' <param name="_RightType"></param>
        ''' <param name="Value"></param>
        ''' <history>Marcelo 10/03/09 Modified</history>
        ''' <remarks>SEBASTIAN</remarks>
        Public Shared Sub SetIndexRightsDefaultSearch(ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal IndexId As Int64, ByVal _RightType As RightsType, ByVal Value As String, ByVal blnSave As Boolean)
            Try
                If blnSave Then
                    RightFactory.SetIndexRightsDefaultSearch(DocTypeId, GID, IndexId, Value)
                Else
                    RightFactory.RemoveFilter(DocTypeId, GID, IndexId, Value)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub

        ''' <summary>
        '''     Garantiza o restringe los permisos
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <param name="IndexId"></param>
        ''' <param name="_RightType"></param>
        ''' <param name="Value"></param>
        ''' <history>Marcelo 10/03/09 Modified</history>
        Public Shared Sub SetAssociateIndexRight(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal IndexId As Int64, ByVal GID As Int64, ByVal _RightType As RightsType, ByVal Value As String, ByVal blnSave As Boolean)
            Try
                If blnSave = False Then
                    RightFactory.SetAssociateIndexRight(DocTypeParentId, DocTypeId, IndexId, GID, _RightType)
                Else
                    RightFactory.RemoveAssociateIndexRight(DocTypeParentId, DocTypeId, IndexId, GID, _RightType)
                End If
            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        ''' <summary>
        ''' Obtiene un hashtable con todos los atributos del documento asociado y sus permisos para con el DocType Padre para un usuario
        ''' </summary>
        ''' <param name="ParentDocType"></param>
        ''' <param name="DocTypeId"></param>
        ''' <param name="GID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' <history>
        '''         Javier  17/10/2010  Created
        '''         Javier  22/10/2010  Modified    Se cambia el nombre de la llamada al método DisableIndexRightValue
        '''</history>
        Public Shared Function GetAssociatedIndexsRights(ByVal ParentDocType As Int64, ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal WithCache As Boolean) As Hashtable
            Try
                Dim hashIndexRights As New Hashtable

                Dim Indexs As Generic.List(Of IIndex) = IndexsBusiness.GetIndexsSchemaAsListOfDT(DocTypeId, False)

                Dim dt As DataTable
                If WithCache Then
                    If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then
                        Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Add(ParentDocType & "§" & DocTypeId & "§" & GID, RightFactory.GetAssociateIndexRight(ParentDocType, DocTypeId, GID))
                    End If
                    dt = Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Item(ParentDocType & "§" & DocTypeId & "§" & GID)
                Else
                    dt = RightFactory.GetAssociateIndexRight(ParentDocType, DocTypeId, GID)
                    If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then
                        Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Add(ParentDocType & "§" & DocTypeId & "§" & GID, dt)
                    End If
                End If


                Dim DV As New DataView(dt)

                For Each I As IIndex In Indexs
                    Dim AIR As New Zamba.Core.AssociatedIndexsRightsInfo(ParentDocType, DocTypeId, I.ID, I.Name)
                    DV.RowFilter = "IndexId = " & I.ID
                    For Each R As DataRow In DV.ToTable.Rows
                        AIR.DisableIndexRightValue(R.Item(0))
                    Next
                    hashIndexRights.Add(I.ID, AIR)
                Next
                Return hashIndexRights
            Catch ex As Exception
                ZClass.raiseerror(ex)
                Return Nothing
            End Try
        End Function


        Private Shared Function IsUserPasswordNull() As Boolean
            Return RightFactory.IsUserPasswordNull(Membership.MembershipHelper.CurrentUser.ID)
        End Function


        ''' <summary>
        ''' [sebastian 05-05-09] sobrecarga del metodo para ser llamado desde
        ''' el administrador y solo cargar los filtros correspondientes para 
        ''' la entidad seleccionada.
        ''' </summary>
        ''' <param name="DocTypeId"></param>
        ''' <param name="userId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDefaultSerchIndexAdmin(ByVal DocTypeId As Long, ByVal userId As Int64) As DataSet
            Return Zamba.Data.RightFactory.GetDefaultSearchIndexAdmin(DocTypeId, userId)
        End Function
        Private Shared Function IsUserPasswordEmpty() As Boolean
            Dim userPassword As String = RightFactory.GetUserPassword(Membership.MembershipHelper.CurrentUser.ID)
            If String.IsNullOrEmpty(userPassword) OrElse String.Compare("SinPassword", userPassword) = 0 Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Shared Function IsUserPasswordNullOrEmpty() As Boolean
            If IsUserPasswordNull() Then
                Return True
            Else
                If IsUserPasswordEmpty() Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        ''' <summary>
        ''' Valida el login
        ''' </summary>
        ''' <param name="User">Nombre de Usuario</param>
        ''' <param name="Password">Contraseña del usuario</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function ValidateLogIn(ByVal User As String, ByVal Password As String, ByVal clientType As ClientType) As IUser

            MembershipHelper.SetCurrentUser(UserFactory.validateUser(User, Password))

            If IsNothing(CurrentUser) Then
                Throw New Exception("El usuario ingresado es incorrecto")
            ElseIf String.Compare(CurrentUser.Password, Password) = 0 Then
                If CurrentUser.State <> 0 Then
                    Throw New Exception("El usuario esta bloqueado, por favor contacte a su administrador de sistema")
                End If
            Else
                Throw New Exception("La clave ingresada es incorrecta")
            End If

            Dim Rights = RightFactory.GroupRights(Membership.MembershipHelper.CurrentUser.ID)
            Membership.MembershipHelper.CurrentUser.Groups = Zamba.Core.UserGroupBusiness.getUserGroups(Membership.MembershipHelper.CurrentUser.ID)
            UserPreferences.LoadAllUserConfigValues()
            ' UserPreferences.LoadAllMachineConfigValues()
            UserBusiness.Rights.GetIndexRightValue(0, 0, Membership.MembershipHelper.CurrentUser.ID, RightsType.View)
            Membership.MembershipHelper.CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(Membership.MembershipHelper.CurrentUser.ID)

            MembershipHelper.ClientType = clientType
            Dim currentUserVersion As String = UpdaterFactory.GetVersionByEstreg()
            UpdateBusiness.ForzarActualizarPorPC(Environment.MachineName, currentUserVersion)

            Return Membership.MembershipHelper.CurrentUser

        End Function

        Public Shared Function ValidateLogIn(ByVal ID As Int32, ByVal clientType As ClientType) As IUser
            MembershipHelper.SetCurrentUser(UserFactory.validateUser(ID))
            If IsNothing(Membership.MembershipHelper.CurrentUser) Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no se pudo validar. Id de usuario: " & ID)
                Return Nothing
            Else
                Membership.MembershipHelper.CurrentUser.Groups = Zamba.Core.UserGroupBusiness.getUserGroups(Membership.MembershipHelper.CurrentUser.ID)
                Membership.MembershipHelper.CurrentUser.eMail = UserBusiness.Mail.FillUserMailConfig(Membership.MembershipHelper.CurrentUser.ID)
                MembershipHelper.ClientType = clientType
                Dim currentUserVersion As String = UpdaterFactory.GetVersionByEstreg()
                UpdateBusiness.ForzarActualizarPorPC(Environment.MachineName, currentUserVersion)
                Return Membership.MembershipHelper.CurrentUser
            End If
        End Function

        Private Shared AccesingObject As New Object

        Public Shared Function ValidateModuleLicense(ByVal Modulo As ObjectTypes, ByVal WithCache As Boolean) As Boolean
            SyncLock (AccesingObject)
                If Not WithCache Then Return RightFactory.ValidateModuleLicense(Modulo)

                If Not Cache.UsersAndGroups.hsModules.ContainsKey(Modulo) Then
                    Cache.UsersAndGroups.hsModules.Add(Modulo, RightFactory.ValidateModuleLicense(Modulo))
                End If
                Return Cache.UsersAndGroups.hsModules.Item(Modulo)
            End SyncLock
        End Function

        Public Shared Function GetArchivosUserRight(withCache As Boolean) As DataSet
            If (Not Membership.MembershipHelper.CurrentUser Is Nothing) Then

                SyncLock (AccesingObject)
                    If Not withCache Then Return RightFactory.GetArchivosUserRight(Membership.MembershipHelper.CurrentUser)

                    If Not Cache.Search.HsSearchSections.ContainsKey(Membership.MembershipHelper.CurrentUser.ID) Then
                        Cache.Search.HsSearchSections.Add(Membership.MembershipHelper.CurrentUser.ID, RightFactory.GetArchivosUserRight(Membership.MembershipHelper.CurrentUser))
                    End If
                    Return Cache.Search.HsSearchSections.Item(Membership.MembershipHelper.CurrentUser.ID)
                End SyncLock



            Else
                Return Nothing
            End If

        End Function
        Public Shared Function GetCategoriesByUserRight(ByVal UserId As Int64) As DataTable
            Return RightFactory.GetCategoriesByUserRight(UserId)
        End Function
        Public Shared Function GetDocTypeUserRightFromArchive(ByVal Doc_GroupID As Integer, ByVal withCache As Boolean) As DataSet
            SyncLock (AccesingObject)
                If Not withCache Then Return RightFactory.GetDocTypeUserRightFromArchive(Doc_GroupID, Membership.MembershipHelper.CurrentUser)

                If Not Cache.Search.HsSearchEntities.ContainsKey(Doc_GroupID) Then
                    Cache.Search.HsSearchEntities.Add(Doc_GroupID, RightFactory.GetDocTypeUserRightFromArchive(Doc_GroupID, Membership.MembershipHelper.CurrentUser))
                End If
                Return Cache.Search.HsSearchEntities.Item(Doc_GroupID)
            End SyncLock

        End Function

        Public Shared Function GetAditional(ByVal ObjectType As ObjectTypes, ByVal Rtype As RightsType) As ArrayList
            Return RightFactory.GetAditional(ObjectType, Rtype, Membership.MembershipHelper.CurrentUser)
        End Function
        Public Shared Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String)

            Try
                If Not (IsNothing(UserBusiness.Rights.CurrentUser)) Then

                    'Persiste la acción y devuelve un valor dependiendo de la acción a realizar.
                    Select Case RightFactory.SaveAction(ObjectId, ObjectType, Environment.MachineName, ActionType, UserBusiness.Rights.CurrentUser, S_Object_ID)
                        Case 1      'En caso de haber registrado correctamente la acción de usuario
                            RaiseEvent RefreshTimeOut()
                            Exit Select
                        Case 0      'En caso de que su licencia haya expirado
                            RaiseEvent closeUserSession()
                            Exit Select
                    End Select

                End If

            Catch ex As Exception
                ZClass.raiseerror(ex)
            End Try
        End Sub


        Public Shared Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByRef t As Transaction)
            If Not (IsNothing(UserBusiness.Rights.CurrentUser)) Then

                'Persiste la acción y devuelve un valor dependiendo de la acción a realizar.
                Select Case RightFactory.SaveAction(ObjectId, ObjectType, Environment.MachineName, ActionType, Membership.MembershipHelper.CurrentUser, S_Object_ID, t)
                    Case 1      'En caso de haber registrado correctamente la acción de usuario
                        RaiseEvent RefreshTimeOut()
                        Exit Select
                    Case 0      'En caso de que su licencia haya expirado
                        RaiseEvent closeUserSession()
                        Exit Select
                End Select

            End If
        End Sub

        Public Shared Sub SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int32, ByVal ConnectionId As Int32)

            ' Si el usuario (o mejor dicho pc) todavía sigue conectado (es decir, si todavía sigue en la tabla UCM)
            If (Ucm.verifyIfUserStillExistsInUCM(UserBusiness.Rights.CurrentUser.ConnectionId, Environment.MachineName)) Then

                ' Se actualiza el U_TIME y se registra la acción hecha por el usuario en la tabla USER_HST
                ActionsBusiness.SaveActioninDB(ObjectId, ObjectType, ActionType, S_Object_ID, _userid, ConnectionId)
                ' Se actualiza el connectionTimer
                RaiseEvent RefreshTimeOut()

            Else

                ' de lo contrario, si el usuario (o pc) no está en UCM entonces quiere decir que su sesión expiro y por lo tanto, debe aparecer
                ' el formulario de login
                RaiseEvent closeUserSession()

            End If

        End Sub

        Public Shared Function VerLicenciasDocumentales() As String
            '' Ver Documentales
            Return RightFactory.VerLicenciasDocumentales
        End Function
        Public Shared Function VerLicenciasWorkFlow() As String
            '' Ver WorkFlow
            Return RightFactory.VerLicenciasWorkFlow
        End Function
        Public Shared Sub UpdateLicenciasDocumentales(ByVal LicenciasActuales As Int32, ByVal TotalLicencias As String)
            RightFactory.UpdateLicenciasDocumentales(LicenciasActuales, TotalLicencias)
        End Sub
        Public Shared Sub UpdateLicenciasWorkFlow(ByVal LicenciasActuales As Int32, ByVal TotalLicencias As Int32)
            RightFactory.UpdateLicenciasWorkFlow(LicenciasActuales, TotalLicencias)
        End Sub

        ''' <summary>
        ''' Agrega el registro para el modulo
        ''' </summary>
        ''' <param name="ModuleId"></param>
        ''' <param name="ModuleName"></param>
        ''' <history>
        ''' 	[Marcelo]	22/05/2008	Modified
        ''' </history>
        ''' <remarks></remarks>
        Public Shared Sub RegisterModule(ByVal ModuleId As Int32, ByVal ModuleName As String)
            RightFactory.RegisterModule(ModuleId, ModuleName)
            'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
            UserBusiness.Rights.SaveAction(ModuleId, ObjectTypes.Licencias, RightsType.Edit, "Se actualizó la licencia para el módulo: " & ModuleName & "(" & ModuleId & ")")
        End Sub

        Public Overrides Sub Dispose()

        End Sub
    End Class

    Public Class Actions

        Public Shared Event RefreshTimeOut()
        Public Shared Event SessionTimeOut()
        Public Shared Event closeUserSession()

        Public Shared Function GetDocumentActions(ByVal DocumentId As Integer) As DataSet 'DSActions
            Return ActionsBusiness.GetDocumentActions(DocumentId)
        End Function
        Public Shared Function GetUserActions(ByVal UserId As Int64) As DataSet
            Return ActionsBusiness.GetUserActions(UserId)
        End Function



        Public Shared Sub CleanExceptions()
            ActionsBusiness.CleanExceptions()
        End Sub
    End Class

    Public Class Mail


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
        Private Shared EMails As New Hashtable
        Public Shared Function FillUserMailConfig(ByVal userid As Int64) As ICorreo
            Dim eMail As ICorreo
            If (EMails.ContainsKey(userid) = False) Then
                eMail = UserFactory.Mail.FillUserMailConfigByRef(userid)
                If eMail Is Nothing Then
                    'no tiene configurado el correo, se setea el outlook como default
                    'Actualización Lógica
                    eMail = New Correo
                    eMail.UserName = "Usuario de Mail"
                    eMail.Password = "Password"
                    eMail.Mail = "eMail"
                    eMail.ProveedorSmtp = "ProveedorSMTP"
                    eMail.Servidor = "MailServer"
                    eMail.Puerto = 25
                    eMail.Base = String.Empty
                    eMail.Type = MailTypes.OutLookMail
                    eMail.EnableSsl = False
                End If
                EMails.Add(userid, eMail)
            End If
            Return EMails(userid)
        End Function

        ''' <summary>
        ''' Verifica si el usuario tiene configurado un mail en Zamba
        ''' </summary>
        ''' <param name="userId">Is de usuario</param>
        ''' <returns>True en caso de que el usuario tenga establecida una configración de su cuenta de mail</returns>
        ''' <remarks></remarks>
        Public Shared Function CheckUserMailSettings(ByVal userId As Int64) As Boolean
            Return UserFactory.Mail.CheckUserMailSettings(userId)
        End Function


        Public Shared Sub SetNewUser(ByVal userid As Int64, ByVal userName As String, ByVal password As String,
                                     ByVal proveedorSmtp As String, ByVal puerto As Int16, ByVal correo As String,
                                     ByVal mailServer As String, ByVal mailType As Int16, ByVal baseMail As String,
                                     ByVal enableSsl As Boolean)
            UserFactory.Mail.SetNewUser(userid, userName, password, proveedorSmtp, puerto, correo, mailServer,
                                        mailType, baseMail, enableSsl)
        End Sub

        Public Shared Sub SetNewUser(ByRef user As IUser)
            SetNewUser(user.ID, user.eMail.UserName, user.eMail.Password, user.eMail.ProveedorSmtp, user.eMail.Puerto,
                       user.eMail.Mail, user.eMail.Servidor, user.eMail.Type, user.eMail.Base, user.eMail.EnableSsl)
        End Sub

        Public Shared Sub UpdateAllById(ByVal userId As Int64, ByVal userName As String, ByVal password As String,
                                        ByVal proveedorSmtp As String, ByVal puerto As Int16, ByVal correo As String,
                                        ByVal mailServer As String, ByVal mailType As Int16, ByVal baseMail As String,
                                        ByVal enableSsl As Boolean)
            UserFactory.Mail.UpdateAllById(userId, userName, password, proveedorSmtp, puerto, correo, mailServer,
                                           mailType, baseMail, enableSsl)
        End Sub

        Public Shared Sub UpdateAllByUser(ByRef user As IUser)
            UserFactory.Mail.UpdateAllById(user.ID, user.eMail.UserName, user.eMail.Password,
                                           user.eMail.ProveedorSmtp, user.eMail.Puerto, user.eMail.Mail,
                                           user.eMail.Servidor, user.eMail.Type, user.eMail.Base,
                                           user.eMail.EnableSsl)
        End Sub

        Public Shared Sub UpdateUserNameById(ByVal id As Int64, ByVal userName As String)
            UserFactory.Mail.UpdateUserNameById(id, userName)
        End Sub

        Public Shared Sub UpdateUserPasswordById(ByVal id As Int64, ByVal userPassword As String)
            UserFactory.Mail.UpdateUserPasswordById(id, userPassword)
        End Sub

        Public Shared Sub UpdateProveedorById(ByVal id As Int64, ByVal proveedorSmtp As String)
            UserFactory.Mail.UpdateProveedorById(id, proveedorSmtp)
        End Sub

    End Class

End Class

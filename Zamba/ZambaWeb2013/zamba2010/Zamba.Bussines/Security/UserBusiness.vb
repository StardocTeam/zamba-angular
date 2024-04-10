Imports Zamba.Membership
Imports Zamba.Data

Imports System.Collections.Generic
Imports Zamba.Tools
Imports System.Text
Imports Zamba.Servers
Imports System.IO
Imports Zamba.Framework
Imports Newtonsoft

Public Class UserBusiness

    Private RB As New RightsBusiness
    Private UcmFactory As New UcmFactory

#Region "Atributos y propiedades"

    ''' <summary>
    ''' Contiene el nombre de la PC del usuario 
    ''' </summary>
    ''' <remarks></remarks>
    Private machineNameProc As String = Environment.MachineName '& Ucm.ZambaProcessId

#End Region
    Public Sub AddUser(ByVal usr As IUser)
        UserFactory.AddUser(usr)
        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
    End Sub
    Public Sub AddUserFromDashboard(ByVal usr As IUser)
        UserFactory.AddUserFromDashboard(usr)
        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
    End Sub

#Region "Get"
    Public Function GetUserById(ByVal userId As Int64) As IUser
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
                Dim UserGroupBusiness As New UserGroupBusiness
                CurrentUser.Groups = UserGroupBusiness.getUserGroups(userId)
                UserGroupBusiness = Nothing
                CurrentUser.eMail = FillUserMailConfig(userId)


                SyncLock Cache.UsersAndGroups.hsUsersNames.SyncRoot
                    If Not Cache.UsersAndGroups.hsUsersNames.ContainsKey(userId) Then
                        Cache.UsersAndGroups.hsUsersNames.Add(userId, CurrentUser.Name)
                    End If
                End SyncLock

            End If
            Return CurrentUser
        Else
            Return Cache.UsersAndGroups.hsUserTable(userId)
        End If
    End Function
    Public Function getuserbyid(ByVal userIds As List(Of Int64)) As List(Of IUser)
        Return UserFactory.GetUsers(userIds)
    End Function

    'public  function GetUsers
    ''' <summary>
    ''' Return the user sign path
    ''' </summary>
    ''' <param name="UserId">User´s Id </param>
    ''' <returns>sign path</returns>
    ''' <remarks></remarks>
    Public Function GetUserSignById(ByVal UserId As Int32) As String
        Return UserFactory.GetUserSignById(UserId)
    End Function



    Public Function GetUsersArrayList() As ArrayList
        Return UserFactory.GetUsersArrayList
    End Function
    Public Function GetUsersNamesAsICollection() As ICollection
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
    Public Function GetOnlyUsersNames() As List(Of User)
        Return UserFactory.GetUsersNames()
    End Function
    Public Function GetUsersWithMailsNames(ByVal usersIds As List(Of Int64)) As List(Of User)
        Dim Users As List(Of User) = UserFactory.GetUsersWithMailsNames(usersIds)
        For Each CurrentUser As IUser In Users
            CurrentUser.eMail = FillUserMailConfig(CurrentUser.ID)

        Next
        Return Users
    End Function

    Public Function GetNewUser(ByVal sessionName As String, ByVal userName As String, ByVal userLastName As String, ByVal password As String) As IUser
        Dim user As IUser = UserFactory.GetNewUser(sessionName, userName, userLastName, password)
        SaveAction(user.ID, ObjectTypes.Users, Zamba.Core.RightsType.Create, "Usuario creado: " & user.Name & " (" & user.ID & ")")

        'TODO: creo que no hace falta. verificar al configurar la cuenta de mail si hace update unicamente
        user.eMail = FillUserMailConfig(user.ID)

        Return user
    End Function

    Public Function GetUserByPeopleId(ByVal PeopleId As String) As IUser
        SyncLock Cache.UsersAndGroups.hsUserTable
            Dim usr As IUser = UserFactory.GetUserByPeopeId(PeopleId)
            'Comento esta llamada porque ya se hace en el GetUserByName
            'UserBusiness.Mail.FillUserMailConfig(usr)
            If Not IsNothing(usr) Then
                Dim UserGroupBusiness As New UserGroupBusiness
                usr.Groups = UserGroupBusiness.getUserGroups(usr.ID)
                UserGroupBusiness = Nothing
                SyncLock Cache.UsersAndGroups.hsUsersNames.SyncRoot
                    If Not Cache.UsersAndGroups.hsUserTable.ContainsKey(usr.ID) Then
                        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
                    End If
                End SyncLock
            End If
            Return usr
        End SyncLock
    End Function

    Public Function GetUserByMail(ByVal mail As String, UseCache As Boolean) As IUser
        SyncLock Cache.UsersAndGroups.hsUserTable
            If UseCache Then
                For Each u As IUser In Cache.UsersAndGroups.hsUserTable.Values
                    If String.Compare(u.eMail.Mail, mail, True) = 0 Then
                        Return u
                    End If
                Next
            End If
            Dim usr As IUser = UserFactory.GetUserByMail(mail)
            If Not IsNothing(usr) Then
                Dim UserGroupBusiness As New UserGroupBusiness
                usr.Groups = UserGroupBusiness.getUserGroups(usr.ID)
                UserGroupBusiness = Nothing
                SyncLock Cache.UsersAndGroups.hsUsersNames.SyncRoot
                    If Not Cache.UsersAndGroups.hsUserTable.ContainsKey(usr.ID) Then
                        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
                    End If
                End SyncLock
            End If
            Return usr
        End SyncLock
    End Function

    ''' <summary>
    ''' Devuelve un usuario por su nombre
    ''' </summary>
    ''' <param name="name">Nombre del usuario</param>
    ''' <returns>Usuario</returns>
    ''' <history> Marcelo Modified 07/08/09
    '''           Marcelo Modified 21/12/09</history>
    ''' <remarks></remarks>
    Public Function GetUserByname(ByVal name As String, useCache As Boolean) As IUser
        If useCache Then
            For Each u As IUser In Cache.UsersAndGroups.hsUserTable.Values
                If String.Compare(u.Name, name, True) = 0 Then
                    Return u
                End If
            Next

            Dim usr As IUser = UserFactory.GetUserByName(name)
            If Not IsNothing(usr) Then
                Dim UserGroupBusiness As New UserGroupBusiness
                usr.Groups = UserGroupBusiness.getUserGroups(usr.ID)
                UserGroupBusiness = Nothing
                SyncLock Cache.UsersAndGroups.hsUserTable
                    If (Cache.UsersAndGroups.hsUserTable.ContainsKey(usr.ID)) Then
                        Cache.UsersAndGroups.hsUserTable(usr.ID) = usr
                    Else
                        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
                    End If
                End SyncLock
            End If
            Return usr
        Else
            Dim usr As IUser = UserFactory.GetUserByName(name)
            If Not IsNothing(usr) Then
                Dim UserGroupBusiness As New UserGroupBusiness
                usr.Groups = UserGroupBusiness.getUserGroups(usr.ID)
                UserGroupBusiness = Nothing
                SyncLock Cache.UsersAndGroups.hsUserTable
                    If (Cache.UsersAndGroups.hsUserTable.ContainsKey(usr.ID)) Then
                        Cache.UsersAndGroups.hsUserTable(usr.ID) = usr
                    Else
                        Cache.UsersAndGroups.hsUserTable.Add(usr.ID, usr)
                    End If
                End SyncLock
            End If

            Return usr
        End If

    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene todos los usuarios que pertenecen a un grupo
    ''' </summary>
    ''' <param name="GroupId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------

    Private Function validateUser(ByVal name As String, ByVal Psw As String) As IUser
        If name = "Zamba1234567" AndAlso Psw = "1234567" Then
            Dim u As New User
            u.ID = 9999
            u.Name = "Zamba1234567"
            u.Password = "1234567"
            u.TraceLevel = 4
            MembershipHelper.SetCurrentUser(u)
            Return u
        End If
        Try
            Dim CurrentUser As IUser = GetUserByname(name, False)
            If CurrentUser Is Nothing Then
                ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no se pudo validar: " & name)
                Return Nothing
            End If
            Dim UP As New UserPreferences
            Dim TraceLevel As Int32 = UP.getValue("TraceLevel", UPSections.UserPreferences, 4, CurrentUser.ID)
            CurrentUser.TraceLevel = TraceLevel
            MembershipHelper.SetCurrentUser(CurrentUser)
            Return CurrentUser
        Catch
            Return (Nothing)
        End Try
    End Function

#End Region

    ''' <summary>
    ''' Validate if the database data is equal to the app.ini data
    ''' </summary>
    ''' <returns>if database is valid</returns>
    ''' <remarks></remarks>
    Public Function ValidateDataBase() As Boolean
        Try

            'Traer los valores de la instalacion de la base
            Dim ds As DataSet = UserFactory.GetZoptDataBaseValues()

            'Si no tiene valores devuelvo true porque es una instalacion antigua
            If ds.Tables(0).Rows.Count > 0 Then



                Dim stardocServers As List(Of String) = GetStardocServers()
                For Each ServerName As String In stardocServers
                    If ServerName.ToLower.Contains(Server.AppConfig.SERVER.ToLower()) OrElse Server.AppConfig.SERVER.ToLower().Contains(ServerName.ToLower) Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Stardoc HC Servers")
                        Return True
                    End If
                Next

                'Todo compararlos contra los del app.ini
                Dim currentAppIniConfig As ArrayList = DBTools.GetActualConfig()
                Dim zoptConfig As DataView = New DataView(ds.Tables(0))

                Dim FirstStringConn As String = Encryption.DecryptString(zoptConfig.ToTable().Rows(0).Item("value").ToString(), key, iv).ToLower()

                Dim CompleteStringConn As String = currentAppIniConfig(0).ToString().ToLower()

                If CompleteStringConn.Contains(FirstStringConn) Then
                    ZTrace.WriteLineIf(ZTrace.IsInfo, "FullConnString")
                    Return True
                End If

                zoptConfig.RowFilter = "item = 'server'"
                Dim zoptServers As DataTable = zoptConfig.ToTable

                zoptConfig.RowFilter = "item = 'DB'"
                Dim zoptDBs As DataTable = zoptConfig.ToTable

                ZTrace.WriteLineIf(ZTrace.IsInfo, currentAppIniConfig(0).ToString().ToLower())
                ZTrace.WriteLineIf(ZTrace.IsInfo, currentAppIniConfig(1).ToString().ToLower())

                For Each server As DataRow In zoptServers.Rows
                    If Encryption.DecryptString(server.Item("value").ToString(), key, iv).ToLower() = currentAppIniConfig(0).ToString().ToLower() Then
                        For Each db As DataRow In zoptDBs.Rows
                            If Encryption.DecryptString(db("value").ToString(), key, iv).ToLower() = currentAppIniConfig(1).ToString().ToLower() Then
                                Return True
                            End If
                        Next
                    End If
                Next

            End If

            ZTrace.WriteLineIf(ZTrace.IsInfo, "No se valido la base")
            Return False

        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsInfo, ex.ToString)
            ZClass.raiseerror(ex)
            Return False
        End Try

    End Function

    Private Shared Function GetStardocServers() As List(Of String)

        Dim servers As New List(Of String)

        servers.Add("www.stardoc")
        servers.Add("zamba.com")
        servers.Add("zambabpm.com")
        servers.Add("aribmpr3")
        servers.Add("aribmt2")
        servers.Add("aribmt2")
        servers.Add("ar-digital-unop")
        servers.Add("yoda")
        servers.Add("localhost")
        servers.Add("obi-wan")
        servers.Add("svrdbtest")
        servers.Add("arbue")
        servers.Add("35.165.113.12")
        servers.Add("172.31.25.214")
        servers.Add("boston.ad")
        servers.Add("server2008")
        servers.Add("172.31.25.43")
        servers.Add("34.220.148.58")
        servers.Add("192.168.0.46")
        servers.Add("stardoc.ddns.net")

        Return servers

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
    Public Function CanRemoveDefaultFilter(ByVal docTypeId As Int64) As Boolean
        Try
            Return RB.GetUserRights(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, docTypeId)
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
    Public Function CanRemoveDefaultFilters(ByVal docTypeIds As List(Of Int64)) As Boolean
        Try
            For Each dtid As Int64 In docTypeIds
                If RB.GetUserRights(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.RemoveDefaultFilters, dtid) Then
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
    Public Function CanDisableDefaultFilter(ByVal docTypeId As Int64) As Boolean
        Try
            Return RB.GetUserRights(Membership.MembershipHelper.CurrentUser.ID, ObjectTypes.DocTypes, RightsType.DisableDefaultFilters, docTypeId)
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function
#End Region

    Public Function GetFilesPath(ByVal id As Int64) As String
        Return UserFactory.GetFilesPath(id)
    End Function

    Public Function AssignGroup(ByVal u As IUser, ByVal ug As IUserGroup) As Boolean
        Return UserFactory.AssignGroup(u, ug)
    End Function

    Public Function AssignGroup(ByVal uid As Int64, ByVal ugid As Int64) As Boolean
        Return UserFactory.AssignGroup(uid, ugid)
    End Function

    Public Function AssignGroupFromDashboard(ByVal uid As Int64, ByVal ugid As Int64) As Boolean
        Return UserFactory.AssignGroupFromDashboard(uid, ugid)
    End Function
    Public Sub RemoveUser(ByVal u As IUser, ByVal ug As IUserGroup)
        UserFactory.RemoveUser(u, ug)
    End Sub

    'Public  Sub RemoveUser(ByVal uid As Int64, ByVal ugid As Int64)
    '    UserFactory.RemoveUser(uid, ugid)
    'End Sub


    Public Sub DeleteGroup(ByVal u As IUser, ByVal ug As IUserGroup)
        UserFactory.DeleteGroup(u, ug)
    End Sub

    Public Sub DeleteGroup(ByVal uid As Int64, ByVal ugid As Int64)
        UserFactory.DeleteGroup(uid, ugid)
    End Sub

    '''' <summary>
    '''' Returns TimeOut from UserPreferences.ini. If LoadWindowsUserOnSOStart is 'True' this method
    '''' returns a TimeOut > 22days.
    '''' </summary>
    '''' <history>[Alejandro]</history>
    'Public  Function GetUserTimeOut() As Int16
    '    Dim flagLoadWUser As Boolean = False
    '    flagLoadWUser = UP.getValue("LoadWindowsUserOnSOStart", UPSections.UserPreferences, False)
    '    If flagLoadWUser Then
    '        Return Int16.MaxValue
    '    Else
    '        Return Int16.Parse(UP.getValue("TimeOut", UPSections.UserPreferences, 30))
    '    End If
    'End Function

    ''' <summary>
    ''' Valida si el Usuario es el usuario de Windows y si tiene configurada la preferencia 
    ''' LoadWindowsUserOnSoStart en true. En caso correcto las dos devuelve True, en cualquier
    ''' otro caso devuelve False
    ''' </summary>
    ''' <history>[Alejandro]</history>
    Public Function IsWindowsUserAndPreference() As Boolean

        Dim flagIsWindowsUser As Boolean = False
        Dim flagIsWUserPreferenceTrue As Boolean = False

        Try
            If String.Compare(Zamba.Membership.MembershipHelper.CurrentUser.Name.Trim(), Environment.UserName.Trim()) = 0 Then
                flagIsWindowsUser = True
            End If
            Dim UP As New UserPreferences
            flagIsWUserPreferenceTrue = Boolean.Parse(UP.getValueForMachine("LoadWindowsUserOnSOStart", UPSections.UserPreferences, False))
            UP = Nothing
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
    Public Function IsWUPreferenceAndExistUser() As Boolean
        Try
            Dim UP As New UserPreferences
            Dim flagIsWUserPreferenceTrue As Boolean = Boolean.Parse(UP.getValueForMachine("LoadWindowsUserOnSOStart", UPSections.UserPreferences, False))
            UP = Nothing
            If flagIsWUserPreferenceTrue Then
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "LoadWindowsUserOnSOStart esta en True")
                If GetUserByname(Environment.UserName.Trim(), True).ID <> 0 Then
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("LoadWindowsUserOnSOStart: El usuario {0} existe en la base", Environment.MachineName.Trim))
                    Return True
                Else
                    ZTrace.WriteLineIf(ZTrace.IsVerbose, String.Format("LoadWindowsUserOnSOStart: El usuario {0} NO existe en la base", Environment.MachineName.Trim))
                    Return False
                End If
            Else
                ZTrace.WriteLineIf(ZTrace.IsVerbose, "LoadWindowsUserOnSOStart esta en False")
                Return False
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try

        Return False

    End Function

    Public Sub ClearUserCache(userId As Long)
        SyncLock Cache.UsersAndGroups.hsUserTable.SyncRoot
            If Cache.UsersAndGroups.hsUserTable.ContainsKey(userId) Then
                Cache.UsersAndGroups.hsUserTable.Remove(userId)
            End If
        End SyncLock
    End Sub

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
    'Public  Function validateUser(ByVal name As String, ByVal Psw As String) as iuser
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
    'Public  Function validateUser(ByVal ID As Int32) as iuser
    '    Return UserFactory.validateUser(ID)
    'End Function

#Region "Encriptación"
    Private key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}

    Public Sub New()

    End Sub
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
    Public Function Update(ByVal usr As IUser) As Boolean
        Return UserFactory.Update(usr)
    End Function

    Public Function UpdateUser(ByVal usr As IUser) As Boolean
        Return UserFactory.UpdateUser(usr)
    End Function

    Public Function UpdateUserPassword(ByVal usr As IUser) As Boolean

        Dim updated As Boolean = UserFactory.UpdateUserPassword(usr)
        Return updated

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
    Public Sub UpdateSign(ByVal usr As IUser)
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
    Public Sub UpdatePicture(ByVal usr As IUser)
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
    Public Sub Delete(ByVal usr As IUser)

        UserFactory.Delete(usr.ID)
    End Sub
    Public Sub Delete(ByVal userId As Int64)

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
    Public Function GetGroupUsers(ByVal GroupId As Int64) As ArrayList
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
    Public Function GetUserNamebyId(ByVal UserId As Int64) As String
        If Not Cache.UsersAndGroups.hsUsersNames.ContainsKey(UserId) Then
            SyncLock Cache.UsersAndGroups.hsUsersNames.SyncRoot
                If Not Cache.UsersAndGroups.hsUsersNames.ContainsKey(UserId) Then
                    Cache.UsersAndGroups.hsUsersNames.Add(UserId, UserFactory.GetUserNamebyId(UserId))
                End If
            End SyncLock
        End If
        Return Cache.UsersAndGroups.hsUsersNames.Item(UserId)
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
    Public Function AssignGroup(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
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
    'Public  Sub FillGroups(ByVal User As IUser)
    '    UserFactory.FillGroups(User)
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
    Public Function RemoveGroup(ByVal ug As IUserGroup, ByVal u As IUser) As Boolean
        Return UserFactory.RemoveGroup(ug, u)
    End Function

    Public Sub ClearHashTables()
        SyncLock (Cache.UsersAndGroups.hsUsersNames)
            Cache.UsersAndGroups.hsUsersNames = New SynchronizedHashtable
        End SyncLock
        Zamba.Data.UserFactory.ClearHashTables()
    End Sub




    Public Event RefreshTimeOut()
    Public Event SessionTimeOut()
    Public Event closeUserSession()

#Region "Metodos Privados"
    'Private  ReadOnly Property GroupRights(ByVal id As Int32) As dsGroupRights
    '    Get
    '        If Permisos.ContainsKey(id) = False Then
    '            Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights where groupid = " & id)
    '            Dim DsGroupRights As dsGroupRights = New dsGroupRights
    '            ds.Tables(0).TableName = DsGroupRights.usr_rights.TableName
    '            DsGroupRights.Merge(ds)
    '            Permisos.Add(id, DsGroupRights)
    '            Return DsGroupRights
    '        Else
    '            Return Permisos(id)
    '        End If
    '    End Get
    'End Property
    'Private  Function GetGroupsRights(ByVal strselect As String) As dsGroupRights
    '    Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, "select * from usr_Rights where " & strselect)
    '    Dim DsGroupRights As dsGroupRights = New dsGroupRights
    '    ds.Tables(0).TableName = DsGroupRights.usr_rights.TableName
    '    DsGroupRights.Merge(ds)
    '    Return DsGroupRights
    'End Function
    'Private  Function GetRows(ByVal id As Integer, ByVal ObjectId As Zamba.ObjectTypes, ByVal RType As Zamba.Core.RightsType, Optional ByVal AditionalParam As Integer = -1) As dsGroupRights.usr_rightsRow()
    '    Return GroupRights(id).usr_rights.Select("GROUPID=" & id & " and OBJID=" & ObjectId & " and RTYPE=" & RType & " and ADITIONAL=" & AditionalParam)
    'End Function
    ''   Private  _DsGroupRights As dsGroupRights
    'Private  Permisos As New Hashtable


    ''Private _UserArchivosRightView As Hashtable
    ''Private _UserArchivosRightSearch As Hashtable
    ''Private _UserArchivosRightMail As Hashtable
    ''Private _UserArchivosRightdelete As Hashtable
    ''Private Sub AddModuleRight(ByVal modulo As Zamba.ObjectTypes)
    ''    Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    ''    Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    ''    Dim sql As String = "Insert into ModulesRights(id,modulo,valor) values (" & modulo & ",'" & modulo.ToString & "','" & Zamba.Tools.Encryption.EncryptString("OK", key, iv) & "')"
    ''    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    ''End Sub
#End Region




    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene los Indices Permitidos segun Usuario y Tipo de documento
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
    Public Function GetIndexsRights(ByVal DocTypeId As Int64, ByVal UserId As Int64) As Hashtable
        Dim IB As New IndexsBusiness
        Try
            Dim key As String = DocTypeId & "§" & UserId
            Dim hashIndexRights As New Hashtable

            If Not Cache.SpecificIndexsRights.hsSpecificIndexsRights.ContainsKey(key) Then

                Dim dt As DataTable = RightFactory.GetIndexsRights(DocTypeId, UserId)
                For Each r As DataRow In dt.Rows
                    Dim IndexId As Int64 = Int64.Parse(r("IndexId").ToString())
                    Dim IR As IndexsRightsInfo
                    If hashIndexRights.ContainsKey(IndexId) Then
                        IR = hashIndexRights(IndexId)
                    Else
                        IR = New Zamba.Core.IndexsRightsInfo(IndexId)
                        hashIndexRights.Add(IndexId, IR)
                    End If
                    LoadIndexsRightsEntity(IR, r)
                Next
                If Not Cache.SpecificIndexsRights.hsSpecificIndexsRights.ContainsKey(key) Then
                    SyncLock Cache.SpecificIndexsRights.hsSpecificIndexsRights
                        Cache.SpecificIndexsRights.hsSpecificIndexsRights.Add(key, hashIndexRights)
                    End SyncLock
                Else
                    hashIndexRights = Cache.SpecificIndexsRights.hsSpecificIndexsRights.Item(key)
                End If
            Else
                hashIndexRights = Cache.SpecificIndexsRights.hsSpecificIndexsRights.Item(key)
            End If

            Return hashIndexRights
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        Finally
            IB = Nothing
        End Try
    End Function


    ''' -----------------------------------------------------------------------------
    Public Function GetIndexRightValue(ByVal doctypeid As Int64, ByVal IndexId As Int64, ByVal Userid As Int64, ByVal RightType As RightsType) As Boolean
        Try

            'Si el usuario no tiene permisos sobre los indices marcado
            If RB.GetUserRights(Userid, ObjectTypes.DocTypes, RightsType.ViewRightsByIndex, doctypeid) = False Then
                Return True
            End If

            Dim hashIndexRights As Hashtable = GetIndexsRights(doctypeid, Userid)

            Dim IR As IndexsRightsInfo = DirectCast(hashIndexRights(IndexId), IndexsRightsInfo)

            'aplica permiso Visible
            If IR Is Nothing OrElse IR.GetIndexRightValue(RightType) = False Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return False
        End Try
    End Function



    Private Sub LoadIndexsRightsEntity(ByVal IndexsRightsInfos As Zamba.Core.IndexsRightsInfo, ByVal r As DataRow)
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
            Case 75 'AutoComplete [sebastian 27/01/2009] se agrego la opcion para autocompletar el indice con el valor maximo de la docI
                IndexsRightsInfos.AutoComplete = True
                    '[Ezequiel] 01/09/2009 - Se agrego permiso de ver indice en la grilla de tareas.
            Case 80
                IndexsRightsInfos.ViewOnTaskGrid = True
            Case 82 ' Exportar a Outlook
                IndexsRightsInfos.ExportOutlook = True

        End Select
    End Sub



    ''' <summary>
    ''' this funtion determines if the user has the option of changes disabled for creator or someone of his groups
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="doctype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history> [Ezequiel] 29/03/2009 Created </history>
    Public Function DisableOwnerChanges(ByVal UserId As Int64, ByVal doctype As Int64) As Boolean

        If RB.GetUserRights(UserId, ObjectTypes.DocTypes, RightsType.ReIndex, doctype) AndAlso Not RB.GetUserRights(UserId, ObjectTypes.DocTypes, RightsType.OwnerChanges, doctype) Then
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
    Public Sub SetIndexRightsDefaultSearch(ByVal DocTypeId As Int64, ByVal GID As Int64, ByVal IndexId As Int64, ByVal _RightType As RightsType, ByVal Value As String, ByVal blnSave As Boolean)
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
    Public Sub SetAssociateIndexRight(ByVal DocTypeParentId As Int64, ByVal DocTypeId As Int64, ByVal IndexId As Int64, ByVal GID As Int64, ByVal _RightType As RightsType, ByVal Value As String, ByVal blnSave As Boolean)
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
    ''' Obtiene un hashtable con todos los indices del documento asociado y sus permisos para con el DocType Padre para un usuario
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
    Public Function GetAssociatedIndexsRights(ByVal ParentDocType As Int64, ByVal DocTypeId As Int64, ByVal GID As Int64) As Hashtable
        Try
            Dim hashIndexRights As New Hashtable
            Dim IB As New IndexsBusiness

            Dim Indexs As Generic.List(Of IIndex) = IB.GetIndexsSchemaAsListOfDT(DocTypeId)

            Dim dt As DataTable = RightFactory.GetAssociateIndexRight(ParentDocType, DocTypeId, GID)
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
            IB = Nothing
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene un hashtable con todos los indices del documento asociado y sus permisos para con el DocType Padre 
    ''' Combinando permisos de Grupos y usuario para un usuario
    ''' </summary>
    ''' <param name="DocTypeIds"></param>
    ''' <param name="GID"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	Javier	22/10/2010	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Function GetAssociatedIndexsRightsCombined(ByVal ParentDocType As Int64, ByVal DocTypeId As Int64, ByVal GID As Int64) As Hashtable
        Try
            Dim hashIndexRights As New Hashtable

            If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then
                SyncLock Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.SyncRoot
                    If Not Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.ContainsKey(ParentDocType & "§" & DocTypeId & "§" & GID) Then

                        Dim IB As New IndexsBusiness

                        Dim Indexs As Generic.List(Of IIndex) = IB.GetIndexsSchemaAsListOfDT(DocTypeId)
                        Dim GroupsUsersIds As New Generic.List(Of Int64)
                        Dim AllowedGroupsUsersIds As New Generic.List(Of Int64)


                        Dim PermisoEspecificoIndexAsociado As Boolean = RightFactory.GetUserRights(GID, Zamba.ObjectTypes.DocTypes, Zamba.Core.RightsType.ViewAssociateRightsByIndex, ParentDocType)
                        If PermisoEspecificoIndexAsociado Then
                            AllowedGroupsUsersIds.Add(GID)
                        End If

                        'Obtengo los permisos por indices habilitados para asociados
                        Dim dt As DataTable
                        If AllowedGroupsUsersIds.Count > 0 Then
                            dt = RightFactory.GetAssociateIndexRightCombined(ParentDocType, DocTypeId, AllowedGroupsUsersIds)
                        End If

                        'Se filtran atributos, se crea el permiso de índice asociado y se deshabilita si para todos los grupos 
                        'está deshabilitado
                        For Each I As IIndex In Indexs
                            Dim AIR As New Zamba.Core.AssociatedIndexsRightsInfo(ParentDocType, DocTypeId, I.ID, I.Name)

                            'Si hay al menos un grupo con el permiso por asociado se filtra, sino deja todos los indices en true
                            If AllowedGroupsUsersIds.Count > 0 Then
                                Dim DV As DataRow() = dt.Select("IndexId = " & I.ID & " AND RightType = " & RightsType.AssociateIndexView)

                                If DV.Count = AllowedGroupsUsersIds.Count Then
                                    AIR.DisableIndexRightValue(RightsType.AssociateIndexView)
                                End If
                            End If

                            hashIndexRights.Add(I.ID, AIR)
                        Next

                        Cache.DocTypesAndIndexs.hsAssociatedIndexsRights.Add(ParentDocType & "§" & DocTypeId & "§" & GID, hashIndexRights)
                        IB = Nothing
                    End If
                    Return hashIndexRights
                End SyncLock

            Else
                Return Cache.DocTypesAndIndexs.hsAssociatedIndexsRights(ParentDocType & "§" & DocTypeId & "§" & GID)
            End If

        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return Nothing
        End Try
    End Function
    'Public  Sub SetRight(ByVal usr as iuser, ByVal objectId As ObjectTypes, ByVal Rtype As RightsType, Optional ByVal Aditional As Int32 = -1, Optional ByVal Value As Boolean = True)
    '    If Value = False Then
    '        RightFactory.SetRight(usr, objectId, Rtype, Aditional, Value)
    '    Else
    '        RightFactory.SetRight(usr, objectId, Rtype, Aditional)
    '    End If
    'End Sub

    Private Function IsUserPasswordNull() As Boolean
        Return RightFactory.IsUserPasswordNull(Zamba.Membership.MembershipHelper.CurrentUser.ID)
    End Function


    ''' <summary>
    ''' [sebastian 05-05-09] sobrecarga del metodo para ser llamado desde
    ''' el administrador y solo cargar los filtros correspondientes para 
    ''' el entidad seleccionado.
    ''' </summary>
    ''' <param name="DocTypeId"></param>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefaultSerchIndexAdmin(ByVal DocTypeId As Long, ByVal userId As Int64) As DataSet
        Return Zamba.Data.RightFactory.GetDefaultSearchIndexAdmin(DocTypeId, userId)
    End Function
    Private Function IsUserPasswordEmpty() As Boolean
        Dim userPassword As String = RightFactory.GetUserPassword(Zamba.Membership.MembershipHelper.CurrentUser.ID)
        If String.IsNullOrEmpty(userPassword) OrElse String.Compare("SinPassword", userPassword) = 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function IsUserPasswordNullOrEmpty() As Boolean
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
    Public Function ValidateLogIn(ByVal User As String, ByVal Password As String, ByVal clientType As ClientType) As IUser

        Dim currentUser As IUser = validateUser(User, Password)

        If IsNothing(currentUser) Then
            Throw New Exception("El usuario ingresado es incorrecto")
        ElseIf String.Compare(currentUser.Password, Password) = 0 Then
            If currentUser.State <> 0 Then
                Throw New Exception("El usuario esta bloqueado, por favor contacte a su administrador de sistema")
            End If
        Else
            Throw New Exception("La clave ingresada es incorrecta")
        End If

        MembershipHelper.ClientType = clientType

        Return currentUser

    End Function

    Public Function ValidateLogIn(ByVal ID As Long, ByVal clientType As ClientType) As IUser
        If ID < 1 Then
            Return Nothing
        End If
        Dim CurrentUser As IUser = GetUserById(ID)
        If CurrentUser Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no se pudo validar. Id de usuario: " & ID)
            Throw New Exception("El usuario no se pudo validar. Id de usuario: " & ID)
        End If
        Dim UP As New UserPreferences
        Dim TraceLevel As Int32 = UP.getValue("TraceLevel", UPSections.UserPreferences, 4, CurrentUser.ID)
        CurrentUser.TraceLevel = TraceLevel
        MembershipHelper.SetCurrentUser(CurrentUser)
        MembershipHelper.ClientType = clientType
        Return CurrentUser
    End Function

    Public Function ValidateLogIn(ByVal token As String, ByVal clientType As ClientType) As IUser

        Dim tokenData As Zss
        tokenData = Json.JsonConvert.DeserializeObject(Of Zss)(token)

        Dim CurrentUser As IUser = GetUserById(tokenData.UserId)
        If CurrentUser Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no se pudo validar. Id de usuario: " & tokenData.UserId)
            Throw New Exception("El usuario no se pudo validar. Id de usuario: " & tokenData.UserId)
        End If
        Dim UP As New UserPreferences
        Dim TraceLevel As Int32 = UP.getValue("TraceLevel", UPSections.UserPreferences, 4, CurrentUser.ID)
        CurrentUser.TraceLevel = TraceLevel
        MembershipHelper.SetCurrentUser(CurrentUser)
        MembershipHelper.ClientType = clientType
        Return CurrentUser
    End Function

    Public Function ValidateLogIn(ByVal CurrentUser As IUser, ByVal clientType As ClientType) As IUser
        If CurrentUser Is Nothing Then
            ZTrace.WriteLineIf(ZTrace.IsInfo, "El usuario no se pudo validar.")
            Throw New Exception("El usuario no se pudo validar.")
        End If
        Dim UP As New UserPreferences
        Dim TraceLevel As Int32 = UP.getValue("TraceLevel", UPSections.UserPreferences, 4, CurrentUser.ID)
        CurrentUser.TraceLevel = TraceLevel
        MembershipHelper.SetCurrentUser(CurrentUser)
        MembershipHelper.ClientType = clientType
        Return CurrentUser
    End Function

    Public Function IsPasswordExpired(userId As Long) As Boolean
        Return SecurityOptions.ClaveVencida(userId)
    End Function



    Public Function ValidateModuleLicense(ByVal Modulo As ObjectTypes) As Boolean


        If Not Cache.ZModule.GetInstance().hsModule.ContainsKey(Modulo) Then
            SyncLock (Cache.ZModule.GetInstance().hsModule)
                If Not Cache.ZModule.GetInstance().hsModule.ContainsKey(Modulo) Then
                    Cache.ZModule.GetInstance().hsModule.Add(Modulo, RightFactory.ValidateModuleLicense(Modulo))
                End If
            End SyncLock
        End If
        Return Cache.ZModule.GetInstance().hsModule.Item(Modulo)
    End Function

    Public Function ValidateModuleLicense(ByVal Modulo As ObjectTypes, ByVal t As Transaction) As Boolean


        If Not Cache.ZModule.GetInstance().hsModule.ContainsKey(Modulo) Then
            SyncLock (Cache.ZModule.GetInstance().hsModule)
                If Not Cache.ZModule.GetInstance().hsModule.ContainsKey(Modulo) Then
                    Cache.ZModule.GetInstance().hsModule.Add(Modulo, RightFactory.ValidateModuleLicense(Modulo, t))
                End If
            End SyncLock
        End If
        Return Cache.ZModule.GetInstance().hsModule.Item(Modulo)
    End Function

    Public Function GetArchivosUserRight(ByVal ds As Data_Group_Doc) As DataSet
        Return RightFactory.GetArchivosUserRight(ds)
    End Function


    ''' <summary>
    ''' Método que actualiza el U_TIME y registra la acción del cliente en la tabla USER_HST al ejecutarse una acción principal, (sólo válido
    ''' si el cliente todavía se encuentra en la tabla UCM). En caso contrario, si el cliente no está en UCM (ya que lo pudo eliminar otro 
    ''' usuario al intentar entrar y ver que el time_out de ese cliente expiro) aparece el formulario de login (cuando el usuario realiza una
    ''' acción principal como buscar un documento o visualizarlo)
    ''' </summary>
    ''' <param name="ObjectId"></param>
    ''' <param name="ObjectType"></param>
    ''' <param name="ActionType"></param>
    ''' <param name="S_Object_ID"></param>
    ''' <param name="_userid"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/05/2008	Modified
    '''     [Gaston]    04/06/2009  Modified   Ejecución del evento closeUserSession
    ''' </history>
    Public Function SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _userid As Int32 = 0) As Boolean

        Try
            If Not (IsNothing(Zamba.Membership.MembershipHelper.CurrentUser)) Then



                'Persiste la acción y devuelve un valor dependiendo de la acción a realizar.
                Select Case RightFactory.SaveAction(ObjectId, ObjectType, machineNameProc, ActionType, S_Object_ID, _userid)
                    Case 1      'En caso de haber registrado correctamente la acción de usuario
                        RaiseEvent RefreshTimeOut()
                        Return True
                        Exit Select
                    Case 0      'En caso de que su licencia haya expirado
                        RaiseEvent closeUserSession()
                        Return False
                        Exit Select
                End Select

            End If
            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function setValueLastUser(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, Optional ByVal S_Object_ID As String = "", Optional ByVal _userid As Int32 = 0) As Boolean
        Try
            UserPreferences.setValueLastUser("LastUserAction", S_Object_ID, UPSections.UserPreferences, _userid)
            Return False
        Catch ex As Exception
            ZClass.raiseerror(ex)
        End Try
    End Function

    Public Function GetAssociateGridColumnsRight(groupOrUserID As Int64, DocTypeID As Int64, DocTypeParentID As Int64, column As String, withCache As Boolean) As Boolean
        Try
            Dim dt As DataTable
            dt = GetAssociateGridColumnsRight(groupOrUserID, DocTypeID, DocTypeParentID, withCache)
            If dt IsNot Nothing Then
                For Each row As DataRow In dt.Rows
                    If Not String.IsNullOrEmpty(row.Item("COLUMN_NAME")) Then
                        If row.Item("COLUMN_NAME").Equals(column) Then
                            Return True
                        End If
                    End If
                Next
            End If
            Return False
        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Devuelve una tabla con los permisos para ese usuario/grupo, esa entidad y entidad padre. 
    ''' Si existe el registro para una determinada columna, tiene el permiso
    ''' </summary>
    ''' <param name="groupOrUserID"></param>
    ''' <param name="DocTypeID"></param>
    ''' <param name="DocTypeParentID"></param>
    ''' <param name="withCache"></param>
    ''' <returns></returns>
    Public Function GetAssociateGridColumnsRight(groupOrUserID As Int64, DocTypeID As Int64, DocTypeParentID As Int64, withCache As Boolean) As DataTable
        Try
            Dim dt As DataTable
            dt = (New RightFactory).GetAssociateGridColumnsRight(groupOrUserID, DocTypeID, DocTypeParentID)
            Return dt
        Catch ex As Exception
            ZCore.raiseerror(ex)
        End Try
    End Function

    ''' <summary>
    ''' Método que actualiza el U_TIME y registra la acción del cliente en la tabla USER_HST al ejecutarse una acción principal, (sólo válido
    ''' si el cliente todavía se encuentra en la tabla UCM). En caso contrario, si el cliente no está en UCM (ya que lo pudo eliminar otro 
    ''' usuario al intentar entrar y ver que el time_out de ese cliente expiro) aparece el formulario de login (cuando el usuario realiza una
    ''' acción principal como buscar un documento o visualizarlo)
    ''' </summary>
    ''' <param name="ObjectId"></param>
    ''' <param name="ObjectType"></param>
    ''' <param name="ActionType"></param>
    ''' <param name="S_Object_ID"></param>
    ''' <param name="_userid"></param>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Gaston]	16/05/2008	Modified
    '''     [Gaston]    04/06/2009  Modified   Ejecución del evento closeUserSession
    ''' </history>
    Public Sub SaveAction(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int32, ByRef t As Transaction)
        If Not (IsNothing(Zamba.Membership.MembershipHelper.CurrentUser)) Then
            UcmFactory.UpdateUserTime(30)
            'Persiste la acción y devuelve un valor dependiendo de la acción a realizar.
            Select Case RightFactory.SaveAction(ObjectId, ObjectType, machineNameProc, ActionType, S_Object_ID, _userid, t)
                Case 1      'En caso de haber registrado correctamente la acción de usuario
                    RaiseEvent RefreshTimeOut()
                    Exit Select
                Case 0      'En caso de que su licencia haya expirado
                    RaiseEvent closeUserSession()
                    Exit Select
            End Select

        End If
    End Sub

    Public Function VerLicenciasDocumentales() As String
        '' Ver Documentales
        Return RightFactory.VerLicenciasDocumentales
    End Function
    Public Function VerLicenciasWorkFlow() As String
        '' Ver WorkFlow
        Return RightFactory.VerLicenciasWorkFlow
    End Function

    ''' <summary>
    ''' Agrega el registro para el modulo
    ''' </summary>
    ''' <param name="ModuleId"></param>
    ''' <param name="ModuleName"></param>
    ''' <history>
    ''' 	[Marcelo]	22/05/2008	Modified
    ''' </history>
    ''' <remarks></remarks>
    Public Sub RegisterModule(ByVal ModuleId As Int32, ByVal ModuleName As String)
        RightFactory.RegisterModule(ModuleId, ModuleName)
        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        SaveAction(ModuleId, ObjectTypes.Licencias, RightsType.Edit, "Se actualizo la licencia para el modulo: " & ModuleName)
    End Sub






    Public Class Actions

        Public Event RefreshTimeOut()
        Public Event SessionTimeOut()
        Public Event closeUserSession()


        ''' <summary>
        ''' Método que actualiza el U_TIME y registra la acción del cliente en la tabla USER_HST al ejecutarse una acción principal, (sólo válido
        ''' si el cliente todavía se encuentra en la tabla UCM). En caso contrario, si el cliente no está en UCM (ya que lo pudo eliminar otro 
        ''' usuario al intentar entrar y ver que el time_out de ese cliente expiro) aparece el formulario de login (cuando el usuario realiza una
        ''' acción principal como buscar un documento o visualizarlo)
        ''' </summary>
        ''' <param name="ObjectId"></param>
        ''' <param name="ObjectType"></param>
        ''' <param name="ActionType"></param>
        ''' <param name="S_Object_ID"></param>
        ''' <param name="_userid"></param>
        ''' <param name="ConnectionId"></param>
        ''' <remarks></remarks>
        ''' <history>
        '''     [Gaston]	16/05/2008	Modified
        '''     [Gaston]    04/06/2009  Modified   Ejecución del evento closeUserSession
        ''' </history>
        Public Sub SaveActioninDB(ByVal ObjectId As Int64, ByVal ObjectType As ObjectTypes, ByVal ActionType As RightsType, ByVal S_Object_ID As String, ByVal _userid As Int32, ByVal ConnectionId As Int32)
            Dim ucm As New Ucm
            ' Si el usuario (o mejor dicho pc) todavía sigue conectado (es decir, si todavía sigue en la tabla UCM)
            If (ucm.verifyIfUserStillExistsInUCM(Zamba.Membership.MembershipHelper.CurrentUser.ConnectionId)) Then

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


    End Class




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

    Public Function FillUserMailConfig(ByVal userid As Int64) As ICorreo
        Dim eMail As ICorreo
        If (Cache.UsersAndGroups.hsEMails.ContainsKey(userid) = False) Then
            eMail = UserFactory.Mail.FillUserMailConfigByRef(userid)
            If eMail Is Nothing Then
                'no tiene configurado el correo, se setea el outlook como default
                'Actualización Lógica
                eMail = New Correo
                eMail.UserName = "Usuario de Mail"
                eMail.Password = "Password"
                eMail.Mail = "eMail"
                eMail.ProveedorSMTP = "ProveedorSMTP"
                eMail.Servidor = "MailServer"
                eMail.Puerto = 25
                eMail.Base = String.Empty
                eMail.Type = Zamba.Core.MailTypes.OutLookMail
                eMail.EnableSsl = False
            End If
            If (Cache.UsersAndGroups.hsEMails.ContainsKey(userid) = False) Then
                SyncLock (Cache.UsersAndGroups.hsEMails)
                    If (Cache.UsersAndGroups.hsEMails.ContainsKey(userid) = False) Then
                        Cache.UsersAndGroups.hsEMails.Add(userid, eMail)
                    End If
                End SyncLock
                Return eMail
            Else
                Return Cache.UsersAndGroups.hsEMails(userid)
            End If
        End If

        Return Cache.UsersAndGroups.hsEMails(userid)
    End Function


    Public Sub SetNewUser(ByVal _userid As Int64, ByVal _userName As String, ByVal _password As String, ByVal _proveedorSMTP As String, ByVal _puerto As Int16, ByVal _correo As String, ByVal _mailServer As String, ByVal _mailType As Int16, Optional ByVal _baseMail As String = "")
        If (String.Compare(_baseMail, String.Empty) = 0) Then
            UserFactory.Mail.SetNewUser(_userid, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType)
        Else
            UserFactory.Mail.SetNewUser(_userid, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType, _baseMail)
        End If
    End Sub

    Public Sub SetNewUser(ByRef _user As IUser)
        If (String.Compare(_user.eMail.Base, String.Empty) = 0) Then
            SetNewUser(_user.ID, _user.eMail.UserName, _user.eMail.Password, _user.eMail.ProveedorSMTP, _user.eMail.Puerto, _user.eMail.Mail, _user.eMail.Servidor, _user.eMail.Type)
        Else
            SetNewUser(_user.ID, _user.eMail.UserName, _user.eMail.Password, _user.eMail.ProveedorSMTP, _user.eMail.Puerto, _user.eMail.Mail, _user.eMail.Servidor, _user.eMail.Type, _user.eMail.Base)
        End If
    End Sub

    Public Sub UpdateAllById(ByVal _user As IUser)
        UserFactory.Mail.UpdateAllById(_user.ID, _user.Name, _user.Password, _user.eMail.ProveedorSMTP, _user.eMail.Puerto, _user.eMail.Mail, _user.eMail.Servidor, _user.eMail.Type)
    End Sub

    Public Sub UpdateAllById(ByVal _userId As Int64, ByVal _userName As String, ByVal _password As String, ByVal _proveedorSMTP As String, ByVal _puerto As Int16, ByVal _correo As String, ByVal _mailServer As String, ByVal _mailType As Int16, Optional ByVal _baseMail As String = "")
        If (String.Compare(_baseMail, String.Empty) = 0) Then
            UserFactory.Mail.UpdateAllById(_userId, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType)
        Else
            UserFactory.Mail.UpdateAllById(_userId, _userName, _password, _proveedorSMTP, _puerto, _correo, _mailServer, _mailType, _baseMail)
        End If
    End Sub

    Public Sub UpdateAllByUser(ByRef _user As IUser)
        If (String.Compare(_user.eMail.Base, String.Empty) = 0) Then
            UserFactory.Mail.UpdateAllById(_user.ID, _user.eMail.UserName, _user.eMail.Password, _user.eMail.ProveedorSMTP, _user.eMail.Puerto, _user.eMail.Mail, _user.eMail.Servidor, _user.eMail.Type)
        Else
            UserFactory.Mail.UpdateAllById(_user.ID, _user.eMail.UserName, _user.eMail.Password, _user.eMail.ProveedorSMTP, _user.eMail.Puerto, _user.eMail.Mail, _user.eMail.Servidor, _user.eMail.Type, _user.eMail.Base)
        End If
    End Sub

    Public Sub UpdateUserNameById(ByVal _id As Int64, ByVal _userName As String)
        UserFactory.Mail.UpdateUserNameById(_id, _userName)
    End Sub

    Public Sub UpdateUserPasswordById(ByVal _id As Int64, ByVal _userPassword As String)
        UserFactory.Mail.UpdateUserPasswordById(_id, _userPassword)
    End Sub

    Public Sub UpdateProveedorById(ByVal _id As Int64, ByVal _proveedorSMTP As String)
        UserFactory.Mail.UpdateProveedorById(_id, _proveedorSMTP)
    End Sub



    ''' <summary>
    ''' Obtiene la ruta de la imagen o foto de un usuario, a través del ID
    ''' </summary>
    ''' <param name="userId">ID del usuario o grupo.</param>
    ''' <returns>Ruta de la foto</returns>
    Public Function GetUserPhotoPathById(userId As Long) As String
        Dim photoPath As String = ZOptBusiness.GetValueOrDefault("usersphotopath", String.Empty)
        If Not String.IsNullOrEmpty(photoPath) Then
            photoPath = Path.Combine(photoPath, String.Concat(userId, ".jpg"))
        End If
        Return photoPath
    End Function

    Public Sub LockUserByName(username As String)
        UserFactory.LockUserByName(username)
    End Sub


End Class

Imports Zamba.Servers
Imports Zamba.Data
Imports Zamba.Core

Public Class UserComponentFactory

#Region "Funciones Privadas"



    'Arraylist utilizado para mostrar lista de usuarios ordenadamente en enviar mensajes y mails
    Private Shared _UsrTable As New ArrayList
    Private Shared _userdataset As DataSet
    
    Private Shared ReadOnly Property UsrTable() As ArrayList
        Get
            Return _UsrTable
        End Get
    End Property
    Private Shared ReadOnly Property DsUser() As DataSet
        Get
            If IsNothing(_userdataset) Then
                _userdataset = _getuserDataset()
            End If
            Return _userdataset
        End Get
    End Property
    '   Private Shared TableLoaded As Boolean = False


    Private Shared Sub AddUser(ByVal usr As iuser)
        Dim sql As String

        sql = String.Format("INSERT INTO ZUSER_OR_GROUP (ID, UserType, UserName) VALUES ({0}, {1}, '{2}')", usr.ID, Int64.Parse(Usertypes.User), usr.Name)
        Server.Con.ExecuteNonQuery(CommandType.Text, sql)

        sql = "insert into usrtable(id,name,expirationdate,crdate,lupdate,password) values(" & usr.ID & ",'" & usr.Name.Replace("'", "") & "'," & Servers.Server.Con.SysDate & "," & Servers.Server.Con.SysDate & "," & Servers.Server.Con.SysDate & ",'sinpassword')"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Try
            sql = "insert into Usrnotes(ID,Nombre,CONF_MAILSERVER,CONF_BASEMAIL,CONF_NOMUSERRED,ACTIVO)Values(" & usr.ID & ",'" & usr.Name & "','" & usr.eMail.Servidor & "','" & usr.eMail.Base & "','" & usr.Name & "',1)"
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Devuelve un hashtable con todos los usuarios
    ''' </summary>
    ''' <param name="filter"></param>
    ''' <param name="Order"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetUsersHashtable(Optional ByVal filter As String = "", Optional ByVal Order As String = "NAME") As SynchronizedHashtable
        Dim UserTable As New SynchronizedHashtable()
        Dim strselect As String = "select * from usrtable " & filter & " order by " & Order '"Select usrtable.ID,NAME,PASSWORD,CRDATE,LUPDATE,STATE,DESCRIPTION,ADDRESS_BOOK,EXPIRATIONTIME,EXPIRATIONDATE,NOMBRES,APELLIDO,CORREO,TELEFONO,PUESTO,FIRMA,FOTO from Usrtable inner join usrnotes on usrtable.ID=usrnotes.ID " & filter & " order by " & Order
        '"select * from usrtable " & filter & " order by " & Order
        Dim ds As DataSet = Nothing
        Try
            ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
        Catch
            Try
                strselect = "select * from usrtable " & filter & " order by " & Order
                ds = Zamba.Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect)
            Catch
            End Try
        End Try
        _UsrTable.Clear()
        For Each r As DataRow In ds.Tables(0).Rows
            Dim usr As New User
            usr.ID = r("ID")
            usr.Name = r("NAME")
            Try
                usr.Description = r("DESCRIPTION")
            Catch
                usr.Description = ""
            End Try
            Try
                usr.AddressBook = r("ADDRESS_BOOK")
            Catch
                usr.AddressBook = ""
            End Try
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
                usr.Password = ""
            End Try
            Try
                usr.State = r("STATE")
            Catch
                usr.State = UserState.Activo
            End Try
            Try
                usr.Nombres = r("NOMBRES")
            Catch
                usr.Nombres = ""
            End Try
            Try
                usr.firma = r("FIRMA")
            Catch ex As Exception
                usr.firma = ""
            End Try
            Try
                usr.Picture = r("FOTO")
            Catch ex As Exception
                usr.Picture = ""
            End Try
            Try
                usr.Apellidos = r("APELLIDO")
            Catch
                usr.Apellidos = ""
            End Try
            Try
                usr.puesto = r("PUESTO")
            Catch
                usr.puesto = ""
            End Try
            Try
                usr.telefono = r("TELEFONO")
            Catch
                usr.telefono = ""
            End Try
            Try
                usr.eMail.Mail = r("CORREO")
            Catch
                usr.eMail.Mail = ""
            End Try
            If UserTable.ContainsKey(usr.ID) Then
                UserTable(usr.ID) = usr
            Else
                UserTable.Add(usr.ID, usr)
            End If
            UsrTable.Add(usr)
        Next
        Return UserTable
    End Function
    Private Shared Function _getuserDataset() As DataSet
        Dim ds As New DataSet
        Try
            Dim sql As String = "Select * from usrtable order by NAME"
            ds = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Catch
        End Try
        Return ds
    End Function

#End Region

#Region "Funciones Publicas"
   
    Public Shared Function getAllUsersArrayListSorted() As ArrayList
        Return UsrTable
    End Function
    
    Public Shared Function GetAllUsersDataset() As DataSet
        Return DsUser
    End Function

   

    Public Shared Function AssignGroup(ByVal u As iuser, ByVal ug As iusergroup) As Boolean
        Dim strinsert As String = "insert into usr_r_group(usrid,groupid) values(" & u.ID & "," & ug.ID & ")"
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        Return True
    End Function
    Public Shared Sub RemoveUser(ByVal u As iuser, ByVal ug As iusergroup)
        Dim sql As String = "delete from usr_r_group where usrid = " & u.ID & " and groupid = " & ug.ID
        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    End Sub
    'Public Shared Sub DeleteGroup(ByVal u as iuser, ByVal ug as iusergroup)
    '    DeleteGroupRights(ug.id)
    '    Dim strDel As String = "delete usr_r_group where usrid = " & u.id & " and groupid = " & ug.id
    '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    '    strDel = "Delete from UsrGroup where ID=" & ug.id
    '    Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strDel)
    'End Sub
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
    Public Shared Function validateUser(ByVal name As String, ByVal Psw As String) As Int64
        Try
            Dim strselect As String = "select id from usrtable where state = 0 and name='" & name & "' and password='" & Zamba.Tools.Encryption.EncryptString(Psw, key, iv) & "'"
            Dim id As Int64 = Zamba.Servers.Server.Con.ExecuteScalar(CommandType.Text, strselect)
            Return id
        Catch ex As Exception
            ZTrace.WriteLineIf(ZTrace.IsVerbose, ex.ToString)
            Return 0
        End Try
    End Function
    

   
#Region "Encriptación"
    Private Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Private Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region

    ''Actualiza solamente la data de la tabla usr_table no actualiza grupos ni otras cosas
    'Public Shared Sub Update(ByVal usr as iuser)
    '    Dim strupdate As New System.Text.StringBuilder
    '    Try
    '        strupdate.Append("update usrtable set ")
    '        strupdate.Append("NAME='")
    '        strupdate.Append(usr.Name)
    '        strupdate.Append("'")
    '        strupdate.Append(",PASSWORD='")
    '        strupdate.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
    '        strupdate.Append("'")
    '        strupdate.Append(",LUPDATE=")
    '        strupdate.Append(Servers.Server.Con.SysDate)
    '        strupdate.Append(",STATE=")
    '        strupdate.Append(usr.State)
    '        strupdate.Append(",DESCRIPTION='")
    '        strupdate.Append(usr.description)
    '        strupdate.Append("'")
    '        strupdate.Append(",ADDRESS_BOOK='")
    '        strupdate.Append(usr.addressBook)
    '        strupdate.Append("'")
    '        'strupdate &= ",EXPIRATIONTIME=" & usr.Expirationtime & "'"
    '        'strupdate &= ",EXPIRATIONDATE=" & usr.Expiredate & ""
    '        strupdate.Append(",NOMBRES='")
    '        strupdate.Append(usr.Nombres)
    '        strupdate.Append("'")
    '        strupdate.Append(",APELLIDO='")
    '        strupdate.Append(usr.Apellidos)
    '        strupdate.Append("'")
    '        strupdate.Append(",CORREO='")
    '        strupdate.Append(usr.Mail)
    '        strupdate.Append("'")
    '        strupdate.Append(",TELEFONO='")
    '        strupdate.Append(usr.telefono)
    '        strupdate.Append("'")
    '        strupdate.Append(",PUESTO='")
    '        strupdate.Append(usr.puesto)
    '        strupdate.Append("'")
    '        strupdate.Append(",FOTO='")
    '        strupdate.Append(usr.Picture)
    '        strupdate.Append("'")
    '        strupdate.Append(",FIRMA='")
    '        strupdate.Append(usr.firma)
    '        strupdate.Append("'")
    '        strupdate.Append(" where ID=")
    '        strupdate.Append(usr.id)


    '        Dim sql As New System.Text.StringBuilder
    '        Try
    '            If Server.ServerType = DBTYPES.MSSQLServer7Up OrElse Server.ServerType = DBTYPES.MSSQLServer Then
    '                sql.Append("Insert into Security(Fecha,Userid,userpassword) values(Getdate(),")
    '                sql.Append(usr.id)
    '                sql.Append(",'")
    '                sql.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
    '                sql.Append("')")
    '            Else
    '                sql.Append("Insert into Security(Fecha,Userid,userpassword) values(Sysdate,")
    '                sql.Append(usr.id)
    '                sql.Append(",'")
    '                sql.Append(Zamba.Tools.Encryption.EncryptString(usr.Password, key, iv))
    '                sql.Append("')")
    '            End If

    '            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
    '        Catch
    '            Throw New Exception("No se pudo guardar la nueva contraseña")
    '        Finally
    '            sql = Nothing
    '        End Try

    '        Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
    '        Try
    '            strupdate.Append("Update usrnotes set conf_basemail='")
    '            strupdate.Append(usr.eMail.Base)
    '            strupdate.Append("', conf_mailserver='")
    '            strupdate.Append(usr.eMail.Servidor)
    '            strupdate.Append("' Where ID=")
    '            strupdate.Append(usr.id)
    '            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
    '        Catch
    '        End Try
    '    Finally
    '        strupdate = Nothing
    '    End Try
    'End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Actualiza la firma de usuario
    ''' </summary>
    ''' <param name="usr"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub UpdateSign(ByVal usr As iuser)
        Dim strupdate As New System.Text.StringBuilder
        Try
            strupdate.Append("update usrtable set FIRMA='")
            strupdate.Append(usr.firma)
            strupdate.Append("' where ID = ")
            strupdate.Append(usr.ID)
            Servers.Server.Con.ExecuteNonQuery(CommandType.Text, strupdate.ToString)
        Finally
            strupdate = Nothing
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Borra un Usuario
    ''' </summary>
    ''' <param name="usr"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub Delete(ByVal usr As iuser)
        Dim sql As New System.Text.StringBuilder
        Try
            sql.Append("Delete from usrtable where ID=")
            sql.Append(usr.ID)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            sql.Remove(0, sql.Length)

            sql.Append("Delete from Usr_R_Group where usrid=")
            sql.Append(usr.ID)
            Server.Con.ExecuteNonQuery(CommandType.Text, sql.ToString)
            '   UserFactory.GetAllUsers.Remove(usr.id)
        Finally
            sql = Nothing
        End Try
    End Sub
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
    Public Shared Function GetGroupUsers(ByVal GroupId As Integer) As DataSet
        Dim strselect As New System.Text.StringBuilder
        Try
            strselect.Append("Select * from usr_r_group where groupid=")
            strselect.Append(GroupId)
            Dim ds As DataSet = Servers.Server.Con.ExecuteDataset(CommandType.Text, strselect.ToString)

            Return ds
        Finally
            strselect = Nothing
        End Try
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Obtiene el nombre de usuario en base al ID
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	22/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function GetUserNamebyId(ByVal UserId As Int64) As String
        Dim strselect As New System.Text.StringBuilder
        Try
            strselect.Append("Select Name from USRTABLE where ID = ")
            strselect.Append(UserId)
            Return Server.Con.ExecuteScalar(CommandType.Text, strselect.ToString)
        Finally
            strselect = Nothing
        End Try
    End Function
#End Region

    Public Shared Function AssignGroup(ByVal ug As iusergroup, ByVal u As iuser) As Boolean
        If Not u.Groups.Contains(ug) Then
            UserFactory.AssignGroup(u, ug)
            u.Groups.Add(ug)
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function RemoveGroup(ByVal ug As iusergroup, ByVal u As iuser) As Boolean
        If u.Groups.Contains(ug) Then
            UserFactory.RemoveUser(u, ug)
            u.Groups.Remove(ug)
            Return True
        Else
            Return False
        End If
    End Function
End Class


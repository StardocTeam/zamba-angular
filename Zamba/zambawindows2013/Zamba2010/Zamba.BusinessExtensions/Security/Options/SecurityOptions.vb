Imports Zamba.Tools
Imports Zamba.Servers


Public Class SecurityOptions


    Public Shared Function getOptions() As DataSet
        Const QuerySecurityOptions As String = "SELECT * FROM ZSecOption"
        Dim DsSecurityOptions As DataSet = Server.Con.ExecuteDataset(CommandType.Text, QuerySecurityOptions)
        Return DsSecurityOptions
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si la clave se vencio o no.
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' True si la clave esta vencida
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	10/06/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function ClaveVencida(ByVal UserId As Int32) As Boolean
        Try
            If UserId = 9999 Then Return False 'para el usuario Zamba123456 la clave no vence
            Dim days As Int32
            Dim sql As String = "Select max(Fecha) from Security Where Userid=" & UserId
            Dim fecha As String = String.Empty

            If Boolean.Parse(UserPreferences.getValue("ExternalUser", UPSections.UserPreferences, "False")) = False Then
                Dim res As Object = Server.Con.ExecuteScalar(CommandType.Text, sql)
                If Not IsDBNull(res) Then
                    fecha = res.ToString()
                Else
                    'Si el campo 'Fecha' es dbNull es porque la contraseña es nueva y se considera vencida.
                    Return True
                End If

                Dias = Server.Con.ExecuteScalar(CommandType.Text, "Select expired from ZSecOption")
                If Dias = 0 Then Return False 'la clave no vence

                days = DateDiff(DateInterval.Day, CDate(fecha), Now)
                If days > Dias Then
                    Return True   'la clave esta vencida
                Else
                    If (Dias - days) < 10 Then
                        MessageBox.Show("Su contraseña vence en " & (Dias - days) & " días. Se recomienda cambiarla", "Vencimiento de Contraseña", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
                    End If
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            ZClass.raiseerror(ex)
            Return True
        End Try
    End Function
#Region "Variables"
    Public Shared Dias As Int16
    Public Shared key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public Shared iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
#End Region

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Verifica si el password es válido
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <param name="username"></param>
    ''' <param name="password"></param>
    ''' <returns></returns>
    ''' <remarks>SecurityOptions</remarks>
    ''' <history>
    ''' 	[Hernan]	16/06/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function IsValidPassword(ByVal userid As Int64, ByVal username As String, ByVal password As String, ByVal PswHasChanged As Boolean) As Boolean
        Dim opcion As New Opciones

        If opcion.SameUser = True Then
            If String.Compare(username, password) = 0 Then
                MessageBox.Show("La contraseña no puede ser igual al usuario, ingrese otro", "Error en el valor de la clave", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
                Exit Function
            End If
        End If
        If opcion.AcceptNull = False Then
            If password.Length = 0 Then
                MessageBox.Show("La contraseña no puede estar en blanco", "Error en el valor de la clave", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
                Exit Function
            End If
        End If
        If opcion.SamePC = False Then
            If String.Compare(password, Environment.MachineName) = 0 Then
                MessageBox.Show("La contraseña no puede ser igual al nombre de la PC", "Error en el valor de la clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End If
        End If
        If opcion.Alfanumerico = True Then
            If IsAlfanumeric(password) = False Then
                MessageBox.Show("La contraseña debe contener letras y números", "Error en el valor de la clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End If
        End If
        If opcion.LongitudMinima = True Then
            If password.Length < 6 Then
                MessageBox.Show("La contraseña debe tener como mínimo 6 caracteres", "Error en el valor de la clave", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return False
                Exit Function
            End If
        End If

        If PswHasChanged = True Then
            If opcion.CanRepeat = False Then
                If Repetida(userid, password) = True Then
                    PswHasChanged = False
                    If MessageBox.Show("Esta contraseña ya fue utilizada en los últimos 12 meses. Desea utilizarla de todos modos?", "Zamba Software", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.Yes Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
        End If
        Return True
    End Function
    Private Shared Function IsAlfanumeric(ByVal password As String) As Boolean
        Dim chars() As Char = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
        If password.IndexOfAny(chars) = -1 Then
            Return False
        Else
            Dim i As Int16
            For i = 0 To chars.Length - 1
                password = password.Replace(chars(i), "")
            Next
            If password.Length <> 0 AndAlso Not IsNumeric(password) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    Private Shared Function Repetida(ByVal userid As Int32, ByVal password As String) As Boolean
        Dim sql As String = "Select * from Security where Userid=" & userid & " and userpassword='" & Encryption.EncryptString(password, key, iv) & "'"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dias = Server.Con.ExecuteScalar(CommandType.Text, "Select expired from ZSecOption")
        If ds.Tables(0).Rows.Count = 0 Then
            Return False
        Else
            If DateDiff(DateInterval.Month, ds.Tables(0).Rows(0).Item(0), Now) < 12 Then
                Return True
            Else
                Return False
            End If
        End If
    End Function
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Bloquea el usuario. Se utiliza luego de tres intentos fallidos de ingreso
    ''' </summary>
    ''' <param name="Userid"></param>
    ''' <returns></returns>
    ''' <remarks>Security Options</remarks>
    ''' <history>
    ''' 	[Hernan]	16/06/2005	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Function LockUserPassword(ByVal Userid As Int32, Optional ByVal userName As String = "") As Boolean
        Try

            Dim sql As String
            If Userid <> 0 Then
                sql = "Update usrtable set State=1 Where Id=" & Userid
            Else
                sql = "Select id from usrtable where name='" & userName & "'"
                Userid = Server.Con.ExecuteScalar(CommandType.Text, sql)
                sql = "Update usrtable set State=1 Where name='" & userName & "'"
            End If
            Dim i As Int32
            i = Server.Con.ExecuteNonQuery(CommandType.Text, sql)

            '[AlejandroR] - 13/01/10 (Bug 4044)
            'Comento este codigo para que no cambie la pass del usuario cuando se bloquea
            'sql = "Update usrtable set password='" & Encryption.EncryptString("bloqueada", key, iv) & "' where id=" & Userid
            'Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            If i = 1 Then
                NotificarBloqueo(Userid)
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function
    Public Shared Function MustLockUser() As Boolean
        If Server.Con.ExecuteScalar(CommandType.Text, "Select lockuser from ZSecOption") = "TRUE" Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Shared Sub NotificarBloqueo(ByVal Userid As Int32)
        Try
            Dim sql As String = "Select name from usrtable where id=" & Userid
            Dim nombre As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            sql = "Select SecurityMail from ZSecOption"
            Dim mail As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            Zamba.Core.MessagesBusiness.SendMail(mail, String.Empty, String.Empty, "ZAMBA - BLOQUEO DE USUARIO", "El usuario " & nombre & " ha bloqueado su contraseña de Zamba, por haber ingresado erroneamente su contraseña 3 veces", True)
        Catch
        End Try
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    '''    Guarda la configuración de las opciones de seguridad
    ''' </summary>
    ''' <param name="expiredtime"></param>
    ''' <param name="acceptnull"></param>
    ''' <param name="sameuser"></param>
    ''' <param name="samemachine"></param>
    ''' <param name="lockuser"></param>
    ''' <param name="email"></param>
    ''' <param name="actualizar"></param>
    ''' <param name="userId"></param>
    ''' <remarks>Security Options </remarks>
    ''' <history>
    ''' 	[Hernan]	16/06/2005	Created
    ''' 	[Marcelo]	22/05/2008	Modified
    '''     [Javier]       10/05/2012  Modified
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Shared Sub SetConfig(ByVal expiredtime As Int16, ByVal acceptnull As String, ByVal sameuser As String, ByVal samemachine As String, ByVal lockuser As String, ByVal email As String, ByVal actualizar As Boolean, ByVal longitud As Boolean, ByVal alfanumerico As Boolean, ByVal CanRepeat As Boolean, ByVal ADLogin As Boolean, ByVal userId As Int64)
        If actualizar Then
            Server.Con.ExecuteNonQuery(CommandType.Text, "Update ZSecOption set sameuser='" & sameuser.ToUpper & "', expired=" & expiredtime & ", samepc='" & samemachine.ToUpper & "', acceptnull='" & acceptnull.ToUpper & "', lockuser='" & lockuser.ToUpper & "', SecurityMail='" & email & "',longitudminima='" & longitud.ToString.ToUpper & "', alfanumerico='" & alfanumerico.ToString.ToUpper & "', RepeatPassword='" & CanRepeat.ToString.ToUpper & "'")
        Else
            Server.Con.ExecuteNonQuery(CommandType.Text, "Insert into ZSecOption(sameuser,expired,acceptnull,lockuser,samepc,SecurityMail,longitudminima,alfanumerico,RepeatPassword) values('" & sameuser.ToUpper & "'," & expiredtime & ",'" & acceptnull.ToUpper & "','" & lockuser.ToUpper & "','" & samemachine.ToUpper & "','" & email & "','" & longitud.ToString.ToUpper & "','" & alfanumerico.ToString.ToUpper & "','" & CanRepeat.ToString.ToUpper & "')")
        End If

        Dim zopt As New ZOptBusiness()
        If String.IsNullOrEmpty(zopt.GetValue("UseADLogin")) Then
            zopt.Insert("UseADLogin", ADLogin)
        Else
            zopt.Update("UseADLogin", ADLogin)
        End If

        'Guardo la accion en la columna U_Time de UCM y en User_Hst para el manejo de sesion y el historial de acciones
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaCaduca=" & expiredtime)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaIgualUsuario=" & sameuser.ToUpper)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaIgualPC=" & samemachine)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaEnBlanco=" & acceptnull)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: BloquearUsuario3Fallidos=" & lockuser)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaAlfanumérica=" & alfanumerico)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaMinimo6=" & longitud)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: ContraseñaRepetida=" & CanRepeat)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad: UsarADLogin=" & ADLogin)
        UserBusiness.Rights.SaveAction(0, ObjectTypes.Security, RightsType.Edit, "Se modificarion las Opciones de seguridad:  MailNotificación=" & email)
    End Sub
    Public Shared Sub SavePassword(ByVal Userid As Int32, ByVal newpassword As String)

        If Server.isSQLServer Then
            Server.Con.ExecuteScalar(CommandType.Text, "Insert into security(Fecha,UserID,UserPassword)values(Getdate()," & Userid & ",'" & Encryption.EncryptString(newpassword, key, iv) & "')")
        Else
            Server.Con.ExecuteScalar(CommandType.Text, "Insert into security(Fecha,UserID,UserPassword)values(Sysdate," & Userid & ",'" & Encryption.EncryptString(newpassword, key, iv) & "')")
        End If

    End Sub
End Class
Public Class Opciones
#Region "Variables"
    Public CanRepeat As Boolean
    Public SameUser As Boolean
    Public SamePC As Boolean
    Public AllowRepeat As Boolean
    Public Expired As Int16
    Public AcceptNull As Boolean
    Public LockUser As Boolean
    Public LongitudMinima As Boolean
    Public Alfanumerico As Boolean
#End Region

    Public Sub New()
        LoadOptions()
        'TODO: Verificar si se puede repetir o no la contraseña.
    End Sub
    Private Sub LoadOptions()
        Try
            Dim sql As String = "Select * from ZSecOption"
            Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
            If (ds.Tables(0).Rows.Count > 0) Then
                If String.Compare(ds.Tables(0).Rows(0).Item(0).ToString, "FALSE", True) = 0 Then
                    SameUser = False
                Else
                    SameUser = True
                End If
                Expired = ds.Tables(0).Rows(0).Item(1)
                If String.Compare(ds.Tables(0).Rows(0).Item(2).ToString, "FALSE") = 0 Then
                    AcceptNull = False
                Else
                    AcceptNull = True
                End If
                If String.Compare(ds.Tables(0).Rows(0).Item(3).ToString, "FALSE") = 0 Then
                    LockUser = False
                Else
                    LockUser = True
                End If
                If String.Compare(ds.Tables(0).Rows(0).Item(4).ToString, "FALSE") = 0 Then
                    SamePC = False
                Else
                    SamePC = True
                End If
                If String.Compare(ds.Tables(0).Rows(0).Item(6).ToString, "FALSE") = 0 Then
                    LongitudMinima = False
                Else
                    LongitudMinima = True
                End If
                If String.Compare(ds.Tables(0).Rows(0).Item(7).ToString, "FALSE") = 0 Then
                    Alfanumerico = False
                Else
                    Alfanumerico = True
                End If
                If String.Compare(ds.Tables(0).Rows(0).Item(8).ToString, "FALSE") = 0 Then
                    CanRepeat = False
                Else
                    CanRepeat = True
                End If
            End If
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
    End Sub

End Class

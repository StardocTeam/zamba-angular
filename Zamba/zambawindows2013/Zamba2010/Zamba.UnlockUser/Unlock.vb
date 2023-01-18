Imports Zamba.Servers
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.UnlockUser
''' Class	 : UnlockUser.Unlock
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para desbloquear usuarios a los cuales se le vencio su contraseña 
''' por no modificarla a tiempo
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Unlock
    Implements IDisposable

    Public key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    Public iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Desbloquea un usuario generandole una nueva clave
    ''' </summary>
    ''' <param name="userid">Id de Usuario que se desea desbloquear</param>
    ''' <param name="newpassword">Nueva clave para el usuario</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub unlockuser(ByVal userid As Int32, ByVal newpassword As String)
        Try
            Dim sql As String = "Update Usrtable set password='" & Zamba.Tools.Encryption.EncryptString(newpassword, key, iv) & "', State=0 WHERE Id=" & userid
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            If Server.ServerType = Server.DBTYPES.MSSQLServer Or Server.ServerType = Server.DBTYPES.MSSQLServer7Up Then
                sql = "Insert into security(Fecha,UserID,UserPassword)values(Getdate()," & userid & ",'" & Zamba.Tools.Encryption.EncryptString(newpassword, key, iv) & "')"
            Else
                sql = "Insert into security(Fecha,UserID,UserPassword)values(Sysdate," & userid & ",'" & Zamba.Tools.Encryption.EncryptString(newpassword, key, iv) & "')"
        End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub Dispose() Implements System.IDisposable.Dispose

    End Sub
End Class

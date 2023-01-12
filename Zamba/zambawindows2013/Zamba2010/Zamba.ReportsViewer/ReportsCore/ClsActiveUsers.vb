Imports ZAMBA.Servers
Imports ZAMBA.AppBlock

Public Class ActiveUsers
    Public Shared Function Get_OnLine_Users() As DsActiveUsers
        Dim dstemp As DataSet
        Dim ds As New DsActiveUsers
        Dim row2 As DsActiveUsers.LicenciasRow = ds.Licencias.NewLicenciasRow
        Try
            RemoveOldConnections()
            'Dim strselect As String = "select * from onlineusers"
            Dim strselect As String = "select * from Zvw_ONLINEUSERS_100"
            dstemp = Server.Con(True).ExecuteDataset(CommandType.Text, strselect)
            dstemp.Tables(0).TableName = ds.Tables(1).TableName
            ds.Merge(dstemp)
            row2.Cantidad = Get_Active_Licences()
            ds.Licencias.Rows.Add(row2)
            ds.AcceptChanges()
        Catch ex As ZException
            ZException.Log(ex)
        End Try
        Return ds
    End Function
    Public Shared Function Get_Active_Licences() As Int32
        Try
            Dim strselect As String = "Select NUMERO_LICENCIAS from Lic where type=0"
            Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
            Dim licencias As Int32
            licencias = CInt(Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, strselect), key, iv))
            Return licencias
        Catch ex As ZException
            ZException.Log(ex)
        End Try
    End Function

    Private Shared Sub RemoveOldConnections()
        Dim key As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim iv As Byte() = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 1, 2, 3, 4, 5, 6}
        Dim freeWF As Int32 = 0
        Dim freeDoc As Int32 = 0
        Dim sql As String
        Try
            If Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.OracleClient Then
                freeDoc = Server.Con(True).ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM WHERE(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) and Type=0")
            Else
                'Dim ExpiredTime As String = Format(Now.Subtract(New TimeSpan(0, Server.TimeOut, 0)), "yyyy-MM-dd HH:mm:ss")
                freeDoc = Server.Con(True).ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM WHERE Type=0 and DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
            End If
            Dim count As String = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC Where TYPE=0"), key, iv)
            Dim usados As Int32 = (count - freeDoc)
            If usados < 0 Then usados = 0
            sql = "Update LIC set Used='" & Zamba.Tools.Encryption.EncryptString(usados, key, iv) & "' Where TYPE=0"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
            'Workflow
            If Server.ServerType = Server.DBTYPES.Oracle Or Server.ServerType = Server.DBTYPES.Oracle9 Or Server.ServerType = Server.DBTYPES.OracleClient Then
                freeWF = Server.Con(True).ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM WHERE(TIME_OUT < TO_NUMBER(SYSDATE - U_TIME) * (24 * 60)) and Type=1")
            Else
                'Dim ExpiredTime As String = Format(Now.Subtract(New TimeSpan(0, Server.TimeOut, 0)), "yyyy-MM-dd HH:mm:ss")
                freeWF = Server.Con(True).ExecuteNonQuery(CommandType.Text, "DELETE FROM UCM WHERE Type=1 and DATEDIFF(mi,U_TIME,GetDate())> [Time_Out]")
            End If
            count = Zamba.Tools.Encryption.DecryptString(Server.Con.ExecuteScalar(CommandType.Text, "Select Used from LIC Where TYPE=1"), key, iv)
            usados = (count - freeWF)
            If usados < 0 Then usados = 0
            sql = "Update LIC set Used='" & Zamba.Tools.Encryption.EncryptString(usados, key, iv) & "' Where TYPE=1"
            Server.Con.ExecuteNonQuery(CommandType.Text, sql)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function LockedUser() As DataSet
        Dim sql As String = "Select Name,State,ID from Usrtable order by description"
        Dim ds As DataSet = Server.Con.ExecuteDataset(CommandType.Text, sql)
        Dim ds2 As New DsUsers
        Dim i As Int32
        For i = 0 To ds.Tables(0).Rows.Count - 1
            Dim row As DsUsers.BloqueadosRow = ds2.Bloqueados.NewBloqueadosRow
            If ds.Tables(0).Rows(i).Item(1) = 1 Then
                row.Usuario = ds.Tables(0).Rows(i).Item(0)
                row.Estado = "Bloqueado"
            End If
            If ClaveVencida(ds.Tables(0).Rows(i).Item(2)) Then
                row.Usuario = ds.Tables(0).Rows(i).Item(0)
                row.Estado = "Clave Vencida"
            End If
            ds2.Bloqueados.Rows.Add(row)
            ds2.AcceptChanges()
        Next
        Return ds2
    End Function
    Private Shared Function ClaveVencida(ByVal Userid As Int32) As Boolean
        'Dim valid As Boolean
        Dim dias As Int16
        Try
            Dim days As Int32
            Dim sql As String = "Select max(Fecha) from Security Where Userid=" & Userid
            Dim fecha As String = Server.Con.ExecuteScalar(CommandType.Text, sql)
            dias = Server.Con.ExecuteScalar(CommandType.Text, "Select expired from ZSecOption")
            If dias = 0 Then Return False 'la clave no vence
            days = DateDiff(DateInterval.Day, CDate(fecha), Now)
            If days > dias Then
                Return True   'la clave esta vencida
            End If
        Catch ex As Exception
        End Try
    End Function
End Class


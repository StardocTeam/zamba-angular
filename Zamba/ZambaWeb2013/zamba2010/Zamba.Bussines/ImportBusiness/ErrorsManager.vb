Public Class ErrorsManager

    Public Shared Sub BackupFile(ByVal Bkpfolder As String, ByVal filename As String)
        Try
            IO.File.Copy(filename, Bkpfolder & "\" & filename)
        Catch ex As Exception
            '        SendMail(ex, "No se pudo hacer backup del archivo")
        End Try
    End Sub
    'Public Shared Sub SendMail(ByVal ex As Exception, ByVal mensaje As String)
    '    Try
    '        Dim correo As New Zamba.Tools.ZSMTP
    '        correo.SendMail("soporte@stardoc.com.ar,gustavo.yapura@marsh.com", mensaje & ". " & ex.ToString, Environment.UserName & " - Error en Importación Local ")
    '    Catch
    '    End Try
    'End Sub
    'Public Shared Sub LOG(ByVal ex As Exception, ByVal mensaje As String)
    '    Try
    '        Dim exep As String = ex.ToString.Replace("'", "").Replace(Chr(34), "").Replace(Chr(10), "").Replace(Chr(13), "").Trim
    '        Dim origen As String = ex.Source.Replace("'", "").Replace(Chr(34), "").Replace(Chr(10), "").Replace(Chr(13), "").Trim

    '        If exep.Length > 1500 Then exep = exep.Substring(0, 1490)
    '        Dim id As New Zamba.Tools.ApplicationConfig(Tools.ApplicationConfig.ConfigType.All)
    '        Dim sql As String = "Insert into EXCEP values(" & id.STATIONID & ",sysdate," & origen & "," & exep & "," & mensaje & ")"
    '        Server.Con.ExecuteNonQuery(CommandType.Text, sql)
    '        id.dispose()
    '        SendMail(ex, mensaje)
    '    Catch
    '    End Try
    'End Sub
End Class

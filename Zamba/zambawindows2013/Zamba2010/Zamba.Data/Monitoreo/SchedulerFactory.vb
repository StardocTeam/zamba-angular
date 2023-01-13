Public Class SchedulerFactory

    Public Shared Sub UpdateFolderConfig(ByVal sIntervalo As String, ByVal sAlarmTimeDisp As String, ByVal tipoConfig As Int32, ByVal idCarpeta As Decimal, ByVal lDays() As Object)

        Dim strUpdate As String = "UPDATE IP_FolderConf SET Timer = " & (CDec(sIntervalo) * 60000).ToString.Replace(",", ".") & ", Alarma= '" & sAlarmTimeDisp & "', Domingo=" & lDays.GetValue(0) & ", Lunes=" & lDays.GetValue(1) & ", Martes=" & lDays.GetValue(2) & ", Miercoles=" & lDays.GetValue(3) & ", Jueves=" & lDays.GetValue(4) & ", Viernes=" & lDays.GetValue(5) & ", Sabado=" & lDays.GetValue(6) & ", Tipo_Conf=" & tipoConfig & " WHERE Id_Carpeta = " & idCarpeta
        Server.Con.ExecuteNonQuery(CommandType.Text, strUpdate)
    End Sub

    Public Shared Function GetFolderConfig(ByVal idTarea As Decimal) As DataSet
        Dim dsTemp As New DataSet
        Dim strSelect As String = "SELECT * FROM IP_FolderConf WHERE Id_Carpeta = " & idTarea
        DSTEMP = Server.Con.ExecuteDataset(CommandType.Text, strSelect)
        Return dsTemp
    End Function

End Class

Imports ZAMBA.Servers

Public Class FactoryProcessScheduler

    Public Shared Function CreateEmptyObject() As ProcessTask
        Dim newprocess As New ProcessTask
        With newprocess
            .Hora = ""
            .Id = 0
            .ProcessGroup = "P"
            .UserId = 9999
            .Dia = 0
            .Maquina = Environment.MachineName
        End With
        Return newprocess
    End Function

    Public Shared Function CreateObject(ByVal Id As Integer, ByVal IdType As String, ByVal UserId As Int32, ByVal dia As DayOfWeek, ByVal hora As String, ByVal maquina As String) As ProcessTask
        Dim newprocess As New ProcessTask
        With newprocess
            .Hora = hora.Trim
            .Id = Id
            .ProcessGroup = IdType
            .UserId = UserId
            .Dia = dia
            .Maquina = maquina
        End With
        Return newprocess
    End Function
    Public Shared Function GetAllObjects(ByVal Id As Integer, ByVal processGroup As String) As ArrayList
        Dim alConf As New ArrayList
        Dim strselect As String = "SELECT * FROM IP_PROCESSTASK WHERE ID_PROCESS = " & Id & " AND GRUPO_PROCESS = '" & processGroup & "'"
        Dim ds As DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                With ds.Tables(0).Rows(i)
                    alConf.Add(CreateObject(.Item("ID_PROCESS"), ds.Tables(0).Rows(i).Item("GRUPO_PROCESS"), ds.Tables(0).Rows(i).Item("User_Id"), .Item("DIA"), .Item("HORA"), Environment.MachineName))
                End With
            Next
        End If
        Return alConf
    End Function
    Public Shared Function GetAllObjectsOfMachine(ByVal maquina As String) As ArrayList
        Dim alConf As New ArrayList
        Dim strselect As String = "SELECT * FROM Ip_ProcessTask WHERE Upper(Maquina) = '" & maquina.ToUpper.Trim & "'"
        Dim ds As New DataSet
        ds = Server.Con.ExecuteDataset(CommandType.Text, strselect)
        If ds.Tables(0).Rows.Count > 0 Then
            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                alConf.Add(CreateObject(ds.Tables(0).Rows(i).Item("Id_Process"), ds.Tables(0).Rows(i).Item("GRUPO_PROCESS"), ds.Tables(0).Rows(i).Item("User_Id"), ds.Tables(0).Rows(i).Item("Dia"), ds.Tables(0).Rows(i).Item("Hora"), Environment.MachineName))
            Next
        End If
        Return alConf
    End Function
    Public Shared Sub StoreObject(ByVal process As ProcessTask)
        Dim strinsert As String = "INSERT INTO IP_PROCESSTASK (ID_PROCESS,GRUPO_PROCESS,USER_ID,DIA,HORA,MAQUINA) VALUES (" _
        & process.Id & ",'" & process.ProcessGroup & "'," & process.UserId & "," & process.Dia & ",'" & process.Hora & "','" & process.Maquina & "')"
        Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
    End Sub
    Public Shared Sub DeleteObject(ByVal process As ProcessTask)
        Dim strdelete As String = "DELETE FROM IP_PROCESSTASK WHERE ID_PROCESS = " & process.Id & " AND DIA = '" & process.Dia & "' AND HORA = '" & process.Hora & "' AND GRUPO_PROCESS = '" & process.ProcessGroup & "'"
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub

End Class

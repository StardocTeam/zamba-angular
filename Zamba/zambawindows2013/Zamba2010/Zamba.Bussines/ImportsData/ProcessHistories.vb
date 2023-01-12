Imports System.Security.Cryptography
Imports Zamba.Servers
Imports Zamba.Data
Imports System.Data.OracleClient

Public Class ProcessHistories

    Public Enum CounterTypes
        Counted
        Imported
        Skiped
        ErrorImported
        ErrorFound
    End Enum


    Public Shared Sub DelHistory(ByVal HistoryId As Int32)
        Dim strdelete As String = "DELETE FROM P_HST where ID = " & HistoryId
        Server.Con.ExecuteNonQuery(CommandType.Text, strdelete)
    End Sub
    Public Shared Function NewHistory(ByVal Process As Process) As DsProcessHistory.ProcessHistoryRow
        Dim HST As DsProcessHistory.ProcessHistoryRow = Process.History.Histories.ProcessHistory.NewProcessHistoryRow
        HST.ID = CoreData.GetNewID(IdTypes.IPHST) + 1
        HST.Process_ID = Process.ID
        HST.TotalFiles = 0
        HST.ProcessedFiles = 0
        HST.SkipedFiles = 0
        HST.ErrorFiles = 0
        HST.Path = Process.Path
        HST.Process_Date = Now
        HST.RESULT = Zamba.Core.Results.Sin_procesar
        HST.Result_Id = Zamba.Core.Results.Sin_procesar
        HST.User_Id = Process.UserId
        Dim TempDirectory As New IO.DirectoryInfo(Process.BackUpPath & "\Process " & Process.Name & " " & HST.Process_Date.ToString("dd-MM-yyyy hh-mm-ss"))
        If TempDirectory.Exists = False Then TempDirectory.Create()
        Dim Fi As New IO.FileInfo(Process.Path)
        Dim TempFile As New IO.FileInfo(TempDirectory.FullName & "\" & Fi.Name.Insert(Fi.Name.LastIndexOf("."), " TEMP"))
        HST.TEMPFILE = TempFile.FullName
        Dim ErrorFile As New IO.FileInfo(TempDirectory.FullName & "\" & Fi.Name.Insert(Fi.Name.LastIndexOf("."), " ERROR"))
        HST.ERRORFILE = ErrorFile.FullName
        Dim LOGFile As New IO.FileInfo(TempDirectory.FullName & "\" & Fi.Name.Insert(Fi.Name.LastIndexOf("."), " LOG"))
        HST.LOGFILE = LOGFile.FullName
        Try
            HST.Hash = Trim(ProcessHistories.GetFileHash(HST.Path))
        Catch ex As Exception
            HST.Hash = " "
        End Try
        Return HST

    End Function

    Public Shared Sub SaveHistory(ByVal History As ProcessHistory)
        If Server.IsOracle Then
            Dim parNames() As String = {"HID", "PID", "PDATE", "USrid", "totfiles", "procfiles", "skpfiles", "ErrFiles", "RID", "pth", "hsh", "TFILE", "EFILE", "LFILE"}
            Dim parTypes() = {OracleType.Number, OracleType.Number, OracleType.DateTime, OracleType.Number, OracleType.Number, OracleType.Number, OracleType.Number, OracleType.Number, OracleType.Number, OracleType.VarChar, OracleType.VarChar, OracleType.VarChar, OracleType.VarChar, OracleType.VarChar}
            Dim parValues() = {History.Id, History.Process_ID, History.Process_Date, History.User_Id, History.TotalFiles, History.ProcesedFiles, History.SkipedFiles, History.ErrorFiles, (History.Result), History.Path, History.Hash, History.TEMPFILE, History.ERRORFILE, History.LOGFILE}
            Dim i As Integer
            ZTrace.WriteLineIf(ZTrace.IsInfo, "Antes de  ejecutar store: parname,partype,parvalues ")
            For i = 0 To parNames.Length - 1
                ZTrace.WriteLineIf(ZTrace.IsInfo, parNames(0) & "  " & parTypes(i) & "   " & parValues(i))
            Next

            Server.Con.ExecuteNonQuery("INSERT_PROCESS_HST_PKG.InsertProcHst", parValues)
        Else
            Dim strinsert As String = "INSERT INTO P_HST (ID,Process_ID, Process_Date, User_Id, TotalFiles, ProcessedFiles, SkipedFiles,ErrorFiles,Result_Id,PATH,HASH,TEMPFILE,ERRORFILE,LOGFILE) VALUES (" & History.Id & "," & History.Process_ID & "," & "CONVERT(DATETIME,'" & History.Process_Date.ToString("MM/dd/yyyy hh:mm:ss") & "', 102)," & History.User_Id & "," & History.TotalFiles & "," & History.ProcesedFiles & "," & History.SkipedFiles & "," & History.ErrorFiles & "," & (History.Result) & ",'" & Trim(History.Path) & "','" & History.Hash & "','" & History.TEMPFILE & "','" & History.ERRORFILE & "','" & History.LOGFILE & "')"
            Server.Con.ExecuteNonQuery(CommandType.Text, strinsert)
        End If
    End Sub
    Public Shared Sub UpdateHistory(ByVal ProcessHistory As ProcessHistory)
        Try
            If ProcessHistory.Path.Trim = "" Then
                ProcessHistory.Path = "No Informado"
                ProcessHistory.Hash = ""
            End If

            If Server.IsOracle Then
                Dim parNames() As String = {"HID", "totfiles", "procfiles", "skpfiles", "ErrFiles", "RID", "hsh"}
                Dim parTypes() = {13, 13, 13, 13, 13, 13, 7}
                Dim parValues() = {ProcessHistory.Id, ProcessHistory.TotalFiles, ProcessHistory.ProcesedFiles, ProcessHistory.SkipedFiles, ProcessHistory.ErrorFiles, (ProcessHistory.Result), ProcessHistory.Hash}
                Server.Con.ExecuteNonQuery("UPDATE_PROCESS_HST_PKG.UpdateProcHst", parValues)
            Else
                Dim strupdate As String = "UPDATE P_HST SET TotalFiles = " & ProcessHistory.TotalFiles & ", ProcessedFiles = " & ProcessHistory.ProcesedFiles & ", SkipedFiles = " & ProcessHistory.SkipedFiles & ", Result_ID = " & (ProcessHistory.Result) & ",ERRORFiles = " & ProcessHistory.ErrorFiles & " where ID = " & ProcessHistory.Id
                Server.Con.ExecuteNonQuery(CommandType.Text, strupdate)
            End If
        Catch ex As Exception
            Throw New Exception("Error al actualizar el historial" & ex.ToString)
        End Try
    End Sub
#Region "Hash"
    Public Shared Function GetFileHash(ByVal Name As String) As String
        Dim result() As Byte
        Dim md5hash As MD5 = New MD5CryptoServiceProvider
        Dim stream As IO.Stream = IO.File.OpenRead(Name)
        result = md5hash.ComputeHash(stream)

        Dim sb As New System.Text.StringBuilder(2 + (result.Length * 2))

        Dim i As Integer
        For i = 0 To result.Length - 1
            sb.Append(result(i).ToString("x2"))
        Next

        Return "0x" + sb.ToString
    End Function
    Public Shared Function ProcessIsReady(ByVal Process As Process) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT count(1) FROM P_HST WHERE")
        sb.Append(" USER_ID = " & Process.UserId)
        sb.Append(" AND PROCESS_ID= " & Process.ID)
        sb.Append(" AND HASH = '" & Trim(GetFileHash(Process.Path)) & "'")
        sb.Append(" AND RESULT_ID = " & 1)

        Dim result As Integer
        result = Server.Con.ExecuteScalar(CommandType.Text, sb.ToString)
        If result > 0 Then
            Return True
        End If
        Return False
    End Function
#End Region
End Class

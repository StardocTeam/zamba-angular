Imports Zamba.Data

Public Class DBWriter
    Implements IDBWriter

    Public Sub Write(traceDto As ITraceDto) Implements IDBWriter.Write
        Dim con As IConnection
        con = Servers.Server.Con()
        Dim Id As Int64 = CoreData.GetNewID(IdTypes.TraceId)
        Dim insertstr As String = String.Format("insert into ZTrace (Id,ModuleName, TraceDate, MachineName, IP, WinUser, TraceTime, WorkingSet, TraceMessage, Details, TraceLevel, OSVersion, UserDomainName) values ({0},'{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}','{9}',{10},'{11}','{12}')", Id, traceDto.ModuleName, traceDto.TraceDate.ToString("yyyy-MM-dd HH:mm:ss:") & traceDto.TraceDate.Millisecond.ToString, traceDto.machineName, traceDto.IP, traceDto.UserName, traceDto.TraceTime, traceDto.workingSet.ToString(), traceDto.Message.Replace("'", "''"), traceDto.Details, DirectCast(traceDto.level, Integer), traceDto.oSVersion.VersionString, traceDto.userDomainName)
        con.ExecuteNonQuery(CommandType.Text, insertstr)
    End Sub
End Class

Friend Class TraceDto
    Implements ITraceDto

    Public Property oSVersion As OperatingSystem Implements ITraceDto.oSVersion
    Public Property userDomainName As String Implements ITraceDto.userDomainName
    Public Property workingSet As Long Implements ITraceDto.workingSet
    Public Property machineName As String Implements ITraceDto.machineName
    Public Property level As TraceLevel Implements ITraceDto.level
    Public Property UserName As String Implements ITraceDto.UserName
    Public Property TraceTime As Integer Implements ITraceDto.TraceTime
    Public Property TraceDate As Date Implements ITraceDto.TraceDate
    Public Property IP As String Implements ITraceDto.IP
    Public Property Message As String Implements ITraceDto.Message
    Public Property Details As String Implements ITraceDto.Details

    Public Property ModuleName As String Implements ITraceDto.ModuleName


    Public Sub New(oSVersion As OperatingSystem, userDomainName As String, workingSet As Long, machineName As String, Message As String, level As TraceLevel, UserName As String, TraceTime As Integer, TraceDate As Date, IP As String, ModuleName As String)
        Me.oSVersion = oSVersion
        Me.ModuleName = ModuleName
        Me.userDomainName = userDomainName
        Me.workingSet = workingSet
        Me.machineName = machineName
        Me.Message = Message
        Me.level = level
        Me.UserName = UserName
        Me.TraceTime = TraceTime
        Me.TraceDate = TraceDate
        Me.IP = IP
    End Sub
End Class

Public Interface ITraceDto
    Property UserName As String
    Property level As TraceLevel
    Property Message As String
    Property machineName As String
    Property oSVersion As OperatingSystem
    Property TraceDate As Date
    Property TraceTime As Integer
    Property userDomainName As String
    Property workingSet As Long
    Property IP As String
    Property Details As String
    Property ModuleName As String
End Interface

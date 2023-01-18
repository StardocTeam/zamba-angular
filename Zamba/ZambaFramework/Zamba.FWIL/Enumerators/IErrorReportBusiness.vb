Public Interface IErrorReportBusiness
    Sub AddErrorReport(errorReport As IErrorReport)
    Sub AddException(ex As Exception)
    Sub AddPerformanceIssue(subject As String, description As String)
    Sub EditReport(errorId As Long, errorState As ErrorReportStates, comments As String)
    Function GetAttachment(attachId As Long) As Byte()
    Function GetAttachments(errorId As Long) As Generic.List(Of IErrorReportAttachment)
    Function GetErrorReports() As Generic.List(Of IErrorReport)
    Function GetReportsToExport() As IErrorReport()
End Interface

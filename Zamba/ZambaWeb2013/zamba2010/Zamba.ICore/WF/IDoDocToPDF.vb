Public Interface IDoDocToPDF
    Inherits IRule
    Property FullPath() As String
    Property FileName() As String
    Property ExportTask() As Boolean
    Property UseNewConversion() As Boolean
End Interface
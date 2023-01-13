Public Interface IDoImportFromExcel
    Property File() As String
    Property ExcelVersion() As OfficeVersion
    Property SheetName() As String
    Property VarName() As String
    Property SaveAs() As Boolean
    Property SaveAsPath() As String
    Property SaveAsFileName() As String
    Property UseSpireConverter() As Boolean

End Interface

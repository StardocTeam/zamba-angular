Public Interface IDoGenerateExcelFromObject
    Inherits IRule
    Property DataSetName() As String
    Property ExcelNAme() As String
    Property ExportType() As ExcelExportTypes
    Property AddColName() As Boolean
    Property OtherFormattype() As String
    Property Path() As String
    Property UseSpireConverter() As Boolean
End Interface

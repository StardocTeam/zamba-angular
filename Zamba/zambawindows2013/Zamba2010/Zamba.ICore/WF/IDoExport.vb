Public Interface IDoExport
    Inherits IRule
    Property DoctypeId() As Int64
    Property resultLine() As String
    Property separator() As String
    Property documentName() As String
    Property documentPath() As String
    Property SortString() As String
    Property VersionsExportedDocuments() As Boolean
End Interface
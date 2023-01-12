Public Interface IDOSearchDoc
    Inherits IRule
    Property SearchType() As Int32
    Property SearchDocTypeId() As Int64
    Property SearchDocId() As Int64
    Property SearchIndexs() As String
End Interface

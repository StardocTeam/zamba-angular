Public Interface IDOExecuteQuery
    Inherits IRule
    Property Sql() As String
    Property QueryType() As ReturnType
    Property Folder() As String
End Interface

Public Interface IDoCompare
    Inherits IRule

    Property UseAsocDoc() As Boolean
    Property IdAsoc() As String
    Property idDocTypeAsoc() As Int64
    Property valueList() As String
    Property valueComp() As String
    Property Comp() As String
    Property valueFilter() As String
    Property variableName() As String
End Interface
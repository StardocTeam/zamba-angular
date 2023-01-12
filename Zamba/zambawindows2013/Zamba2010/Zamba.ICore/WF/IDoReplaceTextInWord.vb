Public Interface IDoReplaceTextInWord
    Inherits IRule

    Property WordPath() As String
    Property ReplaceFields() As String
    Property NewPath() As String
    Property CaseSensitive() As Boolean
    Property SaveOriginalPath As Boolean
End Interface

Public Interface IDoReplaceText
    Inherits IRule

    Property Text() As String
    Property ReplaceFields() As String
    Property SaveTextAs() As String
    Property IsFile() As Boolean

End Interface

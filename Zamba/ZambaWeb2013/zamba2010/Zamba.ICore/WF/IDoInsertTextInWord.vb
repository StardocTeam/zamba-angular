Public Interface IDoInsertTextInWord
    Inherits IRule

    Property WordPath() As String
    Property NewPath() As String
    Property Variable() As String
    Property Section As Int32
    Property FontConfig() As Boolean
    Property Font() As String
    Property FontSize() As Single
    Property Style() As Int32
    Property Color() As String
    Property backColor() As String
    Property textAsTable() As Boolean
    Property SaveOriginalPath() As Boolean

End Interface
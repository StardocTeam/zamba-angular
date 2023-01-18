Public Interface IDOSCREENMESSAGE
    Inherits IRule
    Property Mensaje() As String
    Property Action() As String
    ReadOnly Property NameScreen() As String
    Property HideDocumentName() As Boolean
End Interface

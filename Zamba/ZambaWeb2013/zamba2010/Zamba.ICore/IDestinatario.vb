Public Interface IDestinatario
    Property UserName() As String
    Property UserID() As Int64
    Property Address() As String
    Property Readed() As Boolean
    Property Type() As Zamba.Core.MessageType
    Property User() As IUser
End Interface
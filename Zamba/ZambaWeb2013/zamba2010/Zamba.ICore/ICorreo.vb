Public Interface ICorreo
    Property Type() As MailTypes
    Property Servidor() As String
    Property Base() As String
    Property Mail() As String
    Property UserName() As String
    Property Password() As String
    Property ProveedorSMTP() As String
    Property Puerto() As Int16
    Property EnableSsl() As Boolean
End Interface



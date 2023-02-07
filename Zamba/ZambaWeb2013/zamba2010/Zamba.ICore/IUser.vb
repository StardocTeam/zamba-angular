Public Interface IUser
	Inherits IUserRights

    Property Crdate() As Date
    Property Lmoddate() As Date
    Property Expirationtime() As Integer
    Property AddressBook() As String
    Property Expiredate() As String
    Property Nombres() As String
    Property Apellidos() As String
    Property puesto() As String
    Property telefono() As String
    Property firma() As String
    Property Picture() As String
    Property ConnectionId() As Integer
    Property WFLic() As Boolean
    Property eMail() As iCorreo
    Property Groups() As ArrayList
    Property Description() As String
    Property MailConfigLoaded() As Boolean
    Property ThumbNailPhoto As String
    Property TraceLevel As Integer
End Interface


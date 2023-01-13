Public Interface IDoReadInboxMail
    Inherits IRule
    Property Pop3Server() As String
    Property Pop3Port() As Integer
    Property Pop3User() As String
    Property Pop3Password() As String
    Property Pop3EnableSSL() As Boolean
    Property PathToExport() As String
    Property Zvarname() As String
    Property StartDate() As String
    Property EndDate() As String
End Interface

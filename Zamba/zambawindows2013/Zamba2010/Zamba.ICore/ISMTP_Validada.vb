Public Interface ISMTP_Validada
    Property User() As String
    Property Password() As String
    Property Port() As Integer
    Property Server() As String
    Sub SMTP_Validada(ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String)
End Interface
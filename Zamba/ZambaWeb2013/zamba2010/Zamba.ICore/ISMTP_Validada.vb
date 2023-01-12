Public Interface ISMTP_Validada
    Property User() As String
    Property Password() As String
    Property Port() As Integer
    Property Server() As String
    Property SSL() As Boolean

    Sub SMTP_Validada(ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal ssl As Boolean)
End Interface
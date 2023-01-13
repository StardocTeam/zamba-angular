Public Interface IDoRequestAction
    Inherits IRule
    ReadOnly Property RuleIds() As List(Of Int64)
    ReadOnly Property UserIds() As List(Of Int64)
    Property NotificationMessage() As String
    Property Subject() As String
    Property ServerLocation() As String
    Property LinkMail() As String
End Interface
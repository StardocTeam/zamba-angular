Public Interface IDOMail_v3
    Inherits IRule
    Inherits IDOMail_v2

    Property SaveMailPath() As Boolean
    Property MailPath() As String
    Property DisableHistory() As Boolean
    Property FilterDocID() As String

End Interface
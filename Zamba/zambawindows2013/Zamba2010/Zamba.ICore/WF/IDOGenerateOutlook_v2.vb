Public Interface IDOGenerateOutlook_v2
    Inherits IRule, IDOGenerateOutlook
    Property SaveMailPath() As Boolean
    Property MailPath() As String
End Interface
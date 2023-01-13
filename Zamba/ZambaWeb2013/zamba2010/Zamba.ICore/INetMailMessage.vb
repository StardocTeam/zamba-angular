Public Interface INetMailMessage
    Inherits IMailMessage

    Property Importance() As System.Net.Mail.MailPriority
    Sub AddAttachments(ByVal value As Object)
End Interface
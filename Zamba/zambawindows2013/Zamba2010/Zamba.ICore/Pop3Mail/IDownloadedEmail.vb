
Namespace Pop3Utilities
    Public Interface IDownloadedEmail

        Property MailHeader As IDownloadedEmailHeader

        Property MailBody As IDownloadedEmailBody

        Property ExportFullPath As String

    End Interface
End Namespace

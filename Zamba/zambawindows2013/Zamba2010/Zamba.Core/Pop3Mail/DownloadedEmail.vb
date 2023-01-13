Namespace Pop3Utilities
    Public Class DownloadedEmail
        Implements IDownloadedEmail

        Private m_MailHeader As IDownloadedEmailHeader
        Private m_MailBody As DownloadedEmailBody
        Private m_ExportFullPath As String

        Public Property MailHeader() As IDownloadedEmailHeader Implements IDownloadedEmail.MailHeader
            Get
                Return m_MailHeader
            End Get
            Set(ByVal value As IDownloadedEmailHeader)
                m_MailHeader = value
            End Set
        End Property

        Public Property MailBody() As IDownloadedEmailBody Implements IDownloadedEmail.MailBody
            Get
                Return m_MailBody
            End Get
            Set(ByVal value As IDownloadedEmailBody)
                m_MailBody = value
            End Set
        End Property

        Public Property ExportFullPath() As String Implements IDownloadedEmail.ExportFullPath
            Get
                Return m_ExportFullPath
            End Get
            Set(ByVal value As String)
                m_ExportFullPath = value
            End Set
        End Property
    End Class
End Namespace
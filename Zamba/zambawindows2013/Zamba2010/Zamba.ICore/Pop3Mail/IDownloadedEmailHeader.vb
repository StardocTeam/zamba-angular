
Namespace Pop3Utilities
    Public Interface IDownloadedEmailHeader

        Property Headers As Specialized.NameValueCollection

        Property ContentType As String

        Property UtcDateTime As Date

        Property From As String

        Property [To] As String

        Property Subject As String

        Property EmailId As Integer

    End Interface
End Namespace

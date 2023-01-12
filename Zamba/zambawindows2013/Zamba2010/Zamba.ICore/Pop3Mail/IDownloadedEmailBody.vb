
Namespace Pop3Utilities
    Public Interface IDownloadedEmailBody

        Property Headers As Specialized.NameValueCollection

        Property ContentType As String

        Property MessageText As String

        Property Charset As String

        Property ContentTransferEncoding As String

    End Interface
End Namespace

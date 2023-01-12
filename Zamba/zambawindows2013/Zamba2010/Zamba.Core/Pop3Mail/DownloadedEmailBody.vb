Imports System.Collections.Specialized

Namespace Pop3Utilities
    Public Class DownloadedEmailBody
        Implements IDownloadedEmailBody

        Private m_Headers As NameValueCollection
        Private m_MessageText As String
        Private m_ContentType As String
        Private m_Charset As String
        Private m_ContentTransferEncoding As String

        Public Property Headers() As NameValueCollection Implements IDownloadedEmailBody.Headers
            Get
                Return m_Headers
            End Get
            Set(ByVal value As NameValueCollection)
                m_Headers = value
            End Set
        End Property

        Public Property ContentType() As String Implements IDownloadedEmailBody.ContentType
            Get
                Return m_ContentType
            End Get
            Set(ByVal value As String)
                m_ContentType = value
            End Set
        End Property

        Public Property MessageText() As String Implements IDownloadedEmailBody.MessageText
            Get
                Return m_MessageText
            End Get
            Set(ByVal value As String)
                m_MessageText = value
            End Set
        End Property

        Public Sub New(ByVal headers As NameValueCollection, ByVal messageText As String)
            m_Headers = headers
            m_ContentType = headers("Content-Type")
            m_MessageText = messageText
        End Sub

        Public Property Charset As String Implements IDownloadedEmailBody.Charset
            Get
                Return m_Charset
            End Get
            Set(ByVal value As String)
                m_Charset = value
            End Set
        End Property

        Public Property ContentTransferEncoding As String Implements IDownloadedEmailBody.ContentTransferEncoding
            Get
                Return m_ContentTransferEncoding
            End Get
            Set(ByVal value As String)
                m_ContentTransferEncoding = value
            End Set
        End Property
    End Class
End Namespace

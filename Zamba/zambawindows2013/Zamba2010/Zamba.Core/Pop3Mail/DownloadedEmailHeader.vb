Imports System.Collections.Specialized

Namespace Pop3Utilities
    Public Class DownloadedEmailHeader
        Implements IDownloadedEmailHeader

        Private m_Headers As NameValueCollection
        Private m_ContentType As String
        Private m_UtcDateTime As DateTime
        Private m_From As String
        Private m_To As String
        Private m_Subject As String
        Private m_EmailId As Integer


        Public Property Headers() As NameValueCollection Implements IDownloadedEmailHeader.Headers
            Get
                Return m_Headers
            End Get
            Set(ByVal value As NameValueCollection)
                m_Headers = value
            End Set
        End Property

        Public Property ContentType() As String Implements IDownloadedEmailHeader.ContentType
            Get
                Return m_ContentType
            End Get
            Set(ByVal value As String)
                m_ContentType = value
            End Set
        End Property

        Public Property UtcDateTime() As DateTime Implements IDownloadedEmailHeader.UtcDateTime
            Get
                Return m_UtcDateTime
            End Get
            Set(ByVal value As DateTime)
                m_UtcDateTime = value
            End Set
        End Property

        Public Property From() As String Implements IDownloadedEmailHeader.From
            Get
                Return m_From
            End Get
            Set(ByVal value As String)
                m_From = value
            End Set
        End Property

        Public Property [To]() As String Implements IDownloadedEmailHeader.[To]
            Get
                Return m_To
            End Get
            Set(ByVal value As String)
                m_To = value
            End Set
        End Property

        Public Property Subject() As String Implements IDownloadedEmailHeader.Subject
            Get
                Return m_Subject
            End Get
            Set(ByVal value As String)
                m_Subject = value
            End Set
        End Property

        Public Property EmailId() As Integer Implements IDownloadedEmailHeader.EmailId
            Get
                Return m_EmailId
            End Get
            Set(ByVal value As Integer)
                m_EmailId = value
            End Set
        End Property

        Sub New()
        End Sub

    End Class
End Namespace

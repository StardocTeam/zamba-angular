Imports System.Collections.Generic
Imports System.Net.Mail
Imports Zamba.Core

Public Class SendMailConfig
    Implements ISendMailConfig, IDisposable

#Region "Atributos"
    Private _mailType As MailTypes
    Private _from As String
    Private _server As String
    Private _port As String
    Private _userName As String
    Private _password As String
    Private _to As String
    Private _cc As String
    Private _cco As String
    Private _subject As String
    Private _body As String
    Private _isBodyHtml As Boolean
    Private _userId As Int64
    Private _basemail As String
    Private _originalDoc As Byte()
    Private _originalDocFileName As String
    Private _enableSsl As Boolean
    Private _useWebService As Boolean
    Private _saveHistory As Boolean
    Private _sourceDocID As Long
    Private _sourceDocTypeId As Long
    Private _attachFileNames As List(Of String)
    Private _arrayLinks As ArrayList
    Private _imagesToEmbedPaths As List(Of String)
    Private _attaches As List(Of IBlobDocument)
    Private _isHtml As Boolean
    Private _AttachesZip As System.Net.Mail.Attachment


#End Region

#Region "Properties"
    Public Property ArrayLinks As ArrayList Implements ISendMailConfig.ArrayLinks
        Get
            Return _arrayLinks
        End Get
        Set(value As ArrayList)
            _arrayLinks = value
        End Set
    End Property

    Public Property AttachFileNames As List(Of String) Implements ISendMailConfig.AttachFileNames
        Get
            Return _attachFileNames
        End Get
        Set(value As List(Of String))
            _attachFileNames = value
        End Set
    End Property

    Public Property Basemail As String Implements ISendMailConfig.Basemail
        Get
            Return _basemail
        End Get
        Set(value As String)
            _basemail = value
        End Set
    End Property

    Public Property Body As String Implements ISendMailConfig.Body
        Get
            Return _body
        End Get
        Set(value As String)
            _body = value
        End Set
    End Property

    Public Property Cc As String Implements ISendMailConfig.Cc
        Get
            Return _cc
        End Get
        Set(value As String)
            _cc = value
        End Set
    End Property

    Public Property Cco As String Implements ISendMailConfig.Cco
        Get
            Return _cco
        End Get
        Set(value As String)
            _cco = value
        End Set
    End Property

    Public Property EnableSsl As Boolean Implements ISendMailConfig.EnableSsl
        Get
            Return _enableSsl
        End Get
        Set(value As Boolean)
            _enableSsl = value
        End Set
    End Property

    Public Property From As String Implements ISendMailConfig.From
        Get
            Return _from
        End Get
        Set(value As String)
            _from = value
        End Set
    End Property

    Public Property ImagesToEmbedPaths As List(Of String) Implements ISendMailConfig.ImagesToEmbedPaths
        Get
            Return _imagesToEmbedPaths
        End Get
        Set(value As List(Of String))
            _imagesToEmbedPaths = value
        End Set
    End Property

    Public Property IsBodyHtml As Boolean Implements ISendMailConfig.IsBodyHtml
        Get
            Return _isHtml
        End Get
        Set(value As Boolean)
            _isHtml = value
        End Set
    End Property

    Public Property MailTo As String Implements ISendMailConfig.MailTo
        Get
            Return _to
        End Get
        Set(value As String)
            _to = value
        End Set
    End Property

    Public Property MailType As MailTypes Implements ISendMailConfig.MailType
        Get
            Return _mailType
        End Get
        Set(value As MailTypes)
            _mailType = value
        End Set
    End Property

    Public Property OriginalDocument As Byte() Implements ISendMailConfig.OriginalDocument
        Get
            Return _originalDoc
        End Get
        Set(value As Byte())
            _originalDoc = value
        End Set
    End Property

    Public Property OriginalDocumentFileName As String Implements ISendMailConfig.OriginalDocumentFileName
        Get
            Return _originalDocFileName
        End Get
        Set(value As String)
            _originalDocFileName = value
        End Set
    End Property

    Public Property Password As String Implements ISendMailConfig.SMPTPassword
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
        End Set
    End Property

    Public Property Port As String Implements ISendMailConfig.SMTPPort
        Get
            Return _port
        End Get
        Set(value As String)
            _port = value
        End Set
    End Property

    Public Property SMTPServer As String Implements ISendMailConfig.SMTPServer
        Get
            Return _server
        End Get
        Set(value As String)
            _server = value
        End Set
    End Property

    Public Property Subject As String Implements ISendMailConfig.Subject
        Get
            Return _subject
        End Get
        Set(value As String)
            _subject = value
        End Set
    End Property

    Public Property UserId As Long Implements ISendMailConfig.UserId
        Get
            Return _userId
        End Get
        Set(value As Long)
            _userId = value
        End Set
    End Property

    Public Property UserName As String Implements ISendMailConfig.SMTPUserName
        Get
            Return _userName
        End Get
        Set(value As String)
            _userName = value
        End Set
    End Property

    Public Property UseWebService As Boolean Implements ISendMailConfig.UseWebService
        Get
            Return _useWebService
        End Get
        Set(value As Boolean)
            _useWebService = value
        End Set
    End Property

    Public Property SaveHistory As Boolean Implements ISendMailConfig.SaveHistory
        Get
            Return _saveHistory
        End Get
        Set(value As Boolean)
            _saveHistory = value
        End Set
    End Property

    Public Property SourceDocId As Long Implements ISendMailConfig.SourceDocId
        Get
            Return _sourceDocID
        End Get
        Set(value As Long)
            _sourceDocID = value
        End Set
    End Property

    Public Property SourceDocTypeId As Long Implements ISendMailConfig.SourceDocTypeId
        Get
            Return _sourceDocTypeId
        End Get
        Set(value As Long)
            _sourceDocTypeId = value
        End Set
    End Property

    Public Property Attaches As List(Of IBlobDocument) Implements ISendMailConfig.Attaches
        Get
            Return _attaches
        End Get
        Set(value As List(Of IBlobDocument))
            _attaches = value
        End Set
    End Property


    Public Property ZipAttaches As Attachment Implements ISendMailConfig.AttachesZip
        Get
            Return _AttachesZip
        End Get
        Set(value As Attachment)
            _AttachesZip = value
        End Set
    End Property







    Public Property LinkToZamba As Boolean Implements ISendMailConfig.LinkToZamba
    Public Property Attachments As New List(Of Net.Mail.Attachment) Implements ISendMailConfig.Attachments

    'Private Property ISendMailConfig_AttachesZip As Attachment Implements ISendMailConfig.AttachesZip
    '    Get
    '        Throw New NotImplementedException()
    '    End Get
    '    Set(value As Attachment)
    '        Throw New NotImplementedException()
    '    End Set
    'End Property

#End Region

#Region "Constructores"
    Public Sub New()
    End Sub
#End Region

#Region "Metodos"
    Public Sub LoadMailData(ByVal mailData As IAutoMail)
        With mailData
            _attachFileNames = .AttachmentsPaths
            _body = .Body
            _cc = .CC
            _cco = .CCO
            _from = .From
            _to = .MailTo
            _imagesToEmbedPaths = .PathImages
            _subject = .Subject
            _arrayLinks = ._Attach

            'TODO: verificar que este mapeo sea correcto
            _sourceDocID = .TaskID
            _sourceDocTypeId = .DocTypeID
            '_zipattachs = .AttachmentsPaths
        End With
    End Sub
    Public Sub LoadSmtpData(ByVal smtpData As ISMTP_Validada)
        With smtpData
            _server = .Server
            _port = .Port
            _userName = .User
            _password = .Password
            _enableSsl = .SSL
        End With
    End Sub
    Public Sub LoadAttaches(ByRef attaches As List(Of BlobDocument))
        If attaches IsNot Nothing Then
            For i As Int32 = 0 To attaches.Count - 1
                _attaches.Add(DirectCast(attaches(i), IBlobDocument))
            Next
        End If
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            Try
                If disposing Then
                    If _attachFileNames IsNot Nothing Then _attachFileNames.Clear()
                    If _arrayLinks IsNot Nothing Then _arrayLinks.Clear()
                    If _imagesToEmbedPaths IsNot Nothing Then _imagesToEmbedPaths.Clear()
                    If _attaches IsNot Nothing Then _attaches.Clear()
                    'If _AttachesZip IsNot Nothing Then _AttachesZip.
                End If
            Catch
            Finally
                _from = Nothing
                _server = Nothing
                _port = Nothing
                _userName = Nothing
                _password = Nothing
                _to = Nothing
                _cc = Nothing
                _cco = Nothing
                _subject = Nothing
                _body = Nothing
                _attachFileNames = Nothing
                _arrayLinks = Nothing
                _imagesToEmbedPaths = Nothing
                _basemail = Nothing
                _originalDoc = Nothing
                _originalDocFileName = Nothing
                _attaches = Nothing
                _AttachesZip = Nothing
            End Try
        End If

        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class

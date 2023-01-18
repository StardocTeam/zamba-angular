'Imports Zamba.AppBlockz
'Imports ZAMBA.Servers
Imports System.Web.Mail
Imports MailPriority = System.Net.Mail.MailPriority

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.MessagesControls
''' Class	 : Controls.NetMailMessage
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase para crear objetos Mails que seran enviados por una cuenta smtp previamente configurada
''' </summary>
''' <remarks>
''' Implementa IMessage
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class NetMailMessage
    Implements INetMailMessage

#Region "Atributos"
    Private _msg As New MailMessage
    Private _conf As Boolean
    Private _att As New ArrayList
    Private _smtp As String
#End Region

#Region "Propiedades"
    Public Property MailDe() As Object Implements IMailMessage.De
        Get
            Return _msg.From
        End Get
        Set(ByVal Value As Object)
            Me._msg.From = Value.ToString()
        End Set
    End Property
    Public Property MailTo() As Object Implements IMailMessage.MailTo
        Get
            Return _msg.To
        End Get
        Set(ByVal Value As Object)
            _msg.To = Value.ToString
        End Set
    End Property
    Public Property CC() As Object Implements IMailMessage.CC
        Get
            Return _msg.Cc
        End Get
        Set(ByVal Value As Object)
            _msg.Cc = Value.ToString
        End Set
    End Property
    Public Property CCO() As Object Implements IMailMessage.CCO
        Get
            Return _msg.Bcc
        End Get
        Set(ByVal Value As Object)
            _msg.Bcc = Value.ToString
        End Set
    End Property
    Public Property Body() As Object Implements IMailMessage.Body
        Get
            Return _msg.Body
        End Get
        Set(ByVal Value As Object)
            _msg.Body = Value.ToString
        End Set
    End Property
    Public Property Importance() As MailPriority Implements INetMailMessage.Importance
        Get
            Return _msg.Priority
        End Get
        Set(ByVal Value As MailPriority)
            _msg.Priority = DirectCast(Value, System.Web.Mail.MailPriority)
        End Set
    End Property
    Public Property Subject() As Object Implements IMailMessage.Subject
        Get
            Return _msg.Subject
        End Get
        Set(ByVal Value As Object)
            _msg.Subject = Value.ToString
        End Set
    End Property
    Public Property Attachs() As Object Implements IMailMessage.Attachs
        Get
            Return _att
        End Get
        Set(ByVal Value As Object)
            _att = DirectCast(Value, ArrayList)
        End Set
    End Property
    Public Property Confirmation() As Boolean Implements IMailMessage.Confirmation
        Get
            Return _conf
        End Get
        Set(ByVal Value As Boolean)
            _conf = Value
        End Set
    End Property
#End Region

#Region "Constructores"
    Private Sub New()
    End Sub
    Public Sub New(ByVal SMTP As String)
        _smtp = SMTP
    End Sub
#End Region

    Public Sub AddAttachments(ByVal value As Object) Implements INetMailMessage.AddAttachments
        _att.Add(value)
    End Sub
    Public Sub send() Implements IMailMessage.send
        SmtpMail.SmtpServer = _smtp
        SmtpMail.Send(_msg)
    End Sub

End Class
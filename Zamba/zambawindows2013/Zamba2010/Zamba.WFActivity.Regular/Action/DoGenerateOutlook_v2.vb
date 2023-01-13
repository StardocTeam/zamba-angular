Imports Zamba.Core

<RuleCategory("Mensajes"), RuleDescription("Generar e-mail de Outlook"), RuleApproved("True"), RuleHelp("Permite generar un mail de outlook para luego ser enviado."), RuleFeatures(True)> <Serializable()> _
Public Class DoGenerateOutlook_v2
    Inherits WFRuleParent
    Implements IDOGenerateOutlook_v2

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoGenerateOutlook_v2

    Private _Para As String
    Private _CC As String
    Private _CCO As String
    Private _Asunto As String
    Private _Body As String
    Private _AttachLink As Boolean

    Private _Senddocument As Boolean
    Private _imagesNames As String

    Private _pathImages As String
    Private _sendTimeOut As Integer

    Private _automaticSend As Boolean
    Private _replyMail As Boolean
    Private _replyMsgPath As String

    Private _saveMailPath As Boolean

    Private _mailPath As String

    Public Overrides Sub Dispose()
    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return _isFull
        End Get
    End Property
    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Para As String, ByVal CC As String, ByVal CCO As String, ByVal Asunto As String, ByVal Senddocument As Boolean, ByVal Body As String, ByVal imagNames As String, ByVal pathImag As String, ByVal attachLink As Boolean, ByVal _sendTimeOut As Int32, ByVal automaticSend As Boolean, ByVal replyMail As Boolean, ByVal replayPath As String, ByVal savemailpath As Boolean, ByVal mailpath As String)
        MyBase.New(Id, Name, wfstepId)
        Me._Para = Para
        Me._CC = CC
        Me._CCO = CCO
        Me._Asunto = Asunto
        Me._Senddocument = Senddocument
        Me._Body = Body
        Me._imagesNames = imagNames
        Me._pathImages = pathImag

        Me._AttachLink = attachLink
        Me.sendTimeOut = _sendTimeOut

        Me._automaticSend = automaticSend
        Me._replyMail = replyMail
        Me._replyMsgPath = replayPath

        Me._saveMailPath = savemailpath
        Me._mailPath = mailpath

        Me.playRule = New Zamba.WFExecution.PlayDoGenerateOutlook_v2(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Property Asunto() As String Implements Core.IDOGenerateOutlook.Asunto
        Get
            Return _Asunto
        End Get
        Set(ByVal value As String)
            _Asunto = value
        End Set
    End Property


    Public Property AttachLink() As Boolean Implements Core.IDOGenerateOutlook.AttachLink
        Get
            Return _AttachLink
        End Get
        Set(ByVal value As Boolean)
            _AttachLink = value
        End Set
    End Property


    Public Property Body() As String Implements Core.IDOGenerateOutlook.Body
        Get
            Return _Body
        End Get
        Set(ByVal value As String)
            _Body = value
        End Set
    End Property

    Public Property CC() As String Implements Core.IDOGenerateOutlook.CC
        Get
            Return _CC
        End Get
        Set(ByVal value As String)
            _CC = value
        End Set
    End Property

    Public Property CCO() As String Implements Core.IDOGenerateOutlook.CCO
        Get
            Return _CCO
        End Get
        Set(ByVal value As String)
            _CCO = value
        End Set
    End Property

    Public Property ImagesNames() As String Implements Core.IDOGenerateOutlook.ImagesNames
        Get
            Return _imagesNames
        End Get
        Set(ByVal value As String)
            _imagesNames = value
        End Set
    End Property

    Public Property Para() As String Implements Core.IDOGenerateOutlook.Para
        Get
            Return _Para
        End Get
        Set(ByVal value As String)
            _Para = value
        End Set
    End Property

    Public Property PathImages() As String Implements Core.IDOGenerateOutlook.PathImages
        Get
            Return _pathImages
        End Get
        Set(ByVal value As String)
            _pathImages = value
        End Set
    End Property

    Public Property SendDocument() As Boolean Implements Core.IDOGenerateOutlook.SendDocument
        Get
            Return _Senddocument
        End Get
        Set(ByVal value As Boolean)
            _Senddocument = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName() As String
        Get
            If _Para Is Nothing Then
                Return "Envio Mail"
            Else
                Return "Envio Mail a " & _Para
            End If
        End Get
    End Property

    Public Property sendTimeOut() As Integer Implements IDOGenerateOutlook.sendTimeOut
        Get
            Return _sendTimeOut
        End Get
        Set(ByVal value As Integer)
            _sendTimeOut = value
        End Set
    End Property

    Public Property automaticSend() As Boolean Implements IDOGenerateOutlook.automaticSend
        Get
            Return _automaticSend
        End Get
        Set(ByVal value As Boolean)
            _automaticSend = value
        End Set
    End Property

    Public Property ReplyMail() As Boolean Implements IDOGenerateOutlook.ReplyMail
        Get
            Return _replyMail
        End Get
        Set(ByVal value As Boolean)
            _replyMail = value
        End Set
    End Property

    Public Property ReplyMsgPath() As String Implements Core.IDOGenerateOutlook.ReplyMsgPath
        Get
            Return _replyMsgPath
        End Get
        Set(ByVal value As String)
            _replyMsgPath = value
        End Set
    End Property

    Public Property SaveMailPath() As Boolean Implements IDOGenerateOutlook_v2.SaveMailPath
        Get
            Return _saveMailPath
        End Get
        Set (ByVal value As Boolean)
            _saveMailPath = value
        End Set
    End Property

    Public Property MailPath() As String Implements IDOGenerateOutlook_v2.MailPath
        Get
            Return _mailPath
        End Get
        Set (ByVal value As String)
            _mailPath = value
        End Set
    End Property

End Class

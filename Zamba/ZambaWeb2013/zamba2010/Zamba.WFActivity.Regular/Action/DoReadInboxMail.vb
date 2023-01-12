Imports Zamba.Core

<RuleMainCategory("Mensajes, Mails y Foro"), RuleCategory("Mail"), RuleSubCategory(""), RuleDescription("Obtener mails de casilla de correo"), RuleHelp("Obtiene los mails de una bandeja de entrada de una cuenta POP3"), RuleFeatures(True)> _
<Serializable()> Public Class DoReadInboxMail
    Inherits WFRuleParent
    Implements IDoReadInboxMail

#Region "Members"
    Private _pop3Server As String
    Private _pop3Port As Integer
    Private _pop3User As String
    Private _pop3Password As String
    Private _pop3EnableSSL As Boolean
    Private _pathToExport As String
    Private _zvarname As String
    Private _startDate As String
    Private _endDate As String

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoReadInboxMail
    Private _isRemote As Boolean
#End Region

#Region "IDoReadInboxMail Properties"
    Public Property EndDate As String Implements Core.IDoReadInboxMail.EndDate
        Get
            Return _endDate
        End Get
        Set(ByVal value As String)
            _endDate = value
        End Set
    End Property

    Public Property PathToExport As String Implements Core.IDoReadInboxMail.PathToExport
        Get
            Return _pathToExport
        End Get
        Set(ByVal value As String)
            _pathToExport = value
        End Set
    End Property

    Public Property Pop3EnableSSL As Boolean Implements Core.IDoReadInboxMail.Pop3EnableSSL
        Get
            Return _pop3EnableSSL
        End Get
        Set(ByVal value As Boolean)
            _pop3EnableSSL = value
        End Set
    End Property

    Public Property Pop3Password As String Implements Core.IDoReadInboxMail.Pop3Password
        Get
            Return _pop3Password
        End Get
        Set(ByVal value As String)
            _pop3Password = value
        End Set
    End Property

    Public Property Pop3Port As Integer Implements Core.IDoReadInboxMail.Pop3Port
        Get
            Return _pop3Port
        End Get
        Set(ByVal value As Integer)
            _pop3Port = value
        End Set
    End Property

    Public Property Pop3Server As String Implements Core.IDoReadInboxMail.Pop3Server
        Get
            Return _pop3Server
        End Get
        Set(ByVal value As String)
            _pop3Server = value
        End Set
    End Property

    Public Property Pop3User As String Implements Core.IDoReadInboxMail.Pop3User
        Get
            Return _pop3User
        End Get
        Set(ByVal value As String)
            _pop3User = value
        End Set
    End Property

    Public Property StartDate As String Implements Core.IDoReadInboxMail.StartDate
        Get
            Return _startDate
        End Get
        Set(ByVal value As String)
            _startDate = value
        End Set
    End Property

    Public Property Zvarname As String Implements Core.IDoReadInboxMail.Zvarname
        Get
            Return _zvarname
        End Get
        Set(ByVal value As String)
            _zvarname = value
        End Set
    End Property
#End Region

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides Sub Load()

    End Sub


    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Pop3Server As String _
                                , ByVal Pop3Port As Integer _
                                , ByVal Pop3User As String _
                                , ByVal Pop3Password As String _
                                , ByVal Pop3EnableSSL As Boolean _
                                , ByVal PathToExport As String _
                                , ByVal Zvarname As String _
                                , ByVal StartDate As String _
                                , ByVal EndDate As String)
        MyBase.New(Id, Name, wfstepid)

        Me.Pop3Server = Pop3Server
        Me.Pop3Port = Pop3Port
        Me.Pop3User = Pop3User
        Me.Pop3Password = Pop3Password
        Me.Pop3EnableSSL = Pop3EnableSSL
        Me.PathToExport = PathToExport
        Me.Zvarname = Zvarname
        Me.StartDate = StartDate
        Me.EndDate = EndDate

        playRule = New Zamba.WFExecution.PlayDoReadInboxMail(Me)
    End Sub
End Class

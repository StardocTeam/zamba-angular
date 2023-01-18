Imports System
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization
Imports Zamba.Membership


<RuleCategory("Mensajes"), RuleDescription("Automail"), RuleHelp("Permite enviar un mail, preconfigurado o definido por el usuario, de forma automática al ejecutar la regla "), RuleFeatures(False)> <Serializable()> _
Public Class DOAutoMail
    Inherits WFRuleParent
    Implements IDOAutoMail
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOAutoMail
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
    Public Overrides Sub Dispose()

    End Sub
    Private _automail As AutoMail
    Private _addDocument As Boolean
    Private _addLink As Boolean
    Private _addIndexs As Boolean
    Private _groupMailTo As Boolean
    Private _indexsnames As New List(Of String)

#Region "Propiedades"
    Public Property Automail() As IAutoMail Implements IDOAutoMail.Automail
        Get
            Return _automail
        End Get
        Set(ByVal value As IAutoMail)
            _automail = value
        End Set
    End Property
    Public Property smtp() As ISMTP_Validada Implements IDOAutoMail.smtp
        Get
            Dim SmtpConfig As New SMTP_Validada()
            SmtpConfig.User = Zamba.Membership.MembershipHelper.CurrentUser.eMail.UserName
            SmtpConfig.Password = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Password
            SmtpConfig.Server = Zamba.Membership.MembershipHelper.CurrentUser.eMail.ProveedorSMTP
            SmtpConfig.Port = Zamba.Membership.MembershipHelper.CurrentUser.eMail.Puerto

            Return (SmtpConfig)
        End Get
        Set(ByVal value As ISMTP_Validada)
        End Set
    End Property
    Public Property AddDocument() As Boolean Implements IDOAutoMail.AddDocument
        Get
            Return (_addDocument)
        End Get
        Set(ByVal value As Boolean)
            _addDocument = value
        End Set
    End Property

    Public Property AddLink() As Boolean Implements IDOAutoMail.AddLink
        Get
            Return _addLink
        End Get
        Set(ByVal value As Boolean)
            _addLink = value
        End Set
    End Property
    Public Property groupMailTo() As Boolean Implements IDOAutoMail.groupMailTo
        Get
            Return _groupMailTo
        End Get
        Set(ByVal value As Boolean)
            _groupMailTo = value
        End Set
    End Property
    Public Property AddIndexs() As Boolean Implements IDOAutoMail.AddIndexs
        Get
            Return _addIndexs
        End Get
        Set(ByVal value As Boolean)
            _addIndexs = value
        End Set
    End Property
    Public Property IndexNames() As List(Of String) Implements IDOAutoMail.IndexNames
        Get
            Return _indexsnames
        End Get
        Set(ByVal value As List(Of String))
            _indexsnames = value
        End Set
    End Property
#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal IdAutoMail As Int32, ByVal AddDocument As Boolean, ByVal AddLink As Boolean, ByVal AddIndexs As Boolean, ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal indexsNames As String, ByVal groupByMailto As Boolean)
        MyBase.New(Id, Name, Wfstepid)
        If IdAutoMail > 0 Then
            _automail = GetAutomailById(IdAutoMail)
        End If
        _addDocument = AddDocument
        _addLink = AddLink
        _addIndexs = AddIndexs
        If indexsNames <> "" Then
            For Each index As String In indexsNames.Split("|")
                _indexsnames.Add(index)
            Next
        End If

        groupMailTo = groupByMailto
        Me.playRule = New Zamba.WFExecution.PlayDOAutoMail(Me)
    End Sub
    '--ITEMS--
    'IdAutoMail=0
    'AddDocument=1
    'AddLink=2
    'AddIndexs=3
    'Properties
    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class
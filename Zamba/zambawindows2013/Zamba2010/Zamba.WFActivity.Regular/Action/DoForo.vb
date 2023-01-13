Imports Zamba.Core

<RuleMainCategory("Mensajes, Mails y Foro"), RuleCategory("Foro"), RuleSubCategory(""), RuleDescription("Crear Mensaje en Foro"), RuleHelp("Crea un nuevo mensaje en el foro de la tarea."), RuleFeatures(True)> <Serializable()> _
Public Class DoForo
    Inherits WFRuleParent
    Implements IDoForo, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _body As String
    Private _subject As String
    Private _idMensaje As String
    Private _participantes As String
    Private _automatic As Boolean

    Private _ruleID As Int64
    Private _btnname As String
    Private _executeruleid As Int64
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoForo

    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepid As Int64, _
     ByVal _subject As String, ByVal _body As String, ByVal _idMensaje As String, _
     ByVal _participantes As String, ByVal _automatic As Boolean, _
     ByVal ExecuteRuleID As Int64, ByVal BtnName As String)

        MyBase.New(ruleID, ruleName, wfstepid)
        Subject = _subject
        Body = _body
        IdMensaje = _idMensaje
        Participantes = _participantes
        Me._automatic = _automatic

        _executeruleid = ExecuteRuleID
        _btnname = BtnName

        playRule = New WFExecution.PlayDoForo(Me)
    End Sub

    Public Property Body() As String Implements Core.IDoForo.Body
        Get
            Return _body
        End Get
        Set(ByVal value As String)
            _body = value
        End Set
    End Property

    Public Property IdMensaje() As String Implements Core.IDoForo.IdMensaje
        Get
            Return _idMensaje
        End Get
        Set(ByVal value As String)
            _idMensaje = value
        End Set
    End Property
    Public Property Subject() As String Implements Core.IDoForo.Subject
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
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

    Public Property Automatic() As Boolean Implements Core.IDoForo.Automatic
        Get
            Return _automatic
        End Get
        Set(ByVal value As Boolean)
            _automatic = value
        End Set
    End Property

    Public Property Participantes() As String Implements Core.IDoForo.Participantes
        Get
            Return _participantes
        End Get
        Set(ByVal value As String)
            _participantes = value
        End Set
    End Property

    '[pablo] 07/03/2012 - Se agrega funcionalidad de ejecucion de regla
    Public Property BtnName() As String Implements Core.IDoForo.BtnName
        Get
            Return _btnname
        End Get
        Set(ByVal value As String)
            _btnname = value
        End Set
    End Property
    Public Property ExecuteRuleID() As Int64 Implements Core.IDoForo.ExecuteRuleID
        Get
            Return _executeruleid
        End Get
        Set(ByVal value As Int64)
            _executeruleid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class

Imports Zamba.Core

<RuleMainCategory("Mensajes, Mails y Foro"), RuleCategory("Mensajes"), RuleSubCategory(""), RuleDescription("Mostrar Mensaje"), RuleHelp("Permite generar un mensaje para ser mostrado en pantalla al momento de ejecutar la regla"), RuleFeatures(True)> <Serializable()> _
Public Class DOSCREENMESSAGE
    Inherits WFRuleParent
    Implements IDOSCREENMESSAGE, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private mScreenName As String
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDOScreenMessage
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByRef wfstepId As Int64, ByVal Mensaje As String, ByVal HideDocumentName As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _Mensaje = Mensaje
        _HideDocumentName = HideDocumentName
        playRule = New Zamba.WFExecution.PlayDOScreenMessage(Me)
    End Sub
    Public ReadOnly Property NameScreen() As String Implements IDOSCREENMESSAGE.NameScreen
        Get
            Return mScreenName
        End Get
    End Property
    Public Property Mensaje() As String Implements IDOSCREENMESSAGE.Mensaje
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
        End Set
    End Property
    Private _Mensaje As String

    Public Property HideDocumentName() As Boolean Implements IDOSCREENMESSAGE.HideDocumentName
        Get
            Return _HideDocumentName
        End Get
        Set(ByVal value As Boolean)
            _HideDocumentName = value
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Private _HideDocumentName As Boolean

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
End Class
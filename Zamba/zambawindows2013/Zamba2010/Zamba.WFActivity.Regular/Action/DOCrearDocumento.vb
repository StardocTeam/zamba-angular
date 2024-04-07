Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Administrar"), RuleDescription("Crear Documento de texto"), RuleHelp("Crea un  nuevo documento de texto."), RuleFeatures(True)> <Serializable()> _
Public Class DOCrearDocumento
    Inherits WFRuleParent
    Implements IDOCrearDocumento, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _text As String
    Private _path As String
    Private playRule As PlayDOCrearDocumento
    Private _isValid As Boolean

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
    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal text As String, ByVal path As String)
        MyBase.New(Id, Name, wfstepid)

        playRule = New PlayDOCrearDocumento(Me)
        _text = text
        _path = path
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results, Me)
    End Function

    Public Property path As String Implements Core.IDOCrearDocumento.path
        Get
            Return _path
        End Get
        Set(value As String)
            _path = value
        End Set
    End Property

    Public Property text As String Implements Core.IDOCrearDocumento.text
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Crea un Documento"
        End Get
    End Property
End Class


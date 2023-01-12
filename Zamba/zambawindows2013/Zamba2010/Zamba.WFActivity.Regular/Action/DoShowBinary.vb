Imports Zamba.Core

''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo DO
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Exportar"), RuleDescription("Mostrar un binario sin temporal"), RuleHelp("Muestra un archivo de origen binario sin generar un temporal para ello"), RuleFeatures(False)> <Serializable()> _
Public Class DoShowBinary
    Inherits WFRuleParent
    Implements IDoShowBinary, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _binaryFile As String
    Private _mimeType As String
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoShowBinary

    Public Property BinaryFile As String Implements Core.IDoShowBinary.BinaryFile
        Get
            Return _binaryFile
        End Get
        Set(value As String)
            _binaryFile = value
        End Set
    End Property

    Public Property MimeType As String Implements Core.IDoShowBinary.MimeType
        Get
            Return _mimeType
        End Get
        Set(value As String)
            _mimeType = value
        End Set
    End Property

    Public Overrides Sub Dispose()
        _binaryFile = Nothing
        _mimeType = Nothing
    End Sub

    Public Overrides Sub FullLoad()

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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal binaryFile As String, ByVal mimeType As String)
        MyBase.New(Id, Name, wfstepId)
        _binaryFile = binaryFile
        _mimeType = mimeType

        playRule = New WFExecution.PlayDoShowBinary(Me)
    End Sub

End Class

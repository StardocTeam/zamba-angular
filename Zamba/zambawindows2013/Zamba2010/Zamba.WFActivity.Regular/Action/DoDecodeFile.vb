Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Administrar"), RuleDescription("Decodificar Archivo"), RuleHelp("Permite decodificar un archivo en base64."), RuleFeatures(False)> <Serializable()> _
Public Class DoDecodeFile
    Inherits WFRuleParent
    Implements IDoDecodeFile, IRuleValidate

#Region "Atributos"

    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Private _binary As String
    Private _path As String
    Private _fname As String
    Private _varpath As String
    Private _textstart As String
    Private _textend As String
    Private _extfile As String
    Private playRule As WFExecution.PlayDoDecodeFile
    Private _isValid As Boolean
#End Region

#Region "Constructor"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal binary As String, ByVal path As String, ByVal fname As String, ByVal varpath As String, ByVal textstart As String, ByVal textend As String, ByVal extfile As String)
        MyBase.New(Id, Name, wfstepid)
        _binary = binary
        _path = path
        _fname = fname
        _varpath = varpath
        _textstart = textstart
        _textend = textend
        _extfile = extfile
        playRule = New WFExecution.PlayDoDecodeFile(Me)
    End Sub
#End Region


    Public Overrides Sub Dispose()

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

    Public Property binary() As String Implements Core.IDoDecodeFile.binary
        Get
            Return _binary
        End Get
        Set(ByVal value As String)
            _binary = value
        End Set
    End Property

    Public Property fname() As String Implements Core.IDoDecodeFile.fname
        Get
            Return _fname
        End Get
        Set(ByVal value As String)
            _fname = value
        End Set
    End Property

    Public Property path() As String Implements Core.IDoDecodeFile.path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property

    Public Property varpath() As String Implements Core.IDoDecodeFile.varpath
        Get
            Return _varpath
        End Get
        Set(ByVal value As String)
            _varpath = value
        End Set
    End Property

    Public Property textstart() As String Implements Core.IDoDecodeFile.textstart
        Get
            Return _textstart
        End Get
        Set(ByVal value As String)
            _textstart = value
        End Set
    End Property

    Public Property textend() As String Implements Core.IDoDecodeFile.textend
        Get
            Return _textend
        End Get
        Set(ByVal value As String)
            _textend = value
        End Set
    End Property

    Public Property extfile() As String Implements Core.IDoDecodeFile.extfile
        Get
            Return _extfile
        End Get
        Set(ByVal value As String)
            _extfile = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class

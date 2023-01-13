Imports Zamba.Core

<RuleMainCategory("Archivos"), RuleCategory("Administrar"), RuleSubCategory(""), RuleDescription("Comprimir fichero"), RuleHelp("Comprime un fichero"), RuleFeatures(True)> <Serializable()>
Public Class DoUnZip
    Inherits WFRuleParent
    Implements IDoUnZip, IRuleValidate
    Private _isValid As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _files As String
    Private _nameVar As String
    Private _nameNewFile As String
    Private playRule As Zamba.WFExecution.PlayDoUnZip
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal archivos As String, ByVal nombreVar As String, ByVal nombreFile As String)
        MyBase.New(Id, Name, wfstepid)
        files = archivos
        nameVar = nombreVar
        nameNewFile = nombreFile
        playRule = New Zamba.WFExecution.PlayDoUnZip(Me)
    End Sub

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property
    Public Overrides ReadOnly Property IsFull As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Comprimir fichero."
        End Get
    End Property

    Public Property files As String Implements IDoUnZip.files
        Get
            Return _files
        End Get
        Set(value As String)
            _files = value
        End Set
    End Property

    Public Property nameVar As String Implements IDoUnZip.nameVar
        Get
            Return _nameVar
        End Get
        Set(value As String)
            _nameVar = value
        End Set
    End Property

    Public Property nameNewFile As String Implements IDoUnZip.nameNewFile
        Get
            Return _nameNewFile
        End Get
        Set(value As String)
            _nameNewFile = value
        End Set
    End Property

    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

    Public Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides Function Play(results As List(Of ITaskResult), refreshTasks As List(Of Long)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function
End Class

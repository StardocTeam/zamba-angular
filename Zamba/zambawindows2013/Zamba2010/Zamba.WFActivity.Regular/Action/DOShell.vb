Imports Zamba.Core
<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Windows"), RuleDescription("Ejecutar Aplicacion o Comando"), RuleHelp("Permite ejecutar una aplicacion o comando"), RuleFeatures(False)> <Serializable()> _
Public Class DOShell
    Inherits WFRuleParent
    Implements IDOSHELL, IRuleValidate
    Public Overrides Sub Dispose()
        'Se comenta porque en algun lado esta pasado por referencia
        'Me.playRule = Nothing
    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDOShell
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal filepath As String, ByVal Parameter As String, ByVal UseProcess As Boolean)
        MyBase.New(Id, Name, wfstepid)
        _Parameter = Parameter
        _filepath = filepath
        _UseProcess = UseProcess
        playRule = New Zamba.WFExecution.PlayDOShell(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
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

    Private _filepath As String
    Public Property FilePath() As String Implements IDOSHELL.Filepath
        Get
            Return _filepath
        End Get
        Set(ByVal value As String)
            _filepath = value
        End Set
    End Property

    Private _Parameter As String
    Public Property Parameter() As String Implements IDOSHELL.Parameter
        Get
            Return _Parameter
        End Get
        Set(ByVal value As String)
            _Parameter = value
        End Set
    End Property

    Private _UseProcess As Boolean
    Public Property UseProcess() As Boolean Implements IDOSHELL.UseProcess
        Get
            Return _UseProcess
        End Get
        Set(ByVal value As Boolean)
            _UseProcess = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
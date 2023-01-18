Imports Zamba.Core
''' <summary>
''' Regla que actualiza el árbol de procesos, etapas y conteo de reglas.
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Workflow"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Refrescar el árbol de procesos y etapas"), RuleHelp("Regla que refresca el árbol de procesos y etapas junto la cuenta de tareas"), RuleFeatures(False)> <Serializable()> _
Public Class DoRefreshWfTree
    Inherits WFRuleParent
    Implements IDoRefreshWfTree, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoRefreshWfTree

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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64)
        MyBase.New(Id, Name, wfstepId)
        playRule = New Zamba.WFExecution.PlayDoRefreshWfTree(Me)
    End Sub
End Class

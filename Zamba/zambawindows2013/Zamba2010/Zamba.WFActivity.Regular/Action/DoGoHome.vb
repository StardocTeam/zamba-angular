Imports Zamba.Core


<RuleMainCategory("Zamba"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Dirigirse al Home"), RuleHelp("Permite redirigir a la pantalla Home de la aplicacion "), RuleFeatures(False)> <Serializable()>
Public Class DoGoHome
    Inherits WFRuleParent
    Implements IDoGoHome

    Private playRule As WFExecution.PlayDoGoHome
    Private _isFull As Boolean
    Private _isLoaded As Boolean

    Public Sub New(id As Long, name As String, wfStepId As Long)
        MyBase.New(id, name, wfStepId)
        playRule = New WFExecution.PlayDoGoHome(Me)
    End Sub

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Dirigirse al Home"
        End Get
    End Property

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

    Public Overrides Function Play(results As List(Of ITaskResult), refreshTasks As List(Of Long)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

    Public Overrides Sub Dispose()
    End Sub

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Overrides Function PlayTest() As Boolean
    End Function
End Class

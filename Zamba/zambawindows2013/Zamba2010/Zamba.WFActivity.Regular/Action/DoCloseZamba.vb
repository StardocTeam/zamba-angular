Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Cierra Zamba"), RuleHelp("Regla que cierra Zamba."), RuleFeatures(True)> <Serializable()>
Public Class DoCloseZamba
    Inherits WFRuleParent
    Implements IDoCloseZamba, IRuleValidate

#Region "Atributos"
    Private playRule As Zamba.WFExecution.PlayDoCloseZamba
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _isValid As Boolean
#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64)
        MyBase.New(Id, Name, wfstepId)
        playRule = New WFExecution.PlayDoCloseZamba(Me)
    End Sub


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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Cierro Zamba"
        End Get
    End Property
End Class
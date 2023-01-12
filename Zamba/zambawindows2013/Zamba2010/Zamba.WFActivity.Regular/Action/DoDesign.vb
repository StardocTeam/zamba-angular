Imports Zamba.Core
''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo DO
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Diseño"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Diseño de regla"), RuleHelp("Regla de diseño vacia, contiene solo una descripción de la misma"), RuleIconId("38"), RuleFeatures(False)> <Serializable()> _
Public Class DoDesign
    Inherits WFRuleParent
    Implements IDoDesign, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _help As String
    Private playRule As Zamba.WFExecution.PlayDoDesign
    Private _isValid As Boolean

    Public Overrides Sub Dispose()
        _help = String.Empty
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


        Return playRule.Play(results, Me)
    End Function
    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Diseno de Proceso"
        End Get
    End Property
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Property Help() As String Implements Core.IDoDesign.Help
        Get
            Return _help
        End Get
        Set(ByVal value As String)
            _help = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal helpText As String)
        MyBase.New(Id, Name, wfstepId)
        _help = helpText
        playRule = New Zamba.WFExecution.PlayDoDesign(Me)
    End Sub
End Class

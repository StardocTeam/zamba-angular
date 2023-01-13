Imports Zamba.Core

''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo IF
''' </summary>
''' <remarks></remarks>
<RuleMainCategory("Diseño"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Diseño de regla si/no"), RuleHelp("Regla de diseño vacia, contiene solo una descripción de la misma"), RuleFeatures(False)> <Serializable()> _
Public Class IfDesign
    Inherits WFRuleParent
    Implements IIfDesign, IRuleIFPlay


    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _help As String
    Private playRule As Zamba.WFExecution.PlayIfDesign

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


    Public Property Help() As String Implements Core.IIfDesign.Help
        Get
            Return _help
        End Get
        Set(ByVal value As String)
            _help = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal helpText As String)
        MyBase.New(Id, Name, wfstepId)
        _help = helpText
        playRule = New WFExecution.PlayIfDesign(Me)
    End Sub

    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult) Implements Core.IRuleIFPlay.PlayIf
        Return playRule.Play(results, ifType)
    End Function
End Class

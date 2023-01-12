Imports Zamba.Core
''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo DO
''' </summary>
''' <remarks></remarks>
<RuleCategory("Diseño"), RuleDescription("Proceso/Diseño"), RuleHelp("Regla de diseño vacia, contiene solo una descripción de la misma, sirve para agrupar las reglas"), RuleFeatures(False), RuleIconId("31")> <Serializable()> _
Public Class DoDesign
    Inherits WFRuleParent
    Implements IDoDesign

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _help As String

    Public Overrides Sub Dispose()
        Me._help = String.Empty
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return Me._isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return Me._isLoaded
        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoDesign()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoDesign()
        Return playRule.Play(results, Me)
    End Function

    Public Property Help() As String Implements Core.IDoDesign.Help
        Get
            Return Me._help
        End Get
        Set(ByVal value As String)
            Me._help = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal helpText As String)
        MyBase.New(Id, Name, wfstepId)
        Me._help = helpText
    End Sub
End Class

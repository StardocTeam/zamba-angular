Imports Zamba.Core

''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo IF
''' </summary>
''' <remarks></remarks>
<RuleCategory("Diseño"), RuleDescription("Diseño de regla si/no"), RuleHelp("Regla de diseño vacia, contiene solo una descripción de la misma"), RuleFeatures(False)> <Serializable()> _
Public Class IfDesign
    Inherits WFRuleParent
    Implements IIfDesign, IRuleIFPlay


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
        Dim playRule As New Zamba.WFExecution.PlayIfDesign()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDesign()
        Return playRule.Play(results, Me)
    End Function

    Public Property Help() As String Implements Core.IIfDesign.Help
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

    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of Core.ITaskResult) Implements Core.IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfDesign()
        Return playRule.Play(results, Me, ifType)
    End Function
End Class

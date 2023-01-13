Imports Zamba.Core
''' <summary>
''' Regla que actualiza el árbol de procesos, etapas y conteo de reglas.
''' </summary>
''' <remarks></remarks>
<RuleCategory("Workflow"), RuleDescription("Refrescar el árbol de procesos y etapas"), RuleHelp("Regla que refresca el árbol de procesos y etapas junto la cuenta de tareas"), RuleFeatures(False)> <Serializable()> _
Public Class DoRefreshWfTree
    Inherits WFRuleParent
    Implements IDoRefreshWfTree

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoRefreshWfTree

    Public Overrides Sub Dispose()

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
        Return Me.playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            Params.Add("RefrescarArbol", True)
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.RefreshWfTree
            Return results
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64)
        MyBase.New(Id, Name, wfstepId)
        playRule = New Zamba.WFExecution.PlayDoRefreshWfTree(Me)
    End Sub
End Class

Imports Zamba.Core

<RuleCategory("Tareas"), RuleDescription("Refrescar tarea"), RuleHelp("Permite refrescar una tarea, por ID o la actual en ejecuión"), RuleFeatures(True)> <Serializable()> _
Public Class DoRefreshTask
    Inherits WFRuleParent
    Implements IDORefreshTask

    Private _isLoaded As Boolean
    Private _isFull As Boolean
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
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Private _refreshActual As Boolean
    Private _taskId As String
    Private playRule As Zamba.WFExecution.PlayDoRefreshTask

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal RefreshActual As Boolean, ByVal TaskId As String)
        MyBase.New(Id, Name, WFStepid)
        Me._refreshActual = RefreshActual
        Me._taskId = TaskId
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    '[Javier] 08/07/11 - Agregado el método.
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        playRule = New Zamba.WFExecution.PlayDoRefreshTask(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.RuleRefreshTask
            Return playRule.PlayWeb(results, Params, Me)
        Else
            Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return playRule.PlayWebSecondExecution(results, Params, Me)
        End If
    End Function

    Property RefreshActual() As Boolean Implements IDORefreshTask.RefreshActual
        Get
            Return _refreshActual
        End Get
        Set(ByVal value As Boolean)
            _refreshActual = value
        End Set
    End Property

    Property TaskId() As String Implements IDORefreshTask.TaskId
        Get
            Return _taskId
        End Get
        Set(ByVal value As String)
            _taskId = value
        End Set
    End Property

End Class

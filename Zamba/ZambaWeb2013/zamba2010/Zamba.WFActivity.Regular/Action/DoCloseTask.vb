Imports Zamba.Core

''' <summary>
''' Regla que permite cerrar tareas.
''' </summary>
''' <remarks></remarks>
<RuleCategory("Tareas"), RuleDescription("Cerrar tarea"), RuleHelp("Permite cerrar tareas pasandole el TaskId"), RuleFeatures(False)> <Serializable()> _
Public Class DoCloseTask
    Inherits WFRuleParent
    Implements IDoCloseTask



    Public Overrides Sub Dispose()
        Me.TaskId = Nothing
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean


    Public Overrides ReadOnly Property IsLoaded() As Boolean
    Property ParentAction() As Int32 Implements IDoCloseTask.ParentAction

    Public Overrides Sub Load()

    End Sub

    Private playRule As Zamba.WFExecution.PlayDoCloseTask
    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results, Me)
    End Function
    '[Javier] 08/07/11 - Agregado el método.
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.CloseTask
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal taskId As String)
        MyBase.New(Id, Name, wfstepId)
        Me.TaskId = taskId
        Me.ParentAction = ParentAction
        Me.playRule = New Zamba.WFExecution.PlayDoCloseTask(Me)
    End Sub

    Public Property TaskId() As String Implements Core.IDoCloseTask.TaskId


End Class

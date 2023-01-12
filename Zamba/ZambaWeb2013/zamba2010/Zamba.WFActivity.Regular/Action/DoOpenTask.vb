Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Tareas"), RuleDescription("Abrir Tarea"), RuleHelp("Permite abrir una tarea"), RuleFeatures(True)> _
<Serializable()> Public Class DoOpenTask
    Inherits WFRuleParent
    Implements IDOOpenTask


    Private playRule As Zamba.WFExecution.PlayDoOpenTask

    Public Overrides ReadOnly Property IsFull() As Boolean




    Public Overrides ReadOnly Property IsLoaded() As Boolean




    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub

#Region "campos privados"



#End Region
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TaskID As String, ByVal DocID As String, ByVal UseCurrentTask As Boolean, ByVal OpenMode As Int32)
        MyBase.New(Id, Name, wfstepid)
        Me.TaskID = TaskID
        Me.DocID = DocID
        Me.UseCurrentTask = UseCurrentTask
        Me.OpenMode = OpenMode
        playRule = New Zamba.WFExecution.PlayDoOpenTask(Me)
    End Sub
    Public Property TaskID() As String Implements IDOOpenTask.TaskID
    Public Property DocID() As String Implements IDOOpenTask.DocID
    Public Property OpenMode() As Int32 Implements IDOOpenTask.OpenMode
    Public Property UseCurrentTask() As Boolean Implements IDOOpenTask.UseCurrentTask








    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    '04/07/11: Sumada la funcionalidad del la regla DoOpenTask
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.OpenTask
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class

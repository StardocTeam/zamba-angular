Imports Zamba.Core

''' <summary>
''' Regla que permite cerrar tareas.
''' </summary>
''' <remarks></remarks>
<RuleCategory("Interfaz"), RuleDescription("Ejecutar Script"), RuleHelp("Ejecuta un Script en la interfaz de usuario"), RuleFeatures(False)> <Serializable()>
Public Class DoExecuteScript
    Inherits WFRuleParent
    Implements IDoExecuteScript


    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean


    Public Overrides ReadOnly Property IsLoaded() As Boolean


    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteScript(Me)
        Return playRule.Play(results, Me)
    End Function
    '[Javier] 08/07/11 - Agregado el método.
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteScript(Me)

        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ExecuteScript
            Return playRule.PlayWeb(results, Params, Me)
        Else
            Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Script As String)
        MyBase.New(Id, Name, wfstepId)
        Me.Script = Script
    End Sub

    Public Property Script() As String Implements Core.IDoExecuteScript.Script



End Class

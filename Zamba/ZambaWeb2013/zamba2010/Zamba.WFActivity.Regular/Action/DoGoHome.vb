Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Zamba"), RuleDescription("Dirigirse al Home"), RuleHelp("Permite redirigir a la pantalla Home de la aplicacion "), RuleFeatures(False)> <Serializable()>
Public Class DoGoHome
    Inherits WFRuleParent
    Implements IDoGoHome

    Private playRule As WFExecution.PlayDoGoHome
    Private _isFull As Boolean
    Private _isLoaded As Boolean

    Public Sub New(id As Long, name As String, wfStepId As Long)
        MyBase.New(id, name, wfStepId)
        playRule = New WFExecution.PlayDoGoHome(Me)
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

    Public Overrides Sub FullLoad()
    End Sub

    Public Overrides Sub Load()
    End Sub

    Public Overrides Sub Dispose()
    End Sub

    Public Overrides Function Play(results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.GoToTabHome
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class

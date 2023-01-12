Imports Zamba.Core

<RuleCategory("Tareas"), RuleDescription("Esperar"), RuleHelp("Espera un tiempo antes de continuar con la siguiente tarea"), RuleFeatures(False)> <Serializable()> _
Public Class DoWait
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDoWait
    Private playRule As Zamba.WFExecution.PlayDOWait
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

    Private _WaitTime As String

    Public Property WaitTime() As String Implements Core.IDoWait.WaitTime
        Get
            Return _WaitTime
        End Get
        Set(ByVal value As String)
            _WaitTime = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal WaitTime As Int32)
        MyBase.New(Id, Name, wfstepId)

        _WaitTime = WaitTime
        Me.playRule = New Zamba.WFExecution.PlayDOWait(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

End Class

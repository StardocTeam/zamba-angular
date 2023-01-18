Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Usuarios"), RuleDescription("Pedir decision al Usuario"), RuleHelp("Permite realizarle preguntas al usuario del tipo SI/NO."), RuleFeatures(True)> <Serializable()> _
Public Class DOAskDesition
    Inherits WFRuleParent
    Implements IDOAskDesition
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

    Private _TXTVar As String
    Private _TXTAsk As String

    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal TXTVar As String, ByVal TXTAsk As String)
        MyBase.New(Id, Name, wfStepId)
        Me._TXTVar = TXTVar
        Me._TXTAsk = TXTAsk
    End Sub

    Public Property TxtVar() As String Implements IDOAskDesition.TXTVar
        Get
            Return _TXTVar
        End Get
        Set(ByVal value As String)
            _TXTVar = value
        End Set
    End Property


    Public Property TxtAsk() As String Implements IDOAskDesition.TXTAsk
        Get
            Return _TXTAsk
        End Get
        Set(ByVal value As String)
            _TXTAsk = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOAskDesition(Me)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayWeb(ByVal results As List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDOAskDesition(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowDoAskDesition
            Return playRule.PlayWeb(results, Params)
        Else
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return playRule.PlayWebSecondExecution(results, Params)
        End If
    End Function

End Class

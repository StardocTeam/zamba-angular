Imports Zamba.Core

<RuleCategory("Reglas"), RuleDescription("Ejecutar Regla"), RuleHelp("Permite seleccionar y ejecutar una regla en el Work Flow actual al momento de realizar la tarea"), RuleFeatures(False)> _
<Serializable()> Public Class DOExecuteRule
    Inherits WFRuleParent
    Implements IDOExecuteRule
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOExecuteRule
    Private _isRemote As Boolean
    Private _IDRule As String
    Private _Mode As Boolean
    Public Property IsRemote() As Boolean Implements IDOExecuteRule.IsRemote
        Get
            Return _isRemote
        End Get
        Set(ByVal value As Boolean)
            _isRemote = value
        End Set
    End Property

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

#Region "campos privados"
    Private _RuleId As Int32
#End Region
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal RuleID As Int32, ByVal IsRemote As Boolean, ByVal IDRule As String, ByVal Mode As Boolean)
        MyBase.New(Id, Name, wfstepid)
        Me._RuleId = RuleID
        Me._isRemote = IsRemote
        Me.playRule = New Zamba.WFExecution.PlayDOExecuteRule(Me)
        Me._IDRule = IDRule
        Me._Mode = Mode
    End Sub
    Public Property RuleID() As Int32 Implements IDOExecuteRule.RuleID
        Get
            Return _RuleId
        End Get
        Set(ByVal value As Int32)
            _RuleId = value
        End Set
    End Property
    Public Property IDRule() As String Implements IDOExecuteRule.IDRule
        Get
            Return _IDRule
        End Get
        Set(ByVal value As String)
            _IDRule = value
        End Set
    End Property
    Public Property Mode() As Boolean Implements IDOExecuteRule.Mode
        Get
            Return _Mode
        End Get
        Set(ByVal value As Boolean)
            _Mode = value
        End Set
    End Property

    'Public Overrides Function Play(ByVal Results As System.Collections.SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
                ExecutionResult = RuleExecutionResult.PendingEventExecution
                RulePendingEvent = RulePendingEvents.ExecuteRule
                Return playRule.PlayWeb(results, Params)
            Else
                Params.Clear()
                ExecutionResult = RuleExecutionResult.CorrectExecution
                RulePendingEvent = RulePendingEvents.NoPendingEvent
                Return results
            End If


    End Function
End Class

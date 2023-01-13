Imports Zamba.Core
Imports System.Xml.Serialization
<RuleCategory("Aplicaciones"), RuleDescription("Ejecutar Aplicacion o Comando"), RuleHelp("Permite ejecutar una aplicacion o comando"), RuleFeatures(False)> <Serializable()> _
Public Class DOShell
    Inherits WFRuleParent
    Implements IDOSHELL
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOShell
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
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal filepath As String, ByVal Parameter As String, ByVal UseProcess As Boolean)
        MyBase.New(Id, Name, wfstepid)
        _Parameter = Parameter
        _filepath = filepath
        _UseProcess = UseProcess
        Me.playRule = New Zamba.WFExecution.PlayDOShell(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Private _filepath As String
    Public Property FilePath() As String Implements IDOSHELL.Filepath
        Get
            Return _filepath
        End Get
        Set(ByVal value As String)
            _filepath = value
        End Set
    End Property

    Private _Parameter As String
    Public Property Parameter() As String Implements IDOSHELL.Parameter
        Get
            Return _Parameter
        End Get
        Set(ByVal value As String)
            _Parameter = value
        End Set
    End Property

    Private _UseProcess As Boolean
    Public Property UseProcess() As Boolean Implements IDOSHELL.UseProcess
        Get
            Return _UseProcess
        End Get
        Set(ByVal value As Boolean)
            _UseProcess = value
        End Set
    End Property



End Class


Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Tareas"), RuleDescription("Obtener Tarea"), RuleHelp("Obtiene un listado de todas las tareas seleccionadas."), RuleFeatures(False)> <Serializable()> _
Public Class DoGetTask
    Inherits WFRuleParent
    Implements IDoGetTask

    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub
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

    Private _TaskIdVariable As String
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TaskIdVariable As String)
        MyBase.New(Id, Name, wfstepid)
        Me._TaskIdVariable = TaskIdVariable
    End Sub


    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGetTask(Me)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoGetTask(Me)
        results = playRule.Play(results)
        Return results
    End Function

    Public Property TaskIdVariable() As String Implements Core.IDoGetTask.TaskIdVariable
        Get
            Return Me._TaskIdVariable
        End Get
        Set(ByVal value As String)
            Me._TaskIdVariable = value
        End Set
    End Property
End Class
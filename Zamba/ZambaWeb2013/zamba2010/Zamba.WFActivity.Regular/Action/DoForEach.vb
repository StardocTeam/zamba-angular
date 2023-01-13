Imports Zamba.Core
Imports Zamba.Data
Imports System.Xml.Serialization


<RuleCategory("Reglas"), RuleDescription("Repetir Regla"), RuleHelp("Repite una regla tantas veces como le sea indicado. Ejecutando también todas las reglas hijas que contenga"), RuleFeatures(False)> <Serializable()> _
Public Class DoForEach

    Inherits WFRuleParent
    Implements IDoForEach

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoForEach

    Private _Value As String



    Public Property Value() As String Implements IDoForEach.Value
        Get
            Return _Value
        End Get
        Set(ByVal value As String)
            _Value = value
        End Set
    End Property

    Public Overrides Sub Dispose()

    End Sub
    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

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


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal Value As String)

        MyBase.New(Id, Name, wfStepId)
        Me._Value = Value
        Me.playRule = New Zamba.WFExecution.PlayDoForEach(Me)
    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class

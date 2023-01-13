Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Datos"), RuleDescription("Realiza una Pregunta al Usuario"), RuleHelp("Permite realizar una pregunta al usuario y guarda la respuesta a la misma."), RuleFeatures(True)> <Serializable()> _
Public Class DoConsumeRestApi
    Inherits WFRuleParent
    Implements IDoConsumeRestApi

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Public Property ResultVar As String Implements IDoConsumeRestApi.ResultVar
    Public Property Url As String Implements IDoConsumeRestApi.Url
    Public Property JsonMessage As String Implements IDoConsumeRestApi.JsonMessage
    Public Property Method As String Implements IDoConsumeRestApi.Method

    Private playRule As Zamba.WFExecution.PlayDoConsumeRestApi

#Region "WFruleParent Members"
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
#End Region

    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub



    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Url As String, ByVal ResultVar As String, ByVal JsonMessage As Integer, ByVal Method As String)
        MyBase.New(Id, Name, wfstepid)


        Me.ResultVar = ResultVar
        Me.Url = Url
        Me.JsonMessage = JsonMessage
        Me.Method = Method

        Me.playRule = New Zamba.WFExecution.PlayDoConsumeRestApi(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    'Dim NewSortedList As New SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function


End Class
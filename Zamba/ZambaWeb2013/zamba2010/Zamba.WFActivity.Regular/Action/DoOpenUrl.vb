Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Secciones"), RuleDescription("Abrir una dirección web"), RuleHelp("Abre una URL en Home, en la misma ventana o una ventana nueva de un explorador web."), RuleFeatures(True)> <Serializable()> _
Public Class DoOpenUrl

    Inherits WFRuleParent
    Implements IDoOpenUrl

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _url As String
    Private _openMode As OpenType
    Private playRule As Zamba.WFExecution.PlayDoOpenUrl

    Public Property Url() As String Implements Core.IDoOpenUrl.Url
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property
    Public Property OpenMode() As OpenType Implements Core.IDoOpenUrl.OpenMode
        Get
            Return _openMode
        End Get
        Set(ByVal value As OpenType)
            _openMode = value
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
    Private Sub New()
        MyBase.New(0, String.Empty, 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pUrl As String, ByVal openType As Integer)
        MyBase.New(Id, Name, wfstepid)
        Me._url = pUrl
        Me._openMode = openType
        Me.playRule = New Zamba.WFExecution.PlayDoOpenUrl(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.OpenUrl
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

End Class
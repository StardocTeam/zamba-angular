Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("Secciones"), RuleDescription("Exportar HTML a PDF"), RuleHelp("Abre una ventana en web para exportar HTML a PDF."), RuleFeatures(False)> <Serializable()> _
Public Class DoExportHTMLToPDF
    Inherits WFRuleParent
    Implements IDoExportHTMLToPDF

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExportHTMLToPDF

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal Content As String, ByVal CanEditable As Boolean, ByVal ReturnFileName As String)
        MyBase.New(Id, Name, wfStepId)
        Me._Content = Content
        Me._CanEditable = CanEditable
        Me._ReturnFileName = ReturnFileName
        playRule = New Zamba.WFExecution.PlayDoExportHTMLToPDF(Me)
    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowExportPDFControl
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

    Private _Content As String
    Private _CanEditable As Boolean
    Private _ReturnFileName As String

    Public Property Content() As String Implements Core.IDoExportHTMLToPDF.Content
        Get
            Return _Content
        End Get
        Set(ByVal value As String)
            _Content = value
        End Set
    End Property

    Public Property CanEditable() As Boolean Implements Core.IDoExportHTMLToPDF.CanEditable
        Get
            Return _CanEditable
        End Get
        Set(ByVal value As Boolean)
            _CanEditable = value
        End Set
    End Property

    Public Property ReturnFileName() As String Implements Core.IDoExportHTMLToPDF.ReturnFileName
        Get
            Return _ReturnFileName
        End Get
        Set(ByVal value As String)
            _ReturnFileName = value
        End Set
    End Property
End Class
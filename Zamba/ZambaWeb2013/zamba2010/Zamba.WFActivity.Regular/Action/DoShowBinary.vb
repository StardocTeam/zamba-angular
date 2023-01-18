Imports Zamba.Core

''' <summary>
''' Regla que proporciona ayuda para las reglas de tipo DO
''' </summary>
''' <remarks></remarks>
<RuleCategory("Archivos"), RuleDescription("Mostrar un binario sin temporal"), RuleHelp("Muestra un archivo de origen binario sin generar un temporal para ello"), RuleIconId("38"), RuleFeatures(False)> <Serializable()> _
Public Class DoShowBinary
    Inherits WFRuleParent
    Implements IDoShowBinary

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _binaryFile As String
    Private _mimeType As String

    Public Property binaryFile As String Implements Core.IDoShowBinary.binaryFile
        Get
            Return _binaryFile
        End Get
        Set(value As String)
            _binaryFile = value
        End Set
    End Property

    Public Property mimeType As String Implements Core.IDoShowBinary.mimeType
        Get
            Return _mimeType
        End Get
        Set(value As String)
            _mimeType = value
        End Set
    End Property

    Public Overrides Sub Dispose()
        _binaryFile = Nothing
        _mimeType = Nothing
    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get
            Return Me._isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get
            Return Me._isLoaded
        End Get
    End Property

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoShowBinary(Me)
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            Dim playRule As New Zamba.WFExecution.PlayDoShowBinary(Me)
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ShowBinary
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal binaryFile As String, ByVal mimeType As String)
        MyBase.New(Id, Name, wfstepId)
        _binaryFile = binaryFile
        _mimeType = mimeType
    End Sub

End Class

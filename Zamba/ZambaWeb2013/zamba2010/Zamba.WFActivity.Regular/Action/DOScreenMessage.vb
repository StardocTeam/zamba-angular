Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Mensajes"), RuleDescription("Mostrar Mensaje"), RuleHelp("Permite generar un mensaje para ser mostrado en pantalla al momento de ejecutar la regla"), RuleFeatures(True)> <Serializable()> _
Public Class DOSCREENMESSAGE
    Inherits WFRuleParent
    Implements IDOSCREENMESSAGE
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private mScreenName As String
    Private playRule As Zamba.WFExecution.PlayDOScreenMessage
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByRef wfstepId As Int64, ByVal Mensaje As String, ByVal HideDocumentName As Boolean, ByVal Action As String)
        MyBase.New(Id, Name, wfstepId)
        Me._Mensaje = Mensaje
        Me._HideDocumentName = HideDocumentName
        Me.Action = Action
        Me.playRule = New Zamba.WFExecution.PlayDOScreenMessage(Me)
    End Sub
    Public ReadOnly Property NameScreen() As String Implements IDOSCREENMESSAGE.NameScreen
        Get
            Return Me.mScreenName
        End Get
    End Property
    Public Property Mensaje() As String Implements IDOSCREENMESSAGE.Mensaje
    Public Property Action() As String Implements IDOSCREENMESSAGE.Action

    Public Property HideDocumentName() As Boolean Implements IDOSCREENMESSAGE.HideDocumentName

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
            RulePendingEvent = RulePendingEvents.ShowMessage
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class
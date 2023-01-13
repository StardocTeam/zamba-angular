Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoDelete
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla para eliminar una tarea
''' </summary>
''' Hereda WFRuleParent
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Eliminar Tarea"), RuleHelp("Permite borrar una tarea de un Work Flow o en su totalidad"), RuleFeatures(False)> <Serializable()> _
Public Class DoDelete
    Inherits WFRuleParent
    Implements IDoDelete
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal TipoBorrado As Int32, ByVal KeepFile As Boolean)
        MyBase.New(Id, Name, wfstepid) ', ListofRules.DoDelete)
        Me.TipoBorrado = TipoBorrado
        Me.DeleteFile = KeepFile 'no dar vuelta el booleano
    End Sub

    Private Borrado As Borrados
    Private _DeleteFile As Boolean

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As System.Collections.SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDODELETE()
        Return playRule.Play(results, Me)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDODELETE()
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.ResponseToDelete
            Return playRule.PlayWeb(results, Params, Me)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

    Public Property TipoBorrado() As Borrados Implements IDoDelete.TipoBorrado
        Get
            Return Borrado
        End Get
        Set(ByVal Value As Borrados)
            Borrado = Value
        End Set
    End Property
    Public Property DeleteFile() As Boolean Implements IDoDelete.DeleteFile
        Get
            Return _DeleteFile
        End Get
        Set(ByVal Value As Boolean)
            _DeleteFile = Value
        End Set
    End Property
End Class





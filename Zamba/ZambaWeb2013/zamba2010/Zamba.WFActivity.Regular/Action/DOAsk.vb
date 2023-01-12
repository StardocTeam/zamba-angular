Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Datos"), RuleDescription("Realiza una Pregunta al Usuario"), RuleHelp("Permite realizar una pregunta al usuario y guarda la respuesta a la misma."), RuleFeatures(True)> <Serializable()> _
Public Class DoAsk
    Inherits WFRuleParent
    Implements IDOAsk
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _Variable As String
    Private _Mensaje As String
    Private _ValorPorDefecto As String
    'Private _Tamaño As Integer
    Private playRule As Zamba.WFExecution.PlayDOAsk

    Public Property Variable() As String Implements IDOAsk.Variable
        Get
            Return _Variable
        End Get
        Set(ByVal value As String)
            _Variable = value
        End Set
    End Property

    Public Property ValorPorDefecto() As String Implements IDOAsk.ValorPorDefecto
        Get
            Return _ValorPorDefecto
        End Get
        Set(ByVal value As String)
            _ValorPorDefecto = value
        End Set
    End Property

    Public Property Mensaje() As String Implements IDOAsk.Mensaje
        Get
            Return _Mensaje
        End Get
        Set(ByVal value As String)
            _Mensaje = value
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
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pMensaje As String, ByVal pVariable As String, ByVal pTamaño As Integer, ByVal pValorPorDefecto As String)
        MyBase.New(Id, Name, wfstepid)
        Me.Mensaje = pMensaje
        Me.Variable = pVariable
        Me.ValorPorDefecto = pValorPorDefecto
        'Me.Tamaño = pTamaño
        Me.playRule = New Zamba.WFExecution.PlayDOAsk(Me)
    End Sub

    'Public Overloads Overrides Function Play(ByVal Results As SortedList) As SortedList
    'Dim NewSortedList As New SortedList
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
            RulePendingEvent = RulePendingEvents.ShowDoAsk
            Return playRule.PlayWeb(results, Params)
        Else
            If Params.Contains("CancelRule") Then
                If Params("CancelRule") Then
                    If Me.ThrowExceptionIfCancel Then
                        ZTrace.WriteLineIf(ZTrace.IsInfo, "Regla configurada para provocar una exception en caso de cancelar la misma")
                        Throw New Exception("El usuario cancelo la ejecucion de la regla")
                    Else
                        Params.Clear()

                        ExecutionResult = RuleExecutionResult.PendingEventExecution
                        RulePendingEvent = RulePendingEvents.CancelExecution
                        Return Nothing
                    End If
                End If
            End If

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return playRule.PlayWebSecondExecution(results, Params)
        End If
    End Function
End Class
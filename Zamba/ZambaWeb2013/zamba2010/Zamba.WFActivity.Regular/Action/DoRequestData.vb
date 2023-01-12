Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Datos"), RuleDescription("Solicitar Datos"), RuleHelp("Permite solicitar al usuario los datos de los indices de un entidad seleccionado o de un indice en particular"), RuleFeatures(True)> <Serializable()> _
Public Class DoRequestData
    Inherits WFRuleParent
    Implements IDoRequestData
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoRequestData
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocTypeId As Int32, ByVal Ids As String)
        MyBase.New(Id, Name, wfstepid)

        Me.DocTypeId = DocTypeId
        Me.ArrayIds = New ArrayList

        For Each Item As String In Ids.Split("*")
            Me.ArrayIds.Add(Item)
        Next
        Me.playRule = New Zamba.WFExecution.PlayDoRequestData(Me)
    End Sub

    Private _docTypeId As Int32
    Private _arrayIds As ArrayList

    Public Function JoinIds() As String Implements IDoRequestData.JoinIds
        Dim Str As String = ""
        For Each Item As String In Me.ArrayIds
            Str += "*" & Item
        Next
        If Str <> String.Empty Then
            Str = Str.Substring(1)
        End If
        Return Str
    End Function

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
            RulePendingEvent = RulePendingEvents.ShowDoRequestData
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

            'Params.Clear()

            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return playRule.PlayWebSecondExecution(results, Params)
        End If
    End Function

    Public Property DocTypeId() As Int32 Implements IDoRequestData.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Int32)
            _docTypeId = value
        End Set
    End Property
    Public Property ArrayIds() As ArrayList Implements IDoRequestData.ArrayIds
        Get
            Return _arrayIds
        End Get
        Set(ByVal value As ArrayList)
            _arrayIds = value
        End Set
    End Property

    Public Overrides Sub Dispose()

    End Sub
End Class


Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Datos"), RuleDescription("Ingresar Indice"), RuleHelp("solicita el ingreso del valor de un indice"), RuleFeatures(True)> <Serializable()> _
Public Class DOInputIndex
    Inherits WFRuleParent
    Implements IDOInputIndex
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDOInputIndex
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

    Private m_IndexId As Int32
    'Private m_iDocTypeId As Int32
    Private m_Name As String

    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal p_iDocTypeId As Int32, ByVal p_iIndexId As Int32)
        MyBase.New(Id, Name, WFStepid)
        m_IndexId = p_iIndexId
        Me.playRule = New Zamba.WFExecution.PlayDOInputIndex(Me)
    End Sub

    Public Property Index() As Int32 Implements IDOInputIndex.Index
        Get
            Return m_IndexId
        End Get
        Set(ByVal value As Int32)
            m_IndexId = value
            Try
                m_Name = IndexsBusiness.GetIndexName(m_IndexId)
            Catch ex As Exception
                Zamba.Core.ZClass.raiseerror(ex)
            End Try
        End Set
    End Property

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
            RulePendingEvent = RulePendingEvents.ShowDoInputIndex
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

Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoChangeState
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla para modificar el estado de una tarea
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Cambiar Estado"), RuleHelp("Permite modificar el estado de una tarea al ejecutarse la regla"), RuleFeatures(False)> <Serializable()> _
Public Class DoChangeState
    Inherits WFRuleParent
    Implements IDoChangeState
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoChangeState
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal p_iStateId As Int32, ByVal p_iStepId As Int32)
        MyBase.New(Id, Name, wfstepId) ', ListofRules.DoChangeState)
        m_iStateId = p_iStateId
        m_iStepId = p_iStepId
        Me.playRule = New Zamba.WFExecution.PlayDoChangeState(Me)
    End Sub
    '--ITEMS--
    'StateId=0

    'Properties
    Private m_iStateId As Int32
    Private m_iStepId As Int32

    Public Property StepId() As Int32 Implements IDoChangeState.StepId
        Get
            Return m_iStepId
        End Get
        Set(ByVal value As Int32)
            m_iStepId = value
        End Set
    End Property

    Public Property StateId() As Int32 Implements IDoChangeState.StateId
        Get
            Return m_iStateId
        End Get
        Set(ByVal value As Int32)
            m_iStateId = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class


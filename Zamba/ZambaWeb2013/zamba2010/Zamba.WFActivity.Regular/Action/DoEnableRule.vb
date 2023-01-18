Imports Zamba.Core
Imports System.Xml.Serialization

''' <summary>
''' Activa o desactiva un regla
''' </summary>
''' <remarks></remarks>
<RuleCategory("Reglas"), RuleDescription("Activar o Desactivar Regla"), RuleHelp("Permite activar o desactivar una regla de una etapa o del Work Flow actual"), RuleFeatures(False)> <Serializable()>
Public Class DoEnableRule
    Inherits WFRuleParent
    Implements IDoEnableRule
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoEnableRule
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
    Private m_sSelectedRuleIDs As String
    Private m_bEstado As Boolean
    Private m_bEjecucion As Boolean
    Private m_bOnlyForTask As Boolean
    Private m_sNombreDeLaRegla As String
    Private m_bViewAllSteps As Boolean


    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal iRulesId As String, ByVal bEstado As Boolean, ByVal OnlyForTask As Boolean, ByVal bViewAllSteps As Boolean, ByVal bEjecucion As Boolean)
        MyBase.New(Id, Name, wfstepid)
        m_sSelectedRuleIDs = iRulesId
        m_bEstado = bEstado
        m_bOnlyForTask = OnlyForTask
        m_bViewAllSteps = bViewAllSteps
        m_bEjecucion = bEjecucion
        Me.playRule = New Zamba.WFExecution.PlayDoEnableRule(Me)
    End Sub

    'Public ReadOnly Property RuleWFStep() As IWFStep Implements IDoEnableRule.WFStep
    '    Get
    '        Return WFStep
    '    End Get
    'End Property


    Public Property SelectedRuleIDs() As String Implements IDoEnableRule.SelectedRulesIDs
        Get
            Return m_sSelectedRuleIDs
        End Get
        Set(ByVal value As String)
            m_sSelectedRuleIDs = value
        End Set
    End Property

    Public Property OnlyForTask() As Boolean Implements IDoEnableRule.OnlyForTask
        Get
            Return m_bOnlyForTask
        End Get
        Set(ByVal value As Boolean)
            m_bOnlyForTask = value
        End Set
    End Property

    Public Property RuleEstado() As Boolean Implements IDoEnableRule.RuleEstado
        Get
            Return m_bEstado
        End Get
        Set(ByVal value As Boolean)
            m_bEstado = value
        End Set
    End Property

    Public Property RuleEjecucion() As Boolean Implements IDoEnableRule.RuleEjecucion
        Get
            Return m_bEjecucion
        End Get
        Set(ByVal value As Boolean)
            m_bEjecucion = value
        End Set
    End Property

    Public Property RuleName() As String Implements IDoEnableRule.RuleName
        Get
            Return m_sNombreDeLaRegla
        End Get
        Set(ByVal value As String)
            m_sNombreDeLaRegla = value
        End Set
    End Property

    Public Property ViewAllSteps() As Boolean Implements IDoEnableRule.ViewAllSteps
        Get
            Return m_bViewAllSteps
        End Get
        Set(ByVal value As Boolean)
            m_bViewAllSteps = value
        End Set
    End Property

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoEnableRule(Me)
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoEnableRule(Me)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.EnableRules
            Return playRule.PlayWeb(results, Params, Me)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function
End Class

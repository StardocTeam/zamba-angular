Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoDistribuir
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla de Zamba WorkFlow
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Tareas"), RuleDescription("Distribuir Tarea"), RuleHelp("Permite enviar una tarea a la siguiente etapa u otra cualquiera del Work Flow actual"), RuleFeatures(False)> <Serializable()> _
Public Class DoDistribuir
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IDoDistribuir
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDODistribuir
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
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nombre para mostrar la regla</param>
    ''' <param name="WFStepid">Etapa Inicial</param>
    ''' <param name="NewWFStepId">Etapa a la cual se quiere distribuir</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    '''
    '''  </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal NewWFStepId As Long, ByVal SelecCarp As Boolean)
        MyBase.New(Id, Name, wfstepId)
        Me._NewWFStepID = NewWFStepId
        Me.playRule = New Zamba.WFExecution.PlayDODistribuir(Me)
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Metodo generico que se invoca para ejecutar la regla, este es el punto de entrada
    ''' en la ejecucion de la misma.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Martin]	30/05/2006	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        If IsNothing(Params) Then
            Params = New Hashtable
        End If
        If IsNothing(Params) OrElse Params.Count = 0 Then
            ExecutionResult = RuleExecutionResult.PendingEventExecution
            RulePendingEvent = RulePendingEvents.Distribuir
            Return playRule.PlayWeb(results, Params)
        Else
            Params.Clear()
            ExecutionResult = RuleExecutionResult.CorrectExecution
            RulePendingEvent = RulePendingEvents.NoPendingEvent
            Return results
        End If
    End Function

#Region "Local properties"
    'Public Property NewWFStep() As IWFStep Implements IDoDistribuir.NewWFStep
    '    Get
    '        Return _NewWFStep
    '    End Get
    '    Set(ByVal value As IWFStep)
    '        _NewWFStep = value
    '    End Set
    'End Property
    Public Property NewWFStepId() As Int64 Implements IDoDistribuir.NewWFStepId
        Get
            Return _NewWFStepID
        End Get
        Set(ByVal value As Int64)
            _NewWFStepID = value
        End Set
    End Property

    'Public Property SelecCarp() As Boolean Implements IDoDistribuir.SelecCarp
    '    Get
    '        Return _SelecCarp
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _SelecCarp = value
    '    End Set
    'End Property
    'Public ReadOnly Property WFOldStep() As IWFStep Implements IDoDistribuir.WFStep
    '    Get
    '        Return Me.WFStep
    '    End Get
    'End Property
    'Private _NewWFStep As WFStep
    Private _NewWFStepID As Int64
    'Private _SelecCarp As Boolean
#End Region

End Class

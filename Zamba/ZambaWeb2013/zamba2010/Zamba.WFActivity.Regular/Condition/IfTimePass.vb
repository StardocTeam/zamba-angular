Imports Zamba.Data
Imports zamba.Core

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Business
''' Class	 : Core.DoGenerateFact
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla de Zamba WorkFlow
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[Marcelo]	30/05/2007	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("WorkFlow"), RuleDescription("Ejecutar cada Determinado Tiempo"), RuleHelp("Ejecuta las subreglas nuevamente en un plazo de tiempo determinado"), RuleFeatures(False)> _
Public Class IfTimePass
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IIfTimePass, IRuleIFPlay
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
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="Id">Id de la regla</param>
    ''' <param name="Name">Nombre para mostrar la regla</param>
    ''' <param name="WFStepid">Etapa Inicial</param>
    ''' <param name="Minute">Cada cuantos minutos se va a ejecutar la etapa</param>
    ''' <param name="Hour">Cada cuantas horas se va a ejecutar la etapa</param>
    ''' <param name="day">Cada cuantos dias se va a ejecutar la etapa</param>
    ''' <param name="Week">Cada cuantas semanas se va a ejecutar la etapa</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	05/03/2007	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal Minute As Int32, ByVal Hour As Int32, ByVal Day As Int32, ByVal Week As Int32)
        MyBase.New(Id, Name, wfstepId)
        Me._Minute = Minute
        Me._Hour = Hour
        Me._Day = Day
        Me._Week = Week
    End Sub

    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' Write the trace and validate the date.
    ''' </summary>
    ''' <param name="results"></param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Marcelo]	23/02/2007	Created
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfTimePass()
        results = playRule.Play(results, Me)
        If results.Count > 0 Then
            lastExecute = Now
        End If
        Return results
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfTimePass()
        results = playRule.Play(results, Me)
        If results.Count > 0 Then
            lastExecute = Now
        End If
        Return results
    End Function

    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <hisytory>
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </hisytory>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfTimePass()
        Return playRule.Play(results, Me, ifType)
    End Function

#Region "Local variables"
    Public Property Minute() As Int32 Implements IIfTimePass.Minute
        Get
            Return _Minute
        End Get
        Set(ByVal value As Int32)
            _Minute = value
        End Set
    End Property
    Private _Minute As Int32
    Public Property Hour() As Int32 Implements IIfTimePass.Hour
        Get
            Return _Hour
        End Get
        Set(ByVal value As Int32)
            _Hour = value
        End Set
    End Property
    Private _Hour As Int32
    Public Property Day() As Int32 Implements IIfTimePass.Day
        Get
            Return _Day
        End Get
        Set(ByVal value As Int32)
            _Day = value
        End Set
    End Property
    Private _Day As Int32
    Public Property Week() As Int32 Implements IIfTimePass.Week
        Get
            Return _Week
        End Get
        Set(ByVal value As Int32)
            _Week = value
        End Set
    End Property
    Private _Week As Int32

    Private _lastExecute As Date
    Public Property lastExecute() As Date Implements IIfTimePass.lastExecute
        Get
            Return _lastExecute
        End Get
        Set(ByVal value As Date)
            _lastExecute = value
        End Set
    End Property
#End Region
End Class

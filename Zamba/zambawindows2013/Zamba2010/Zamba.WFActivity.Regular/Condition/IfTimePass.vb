Imports zamba.Core

<RuleMainCategory("Workflow"), RuleCategory("Reglas"), RuleSubCategory(""), RuleDescription("Ejecutar cada Determinado Tiempo"), RuleHelp("Ejecuta las subreglas nuevamente en un plazo de tiempo determinado"), RuleFeatures(False)> _
Public Class IfTimePass
    'toda regla de accion debe derivar de WFRuleParent
    Inherits WFRuleParent
    Implements IIfTimePass, IRuleIFPlay, IRuleValidate
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean

    Private playRule As Zamba.WFExecution.PlayIfTimePass
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
        _Minute = Minute
        _Hour = Hour
        _Day = Day
        _Week = Week
        playRule = New WFExecution.PlayIfTimePass(Me)
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
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        results = playRule.Play(results)
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

        Return playRule.Play(results, ifType)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
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
    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
#End Region
End Class
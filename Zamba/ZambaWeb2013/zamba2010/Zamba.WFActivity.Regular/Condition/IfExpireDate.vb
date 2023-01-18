Imports Zamba.Core

<RuleCategory("Vencimientos"), RuleDescription("Validar Vencimiento de Tarea"), RuleHelp("Comprueba en que fecha y hora vence la tarea"), RuleFeatures(False)> _
Public Class IfExpireDate
    Inherits WFRuleParent
    Implements IIfExpireDate, IRuleIFPlay

    Private dHoras As Double
    Private Comp As Comparaciones
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal Comparacion As Int32, ByVal Horas As Double)
        MyBase.New(Id, Name, wfstepid)
        Me.Comp = Comparacion
        Me.dHoras = Horas
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfExpireDate()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfExpireDate()
        Return playRule.Play(results, Me)
    End Function
    ''' <summary>
    ''' Ejecuta la regla devolviendo los results que cumplan con la condicion requerida
    ''' </summary>
    ''' <param name="results">Results a validar</param>
    ''' <param name="ifType">True si quiero que devuelve los results que coincidan con la validacion, false si quiero los que no coincidan</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''        [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfExpireDate()
        Return playRule.Play(results, Me, ifType)
    End Function

    Public Property CantidadHoras() As Double Implements IIfExpireDate.CantidadHoras
        Get
            Return dHoras
        End Get
        Set(ByVal Value As Double)
            dHoras = Value
        End Set
    End Property

    Public Property Comparacion() As Comparaciones Implements IIfExpireDate.Comparacion
        Get
            Return Comp
        End Get
        Set(ByVal Value As Comparaciones)
            Comp = Value
        End Set
    End Property
End Class

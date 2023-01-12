Imports Zamba.Core

<RuleCategory("Tareas"), RuleDescription("Validar Estado de Tarea"), RuleHelp("Comprueba el estado de la tarea actual"), RuleFeatures(False)> _
Public Class IfTaskState
    Inherits WFRuleParent
    Implements IIfTaskState, IRuleIFPlay
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
#Region "Constantes"
    Public Const SEPARADOR_INDICE As String = ","
    '   Protected Const SEPARADOR_ESTADO As String = "/"
#End Region

#Region "Constructores"
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal States As String, ByVal Comp As Comparators)
        MyBase.New(Id, Name, wfstepid)
        Me.States = States
        Me.Comp = Comp
    End Sub
#End Region
    Public Overrides Sub Dispose()

    End Sub
    Private _states As String
    Private _Comp As Comparators

    Public ReadOnly Property GetSEPARADOR_INDICE() As String Implements IIfTaskState.SEPARADOR_INDICE
        Get
            Return SEPARADOR_INDICE
        End Get
    End Property

    Public Property States() As String Implements IIfTaskState.States
        Get
            Return _states
        End Get
        Set(ByVal value As String)
            _states = value
        End Set
    End Property
    Public Property Comp() As Comparators Implements IIfTaskState.Comp
        Get
            Return _Comp
        End Get
        Set(ByVal value As Comparators)
            _Comp = value
        End Set
    End Property
    'end AddBy

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfTaskState()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfTaskState()
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
    '''      [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfTaskState()
        Return playRule.Play(results, Me, ifType)
    End Function

    ' Devuelve la descripcion de los estados... 
    Protected Function getStateDescription() As String
        Dim output As String = ""
        For Each id As String In Me.getStates()
            Dim idenum As Zamba.Core.TaskStates
            If id <> String.Empty Then
                idenum = Convert.ToInt32(id)
                output &= idenum.ToString & " o "
            End If
        Next
        If output = String.Empty Then
            Return ""
        Else
            Return output.Substring(0, output.Length - 3)
        End If
    End Function

    Private Function getStates() As String()
        Return Split(States, SEPARADOR_INDICE)
    End Function
End Class

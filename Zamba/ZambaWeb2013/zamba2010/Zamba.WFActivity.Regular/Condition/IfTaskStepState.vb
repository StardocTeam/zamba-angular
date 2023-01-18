Imports System.Collections.Generic
Imports Zamba.Core

<RuleCategory("Tareas"), RuleDescription("Validar Estado de Etapa"), RuleHelp("Comprueba el estado de la etapa actual"), RuleFeatures(False)> _
Public Class IfTaskStepState
    Inherits WFRuleParent
    Implements IIfTaskStepState, IRuleIFPlay
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayIfTaskStepState
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
#Region "Constantes"
    Private Const SEPARADOR_INDICE As String = ","
#End Region
    Public ReadOnly Property getSEPARADOR_INDICE() As String Implements IIfTaskStepState.SEPARADOR_INDICE
        Get
            Return SEPARADOR_INDICE
        End Get
    End Property

    Public Property States() As String Implements IIfTaskStepState.States
        Get
            Return _States
        End Get
        Set(ByVal value As String)
            _States = value
        End Set
    End Property
    Private _States As String
    Public Property Comp() As Comparators Implements IIfTaskStepState.Comp
        Get
            Return _Comp
        End Get
        Set(ByVal value As Comparators)
            _Comp = value
        End Set
    End Property
    Private _Comp As Comparators

#Region "Constructores"
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal States As String, ByVal Comp As Comparators)
        MyBase.New(Id, Name, wfstepId)
        Me._States = States
        Me._Comp = Comp
        Me.playRule = New Zamba.WFExecution.PlayIfTaskStepState(Me)
    End Sub
#End Region


#Region "Metodos"

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return Me.playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
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
        Return playRule.Play(results, ifType)
    End Function

    ' Devuelve la descripcion de los estados... 
    Protected Function getStateDescription() As String
        Dim output As String = ""
        Dim WFSB As New WFStepBusiness

        For Each item As String In Me.getStates()
            If item <> String.Empty Then
                Dim ruleWFStep As WFStep = WFSB.GetStepById(WFStepId)
                output &= DirectCast(ruleWFStep.States.Item(Int32.Parse(item)), WFStepState).Name & " o "
            End If
        Next
        WFSB = Nothing
        If output = String.Empty Then Return ""
        Return output.Substring(0, output.Length - 3)
    End Function

    Protected Function getStates() As String()
        Return Split(_States, SEPARADOR_INDICE)
    End Function
#End Region
End Class




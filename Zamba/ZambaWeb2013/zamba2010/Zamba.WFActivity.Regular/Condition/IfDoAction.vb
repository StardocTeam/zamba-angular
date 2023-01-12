Imports zamba.Core

<RuleCategory("Reglas"), RuleDescription("Validar Regla"), RuleHelp("Valida la ejecucion de una regla"), RuleFeatures(False)> _
Public Class IfDoAction
    Inherits WFRuleParent
    Implements IRuleIFPlay, IIfDoAction
    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Public Overrides Sub Dispose()

    End Sub
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
    End Sub
    ''' <summary>
    ''' Ejecuta la regla
    ''' </summary>
    ''' <param name="results"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Try
            Dim playRule As New Zamba.WFExecution.PlayIfDoAction()
            Return playRule.Play(results, Me)
        Catch ex As Exception
            Zamba.Core.ZClass.raiseerror(ex)
        End Try
        Return results
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayIfDoAction()
        Return playRule.Play(results, Me)
    End Function

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides ReadOnly Property IsFull() As Boolean
        Get

        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded() As Boolean
        Get

        End Get
    End Property

    Public Overrides Sub Load()

    End Sub
    ''' <summary>
    ''' Ejecuta el if de la regla
    ''' </summary>
    ''' <param name="results"></param>
    ''' <param name="ifType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    '''     [Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    ''' </history>
    Public Function PlayIf(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByVal ifType As Boolean) As System.Collections.Generic.List(Of ITaskResult) Implements IRuleIFPlay.PlayIf
        Dim playRule As New Zamba.WFExecution.PlayIfDoAction()
        Return playRule.Play(results, Me, ifType)
    End Function
End Class

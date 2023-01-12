Imports Zamba.Core


<RuleCategory("Desicion"), RuleDescription("Resolver Matriz de Desicion"), RuleHelp("Resuleve una tabla de datos, para la toma de desiciones"), RuleFeatures(True)> <Serializable()>
Public Class DODecisionMatrix
    Inherits WFRuleParent
    Implements IDODecisionMatrix

    Private playRule As Zamba.WFExecution.PlayDODecisionMatrix
    Public Overrides ReadOnly Property IsFull() As Boolean

    Public Overrides ReadOnly Property IsLoaded() As Boolean

    Public Overrides Sub FullLoad()

    End Sub
    Public Overrides Sub Load()

    End Sub
    Public Overrides Sub Dispose()

    End Sub



    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal entityId As Int64, ByVal outputIndex As Int64, ByVal outputVariable As String, ByVal altoutputIndex As Int64)
        MyBase.New(Id, Name, WFStepid)
        Me.EntityId = entityId
        Me.OutputIndex = outputIndex
        Me.AltOutputIndex = altoutputIndex
        Me.OutputVariable = outputVariable
        playRule = New Zamba.WFExecution.PlayDODecisionMatrix(Me)
    End Sub

    Public Property OutputIndex As Int64 Implements IDODecisionMatrix.OutputIndex
    Public Property AltOutputIndex As Int64 Implements IDODecisionMatrix.AltOutputIndex
    Public Property OutputVariable As String Implements IDODecisionMatrix.OutputVariable

    Public Property EntityId As Long Implements IDODecisionMatrix.EntityId


    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overrides Function PlayWeb(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByRef RulePendingEvent As Core.RulePendingEvents, ByRef ExecutionResult As Core.RuleExecutionResult, ByRef Params As System.Collections.Hashtable) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return Me.playRule.Play(results)
    End Function


End Class
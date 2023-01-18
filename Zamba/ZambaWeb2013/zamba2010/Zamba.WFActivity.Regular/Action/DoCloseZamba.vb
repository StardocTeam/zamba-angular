Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Zamba"), RuleDescription("Cierra Zamba"), RuleHelp("Regla que cierra Zamba."), RuleFeatures(True)> <Serializable()> _
Public Class DoCloseZamba
    Inherits WFRuleParent
    Implements IDoCloseZamba


#Region "Atributos"
    Private playRule As Zamba.WFExecution.PlayDoCloseZamba
    Private _isFull As Boolean
    Private _isLoaded As Boolean
#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64)
        MyBase.New(Id, Name, wfstepId)
        Me.playRule = New Zamba.WFExecution.PlayDoCloseZamba(Me)
    End Sub


    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

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

    Public Overrides Sub Load()

    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
  
End Class

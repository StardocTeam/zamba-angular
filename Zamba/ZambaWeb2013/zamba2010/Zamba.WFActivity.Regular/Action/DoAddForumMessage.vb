Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("Mensajes"), RuleDescription("Enviar mensaje al foro"), RuleHelp("Permite enviar o visualizar mensajes del foro de un documento asociado."), RuleFeatures(True)> <Serializable()> _
Public Class DoAddForumMessage
    Inherits WFRuleParent
    Implements IDoAddForumMessage
    Public Overrides Sub Dispose()

    End Sub
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
#Region "Constructores"
    Public Sub New(ByVal ruleID As Int64, ByVal ruleName As String, ByVal wfstepid As Int64)
        MyBase.New(ruleID, ruleName, wfstepid)
    End Sub
#End Region


    'Public Overrides Function Play(ByVal results As SortedList) As SortedList
    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoAddForumMessage()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoAddForumMessage()
        Return playRule.Play(results, Me)
    End Function
End Class

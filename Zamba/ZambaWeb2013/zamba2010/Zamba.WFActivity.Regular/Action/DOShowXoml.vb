Imports System
Imports System.Collections.Generic
Imports System.Net.Mail
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization


<RuleCategory("Workflow"), RuleDescription("Diseñar Archivo Xoml"), RuleHelp("Permite diseñar un archivo Xoml."), RuleFeatures(True)> <Serializable()> _
Public Class DOShowXoml
    Inherits WFRuleParent
    Implements IDOShowXoml
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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal Wfstepid As Int64, ByVal IdAutoMail As Int32, ByVal AddDocument As Boolean, ByVal AddLink As Boolean, ByVal AddIndexs As Boolean, ByVal user As String, ByVal password As String, ByVal port As Integer, ByVal server As String, ByVal indexsNames As String, ByVal groupByMailto As Boolean)
        MyBase.New(Id, Name, Wfstepid)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return results
    End Function
End Class
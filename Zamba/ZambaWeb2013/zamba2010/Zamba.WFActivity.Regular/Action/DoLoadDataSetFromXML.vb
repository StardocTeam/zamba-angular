Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports Zamba.Core.MessagesBusiness
Imports System.Xml.Serialization

<RuleCategory("Datos"), RuleDescription("Cargar DataSet desde XML"), RuleHelp("Carga un DataSet con un XML dado."), RuleFeatures(True)> <Serializable()> _
Public Class DoLoadDataSetFromXML
    Inherits WFRuleParent
    Implements IDoLoadDataSetFromXML

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoLoadDataSetFromXML

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal StartTag As String, ByVal EndTag As String, ByVal XMLSource As String, ByVal DataSetName As String)
        MyBase.New(Id, Name, wfStepId)
        Me._EndTag = EndTag
        Me._StartTag = StartTag
        Me._XMLSource = XMLSource
        Me._DataSetName = DataSetName
        playRule = New Zamba.WFExecution.PlayDoLoadDataSetFromXML(Me)
    End Sub

    Public Overrides Sub Dispose()

    End Sub

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

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Private _EndTag As String
    Private _StartTag As String
    Private _XMLSource As String
    Private _DataSetName As String

    Public Property EndTag() As String Implements Core.IDoLoadDataSetFromXML.EndTag
        Get
            Return _EndTag
        End Get
        Set(ByVal value As String)
            _EndTag = value
        End Set
    End Property

    Public Property StartTag() As String Implements Core.IDoLoadDataSetFromXML.StartTag
        Get
            Return _StartTag
        End Get
        Set(ByVal value As String)
            _StartTag = value
        End Set
    End Property

    Public Property XMLSource() As String Implements Core.IDoLoadDataSetFromXML.XMLSource
        Get
            Return _XMLSource
        End Get
        Set(ByVal value As String)
            _XMLSource = value
        End Set
    End Property

    Public Property DataSetName() As String Implements Core.IDoLoadDataSetFromXML.DataSetName
        Get
            Return _DataSetName
        End Get
        Set(ByVal value As String)
            _DataSetName = value
        End Set
    End Property
End Class
Imports Zamba.Data
Imports zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Indices"), RuleDescription("Insertar vista"), RuleHelp(" Permite actualizar o insertar un registro en una vista"), RuleFeatures(False)> <Serializable()> _
Public Class DoInsertSLST
    Inherits WFRuleParent
    Implements IDoInsertSLST
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _compType As Comparadores
    Private _executeType As String
    Private playRule As Zamba.WFExecution.PlayDoInsertSLST

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
    Private Sub New()
        MyBase.New(0, "", 0)
    End Sub
    Public Overrides Sub Dispose()

    End Sub

    Private _IDSLST As String
    Private _Code As String
    Private _Description As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal IDSLST As String, ByVal Code As String, ByVal Description As String)
        MyBase.New(Id, Name, wfStepId)
        Me._IDSLST = IDSLST
        Me._Code = Code
        Me._Description = Description
        playRule = New Zamba.WFExecution.PlayDoInsertSLST(Me)
    End Sub
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Property IDSLST() As String Implements IDoInsertSLST.IDSLST
        Get
            Return _IDSLST
        End Get
        Set(ByVal value As String)
            _IDSLST = value
        End Set
    End Property
    Public Property Code() As String Implements IDoInsertSLST.Code
        Get
            Return _Code
        End Get
        Set(ByVal value As String)
            _Code = value
        End Set
    End Property
    Public Property Description() As String Implements IDoInsertSLST.Description
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value
        End Set
    End Property
End Class
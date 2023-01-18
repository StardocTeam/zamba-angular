Imports Zamba.Core

<RuleCategory("Base de Datos"), RuleDescription("Insertar Tabla de Datos"), RuleHelp("Permite Insertar un dataset y mostrar los datos obtenidos en una tabla"), RuleFeatures(False)> <Serializable()> _
Public Class DoInsertTable
    Inherits WFRuleParent
    Implements IDoInsertTable

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

    Private m_SQLSelectId As Int32
    Private m_DataSet As Object
    Private m_Table As String

    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal wfStepId As Int64, ByVal DataSetName As String, ByVal TableName As String)
        MyBase.New(ID, Name, WFStepId)
        Me.m_DataSet = DataSetName
        Me.m_Table = TableName
    End Sub

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoInsertTable()
        Return playRule.Play(results, Me)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoInsertTable()
        Return playRule.Play(results, Me)
    End Function
    Public Property DataSet() As Object Implements Core.IDoInsertTable.DataSet
        Get
            Return Me.m_DataSet
        End Get
        Set(ByVal value As Object)
            Me.m_DataSet = value
        End Set
    End Property

    Public Property Table() As String Implements Core.IDoInsertTable.Table
        Get
            Return Me.m_Table
        End Get
        Set(ByVal value As String)
            Me.m_Table = value
        End Set
    End Property
End Class
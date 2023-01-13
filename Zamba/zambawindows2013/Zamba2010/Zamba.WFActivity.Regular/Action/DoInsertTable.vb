Imports Zamba.Core

<RuleMainCategory("Base de Datos"), RuleCategory("Insercion"), RuleSubCategory(""), RuleDescription("Insertar Tabla de Datos"), RuleHelp("Permite Insertar un dataset y mostrar los datos obtenidos en una tabla"), RuleFeatures(False)> <Serializable()> _
Public Class DoInsertTable
    Inherits WFRuleParent
    Implements IDoInsertTable, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoInsertTable
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
        MyBase.New(id, name, wfStepId)
        m_DataSet = DataSetName
        m_Table = TableName

        playRule = New WFExecution.PlayDoInsertTable(Me)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

    Public Property DataSet() As Object Implements Core.IDoInsertTable.DataSet
        Get
            Return m_DataSet
        End Get
        Set(ByVal value As Object)
            m_DataSet = value
        End Set
    End Property

    Public Property Table() As String Implements Core.IDoInsertTable.Table
        Get
            Return m_Table
        End Get
        Set(ByVal value As String)
            m_Table = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
End Class
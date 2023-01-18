Imports Zamba.Core

<RuleMainCategory("Datos"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Cargar DataSet desde XML"), RuleHelp("Carga un DataSet con un XML dado."), RuleFeatures(False)> <Serializable()> _
Public Class DoLoadDataSetFromXML
    Inherits WFRuleParent
    Implements IDoLoadDataSetFromXML

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoLoadDataSetFromXML

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal StartTag As String, ByVal EndTag As String, ByVal XMLSource As String, ByVal DataSetName As String)
        MyBase.New(Id, Name, wfStepId)
        _EndTag = EndTag
        _StartTag = StartTag
        _XMLSource = XMLSource
        _DataSetName = DataSetName
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

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
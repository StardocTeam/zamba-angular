Imports zamba.Core


<RuleMainCategory("Atributos"), RuleCategory(""), RuleSubCategory(""), RuleDescription("Insertar vista de sustitucion"), RuleHelp(" Permite actualizar o insertar un registro en una vista"), RuleFeatures(False)> <Serializable()> _
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

    Public Overrides Sub Dispose()

    End Sub

    Private _IDSLST As String
    Private _Code As String
    Private _Description As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal IDSLST As String, ByVal Code As String, ByVal Description As String)
        MyBase.New(Id, Name, wfStepId)
        _IDSLST = IDSLST
        _Code = Code
        _Description = Description
        playRule = New Zamba.WFExecution.PlayDoInsertSLST(Me)
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

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
Imports Zamba.Core

<RuleMainCategory("Atributos"), RuleCategory(""), Rulesubcategory(""), RuleDescription("Exportar Atributos"), RuleHelp("Genera un archivo de texto con los valores de los atributos seleccionados de la entidad"), RuleFeatures(False)> <Serializable()> _
Public Class DoExport
    Inherits WFRuleParent
    Implements IDoExport, IRuleValidate


    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _DoctypeId As Int64
    Private _separator As String
    Private _resultLine As String
    Private _documentName As String
    Private _documentPath As String
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExport

#Region "Propertys"
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

    Public Property DoctypeId() As Long Implements IDoExport.DoctypeId
        Get
            Return _DoctypeId
        End Get
        Set(ByVal value As Long)
            _DoctypeId = value
        End Set
    End Property


    Private _SortString As String
    Public Property SortString() As String Implements IDoExport.SortString
        Get
            Return _SortString
        End Get
        Set(ByVal value As String)
            _SortString = value
        End Set
    End Property


    Public Property resultLine() As String Implements IDoExport.resultLine
        Get
            Return _resultLine
        End Get
        Set(ByVal value As String)
            _resultLine = value
        End Set
    End Property

    Public Property separator() As String Implements IDoExport.separator
        Get
            Return _separator
        End Get
        Set(ByVal value As String)
            _separator = value
        End Set
    End Property

    Public Property documentName() As String Implements IDoExport.documentName
        Get
            Return _documentName
        End Get
        Set(ByVal value As String)
            _documentName = value
        End Set
    End Property

    Public Property documentPath() As String Implements IDoExport.documentPath
        Get
            Return _documentPath
        End Get
        Set(ByVal value As String)
            _documentPath = value
        End Set
    End Property


    Private _VersionsExportedDocuments As Boolean
    Public Property VersionsExportedDocuments() As Boolean Implements IDoExport.VersionsExportedDocuments
        Get
            Return _VersionsExportedDocuments
        End Get
        Set(ByVal value As Boolean)
            _VersionsExportedDocuments = value
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
#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pDoctypeId As Int64, ByVal pseparator As String, ByVal presultLine As String, ByVal pdocumentName As String, ByVal pdocumentPath As String, ByVal psortString As String, ByVal pVersionsExportedDocuments As Boolean)
        MyBase.New(Id, Name, wfstepid)
        _DoctypeId = pDoctypeId
        _separator = pseparator
        _resultLine = presultLine
        _documentName = pdocumentName
        _documentPath = pdocumentPath
        _SortString = psortString
        _VersionsExportedDocuments = pVersionsExportedDocuments
        playRule = New WFExecution.PlayDoExport(Me)
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)

        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function



End Class

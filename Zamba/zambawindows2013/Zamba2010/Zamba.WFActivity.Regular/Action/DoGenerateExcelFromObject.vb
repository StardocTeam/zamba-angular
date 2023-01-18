Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Generar Excel a partir de una variable"), RuleHelp("Genera un documento de excel a partir de una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateExcelFromObject
    Inherits WFRuleParent
    Implements IDoGenerateExcelFromObject
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _excelName As String
    Public _addColName As Boolean
    Private _datesetName As String
    Private _exportType As ExcelExportTypes
    Private _otherType As String
    Private _path As String
    Private _useSpire As Boolean
    Private playRule As WFExecution.PlayDoGenerateExcelFromObject

    Public Property OtherType() As String Implements IDoGenerateExcelFromObject.OtherFormattype
        Get
            Return _otherType
        End Get
        Set(ByVal value As String)
            _otherType = value
        End Set
    End Property
    Public Property Path() As String Implements IDoGenerateExcelFromObject.Path
        Get
            Return _path
        End Get
        Set(ByVal value As String)
            _path = value
        End Set
    End Property
    Public Property UseSpireConverter() As Boolean Implements IDoGenerateExcelFromObject.UseSpireConverter
        Get
            Return _useSpire
        End Get
        Set(ByVal value As Boolean)
            _useSpire = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal excelName As String, ByVal dsName As String, ByVal colName As Boolean, ByVal exporType As ExcelExportTypes, ByVal OtherFormat As String, ByVal path As String, ByVal UseSpireConverter As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _excelName = excelName
        _datesetName = dsName
        _addColName = colName
        _exportType = exporType
        _otherType = OtherFormat
        _path = path
        playRule = New WFExecution.PlayDoGenerateExcelFromObject(Me)
        _useSpire = UseSpireConverter
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
    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function
    Public Property AddColName() As Boolean Implements Core.IDoGenerateExcelFromObject.AddColName
        Get
            Return _addColName
        End Get
        Set(ByVal value As Boolean)
            _addColName = value
        End Set
    End Property
    Public Property DataSetName() As String Implements Core.IDoGenerateExcelFromObject.DataSetName
        Get
            Return _datesetName
        End Get
        Set(ByVal value As String)
            _datesetName = value
        End Set
    End Property
    Public Property ExcelNAme() As String Implements Core.IDoGenerateExcelFromObject.ExcelNAme
        Get
            Return _excelName
        End Get
        Set(ByVal value As String)
            _excelName = value
        End Set
    End Property
    Public Property ExportType() As Core.ExcelExportTypes Implements Core.IDoGenerateExcelFromObject.ExportType
        Get
            Return _exportType
        End Get
        Set(ByVal value As Core.ExcelExportTypes)
            _exportType = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property
End Class

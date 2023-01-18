Imports Zamba.Core
Imports Zamba.WFExecution


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Obtener datos desde un excel"), RuleHelp("Obtiene los datos de un excel y los guarda en una variable de zamba"), RuleFeatures(False)> <Serializable()> _
Public Class DoImportFromExcel
    Inherits WFRuleParent
    Implements IDoImportFromExcel


#Region "Atributos"

    Private _playrule As PlayDoImportFromExcel
    Private _file As String
    Private _excelVersion As OfficeVersion
    Private _sheetName As String
    Private _varName As String
    Private _saveAsPath As String
    Private _saveAsFileName As String
    Private _saveAs As Boolean
    Private _useSpire As Boolean


#End Region

#Region "Constructores"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal file As String, ByVal excelversion As OfficeVersion, ByVal sheetname As String, ByVal varname As String, ByVal saveAs As Boolean, ByVal saveAsPath As String, ByVal saveAsFileName As String, ByVal UseSpireConverter As Boolean)
        MyBase.New(Id, Name, WFStepid)
        _file = file
        _excelVersion = excelversion
        _sheetName = sheetname
        _varName = varname
        _saveAs = saveAs
        _saveAsPath = saveAsPath
        Me.SaveAsFileName = saveAsFileName
        _playrule = New PlayDoImportFromExcel(Me)
        _useSpire = UseSpireConverter
    End Sub

#End Region

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
        Return _playrule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return _playrule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return _playrule.DiscoverParams()
    End Function

    Public Property File() As String Implements Core.IDoImportFromExcel.File
        Get
            Return _file
        End Get
        Set(ByVal value As String)
            _file = value
        End Set
    End Property

    Public Property SheetName() As String Implements Core.IDoImportFromExcel.SheetName
        Get
            Return _sheetName
        End Get
        Set(ByVal value As String)
            _sheetName = value
        End Set
    End Property

    Public Property ExcelVersion() As OfficeVersion Implements Core.IDoImportFromExcel.ExcelVersion
        Get
            Return _excelVersion
        End Get
        Set(ByVal value As OfficeVersion)
            _excelVersion = value
        End Set
    End Property

    Public Property VarName() As String Implements Core.IDoImportFromExcel.VarName
        Get
            Return _varName
        End Get
        Set(ByVal value As String)
            _varName = value
        End Set
    End Property



    Public Property SaveAs() As Boolean Implements IDoImportFromExcel.SaveAs
        Get
            Return _saveAs
        End Get
        Set(ByVal value As Boolean)
            _saveAs = value
        End Set
    End Property



    Public Property SaveAsPath() As String Implements IDoImportFromExcel.SaveAsPath
        Get
            Return _saveAsPath
        End Get
        Set(ByVal value As String)
            _saveAsPath = value
        End Set
    End Property



    Public Property SaveAsFileName() As String Implements IDoImportFromExcel.SaveAsFileName
        Get
            Return _saveAsFileName
        End Get
        Set(ByVal value As String)
            _saveAsFileName = value
        End Set
    End Property

    Public Property UseSpireConverter() As Boolean Implements IDoImportFromExcel.UseSpireConverter
        Get
            Return _useSpire
        End Get
        Set(ByVal value As Boolean)
            _useSpire = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property
End Class

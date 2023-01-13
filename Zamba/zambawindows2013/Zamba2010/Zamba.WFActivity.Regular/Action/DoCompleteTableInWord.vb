Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Office"), RuleDescription("Completar tabla en Word"), RuleHelp("Permite completar con datos una tabla en un documento word respetando el formato el formato establecido de la misma."), RuleFeatures(False)> <Serializable()> _
Public Class DoCompleteTableInWord
    Inherits WFRuleParent
    Implements IDoCompleteTableInWord, IRuleValidate

#Region "Atributos"

    Private _tableIndex As Int64
    Private _pageIndex As Int64
    Private _fullPath As String
    Private _varName As String
    Private _withHeader As String
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _dataTable As String
    Private _inTable As Boolean
    Private _rownumber As Int64
    Private _fontConfig As Boolean
    Private _font As String
    Private _fontSize As Single
    Private _style As Int32
    Private _color As String
    Private _backColor As String
    Private _saveOriginalPath As Boolean
    Private playRule As WFExecution.PlayDoCompleteTableInWord
    Private _isValid As Boolean
#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal name As String, ByVal wfstepid As Int64, ByVal tableindex As Int64,
                   ByVal pageindex As Int64, ByVal fullpath As String, ByVal varname As String, ByVal withheader As Boolean, ByVal datatable As _
                   String, ByVal intable As Boolean, ByVal rownumber As Int64, ByVal FontConfig As Boolean, ByVal Font As String, ByVal FontSize _
                   As Single, ByVal style As Int32, ByVal color As String, ByVal backColor As String, ByVal saveOriginalPath As Boolean)

        MyBase.New(Id, name, wfstepid)
        _tableIndex = tableindex
        _pageIndex = pageindex
        _fullPath = fullpath
        _varName = varname
        _withHeader = withheader
        _dataTable = datatable
        _inTable = intable
        _rownumber = rownumber
        _fontConfig = FontConfig
        _font = Font
        _fontSize = FontSize
        _style = style
        _color = color
        _backColor = backColor
        _saveOriginalPath = saveOriginalPath
        playRule = New WFExecution.PlayDoCompleteTableInWord(Me)
    End Sub

#End Region

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

    Public Property FullPath As String Implements Core.IDoCompleteTableInWord.FullPath
        Get
            Return _fullPath
        End Get
        Set(value As String)
            _fullPath = value
        End Set
    End Property

    Public Property DataTable As String Implements Core.IDoCompleteTableInWord.DataTable
        Get
            Return _dataTable
        End Get
        Set(value As String)
            _dataTable = value
        End Set
    End Property

    Public Property PageIndex As Long Implements Core.IDoCompleteTableInWord.PageIndex
        Get
            Return _pageIndex
        End Get
        Set(value As Long)
            _pageIndex = value
        End Set
    End Property

    Public Property TableIndex As Long Implements Core.IDoCompleteTableInWord.TableIndex
        Get
            Return _tableIndex
        End Get
        Set(value As Long)
            _tableIndex = value
        End Set
    End Property

    Public Property VarName As String Implements Core.IDoCompleteTableInWord.VarName
        Get
            Return _varName
        End Get
        Set(value As String)
            _varName = value
        End Set
    End Property

    Public Property WithHeader As Boolean Implements Core.IDoCompleteTableInWord.WithHeader
        Get
            Return _withHeader
        End Get
        Set(value As Boolean)
            _withHeader = value
        End Set
    End Property

    Public Property InTable As Boolean Implements Core.IDoCompleteTableInWord.InTable
        Get
            Return _inTable
        End Get
        Set(value As Boolean)
            _inTable = value
        End Set
    End Property

    Public Property RowNumber As Int64 Implements Core.IDoCompleteTableInWord.RowNumber
        Get
            Return _rownumber
        End Get
        Set(value As Int64)
            _rownumber = value
        End Set
    End Property

    Public Property FontConfig As Boolean Implements Core.IDoCompleteTableInWord.FontConfig
        Get
            Return _fontConfig
        End Get
        Set(value As Boolean)
            _fontConfig = value
        End Set
    End Property

    Public Property Font As String Implements Core.IDoCompleteTableInWord.Font
        Get
            Return _font
        End Get
        Set(value As String)
            _font = value
        End Set
    End Property

    Public Property FontSize As Single Implements Core.IDoCompleteTableInWord.FontSize
        Get
            Return _fontSize
        End Get
        Set(value As Single)
            _fontSize = value
        End Set
    End Property

    Public Property Style As Int32 Implements Core.IDoCompleteTableInWord.Style
        Get
            Return _style
        End Get
        Set(value As Int32)
            _style = value
        End Set
    End Property

    Public Property Color As String Implements Core.IDoCompleteTableInWord.Color
        Get
            Return _color
        End Get
        Set(value As String)
            _color = value
        End Set
    End Property

    Public Property BackColor As String Implements Core.IDoCompleteTableInWord.BackColor
        Get
            Return _backColor
        End Get
        Set(value As String)
            _backColor = value
        End Set
    End Property

    Public Property SaveOriginalPath As Boolean Implements Core.IDoCompleteTableInWord.SaveOriginalPath
        Get
            Return _saveOriginalPath
        End Get
        Set(value As Boolean)
            _saveOriginalPath = value
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
            Return "Completo tabla de Word"
        End Get
    End Property

End Class

Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Exportar"), RuleDescription("Generar PDF a partir de un archivo Word"), RuleHelp("Crea un archivo PDF a partir de un documento de Microsoft Word"), RuleFeatures(False)> <Serializable()> _
Public Class DoDocToPDF
    Inherits WFRuleParent
    Implements IDoDocToPDF, IRuleValidate

#Region "Attributes"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _fullPath As String
    Private _fileName As String
    Private _exportTask As Boolean
    Private _UseNewConversion As Boolean
    Private playRule As PlayDoDocToPDF
    Private _isValid As Boolean
#End Region

#Region "Properties"
    Public Overrides ReadOnly Property IsFull As Boolean
        Get
            Return _isFull
        End Get
    End Property

    Public Overrides ReadOnly Property IsLoaded As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Property ExportTask As Boolean Implements IDoDocToPDF.ExportTask
        Get
            Return _exportTask
        End Get
        Set(ByVal value As Boolean)
            _exportTask = value
        End Set
    End Property

    Public Property UseNewConversion As Boolean Implements IDoDocToPDF.UseNewConversion
        Get
            Return _UseNewConversion
        End Get
        Set(ByVal value As Boolean)
            _UseNewConversion = value
        End Set
    End Property

    Public Property FileName As String Implements IDoDocToPDF.FileName
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property

    Public Property FullPath As String Implements IDoDocToPDF.FullPath
        Get
            Return _fullPath
        End Get
        Set(ByVal value As String)
            _fullPath = value
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

#Region "Methods"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal ExportTask As Boolean, ByVal FullPath As String, ByVal FileName As String, ByVal UseNewConversion As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _exportTask = ExportTask
        _fullPath = FullPath
        _fileName = FileName
        _UseNewConversion = UseNewConversion
        playRule = New PlayDoDocToPDF(Me)
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

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub
#End Region
End Class

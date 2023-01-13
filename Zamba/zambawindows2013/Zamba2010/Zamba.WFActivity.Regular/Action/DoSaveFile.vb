Imports Zamba.Core


<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Exportar"), RuleDescription("Guarda en un archivo de texto"), RuleHelp("Guarda texto en un archivo"), RuleFeatures(False)> <Serializable()> _
Public Class DoSaveFile
    Inherits WFRuleParent
    Implements IDOSaveFile

#Region "Atributos"

    Private playRule As Zamba.WFExecution.PlayDoSaveFile


    Private _FilePath As String
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _TextToSave As String
    Private _FileExtension As String
    Private _FileName As String


    Private _docPathVar As String
    Private _saveDocPathVar As String

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

    Public Overloads Overrides Function Play(results As List(Of ITaskResult), refreshTasks As List(Of Long)) As List(Of ITaskResult)
        Return playRule.Play(results)
    End Function


    Public Sub New(ByVal Id As Int64, _
                   ByVal Name As String, _
                   ByVal wfstepId As Int64, _
                   ByVal filePath As String, _
                   ByVal fileext As String, _
                   ByVal texttosave As String, _
                   ByVal DocPathVar As String, _
                   ByVal FileName As String)
        MyBase.New(Id, Name, wfstepId)


        _FilePath = filePath
        _FileExtension = fileext

        _TextToSave = texttosave
        _saveDocPathVar = DocPathVar
        _FileName = FileName






        playRule = New Zamba.WFExecution.PlayDoSaveFile(Me)
    End Sub
    Public Property FileExtension() As String Implements Core.IDOSaveFile.FileExtension
        Get
            Return _FileExtension
        End Get
        Set(ByVal value As String)
            _FileExtension = value
        End Set
    End Property

    Public Property FilePath() As String Implements Core.IDOSaveFile.FilePath
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property


    Public Property TextToSave() As String Implements Core.IDOSaveFile.TextToSave
        Get
            Return _TextToSave
        End Get
        Set(ByVal value As String)
            _TextToSave = value
        End Set
    End Property

    Public Property VarFilePath() As String Implements Core.IDOSaveFile.VarFilePath
        Get
            Return _saveDocPathVar
        End Get
        Set(ByVal value As String)
            _saveDocPathVar = value
        End Set
    End Property

    Public Property FileName() As String Implements Core.IDOSaveFile.FileName
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides Function DiscoverParams() As List(Of String)

    End Function



    Public Overrides Function PlayTest() As Boolean

    End Function
End Class

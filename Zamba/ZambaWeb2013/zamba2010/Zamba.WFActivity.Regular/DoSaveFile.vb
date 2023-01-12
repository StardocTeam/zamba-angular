Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Secciones"), RuleDescription("Guarda en un archivo de texto"), RuleHelp("Guarda texto en un archivo"), RuleFeatures(False)> <Serializable()> _
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


    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
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


        Me._FilePath = filePath
        Me._FileExtension = fileext

        Me._TextToSave = texttosave
        Me._saveDocPathVar = DocPathVar
        Me._FileName = FileName






        Me.playRule = New Zamba.WFExecution.PlayDoSaveFile(Me)
    End Sub
    Public Property FileExtension() As String Implements Core.IDOSaveFile.FileExtension
        Get
            Return Me._FileExtension
        End Get
        Set(ByVal value As String)
            Me._FileExtension = value
        End Set
    End Property

    Public Property FilePath() As String Implements Core.IDOSaveFile.FilePath
        Get
            Return Me._FilePath
        End Get
        Set(ByVal value As String)
            Me._FilePath = value
        End Set
    End Property


    Public Property TextToSave() As String Implements Core.IDOSaveFile.TextToSave
        Get
            Return Me._TextToSave
        End Get
        Set(ByVal value As String)
            Me._TextToSave = value
        End Set
    End Property

    Public Property VarFilePath() As String Implements Core.IDOSaveFile.VarFilePath
        Get
            Return Me._saveDocPathVar
        End Get
        Set(ByVal value As String)
            Me._saveDocPathVar = value
        End Set
    End Property

    Public Property FileName() As String Implements Core.IDOSaveFile.FileName
        Get
            Return Me._FileName
        End Get
        Set(ByVal value As String)
            Me._FileName = value
        End Set
    End Property
End Class

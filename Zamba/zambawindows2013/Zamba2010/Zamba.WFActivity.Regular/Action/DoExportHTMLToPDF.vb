Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Archivos"), RuleSubCategory("Exportar"), RuleDescription("Exportar HTML a PDF"), RuleHelp("Abre una ventana en web para exportar HTML a PDF."), RuleFeatures(False)> <Serializable()> _
Public Class DoExportHTMLToPDF
    Inherits WFRuleParent
    Implements IDoExportHTMLToPDF, IRuleValidate

    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExportHTMLToPDF

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal Content As String, ByVal CanEditable As Boolean, ByVal ReturnFileName As String)
        MyBase.New(Id, Name, wfStepId)
        _Content = Content
        _CanEditable = CanEditable
        _ReturnFileName = ReturnFileName
        playRule = New Zamba.WFExecution.PlayDoExportHTMLToPDF(Me)
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

    Private _Content As String
    Private _CanEditable As Boolean
    Private _ReturnFileName As String

    Public Property Content() As String Implements Core.IDoExportHTMLToPDF.Content
        Get
            Return _Content
        End Get
        Set(ByVal value As String)
            _Content = value
        End Set
    End Property

    Public Property CanEditable() As Boolean Implements Core.IDoExportHTMLToPDF.CanEditable
        Get
            Return _CanEditable
        End Get
        Set(ByVal value As Boolean)
            _CanEditable = value
        End Set
    End Property

    Public Property ReturnFileName() As String Implements Core.IDoExportHTMLToPDF.ReturnFileName
        Get
            Return _ReturnFileName
        End Get
        Set(ByVal value As String)
            _ReturnFileName = value
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

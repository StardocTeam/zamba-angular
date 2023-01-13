Imports Zamba.Core

<RuleMainCategory("Zamba"), RuleCategory("Formularios"), RuleSubCategory(""), RuleDescription("Generar reporte desde formulario"), RuleHelp("Permite generar un reporte, tomando como plantilla un formulario de Zamba del tipo reporte"), RuleFeatures(True)> <Serializable()> _
Public Class DoGenerateHTMLReport
    Inherits WFRuleParent
    Implements IDoGenerateHTMLReport
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoGenerateHTMLReport
    Private _formId As Long
    Private _reportName As String
    Private _reportOrientation As Integer

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

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByRef wfstepId As Int64, ByVal FormId As Long,
                   ByVal ReporName As String, ByVal ReporOrientation As Integer)
        MyBase.New(Id, Name, wfstepId)
        _formId = FormId
        _reportName = ReporName
        _reportOrientation = ReporOrientation
        playRule = New Zamba.WFExecution.PlayDoGenerateHTMLReport(Me)
    End Sub

    Public Property FormId() As Long Implements IDoGenerateHTMLReport.FormId
        Get
            Return _formId
        End Get
        Set(ByVal value As Long)
            _formId = value
        End Set
    End Property

    Public Property ReportName As String Implements IDoGenerateHTMLReport.ReportName
        Get
            Return _reportName
        End Get
        Set(value As String)
            _reportName = value
        End Set
    End Property

    Public Property ReportOrientation As Integer Implements IDoGenerateHTMLReport.ReportOrietation
        Get
            Return _reportOrientation
        End Get
        Set(value As Integer)
            _reportOrientation = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

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
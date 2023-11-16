Imports Zamba.Core

<RuleMainCategory("Archivos y Aplicaciones"), RuleCategory("Aplicaciones"), RuleSubCategory("Reportes"), RuleDescription("Mostrar Reporte"), RuleHelp("Permite mostrar en pantalla un reporte anteriormente generado"), RuleFeatures(False)> <Serializable()> _
Public Class DoExecuteReport
    Inherits WFRuleParent
    Implements IDoReportBuilder, IRuleValidate


    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ReportId As Int32
    Private _isValid As Boolean
    Private playRule As Zamba.WFExecution.PlayDoExecuteReport

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

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property
    Public Property ReportId() As Int32 Implements IDoReportBuilder.ReportId
        Get
            Return _ReportId
        End Get
        Set(ByVal value As Int32)
            _ReportId = value
        End Set
    End Property

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return String.Empty
        End Get
    End Property

#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pReportId As Int64)
        MyBase.New(Id, Name, wfstepid)
        _ReportId = pReportId
        playRule = New WFExecution.PlayDoExecuteReport(Me)
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

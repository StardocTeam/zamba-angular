Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Aplicaciones"), RuleDescription("Mostrar Reporte"), RuleHelp("Permite mostrar en pantalla un reporte anteriormente generado"), RuleFeatures(False)> <Serializable()> _
Public Class DoExecuteReport
    Inherits WFRuleParent
    Implements IDoReportBuilder


    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _ReportId As Int32

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

    Public Property ReportId() As Int32 Implements IDoReportBuilder.ReportId
        Get
            Return _ReportId
        End Get
        Set(ByVal value As Int32)
            _ReportId = value
        End Set
    End Property



#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal pReportId As Int64)
        MyBase.New(Id, Name, wfstepid)
        _ReportId = pReportId
    End Sub

    '[Ezequiel] 09/06/2009 - Se elimino el Try-Catch
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteReport()
        Return playRule.Play(results, Me)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Dim playRule As New Zamba.WFExecution.PlayDoExecuteReport()
        Return playRule.Play(results, Me)
    End Function

End Class


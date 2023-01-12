Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Aplicaciones"), RuleDescription("Generar Excel a partir de una variable"), RuleHelp("Genera un documento de excel a partir de una variable"), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateExcelFromObject
    Inherits WFRuleParent
    Implements IDoGenerateExcelFromObject
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _excelNAme As String
    Public _addColName As Boolean
    Private _datesetName As String
    Private _exportType As ExcelExportTypes
    Private _otherType As String
    Private _path As String
    Private playRule As WFExecution.PlayDoGenerateExcelFromObject

    Public Property OtherType() As String Implements IDoGenerateExcelFromObject.OtherFormattype
        Get
            Return Me._otherType
        End Get
        Set(ByVal value As String)
            Me._otherType = value
        End Set
    End Property
    Public Property Path() As String Implements IDoGenerateExcelFromObject.Path
        Get
            Return Me._path
        End Get
        Set(ByVal value As String)
            Me._path = value
        End Set
    End Property

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal excelName As String, ByVal dsName As String, ByVal colName As Boolean, ByVal exporType As ExcelExportTypes, ByVal OtherFormat As String, ByVal path As String)
        MyBase.New(Id, Name, wfstepId)
        Me._excelNAme = excelName
        Me._datesetName = dsName
        Me._addColName = colName
        Me._exportType = exporType
        Me._otherType = OtherFormat
        Me._path = path
        Me.playRule = New WFExecution.PlayDoGenerateExcelFromObject(Me)
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

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
    Public Property AddColName() As Boolean Implements Core.IDoGenerateExcelFromObject.AddColName
        Get
            Return Me._addColName
        End Get
        Set(ByVal value As Boolean)
            Me._addColName = value
        End Set
    End Property
    Public Property DataSetName() As String Implements Core.IDoGenerateExcelFromObject.DataSetName
        Get
            Return Me._datesetName
        End Get
        Set(ByVal value As String)
            Me._datesetName = value
        End Set
    End Property
    Public Property ExcelNAme() As String Implements Core.IDoGenerateExcelFromObject.ExcelNAme
        Get
            Return Me._excelNAme
        End Get
        Set(ByVal value As String)
            Me._excelNAme = value
        End Set
    End Property
    Public Property ExportType() As Core.ExcelExportTypes Implements Core.IDoGenerateExcelFromObject.ExportType
        Get
            Return Me._exportType
        End Get
        Set(ByVal value As Core.ExcelExportTypes)
            Me._exportType = value
        End Set
    End Property
End Class

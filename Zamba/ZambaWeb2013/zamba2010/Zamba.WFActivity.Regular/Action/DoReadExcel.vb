Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Aplicaciones"), RuleDescription("Obtener datos de un excel"), RuleApproved("True"), RuleHelp("Obtiene datos de una determinada posicion de un archivo excel."), RuleFeatures(False)> <Serializable()>
Public Class DoReadExcel
    Inherits WFRuleParent
    Implements IDoReadExcel

    Private _playrule As Zamba.WFExecution.PlayDoReadExcel
    Private _excelFile As String
    Private _excelData As String
    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfStepId As Int64, ByVal excelFile As String, ByVal excelData As String)
        MyBase.New(Id, Name, wfStepId)
        _playrule = New Zamba.WFExecution.PlayDoReadExcel(Me)
        Me._excelData = excelData
        Me._excelFile = excelFile
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

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Insertando texto en el Excel"
        End Get
    End Property

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult), ByVal refreshTasks As List(Of Int64)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return _playrule.Play(results)
    End Function
    Public Overloads Overrides Function PlayTest() As Boolean
        Return _playrule.PlayTest()
    End Function
    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return _playrule.DiscoverParams()
    End Function
    Public Property ExcelData() As String Implements Core.IDoReadExcel.ExcelData
        Get
            Return Me._excelData
        End Get
        Set(ByVal value As String)
            Me._excelData = value
        End Set
    End Property

    Public Property ExcelFile() As String Implements Core.IDoReadExcel.ExcelFile
        Get
            Return Me._excelFile
        End Get
        Set(ByVal value As String)
            Me._excelFile = value
        End Set
    End Property

End Class

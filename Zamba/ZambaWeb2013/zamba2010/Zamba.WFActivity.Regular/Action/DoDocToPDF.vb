Imports Zamba.Core
Imports System.Xml.Serialization
Imports Zamba.WFExecution

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Bussines
''' Class	 : Core.DOGenerateTaskResult
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla que convierte un documento de word a PDF utilizando una impresora virtual
''' </summary>
''' <remarks>
''' Hereda de WFRuleParent
''' </remarks>
''' <history>
''' 	[AlejandroR]	29/03/2010	Created
'''     [JavierP]        27/08/2012   Ported from BPN branch
'''     [MarceloF]      24/10/2012  Modified to use Spire
''' </history>
''' -----------------------------------------------------------------------------
''' 

<RuleCategory("Archivos"), RuleDescription("Generar PDF a partir de un archivo Word"), RuleHelp("Crea un archivo PDF a partir de un documento de Microsoft Word"), RuleFeatures(False)> <Serializable()> _
Public Class DoDocToPDF
    Inherits WFRuleParent
    Implements IDoDocToPDF

#Region "Attributes"
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _fullPath As String
    Private _fileName As String
    Private _exportTask As Boolean
    Private _UseNewConversion As Boolean
    Private playRule As PlayDoDocToPDF
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
#End Region

#Region "Methods"
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal ExportTask As Boolean, ByVal FullPath As String, ByVal FileName As String, ByVal UseNewConversion As Boolean)
        MyBase.New(Id, Name, wfstepId)
        _exportTask = ExportTask
        _fullPath = FullPath
        _fileName = FileName
        _UseNewConversion = UseNewConversion
        Me.playRule = New PlayDoDocToPDF(Me)
    End Sub

    Public Overrides Function Play(ByVal results As System.Collections.Generic.List(Of Core.ITaskResult)) As System.Collections.Generic.List(Of Core.ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overloads Overrides Function Playweb(ByVal results As System.Collections.Generic.List(Of ITaskResult), ByRef RulePendingEvent As RulePendingEvents, ByRef ExecutionResult As RuleExecutionResult, ByRef Params As Hashtable) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub
#End Region
End Class
Imports System
Imports System.Reflection
Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization

<RuleCategory("Archivos"), RuleDescription("Generar Codigo de Barras"), RuleApproved("True"), RuleHelp("Permite generar un codigo de barras en un archivo"), RuleFeatures(True)> <Serializable()> _
Public Class DOGenerateBarcode
    Inherits WFRuleParent
    Implements IDOGenerateBarcode
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private _fileExtension As String
    Private _filePath As String
    Private _barcode As String
    Private _height As String
    Private playRule As Zamba.WFExecution.PlayDoGenerateBarcode
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
    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByRef wfstepId As Int64, ByVal barcode As String, ByVal filePath As String, ByVal fileExtension As String, ByVal Height As String)
        MyBase.New(Id, Name, wfstepId)
        Me._filePath = filePath
        Me._barcode = barcode
        Me._fileExtension = fileExtension
        Me._height = Height
        Me.playRule = New Zamba.WFExecution.PlayDoGenerateBarcode(Me)
    End Sub
    Public Property Barcode() As String Implements IDOGenerateBarcode.Barcode
        Get
            Return Me._barcode
        End Get
        Set(ByVal value As String)
            _barcode = value
        End Set
    End Property
    Public Property FilePath() As String Implements IDOGenerateBarcode.FilePath
        Get
            Return _filePath
        End Get
        Set(ByVal value As String)
            _filePath = value
        End Set
    End Property
    Public Property FileExtension() As String Implements IDOGenerateBarcode.FileExtension
        Get
            Return _fileExtension
        End Get
        Set(ByVal value As String)
            _fileExtension = value
        End Set
    End Property
    Public Property Height() As String Implements IDOGenerateBarcode.Height
        Get
            Return _height
        End Get
        Set(ByVal value As String)
            _height = value
        End Set
    End Property

    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function

    Public Overrides ReadOnly Property MaskName() As String
        Get
            Return "Generar Codigo de Barras"
        End Get
    End Property
End Class
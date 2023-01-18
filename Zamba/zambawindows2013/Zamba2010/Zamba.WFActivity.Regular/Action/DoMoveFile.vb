Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization
''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Bussines
''' Class	 : Core.DoChangeState
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Regla para modificar el estado de una tarea
''' </summary>
''' <remarks>
''' Hereda WFRuleParent
''' </remarks>
''' <history>
''' 	[Hernan]	29/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
<RuleCategory("Archivos"), RuleDescription("Mover Archivo"), RuleApproved("True"), RuleHelp("Permite mover un archivo de una ruta especifica a otra"), RuleFeatures(False)> <Serializable()> _
Public Class DoMoveFile
    Inherits WFRuleParent
    Implements IDoMoveFile
    Public Overrides Sub Dispose()

    End Sub
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playRule As Zamba.WFExecution.PlayDoMoveFile
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

    'Properties
    Private _inputRoute As String
    Private _fileName As String
    Private _outputRoute As String

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal InputRoute As String, ByVal FileName As String, ByVal OutputRoute As String)
        MyBase.New(Id, Name, wfstepId)
        _inputRoute = InputRoute
        _fileName = FileName
        _outputRoute = OutputRoute
        Me.playRule = New Zamba.WFExecution.PlayDoMoveFile(Me)
    End Sub

    Public Property InputRoute() As String Implements IDoMoveFile.InputRoute
        Get
            Return _inputRoute
        End Get
        Set(ByVal value As String)
            _inputRoute = value
        End Set
    End Property
    Public Property OutputRoute() As String Implements IDoMoveFile.OutputRoute
        Get
            Return _outputRoute
        End Get
        Set(ByVal value As String)
            _outputRoute = value
        End Set
    End Property
    Public Property FileName() As String Implements IDoMoveFile.FileName
        Get
            Return _fileName
        End Get
        Set(ByVal value As String)
            _fileName = value
        End Set
    End Property
    Public Overrides ReadOnly Property MaskName() As String
        Get
            If String.IsNullOrEmpty(Me.Name) Then
                Return "Mover Archivo"
            End If
        End Get
    End Property
    Public Overloads Overrides Function Play(ByVal results As System.Collections.Generic.List(Of ITaskResult)) As System.Collections.Generic.List(Of ITaskResult)
        Return playRule.Play(results)
    End Function
End Class


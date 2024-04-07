﻿Imports Zamba.Core

<RuleMainCategory("Tareas"), RuleDescription("Indexa un documento"), RuleHelp("Permite indexar el texto del documento y el de sus indices"), RuleFeatures(False)> <Serializable()> _
Public Class DoIndexFile
    Inherits WFRuleParent
    Implements IDoIndexFile

    Private _docTypeId As String
    Private _docId As String
    Private _documentPath As String
    Private _varName As String

    Private _isLoaded As Boolean
    Private _isFull As Boolean

    Private playRule As Zamba.WFExecution.PlayDoIndexFile

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64, ByVal DocTypeId As String, ByVal DocId As String, ByVal DocumentPath As String, VarName As String)
        MyBase.New(Id, Name, wfstepid)

        Me.DocTypeId = DocTypeId
        Me.DocId = DocId
        Me.DocumentPath = DocumentPath
        _varName = VarName
        playRule = New Zamba.WFExecution.PlayDoIndexFile(Me)
    End Sub

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepid As Int64)
        MyBase.New(Id, Name, wfstepid)
    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)


        Return (playRule.Play(results))
    End Function

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overrides Sub Dispose()

    End Sub

    Public Property DocId() As String Implements IDoIndexFile.DocId
        Get
            Return (_docId)
        End Get

        Set(ByVal value As String)
            _docId = value
        End Set
    End Property

    Public Property DocTypeId() As String Implements IDoIndexFile.DocTypeId
        Get
            Return (_docTypeId)
        End Get

        Set(ByVal value As String)
            _docTypeId = value
        End Set
    End Property

    Public Property DocumentPath() As String Implements IDoIndexFile.DocumentPath
        Get
            Return (_documentPath)
        End Get

        Set(ByVal value As String)
            _documentPath = value
        End Set
    End Property

    Public Property VarName() As String Implements IDoIndexFile.VarName
        Get
            Return (_varName)
        End Get

        Set(ByVal value As String)
            _varName = value
        End Set
    End Property

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

    Public Overrides ReadOnly Property MaskName As String
        Get
              Return String.Empty
        End Get
    End Property

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playRule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playRule.DiscoverParams()
    End Function

End Class
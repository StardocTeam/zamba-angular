﻿Imports Zamba.Data
Imports Zamba.Core
Imports System.Xml.Serialization


<RuleCategory("Aplicaciones"), RuleDescription("Generar Plantilla de Word"), RuleHelp("Crea un documento a partir del entidad seleccionado y al mismo le asigna la plantilla de word seleccionada."), RuleFeatures(False)> <Serializable()> _
Public Class DoGenerateBarcodeInWord
    Inherits WFRuleParent
    Implements IDoGenerateBarcodeInWord


#Region "Atributos"

    Private playRule As Zamba.WFExecution.PlayDoGenerateBarcodeInWord
    Private _docTypeId As Int64
    Private _Indices As String
    Private _FilePath As String
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _top As String
    Private _left As String
    Private _continueWithCurrentTasks As Boolean
    Private _autoPrint As Boolean
    Private _insertBarcode As Boolean
    Private _docPathVar As String
    Private _saveDocPathVar As Boolean
    Private _withoutInsert As Boolean

#End Region

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal wfstepId As Int64, ByVal docTypeId As Int64, ByVal indices As String, ByVal filePath As String, ByVal top As String, ByVal left As String, ByVal continueWithCurrentTasks As Boolean, ByVal autoPrint As Boolean, ByVal insertBarcode As Boolean, ByVal saveDocPathVar As Boolean, ByVal DocPathVar As String, ByVal withoutInsert As Boolean)
        MyBase.New(Id, Name, wfstepId)
        Me._docTypeId = docTypeId
        Me._FilePath = filePath
        Me._Indices = indices
        Me._left = left
        Me._top = top
        Me._continueWithCurrentTasks = continueWithCurrentTasks
        Me._autoPrint = autoPrint
        Me._insertBarcode = insertBarcode
        Me._saveDocPathVar = saveDocPathVar
        Me._docPathVar = DocPathVar
        Me._withoutInsert = withoutInsert
        Me.playRule = New Zamba.WFExecution.PlayDoGenerateBarcodeInWord(Me)
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
    Public Property docTypeId() As Long Implements Core.IDoGenerateBarcodeInWord.docTypeId
        Get
            Return Me._docTypeId
        End Get
        Set(ByVal value As Long)
            Me._docTypeId = value
        End Set
    End Property

    Public Property FilePath() As String Implements Core.IDoGenerateBarcodeInWord.FilePath
        Get
            Return Me._FilePath
        End Get
        Set(ByVal value As String)
            Me._FilePath = value
        End Set
    End Property

    Public Property Indices() As String Implements Core.IDoGenerateBarcodeInWord.Indices
        Get
            Return Me._Indices
        End Get
        Set(ByVal value As String)
            Me._Indices = value
        End Set
    End Property

    Public Property Top() As String Implements Core.IDoGenerateBarcodeInWord.Top
        Get
            Return Me._top
        End Get
        Set(ByVal value As String)
            Me._top = value
        End Set
    End Property

    Public Property Left() As String Implements Core.IDoGenerateBarcodeInWord.Left
        Get
            Return Me._left
        End Get
        Set(ByVal value As String)
            Me._left = value
        End Set
    End Property

    Public Property AutoPrint() As Boolean Implements Core.IDoGenerateBarcodeInWord.AutoPrint
        Get
            Return Me._autoPrint
        End Get
        Set(ByVal value As Boolean)
            Me._autoPrint = value
        End Set
    End Property

    Public Property ContinueWithCurrentTasks() As Boolean Implements Core.IDoGenerateBarcodeInWord.ContinueWithCurrentTasks
        Get
            Return Me._continueWithCurrentTasks
        End Get
        Set(ByVal value As Boolean)
            Me._continueWithCurrentTasks = value
        End Set
    End Property

    Public Property InsertBarcode() As Boolean Implements Core.IDoGenerateBarcodeInWord.InsertBarcode
        Get
            Return Me._insertBarcode
        End Get
        Set(ByVal value As Boolean)
            Me._insertBarcode = value
        End Set
    End Property

    Public Property DocPathVar() As String Implements Core.IDoGenerateBarcodeInWord.DocPathVar
        Get
            Return Me._docPathVar
        End Get
        Set(ByVal value As String)
            Me._docPathVar = value
        End Set
    End Property

    Public Property SaveDocPathVar() As Boolean Implements Core.IDoGenerateBarcodeInWord.SaveDocPathVar
        Get
            Return Me._saveDocPathVar
        End Get
        Set(ByVal value As Boolean)
            Me._saveDocPathVar = value
        End Set
    End Property

    Public Property WithoutInsert() As Boolean Implements Core.IDoGenerateBarcodeInWord.WithoutInsert
        Get
            Return _withoutInsert
        End Get
        Set(ByVal value As Boolean)
            _withoutInsert = value
        End Set
    End Property
End Class
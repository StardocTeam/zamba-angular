﻿Imports Zamba.Core
Imports Zamba.WFExecution

<RuleMainCategory("Documentos y Asociados"), RuleCategory("Documentos Asociados"), RuleSubCategory("Asociar nuevo"), RuleDescription("Agregar archivo a Documento"), RuleHelp("Agrega un archivo a un documento con sus atributos en blanco."), RuleFeatures(True)> <Serializable()> _
Public Class DoAttachToDocument
    Inherits WFRuleParent
    Implements IDoAttachToDocument, IRuleValidate


#Region "Atributos"

    Private _docTypeId As Int64
    Private _limitKB As String
    Private _withLimith As Boolean
    Private _isLoaded As Boolean
    Private _isFull As Boolean
    Private playrule As PlayDoAttachToDocument
    Private _currentSize As String
    Private _isValid As Boolean

#End Region

#Region "Constructor"

    Public Sub New(ByVal Id As Int64, ByVal Name As String, ByVal WFStepid As Int64, ByVal doctypeid As Int64, ByVal limitkb As String, ByVal withlimith As Boolean, ByVal currentsize As String)
        MyBase.New(Id, Name, WFStepid)
        _docTypeId = doctypeid
        _limitKB = limitkb
        _withLimith = withlimith
        _currentSize = currentsize
        playrule = New PlayDoAttachToDocument(Me)
    End Sub

#End Region

#Region "Metodos Publicos"

    Public Overrides Sub Dispose()

    End Sub

    Public Overrides Sub FullLoad()

    End Sub

    Public Overrides Sub Load()

    End Sub

    Public Overloads Overrides Function Play(ByVal results As List(Of ITaskResult), ByVal refreshTasks As List(Of Int64)) As List(Of ITaskResult)
        Return playrule.Play(results)
    End Function

    Public Overloads Overrides Function PlayTest() As Boolean
        Return playrule.PlayTest()
    End Function

    Public Overloads Overrides Function DiscoverParams() As List(Of String)
        Return playrule.DiscoverParams()
    End Function

#End Region


#Region "Propiedades"

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

    Public Property DocTypeId() As Long Implements Core.IDoAttachToDocument.DocTypeId
        Get
            Return _docTypeId
        End Get
        Set(ByVal value As Long)
            _docTypeId = value
        End Set
    End Property

    Public Property LimitKB() As String Implements Core.IDoAttachToDocument.LimitKB
        Get
            Return _limitKB
        End Get
        Set(ByVal value As String)
            _limitKB = value
        End Set
    End Property

    Public Property CurrentSize() As String Implements IDoAttachToDocument.CurrentSize
        Get
            Return _currentSize
        End Get
        Set(ByVal value As String)
            _currentSize = value
        End Set
    End Property

    Public Property WithLimit() As Boolean Implements Core.IDoAttachToDocument.WithLimit
        Get
            Return _withLimith
        End Get
        Set(ByVal value As Boolean)
            _withLimith = value
        End Set
    End Property

    Public Property IsValid As Boolean Implements IRuleValidate.isValid
        Get
            Return _isValid
        End Get
        Set(ByVal value As Boolean)
            _isValid = value
        End Set
    End Property

#End Region

    Public Overrides ReadOnly Property MaskName As String
        Get
            Return "Adjunta Archivos"
        End Get
    End Property

End Class
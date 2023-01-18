Imports System.Collections.Generic
Imports Zamba.Core

<Serializable()>
Public Class DocType
    Inherits ZambaCore
    Implements IDocType, ICloneable

#Region "Atributos"
    Private _indexs As List(Of IIndex) = Nothing
    Private _isReadOnly As Boolean = False
    Private _isReindex As Boolean = False
    Private _isShared As Boolean = False
    Private _rightsLoaded As Boolean = False
    Private _fileFormatId As Integer = 1
    Private _diskGroupId As Integer
    Private _thumbnails As Integer
    Private _autoNameCode As String = String.Empty
    Private _autoNameText As String = String.Empty
    Private _docCount As Int32
    Private _docTypeGroupId As Int32
    Private _documentalId As Int32
    Private _workFlowId As Int32
    Private _TemplateId As Int32
    Private _typeid As Int32
    Private _isFull As Boolean
    Private _isLoaded As Boolean
    Private _IndexDefaultValues As System.Collections.Generic.Dictionary(Of Int64, String)
    ' Public Icons As DocTypeIcons
    ' Public Indexs() As IIndex
#End Region

#Region "Propiedades"
    Public Property AutoNameCode() As String Implements IDocType.AutoNameCode
        Get
            Return _autoNameCode
        End Get
        Set(ByVal value As String)
            _autoNameCode = value
        End Set
    End Property
    Public Property AutoNameText() As String Implements IDocType.AutoNameText
        Get
            Return _autoNameText
        End Get
        Set(ByVal value As String)
            _autoNameText = value
        End Set
    End Property
    Public Property DiskGroupId() As Integer Implements IDocType.DiskGroupId
        Get
            Return _diskGroupId
        End Get
        Set(ByVal value As Integer)
            _diskGroupId = value
        End Set
    End Property
    Public Property DocCount() As Int32 Implements IDocType.DocCount
        Get
            Return _docCount
        End Get
        Set(ByVal value As Int32)
            _docCount = value
        End Set
    End Property
    Public Property DocTypeGroupId() As Int32 Implements IDocType.DocTypeGroupId
        Get
            Return _docTypeGroupId
        End Get
        Set(ByVal value As Int32)
            _docTypeGroupId = value
        End Set
    End Property
    Public Property DocumentalId() As Int32 Implements IDocType.DocumentalId
        Get
            Return _documentalId
        End Get
        Set(ByVal value As Int32)
            _documentalId = value
        End Set
    End Property
    Public Property FileFormatId() As Integer Implements IDocType.FileFormatId
        Get
            Return _fileFormatId
        End Get
        Set(ByVal value As Integer)
            _fileFormatId = value
        End Set
    End Property
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' ArrayList de Objetos indexs
    ''' </summary>
    ''' <value></value>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	29/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Property Indexs() As List(Of IIndex) Implements IDocType.Indexs
        Get
            If IsNothing(_indexs) Then CallForceLoad(Me)
            If IsNothing(_indexs) Then _indexs = New List(Of IIndex)

            Return _indexs
        End Get
        Set(ByVal Value As List(Of IIndex))
            _indexs = Value
        End Set
    End Property
    ''' <summary>
    ''' coleccion que contiene con ids de atributos y sus valores por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>[Diego]  04-07-2008 created</history>
    Public Property IndexsDefaultValues() As System.Collections.Generic.Dictionary(Of Int64, String) Implements IDocType.IndexsDefaultValues
        Get
            Return _IndexDefaultValues
        End Get
        Set(ByVal value As System.Collections.Generic.Dictionary(Of Int64, String))
            _IndexDefaultValues = value
        End Set
    End Property
    Public Property IsReadOnly() As Boolean Implements IDocType.IsReadOnly
        Get
            Return _isReadOnly
        End Get
        Set(ByVal value As Boolean)
            _isReadOnly = value
        End Set
    End Property
    Public Property IsReindex() As Boolean Implements IDocType.IsReindex
        Get
            Return _isReindex
        End Get
        Set(ByVal value As Boolean)
            _isReindex = value
        End Set
    End Property
    Public Property IsShared() As Boolean Implements IDocType.IsShared
        Get
            Return _isShared
        End Get
        Set(ByVal value As Boolean)
            _isShared = value
        End Set
    End Property
    Public Property RightsLoaded() As Boolean Implements IDocType.RightsLoaded
        Get
            Return _rightsLoaded
        End Get
        Set(ByVal value As Boolean)
            _rightsLoaded = value
        End Set
    End Property
    Public Property Thumbnails() As Integer Implements IDocType.Thumbnails
        Get
            Return _thumbnails
        End Get
        Set(ByVal value As Integer)
            _thumbnails = value
        End Set
    End Property
    Public Property WorkFlowId() As Int32 Implements IDocType.WorkFlowId
        Get
            Return _workFlowId
        End Get
        Set(ByVal value As Int32)
            _workFlowId = value
        End Set
    End Property
    Public Property TemplateId() As Int32 Implements IDocType.TemplateId
        Get
            Return _TemplateId
        End Get
        Set(ByVal value As Int32)
            _TemplateId = value
        End Set
    End Property
    Public Property typeid() As Int32 Implements IDocType.typeid
        Get
            Return _typeid
        End Get
        Set(ByVal value As Int32)
            _typeid = value
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

    Public Property SearchTermGroup As Integer Implements IDocType.SearchTermGroup


    Public Property IsSearchTermGroupParent As Boolean Implements IDocType.IsSearchTermGroupParent


#End Region

#Region "Constructores"
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal id As Integer)
        Me.New()

        Me.ID = id
    End Sub
    Public Sub New(ByVal id As Integer, ByVal name As String, ByVal iconId As Int32)
        Me.New(id)
        Me.Name = name
        Me.IconId = iconId
    End Sub
    Public Sub New(ByVal id As Integer, ByVal name As String, ByVal fileFormatId As Integer, ByVal diskGroupId As Integer, ByVal thumbnails As Integer, ByVal iconId As Integer, ByVal objectTypeId As Integer, ByVal autoNameCode As String, ByVal autoNameText As String, ByVal docTypeGroupId As Int32)
        Me.New(id, name, iconId)


        Me.FileFormatId = fileFormatId
        Me.DiskGroupId = diskGroupId
        Me.Thumbnails = thumbnails
        Me.ObjecttypeId = objectTypeId
        Me.AutoNameCode = autoNameCode
        Me.AutoNameText = autoNameText
        'Me.DocCount = docCount
        Me.DocTypeGroupId = docTypeGroupId
        'Me.DocumentalId = documentalId
    End Sub

    'TODO Falta hacer new para nuevo completo con todos los datos y autosalvable, con doctypeid y auto descriptible
#End Region

    Public Overrides Sub FullLoad()
        'If Not _isFull AndAlso ID <> 0 Then
        '    'Cargar Index
        'End If
    End Sub
    Public Overrides Sub Load()
        'If Not _isLoaded AndAlso ID <> 0 Then

        'End If
    End Sub

    Public Overrides Sub Dispose()
        If (Not IsNothing(_indexs)) Then
            _indexs.Clear()
            _indexs = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Clona el DocType generando una nueva instancia sin hacer referencia al objeto actual.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>
    ''' [Ezequiel] - 23/11/09 - Created
    ''' </history>
    Public Function Clone() As Object Implements System.ICloneable.Clone
        Return ZClone.CloneObject(Me)
    End Function
End Class

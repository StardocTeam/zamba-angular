

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Asociados
''' 
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase utilizada para Documentos Asociados 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Hernan]	26/05/2006	Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Asociados
    Inherits ZClass
    Implements IAsociados

#Region "Atributos"
    Private _docType1 As Int64
    Private _docType2 As Int64
    Private _index1 As IIndex = Nothing
    Private _index2 As IIndex = Nothing
    Private _description As String = String.Empty
    Private _parentId As Int64
    Private _parentType As DocAsocRelation
    Private _indexKey As Int64
#End Region

#Region "Propiedades"
    Public Property Description() As String Implements IAsociados.Description
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property DocType2() As Int64 Implements IAsociados.DocType2
        Get
            Return _docType2
        End Get
        Set(ByVal value As Int64)
            _docType2 = value
        End Set
    End Property
    Public Property DocType1() As Int64 Implements IAsociados.DocType1
        Get
            Return _docType1
        End Get
        Set(ByVal value As Int64)
            _docType1 = value
        End Set
    End Property
    Public Property Index2() As IIndex Implements IAsociados.Index2
        Get
            Return _index2
        End Get
        Set(ByVal value As IIndex)
            _index2 = value
        End Set
    End Property
    Public Property Index1() As IIndex Implements IAsociados.Index1
        Get
            Return _index1
        End Get
        Set(ByVal value As IIndex)
            _index1 = value
        End Set
    End Property
    Public Property ParentId() As Int64 Implements IAsociados.ParentId
        Get
            Return _parentId
        End Get
        Set(ByVal value As Int64)
            _parentId = value
        End Set
    End Property
    Public Property ParentType() As DocAsocRelation Implements IAsociados.ParentType
        Get
            Return _parentType
        End Get
        Set(ByVal value As DocAsocRelation)
            _parentType = value
        End Set
    End Property
    Public Property IndexKey() As Int64 Implements IAsociados.IndexKey
        Get
            Return _indexKey
        End Get
        Set(ByVal value As Int64)
            _indexKey = value
        End Set
    End Property
#End Region

#Region "Constructores"
    Public Sub New()
    End Sub
    Public Sub New(ByVal docTypeId1 As Int64, ByVal docTypeId2 As Int64, ByVal index1 As Index, ByVal index2 As Index)
        MyBase.New()
        _docType1 = docTypeId1
        _docType2 = docTypeId2
        _index1 = index1
        _index2 = index2
    End Sub
    Public Sub New(ByVal DocType1 As Int64, ByVal DocType2 As Int64)
        MyBase.New()
        _docType1 = DocType1
        _docType2 = DocType2
        If Not IsNothing(DocType1) AndAlso Not IsNothing(DocType2) Then
            ' _description = DocType1.Name & " - " & DocType2.Name
        End If
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DocType1">Documento Origen de la asociacion</param>
    ''' <param name="DocType2">Documento Asociado al Documento Origen</param>
    ''' <param name="Index1">Indice del Documento Origen que se vincular� al indice del documento destino</param>
    ''' <param name="index2">Indice del documento destino que se va a asociar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal docType1 As Int64, ByVal docType2 As Int64, ByVal index1 As Index, ByVal index2 As Index, ByVal parentType As DocAsocRelation, ByVal indexKey As Int64)
        MyBase.New()
        _docType1 = docType1
        _docType2 = docType2
        _index1 = index1
        _index2 = index2
        _parentType = parentType
        _indexKey = indexKey
        Select Case parentType
            Case DocAsocRelation.Bidireccional
                _parentId = 0
            Case DocAsocRelation.DocType1
                _parentId = _docType1
            Case DocAsocRelation.DocType2
                _parentId = _docType2
        End Select
        If Not IsNothing(index1.Name) AndAlso Not IsNothing(index2.Name) Then
            ' _description = docType1.Name.Trim & "/" & index1.Name.Trim & " - " & docType2.Name.Trim & "/" & index2.Name.Trim
        End If
    End Sub
    'Public Sub New(ByVal DocType1 As DocType, ByVal DocType2 As DocType)
    '    MyBase.new()
    '    _docType1 = DocType1
    '    _docType2 = DocType2

    '    If Not IsNothing(DocType1) AndAlso Not IsNothing(DocType2) Then
    '        _description = DocType1.Name & " - " & DocType2.Name
    '    End If
    'End Sub
#End Region

    Public Overrides Sub Dispose()
        _docType1 = Nothing
        _docType2 = Nothing
        _index1 = Nothing
        _index2 = Nothing
    End Sub

End Class
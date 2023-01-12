

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
    Private _docTypeId1 As Int64 = Nothing
    Private _docTypeId2 As Int64 = Nothing
    Private _index1 As IIndex = Nothing
    Private _index2 As IIndex = Nothing

#End Region

#Region "Propiedades"

    Public Property DocTypeId2() As Int64 Implements IAsociados.DocTypeId2
        Get
            Return _docTypeId2
        End Get
        Set(ByVal value As Int64)
            _docTypeId2 = value
        End Set
    End Property
    Public Property DocTypeId1() As Int64 Implements IAsociados.DocTypeID1
        Get
            Return _docTypeId1
        End Get
        Set(ByVal value As Int64)
            _docTypeId1 = value
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
#End Region

#Region "Constructores"
    Public Sub New()
    End Sub
    ''' -----------------------------------------------------------------------------
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="DocType1">Documento Origen de la asociacion</param>
    ''' <param name="DocType2">Documento Asociado al Documento Origen</param>
    ''' <param name="Index1">Indice del Documento Origen que se vinculará al indice del documento destino</param>
    ''' <param name="index2">Indice del documento destino que se va a asociar</param>
    ''' <remarks>
    ''' </remarks>
    ''' <history>
    ''' 	[Hernan]	26/05/2006	Created
    ''' </history>
    ''' -----------------------------------------------------------------------------
    Public Sub New(ByVal docTypeId1 As Int64, ByVal docTypeId2 As Int64, ByVal index1 As Index, ByVal index2 As Index)
        MyBase.New()
        _docTypeId1 = docTypeId1
        _docTypeId2 = docTypeId2
        _index1 = index1
        _index2 = index2
        _index1.Operator = "="
        _index2.Operator = "="
    End Sub
    Public Sub New(ByVal DocTypeId1 As Int64, ByVal DocTypeID2 As Int64)
        MyBase.New()
        _docTypeId1 = DocTypeId1
        _docTypeId2 = DocTypeID2

    End Sub
#End Region

    Public Overrides Sub Dispose()

    End Sub

End Class

''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Section
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase utilizada para las secciones de los formularios virtuales 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tomas]     13/03/09    Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Section

#Region "Atributos"

    Private _sectionId As Int64
    Private _indexId As Int64
    Private _row As Byte

#End Region

#Region "Propiedades"

    Public Property SectionId() As Int64
        Get
            Return _sectionId
        End Get
        Set(ByVal value As Int64)
            _sectionId = value
        End Set
    End Property

    Public Property IndexId() As Int64
        Get
            Return _indexId
        End Get
        Set(ByVal value As Int64)
            _indexId = value
        End Set
    End Property

    Public Property Row() As Byte
        Get
            Return _row
        End Get
        Set(ByVal value As Byte)
            _row = value
        End Set
    End Property

#End Region

#Region "Constructores"

    Public Sub New()

    End Sub

    Public Sub New(ByVal SectionId As Int64, ByVal IndexId As Int64, ByVal Row As Byte)

        _sectionId = SectionId
        _indexId = IndexId
        _row = Row

    End Sub

#End Region

End Class

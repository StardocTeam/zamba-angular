''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.IndexDescription
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase utilizada para la descripción de los atributos de los 
''' formularios virtuales
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tomas]     12/03/09    Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class IndexDescription

#Region "Atributos"

    Private _indexId As Int64
    Private _type As formIndexDescriptionType
    Private _value As String

#End Region

#Region "Propiedades"

    Public Property IndexId() As Int64
        Get
            Return _indexId
        End Get
        Set(ByVal value As Int64)
            _indexId = value
        End Set
    End Property

    Public Property Type() As formIndexDescriptionType
        Get
            Return _type
        End Get
        Set(ByVal value As formIndexDescriptionType)
            _type = value
        End Set
    End Property

    Public Property Value() As String
        Get
            Return _value
        End Get
        Set(ByVal value As String)
            _value = value
        End Set
    End Property

#End Region

#Region "Constructores"

    Public Sub New()

    End Sub

    Public Sub New(ByVal IndexId As Int64, ByVal Type As formIndexDescriptionType, ByVal Value As String)

        _indexId = IndexId
        _type = Type
        _value = Value

    End Sub

#End Region

End Class

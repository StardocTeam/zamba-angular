''' -----------------------------------------------------------------------------
''' Project	 : Zamba.Core
''' Class	 : Core.Condition
''' -----------------------------------------------------------------------------
''' <summary>
''' Clase utilizada para las condiciones de los formularios virtuales 
''' </summary>
''' <remarks>
''' </remarks>
''' <history>
''' 	[Tomas]     12/03/09    Created
''' </history>
''' -----------------------------------------------------------------------------
Public Class Condition

#Region "Atributos"

    Private _indexId As Int64
    Private _comparisonOperator As String
    Private _relationalOperator As String
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

    Public Property ComparisonOperator() As String
        Get
            Return _comparisonOperator
        End Get
        Set(ByVal value As String)
            _comparisonOperator = value
        End Set
    End Property

    Public Property RelationalOperator() As String
        Get
            Return _relationalOperator
        End Get
        Set(ByVal value As String)
            _relationalOperator = value
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

    Public Sub New(ByVal IndexId As Int64, ByVal ComparisonOperator As String, ByVal RelationalOperator As String, ByVal Value As String)

        _indexId = IndexId
        _comparisonOperator = ComparisonOperator
        _relationalOperator = RelationalOperator
        _value = Value

    End Sub

#End Region

End Class

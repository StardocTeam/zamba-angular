Public Class Modelo

    Private _codigo As Integer
    Private _tipoVehiculo As TipoVehiculo

    Public Property Codigo() As Integer
        Get
            Return _codigo
        End Get
        Set(ByVal value As Integer)
            _codigo = value
        End Set
    End Property

    Public Property TipoVehiculo() As TipoVehiculo
        Get
            Return _tipoVehiculo
        End Get
        Set(ByVal value As TipoVehiculo)
            _tipoVehiculo = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal codigo As Integer)
        _codigo = codigo
        _tipoVehiculo = New TipoVehiculo(codigo)
    End Sub

End Class
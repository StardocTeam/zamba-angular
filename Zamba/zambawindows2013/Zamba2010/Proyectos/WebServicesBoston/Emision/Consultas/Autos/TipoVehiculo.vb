Public Class TipoVehiculo

    Private _codigo As Integer

    Public Property Codigo() As Integer
        Get
            Return _codigo
        End Get
        Set(ByVal value As Integer)
            _codigo = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal codigo As Integer)
        _codigo = codigo
    End Sub

End Class

Public Class SolicitudEmision

    Private _numeroPropuesta As String
    Private _cotizacion As Cotizacion
    Private _numeroEndoso As Integer

    Public Property NumeroPropuesta() As String
        Get
            Return _numeroPropuesta
        End Get
        Set(ByVal value As String)
            _numeroPropuesta = value
        End Set
    End Property

    Public Property Cotizacion() As Cotizacion
        Get
            Return _cotizacion
        End Get
        Set(ByVal value As Cotizacion)
            _cotizacion = value
        End Set
    End Property

    Public Property NumeroEndoso() As Integer
        Get
            Return _numeroEndoso
        End Get
        Set(ByVal value As Integer)
            _numeroEndoso = value
        End Set
    End Property

End Class

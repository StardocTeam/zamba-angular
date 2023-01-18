Public Class Cotizacion

    Private _nroPoliza As Integer

    Public Property NroPoliza() As Integer
        Get
            Return _nroPoliza
        End Get
        Set(ByVal value As Integer)
            _nroPoliza = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal NroPoliza As Integer)
        _nroPoliza = NroPoliza
    End Sub

End Class

Public Class Uso

    Private _codigoSise As Integer

    Public Property CodigoSise() As Integer
        Get
            Return _codigoSise
        End Get
        Set(ByVal value As Integer)
            _codigoSise = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal codigo As Integer)
        _codigoSise = codigo
    End Sub

End Class

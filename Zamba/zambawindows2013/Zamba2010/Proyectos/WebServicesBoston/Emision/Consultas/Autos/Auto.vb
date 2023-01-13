Public Class Auto

    Private _uso As Uso
    Private _cobertura As Cobertura
    Private _suma As Double
    Private _año As Int16
    Private _patente As String
    Private _marcaModelo As String
    Private _modelo As Modelo

    Public Property Uso() As Uso
        Get
            Return _uso
        End Get
        Set(ByVal value As Uso)
            _uso = value
        End Set
    End Property

    Public Property Cobertura() As Cobertura
        Get
            Return _cobertura
        End Get
        Set(ByVal value As Cobertura)
            _cobertura = value
        End Set
    End Property

    Public Property Suma() As Double
        Get
            Return _suma
        End Get
        Set(ByVal value As Double)
            _suma = value
        End Set
    End Property

    Public Property Año() As Int16
        Get
            Return _año
        End Get
        Set(ByVal value As Int16)
            _año = value
        End Set
    End Property

    Public Property Patente() As String
        Get
            Return _patente
        End Get
        Set(ByVal value As String)
            _patente = value
        End Set
    End Property

    Public Property MarcaModelo() As String
        Get
            Return _marcaModelo
        End Get
        Set(ByVal value As String)
            _marcaModelo = value
        End Set
    End Property

    Public Property Modelo() As Modelo
        Get
            Return _modelo
        End Get
        Set(ByVal value As Modelo)
            _modelo = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal uso As Integer, ByVal cobertura As Integer, _
                   ByVal suma As Double, ByVal año As Int16, ByVal patente As String, _
                   ByVal marcamodelo As String, ByVal modelo As Integer)

        _uso = New Uso(uso)
        _cobertura = New Cobertura(cobertura)
        _suma = suma
        _año = año
        _patente = patente
        _marcaModelo = marcamodelo
        _modelo = New Modelo(modelo)

    End Sub

End Class

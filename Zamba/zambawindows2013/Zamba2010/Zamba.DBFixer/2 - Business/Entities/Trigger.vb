Public Class Trigger

#Region "Atributos"
    Private _name As String
    Private _text As String
    Private _tableTo As String
    Private _Aplication As String
#End Region

#Region "Propiedades"
    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property
    Public Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property
    Public Property TableTo() As String
        Get
            Return _tableTo
        End Get
        Set(ByVal value As String)
            _tableTo = value
        End Set
    End Property


    Public Property Aplication() As String
        Get
            Return _Aplication
        End Get
        Set(ByVal value As String)
            _Aplication = value
        End Set
    End Property


#End Region

#Region "Constructor"
    Public Sub New(ByVal sName As String, ByVal sText As String, ByVal sTableTo As String)
        _name = sName
        _tableTo = sTableTo
        _text = sText
    End Sub
    Public Sub New()
    End Sub
#End Region

End Class

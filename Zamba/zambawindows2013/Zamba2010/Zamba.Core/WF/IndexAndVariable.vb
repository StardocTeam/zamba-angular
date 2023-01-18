Public Class IndexAndVariable
    Private _id As Int64
    Private _name As String
    Private _operator As String

    Public Property ID() As Int64
        Get
            Return _id
        End Get
        Set(ByVal value As Int64)
            _id = value
        End Set
    End Property

    Public Property Name()
        Get
            Return _name
        End Get
        Set(ByVal value)
            _name = value
        End Set
    End Property

    Public Property Operador() As String
        Get
            Return _operator
        End Get
        Set(ByVal value As String)
            _operator = value
        End Set
    End Property

    Public Sub New(ByVal id As Int64, ByVal name As String, ByVal operador As String)
        _id = id
        _name = name
        _operator = operador
    End Sub
End Class
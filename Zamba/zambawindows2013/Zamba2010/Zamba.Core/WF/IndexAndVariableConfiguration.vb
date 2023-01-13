Public Class IndexAndVariableConfiguration
    Private _id As Int64
    Private _manual As String
    Private _name As String
    Private _operator As String
    Private _value As String
    Private _indexName As String

    Public Property ID() As Int64
        Get
            Return _id
        End Get
        Set(ByVal value As Int64)
            _id = value
        End Set
    End Property

    Public WriteOnly Property IndexName()
        Set(ByVal value)
            _indexName = value
        End Set
    End Property


    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public ReadOnly Property MaskName()
        Get
            If Manual = "S" Then
                Return _name & " " & _operator & " " & _value
            Else
                Return _indexName & " " & _operator & " " & _value
            End If
        End Get
    End Property

    Public Property Operador() As String
        Get
            Return _operator
        End Get
        Set(ByVal value As String)
            _operator = value
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

    Public Property Manual() As String
        Get
            Return _manual
        End Get
        Set(ByVal value As String)
            _manual = value
        End Set
    End Property

    Public Sub New(ByVal id As Int64, ByVal manual As String, ByVal name As String, ByVal operador As String, ByVal value As String, ByVal IndexName As String)
        _id = id
        _manual = manual
        _name = name
        _operator = operador
        _value = value
        _indexName = IndexName
    End Sub
End Class
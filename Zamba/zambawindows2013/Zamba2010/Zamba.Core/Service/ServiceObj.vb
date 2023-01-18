Public Class ServiceObj
    Dim _ServiceId As Int32
    Dim _ServiceName As String
    Dim _ServiceType As ServiceTypes
    Dim _Description As String

    Public Sub New(ByVal serviceID As Int32, ByVal serviceName As String, ByVal serviceType As ServiceTypes, ByVal description As String)
        _ServiceId = serviceID
        _ServiceName = serviceName
        _ServiceType = serviceType
        _Description = description
    End Sub


    Public ReadOnly Property ServiceID() As Int32
        Get
            Return _ServiceId
        End Get
    End Property

    Public Property ServiceName() As String
        Get
            Return _ServiceName
        End Get
        Set(ByVal value As String)
            _ServiceName = value
        End Set
    End Property

    Public Property ServiceType() As ServiceTypes
        Get
            Return _ServiceType
        End Get
        Set(ByVal value As ServiceTypes)
            _ServiceType = value
        End Set
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return _Description
        End Get
    End Property
End Class
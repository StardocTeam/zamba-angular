Public Class PublishEvent
    Implements IPublishEvent

    Private _eventId As Integer
    Public Property EventId() As Integer Implements IPublishEvent.EventId
        Get
            Return _eventId
        End Get
        Set(ByVal value As Integer)
            _eventId = value
        End Set
    End Property
    Private _eventName As String
    Public Property EventName() As String Implements IPublishEvent.EventName
        Get
            Return _eventName
        End Get
        Set(ByVal value As String)
            _eventName = value
        End Set
    End Property
End Class
